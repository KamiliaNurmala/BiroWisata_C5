using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;
using System.Linq;

namespace BiroWisataForm
{
    public partial class Pemesanan : Form
    {
        // --- Member Variables ---
        private string connectionString = @"Data Source=KAMILIA\KAMILIANURMALA;Initial Catalog=BiroWisata;Integrated Security=True;";
        private int selectedPemesananId = -1;

        // --- Constructor ---
        public Pemesanan()
        {
            InitializeComponent(); // Designer initializes controls and DGV columns structure
            InitializeDataGridViewSettings(); // Set additional runtime DGV properties
            InitializeSearchBox();
            LoadPelanggan();
            LoadPaketWisata();
            LoadStatusPembayaran();
            LoadStatusPemesananOptions();
            // RefreshData called in Load event
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

        private void InitializeSearchBox()
        {
            // txtSearch should be initialized by the Designer
            this.txtSearch.TextChanged += (s, e) =>
            {
                if (dgvPemesanan.DataSource is DataTable dt)
                {
                    try
                    {
                        string filterText = this.txtSearch.Text.Trim()
                            .Replace("'", "''").Replace("%", "[%]").Replace("_", "[_]");

                        // Filter on columns available from the RefreshData query's JOIN results
                        dt.DefaultView.RowFilter = string.Format(
                            "NamaPelanggan LIKE '%{0}%' OR NamaPaket LIKE '%{0}%' OR StatusPembayaran LIKE '%{0}%'",
                            filterText);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error setting RowFilter: " + ex.Message);
                        dt.DefaultView.RowFilter = string.Empty;
                    }
                }
            };
        }


        // --- Data Loading ---
        private void LoadComboBoxData(ComboBox comboBox, string query, string displayMember, string valueMember, string placeholder)
        {
            // This helper method looks good
            using (var conn = new SqlConnection(connectionString))
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

            using (var conn = new SqlConnection(connectionString))
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

        private bool RefreshData() // <-- DIUBAH dari void menjadi bool
        {
            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"
            SELECT
                p.IDPemesanan, p.IDPelanggan, p.IDPaket,
                p.TanggalPemesanan, p.StatusPembayaran, p.TotalPembayaran,
                pl.NamaPelanggan,
                pw.NamaPaket,
                p.StatusPemesanan
            FROM Pemesanan p
            INNER JOIN Pelanggan pl ON p.IDPelanggan = pl.IDPelanggan
            INNER JOIN PaketWisata pw ON p.IDPaket = pw.IDPaket
            ORDER BY p.TanggalPemesanan DESC, pl.NamaPelanggan";

                    var dt = new DataTable();
                    var adapter = new SqlDataAdapter(query, conn);
                    adapter.Fill(dt);

                    dgvPemesanan.DataSource = null;
                    dgvPemesanan.DataSource = dt;

                    ClearInputs();
                    return true; // <-- DITAMBAHKAN: Kembalikan true jika sukses
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Error DB Refresh Pemesanan: {ex.Message}", "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false; // <-- DITAMBAHKAN: Kembalikan false jika gagal
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error Refresh Pemesanan: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false; // <-- DITAMBAHKAN: Kembalikan false jika gagal
                }
            }
            // ClearInputs() dipindah ke dalam blok 'try' agar hanya dijalankan saat refresh berhasil.
        }

        // --- CRUD Button Event Handlers ---
        private void btnTambah_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            // Ambil nilai dari input form
            int idPelanggan = Convert.ToInt32(cmbPelanggan.SelectedValue);
            int idPaket = Convert.ToInt32(cmbPaketWisata.SelectedValue);
            DateTime tanggalPemesanan = dtpTanggalPesan.Value;
             string statusPembayaran = cmbStatus.SelectedItem.ToString();
            decimal totalPembayaran = decimal.Parse(txtTotal.Text, CultureInfo.InvariantCulture);
            string createdBy = Environment.UserName;

            // Objek koneksi harus dideklarasikan di luar try-catch agar bisa diakses di catch
            SqlConnection conn = new SqlConnection(connectionString);
            SqlTransaction transaction = null; // Deklarasikan transaction di sini

