// Operasional.cs
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Globalization; // Diperlukan untuk TryParse dengan CultureInfo

namespace BiroWisataForm
{
    public partial class Operasional : Form
    {
        // --- Variabel Member ---
        private string connectionString = @"Data Source=KAMILIA\KAMILIANURMALA;Initial Catalog=BiroWisata;Integrated Security=True;";
        private int selectedOperasionalId = -1; // Menyimpan ID Operasional yang dipilih

        // --- Constructor ---
        public Operasional()
        {
            InitializeComponent(); // Memuat kontrol dari Designer
            // Panggil metode inisialisasi setelah InitializeComponent
            InitializeDataGridView();
            LoadPaketWisata();
            LoadDrivers();
            LoadKendaraan();
            LoadJenisPengeluaran();
            // RefreshData dipanggil di Load event agar form tampil dulu
        }

        // --- Form Load Event Handler (Pastikan terhubung di Designer) ---
        private void Operasional_Load(object sender, EventArgs e)
        {
            RefreshData(); // Tampilkan data awal saat form siap
            ClearInputs(); // Pastikan bersih saat mulai
                           // Kaitkan event handler secara eksplisit jika perlu (double check di Designer)
            WireUpEventHandlers();
        }

        // --- Inisialisasi Kontrol ---
        private void InitializeDataGridView()
        {
            dgvOperasional.AutoGenerateColumns = false;
            dgvOperasional.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOperasional.MultiSelect = false;
            dgvOperasional.ReadOnly = true;
            dgvOperasional.AllowUserToAddRows = false;
            dgvOperasional.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Fill sisa ruang
            dgvOperasional.CellClick += DgvOperasional_CellClick;

            dgvOperasional.Columns.Clear(); // Pastikan bersih sebelum menambah kolom

            // Definisikan Kolom
            dgvOperasional.Columns.Add(new DataGridViewTextBoxColumn { Name = "IDOperasional", HeaderText = "ID", DataPropertyName = "IDOperasional", Visible = false });
            dgvOperasional.Columns.Add(new DataGridViewTextBoxColumn { Name = "NamaPaket", HeaderText = "Paket Wisata", DataPropertyName = "NamaPaket", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells });
            dgvOperasional.Columns.Add(new DataGridViewTextBoxColumn { Name = "NamaDriver", HeaderText = "Driver", DataPropertyName = "NamaDriver", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells });
            dgvOperasional.Columns.Add(new DataGridViewTextBoxColumn { Name = "KendaraanInfo", HeaderText = "Kendaraan", DataPropertyName = "KendaraanInfo", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells });
            dgvOperasional.Columns.Add(new DataGridViewTextBoxColumn { Name = "JenisPengeluaran", HeaderText = "Jenis", DataPropertyName = "JenisPengeluaran", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells });
            dgvOperasional.Columns.Add(new DataGridViewTextBoxColumn { Name = "BiayaOperasional", HeaderText = "Biaya", DataPropertyName = "BiayaOperasional", DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight }, AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells });
            // Sembunyikan ID FK
            dgvOperasional.Columns.Add(new DataGridViewTextBoxColumn { Name = "IDPaket", DataPropertyName = "IDPaket", Visible = false });
            dgvOperasional.Columns.Add(new DataGridViewTextBoxColumn { Name = "IDDriver", DataPropertyName = "IDDriver", Visible = false });
            dgvOperasional.Columns.Add(new DataGridViewTextBoxColumn { Name = "IDKendaraan", DataPropertyName = "IDKendaraan", Visible = false });
        }

        // --- Kaitkan Event Handler (Panggil dari Constructor atau Load) ---
        private void WireUpEventHandlers()
        {
            // Pastikan event handler tombol terhubung (bisa juga via Properties Window di Designer)
            this.btnTambah.Click -= new System.EventHandler(this.btnTambah_Click); // Hapus dulu untuk hindari duplikasi
            this.btnTambah.Click += new System.EventHandler(this.btnTambah_Click);

            this.btnUbah.Click -= new System.EventHandler(this.btnUbah_Click);
            this.btnUbah.Click += new System.EventHandler(this.btnUbah_Click);

            this.btnHapus.Click -= new System.EventHandler(this.btnHapus_Click);
            this.btnHapus.Click += new System.EventHandler(this.btnHapus_Click);

            this.btnRefresh.Click -= new System.EventHandler(this.btnRefresh_Click);
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            // Event handler untuk ComboBox (jika diperlukan aksi saat pilihan berubah)
            this.cmbJenisPengeluaran.SelectedIndexChanged -= new System.EventHandler(this.cmbJenisPengeluaran_SelectedIndexChanged);
            this.cmbJenisPengeluaran.SelectedIndexChanged += new System.EventHandler(this.cmbJenisPengeluaran_SelectedIndexChanged);
        }


