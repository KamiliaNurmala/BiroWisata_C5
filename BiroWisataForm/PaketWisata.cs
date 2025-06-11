using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing; // Added for Font/Color if needed
using System.Windows.Forms;
using System.Globalization; // Added for parsing potentially
using System.Linq;

namespace BiroWisataForm
{
    public partial class PaketWisata : Form
    {
        // --- Member Variables ---
        private string connectionString = @"Data Source=KAMILIA\KAMILIANURMALA;Initial Catalog=BiroWisata;Integrated Security=True;";
        private int selectedPaketId = -1;

        // --- Constructor ---
        public PaketWisata()
        {
            InitializeComponent();
            InitializeDataGridViewSettings(); // Configure DGV properties only
            InitializeSearchBox(); // <--- TAMBAHKAN INI
            LoadDrivers();
            LoadKendaraan();
            LoadKategori();
            // RefreshData is called in Load event
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
                txtSearch.TextChanged += (s, e) =>
                {
                    RefreshData(txtSearch.Text);
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
            using (var conn = new SqlConnection(connectionString))
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
            using (var conn = new SqlConnection(connectionString))
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

        private bool RefreshData(string searchTerm = null) // <-- CHANGED from void to bool
        {
            using (var conn = new SqlConnection(connectionString))
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
                        query += " AND (p.NamaPaket LIKE @SearchTerm OR p.Destinasi LIKE @SearchTerm OR p.Kategori LIKE @SearchTerm OR d.NamaDriver LIKE @SearchTerm)";
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

                    ClearInputs();
                    return true; // <-- ADDED: Return true on success
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Error DB Refresh Paket: {ex.Message}", "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false; // <-- ADDED: Return false on database error
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error Refresh Paket: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false; // <-- ADDED: Return false on general error
                }
            }
            // The ClearInputs() call was here, but it's better inside the 'try' block
            // so it only runs on a successful refresh.
        }


