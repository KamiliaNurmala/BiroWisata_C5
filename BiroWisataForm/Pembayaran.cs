// Pembayaran.cs
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Globalization;
using System.Linq;

namespace BiroWisataForm
{
    public partial class Pembayaran : Form
    {
        //private string connectionString =
        //    //@"Data Source=MSI;Initial Catalog=BiroWisata;Integrated Security=True";
        private string connectionString = @"Data Source=KAMILIA\KAMILIANURMALA;Initial Catalog=BiroWisata;Integrated Security=True;";
        private int selectedPembayaranId = -1;
        // private int currentSelectedPemesananIdForPayment = -1; // Less critical if we enforce one payment

        public Pembayaran()
        {
            InitializeComponent();
            EnsureDatabaseIndexes();
        }



        private void EnsureDatabaseIndexes()
        {
            // Skrip SQL untuk membuat indeks pada tabel Pembayaran
            string scriptIndex = @"
                IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_Pembayaran_IDPemesanan' AND object_id = OBJECT_ID('dbo.Pembayaran'))
                BEGIN
                    CREATE NONCLUSTERED INDEX IX_Pembayaran_IDPemesanan ON dbo.Pembayaran(IDPemesanan);
                END;
            ";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(scriptIndex, conn))
                    {
                        cmd.ExecuteNonQuery();
                        // Log untuk debugging
                        Console.WriteLine("Pengecekan indeks untuk tabel Pembayaran selesai.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal memverifikasi/membuat indeks untuk Pembayaran: {ex.Message}",
                                "Kesalahan Konfigurasi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Pembayaran_Load(object sender, EventArgs e)
        {
            InitializeDataGridViewSettings();
            LoadPemesananForComboBox(); // Renamed for clarity
            LoadMetodePembayaran();

            dtpPemesananFilterStart.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpPemesananFilterEnd.Value = DateTime.Now.Date;
            dtpPembayaranFilterStart.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpPembayaranFilterEnd.Value = DateTime.Now.Date;

            RefreshData(null, null, null, null);
            ClearInputs();

            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            this.btnTambah.Click += new System.EventHandler(this.btnTambah_Click);
            this.btnUbah.Click += new System.EventHandler(this.btnUbah_Click);
            this.btnHapus.Click += new System.EventHandler(this.btnHapus_Click);
            this.dgvPembayaran.CellClick += DgvPembayaran_CellClick;
            this.cmbPemesanan.SelectedIndexChanged += CmbPemesanan_SelectedIndexChanged; // Add this
        }

        private void InitializeDataGridViewSettings()
        {
            dgvPembayaran.AutoGenerateColumns = false;
            dgvPembayaran.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPembayaran.MultiSelect = false;
            dgvPembayaran.ReadOnly = true;
            dgvPembayaran.AllowUserToAddRows = false;
            dgvPembayaran.AllowUserToDeleteRows = false;
            dgvPembayaran.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Good for fitting content

            dgvPembayaran.Columns.Clear();

            dgvPembayaran.Columns.Add(new DataGridViewTextBoxColumn { Name = "colIDPembayaran", HeaderText = "ID Bayar", DataPropertyName = "IDPembayaran", Visible = false });
            dgvPembayaran.Columns.Add(new DataGridViewTextBoxColumn { Name = "colIDPemesanan", HeaderText = "ID Pesan", DataPropertyName = "IDPemesanan" });
            dgvPembayaran.Columns.Add(new DataGridViewTextBoxColumn { Name = "colNamaPelanggan", HeaderText = "Pelanggan", DataPropertyName = "NamaPelanggan" });
            dgvPembayaran.Columns.Add(new DataGridViewTextBoxColumn { Name = "colNamaPaket", HeaderText = "Paket Wisata", DataPropertyName = "NamaPaket" });
            dgvPembayaran.Columns.Add(new DataGridViewTextBoxColumn { Name = "colJumlahPembayaran", HeaderText = "Jumlah Bayar", DataPropertyName = "JumlahPembayaran", DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight } });
            dgvPembayaran.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTanggalPembayaran", HeaderText = "Tgl Bayar", DataPropertyName = "TanggalPembayaran", DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" } });
            dgvPembayaran.Columns.Add(new DataGridViewTextBoxColumn { Name = "colMetodePembayaran", HeaderText = "Metode", DataPropertyName = "MetodePembayaran" });
            // No need for TanggalPemesanan here if not directly used or filtered extensively
        }

