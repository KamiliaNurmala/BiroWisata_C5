// Operasional.cs
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Globalization;
using System.Linq;
using praktikum7;

namespace BiroWisataForm
{
    public partial class Operasional : Form
    {
        Koneksi kn = new Koneksi();
        private string connectionString = "";
        private int selectedOperasionalId = -1;

        public Operasional()
        {
            connectionString = kn.connectionString();

            InitializeComponent();
            EnsureDatabaseIndexes();
            InitializeDataGridView();
            InitializeSearchBox(); // New method to setup search
            LoadPaketWisata();
            LoadDrivers();
            LoadKendaraan();
            LoadJenisPengeluaran();
        }


        private void EnsureDatabaseIndexes()
        {
            // Menggabungkan semua skrip CREATE INDEX untuk tabel Operasional
            string scriptIndex = @"
                IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_Operasional_IDPaket' AND object_id = OBJECT_ID('dbo.Operasional'))
                BEGIN
                    CREATE NONCLUSTERED INDEX IX_Operasional_IDPaket ON dbo.Operasional(IDPaket);
                END;

                IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_Operasional_IDDriver' AND object_id = OBJECT_ID('dbo.Operasional'))
                BEGIN
                    CREATE NONCLUSTERED INDEX IX_Operasional_IDDriver ON dbo.Operasional(IDDriver);
                END;

                IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_Operasional_IDKendaraan' AND object_id = OBJECT_ID('dbo.Operasional'))
                BEGIN
                    CREATE NONCLUSTERED INDEX IX_Operasional_IDKendaraan ON dbo.Operasional(IDKendaraan);
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
                        // Log untuk debugging
                        Console.WriteLine("Pengecekan indeks untuk tabel Operasional selesai.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal memverifikasi/membuat indeks untuk Operasional: {ex.Message}",
                                "Kesalahan Konfigurasi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Operasional_Load(object sender, EventArgs e)
        {
            RefreshData();
            // Event handlers are now managed by the Designer.cs, no need for manual WireUp
        }

        private void InitializeDataGridView()
        {
            dgvOperasional.AutoGenerateColumns = false;
            dgvOperasional.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOperasional.MultiSelect = false;
            dgvOperasional.ReadOnly = true;
            dgvOperasional.AllowUserToAddRows = false;

            // Hapus atau pastikan tidak ada duplikasi jika sudah ada di designer
            dgvOperasional.CellClick -= DgvOperasional_CellClick;
            dgvOperasional.SelectionChanged -= DgvOperasional_SelectionChanged;

            // Tambahkan event handler
            dgvOperasional.CellClick += DgvOperasional_CellClick;
            dgvOperasional.SelectionChanged += DgvOperasional_SelectionChanged; // <-- INI YANG PENTING
        }

        private void InitializeSearchBox()
        {
            // Attach an event handler to the search TextBox
            txtCari.TextChanged += (sender, e) => {
                RefreshData(txtCari.Text);
            };
        }

        // PERBAIKAN BUG PENTING DI SINI
        private void LoadComboBoxData(ComboBox comboBox, string query, string displayMember, string valueMember, string placeholder)
        {
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
                    // --- BUG FIX: Ganti cmbJenisPengeluaran dengan parameter comboBox ---
                    if (comboBox.Items.Count > 0) comboBox.SelectedIndex = 0;
                    comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                    // -------------------------------------------------------------------
                }
                // PERBAIKAN: Tampilkan pesan error jika gagal memuat data combo
                catch (SqlException ex) { HandleSqlError(ex, $"memuat data for {comboBox.Name}"); }
                catch (Exception ex) { HandleLoadError($"data for {comboBox.Name}", ex); }
            }
        }

        private void LoadPaketWisata() { LoadComboBoxData(cmbPaket, "SELECT IDPaket, NamaPaket FROM PaketWisata WHERE IsDeleted = 0 ORDER BY NamaPaket", "NamaPaket", "IDPaket", "-- Pilih Paket Wisata --"); }
        private void LoadDrivers() { LoadComboBoxData(cmbDriver, "SELECT IDDriver, NamaDriver FROM Driver WHERE IsDeleted = 0 AND Status = 'Aktif' ORDER BY NamaDriver", "NamaDriver", "IDDriver", "-- Pilih Driver --"); }
        private void LoadKendaraan() { LoadComboBoxData(cmbKendaraan, "SELECT IDKendaraan, Jenis + ' (' + PlatNomor + ')' AS Deskripsi FROM Kendaraan WHERE Status = 'Aktif' ORDER BY Jenis, PlatNomor", "Deskripsi", "IDKendaraan", "-- Pilih Kendaraan --"); }

