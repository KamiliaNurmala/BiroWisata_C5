// Pembayaran.cs
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Globalization;

namespace BiroWisataForm
{
    public partial class Pembayaran : Form
    {
        private string connectionString = @"Data Source=KAMILIA\KAMILIANURMALA;Initial Catalog=BiroWisata;Integrated Security=True;";
        private int selectedPembayaranId = -1;

        public Pembayaran()
        {
            InitializeComponent();
            // Pengaturan awal dipindahkan ke Load
        }

        private void Pembayaran_Load(object sender, EventArgs e)
        {
            InitializeDataGridViewSettings();
            LoadPemesanan();
            LoadMetodePembayaran(); // <-- PANGGIL METHOD BARU

            // Set nilai default filter (sesuaikan sesuai kebutuhan)
            dtpPemesananFilterStart.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpPemesananFilterEnd.Value = DateTime.Now.Date;
            dtpPembayaranFilterStart.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpPembayaranFilterEnd.Value = DateTime.Now.Date;

            RefreshData(null, null, null, null); // Tampilkan semua data awal
            ClearInputs(); // Pastikan bersih saat mulai

            // Kaitkan event handler (jika belum otomatis)
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            this.btnTambah.Click += new System.EventHandler(this.btnTambah_Click);
            this.btnUbah.Click += new System.EventHandler(this.btnUbah_Click);
            this.btnHapus.Click += new System.EventHandler(this.btnHapus_Click);
        }

        private void InitializeDataGridViewSettings()
        {
            // ... (Kode InitializeDataGridViewSettings Anda sudah benar) ...
            dgvPembayaran.AutoGenerateColumns = false; // Penting!
            dgvPembayaran.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPembayaran.MultiSelect = false;
            dgvPembayaran.ReadOnly = true;
            dgvPembayaran.AllowUserToAddRows = false;
            dgvPembayaran.AllowUserToDeleteRows = false;
            dgvPembayaran.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Atau AllCells
            dgvPembayaran.CellClick += DgvPembayaran_CellClick;

            dgvPembayaran.Columns.Clear(); // Pastikan bersih

            dgvPembayaran.Columns.Add(new DataGridViewTextBoxColumn { Name = "IDPembayaran", HeaderText = "ID", DataPropertyName = "IDPembayaran", Visible = false });
            dgvPembayaran.Columns.Add(new DataGridViewTextBoxColumn { Name = "IDPemesanan", HeaderText = "ID Pesan", DataPropertyName = "IDPemesanan", Width = 90 });
            dgvPembayaran.Columns.Add(new DataGridViewTextBoxColumn { Name = "JumlahPembayaran", HeaderText = "Jumlah", DataPropertyName = "JumlahPembayaran", DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight }, Width = 120 });
            dgvPembayaran.Columns.Add(new DataGridViewTextBoxColumn { Name = "TanggalPembayaran", HeaderText = "Tgl Bayar", DataPropertyName = "TanggalPembayaran", DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" }, Width = 160 });
            dgvPembayaran.Columns.Add(new DataGridViewTextBoxColumn { Name = "MetodePembayaran", HeaderText = "Metode", DataPropertyName = "MetodePembayaran" });
            // Sembunyikan kolom TanggalPemesanan dari refresh data jika tidak ingin ditampilkan
            dgvPembayaran.Columns.Add(new DataGridViewTextBoxColumn { Name = "TanggalPemesanan", HeaderText = "Tgl Pesan (Hidden)", DataPropertyName = "TanggalPemesanan", Visible = false });
        }

        private void LoadPemesanan()
        {
            // ... (Kode LoadPemesanan Anda sudah benar) ...
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"SELECT p.IDPemesanan, CAST(p.IDPemesanan AS VARCHAR) + ' - ' + pl.NamaPelanggan + ' (' + pw.NamaPaket + ')' AS Deskripsi
                                     FROM Pemesanan p JOIN Pelanggan pl ON p.IDPelanggan = pl.IDPelanggan JOIN PaketWisata pw ON p.IDPaket = pw.IDPaket
                                     ORDER BY p.IDPemesanan";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    DataRow row = dt.NewRow();
                    row["IDPemesanan"] = DBNull.Value; row["Deskripsi"] = "-- Pilih Pemesanan --"; dt.Rows.InsertAt(row, 0);
                    cmbPemesanan.DataSource = dt; cmbPemesanan.DisplayMember = "Deskripsi"; cmbPemesanan.ValueMember = "IDPemesanan"; cmbPemesanan.DropDownStyle = ComboBoxStyle.DropDownList;
                }
                catch (Exception ex) { HandleLoadError("Pemesanan", ex); }
            }
        }