            try
            {
                // 1. Buka Koneksi
                conn.Open();

                // 2. Mulai Transaksi
                // Mulai transaksi DARI koneksi yang sudah terbuka
                transaction = conn.BeginTransaction();

                // --- TAHAP 1: Cek Kuota ---
                int kuotaTersedia;
                string checkKuotaQuery = "SELECT Kuota FROM dbo.PaketWisata WHERE IDPaket = @IDPaket";

                // Penting: Semua command di dalam transaksi HARUS menggunakan objek koneksi dan transaksi yang sama
                using (SqlCommand checkCmd = new SqlCommand(checkKuotaQuery, conn, transaction))
                {
                    checkCmd.Parameters.AddWithValue("@IDPaket", idPaket);
                    // ExecuteScalar digunakan untuk mengambil satu nilai (kuota)
                    object result = checkCmd.ExecuteScalar();

                    // Jika paket tidak ditemukan atau kuota null, anggap 0
                    kuotaTersedia = (result != null && result != DBNull.Value) ? Convert.ToInt32(result) : 0;
                }

                // --- TAHAP 2: Logika Pengecekan di C# ---
                if (kuotaTersedia > 0)
                {
                    // Kuota ada, lanjutkan dengan INSERT dan UPDATE

                    // --- Command 1: INSERT ke tabel Pemesanan ---
                    // PERUBAHAN: Hardcode 'Belum Bayar' di sini
                    string insertQuery = @"INSERT INTO dbo.Pemesanan
                                    (IDPelanggan, IDPaket, TanggalPemesanan, StatusPembayaran, TotalPembayaran, StatusPemesanan, CreatedBy, UpdatedBy)
                                VALUES
                                    (@IDPelanggan, @IDPaket, @TanggalPemesanan, @StatusPembayaran, @TotalPembayaran, 'Menunggu', @CreatedBy, @CreatedBy);";

                    using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn, transaction))
                    {
                        insertCmd.Parameters.AddWithValue("@IDPelanggan", idPelanggan);
                        insertCmd.Parameters.AddWithValue("@IDPaket", idPaket); // ==> KESALAHAN SAYA SEBELUMNYA DI SINI. INI HARUS ADA.
                        insertCmd.Parameters.AddWithValue("@TanggalPemesanan", tanggalPemesanan);
                        insertCmd.Parameters.AddWithValue("@StatusPembayaran", statusPembayaran);
                        insertCmd.Parameters.AddWithValue("@TotalPembayaran", totalPembayaran);
                        insertCmd.Parameters.AddWithValue("@CreatedBy", createdBy);

                        // Panggil ExecuteNonQuery() di dalam using block ini
                        insertCmd.ExecuteNonQuery();

                    }

                    // --- Command 2: UPDATE kuota di tabel PaketWisata ---
                    string updateQuery = "UPDATE dbo.PaketWisata SET Kuota = Kuota - 1 WHERE IDPaket = @IDPaket;";