        // --- CRUD Button Event Handlers ---
        private void btnTambah_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            decimal.TryParse(txtHarga.Text.Trim(), out decimal harga);
            short.TryParse(txtDurasi.Text.Trim(), out short durasi);
            short.TryParse(txtKuota.Text.Trim(), out short kuota);

            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("sp_AddPaketWisata", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

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
                        cmd.Parameters.AddWithValue("@CreatedBy", Environment.UserName);

                        // --- PERUBAHAN DI SINI ---
                        // We execute the command. If it fails, it will throw an exception.
                        // If it succeeds, we proceed to the success message.
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Paket wisata berhasil ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RefreshData();
                    }
                }
                catch (SqlException ex) { HandleSqlError(ex, "menambahkan"); }
                catch (Exception ex) { MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
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

            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("sp_UpdatePaketWisata", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

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

                        // --- PERUBAHAN DI SINI ---
                        // We execute the command. If it fails, it throws an exception.
                        // If the ID doesn't exist, it simply does nothing but doesn't throw an error.
                        // This is a more direct way to handle SPs with SET NOCOUNT ON.
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Paket wisata berhasil diubah!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RefreshData();
                    }
                }
                catch (SqlException ex) { HandleSqlError(ex, "mengubah"); }
                catch (Exception ex) { MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
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

            SqlConnection conn = new SqlConnection(connectionString);
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
            if (this.Controls.Find("txtSearch", true).FirstOrDefault() is TextBox txtSearch)
            {
                txtSearch.Clear();
            }

            // --- PERUBAHAN DI SINI ---
            // Call RefreshData and check if it returned true (meaning it succeeded)
            if (RefreshData())
            {
                MessageBox.Show("Data berhasil dimuat ulang.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            // If RefreshData() fails, it will return false and will have already shown its own error message.
        }


        // --- Helper Methods ---
        private bool ValidateInputs() // Removed unused isUpdate parameter
        {
            this.errorProvider1.Clear(); // Use the error provider from the designer
            bool isValid = true;

            // Validate ComboBox FKs
            if (cmbDriver.SelectedValue == null || cmbDriver.SelectedValue == DBNull.Value || cmbDriver.SelectedIndex <= 0) // Check index too for placeholder
            { errorProvider1.SetError(cmbDriver, "Pilih Driver yang valid!"); isValid = false; }
            if (cmbKendaraan.SelectedValue == null || cmbKendaraan.SelectedValue == DBNull.Value || cmbKendaraan.SelectedIndex <= 0)
            { errorProvider1.SetError(cmbKendaraan, "Pilih Kendaraan yang valid!"); isValid = false; }
            if (cmbKategori.SelectedIndex <= 0) // Use renamed control
            { errorProvider1.SetError(cmbKategori, "Pilih Kategori yang valid!"); isValid = false; }

            // Validate TextBoxes
            if (string.IsNullOrWhiteSpace(txtNamaPaket.Text))
            { errorProvider1.SetError(txtNamaPaket, "Nama Paket tidak boleh kosong!"); isValid = false; }
            if (string.IsNullOrWhiteSpace(txtDestinasi.Text))
            { errorProvider1.SetError(txtDestinasi, "Destinasi tidak boleh kosong!"); isValid = false; }
            if (string.IsNullOrWhiteSpace(txtFasilitas.Text))
            { errorProvider1.SetError(txtFasilitas, "Fasilitas tidak boleh kosong!"); isValid = false; }

            // Validate Numeric TextBoxes
            if (string.IsNullOrWhiteSpace(txtHarga.Text))
            { errorProvider1.SetError(txtHarga, "Harga tidak boleh kosong!"); isValid = false; }
            else if (!decimal.TryParse(txtHarga.Text, out decimal harga) || harga <= 0)
            { errorProvider1.SetError(txtHarga, "Harga harus berupa angka positif!"); isValid = false; }

            if (string.IsNullOrWhiteSpace(txtDurasi.Text))
            { errorProvider1.SetError(txtDurasi, "Durasi tidak boleh kosong!"); isValid = false; }
            else if (!short.TryParse(txtDurasi.Text, out short durasi) || durasi <= 0) // Use short for smallint
            { errorProvider1.SetError(txtDurasi, "Durasi harus berupa angka positif!"); isValid = false; }

            if (string.IsNullOrWhiteSpace(txtKuota.Text))
            { errorProvider1.SetError(txtKuota, "Kuota tidak boleh kosong!"); isValid = false; }
            else if (!short.TryParse(txtKuota.Text, out short kuota) || kuota < 0) // Use short for smallint
            { errorProvider1.SetError(txtKuota, "Kuota harus berupa angka non-negatif!"); isValid = false; }

            // Basic validation for DateTimePicker (usually less critical unless specific range needed)
            // if (dtpJadwalKeberangkatan.Value.Date < DateTime.Now.Date)
            // { errorProvider1.SetError(dtpJadwalKeberangkatan, "Jadwal tidak boleh di masa lalu!"); isValid = false; }

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
            if (dgvPaketWisata.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvPaketWisata.SelectedRows[0];

                // Use column NAME defined in Designer.cs or programmatically
                if (row.Cells["colIDPaket"].Value != null && int.TryParse(row.Cells["colIDPaket"].Value.ToString(), out int idPaket))
                { selectedPaketId = idPaket; }
                else { selectedPaketId = -1; ClearInputs(); return; }

                // Set ComboBoxes using the hidden ID columns
                cmbDriver.SelectedValue = row.Cells["colIDDriver_hidden"].Value ?? DBNull.Value;
                cmbKendaraan.SelectedValue = row.Cells["colIDKendaraan_hidden"].Value ?? DBNull.Value;

                // Set Kategori ComboBox
                string kategoriValue = row.Cells["colKategori"].Value?.ToString();
                cmbKategori.SelectedItem = kategoriValue; // Use renamed control
                if (cmbKategori.SelectedIndex < 0) { cmbKategori.SelectedIndex = 0; }

                // Set TextBoxes
                txtNamaPaket.Text = row.Cells["colNamaPaket"].Value?.ToString();
                txtDestinasi.Text = row.Cells["colDestinasi"].Value?.ToString();
                txtHarga.Text = row.Cells["colHarga"].Value?.ToString();
                txtDurasi.Text = row.Cells["colDurasi"].Value?.ToString();
                txtFasilitas.Text = row.Cells["colFasilitas"].Value?.ToString();
                txtKuota.Text = row.Cells["colKuota"].Value?.ToString();

                // Set DateTimePicker (Use renamed control)
                object jadwalValue = row.Cells["colJadwal"].Value;
                if (jadwalValue != null && jadwalValue != DBNull.Value)
                {
                    try { dtpJadwalKeberangkatan.Value = Convert.ToDateTime(jadwalValue); }
                    catch { dtpJadwalKeberangkatan.Value = DateTime.Now; } // Fallback
                }
                else { dtpJadwalKeberangkatan.Value = DateTime.Now; }

                errorProvider1.Clear(); // Clear error provider
            }
            else { ClearInputs(); } // Clear if no row selected
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

    }
}