        // --- Memuat Data ke Kontrol (ComboBoxes) ---
        private void LoadComboBoxData(ComboBox comboBox, string query, string displayMember, string valueMember, string placeholder)
        {
            // ... (Kode LoadComboBoxData Anda sudah benar) ...
            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open(); var dt = new DataTable(); var adapter = new SqlDataAdapter(query, conn); adapter.Fill(dt);
                    var emptyRow = dt.NewRow(); emptyRow[valueMember] = DBNull.Value; emptyRow[displayMember] = placeholder; dt.Rows.InsertAt(emptyRow, 0);
                    comboBox.DataSource = dt; comboBox.DisplayMember = displayMember; comboBox.ValueMember = valueMember; comboBox.SelectedIndex = 0;
                    comboBox.DropDownStyle = ComboBoxStyle.DropDownList; // Pastikan tidak bisa diketik
                }
                catch (Exception ex) { HandleLoadError(displayMember, ex); }
            }
        }
        private void LoadPaketWisata() { LoadComboBoxData(cmbPaket, "SELECT IDPaket, NamaPaket FROM PaketWisata ORDER BY NamaPaket", "NamaPaket", "IDPaket", "-- Pilih Paket Wisata --"); }
        private void LoadDrivers() { LoadComboBoxData(cmbDriver, "SELECT IDDriver, NamaDriver FROM Driver WHERE Status = 'Aktif' ORDER BY NamaDriver", "NamaDriver", "IDDriver", "-- Pilih Driver --"); }
        private void LoadKendaraan() { LoadComboBoxData(cmbKendaraan, "SELECT IDKendaraan, Jenis + ' - ' + PlatNomor AS Deskripsi FROM Kendaraan ORDER BY Deskripsi", "Deskripsi", "IDKendaraan", "-- Pilih Kendaraan --"); }
        private void LoadJenisPengeluaran()
        {
            // ... (Kode LoadJenisPengeluaran Anda sudah benar) ...
            cmbJenisPengeluaran.Items.Clear();
            cmbJenisPengeluaran.Items.Add("-- Pilih Jenis --"); cmbJenisPengeluaran.Items.Add("BBM"); cmbJenisPengeluaran.Items.Add("Tol"); cmbJenisPengeluaran.Items.Add("Konsumsi Supir");
            cmbJenisPengeluaran.SelectedIndex = 0;
            cmbJenisPengeluaran.DropDownStyle = ComboBoxStyle.DropDownList; // Pastikan tidak bisa diketik
        }

        // --- Memuat Data ke Grid ---
        private void RefreshData()
        {
            // ... (Kode RefreshData Anda sudah benar, pastikan alias BiayaOperasional ada) ...
            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"SELECT o.IDOperasional, o.IDPaket, o.IDDriver, o.IDKendaraan, o.BiayaBBM AS BiayaOperasional, o.JenisPengeluaran, p.NamaPaket, d.NamaDriver, k.Jenis + ' - ' + k.PlatNomor AS KendaraanInfo
                                     FROM Operasional o INNER JOIN PaketWisata p ON o.IDPaket = p.IDPaket INNER JOIN Driver d ON o.IDDriver = d.IDDriver INNER JOIN Kendaraan k ON o.IDKendaraan = k.IDKendaraan
                                     ORDER BY p.NamaPaket, o.IDOperasional";
                    var dt = new DataTable(); var adapter = new SqlDataAdapter(query, conn); adapter.Fill(dt);
                    dgvOperasional.DataSource = null; dgvOperasional.DataSource = dt;
                }
                catch (Exception ex) { HandleLoadError("Refresh Data Operasional", ex); }
            }
            // ClearInputs(); // Panggil ClearInputs secara terpisah jika diperlukan setelah refresh
        }

        // --- Event Handler Tombol CRUD ---
        private void btnTambah_Click(object sender, EventArgs e)
        {
            // ... (Kode btnTambah_Click Anda sudah benar) ...
            if (!ValidateInputs()) return;
            if (!decimal.TryParse(txtBiaya.Text.Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal biaya) || biaya <= 0)
            { MessageBox.Show("Biaya harus angka positif.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtBiaya.Focus(); return; }

            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string sql = @"INSERT INTO Operasional (IDPaket, IDDriver, IDKendaraan, BiayaBBM, JenisPengeluaran) VALUES (@IDPaket, @IDDriver, @IDKendaraan, @Biaya, @Jenis)";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@IDPaket", cmbPaket.SelectedValue); cmd.Parameters.AddWithValue("@IDDriver", cmbDriver.SelectedValue); cmd.Parameters.AddWithValue("@IDKendaraan", cmbKendaraan.SelectedValue);
                        cmd.Parameters.AddWithValue("@Biaya", biaya); cmd.Parameters.AddWithValue("@Jenis", cmbJenisPengeluaran.SelectedItem.ToString());
                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Data operasional ditambahkan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            RefreshData(); ClearInputs(); // Refresh dan Clear
                        }
                        else { /* Handle no rows */ }
                    }
                }
                catch (SqlException ex) { HandleSqlError(ex, "menambahkan"); }
                catch (Exception ex) { HandleGeneralError("menambahkan", ex); }
            }
        }

        private void btnUbah_Click(object sender, EventArgs e)
        {
            // ... (Kode btnUbah_Click Anda sudah benar) ...
            if (selectedOperasionalId < 0) { MessageBox.Show("Pilih data untuk diubah.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (!ValidateInputs()) return;
            if (!decimal.TryParse(txtBiaya.Text.Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal biaya) || biaya <= 0)
            { MessageBox.Show("Biaya harus angka positif.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtBiaya.Focus(); return; }

            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string sql = @"UPDATE Operasional SET IDPaket = @IDPaket, IDDriver = @IDDriver, IDKendaraan = @IDKendaraan, BiayaBBM = @Biaya, JenisPengeluaran = @Jenis WHERE IDOperasional = @ID";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@IDPaket", cmbPaket.SelectedValue); cmd.Parameters.AddWithValue("@IDDriver", cmbDriver.SelectedValue); cmd.Parameters.AddWithValue("@IDKendaraan", cmbKendaraan.SelectedValue);
                        cmd.Parameters.AddWithValue("@Biaya", biaya); cmd.Parameters.AddWithValue("@Jenis", cmbJenisPengeluaran.SelectedItem.ToString()); cmd.Parameters.AddWithValue("@ID", selectedOperasionalId);
                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Data operasional diubah.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            RefreshData(); ClearInputs(); // Refresh dan Clear
                        }
                        else { MessageBox.Show("Data tidak berubah/ditemukan.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    }
                }
                catch (SqlException ex) { HandleSqlError(ex, "mengubah"); }
                catch (Exception ex) { HandleGeneralError("mengubah", ex); }
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            // ... (Kode btnHapus_Click Anda sudah benar) ...
            if (selectedOperasionalId < 0) { MessageBox.Show("Pilih data untuk dihapus.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (MessageBox.Show("Yakin hapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open(); string sql = "DELETE FROM Operasional WHERE IDOperasional = @ID";
                        using (var cmd = new SqlCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("@ID", selectedOperasionalId);
                            if (cmd.ExecuteNonQuery() > 0)
                            {
                                MessageBox.Show("Data operasional dihapus.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                RefreshData(); ClearInputs(); // Refresh dan Clear
                            }
                            else { /* Handle not found */ }
                        }
                    }
                    catch (SqlException ex) { HandleSqlError(ex, "menghapus"); }
                    catch (Exception ex) { HandleGeneralError("menghapus", ex); }
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshData();
            ClearInputs(); // Selalu clear input saat refresh manual
            MessageBox.Show("Data telah dimuat ulang.", "Refresh Selesai", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // --- Helper Methods ---
        private bool ValidateInputs()
        {
            // ... (Kode ValidateInputs Anda sudah benar) ...
            string errorMsg = ""; bool valid = true;
            if (cmbPaket.SelectedIndex <= 0) { errorMsg += "- Pilih Paket Wisata.\n"; valid = false; if (valid) cmbPaket.Focus(); }
            if (cmbDriver.SelectedIndex <= 0) { errorMsg += "- Pilih Driver.\n"; valid = false; if (valid) cmbDriver.Focus(); }
            if (cmbKendaraan.SelectedIndex <= 0) { errorMsg += "- Pilih Kendaraan.\n"; valid = false; if (valid) cmbKendaraan.Focus(); }
            if (cmbJenisPengeluaran.SelectedIndex <= 0) { errorMsg += "- Pilih Jenis Pengeluaran.\n"; valid = false; if (valid) cmbJenisPengeluaran.Focus(); }
            if (string.IsNullOrWhiteSpace(txtBiaya.Text)) { errorMsg += "- Biaya kosong.\n"; valid = false; if (valid) txtBiaya.Focus(); }
            else if (!decimal.TryParse(txtBiaya.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out _)) { errorMsg += "- Format Biaya salah.\n"; valid = false; if (valid) txtBiaya.Focus(); }

            if (!valid) { MessageBox.Show("Input tidak valid:\n" + errorMsg, "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            return valid;
        }

        private void ClearInputs()
        {
            // ... (Kode ClearInputs Anda sudah benar) ...
            selectedOperasionalId = -1; cmbPaket.SelectedIndex = 0; cmbDriver.SelectedIndex = 0; cmbKendaraan.SelectedIndex = 0; cmbJenisPengeluaran.SelectedIndex = 0;
            txtBiaya.Clear(); dgvOperasional.ClearSelection(); cmbPaket.Focus();
        }

        private void HandleSqlError(SqlException ex, string operation)
        {
            // ... (Kode HandleSqlError Anda sudah benar) ...
            MessageBox.Show($"Error DB saat {operation}: {ex.Message}\nNo: {ex.Number}", "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if (ex.Number == 547) { MessageBox.Show("Pastikan Paket, Driver, Kendaraan valid.", "Referensi Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        // Tambahkan helper error umum
        private void HandleGeneralError(string operation, Exception ex)
        {
            MessageBox.Show($"Error saat {operation}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        // Tambahkan helper error saat load data
        private void HandleLoadError(string dataType, Exception ex)
        {
            MessageBox.Show($"Error saat memuat data {dataType}: {ex.Message}", "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        // --- Event Handler Grid ---
        private void DgvOperasional_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // ... (Kode DgvOperasional_CellClick Anda sudah benar) ...
            if (e.RowIndex >= 0 && e.RowIndex < dgvOperasional.Rows.Count)
            {
                DataGridViewRow row = dgvOperasional.Rows[e.RowIndex];
                try
                {
                    if (row.Cells["IDOperasional"].Value != null && int.TryParse(row.Cells["IDOperasional"].Value.ToString(), out int id)) { selectedOperasionalId = id; }
                    else { selectedOperasionalId = -1; ClearInputs(); return; }

                    // Set FK ComboBoxes
                    cmbPaket.SelectedValue = row.Cells["IDPaket"].Value ?? DBNull.Value;
                    if (cmbPaket.SelectedIndex < 0) cmbPaket.SelectedIndex = 0; // Fallback jika FK tidak ada di list
                    cmbDriver.SelectedValue = row.Cells["IDDriver"].Value ?? DBNull.Value;
                    if (cmbDriver.SelectedIndex < 0) cmbDriver.SelectedIndex = 0;
                    cmbKendaraan.SelectedValue = row.Cells["IDKendaraan"].Value ?? DBNull.Value;
                    if (cmbKendaraan.SelectedIndex < 0) cmbKendaraan.SelectedIndex = 0;

                    // Set Jenis Pengeluaran ComboBox
                    string jenis = row.Cells["JenisPengeluaran"].Value?.ToString(); cmbJenisPengeluaran.SelectedItem = jenis;
                    if (cmbJenisPengeluaran.SelectedIndex < 0) { cmbJenisPengeluaran.SelectedIndex = 0; }

                    // Set Biaya TextBox
                    txtBiaya.Text = row.Cells["BiayaOperasional"].Value?.ToString();
                }
                catch (Exception ex) { HandleGeneralError("memilih data", ex); ClearInputs(); }
            }
            else { ClearInputs(); }
        }

        // Event handler ini ada di Designer.cs Anda, biarkan kosong jika tidak ada aksi khusus
        private void cmbJenisPengeluaran_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Anda bisa tambahkan logika di sini jika perlu melakukan sesuatu
            // saat pengguna mengubah pilihan jenis pengeluaran.
            // Contoh: Mungkin mengubah label biaya atau validasi lain.
        }
    }
}