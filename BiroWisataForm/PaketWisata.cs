using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing; // Added for Font/Color if needed
using System.Windows.Forms;
using System.Globalization; // Added for parsing potentially
using System.Linq;
using praktikum7;

namespace BiroWisataForm
{
    public partial class PaketWisata : Form
    {
        Koneksi kn = new Koneksi();
        // --- Member Variables ---
        private string connectionString = "";
        private int selectedPaketId = -1;

        // --- Constructor ---
        // --- Constructor ---
        public PaketWisata()
        {
            InitializeComponent();

            // === PERBAIKAN DIMULAI DI SINI ===
            // Mengisi variabel connectionString dengan data dari kelas Koneksi
            // Baris ini akan memperbaiki error "ConnectionString has not been initialized"
            connectionString = kn.connectionString();
            // === AKHIR PERBAIKAN ===

            InitializeDataGridViewSettings();
            InitializeSearchBox();
            EnsureDatabaseIndexes();
            LoadDrivers();
            LoadKendaraan();
            LoadKategori();
        }


        private void EnsureDatabaseIndexes()
        {
            // Menggabungkan semua perintah CREATE INDEX menjadi satu string
            string scriptIndex = @"
                IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_PaketWisata_IDDriver' AND object_id = OBJECT_ID('dbo.PaketWisata'))
                BEGIN
                    CREATE NONCLUSTERED INDEX IX_PaketWisata_IDDriver ON dbo.PaketWisata(IDDriver);
                END;

                IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_PaketWisata_IDKendaraan' AND object_id = OBJECT_ID('dbo.PaketWisata'))
                BEGIN
                    CREATE NONCLUSTERED INDEX IX_PaketWisata_IDKendaraan ON dbo.PaketWisata(IDKendaraan);
                END;

                IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_PaketWisata_IsDeleted' AND object_id = OBJECT_ID('dbo.PaketWisata'))
                BEGIN
                    CREATE NONCLUSTERED INDEX IX_PaketWisata_IsDeleted ON dbo.PaketWisata(IsDeleted);
                END;

                IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_PaketWisata_NamaPaket' AND object_id = OBJECT_ID('dbo.PaketWisata'))
                BEGIN
                    CREATE NONCLUSTERED INDEX IX_PaketWisata_NamaPaket ON dbo.PaketWisata(NamaPaket);
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
                        // Untuk debugging, bisa ditambahkan log
                        Console.WriteLine("Pengecekan indeks untuk tabel PaketWisata selesai.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal memverifikasi/membuat indeks untuk PaketWisata: {ex.Message}",
                                "Kesalahan Konfigurasi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // --- Form Load ---
        private void PaketWisata_Load(object sender, EventArgs e)
        {
            RefreshData(); // Load data when form loads
            // Ensure SelectionChanged is connected in the designer
            // this.dgvPaketWisata.SelectionChanged += new System.EventHandler(this.DgvPaketWisata_SelectionChanged);
        }

        // --- Control Initialization ---
        private void InitializeDataGridViewSettings()
        {
            // Set DGV properties. Columns are defined in Designer.cs.
            dgvPaketWisata.AutoGenerateColumns = false; // CRITICAL
            dgvPaketWisata.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPaketWisata.MultiSelect = false;
            dgvPaketWisata.ReadOnly = true;
            dgvPaketWisata.AllowUserToAddRows = false;
            dgvPaketWisata.AllowUserToDeleteRows = false;
            // dgvPaketWisata.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells; // Or Fill, or None if widths are set manually
            dgvPaketWisata.RowHeadersVisible = false;

            // Optional Styling overrides (most should be in Designer.cs)
            // dgvPaketWisata.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
        }

        private void InitializeSearchBox()
        {
            // Pastikan Anda sudah menambahkan TextBox dengan nama 'txtSearch' di form designer
            if (this.Controls.Find("txtSearch", true).FirstOrDefault() is TextBox txtSearch)
            {
                this.txtSearch.TextChanged += (s, e) =>
                {
                    // Pass the search text to the data refresh method
                    RefreshData(this.txtSearch.Text);
                };
            }
        }

        private void LoadKategori()
        {
            cmbKategori.Items.Clear(); // Use the renamed control
            cmbKategori.Items.Add("-- Pilih Kategori --");
            cmbKategori.Items.Add("Luar Kota");
            cmbKategori.Items.Add("Dalam Kota");
            cmbKategori.SelectedIndex = 0;
            cmbKategori.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void LoadDrivers()
        {
            using (var conn = new SqlConnection(kn.connectionString()))
            {
                try
                {
                    conn.Open();
                    var dt = new DataTable();
                    // Select only active drivers that haven't been soft-deleted
                    var adapter = new SqlDataAdapter("SELECT IDDriver, NamaDriver FROM Driver WHERE Status = 'Aktif' AND IsDeleted = 0", conn);
                    adapter.Fill(dt);

                    var emptyRow = dt.NewRow();
                    emptyRow["IDDriver"] = DBNull.Value;
                    emptyRow["NamaDriver"] = "-- Pilih Driver --";
                    dt.Rows.InsertAt(emptyRow, 0);

                    cmbDriver.DataSource = dt;
                    cmbDriver.DisplayMember = "NamaDriver";
                    cmbDriver.ValueMember = "IDDriver";
                }
                catch (SqlException ex) { MessageBox.Show($"Error DB Load Driver: {ex.Message}", "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                catch (Exception ex) { MessageBox.Show($"Error Load Driver: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void LoadKendaraan()
        {
            using (var conn = new SqlConnection(kn.connectionString()))
            {
                try
                {
                    conn.Open();
                    var dt = new DataTable();
                    // Select only active vehicles
                    string sql = "SELECT IDKendaraan, Jenis + ' - ' + PlatNomor AS Deskripsi FROM Kendaraan WHERE Status = 'Aktif'";
                    var adapter = new SqlDataAdapter(sql, conn);
                    adapter.Fill(dt);

                    var emptyRow = dt.NewRow();
                    emptyRow["IDKendaraan"] = DBNull.Value;
                    emptyRow["Deskripsi"] = "-- Pilih Kendaraan --";
                    dt.Rows.InsertAt(emptyRow, 0);

                    cmbKendaraan.DataSource = dt;
                    cmbKendaraan.DisplayMember = "Deskripsi";
                    cmbKendaraan.ValueMember = "IDKendaraan";
                }
                catch (SqlException ex) { MessageBox.Show($"Error DB Load Kendaraan: {ex.Message}", "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                catch (Exception ex) { MessageBox.Show($"Error Load Kendaraan: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        // GANTI METODE INI DI FILE PaketWisata.cs
        private bool RefreshData(string searchTerm = null)
        {
            using (var conn = new SqlConnection(kn.connectionString()))
            {
                try
                {
                    conn.Open();
                    string query = @"
    SELECT
        p.IDPaket, p.NamaPaket, p.Destinasi, p.Harga, p.Durasi,
        p.Fasilitas, p.Kategori, p.Kuota, p.JadwalKeberangkatan,
        p.IDDriver, d.NamaDriver,
        p.IDKendaraan, k.Jenis + ' - ' + k.PlatNomor AS KendaraanInfo,
        p.CreatedAt, p.UpdatedAt, p.CreatedBy, p.UpdatedBy
    FROM PaketWisata p
    INNER JOIN Driver d ON p.IDDriver = d.IDDriver
    INNER JOIN Kendaraan k ON p.IDKendaraan = k.IDKendaraan
    WHERE p.IsDeleted = 0";

                    if (!string.IsNullOrWhiteSpace(searchTerm))
                    {
                        // EXPANDED SEARCH: Include ALL fields that users want to search
                        query += @" AND (
                    p.NamaPaket LIKE @SearchTerm OR 
                    p.Destinasi LIKE @SearchTerm OR 
                    p.Kategori LIKE @SearchTerm OR 
                    p.Fasilitas LIKE @SearchTerm OR
                    d.NamaDriver LIKE @SearchTerm OR 
                    (k.Jenis + ' - ' + k.PlatNomor) LIKE @SearchTerm OR
                    CAST(p.Harga AS VARCHAR(50)) LIKE @SearchTerm OR
                    CAST(p.Kuota AS VARCHAR(10)) LIKE @SearchTerm OR
                    CONVERT(VARCHAR, p.JadwalKeberangkatan, 103) LIKE @SearchTerm
                )";
                    }

                    var dt = new DataTable();
                    var adapter = new SqlDataAdapter(query, conn);

                    if (!string.IsNullOrWhiteSpace(searchTerm))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@SearchTerm", $"%{searchTerm}%");
                    }

                    adapter.Fill(dt);

                    dgvPaketWisata.DataSource = null;
                    dgvPaketWisata.DataSource = dt;

                    return true;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Error DB Refresh Paket: {ex.Message}", "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error Refresh Paket: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }


        // --- CRUD Button Event Handlers ---
        private void btnTambah_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            // Ambil nilai dari form untuk pengecekan dan penambahan
            string namaPaket = txtNamaPaket.Text.Trim();

            decimal.TryParse(txtHarga.Text.Trim(), out decimal harga);
            short.TryParse(txtDurasi.Text.Trim(), out short durasi);
            short.TryParse(txtKuota.Text.Trim(), out short kuota);

            SqlConnection conn = new SqlConnection(kn.connectionString());
            SqlTransaction transaction = null;

            try
            {
                conn.Open();
                transaction = conn.BeginTransaction();

                // --- LANGKAH 1: Cek duplikasi data hanya berdasarkan Nama Paket ---
                // PERUBAHAN DI SINI: Query hanya mengecek NamaPaket
                string checkQuery = "SELECT COUNT(*) FROM dbo.PaketWisata WHERE NamaPaket = @NamaPaket AND IsDeleted = 0";
                int existingCount = 0;

                using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn, transaction))
                {
                    // Parameter yang dicek hanya NamaPaket
                    checkCmd.Parameters.AddWithValue("@NamaPaket", namaPaket);
                    existingCount = (int)checkCmd.ExecuteScalar();
                }

                // --- LANGKAH 2: Buat keputusan berdasarkan hasil pengecekan ---
                if (existingCount > 0)
                {
                    // Jika data sudah ada, tampilkan pesan dan batalkan transaksi
                    // PERUBAHAN DI SINI: Pesan disesuaikan
                    MessageBox.Show("Gagal menambahkan data.\n\nPaket wisata dengan nama yang sama sudah ada.", "Data Duplikat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    transaction.Rollback();
                }
                else
                {
                    // Jika data belum ada, lanjutkan proses penambahan
                    using (var cmd = new SqlCommand("sp_AddPaketWisata", conn, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@IDDriver", cmbDriver.SelectedValue);
                        cmd.Parameters.AddWithValue("@IDKendaraan", cmbKendaraan.SelectedValue);
                        cmd.Parameters.AddWithValue("@NamaPaket", namaPaket);
                        cmd.Parameters.AddWithValue("@Destinasi", txtDestinasi.Text.Trim());
                        cmd.Parameters.AddWithValue("@Harga", harga);
                        cmd.Parameters.AddWithValue("@Durasi", durasi);
                        cmd.Parameters.AddWithValue("@Fasilitas", txtFasilitas.Text.Trim());
                        cmd.Parameters.AddWithValue("@Kategori", cmbKategori.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@Kuota", kuota);
                        cmd.Parameters.AddWithValue("@JadwalKeberangkatan", dtpJadwalKeberangkatan.Value.Date);
                        cmd.Parameters.AddWithValue("@CreatedBy", Environment.UserName);

                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    MessageBox.Show("Paket wisata berhasil ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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


        private void btnUbah_Click(object sender, EventArgs e)
        {
            if (selectedPaketId < 0)
            {
                MessageBox.Show("Pilih paket wisata yang akan diubah!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInputs()) return;

            decimal.TryParse(txtHarga.Text.Trim(), out decimal harga);
            short.TryParse(txtDurasi.Text.Trim(), out short durasi);
            short.TryParse(txtKuota.Text.Trim(), out short kuota);

            // --- LOGIKA TRANSAKSI DIMULAI DI SINI ---
            SqlConnection conn = new SqlConnection(kn.connectionString());
            SqlTransaction transaction = null;

            try
            {
                // 1. Buka Koneksi dan Mulai Transaksi
                conn.Open();
                transaction = conn.BeginTransaction();

                using (var cmd = new SqlCommand("sp_UpdatePaketWisata", conn, transaction)) // Sertakan transaksi di command
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Menambahkan parameter seperti sebelumnya
                    cmd.Parameters.AddWithValue("@IDPaket", selectedPaketId);
                    cmd.Parameters.AddWithValue("@IDDriver", cmbDriver.SelectedValue);
                    cmd.Parameters.AddWithValue("@IDKendaraan", cmbKendaraan.SelectedValue);
                    cmd.Parameters.AddWithValue("@NamaPaket", txtNamaPaket.Text.Trim());
                    cmd.Parameters.AddWithValue("@Destinasi", txtDestinasi.Text.Trim());
                    cmd.Parameters.AddWithValue("@Harga", harga);
                    cmd.Parameters.AddWithValue("@Durasi", durasi);
                    cmd.Parameters.AddWithValue("@Fasilitas", txtFasilitas.Text.Trim());
                    cmd.Parameters.AddWithValue("@Kategori", cmbKategori.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Kuota", kuota);
                    cmd.Parameters.AddWithValue("@JadwalKeberangkatan", dtpJadwalKeberangkatan.Value.Date);
                    cmd.Parameters.AddWithValue("@UpdatedBy", Environment.UserName);

                    cmd.ExecuteNonQuery();
                }

                // 2. Jika berhasil, Commit Transaksi
                transaction.Commit();

                MessageBox.Show("Paket wisata berhasil diubah!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshData();
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Operasi gagal: {ex.Message}", "Transaksi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // 3. Jika terjadi error, Rollback semua perubahan
                try
                {
                    transaction?.Rollback();
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


        // Ganti metode ini di PaketWisata.cs
        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (selectedPaketId < 0)
            {
                MessageBox.Show("Pilih paket wisata yang akan dihapus!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tampilkan pesan konfirmasi yang lebih detail
            DialogResult confirmation = MessageBox.Show(
                "Yakin ingin menghapus paket wisata ini?\n\nPERHATIAN: Semua pemesanan aktif untuk paket ini akan DIBATALKAN secara otomatis.",
                "Konfirmasi Hapus dengan Efek Berantai",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmation != DialogResult.Yes)
            {
                return;
            }

            // =================================================================
            // LOGIKA TRANSAKSI DIMULAI DI SINI
            // =================================================================

            SqlConnection conn = new SqlConnection(kn.connectionString());
            SqlTransaction transaction = null;

            try
            {
                // 1. Buka Koneksi dan Mulai Transaksi
                conn.Open();
                transaction = conn.BeginTransaction();

                // --- Langkah A: Nonaktifkan (soft delete) Paket Wisata ---
                string updatePaketQuery = @"UPDATE PaketWisata SET
                                        IsDeleted = 1,
                                        Status = 'Tidak Aktif',
                                        UpdatedAt = GETDATE(),
                                        UpdatedBy = @UpdatedBy
                                    WHERE IDPaket = @IDPaket;";

                using (SqlCommand updatePaketCmd = new SqlCommand(updatePaketQuery, conn, transaction))
                {
                    updatePaketCmd.Parameters.AddWithValue("@IDPaket", selectedPaketId);
                    updatePaketCmd.Parameters.AddWithValue("@UpdatedBy", Environment.UserName);
                    updatePaketCmd.ExecuteNonQuery();
                }

                // --- Langkah B: Batalkan semua pemesanan terkait yang masih aktif ---
                // Kita hanya membatalkan yang statusnya 'Menunggu' atau 'Dikonfirmasi'
                string cancelPemesananQuery = @"UPDATE Pemesanan SET
                                            StatusPemesanan = 'Dibatalkan',
                                            UpdatedAt = GETDATE(),
                                            UpdatedBy = @UpdatedBy
                                        WHERE IDPaket = @IDPaket 
                                        AND StatusPemesanan IN ('Menunggu', 'Dikonfirmasi');";

                int pemesananDibatalkan = 0;
                using (SqlCommand cancelPemesananCmd = new SqlCommand(cancelPemesananQuery, conn, transaction))
                {
                    cancelPemesananCmd.Parameters.AddWithValue("@IDPaket", selectedPaketId);
                    cancelPemesananCmd.Parameters.AddWithValue("@UpdatedBy", "SYSTEM_AUTO_CANCEL"); // Tandai bahwa ini dibatalkan oleh sistem

                }

                // 3. Jika semua berhasil, Commit Transaksi
                transaction.Commit();

                MessageBox.Show(
                    $"Paket wisata berhasil dihapus.",
                    "Operasi Sukses",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                RefreshData();
            }
            catch (Exception ex)
            {
                // 4. Jika terjadi error, Rollback semua perubahan
                MessageBox.Show($"Operasi gagal, semua perubahan dibatalkan: {ex.Message}", "Transaksi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                // 5. Selalu tutup koneksi
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }


        private void btnRefresh_Click(object sender, EventArgs e)
        {
            // 1. Bersihkan kotak pencarian
            if (this.Controls.Find("txtSearch", true).FirstOrDefault() is TextBox txtSearch)
            {
                txtSearch.Clear();
            }

            // 2. Muat ulang data tabel
            if (RefreshData())
            {
                // 3. Bersihkan semua kotak isian form
                ClearInputs(); // <-- TAMBAHKAN PEMANGGILAN INI DI SINI

                MessageBox.Show("Data berhasil dimuat ulang.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // --- Helper Methods ---
        private bool ValidateInputs()
        {
            this.errorProvider1.Clear();
            bool isValid = true;

            if (cmbDriver.SelectedValue == null || cmbDriver.SelectedValue == DBNull.Value || cmbDriver.SelectedIndex <= 0)
            { errorProvider1.SetError(cmbDriver, "Pilih Driver yang valid!"); isValid = false; }
            if (cmbKendaraan.SelectedValue == null || cmbKendaraan.SelectedValue == DBNull.Value || cmbKendaraan.SelectedIndex <= 0)
            { errorProvider1.SetError(cmbKendaraan, "Pilih Kendaraan yang valid!"); isValid = false; }
            if (cmbKategori.SelectedIndex <= 0)
            { errorProvider1.SetError(cmbKategori, "Pilih Kategori yang valid!"); isValid = false; }

            if (string.IsNullOrWhiteSpace(txtNamaPaket.Text))
            {
                errorProvider1.SetError(txtNamaPaket, "Nama Paket tidak boleh kosong!");
                isValid = false;
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(txtNamaPaket.Text, @"^[a-zA-Z0-9\s]+$"))
            {
                errorProvider1.SetError(txtNamaPaket, "Nama Paket hanya boleh berisi huruf, angka, dan spasi!");
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(txtDestinasi.Text))
            { errorProvider1.SetError(txtDestinasi, "Destinasi tidak boleh kosong!"); isValid = false; }

            if (string.IsNullOrWhiteSpace(txtFasilitas.Text))
            { errorProvider1.SetError(txtFasilitas, "Fasilitas tidak boleh kosong!"); isValid = false; }

            if (string.IsNullOrWhiteSpace(txtHarga.Text))
            { errorProvider1.SetError(txtHarga, "Harga tidak boleh kosong!"); isValid = false; }
            else if (!decimal.TryParse(txtHarga.Text, out decimal harga) || harga <= 0)
            { errorProvider1.SetError(txtHarga, "Harga harus berupa angka positif!"); isValid = false; }

            if (string.IsNullOrWhiteSpace(txtDurasi.Text))
            { errorProvider1.SetError(txtDurasi, "Durasi tidak boleh kosong!"); isValid = false; }
            else if (!short.TryParse(txtDurasi.Text, out short durasi) || durasi <= 0)
            { errorProvider1.SetError(txtDurasi, "Durasi harus berupa angka positif!"); isValid = false; }

            // --- PERUBAHAN DI SINI ---
            if (string.IsNullOrWhiteSpace(txtKuota.Text))
            { errorProvider1.SetError(txtKuota, "Kuota tidak boleh kosong!"); isValid = false; }
            else if (!short.TryParse(txtKuota.Text, out short kuota) || kuota < 3)
            { errorProvider1.SetError(txtKuota, "Kuota tidak boleh kurang dari 3!"); isValid = false; }
            // --- AKHIR PERUBAHAN ---

            if (!isValid)
            {
                MessageBox.Show("Harap perbaiki data yang tidak valid.", "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return isValid;
        }

        private void ClearInputs()
        {
            selectedPaketId = -1;
            cmbDriver.SelectedIndex = 0;
            cmbKendaraan.SelectedIndex = 0;
            cmbKategori.SelectedIndex = 0; // Use renamed control
            txtNamaPaket.Clear();
            txtDestinasi.Clear();
            txtHarga.Clear();
            txtDurasi.Clear();
            txtFasilitas.Clear();
            txtKuota.Clear();
            dtpJadwalKeberangkatan.Value = DateTime.Now; // Use renamed control
            dgvPaketWisata.ClearSelection();
            txtNamaPaket.Focus();
            errorProvider1.Clear(); // Use the form's error provider
        }

        private void HandleSqlError(SqlException ex, string operation)
        {
            // Simplified error handling for now, can be expanded as before
            MessageBox.Show($"Error Database saat {operation} paket: {ex.Message}\nNomor Error: {ex.Number}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            // Add specific FK checks if needed, especially for INSERT/UPDATE
        }


        // --- DataGridView Event Handlers ---
        private void PopulateFieldsFromSelectedRow()
        {
            // Hanya jalankan jika ada baris yang dipilih.
            if (dgvPaketWisata.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvPaketWisata.SelectedRows[0];

                // Use column NAME defined in Designer.cs or programmatically
                if (row.Cells["colIDPaket"].Value != null && int.TryParse(row.Cells["colIDPaket"].Value.ToString(), out int idPaket))
                { selectedPaketId = idPaket; }
                else { selectedPaketId = -1; return; } // Cukup keluar jika ID tidak valid

                // Set ComboBoxes using the hidden ID columns
                cmbDriver.SelectedValue = row.Cells["colIDDriver_hidden"].Value ?? DBNull.Value;
                cmbKendaraan.SelectedValue = row.Cells["colIDKendaraan_hidden"].Value ?? DBNull.Value;

                // Set Kategori ComboBox
                string kategoriValue = row.Cells["colKategori"].Value?.ToString();
                cmbKategori.SelectedItem = kategoriValue;
                if (cmbKategori.SelectedIndex < 0) { cmbKategori.SelectedIndex = 0; }

                // Set TextBoxes
                txtNamaPaket.Text = row.Cells["colNamaPaket"].Value?.ToString();
                txtDestinasi.Text = row.Cells["colDestinasi"].Value?.ToString();
                txtHarga.Text = row.Cells["colHarga"].Value?.ToString();
                txtDurasi.Text = row.Cells["colDurasi"].Value?.ToString();
                txtFasilitas.Text = row.Cells["colFasilitas"].Value?.ToString();
                txtKuota.Text = row.Cells["colKuota"].Value?.ToString();

                // Set DateTimePicker
                object jadwalValue = row.Cells["colJadwal"].Value;
                if (jadwalValue != null && jadwalValue != DBNull.Value)
                {
                    try { dtpJadwalKeberangkatan.Value = Convert.ToDateTime(jadwalValue); }
                    catch { dtpJadwalKeberangkatan.Value = DateTime.Now; } // Fallback
                }
                else { dtpJadwalKeberangkatan.Value = DateTime.Now; }

                errorProvider1.Clear(); // Clear error provider
            }
            // else
            // { 
            //     ClearInputs(); 
            // }  <-- BLOK INI DIHAPUS
        }

        private void DgvPaketWisata_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // e.RowIndex >= 0 ensures it's not a header click
            if (e.RowIndex >= 0)
            {
                // Select the whole row when a cell is clicked
                if (!dgvPaketWisata.Rows[e.RowIndex].Selected)
                {
                    dgvPaketWisata.ClearSelection();
                    dgvPaketWisata.Rows[e.RowIndex].Selected = true;
                }
                // PopulateFieldsFromSelectedRow is called by SelectionChanged, no need here
                // PopulateFieldsFromSelectedRow();
            }
        }

        // Added SelectionChanged handler for robustness
        private void DgvPaketWisata_SelectionChanged(object sender, EventArgs e)
        {
            PopulateFieldsFromSelectedRow();
        }

        private void inputPanel_Paint(object sender, PaintEventArgs e)
        {


        }

        // Letakkan method ini bersama dengan event handler tombol lainnya (seperti btnTambah_Click)

        private void btnKembali_Click(object sender, EventArgs e)
        {
            // Perintah ini akan menutup form 'Kendaraan' saat ini,
            // dan mengembalikan kontrol ke form yang membukanya (yaitu MenuAdmin).
            this.Close();
        }

        private void txtKuota_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }
    }
}