        private void LoadJenisPengeluaran()
        {
            cmbJenisPengeluaran.Items.Clear();
            cmbJenisPengeluaran.Items.Add("-- Pilih Jenis --");
            cmbJenisPengeluaran.Items.Add("BBM");
            cmbJenisPengeluaran.Items.Add("Tol");
            cmbJenisPengeluaran.Items.Add("Konsumsi Supir");
            if (cmbJenisPengeluaran.Items.Count > 0) cmbJenisPengeluaran.SelectedIndex = 0;
            cmbJenisPengeluaran.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        // DIUBAH menjadi bool
        private bool RefreshData(string searchTerm = null)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"SELECT o.IDOperasional, o.IDPaket, o.IDDriver, o.IDKendaraan, 
                                    o.BiayaBBM AS BiayaOperasional, o.JenisPengeluaran, 
                                    p.NamaPaket, d.NamaDriver, 
                                    k.Jenis + ' (' + k.PlatNomor + ')' AS KendaraanInfo
                               FROM Operasional o 
                               INNER JOIN PaketWisata p ON o.IDPaket = p.IDPaket 
                               INNER JOIN Driver d ON o.IDDriver = d.IDDriver 
                               INNER JOIN Kendaraan k ON o.IDKendaraan = k.IDKendaraan";

                    string whereClause = "";
                    if (!string.IsNullOrWhiteSpace(searchTerm))
                    {
                        whereClause = " WHERE p.NamaPaket LIKE @SearchTerm OR d.NamaDriver LIKE @SearchTerm OR (k.Jenis + ' (' + k.PlatNomor + ')') LIKE @SearchTerm OR o.JenisPengeluaran LIKE @SearchTerm";
                    }
                    query += whereClause;

                    // --- PERUBAHAN UTAMA DI SINI ---
                    // Logika pengurutan baru: Prioritaskan hasil pencarian
                    string orderByClause;
                    if (!string.IsNullOrWhiteSpace(searchTerm))
                    {
                        // Jika sedang mencari, urutkan berdasarkan relevansi, lalu ID
                        orderByClause = @" ORDER BY 
                                    CASE 
                                        WHEN p.NamaPaket LIKE @SearchTerm THEN 1
                                        WHEN o.JenisPengeluaran LIKE @SearchTerm THEN 2
                                        ELSE 3
                                    END, 
                                    o.IDOperasional DESC";
                    }
                    else
                    {
                        // Jika tidak mencari, urutkan seperti biasa
                        orderByClause = " ORDER BY o.IDOperasional DESC";
                    }
                    query += orderByClause;
                    // --- AKHIR PERUBAHAN ---

                    var dt = new DataTable();
                    var adapter = new SqlDataAdapter(query, conn);

                    if (!string.IsNullOrWhiteSpace(searchTerm))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@SearchTerm", $"%{searchTerm}%");
                    }

                    adapter.Fill(dt);
                    dgvOperasional.DataSource = dt;

