// Pemesanan.cs
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BiroWisataForm
{
    public partial class Pemesanan : Form
    {
        // --- Variabel Member ---
        private string connectionString = @"Data Source=KAMILIA\KAMILIANURMALA;Initial Catalog=BiroWisata;Integrated Security=True;";
        private int selectedPemesananId = -1;

        // --- Constructor ---
        public Pemesanan()
        {
            InitializeComponent();
            InitializeDataGridView();
            LoadPelanggan();
            LoadPaketWisata();
            LoadStatusPembayaran(); // <-- PANGGIL METHOD BARU
            RefreshData();

            this.btnTambah.Click += new System.EventHandler(this.btnTambah_Click);
            this.btnUbah.Click += new System.EventHandler(this.btnUbah_Click);
            this.btnHapus.Click += new System.EventHandler(this.btnHapus_Click);
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
        }

        // --- Inisialisasi Kontrol ---
        private void InitializeDataGridView()
        {
            // ... (Kode InitializeDataGridView Anda sudah benar) ...
            dgvPemesanan.AutoGenerateColumns = false;
            dgvPemesanan.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPemesanan.MultiSelect = false;
            dgvPemesanan.ReadOnly = true;
            dgvPemesanan.AllowUserToAddRows = false;
            dgvPemesanan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvPemesanan.CellClick += DgvPemesanan_CellClick;

            dgvPemesanan.Columns.Add(new DataGridViewTextBoxColumn { Name = "IDPemesanan", HeaderText = "ID", DataPropertyName = "IDPemesanan", Visible = false });
            dgvPemesanan.Columns.Add(new DataGridViewTextBoxColumn { Name = "NamaPelanggan", HeaderText = "Pelanggan", DataPropertyName = "NamaPelanggan" });
            dgvPemesanan.Columns.Add(new DataGridViewTextBoxColumn { Name = "NamaPaket", HeaderText = "Paket Wisata", DataPropertyName = "NamaPaket" });
            dgvPemesanan.Columns.Add(new DataGridViewTextBoxColumn { Name = "TanggalPemesanan", HeaderText = "Tgl Pesan", DataPropertyName = "TanggalPemesanan", DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" } });
            dgvPemesanan.Columns.Add(new DataGridViewTextBoxColumn { Name = "StatusPembayaran", HeaderText = "Status Bayar", DataPropertyName = "StatusPembayaran" });
            dgvPemesanan.Columns.Add(new DataGridViewTextBoxColumn { Name = "TotalPembayaran", HeaderText = "Total Bayar", DataPropertyName = "TotalPembayaran", DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" } });
            dgvPemesanan.Columns.Add(new DataGridViewTextBoxColumn { Name = "IDPelanggan", DataPropertyName = "IDPelanggan", Visible = false });
            dgvPemesanan.Columns.Add(new DataGridViewTextBoxColumn { Name = "IDPaket", DataPropertyName = "IDPaket", Visible = false });
        }

        // --- Memuat Data ke Kontrol (ComboBoxes) ---

        private void LoadComboBoxData(ComboBox comboBox, string query, string displayMember, string valueMember, string placeholder)
        {
            // ... (Kode LoadComboBoxData Anda sudah benar) ...
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
            LoadComboBoxData(comboBoxPelanggan, "SELECT IDPelanggan, NamaPelanggan FROM Pelanggan ORDER BY NamaPelanggan", "NamaPelanggan", "IDPelanggan", "-- Pilih Pelanggan --");
        }

        private void LoadPaketWisata()
        {
            LoadComboBoxData(comboBoxPaketWisata, "SELECT IDPaket, NamaPaket FROM PaketWisata ORDER BY NamaPaket", "NamaPaket", "IDPaket", "-- Pilih Paket Wisata --");
        }

        // --- Method Baru untuk Load Status Pembayaran ---
        private void LoadStatusPembayaran()
        {
            comboBoxPemesanan.Items.Clear();
            comboBoxPemesanan.Items.Add("-- Pilih Status --"); // Placeholder
            comboBoxPemesanan.Items.Add("DP");
            comboBoxPemesanan.Items.Add("Lunas");
            comboBoxPemesanan.SelectedIndex = 0; // Pilih placeholder
            comboBoxPemesanan.DropDownStyle = ComboBoxStyle.DropDownList; // Agar tidak bisa diketik manual
        }
        // -------------------------------------------

        // --- Memuat Data ke Grid ---
        private void RefreshData()
        {
            // ... (Kode RefreshData Anda sudah benar) ...
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
                        pw.NamaPaket
                    FROM Pemesanan p
                    INNER JOIN Pelanggan pl ON p.IDPelanggan = pl.IDPelanggan
                    INNER JOIN PaketWisata pw ON p.IDPaket = pw.IDPaket
                    ORDER BY p.TanggalPemesanan DESC, pl.NamaPelanggan";

                    var dt = new DataTable();
                    var adapter = new SqlDataAdapter(query, conn);
                    adapter.Fill(dt);

                    dgvPemesanan.DataSource = null;
                    dgvPemesanan.DataSource = dt;
                }
                catch (SqlException ex) { MessageBox.Show($"Error DB Refresh Pemesanan: {ex.Message}", "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                catch (Exception ex) { MessageBox.Show($"Error Refresh Pemesanan: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            ClearInputs();
        }

        // --- Event Handler Tombol CRUD ---
        private void btnTambah_Click(object sender, EventArgs e)
        {
            // --- PERUBAHAN: Ambil status dari ComboBox ---
            if (!ValidateInputs()) return;

            if (!decimal.TryParse(txtTotal.Text.Trim(), out decimal total) || total <= 0)
            { MessageBox.Show("Total Pembayaran harus berupa angka positif!", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtTotal.Focus(); return; }

            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string sql = @"INSERT INTO Pemesanan
                                   (IDPelanggan, IDPaket, TanggalPemesanan, StatusPembayaran, TotalPembayaran)
                                   VALUES
                                   (@IDPelanggan, @IDPaket, @Tanggal, @Status, @Total)";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@IDPelanggan", comboBoxPelanggan.SelectedValue);
                        cmd.Parameters.AddWithValue("@IDPaket", comboBoxPaketWisata.SelectedValue);
                        cmd.Parameters.AddWithValue("@Tanggal", dateTimePicker1.Value.Date);
                        cmd.Parameters.AddWithValue("@Status", comboBoxPemesanan.SelectedItem.ToString()); // <-- Ambil dari ComboBox
                        cmd.Parameters.AddWithValue("@Total", total);

                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Pemesanan berhasil ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            RefreshData();
                        }
                        else { /* Handle no rows */ }
                    }
                }
                catch (SqlException ex) { HandleSqlError(ex, "menambahkan"); }
                catch (Exception ex) { MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void btnUbah_Click(object sender, EventArgs e)
        {
            // --- PERUBAHAN: Ambil status dari ComboBox ---
            if (selectedPemesananId < 0)
            { MessageBox.Show("Pilih pemesanan yang akan diubah!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            if (!ValidateInputs()) return;

            if (!decimal.TryParse(txtTotal.Text.Trim(), out decimal total) || total <= 0)
            { MessageBox.Show("Total Pembayaran harus berupa angka positif!", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtTotal.Focus(); return; }

            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string sql = @"UPDATE Pemesanan SET
                                       IDPelanggan = @IDPelanggan, IDPaket = @IDPaket,
                                       TanggalPemesanan = @Tanggal, StatusPembayaran = @Status,
                                       TotalPembayaran = @Total
                                   WHERE IDPemesanan = @ID";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@IDPelanggan", comboBoxPelanggan.SelectedValue);
                        cmd.Parameters.AddWithValue("@IDPaket", comboBoxPaketWisata.SelectedValue);
                        cmd.Parameters.AddWithValue("@Tanggal", dateTimePicker1.Value.Date);
                        cmd.Parameters.AddWithValue("@Status", comboBoxPemesanan.SelectedItem.ToString()); // <-- Ambil dari ComboBox
                        cmd.Parameters.AddWithValue("@Total", total);
                        cmd.Parameters.AddWithValue("@ID", selectedPemesananId);

                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Pemesanan berhasil diubah!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            RefreshData();
                        }
                        else { MessageBox.Show("Data tidak berubah atau tidak ditemukan.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    }
                }
                catch (SqlException ex) { HandleSqlError(ex, "mengubah"); }
                catch (Exception ex) { MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        // ... (btnHapus_Click Anda sudah benar) ...
        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (selectedPemesananId < 0)
            { MessageBox.Show("Pilih pemesanan yang akan dihapus!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            if (MessageBox.Show("Yakin ingin menghapus pemesanan ini?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        string sql = "DELETE FROM Pemesanan WHERE IDPemesanan = @ID";
                        using (var cmd = new SqlCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("@ID", selectedPemesananId);

                            if (cmd.ExecuteNonQuery() > 0)
                            {
                                MessageBox.Show("Pemesanan berhasil dihapus!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                RefreshData();
                            }
                            else { MessageBox.Show("Gagal menghapus (mungkin sudah dihapus).", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                        }
                    }
                    catch (SqlException ex) { HandleSqlError(ex, "menghapus"); }
                    catch (Exception ex) { MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshData();
            MessageBox.Show("Data telah dimuat ulang.", "Refresh Selesai", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        // --- Helper Methods ---
        private bool ValidateInputs()
        {
            // --- PERUBAHAN: Validasi ComboBox Status ---
            if (comboBoxPelanggan.SelectedValue == null || comboBoxPelanggan.SelectedValue == DBNull.Value)
            { MessageBox.Show("Pilih Pelanggan!", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning); comboBoxPelanggan.Focus(); return false; }
            if (comboBoxPaketWisata.SelectedValue == null || comboBoxPaketWisata.SelectedValue == DBNull.Value)
            { MessageBox.Show("Pilih Paket Wisata!", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning); comboBoxPaketWisata.Focus(); return false; }

            // Validasi Status Pembayaran ComboBox
            if (comboBoxPemesanan.SelectedIndex <= 0) // Index 0 adalah "-- Pilih Status --"
            { MessageBox.Show("Pilih Status Pembayaran yang valid!", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning); comboBoxPemesanan.Focus(); return false; }
            // Hapus validasi TextBox status yang lama
            // string status = txtStatus.Text.Trim();
            // if (string.IsNullOrWhiteSpace(status)) ... (HAPUS)
            // if (status != "DP" && status != "Lunas") ... (HAPUS)

            // Validasi Total Pembayaran
            if (string.IsNullOrWhiteSpace(txtTotal.Text))
            { MessageBox.Show("Total Pembayaran tidak boleh kosong!", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtTotal.Focus(); return false; }
            if (!decimal.TryParse(txtTotal.Text, out _))
            { MessageBox.Show("Format Total Pembayaran tidak valid!", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtTotal.Focus(); return false; }

            return true; // Jika semua valid
        }

        private void ClearInputs()
        {
            // --- PERUBAHAN: Reset ComboBox Status ---
            selectedPemesananId = -1;
            comboBoxPelanggan.SelectedIndex = 0;
            comboBoxPaketWisata.SelectedIndex = 0;
            comboBoxPemesanan.SelectedIndex = 0; // <-- Reset ComboBox Status
            dateTimePicker1.Value = DateTime.Now;
            // txtStatus.Clear(); // <-- Hapus ini
            txtTotal.Clear();
            dgvPemesanan.ClearSelection();
            comboBoxPelanggan.Focus();
        }

        // ... (HandleSqlError Anda sudah benar) ...
        private void HandleSqlError(SqlException ex, string operation)
        {
            MessageBox.Show($"Error Database saat {operation} data: {ex.Message}\nNomor Error: {ex.Number}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if (ex.Number == 547) // FK violation
            { MessageBox.Show("Pastikan Pelanggan dan Paket Wisata yang dipilih ada.", "Kesalahan Referensi Data", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }


        // --- Event Handler Grid ---
        private void DgvPemesanan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // --- PERUBAHAN: Set ComboBox Status ---
            if (e.RowIndex >= 0 && e.RowIndex < dgvPemesanan.Rows.Count)
            {
                DataGridViewRow row = dgvPemesanan.Rows[e.RowIndex];

                if (row.Cells["IDPemesanan"].Value != null && int.TryParse(row.Cells["IDPemesanan"].Value.ToString(), out int id))
                { selectedPemesananId = id; }
                else { selectedPemesananId = -1; ClearInputs(); return; }

                comboBoxPelanggan.SelectedValue = row.Cells["IDPelanggan"].Value ?? DBNull.Value;
                comboBoxPaketWisata.SelectedValue = row.Cells["IDPaket"].Value ?? DBNull.Value;

                object tglValue = row.Cells["TanggalPemesanan"].Value;
                if (tglValue != null && tglValue != DBNull.Value && DateTime.TryParse(tglValue.ToString(), out DateTime tgl))
                { dateTimePicker1.Value = tgl; }
                else { dateTimePicker1.Value = DateTime.Now; }

                // Isi ComboBox Status Pembayaran
                string statusValue = row.Cells["StatusPembayaran"].Value?.ToString();
                comboBoxPemesanan.SelectedItem = statusValue; // Cari item yang cocok
                if (comboBoxPemesanan.SelectedIndex < 0) // Jika tidak cocok (data lama?)
                {
                    comboBoxPemesanan.SelectedIndex = 0; // Set ke placeholder
                }
                // Hapus pengisian TextBox status
                // txtStatus.Text = row.Cells["StatusPembayaran"].Value?.ToString();

                // Isi TextBox Total
                txtTotal.Text = row.Cells["TotalPembayaran"].Value?.ToString();
            }
            else { ClearInputs(); }
        }

        // Event handler Load Form
        private void Pemesanan_Load(object sender, EventArgs e)
        {
            // Kosongkan jika semua inisialisasi di constructor
        }
    }
}