        // Renamed and modified to load only 'Belum Bayar' Pemesanan for new payments
        private void LoadPemesananForComboBox()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    // Load only Pemesanan that are 'Belum Bayar' for new payments
                    string query = @"SELECT p.IDPemesanan, p.TotalPembayaran,
                                        CAST(p.IDPemesanan AS VARCHAR) + ' - ' + pl.NamaPelanggan + 
                                        ' (' + pw.NamaPaket + ') - Total: ' + FORMAT(p.TotalPembayaran, 'N0') AS Deskripsi
                                    FROM Pemesanan p 
                                    JOIN Pelanggan pl ON p.IDPelanggan = pl.IDPelanggan 
                                    JOIN PaketWisata pw ON p.IDPaket = pw.IDPaket
                                    WHERE p.StatusPembayaran = 'Belum Bayar' AND p.StatusPemesanan != 'Dibatalkan'
                                          AND pl.IsDeleted = 0 AND pw.IsDeleted = 0 
                                    ORDER BY p.IDPemesanan DESC";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    DataRow row = dt.NewRow();
                    row["IDPemesanan"] = DBNull.Value;
                    row["Deskripsi"] = "-- Pilih Pemesanan (Belum Bayar) --";
                    row["TotalPembayaran"] = DBNull.Value; // Add for consistency
                    dt.Rows.InsertAt(row, 0);