                    if (dgvOperasional.Rows.Count == 1)
                    {
                        dgvOperasional.Rows[0].Selected = true;
                    }
                    else
                    {
                        ClearInputs();
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    HandleGeneralError("Refresh Data Operasional", ex);
                    return false;
                }
            }
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs(out decimal biaya)) return;

            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("sp_AddOperasional", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IDPaket", cmbPaket.SelectedValue);
                        cmd.Parameters.AddWithValue("@IDDriver", cmbDriver.SelectedValue);
                        cmd.Parameters.AddWithValue("@IDKendaraan", cmbKendaraan.SelectedValue);
                        // Pastikan nama parameter @BiayaOperasional sesuai dengan di Stored Procedure Anda
                        // Jika di SP namanya @BiayaBBM, ganti di sini.
                        cmd.Parameters.AddWithValue("@BiayaOperasional", biaya);
                        cmd.Parameters.AddWithValue("@JenisPengeluaran", cmbJenisPengeluaran.SelectedItem.ToString());

                        // --- PERUBAHAN DI SINI ---
                        cmd.ExecuteNonQuery(); // Hapus IF 
                        MessageBox.Show("Data operasional berhasil ditambahkan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RefreshData(); // RefreshData akan memanggil ClearInputs()
                        // -------------------------
                    }
                }
                catch (SqlException ex) { HandleSqlError(ex, "menambahkan data"); }
                catch (Exception ex) { HandleGeneralError("menambahkan data", ex); }
            }
        }

        private void btnUbah_Click(object sender, EventArgs e)
        {
            if (selectedOperasionalId < 0) { MessageBox.Show("Pilih data yang akan diubah.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (!ValidateInputs(out decimal biaya)) return;

            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("sp_UpdateOperasional", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IDOperasional", selectedOperasionalId);
                        cmd.Parameters.AddWithValue("@IDPaket", cmbPaket.SelectedValue);
                        cmd.Parameters.AddWithValue("@IDDriver", cmbDriver.SelectedValue);
                        cmd.Parameters.AddWithValue("@IDKendaraan", cmbKendaraan.SelectedValue);
                        // Pastikan nama parameter @BiayaOperasional sesuai dengan di Stored Procedure Anda
                        cmd.Parameters.AddWithValue("@BiayaOperasional", biaya);
                        cmd.Parameters.AddWithValue("@JenisPengeluaran", cmbJenisPengeluaran.SelectedItem.ToString());

                        // --- PERUBAHAN DI SINI: Hapus IF dan ELSE ---
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Data operasional berhasil diubah.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RefreshData(); // RefreshData akan memanggil ClearInputs()
                                       // ----------------------------------------------
                    }
                }
                catch (SqlException ex) { HandleSqlError(ex, "mengubah data"); }
                catch (Exception ex) { HandleGeneralError("mengubah data", ex); }
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (selectedOperasionalId < 0) { MessageBox.Show("Pilih data yang akan dihapus.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            if (MessageBox.Show("Yakin ingin menghapus data ini?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        using (var cmd = new SqlCommand("sp_DeleteOperasional", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@IDOperasional", selectedOperasionalId);

                            // --- PERUBAHAN DI SINI ---
                            cmd.ExecuteNonQuery(); // Hapus IF
                            MessageBox.Show("Data operasional berhasil dihapus.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            RefreshData(); // RefreshData akan memanggil ClearInputs()
                                           // -------------------------
                        }
                    }
                    catch (SqlException ex) { HandleSqlError(ex, "menghapus data"); }
                    catch (Exception ex) { HandleGeneralError("menghapus data", ex); }
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            // Bersihkan kotak pencarian. Ini akan otomatis memicu RefreshData("")
            txtCari.Clear();

            // Panggil ClearInputs() di sini untuk memastikan form bersih setelah refresh manual
            ClearInputs();

            // Refresh data sekali lagi untuk memastikan state bersih total
            if (RefreshData())
            {
                MessageBox.Show("Data telah dimuat ulang.", "Refresh Selesai", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private bool ValidateInputs(out decimal biaya)
        {
            biaya = 0;
            string errorMsg = "";
            bool isValid = true;
            if (cmbPaket.SelectedValue == null || cmbPaket.SelectedValue == DBNull.Value)
            { errorMsg += "- Paket Wisata harus dipilih.\n"; isValid = false; }
            if (cmbDriver.SelectedValue == null || cmbDriver.SelectedValue == DBNull.Value)
            { errorMsg += "- Driver harus dipilih.\n"; isValid = false; }
            if (cmbKendaraan.SelectedValue == null || cmbKendaraan.SelectedValue == DBNull.Value)
            { errorMsg += "- Kendaraan harus dipilih.\n"; isValid = false; }
            if (cmbJenisPengeluaran.SelectedIndex <= 0)
            { errorMsg += "- Jenis Pengeluaran harus dipilih.\n"; isValid = false; }
            if (string.IsNullOrWhiteSpace(txtBiaya.Text))
            { errorMsg += "- Biaya tidak boleh kosong.\n"; isValid = false; }
            else if (!decimal.TryParse(txtBiaya.Text.Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out biaya) || biaya <= 0)
            { errorMsg += "- Biaya harus berupa angka positif.\n"; isValid = false; }

            if (!isValid) { MessageBox.Show("Harap perbaiki input yang tidak valid:\n" + errorMsg, "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            return isValid;
        }

        private void ClearInputs()
        {
            selectedOperasionalId = -1;
            if (cmbPaket.Items.Count > 0) cmbPaket.SelectedIndex = 0;
            if (cmbDriver.Items.Count > 0) cmbDriver.SelectedIndex = 0;
            if (cmbKendaraan.Items.Count > 0) cmbKendaraan.SelectedIndex = 0;
            if (cmbJenisPengeluaran.Items.Count > 0) cmbJenisPengeluaran.SelectedIndex = 0;
            txtBiaya.Clear();
            dgvOperasional.ClearSelection();

            // if (cmbPaket.CanFocus) cmbPaket.Focus(); // <-- HAPUS BARIS INI
        }

        private void PopulateFieldsFromSelectedRow()
        {
            // Hanya berjalan jika ada baris yang benar-benar dipilih
            if (dgvOperasional.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvOperasional.SelectedRows[0];
                try
                {
                    if (row.Cells["colIDOperasional"].Value != null && int.TryParse(row.Cells["colIDOperasional"].Value.ToString(), out int id))
                    {
                        selectedOperasionalId = id;
                    }

                    // Gunakan SelectedValue untuk ComboBox yang di-binding
                    cmbPaket.SelectedValue = row.Cells["colIDPaket"].Value ?? DBNull.Value;
                    cmbDriver.SelectedValue = row.Cells["colIDDriver"].Value ?? DBNull.Value;
                    cmbKendaraan.SelectedValue = row.Cells["colIDKendaraan"].Value ?? DBNull.Value;

                    // Gunakan SelectedItem untuk ComboBox yang diisi manual
                    cmbJenisPengeluaran.SelectedItem = row.Cells["colJenisPengeluaran"].Value?.ToString();

                    // Atur fallback jika ada nilai yang tidak ditemukan di ComboBox
                    if (cmbPaket.SelectedIndex < 0) cmbPaket.SelectedIndex = 0;
                    if (cmbDriver.SelectedIndex < 0) cmbDriver.SelectedIndex = 0;
                    if (cmbKendaraan.SelectedIndex < 0) cmbKendaraan.SelectedIndex = 0;
                    if (cmbJenisPengeluaran.SelectedIndex < 0) cmbJenisPengeluaran.SelectedIndex = 0;

                    txtBiaya.Text = row.Cells["colBiayaOperasional"].Value?.ToString();
                }
                catch (Exception ex)
                {
                    HandleGeneralError("memuat data dari baris terpilih", ex);
                    ClearInputs();
                }
            }
        }


        // GANTI event CellClick Anda dengan ini
        private void DgvOperasional_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Cukup pastikan baris yang diklik terpilih.
            // Logika pengisian form akan ditangani oleh SelectionChanged.
            if (e.RowIndex >= 0)
            {
                if (!dgvOperasional.Rows[e.RowIndex].Selected)
                {
                    dgvOperasional.ClearSelection();
                    dgvOperasional.Rows[e.RowIndex].Selected = true;
                }
            }
        }

        // TAMBAHKAN event handler baru ini
        private void DgvOperasional_SelectionChanged(object sender, EventArgs e)
        {
            // Setiap kali seleksi berubah, panggil metode pengisian form
            PopulateFieldsFromSelectedRow();
        }

        private void cmbJenisPengeluaran_SelectedIndexChanged(object sender, EventArgs e) { /* Placeholder for future logic */ }
        private void HandleSqlError(SqlException ex, string operation) { MessageBox.Show($"Error DB saat {operation}: {ex.Message} (No: {ex.Number})", "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        private void HandleGeneralError(string operation, Exception ex) { MessageBox.Show($"Error saat {operation}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        private void HandleLoadError(string dataType, Exception ex) { MessageBox.Show($"Gagal memuat {dataType}: {ex.Message}", "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }

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
    }
}