        // --- Method Baru untuk Load Metode Pembayaran ---
        private void LoadMetodePembayaran()
        {
            comboBoxMetode.Items.Clear();
            comboBoxMetode.Items.Add("-- Pilih Metode --"); // Placeholder
            comboBoxMetode.Items.Add("Transfer");          // Hanya opsi Transfer
            comboBoxMetode.SelectedIndex = 0;              // Pilih placeholder
            comboBoxMetode.DropDownStyle = ComboBoxStyle.DropDownList; // Tidak bisa ketik manual
        }
        // ----------------------------------------------

        private void RefreshData(DateTime? pemesananFilterStart, DateTime? pemesananFilterEnd,
                                 DateTime? pembayaranFilterStart, DateTime? pembayaranFilterEnd)
        {
            // ... (Kode RefreshData Anda sudah benar) ...
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"SELECT p.IDPembayaran, p.IDPemesanan, p.JumlahPembayaran, p.TanggalPembayaran, p.MetodePembayaran, pem.TanggalPemesanan
                                     FROM Pembayaran p INNER JOIN Pemesanan pem ON p.IDPemesanan = pem.IDPemesanan WHERE 1=1 ";
                    SqlCommand cmd = new SqlCommand(); cmd.Parameters.Clear();
                    if (pemesananFilterStart.HasValue) { query += " AND pem.TanggalPemesanan >= @PemesananStartDate "; cmd.Parameters.AddWithValue("@PemesananStartDate", pemesananFilterStart.Value.Date); }
                    if (pemesananFilterEnd.HasValue) { query += " AND pem.TanggalPemesanan <= @PemesananEndDate "; cmd.Parameters.AddWithValue("@PemesananEndDate", pemesananFilterEnd.Value.Date); }
                    if (pembayaranFilterStart.HasValue) { query += " AND p.TanggalPembayaran >= @PembayaranStartDate "; cmd.Parameters.AddWithValue("@PembayaranStartDate", pembayaranFilterStart.Value.Date); }
                    if (pembayaranFilterEnd.HasValue) { query += " AND p.TanggalPembayaran < @PembayaranEndDatePlusOne "; cmd.Parameters.AddWithValue("@PembayaranEndDatePlusOne", pembayaranFilterEnd.Value.Date.AddDays(1)); }
                    query += " ORDER BY p.IDPembayaran DESC";
                    cmd.CommandText = query; cmd.Connection = conn;
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd); DataTable dt = new DataTable(); adapter.Fill(dt);
                    dgvPembayaran.DataSource = null; dgvPembayaran.DataSource = dt;
                }
                catch (Exception ex) { HandleLoadError("Refresh Data", ex); }
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            // ... (Kode btnFilter_Click Anda sudah benar) ...
            DateTime pemesananStart = dtpPemesananFilterStart.Value; DateTime pemesananEnd = dtpPemesananFilterEnd.Value;
            DateTime pembayaranStart = dtpPembayaranFilterStart.Value; DateTime pembayaranEnd = dtpPembayaranFilterEnd.Value;
            if (pemesananStart.Date > pemesananEnd.Date) { MessageBox.Show("Tgl Awal Pesan > Tgl Akhir Pesan.", "Filter", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (pembayaranStart.Date > pembayaranEnd.Date) { MessageBox.Show("Tgl Awal Bayar > Tgl Akhir Bayar.", "Filter", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            RefreshData(pemesananStart, pemesananEnd, pembayaranStart, pembayaranEnd);
            MessageBox.Show("Filter diterapkan.", "Filter", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            // ... (Kode btnRefresh_Click Anda sudah benar) ...
            RefreshData(null, null, null, null);
            dtpPemesananFilterStart.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1); dtpPemesananFilterEnd.Value = DateTime.Now.Date;
            dtpPembayaranFilterStart.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1); dtpPembayaranFilterEnd.Value = DateTime.Now.Date;
            ClearInputs();
            MessageBox.Show("Data diperbarui, filter dihapus.", "Refresh", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void DgvPembayaran_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // --- PERUBAHAN: Set ComboBox Metode ---
            if (e.RowIndex >= 0 && e.RowIndex < dgvPembayaran.Rows.Count)
            {
                DataGridViewRow row = dgvPembayaran.Rows[e.RowIndex];
                try
                {
                    if (row.Cells["IDPembayaran"].Value != null && int.TryParse(row.Cells["IDPembayaran"].Value.ToString(), out int id))
                    { selectedPembayaranId = id; }
                    else { selectedPembayaranId = -1; ClearInputs(); return; }

                    object idPemesananValue = row.Cells["IDPemesanan"].Value;
                    if (idPemesananValue != null && idPemesananValue != DBNull.Value) { cmbPemesanan.SelectedValue = idPemesananValue; if (cmbPemesanan.SelectedValue == null || cmbPemesanan.SelectedValue == DBNull.Value) cmbPemesanan.SelectedIndex = 0; }
                    else { cmbPemesanan.SelectedIndex = 0; }

                    txtJumlah.Text = row.Cells["JumlahPembayaran"].Value?.ToString() ?? "";

                    object tanggalValue = row.Cells["TanggalPembayaran"].Value;
                    if (tanggalValue != null && tanggalValue != DBNull.Value && DateTime.TryParse(tanggalValue.ToString(), out DateTime tanggal))
                    { dateTimePicker1.Value = tanggal; }
                    else { dateTimePicker1.Value = DateTime.Now; }

                    // Set ComboBox Metode
                    string metodeValue = row.Cells["MetodePembayaran"].Value?.ToString();
                    comboBoxMetode.SelectedItem = metodeValue; // Cari item "Transfer"
                    if (comboBoxMetode.SelectedIndex < 0) // Jika tidak ketemu (seharusnya tidak terjadi)
                    {
                        comboBoxMetode.SelectedIndex = 0; // Reset ke placeholder
                    }
                    // Hapus baris ini: txtMetode.Text = ...
                }
                catch (Exception ex) { MessageBox.Show($"Error Selection: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); ClearInputs(); }
            }
            else { ClearInputs(); }
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            // --- PERUBAHAN: Ambil dari ComboBox Metode ---
            if (!ValidateInputs(out decimal jumlahPembayaran)) return; // Metode tidak perlu out lagi

            object idPemesanan = cmbPemesanan.SelectedValue;
            DateTime tanggalPembayaran = dateTimePicker1.Value;
            string metodePembayaran = comboBoxMetode.SelectedItem.ToString(); // <-- Ambil dari ComboBox

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"INSERT INTO Pembayaran (IDPemesanan, JumlahPembayaran, TanggalPembayaran, MetodePembayaran)
                                     VALUES (@IDPemesanan, @JumlahPembayaran, @TanggalPembayaran, @MetodePembayaran)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IDPemesanan", idPemesanan);
                        cmd.Parameters.AddWithValue("@JumlahPembayaran", jumlahPembayaran);
                        cmd.Parameters.AddWithValue("@TanggalPembayaran", tanggalPembayaran);
                        cmd.Parameters.AddWithValue("@MetodePembayaran", metodePembayaran); // <-- Gunakan nilai dari ComboBox

                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Pembayaran berhasil ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            RefreshData(null, null, null, null); // Refresh semua
                        }
                        else { /* Handle no rows */ }
                    }
                }
                catch (SqlException ex) { HandleSqlError(ex, "menambah"); }
                catch (Exception ex) { MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void btnUbah_Click(object sender, EventArgs e)
        {
            // --- PERUBAHAN: Ambil dari ComboBox Metode ---
            if (selectedPembayaranId < 0) { MessageBox.Show("Pilih data pembayaran!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (!ValidateInputs(out decimal jumlahPembayaran)) return; // Metode tidak perlu out lagi

            object idPemesanan = cmbPemesanan.SelectedValue;
            DateTime tanggalPembayaran = dateTimePicker1.Value;
            string metodePembayaran = comboBoxMetode.SelectedItem.ToString(); // <-- Ambil dari ComboBox

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"UPDATE Pembayaran SET
                                         IDPemesanan = @IDPemesanan, JumlahPembayaran = @JumlahPembayaran,
                                         TanggalPembayaran = @TanggalPembayaran, MetodePembayaran = @MetodePembayaran
                                     WHERE IDPembayaran = @IDPembayaran";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IDPemesanan", idPemesanan);
                        cmd.Parameters.AddWithValue("@JumlahPembayaran", jumlahPembayaran);
                        cmd.Parameters.AddWithValue("@TanggalPembayaran", tanggalPembayaran);
                        cmd.Parameters.AddWithValue("@MetodePembayaran", metodePembayaran); // <-- Gunakan nilai dari ComboBox
                        cmd.Parameters.AddWithValue("@IDPembayaran", selectedPembayaranId);

                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Pembayaran berhasil diubah!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            RefreshData(null, null, null, null); // Refresh semua
                        }
                        else { MessageBox.Show("Data tidak berubah/ditemukan.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    }
                }
                catch (SqlException ex) { HandleSqlError(ex, "mengubah"); }
                catch (Exception ex) { MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            // ... (Kode btnHapus_Click Anda sudah benar) ...
            if (selectedPembayaranId < 0) { MessageBox.Show("Pilih data pembayaran!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (MessageBox.Show($"Yakin hapus ID {selectedPembayaranId}?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        string query = "DELETE FROM Pembayaran WHERE IDPembayaran = @IDPembayaran";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@IDPembayaran", selectedPembayaranId);
                            if (cmd.ExecuteNonQuery() > 0)
                            {
                                MessageBox.Show("Pembayaran berhasil dihapus!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                RefreshData(null, null, null, null);
                            }
                            else { /* Handle not found */ }
                        }
                    }
                    catch (SqlException ex) { HandleSqlError(ex, "menghapus"); }
                    catch (Exception ex) { MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
        }

        // --- Metode Helper ---
        // --- PERUBAHAN: Hapus out parameter metode dari ValidateInputs ---
        private bool ValidateInputs(out decimal jumlah)
        {
            jumlah = 0; // Inisialisasi out parameter jumlah
            bool isValid = true;
            string errorMessage = "";

            if (cmbPemesanan.SelectedValue == null || cmbPemesanan.SelectedValue == DBNull.Value)
            { errorMessage += "- ID Pemesanan harus dipilih.\n"; isValid = false; if (isValid) cmbPemesanan.Focus(); }

            // Validasi ComboBox Metode Pembayaran
            if (comboBoxMetode.SelectedIndex <= 0) // Index 0 adalah placeholder
            { errorMessage += "- Metode Pembayaran harus dipilih.\n"; isValid = false; if (isValid) comboBoxMetode.Focus(); }

            // Validasi Jumlah
            if (string.IsNullOrWhiteSpace(txtJumlah.Text))
            { errorMessage += "- Jumlah pembayaran kosong.\n"; isValid = false; if (isValid) txtJumlah.Focus(); }
            else if (!decimal.TryParse(txtJumlah.Text.Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out jumlah) || jumlah <= 0)
            { errorMessage += "- Jumlah pembayaran harus angka positif.\n"; isValid = false; if (isValid) txtJumlah.Focus(); }

            // Hapus validasi txtMetode
            // metode = txtMetode.Text.Trim();
            // if (string.IsNullOrWhiteSpace(metode))...
            // else if (!metode.Equals("Transfer"...)...

            if (!isValid) { MessageBox.Show("Input tidak valid:\n" + errorMessage, "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            return isValid;
        }

        private void ClearInputs()
        {
            // --- PERUBAHAN: Reset ComboBox Metode ---
            cmbPemesanan.SelectedIndex = 0;
            txtJumlah.Clear();
            dateTimePicker1.Value = DateTime.Now;
            comboBoxMetode.SelectedIndex = 0; // <-- Reset ComboBox Metode
            // Hapus txtMetode.Clear();
            selectedPembayaranId = -1;
            dgvPembayaran.ClearSelection();
            cmbPemesanan.Focus();
        }

        private void HandleSqlError(SqlException ex, string operation)
        {
            // ... (Kode HandleSqlError Anda sudah benar) ...
            MessageBox.Show($"Error DB saat {operation}: {ex.Message}\nNo: {ex.Number}", "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if (ex.Number == 547)
            { // FK
                MessageBox.Show("Pastikan ID Pemesanan yang dipilih valid.", "Kesalahan Referensi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Helper untuk error saat Load ComboBox (contoh)
        private void HandleLoadError(string controlName, Exception ex)
        {
            MessageBox.Show($"Error saat memuat data {controlName}: {ex.Message}", "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

    }
}