                    using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn, transaction))
                    {
                        updateCmd.Parameters.AddWithValue("@IDPaket", idPaket);
                        updateCmd.ExecuteNonQuery();
                    }

                    // 3. Jika semua command berhasil, Commit Transaksi
                    transaction.Commit();

                    MessageBox.Show("Data pemesanan berhasil ditambahkan! Status: Belum Bayar.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshData();
                }
                else
                {
                    // Kuota habis, tidak perlu ROLLBACK karena belum ada perubahan yang ditulis
                    // Cukup tampilkan pesan error.
                    MessageBox.Show("Maaf, kuota untuk paket wisata ini sudah habis.", "Gagal Menambahkan", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    // Jika Anda sudah melakukan Commit/Rollback, Anda tidak bisa melakukannya lagi.
                    // Di sini, kita bisa melakukan Rollback untuk "menutup" transaksi secara formal.
                    if (transaction != null) transaction.Rollback();
                }
            }
            catch (Exception ex)
            {
                // 4. Jika terjadi error di mana saja dalam blok 'try', Rollback Transaksi
                MessageBox.Show($"Terjadi error: {ex.Message}", "Transaksi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                try
                {
                    // Pastikan transaksi ada dan belum selesai sebelum mencoba Rollback
                    if (transaction != null)
                    {
                        transaction.Rollback();
                    }
                }
                catch (Exception rollbackEx)
                {
                    // Jika rollback juga gagal, log error ini
                    MessageBox.Show($"Error saat melakukan rollback: {rollbackEx.Message}", "Kesalahan Kritis", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            finally
            {
                // 5. Selalu tutup koneksi pada akhirnya
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

            // =================================================================
            // LOGIKA TRANSAKSI DIMULAI DI SINI
            // =================================================================

            SqlConnection conn = new SqlConnection(connectionString);
            SqlTransaction transaction = null;

            int newIdPaket = Convert.ToInt32(cmbPaketWisata.SelectedValue);
            int newIdPelanggan = Convert.ToInt32(cmbPelanggan.SelectedValue);
            DateTime newTanggalPemesanan = dtpTanggalPesan.Value;
            // string newStatusPembayaran = cmbStatus.SelectedItem.ToString(); // HAPUS BARIS INI
            decimal newTotalPembayaran = decimal.Parse(txtTotal.Text);
            string updatedBy = Environment.UserName;

            try
            {
                // 1. Buka Koneksi dan Mulai Transaksi
                conn.Open();
                transaction = conn.BeginTransaction();

                // --- Langkah A: Dapatkan IDPaket LAMA dari database ---
                int oldIdPaket;
                string getOldPaketIdQuery = "SELECT IDPaket FROM dbo.Pemesanan WHERE IDPemesanan = @IDPemesanan";

                using (SqlCommand getOldIdCmd = new SqlCommand(getOldPaketIdQuery, conn, transaction))
                {
                    getOldIdCmd.Parameters.AddWithValue("@IDPemesanan", selectedPemesananId);
                    object result = getOldIdCmd.ExecuteScalar();
                    if (result == null || result == DBNull.Value)
                    {
                        throw new InvalidOperationException("Pemesanan yang akan diubah tidak ditemukan.");
                    }
                    oldIdPaket = Convert.ToInt32(result);
                }

                // --- Langkah B: Cek apakah IDPaket berubah. Jika ya, sesuaikan kuota ---
                if (oldIdPaket != newIdPaket)
                {
                    // ---- B.1: Kembalikan kuota paket LAMA ----
                    string returnOldKuotaQuery = "UPDATE dbo.PaketWisata SET Kuota = Kuota + 1 WHERE IDPaket = @OldIDPaket";
                    using (SqlCommand returnCmd = new SqlCommand(returnOldKuotaQuery, conn, transaction))
                    {
                        returnCmd.Parameters.AddWithValue("@OldIDPaket", oldIdPaket);
                        returnCmd.ExecuteNonQuery();
                    }

                    // ---- B.2: Cek ketersediaan dan kurangi kuota paket BARU ----
                    string checkAndTakeNewKuotaQuery = "UPDATE dbo.PaketWisata SET Kuota = Kuota - 1 WHERE IDPaket = @NewIDPaket AND Kuota > 0";
                    using (SqlCommand takeCmd = new SqlCommand(checkAndTakeNewKuotaQuery, conn, transaction))
                    {
                        takeCmd.Parameters.AddWithValue("@NewIDPaket", newIdPaket);
                        int rowsAffected = takeCmd.ExecuteNonQuery();

                        // Jika tidak ada baris yang terpengaruh (rowsAffected = 0), berarti kuota paket baru habis.
                        if (rowsAffected == 0)
                        {
                            // Lempar error untuk memicu ROLLBACK
                            throw new InvalidOperationException("Kuota untuk paket wisata baru yang dipilih sudah habis.");
                        }
                    }
                }

                // --- Langkah C: Update data utama di tabel Pemesanan ---
                // PERUBAHAN: Hapus StatusPembayaran dari query UPDATE
                string updatePemesananQuery = @"UPDATE dbo.Pemesanan SET
                                            IDPelanggan = @IDPelanggan,
                                            IDPaket = @IDPaket,
                                            TanggalPemesanan = @TanggalPemesanan,
                                            TotalPembayaran = @TotalPembayaran,
                                            UpdatedAt = GETDATE(),
                                            UpdatedBy = @UpdatedBy
                                        WHERE IDPemesanan = @IDPemesanan";

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

                // 4. Jika semua berhasil, Commit Transaksi
                transaction.Commit();

                MessageBox.Show("Data pemesanan berhasil diubah!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshData();
            }
            catch (Exception ex)
            {
                // 5. Jika terjadi error, Rollback semua perubahan
                MessageBox.Show($"Operasi gagal, semua perubahan dibatalkan: {ex.Message}", "Transaksi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                try
                {
                    if (transaction != null)
                    {
                        transaction.Rollback();
                    }
                }
                catch (Exception rollbackEx)
                {
                    MessageBox.Show($"Kesalahan fatal saat rollback: {rollbackEx.Message}", "Error Kritis", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            finally
            {
                // 6. Selalu tutup koneksi
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (selectedPemesananId < 0)
            {
                MessageBox.Show("Pilih pemesanan yang akan dibatalkan!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Ambil status saat ini untuk validasi tambahan
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

            // =================================================================
            // LOGIKA TRANSAKSI DIMULAI DI SINI
            // =================================================================

            SqlConnection conn = new SqlConnection(connectionString);
            SqlTransaction transaction = null;

            try
            {
                // 1. Buka Koneksi dan Mulai Transaksi
                conn.Open();
                transaction = conn.BeginTransaction();

                // --- Langkah A: Ambil IDPaket dari pemesanan yang akan dibatalkan untuk mengembalikan kuota ---
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

                // --- Langkah B: Hapus semua data pembayaran yang terkait dengan pemesanan ini ---
                // Ini aman bahkan jika tidak ada data pembayaran.
                string deletePembayaranQuery = "DELETE FROM dbo.Pembayaran WHERE IDPemesanan = @IDPemesanan";
                using (SqlCommand deletePembayaranCmd = new SqlCommand(deletePembayaranQuery, conn, transaction))
                {
                    deletePembayaranCmd.Parameters.AddWithValue("@IDPemesanan", selectedPemesananId);
                    deletePembayaranCmd.ExecuteNonQuery();
                }

                // --- Langkah C: Kembalikan kuota paket wisata ---
                string updateKuotaQuery = "UPDATE dbo.PaketWisata SET Kuota = Kuota + 1 WHERE IDPaket = @IDPaket";
                using (SqlCommand updateKuotaCmd = new SqlCommand(updateKuotaQuery, conn, transaction))
                {
                    updateKuotaCmd.Parameters.AddWithValue("@IDPaket", idPaket);
                    updateKuotaCmd.ExecuteNonQuery();
                }

                // --- Langkah D: Ubah status pemesanan menjadi 'Dibatalkan' (menggunakan SP yang sudah ada) ---
                // Menggunakan SP sp_CancelPemesanan Anda jika isinya hanya mengubah status.
                using (var cmd = new SqlCommand("sp_CancelPemesanan", conn, transaction))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDPemesanan", selectedPemesananId);
                    cmd.Parameters.AddWithValue("@UpdatedBy", Environment.UserName);
                    cmd.ExecuteNonQuery();
                }

                // 2. Jika semua perintah berhasil, Commit Transaksi
                transaction.Commit();

                // Tampilkan pesan sukses dan perbarui UI
                MessageBox.Show("Pemesanan berhasil dibatalkan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshData(); // RefreshData sudah memanggil ClearInputs
            }
            catch (Exception ex)
            {
                // 3. Jika ada error, Rollback Transaksi untuk membatalkan semua perubahan
                MessageBox.Show($"Terjadi kesalahan, semua perubahan dibatalkan: {ex.Message}", "Transaksi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                try
                {
                    if (transaction != null)
                    {
                        transaction.Rollback();
                    }
                }
                catch (Exception rollbackEx)
                {
                    MessageBox.Show($"Kesalahan fatal saat rollback: {rollbackEx.Message}", "Error Kritis", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            finally
            {
                // 4. Selalu tutup koneksi
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
                using (var conn = new SqlConnection(connectionString))
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

    }
}