                    cmbPemesanan.DataSource = dt;
                    cmbPemesanan.DisplayMember = "Deskripsi";
                    cmbPemesanan.ValueMember = "IDPemesanan";
                    cmbPemesanan.DropDownStyle = ComboBoxStyle.DropDownList;
                }
                catch (Exception ex) { HandleLoadError("Pemesanan (Belum Bayar)", ex); }
            }
        }

        // Event handler for cmbPemesanan selection change
        // Di dalam Pembayaran.cs
        private void CmbPemesanan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPemesanan.SelectedValue != null && cmbPemesanan.SelectedValue != DBNull.Value)
            {
                DataRowView drv = cmbPemesanan.SelectedItem as DataRowView;
                if (drv != null && drv.Row["TotalPembayaran"] != DBNull.Value)
                {
                    decimal totalTagihan = Convert.ToDecimal(drv.Row["TotalPembayaran"]);

                    // PERUBAHAN: Gunakan format "F2" dengan CultureInfo.InvariantCulture
                    // Ini akan menghasilkan string seperti "255000.00" yang aman untuk di-parse
                    txtJumlah.Text = totalTagihan.ToString("F2", CultureInfo.InvariantCulture);
                }
                else
                {
                    txtJumlah.Clear();
                }
            }
            else
            {
                txtJumlah.Clear();
            }
        }


        private void LoadMetodePembayaran()
        {
            comboBoxMetode.Items.Clear();
            comboBoxMetode.Items.Add("-- Pilih Metode --");
            comboBoxMetode.Items.Add("Transfer");
            comboBoxMetode.SelectedIndex = 0;
            comboBoxMetode.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private bool RefreshData(DateTime? pemesananFilterStart, DateTime? pemesananFilterEnd,
                         DateTime? pembayaranFilterStart, DateTime? pembayaranFilterEnd) // <-- DIUBAH jadi bool
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    // ... (kode di dalam try block Anda tetap sama) ...
                    conn.Open();
                    string query = @"SELECT 
                                pb.IDPembayaran, pb.IDPemesanan, pl.NamaPelanggan, pw.NamaPaket,
                                pb.JumlahPembayaran, pb.TanggalPembayaran, pb.MetodePembayaran
                             FROM Pembayaran pb 
                             INNER JOIN Pemesanan pem ON pb.IDPemesanan = pem.IDPemesanan
                             INNER JOIN Pelanggan pl ON pem.IDPelanggan = pl.IDPelanggan
                             INNER JOIN PaketWisata pw ON pem.IDPaket = pw.IDPaket
                             WHERE 1=1 ";
                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.Clear();
                    if (pemesananFilterStart.HasValue) { query += " AND pem.TanggalPemesanan >= @PemesananStartDate "; cmd.Parameters.AddWithValue("@PemesananStartDate", pemesananFilterStart.Value.Date); }
                    if (pemesananFilterEnd.HasValue) { query += " AND pem.TanggalPemesanan <= @PemesananEndDate "; cmd.Parameters.AddWithValue("@PemesananEndDate", pemesananFilterEnd.Value.Date.AddDays(1).AddTicks(-1)); }
                    if (pembayaranFilterStart.HasValue) { query += " AND pb.TanggalPembayaran >= @PembayaranStartDate "; cmd.Parameters.AddWithValue("@PembayaranStartDate", pembayaranFilterStart.Value.Date); }
                    if (pembayaranFilterEnd.HasValue) { query += " AND pb.TanggalPembayaran <= @PembayaranEndDate "; cmd.Parameters.AddWithValue("@PembayaranEndDate", pembayaranFilterEnd.Value.Date.AddDays(1).AddTicks(-1)); }

                    query += " ORDER BY pb.TanggalPembayaran DESC, pb.IDPembayaran DESC";
                    cmd.CommandText = query; cmd.Connection = conn;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd); DataTable dt = new DataTable(); adapter.Fill(dt);
                    dgvPembayaran.DataSource = null;
                    dgvPembayaran.DataSource = dt;
                    return true; // <-- DITAMBAHKAN: Kembalikan true jika sukses
                }
                catch (Exception ex)
                {
                    HandleLoadError("Refresh Data Pembayaran", ex);
                    return false; // <-- DITAMBAHKAN: Kembalikan false jika gagal
                }
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            DateTime pemesananStart = dtpPemesananFilterStart.Value; DateTime pemesananEnd = dtpPemesananFilterEnd.Value;
            DateTime pembayaranStart = dtpPembayaranFilterStart.Value; DateTime pembayaranEnd = dtpPembayaranFilterEnd.Value;
            if (pemesananStart.Date > pemesananEnd.Date) { MessageBox.Show("Tanggal Awal Filter Pemesanan tidak boleh melebihi Tanggal Akhir.", "Filter Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (pembayaranStart.Date > pembayaranEnd.Date) { MessageBox.Show("Tanggal Awal Filter Pembayaran tidak boleh melebihi Tanggal Akhir.", "Filter Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            RefreshData(pemesananStart, pemesananEnd, pembayaranStart, pembayaranEnd);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            // Reset filter
            dtpPemesananFilterStart.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpPemesananFilterEnd.Value = DateTime.Now.Date;
            dtpPembayaranFilterStart.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpPembayaranFilterEnd.Value = DateTime.Now.Date;

            // --- PERUBAHAN DI SINI ---
            // Panggil RefreshData dan periksa hasilnya
            if (RefreshData(null, null, null, null))
            {
                // Jika sukses, baru jalankan sisanya dan tampilkan pesan
                ClearInputs();
                LoadPemesananForComboBox(); // Muat ulang untuk mencerminkan status terbaru
                MessageBox.Show("Data diperbarui dan filter dihapus.", "Refresh", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            // Jika gagal, RefreshData() sudah menampilkan pesan errornya sendiri.
        }

        private void DgvPembayaran_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvPembayaran.Rows.Count)
            {
                DataGridViewRow row = dgvPembayaran.Rows[e.RowIndex];
                try
                {
                    if (row.Cells["colIDPembayaran"].Value != null && int.TryParse(row.Cells["colIDPembayaran"].Value.ToString(), out int id))
                    { selectedPembayaranId = id; }
                    else { selectedPembayaranId = -1; ClearInputs(); return; }

                    // For viewing/editing, we might need to load *all* Pemesanan in combo temporarily,
                    // or disable editing of Pemesanan ID for an existing payment.
                    // Current LoadPemesananForComboBox only loads 'Belum Bayar'.
                    // This part is tricky if editing Pemesanan for an existing payment.
                    // For now, let's just display the selected value if found.

                    object idPemesananValue = row.Cells["colIDPemesanan"].Value;
                    cmbPemesanan.Enabled = false; // Disable changing Pemesanan for existing payment

                    // Attempt to select the existing Pemesanan if it's in the list (it might not be if it was already 'Lunas')
                    // This part needs a more robust solution if we want to edit the Pemesanan for an existing payment
                    // or if the ComboBox only shows "Belum Bayar" items.
                    // A simpler approach for "Ubah" might be to only allow changing Tanggal or Metode.
                    // For now, this will likely fail to select if the Pemesanan is already Lunas.
                    if (cmbPemesanan.Items.Count > 0) cmbPemesanan.SelectedIndex = 0; // Default to placeholder
                    foreach (DataRowView item in cmbPemesanan.Items)
                    {
                        if (item.Row["IDPemesanan"] != DBNull.Value && Convert.ToInt32(item.Row["IDPemesanan"]) == Convert.ToInt32(idPemesananValue))
                        {
                            cmbPemesanan.SelectedValue = idPemesananValue;
                            break;
                        }
                    }
                    // If not found in the "Belum Bayar" list, it means it was likely paid.
                    // We can show a message or load a specific detail. For simplicity, we'll just leave it.


                    decimal jumlah = Convert.ToDecimal(row.Cells["colJumlahPembayaran"].Value);
                    txtJumlah.Text = jumlah.ToString("F2", CultureInfo.InvariantCulture);

                    object tanggalValue = row.Cells["colTanggalPembayaran"].Value;
                    if (tanggalValue != null && tanggalValue != DBNull.Value && DateTime.TryParse(tanggalValue.ToString(), out DateTime tanggal))
                    { dateTimePicker1.Value = tanggal; }
                    else { dateTimePicker1.Value = DateTime.Now; }

                    string metodeValue = row.Cells["colMetodePembayaran"].Value?.ToString();
                    comboBoxMetode.SelectedItem = metodeValue;
                    if (comboBoxMetode.SelectedIndex < 0 && comboBoxMetode.Items.Count > 0)
                    {
                        comboBoxMetode.SelectedIndex = 0;
                    }
                }
                catch (Exception ex) { MessageBox.Show($"Error saat memilih baris: {ex.Message}", "Error Seleksi", MessageBoxButtons.OK, MessageBoxIcon.Error); ClearInputs(); }
            }
        }

        // Ganti metode ini di Pembayaran.cs
        private void btnTambah_Click(object sender, EventArgs e)
        {
            if (!ValidateInputsForAdd(out decimal jumlahPembayaran, out int idPemesanan, out decimal totalTagihan)) return;

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

                // --- Langkah A: Cek apakah pemesanan sudah lunas sebelumnya ---
                string checkStatusQuery = "SELECT StatusPembayaran FROM dbo.Pemesanan WHERE IDPemesanan = @IDPemesanan";
                using (SqlCommand checkCmd = new SqlCommand(checkStatusQuery, conn, transaction))
                {
                    checkCmd.Parameters.AddWithValue("@IDPemesanan", idPemesanan);
                    string currentStatus = checkCmd.ExecuteScalar()?.ToString();

                    if (currentStatus == "Lunas")
                    {
                        // Jika sudah lunas, lempar error untuk memicu rollback dan menampilkan pesan.
                        throw new InvalidOperationException("Pemesanan ini sudah lunas. Tidak dapat melakukan pembayaran lagi.");
                    }
                    if (string.IsNullOrEmpty(currentStatus))
                    {
                        throw new InvalidOperationException("ID Pemesanan tidak ditemukan.");
                    }
                }

                // --- Langkah B: Insert data ke tabel Pembayaran menggunakan SP sederhana ---
                using (SqlCommand addCmd = new SqlCommand("sp_AddPembayaran", conn, transaction))
                {
                    addCmd.CommandType = CommandType.StoredProcedure;
                    addCmd.Parameters.AddWithValue("@IDPemesanan", idPemesanan);
                    addCmd.Parameters.AddWithValue("@JumlahPembayaran", jumlahPembayaran);
                    addCmd.Parameters.AddWithValue("@TanggalPembayaran", dateTimePicker1.Value);
                    addCmd.Parameters.AddWithValue("@MetodePembayaran", comboBoxMetode.SelectedItem.ToString());
                    addCmd.ExecuteNonQuery();
                }

                // --- Langkah C: Update status di tabel Pemesanan jika pembayaran mencukupi ---
                // Variabel totalTagihan sudah kita dapatkan dari validasi input sebelumnya.
                if (jumlahPembayaran >= totalTagihan)
                {
                    string updateStatusQuery = "UPDATE dbo.Pemesanan SET StatusPembayaran = 'Lunas' WHERE IDPemesanan = @IDPemesanan";
                    using (SqlCommand updateCmd = new SqlCommand(updateStatusQuery, conn, transaction))
                    {
                        updateCmd.Parameters.AddWithValue("@IDPemesanan", idPemesanan);
                        updateCmd.ExecuteNonQuery();
                    }
                }

                // 3. Jika semua perintah berhasil, Commit Transaksi
                transaction.Commit();

                // Tampilkan pesan sukses dan perbarui UI setelah commit berhasil
                MessageBox.Show("Pembayaran berhasil ditambahkan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshData(null, null, null, null);
                LoadPemesananForComboBox();
                ClearInputs();
            }
            catch (Exception ex)
            {
                // 4. Jika ada error di mana saja dalam blok 'try', Rollback Transaksi
                MessageBox.Show($"Terjadi kesalahan, perubahan dibatalkan: {ex.Message}", "Transaksi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                // 5. Selalu tutup koneksi pada akhirnya
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        private bool CheckIfPemesananAlreadyPaid(int idPemesanan)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                // Check Pembayaran table OR Pemesanan.StatusPembayaran
                string query = "SELECT COUNT(*) FROM Pembayaran WHERE IDPemesanan = @IDPemesanan";
                // OR string query = "SELECT StatusPembayaran FROM Pemesanan WHERE IDPemesanan = @IDPemesanan";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IDPemesanan", idPemesanan);
                    int count = (int)cmd.ExecuteScalar();
                    // If checking Pemesanan.StatusPembayaran:
                    // string status = cmd.ExecuteScalar()?.ToString();
                    // return status == "Lunas";
                    return count > 0;
                }
            }
        }


        // "Ubah" button for payments is often tricky.
        // If a payment is changed, what happens to the Pemesanan status?
        // For simplicity, let's assume "Ubah" mostly changes Tanggal or Metode, not amount or linked Pemesanan.
        // Or, if amount changes, it needs complex logic to revert/update Pemesanan status.
        // A common approach is to DELETE the incorrect payment and ADD a new one.
        private void btnUbah_Click(object sender, EventArgs e)
        {
            if (selectedPembayaranId < 0)
            {
                MessageBox.Show("Pilih data pembayaran yang akan diubah.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInputsForUpdate()) return;

            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("sp_UpdatePembayaran", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IDPembayaran", selectedPembayaranId);
                        cmd.Parameters.AddWithValue("@TanggalPembayaran", dateTimePicker1.Value);
                        cmd.Parameters.AddWithValue("@MetodePembayaran", comboBoxMetode.SelectedItem.ToString());

                        // --- PERUBAHAN DI SINI ---
                        // Hapus kondisi 'if' dan 'else'.
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Data pembayaran berhasil diubah.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RefreshData(null, null, null, null);
                        ClearInputs();
                    }
                }
            }
            catch (SqlException ex) { HandleSqlError(ex, "mengubah pembayaran"); }
            catch (Exception ex) { HandleGeneralError("mengubah pembayaran", ex); }
        }


        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (selectedPembayaranId < 0)
            {
                MessageBox.Show("Pilih data yang akan dihapus.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Yakin ingin menghapus data pembayaran ini?\nStatus pemesanan terkait akan dikembalikan menjadi 'Belum Bayar'.", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            // =================================================================
            // LOGIKA TRANSAKSI DIMULAI DI SINI
            // =================================================================

            // Deklarasikan koneksi dan transaksi di luar blok 'try'
            SqlConnection conn = new SqlConnection(connectionString);
            SqlTransaction transaction = null;

            try
            {
                // 1. Buka Koneksi
                conn.Open();
                // 2. Mulai Transaksi
                transaction = conn.BeginTransaction();

                // --- Langkah A: Ambil IDPemesanan dari pembayaran yang akan dihapus ---
                int idPemesanan;
                string getPemesananIdQuery = "SELECT IDPemesanan FROM dbo.Pembayaran WHERE IDPembayaran = @IDPembayaran";

                using (SqlCommand getIdCmd = new SqlCommand(getPemesananIdQuery, conn, transaction))
                {
                    getIdCmd.Parameters.AddWithValue("@IDPembayaran", selectedPembayaranId);
                    object result = getIdCmd.ExecuteScalar();
                    if (result == null || result == DBNull.Value)
                    {
                        // Jika pembayaran tidak ditemukan, lempar error untuk mengaktifkan rollback
                        throw new InvalidOperationException("Data pembayaran tidak ditemukan. Mungkin sudah dihapus oleh user lain.");
                    }
                    idPemesanan = Convert.ToInt32(result);
                }

                // --- Langkah B: Hapus data dari tabel Pembayaran menggunakan SP sederhana ---
                // (Atau bisa juga menggunakan query DELETE langsung)
                using (SqlCommand deleteCmd = new SqlCommand("sp_DeletePembayaran", conn, transaction))
                {
                    deleteCmd.CommandType = CommandType.StoredProcedure;
                    deleteCmd.Parameters.AddWithValue("@IDPembayaran", selectedPembayaranId);
                    deleteCmd.ExecuteNonQuery();
                }

                // --- Langkah C: Update status di tabel Pemesanan kembali menjadi 'Belum Bayar' ---
                string updateStatusQuery = "UPDATE dbo.Pemesanan SET StatusPembayaran = 'Belum Bayar' WHERE IDPemesanan = @IDPemesanan";
                using (SqlCommand updateCmd = new SqlCommand(updateStatusQuery, conn, transaction))
                {
                    updateCmd.Parameters.AddWithValue("@IDPemesanan", idPemesanan);
                    updateCmd.ExecuteNonQuery();
                }

                // 3. Jika semua perintah berhasil, Commit Transaksi
                transaction.Commit();

                // Tampilkan pesan sukses dan perbarui UI setelah commit berhasil
                MessageBox.Show("Pembayaran berhasil dihapus.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshData(null, null, null, null);
                LoadPemesananForComboBox();
                ClearInputs();
            }
            catch (Exception ex)
            {
                // 4. Jika ada error di mana saja dalam blok 'try', Rollback Transaksi
                MessageBox.Show($"Terjadi kesalahan, perubahan dibatalkan: {ex.Message}", "Transaksi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                // 5. Selalu tutup koneksi pada akhirnya
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        private bool ValidateInputsForUpdate()
        {
            if (comboBoxMetode.SelectedIndex <= 0)
            {
                MessageBox.Show("Metode pembayaran harus dipilih.", "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            // Tambahkan validasi lain jika perlu, misalnya untuk tanggal
            return true;
        }

        // Modified validation for Add operation
        // Di dalam Pembayaran.cs
        private bool ValidateInputsForAdd(out decimal jumlahPembayaran, out int idPemesanan, out decimal totalTagihan)
        {
            // Inisialisasi nilai awal
            jumlahPembayaran = 0;
            idPemesanan = -1;
            totalTagihan = 0;
            bool isValid = true;
            string errorMessage = "";

            // Validasi ComboBox Pemesanan (sudah benar)
            if (cmbPemesanan.SelectedValue == null || cmbPemesanan.SelectedValue == DBNull.Value)
            {
                errorMessage += "- Pemesanan harus dipilih.\n";
                isValid = false;
            }
            else
            {
                idPemesanan = Convert.ToInt32(cmbPemesanan.SelectedValue);
                DataRowView drv = cmbPemesanan.SelectedItem as DataRowView;
                if (drv != null && drv.Row["TotalPembayaran"] != DBNull.Value)
                {
                    totalTagihan = Convert.ToDecimal(drv.Row["TotalPembayaran"]);
                }
                else
                {
                    errorMessage += "- Tidak dapat mengambil total tagihan untuk Pemesanan yang dipilih.\n";
                    isValid = false;
                }
            }

            // Validasi ComboBox Metode (sudah benar)
            if (comboBoxMetode.SelectedIndex <= 0)
            {
                errorMessage += "- Metode Pembayaran harus dipilih.\n";
                isValid = false;
            }

            // --- PERBAIKAN UTAMA DI SINI ---
            // Validasi dan PARSING txtJumlah
            if (string.IsNullOrWhiteSpace(txtJumlah.Text))
            {
                errorMessage += "- Jumlah pembayaran tidak boleh kosong (pilih pesanan yang valid).\n";
                isValid = false;
            }
            // Coba parsing nilainya. Jika berhasil, nilai akan disimpan di 'jumlahPembayaran'.
            // Kita gunakan CultureInfo.InvariantCulture agar konsisten dengan cara kita mengisinya.
            else if (!decimal.TryParse(txtJumlah.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out jumlahPembayaran))
            {
                // Error ini seharusnya tidak pernah terjadi jika txtJumlah terkunci, tapi sebagai pengaman.
                errorMessage += "- Format jumlah pembayaran tidak valid.\n";
                isValid = false;
            }
            // Cek apakah nilai yang berhasil di-parse lebih besar dari 0.
            else if (jumlahPembayaran <= 0)
            {
                errorMessage += "- Jumlah pembayaran harus lebih besar dari nol.\n";
                isValid = false;
            }

            if (!isValid)
            {
                MessageBox.Show("Harap perbaiki input yang tidak valid:\n" + errorMessage, "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return isValid;
        }

        private void ClearInputs()
        {
            if (cmbPemesanan.Items.Count > 0) cmbPemesanan.SelectedIndex = 0;
            cmbPemesanan.Enabled = true; // Re-enable for adding new payment
            txtJumlah.Clear();
            txtJumlah.ReadOnly = true; // Re-enable for adding new payment
            dateTimePicker1.Value = DateTime.Now;
            if (comboBoxMetode.Items.Count > 0) comboBoxMetode.SelectedIndex = 0;
            selectedPembayaranId = -1;
            // currentSelectedPemesananIdForPayment = -1; // Not strictly needed anymore here
            dgvPembayaran.ClearSelection();
        }

        private void HandleSqlError(SqlException ex, string operation)
        {
            string userMessage = $"Error Database saat {operation}: ";
            if (ex.Number == 547)
            {
                userMessage += "Pastikan ID Pemesanan yang dipilih valid dan ada.";
                MessageBox.Show(userMessage, "Kesalahan Referensi Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (ex.Number == 2627) // Unique key violation (if you add one to Pembayaran.IDPemesanan)
            {
                userMessage += "Pemesanan ini sudah memiliki data pembayaran. Tidak bisa menambah duplikat.";
                MessageBox.Show(userMessage, "Data Duplikat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                userMessage += ex.Message;
                MessageBox.Show(userMessage, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HandleGeneralError(string operation, Exception ex)
        {
            MessageBox.Show($"Error saat {operation}: {ex.Message}", "General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void HandleLoadError(string controlName, Exception ex)
        {
            MessageBox.Show($"Error saat memuat data {controlName}: {ex.Message}", "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // Letakkan method ini bersama dengan event handler tombol lainnya (seperti btnTambah_Click)

        private void btnKembali_Click(object sender, EventArgs e)
        {
            // Perintah ini akan menutup form 'Kendaraan' saat ini,
            // dan mengembalikan kontrol ke form yang membukanya (yaitu MenuAdmin).
            this.Close();
        }
    }
}