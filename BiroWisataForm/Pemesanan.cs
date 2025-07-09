using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;
using System.Linq;
using praktikum7;

namespace BiroWisataForm
{
    public partial class Pemesanan : Form
    {
        Koneksi kn = new Koneksi();
        // --- Member Variables ---
        private string connectionString = "";
        private int selectedPemesananId = -1;
        private Timer searchTimer;

        // --- Constructor ---
        public Pemesanan()
        {
            connectionString = kn.connectionString();

            InitializeComponent();
            InitializeDataGridViewSettings();
            InitializeSearchBox(); // Sekarang tanpa timer
            EnsureDatabaseIndexes();
            LoadPelanggan();
            LoadPaketWisata();
            LoadStatusPembayaran();
            LoadStatusPemesananOptions();
        }


        private void EnsureDatabaseIndexes()
        {
            // Menggabungkan semua skrip CREATE INDEX untuk tabel Pemesanan
            string scriptIndex = @"
                IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_Pemesanan_IDPelanggan' AND object_id = OBJECT_ID('dbo.Pemesanan'))
                BEGIN
                    CREATE NONCLUSTERED INDEX IX_Pemesanan_IDPelanggan ON dbo.Pemesanan(IDPelanggan);
                END;

                IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_Pemesanan_IDPaket' AND object_id = OBJECT_ID('dbo.Pemesanan'))
                BEGIN
                    CREATE NONCLUSTERED INDEX IX_Pemesanan_IDPaket ON dbo.Pemesanan(IDPaket);
                END;

                IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_Pemesanan_TanggalPemesanan' AND object_id = OBJECT_ID('dbo.Pemesanan'))
                BEGIN
                    CREATE NONCLUSTERED INDEX IX_Pemesanan_TanggalPemesanan ON dbo.Pemesanan(TanggalPemesanan DESC);
                END;

                IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_Pemesanan_StatusPembayaran' AND object_id = OBJECT_ID('dbo.Pemesanan'))
                BEGIN
                    CREATE NONCLUSTERED INDEX IX_Pemesanan_StatusPembayaran ON dbo.Pemesanan(StatusPembayaran);
                END;
            ";

            try
            {
                using (SqlConnection conn = new SqlConnection(kn.connectionString()))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(scriptIndex, conn))
                    {
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Pengecekan indeks untuk tabel Pemesanan selesai.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal memverifikasi/membuat indeks untuk Pemesanan: {ex.Message}",
                                "Kesalahan Konfigurasi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // --- Form Load ---
        private void Pemesanan_Load(object sender, EventArgs e)
        {
            RefreshData();
            // this.dgvPemesanan.SelectionChanged += new System.EventHandler(this.DgvPemesanan_SelectionChanged);
        }

        // --- Control Initialization ---
        private void InitializeDataGridViewSettings()
        {
            // Set DGV runtime properties. Columns and essential styles are defined in Designer.cs.
            // AutoGenerateColumns = false is set in Designer.cs
            dgvPemesanan.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPemesanan.MultiSelect = false;
            dgvPemesanan.AutoGenerateColumns = false;
            // ReadOnly, AllowUserToAddRows, AllowUserToDeleteRows, RowHeadersVisible etc.
            // are also likely set correctly in Designer.cs based on the code you provided.
            // You could override styles here if needed, but it's cleaner in Designer.cs
        }

        // Ganti metode ini di Pemesanan.cs
        // GANTI metode InitializeSearchBox dengan yang ini
        private void InitializeSearchBox()
        {
            this.txtSearch.TextChanged += (s, e) =>
            {
                // Simpan posisi cursor sebelum search
                int cursorPosition = txtSearch.SelectionStart;

                PerformSearch();

                // Kembalikan focus dan posisi cursor setelah search
                if (!txtSearch.Focused)
                {
                    txtSearch.Focus();
                }
                txtSearch.SelectionStart = cursorPosition;
                txtSearch.SelectionLength = 0;
            };
        }

        // TAMBAHKAN metode BARU ini di mana saja di dalam kelas Pemesanan
        private void PerformSearch()
        {
            if (dgvPemesanan.DataSource is DataTable dt)
            {
                try
                {
                    string filterText = this.txtSearch.Text.Trim()
                        .Replace("'", "''").Replace("%", "[%]").Replace("_", "[_]");

                    if (string.IsNullOrWhiteSpace(filterText))
                    {
                        dt.DefaultView.RowFilter = string.Empty;
                    }
                    else
                    {
                        dt.DefaultView.RowFilter = string.Format(
                            "NamaPelanggan LIKE '%{0}%' OR " +
                            "NamaPaket LIKE '%{0}%' OR " +
                            "StatusPembayaran LIKE '%{0}%' OR " +
                            "StatusPemesanan LIKE '%{0}%' OR " +
                            "CONVERT(TotalPembayaran, 'System.String') LIKE '%{0}%' OR " +
                            "CONVERT(TanggalPemesanan, 'System.String') LIKE '%{0}%'",
                            filterText);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error setting RowFilter: " + ex.Message);
                    dt.DefaultView.RowFilter = string.Empty;
                }
            }
        }


        // --- Data Loading ---
        private void LoadComboBoxData(ComboBox comboBox, string query, string displayMember, string valueMember, string placeholder)
        {
            // This helper method looks good
            using (var conn = new SqlConnection(kn.connectionString()))
            {
                try
                {
                    conn.Open();
                    var dt = new DataTable();
                    var adapter = new SqlDataAdapter(query, conn);
                    adapter.Fill(dt);

                    var emptyRow = dt.NewRow();
                    emptyRow[valueMember] = DBNull.Value;
                    emptyRow[displayMember] = placeholder;
                    dt.Rows.InsertAt(emptyRow, 0);

                    comboBox.DataSource = dt;
                    comboBox.DisplayMember = displayMember;
                    comboBox.ValueMember = valueMember;
                    comboBox.SelectedIndex = 0;
                }
                catch (SqlException ex) { MessageBox.Show($"Error DB Load {displayMember}: {ex.Message}", "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                catch (Exception ex) { MessageBox.Show($"Error Load {displayMember}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void LoadPelanggan()
        {
            LoadComboBoxData(cmbPelanggan, "SELECT IDPelanggan, NamaPelanggan FROM Pelanggan WHERE IsDeleted = 0 ORDER BY NamaPelanggan", "NamaPelanggan", "IDPelanggan", "-- Pilih Pelanggan --");
        }

        private void LoadPaketWisata()
        {
            // Pastikan query mengambil 'Harga'
            string query = "SELECT IDPaket, NamaPaket, Harga FROM PaketWisata WHERE IsDeleted = 0 ORDER BY NamaPaket";

            using (var conn = new SqlConnection(kn.connectionString()))
            {
                try
                {
                    conn.Open();
                    var dt = new DataTable();
                    var adapter = new SqlDataAdapter(query, conn);
                    adapter.Fill(dt);

                    var emptyRow = dt.NewRow();
                    emptyRow["IDPaket"] = DBNull.Value;
                    emptyRow["NamaPaket"] = "-- Pilih Paket Wisata --";
                    // Penting: Tambahkan nilai null untuk harga di baris placeholder
                    emptyRow["Harga"] = DBNull.Value;
                    dt.Rows.InsertAt(emptyRow, 0);

                    cmbPaketWisata.DataSource = dt;
                    cmbPaketWisata.DisplayMember = "NamaPaket";
                    cmbPaketWisata.ValueMember = "IDPaket";
                    cmbPaketWisata.SelectedIndex = 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error DB Load Paket Wisata: {ex.Message}", "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void LoadStatusPembayaran()
        {
            cmbStatus.Items.Clear();
            cmbStatus.Items.Add("-- Pilih Status --");
            cmbStatus.Items.Add("Belum Bayar"); // Match DB Check Constraint
            cmbStatus.SelectedIndex = 0;
            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;

            if (cmbStatus.Items.Count > 0)
            {
                cmbStatus.SelectedIndex = 0;
            }
        }

        // Di dalam file Pemesanan.cs

        private bool RefreshData()
        {
            using (var conn = new SqlConnection(kn.connectionString()))
            {
                try
                {
                    conn.Open();
                    // --- PERUBAHAN DI SINI: Tambahkan kolom tanggal yang diformat ---
                    string query = @"
        SELECT
            p.IDPemesanan, p.IDPelanggan, p.IDPaket,
            p.TanggalPemesanan, p.StatusPembayaran, p.TotalPembayaran,
            pl.NamaPelanggan,
            pw.NamaPaket,
            p.StatusPemesanan,
            CONVERT(VARCHAR, p.TanggalPemesanan, 103) AS TanggalPemesananFormatted
        FROM Pemesanan p
        INNER JOIN Pelanggan pl ON p.IDPelanggan = pl.IDPelanggan
        INNER JOIN PaketWisata pw ON p.IDPaket = pw.IDPaket
        WHERE p.StatusPemesanan != 'Dibatalkan'
        ORDER BY p.TanggalPemesanan DESC, pl.NamaPelanggan";

                    var dt = new DataTable();
                    var adapter = new SqlDataAdapter(query, conn);
                    adapter.Fill(dt);

                    dgvPemesanan.DataSource = null;
                    dgvPemesanan.DataSource = dt;

                    ClearInputs();
                    return true;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Error DB Refresh Pemesanan: {ex.Message}", "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error Refresh Pemesanan: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }


        // --- CRUD Button Event Handlers ---
        private void btnTambah_Click(object sender, EventArgs e)
        {
            // Validasi dan Pengecekan Duplikasi awal (TETAP SAMA)
            if (!ValidateInputs()) return;

            int idPelanggan = Convert.ToInt32(cmbPelanggan.SelectedValue);
            int idPaket = Convert.ToInt32(cmbPaketWisata.SelectedValue);

            try
            {
                using (SqlConnection checkConn = new SqlConnection(kn.connectionString()))
                {
                    string checkQuery = @"SELECT COUNT(1) FROM dbo.Pemesanan WHERE IDPelanggan = @IDPelanggan AND IDPaket = @IDPaket AND StatusPemesanan NOT IN ('Selesai', 'Dibatalkan')";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, checkConn))
                    {
                        checkCmd.Parameters.AddWithValue("@IDPelanggan", idPelanggan);
                        checkCmd.Parameters.AddWithValue("@IDPaket", idPaket);
                        checkConn.Open();
                        if (Convert.ToInt32(checkCmd.ExecuteScalar()) > 0)
                        {
                            MessageBox.Show("Pelanggan ini sudah memiliki pemesanan aktif untuk paket wisata yang sama.\nSelesaikan atau batalkan pemesanan sebelumnya untuk bisa memesan lagi.", "Duplikasi Pemesanan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi error saat memeriksa duplikasi data: {ex.Message}", "Error Validasi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Persiapan data untuk transaksi
            DateTime tanggalPemesanan = dtpTanggalPesan.Value;
            string statusPembayaran = cmbStatus.SelectedItem.ToString();
            decimal totalPembayaran = decimal.Parse(txtTotal.Text, CultureInfo.InvariantCulture);
            string createdBy = Environment.UserName;

            // Logika transaksi yang sudah disederhanakan
            SqlConnection conn = new SqlConnection(kn.connectionString());
            SqlTransaction transaction = null;

            try
            {
                conn.Open();
                transaction = conn.BeginTransaction();

                // Cek Kuota
                int kuotaTersedia;
                string checkKuotaQuery = "SELECT Kuota FROM dbo.PaketWisata WHERE IDPaket = @IDPaket";
                using (SqlCommand checkKuotaCmd = new SqlCommand(checkKuotaQuery, conn, transaction))
                {
                    checkKuotaCmd.Parameters.AddWithValue("@IDPaket", idPaket);
                    object result = checkKuotaCmd.ExecuteScalar();
                    kuotaTersedia = (result != DBNull.Value) ? Convert.ToInt32(result) : 0;
                }

                // =======================================================
                // --- PERUBAHAN UTAMA DI SINI ---
                // Jika kuota habis, lemparkan error untuk ditangkap oleh blok 'catch'
                if (kuotaTersedia <= 0)
                {
                    throw new InvalidOperationException("Maaf, kuota untuk paket wisata ini sudah habis.");
                }
                // =======================================================

                // Jika kode berlanjut, berarti kuota aman. Lakukan proses INSERT dan UPDATE.
                // Blok 'else' yang berisi Rollback() sudah dihapus.

                // INSERT Pemesanan
                string insertQuery = @"INSERT INTO dbo.Pemesanan (IDPelanggan, IDPaket, TanggalPemesanan, StatusPembayaran, TotalPembayaran, StatusPemesanan, CreatedBy, UpdatedBy) VALUES (@IDPelanggan, @IDPaket, @TanggalPemesanan, @StatusPembayaran, @TotalPembayaran, 'Menunggu', @CreatedBy, @CreatedBy);";
                using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn, transaction))
                {
                    insertCmd.Parameters.AddWithValue("@IDPelanggan", idPelanggan);
                    insertCmd.Parameters.AddWithValue("@IDPaket", idPaket);
                    insertCmd.Parameters.AddWithValue("@TanggalPemesanan", tanggalPemesanan);
                    insertCmd.Parameters.AddWithValue("@StatusPembayaran", statusPembayaran);
                    insertCmd.Parameters.AddWithValue("@TotalPembayaran", totalPembayaran);
                    insertCmd.Parameters.AddWithValue("@CreatedBy", createdBy);
                    insertCmd.ExecuteNonQuery();
                }

                // UPDATE Kuota
                string updateQuery = "UPDATE dbo.PaketWisata SET Kuota = Kuota - 1 WHERE IDPaket = @IDPaket;";
                using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn, transaction))
                {
                    updateCmd.Parameters.AddWithValue("@IDPaket", idPaket);
                    updateCmd.ExecuteNonQuery();
                }

                // Hanya jika semua perintah di atas berhasil, commit transaksi
                transaction.Commit();
                MessageBox.Show("Data pemesanan berhasil ditambahkan! Kuota telah diperbarui.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshData();
            }
            catch (Exception ex)
            {
                // =======================================================
                // --- INI SATU-SATUNYA TEMPAT UNTUK ROLLBACK ---
                // =======================================================
                // Menangkap SEMUA error, termasuk error kuota habis yang kita lempar.
                MessageBox.Show($"Transaksi gagal dan semua perubahan telah dibatalkan.\n\nPesan: {ex.Message}", "Transaksi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                transaction?.Rollback(); // Tanda '?' aman jika transaksi null
            }
            finally
            {
                // Selalu tutup koneksi pada akhirnya
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        // Ganti metode ini di Pemesanan.cs

        private void btnUbah_Click(object sender, EventArgs e)
        {
            if (selectedPemesananId < 0)
            {
                MessageBox.Show("Pilih pemesanan yang akan diubah detailnya!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInputs()) return;

            // *** TAMBAHAN BARU: Cek apakah ada perubahan data ***
            if (!HasDataChanged())
            {
                MessageBox.Show("Tidak ada perubahan data untuk disimpan.", "Informasi",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            // *** AKHIR TAMBAHAN ***

            int newIdPaket = Convert.ToInt32(cmbPaketWisata.SelectedValue);
            int newIdPelanggan = Convert.ToInt32(cmbPelanggan.SelectedValue);

            // Pengecekan Duplikasi Langsung di Dalam Tombol
            try
            {
                using (SqlConnection checkConn = new SqlConnection(kn.connectionString()))
                {
                    string checkQuery = @"SELECT COUNT(1) FROM dbo.Pemesanan
                                  WHERE IDPelanggan = @IDPelanggan
                                    AND IDPaket = @IDPaket
                                    AND StatusPemesanan NOT IN ('Selesai', 'Dibatalkan')
                                    AND IDPemesanan != @IDPemesananToExclude";

                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, checkConn))
                    {
                        checkCmd.Parameters.AddWithValue("@IDPelanggan", newIdPelanggan);
                        checkCmd.Parameters.AddWithValue("@IDPaket", newIdPaket);
                        checkCmd.Parameters.AddWithValue("@IDPemesananToExclude", selectedPemesananId);

                        checkConn.Open();
                        int existingCount = Convert.ToInt32(checkCmd.ExecuteScalar());
                        checkConn.Close();

                        if (existingCount > 0)
                        {
                            MessageBox.Show("Pelanggan ini sudah memiliki pemesanan aktif untuk paket wisata yang sama.",
                                            "Duplikasi Pemesanan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi error saat memeriksa duplikasi data: {ex.Message}", "Error Validasi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // ... sisa kode tetap sama ...
            // === Akhir Blok Pengecekan Duplikasi ===

            DateTime newTanggalPemesanan = dtpTanggalPesan.Value;
            decimal newTotalPembayaran = decimal.Parse(txtTotal.Text, CultureInfo.InvariantCulture);
            string updatedBy = Environment.UserName;

            // Logika transaksi untuk mengubah data dan menyesuaikan kuota tetap sama
            SqlConnection conn = new SqlConnection(kn.connectionString());
            SqlTransaction transaction = null;

            try
            {
                // ... (Sisa kode untuk transaksi UPDATE Anda tidak berubah)
                // (Salin sisa kode dari metode btnUbah_Click Anda sebelumnya di sini)
                conn.Open();
                transaction = conn.BeginTransaction();

                // Dapatkan IDPaket LAMA
                int oldIdPaket;
                string getOldPaketIdQuery = "SELECT IDPaket FROM dbo.Pemesanan WHERE IDPemesanan = @IDPemesanan";
                using (SqlCommand getOldIdCmd = new SqlCommand(getOldPaketIdQuery, conn, transaction))
                {
                    getOldIdCmd.Parameters.AddWithValue("@IDPemesanan", selectedPemesananId);
                    object result = getOldIdCmd.ExecuteScalar();
                    if (result == null || result == DBNull.Value) throw new InvalidOperationException("Pemesanan yang akan diubah tidak ditemukan.");
                    oldIdPaket = Convert.ToInt32(result);
                }

                // Cek apakah IDPaket berubah
                if (oldIdPaket != newIdPaket)
                {
                    // Kembalikan kuota paket LAMA
                    string returnOldKuotaQuery = "UPDATE dbo.PaketWisata SET Kuota = Kuota + 1 WHERE IDPaket = @OldIDPaket";
                    using (SqlCommand returnCmd = new SqlCommand(returnOldKuotaQuery, conn, transaction))
                    {
                        returnCmd.Parameters.AddWithValue("@OldIDPaket", oldIdPaket);
                        returnCmd.ExecuteNonQuery();
                    }

                    // Kurangi kuota paket BARU
                    string checkAndTakeNewKuotaQuery = "UPDATE dbo.PaketWisata SET Kuota = Kuota - 1 WHERE IDPaket = @NewIDPaket AND Kuota > 0";
                    using (SqlCommand takeCmd = new SqlCommand(checkAndTakeNewKuotaQuery, conn, transaction))
                    {
                        takeCmd.Parameters.AddWithValue("@NewIDPaket", newIdPaket);
                        if (takeCmd.ExecuteNonQuery() == 0) throw new InvalidOperationException("Kuota untuk paket wisata baru yang dipilih sudah habis.");
                    }
                }

                // Update data utama di tabel Pemesanan
                string updatePemesananQuery = @"UPDATE dbo.Pemesanan SET IDPelanggan = @IDPelanggan, IDPaket = @IDPaket, TanggalPemesanan = @TanggalPemesanan, TotalPembayaran = @TotalPembayaran, UpdatedAt = GETDATE(), UpdatedBy = @UpdatedBy WHERE IDPemesanan = @IDPemesanan";
                using (SqlCommand updatePemesananCmd = new SqlCommand(updatePemesananQuery, conn, transaction))
                {
                    updatePemesananCmd.Parameters.AddWithValue("@IDPemesanan", selectedPemesananId);
                    updatePemesananCmd.Parameters.AddWithValue("@IDPelanggan", newIdPelanggan);
                    updatePemesananCmd.Parameters.AddWithValue("@IDPaket", newIdPaket);
                    updatePemesananCmd.Parameters.AddWithValue("@TanggalPemesanan", newTanggalPemesanan);
                    updatePemesananCmd.Parameters.AddWithValue("@TotalPembayaran", newTotalPembayaran);
                    updatePemesananCmd.Parameters.AddWithValue("@UpdatedBy", updatedBy);
                    updatePemesananCmd.ExecuteNonQuery();
                }

                transaction.Commit();
                MessageBox.Show("Data pemesanan berhasil diubah!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Operasi gagal, semua perubahan dibatalkan: {ex.Message}", "Transaksi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                transaction?.Rollback();
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        // Ganti metode ini di Pemesanan.cs

        // Ganti metode ini di file Pemesanan.cs
        // Kode di file Pemesanan.cs ini sudah benar dan tidak perlu diubah.
        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (selectedPemesananId < 0)
            {
                MessageBox.Show("Pilih pemesanan yang akan dibatalkan!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string currentStatus = dgvPemesanan.SelectedRows[0].Cells["colStatusPemesanan"].Value?.ToString();
            if (currentStatus == "Dibatalkan" || currentStatus == "Selesai")
            {
                MessageBox.Show($"Pemesanan yang sudah '{currentStatus}' tidak dapat dibatalkan lagi.", "Aksi Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Apakah Anda yakin ingin membatalkan pemesanan ini?\n- Data pembayaran terkait (jika ada) akan dihapus.\n- Kuota paket akan dikembalikan.", "Konfirmasi Pembatalan", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            SqlConnection conn = new SqlConnection(kn.connectionString());
            SqlTransaction transaction = null;

            try
            {
                conn.Open();
                transaction = conn.BeginTransaction();

                // Ambil IDPaket untuk mengembalikan kuota
                int idPaket;
                string getPaketIdQuery = "SELECT IDPaket FROM dbo.Pemesanan WHERE IDPemesanan = @IDPemesanan";
                using (SqlCommand getIdCmd = new SqlCommand(getPaketIdQuery, conn, transaction))
                {
                    getIdCmd.Parameters.AddWithValue("@IDPemesanan", selectedPemesananId);
                    object result = getIdCmd.ExecuteScalar();
                    if (result == null || result == DBNull.Value)
                    {
                        throw new InvalidOperationException("Pemesanan tidak ditemukan.");
                    }
                    idPaket = Convert.ToInt32(result);
                }

                // Hapus semua data pembayaran yang terkait
                string deletePembayaranQuery = "DELETE FROM dbo.Pembayaran WHERE IDPemesanan = @IDPemesanan";
                using (SqlCommand deletePembayaranCmd = new SqlCommand(deletePembayaranQuery, conn, transaction))
                {
                    deletePembayaranCmd.Parameters.AddWithValue("@IDPemesanan", selectedPemesananId);
                    deletePembayaranCmd.ExecuteNonQuery();
                }

                // Kembalikan kuota paket wisata
                string updateKuotaQuery = "UPDATE dbo.PaketWisata SET Kuota = Kuota + 1 WHERE IDPaket = @IDPaket";
                using (SqlCommand updateKuotaCmd = new SqlCommand(updateKuotaQuery, conn, transaction))
                {
                    updateKuotaCmd.Parameters.AddWithValue("@IDPaket", idPaket);
                    updateKuotaCmd.ExecuteNonQuery();
                }

                // Panggil Stored Procedure untuk mengubah status
                // BARIS INI TIDAK AKAN ERROR LAGI
                using (var cmd = new SqlCommand("sp_CancelPemesanan", conn, transaction))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDPemesanan", selectedPemesananId);
                    cmd.Parameters.AddWithValue("@UpdatedBy", Environment.UserName);
                    cmd.ExecuteNonQuery();
                }

                transaction.Commit();

                MessageBox.Show("Pemesanan berhasil dibatalkan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan, semua perubahan dibatalkan: {ex.Message}", "Transaksi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                transaction?.Rollback(); // Aman untuk memanggil rollback
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            // Hapus dulu isi kotak pencarian
            if (!string.IsNullOrEmpty(this.txtSearch.Text))
            {
                this.txtSearch.Clear();
            }

            // --- PERUBAHAN DI SINI ---
            // Panggil RefreshData dan periksa apakah berhasil (return true)
            if (RefreshData())
            {
                // Jika berhasil, tampilkan pesan sukses
                MessageBox.Show("Data berhasil dimuat ulang.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            // Jika gagal, RefreshData() sudah menampilkan pesan errornya sendiri.
        }


        // --- Helper Methods ---
        // Di dalam Pemesanan.cs
        // Di dalam Pemesanan.cs
        private bool ValidateInputs()
        {
            this.errorProvider1.Clear();
            bool isValid = true;

            if (cmbPelanggan.SelectedValue == null || cmbPelanggan.SelectedValue == DBNull.Value)
            {
                this.errorProvider1.SetError(cmbPelanggan, "Pilih Pelanggan!");
                isValid = false;
            }

            if (cmbPaketWisata.SelectedValue == null || cmbPaketWisata.SelectedValue == DBNull.Value)
            {
                this.errorProvider1.SetError(cmbPaketWisata, "Pilih Paket Wisata!");
                isValid = false;
            }

            // --- TAMBAHKAN VALIDASI INI ---
            if (cmbStatus.SelectedIndex <= 0) // Index 0 adalah "-- Pilih Status --"
            {
                this.errorProvider1.SetError(cmbStatus, "Status Pembayaran harus dipilih!");
                isValid = false;
            }
            // -----------------------------

            if (string.IsNullOrWhiteSpace(txtTotal.Text))
            {
                this.errorProvider1.SetError(cmbPaketWisata, "Pilih paket wisata yang valid untuk mendapatkan harga!");
                isValid = false;
            }

            if (!isValid)
            {
                MessageBox.Show("Harap perbaiki data yang tidak valid.", "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return isValid;
        }

        private void LoadStatusPemesananOptions()
        {
            cmbUbahStatusPemesanan.Items.Clear();
            cmbUbahStatusPemesanan.Items.Add("-- Pilih Status Baru --"); // Placeholder
                                                                         // These should match your CK_StatusPemesanan constraint in the DB
            cmbUbahStatusPemesanan.Items.Add("Menunggu");
            cmbUbahStatusPemesanan.Items.Add("Dikonfirmasi");
            cmbUbahStatusPemesanan.Items.Add("Selesai");
            cmbUbahStatusPemesanan.Items.Add("Dibatalkan"); // "Dibatalkan" is handled by btnHapus
            cmbUbahStatusPemesanan.SelectedIndex = 0;
            cmbUbahStatusPemesanan.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void ClearInputs()
        {
            selectedPemesananId = -1;
            cmbPelanggan.SelectedIndex = 0;
            cmbPaketWisata.SelectedIndex = 0;
            //cmbStatus.SelectedIndex = 0;
            dtpTanggalPesan.Value = DateTime.Now;
            txtTotal.Clear();
            if (cmbUbahStatusPemesanan.Items.Count > 0) cmbUbahStatusPemesanan.SelectedIndex = 0; // Reset new ComboBox
            dgvPemesanan.ClearSelection();
            dgvPemesanan.ClearSelection();
            cmbPelanggan.Focus();
            errorProvider1.Clear();
        }

        private void HandleGeneralError(string operation, Exception ex)
        {
            MessageBox.Show($"Error saat {operation}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void HandleSqlError(SqlException ex, string operation)
        {
            string userMessage = $"Error Database saat {operation} data: Terjadi masalah umum.";

            // Check for CHECK constraint violations FIRST
            if (ex.Message.ToUpper().Contains("CHECK CONSTRAINT"))
            {
                string constraintName = "Unknown CHECK";
                string columnName = "Unknown Column";

                // Try to parse constraint name (usually between "constraint \"" and "\"")
                int CkStartIndex = ex.Message.IndexOf("constraint \"") + "constraint \"".Length;
                int CkEndIndex = ex.Message.IndexOf("\"", CkStartIndex);
                if (CkStartIndex > "constraint \"".Length - 1 && CkEndIndex > CkStartIndex)
                {
                    constraintName = ex.Message.Substring(CkStartIndex, CkEndIndex - CkStartIndex);
                }

                // Try to parse column name (usually between "column '" and "'")
                int ColStartIndex = ex.Message.IndexOf("column '") + "column '".Length;
                int ColEndIndex = ex.Message.IndexOf("'", ColStartIndex);
                if (ColStartIndex > "column '".Length - 1 && ColEndIndex > ColStartIndex)
                {
                    columnName = ex.Message.Substring(ColStartIndex, ColEndIndex - ColStartIndex);
                }

                MessageBox.Show($"Full CHECK Constraint Violation Message: {ex.Message}\n\nParsed Constraint Name: '{constraintName}'\nParsed Column Name: '{columnName}'",
                                "Detailed CHECK Constraint Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                userMessage = $"Error Validasi Data: Nilai untuk kolom '{columnName}' tidak valid. Pastikan nilai sesuai aturan database (Pelanggaran pada aturan: {constraintName}).";
                MessageBox.Show(userMessage, "Kesalahan Validasi Data (Aturan Database)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            // Then check for Foreign Key violations
            else if (ex.Message.ToUpper().Contains("FOREIGN KEY"))
            {
                MessageBox.Show($"Full FK Violation Message: {ex.Message}\n\nPlease report this full message.", "Detailed FK Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                string constraintName = "Unknown FK";
                // FK names are often between "constraint \"" and "\"."
                int fkStartIndex = ex.Message.IndexOf("constraint \"") + "constraint \"".Length;
                int fkEndIndex = ex.Message.IndexOf("\".", fkStartIndex); // Note the "." at the end of the closing quote for FKs
                if (fkStartIndex > "constraint \"".Length - 1 && fkEndIndex > fkStartIndex)
                {
                    constraintName = ex.Message.Substring(fkStartIndex, fkEndIndex - fkStartIndex);
                }
                else // Fallback for slightly different FK message structures if any
                {
                    string[] parts = ex.Message.Split('\''); // FK_Constraint_Name - Corrected: declared parts
                    if (parts.Length > 1) constraintName = parts[1];
                }


                if (ex.Message.Contains("FK_Pemesanan_IDPel")) userMessage = $"Error: Pelanggan yang dipilih tidak valid/tidak ada (Constraint: {constraintName}).";
                else if (ex.Message.Contains("FK_Pemesanan_IDPak")) userMessage = $"Error: Paket Wisata yang dipilih tidak valid/tidak ada (Constraint: {constraintName}).";
                else if (ex.Message.Contains("FK_Pembayara_IDPem")) userMessage = $"Error Hapus: Pemesanan ini memiliki data Pembayaran terkait (Constraint: {constraintName}).";
                else
                {
                    userMessage = $"Error Relasi (Foreign Key): Data terkait tidak valid atau relasi terganggu (Constraint: {constraintName}).";
                }
                MessageBox.Show(userMessage, "Kesalahan Relasi Data (Foreign Key)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else // Other SQL errors
            {
                MessageBox.Show($"Error Database Lainnya (Nomor: {ex.Number}) saat {operation} data: {ex.Message}", "Database Error Umum", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // --- DataGridView Event Handlers ---
        private void PopulateFieldsFromSelectedRow()
        {
            if (dgvPemesanan.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvPemesanan.SelectedRows[0];

                // Get ID from the specific DGV column name
                // Using Cells["colIDPemesanan"] because that's the Name property we set in Designer.cs
                if (row.Cells["colIDPemesanan"].Value != null && int.TryParse(row.Cells["colIDPemesanan"].Value.ToString(), out int id))
                {
                    selectedPemesananId = id;
                }
                else
                {
                    selectedPemesananId = -1;
                    ClearInputs(); // If ID is invalid, clear everything
                    return;
                }

                // Populate main input fields based on the selected row:
                // Use the hidden columns for foreign keys for ComboBoxes
                cmbPelanggan.SelectedValue = row.Cells["colIDPelanggan_hidden"].Value ?? DBNull.Value;
                cmbPaketWisata.SelectedValue = row.Cells["colIDPaket_hidden"].Value ?? DBNull.Value;

                // Set StatusPembayaran ComboBox (cmbStatus)
                //string statusPembayaranValue = row.Cells["colStatusPembayaran"].Value?.ToString();
                //cmbStatus.SelectedItem = statusPembayaranValue;
                //if (cmbStatus.SelectedIndex < 0 && cmbStatus.Items.Count > 0) // If not found, reset to placeholder
                //{
                //    cmbStatus.SelectedIndex = 0;
                //}

                // Set DateTimePicker for Tanggal Pemesanan (dtpTanggalPesan)
                object tglValue = row.Cells["colTanggalPemesanan"].Value;
                if (tglValue != null && tglValue != DBNull.Value && DateTime.TryParse(tglValue.ToString(), out DateTime tgl))
                {
                    dtpTanggalPesan.Value = tgl;
                }
                else
                {
                    dtpTanggalPesan.Value = DateTime.Now; // Default if null or invalid
                }

                // Set Total Pembayaran TextBox
                txtTotal.Text = row.Cells["colTotalPembayaran"].Value?.ToString() ?? "0";


                // For the NEW ComboBox (cmbUbahStatusPemesanan) meant for CHANGING StatusPemesanan:
                // Reset it to its placeholder because the user will pick a NEW status to process.
                // The *current* StatusPemesanan is visible in the grid in its own column.
                if (this.Controls.Find("cmbUbahStatusPemesanan", true).FirstOrDefault() is ComboBox cmbUbahStatus)
                {
                    if (cmbUbahStatus.Items.Count > 0)
                    {
                        cmbUbahStatus.SelectedIndex = 0; // Reset to "-- Pilih Status Baru --"
                    }
                }

                this.errorProvider1.Clear(); // Use the form's errorProvider1
            }
            else
            {
                // Optional: if you want to clear inputs when no row is selected after a deselection action
                ClearInputs();
            }
        }

        private void btnProsesStatusPemesanan_Click(object sender, EventArgs e)
        {
            if (selectedPemesananId < 0)
            {
                MessageBox.Show("Pilih pemesanan yang statusnya akan diubah!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbUbahStatusPemesanan.SelectedIndex <= 0) // Index 0 is "-- Pilih Status Baru --"
            {
                errorProvider1.SetError(cmbUbahStatusPemesanan, "Pilih status baru untuk diproses!");
                MessageBox.Show("Pilih status baru yang valid dari dropdown.", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbUbahStatusPemesanan.Focus();
                return;
            }
            errorProvider1.SetError(cmbUbahStatusPemesanan, ""); // Clear error if any

            string newStatusPemesanan = cmbUbahStatusPemesanan.SelectedItem.ToString();
            string currentStatusPemesananInGrid = "";

            // Get current status from grid to prevent invalid transitions if needed (optional advanced logic)
            if (dgvPemesanan.SelectedRows.Count > 0)
            {
                currentStatusPemesananInGrid = dgvPemesanan.SelectedRows[0].Cells["colStatusPemesanan"].Value?.ToString() ?? "";
            }

            // Optional: Add logic here to prevent invalid status transitions, e.g.,
            // if (currentStatusPemesananInGrid == "Selesai" && newStatusPemesanan != "Selesai") {
            //     MessageBox.Show("Pemesanan yang sudah Selesai tidak bisa diubah statusnya lagi.", "Aksi Tidak Valid"); return;
            // }
            // if (currentStatusPemesananInGrid == "Dibatalkan" && newStatusPemesanan != "Dibatalkan") {
            //     MessageBox.Show("Pemesanan yang sudah Dibatalkan tidak bisa diubah statusnya lagi.", "Aksi Tidak Valid"); return;
            // }

            if (MessageBox.Show($"Yakin ingin mengubah status pemesanan ID {selectedPemesananId} menjadi '{newStatusPemesanan}'?",
                                 "Konfirmasi Ubah Status", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (var conn = new SqlConnection(kn.connectionString()))
                {
                    try
                    {
                        conn.Open();
                        string sql = @"UPDATE Pemesanan SET
                                       StatusPemesanan = @StatusPesan,
                                       UpdatedAt = @UpdatedAt,
                                       UpdatedBy = @UpdatedBy
                                   WHERE IDPemesanan = @IDPemesanan";
                        using (var cmd = new SqlCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("@StatusPesan", newStatusPemesanan);
                            cmd.Parameters.AddWithValue("@IDPemesanan", selectedPemesananId);
                            cmd.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);
                            cmd.Parameters.AddWithValue("@UpdatedBy", Environment.UserName);

                            if (cmd.ExecuteNonQuery() > 0)
                            {
                                MessageBox.Show($"Status pemesanan berhasil diubah menjadi '{newStatusPemesanan}'.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                RefreshData(); // Refresh to show the updated status
                            }
                            else { MessageBox.Show("Gagal mengubah status (data tidak ditemukan atau tidak ada perubahan).", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                        }
                    }
                    catch (SqlException ex) { HandleSqlError(ex, "mengubah status pemesanan"); }
                    catch (Exception ex) { MessageBox.Show($"Error saat mengubah status pemesanan: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
        }


        private void DgvPemesanan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Selecting the row is preferred over cell click for FullRowSelect mode
            if (e.RowIndex >= 0)
            {
                // Ensure the row becomes selected, SelectionChanged will handle populating
                if (!dgvPemesanan.Rows[e.RowIndex].Selected)
                {
                    dgvPemesanan.ClearSelection();
                    dgvPemesanan.Rows[e.RowIndex].Selected = true;
                }
            }
        }

        // Ini adalah metode baru yang dibuat oleh Visual Studio
        private void cmbPaketWisata_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Pastikan item yang dipilih bukan placeholder "-- Pilih Paket Wisata --"
            if (cmbPaketWisata.SelectedValue != null && cmbPaketWisata.SelectedValue != DBNull.Value)
            {
                // Ambil seluruh baris data dari item yang dipilih
                DataRowView drv = cmbPaketWisata.SelectedItem as DataRowView;

                if (drv != null && drv.Row["Harga"] != DBNull.Value)
                {
                    // Ambil harga dari DataRowView
                    decimal harga = Convert.ToDecimal(drv.Row["Harga"]);

                    // Format harga ke dalam TextBox "Total Pembayaran"
                    // Menggunakan "N0" akan memformatnya dengan pemisah ribuan tanpa desimal (misal: 5,000,000)
                    txtTotal.Text = harga.ToString("F2", CultureInfo.InvariantCulture);
                }
                else
                {
                    // Jika tidak ada harga, kosongkan TextBox
                    txtTotal.Clear();
                }
            }
            else
            {
                // Jika user memilih placeholder, kosongkan TextBox
                txtTotal.Clear();
            }
        }

        // Primary event handler for populating fields
        private void DgvPemesanan_SelectionChanged(object sender, EventArgs e)
        {
            PopulateFieldsFromSelectedRow();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        // Letakkan method ini bersama dengan event handler tombol lainnya (seperti btnTambah_Click)

        private void btnKembali_Click(object sender, EventArgs e)
        {
            // Perintah ini akan menutup form 'Kendaraan' saat ini,
            // dan mengembalikan kontrol ke form yang membukanya (yaitu MenuAdmin).
            this.Close();
        }

        private void dgvPemesanan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        /// <summary>
        /// Mengecek apakah ada perubahan data antara form input dengan data di grid
        /// </summary>
        /// <returns>True jika ada perubahan, False jika tidak ada perubahan</returns>
        private bool HasDataChanged()
        {
            if (dgvPemesanan.SelectedRows.Count == 0) return false;

            try
            {
                DataGridViewRow selectedRow = dgvPemesanan.SelectedRows[0];

                // Ambil data dari grid (data asli)
                var gridIdPelanggan = selectedRow.Cells["colIDPelanggan_hidden"].Value?.ToString();
                var gridIdPaket = selectedRow.Cells["colIDPaket_hidden"].Value?.ToString();
                var gridTotalPembayaran = selectedRow.Cells["colTotalPembayaran"].Value?.ToString();

                // Format tanggal untuk perbandingan (hanya tanggal, bukan waktu)
                var gridTanggalPemesanan = "";
                if (selectedRow.Cells["colTanggalPemesanan"].Value != null && selectedRow.Cells["colTanggalPemesanan"].Value != DBNull.Value)
                {
                    gridTanggalPemesanan = Convert.ToDateTime(selectedRow.Cells["colTanggalPemesanan"].Value).ToString("yyyy-MM-dd");
                }

                // Ambil data dari form input (data yang akan disimpan)
                var formIdPelanggan = cmbPelanggan.SelectedValue?.ToString();
                var formIdPaket = cmbPaketWisata.SelectedValue?.ToString();
                var formTotalPembayaran = txtTotal.Text.Trim();
                var formTanggalPemesanan = dtpTanggalPesan.Value.ToString("yyyy-MM-dd");

                // Bandingkan setiap field
                bool pelangganChanged = gridIdPelanggan != formIdPelanggan;
                bool paketChanged = gridIdPaket != formIdPaket;
                bool totalChanged = gridTotalPembayaran != formTotalPembayaran;
                bool tanggalChanged = gridTanggalPemesanan != formTanggalPemesanan;

                // Return true jika ada minimal 1 field yang berubah
                return pelangganChanged || paketChanged || totalChanged || tanggalChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saat mengecek perubahan data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true; // Jika error, anggap ada perubahan untuk safety
            }
        }


    }
}