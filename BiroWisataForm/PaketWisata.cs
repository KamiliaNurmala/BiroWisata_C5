// PaketWisata.cs
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BiroWisataForm
{
    public partial class PaketWisata : Form
    {
        // --- Variabel Member ---
        private string connectionString = @"Data Source=KAMILIA\KAMILIANURMALA;Initial Catalog=BiroWisata;Integrated Security=True;";
        private int selectedPaketId = -1; // Menyimpan ID paket yang dipilih di grid

        // --- Constructor ---
        public PaketWisata()
        {
            InitializeComponent();
            InitializeDataGridView(); // Siapkan grid
            LoadDrivers();          // Isi ComboBox Driver
            LoadKendaraan();        // Isi ComboBox Kendaraan
            LoadKategori();         // Panggil method untuk isi ComboBox Kategori
            RefreshData();          // Tampilkan data awal di grid
        }

        // --- Inisialisasi Kontrol ---
        private void InitializeDataGridView()
        {
            // ... (kode InitializeDataGridView Anda yang sudah ada, tidak perlu diubah) ...
            dgvPaketWisata.AutoGenerateColumns = false;
            dgvPaketWisata.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPaketWisata.MultiSelect = false;
            dgvPaketWisata.ReadOnly = true;
            dgvPaketWisata.AllowUserToAddRows = false;
            dgvPaketWisata.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvPaketWisata.CellClick += DgvPaketWisata_CellClick;

            dgvPaketWisata.Columns.Add(new DataGridViewTextBoxColumn { Name = "IDPaket", HeaderText = "ID", DataPropertyName = "IDPaket", Visible = false });
            dgvPaketWisata.Columns.Add(new DataGridViewTextBoxColumn { Name = "NamaPaket", HeaderText = "Nama Paket", DataPropertyName = "NamaPaket" });
            dgvPaketWisata.Columns.Add(new DataGridViewTextBoxColumn { Name = "NamaDriver", HeaderText = "Driver", DataPropertyName = "NamaDriver" });
            dgvPaketWisata.Columns.Add(new DataGridViewTextBoxColumn { Name = "KendaraanInfo", HeaderText = "Kendaraan", DataPropertyName = "KendaraanInfo" });
            dgvPaketWisata.Columns.Add(new DataGridViewTextBoxColumn { Name = "Destinasi", HeaderText = "Destinasi", DataPropertyName = "Destinasi" });
            dgvPaketWisata.Columns.Add(new DataGridViewTextBoxColumn { Name = "Harga", HeaderText = "Harga", DataPropertyName = "Harga", DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" } });
            dgvPaketWisata.Columns.Add(new DataGridViewTextBoxColumn { Name = "Durasi", HeaderText = "Durasi (Hari)", DataPropertyName = "Durasi" });
            dgvPaketWisata.Columns.Add(new DataGridViewTextBoxColumn { Name = "Fasilitas", HeaderText = "Fasilitas", DataPropertyName = "Fasilitas" });
            dgvPaketWisata.Columns.Add(new DataGridViewTextBoxColumn { Name = "Kategori", HeaderText = "Kategori", DataPropertyName = "Kategori" });
            dgvPaketWisata.Columns.Add(new DataGridViewTextBoxColumn { Name = "Kuota", HeaderText = "Kuota", DataPropertyName = "Kuota" });
            dgvPaketWisata.Columns.Add(new DataGridViewTextBoxColumn { Name = "Jadwal", HeaderText = "Jadwal", DataPropertyName = "JadwalKeberangkatan", DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" } });
            dgvPaketWisata.Columns.Add(new DataGridViewTextBoxColumn { Name = "IDDriver", DataPropertyName = "IDDriver", Visible = false });
            dgvPaketWisata.Columns.Add(new DataGridViewTextBoxColumn { Name = "IDKendaraan", DataPropertyName = "IDKendaraan", Visible = false });
        }

        // --- Memuat Data ke Kontrol ---

        // --- Method untuk Load Kategori (BARU) ---
        private void LoadKategori()
        {
            comboBoxKategori.Items.Clear(); // Pastikan kosong dulu
            comboBoxKategori.Items.Add("-- Pilih Kategori --"); // Placeholder
            comboBoxKategori.Items.Add("Luar Kota");
            comboBoxKategori.Items.Add("Dalam Kota");
            comboBoxKategori.SelectedIndex = 0; // Pilih placeholder sebagai default
                                                // Atur DropDownStyle ke DropDownList di Properties window agar user tidak bisa ketik manual
            comboBoxKategori.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        // ... (LoadDrivers, LoadKendaraan, RefreshData Anda yang sudah ada) ...
        private void LoadDrivers()
        {
            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    var dt = new DataTable();
                    var adapter = new SqlDataAdapter("SELECT IDDriver, NamaDriver FROM Driver WHERE Status = 'Aktif'", conn);
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
                    string sql = "SELECT IDKendaraan, Jenis + ' - ' + PlatNomor AS Deskripsi FROM Kendaraan";
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
        private void RefreshData()
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
                        p.IDKendaraan, k.Jenis + ' - ' + k.PlatNomor AS KendaraanInfo
                    FROM PaketWisata p
                    INNER JOIN Driver d ON p.IDDriver = d.IDDriver
                    INNER JOIN Kendaraan k ON p.IDKendaraan = k.IDKendaraan";

                    var dt = new DataTable();
                    var adapter = new SqlDataAdapter(query, conn);
                    adapter.Fill(dt);

                    dgvPaketWisata.DataSource = null;
                    dgvPaketWisata.DataSource = dt;
                }
                catch (SqlException ex) { MessageBox.Show($"Error DB Refresh Paket: {ex.Message}", "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                catch (Exception ex) { MessageBox.Show($"Error Refresh Paket: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            ClearInputs(); // Panggil ClearInputs setelah refresh
        }


        // --- Event Handler Tombol CRUD ---
        private void btnTambah_Click(object sender, EventArgs e)
        {
            // --- PERUBAHAN di btnTambah_Click ---
            if (!ValidateInputs(isUpdate: false)) return; // Validasi (sudah termasuk ComboBox Kategori)

            if (!decimal.TryParse(txtHarga.Text.Trim(), out decimal harga) || harga <= 0)
            { MessageBox.Show("Harga harus berupa angka positif!", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtHarga.Focus(); return; }
            if (!int.TryParse(txtDurasi.Text.Trim(), out int durasi) || durasi <= 0)
            { MessageBox.Show("Durasi harus berupa angka positif!", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtDurasi.Focus(); return; }
            if (!int.TryParse(txtKuota.Text.Trim(), out int kuota) || kuota < 0)
            { MessageBox.Show("Kuota harus berupa angka non-negatif!", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtKuota.Focus(); return; }

            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string sql = @"INSERT INTO PaketWisata
                                   (IDDriver, IDKendaraan, NamaPaket, Destinasi, Harga, Durasi,
                                    Fasilitas, Kategori, Kuota, JadwalKeberangkatan)
                                   VALUES
                                   (@IDDriver, @IDKendaraan, @NamaPaket, @Destinasi, @Harga, @Durasi,
                                    @Fasilitas, @Kategori, @Kuota, @Jadwal)";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@IDDriver", cmbDriver.SelectedValue);
                        cmd.Parameters.AddWithValue("@IDKendaraan", cmbKendaraan.SelectedValue);
                        cmd.Parameters.AddWithValue("@NamaPaket", txtNamaPaket.Text.Trim());
                        cmd.Parameters.AddWithValue("@Destinasi", txtDestinasi.Text.Trim());
                        cmd.Parameters.AddWithValue("@Harga", harga);
                        cmd.Parameters.AddWithValue("@Durasi", durasi);
                        cmd.Parameters.AddWithValue("@Fasilitas", txtFasilitas.Text.Trim());
                        // Ambil nilai dari ComboBox Kategori
                        cmd.Parameters.AddWithValue("@Kategori", comboBoxKategori.SelectedItem.ToString()); // <-- PERUBAHAN
                        cmd.Parameters.AddWithValue("@Kuota", kuota);
                        cmd.Parameters.AddWithValue("@Jadwal", dateTimePicker1.Value.Date);

                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Paket wisata berhasil ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            RefreshData();
                        }
                        else { /* Handle no rows affected */ }
                    }
                }
                catch (SqlException ex) { HandleSqlError(ex, "menambahkan"); }
                catch (Exception ex) { MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void btnUbah_Click(object sender, EventArgs e)
        {
            // --- PERUBAHAN di btnUbah_Click ---
            if (selectedPaketId < 0)
            { MessageBox.Show("Pilih paket wisata yang akan diubah!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            if (!ValidateInputs(isUpdate: true)) return; // Validasi

            if (!decimal.TryParse(txtHarga.Text.Trim(), out decimal harga) || harga <= 0)
            { MessageBox.Show("Harga harus berupa angka positif!", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtHarga.Focus(); return; }
            if (!int.TryParse(txtDurasi.Text.Trim(), out int durasi) || durasi <= 0)
            { MessageBox.Show("Durasi harus berupa angka positif!", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtDurasi.Focus(); return; }
            if (!int.TryParse(txtKuota.Text.Trim(), out int kuota) || kuota < 0)
            { MessageBox.Show("Kuota harus berupa angka non-negatif!", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtKuota.Focus(); return; }

            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string sql = @"UPDATE PaketWisata SET
                                       IDDriver = @IDDriver, IDKendaraan = @IDKendaraan, NamaPaket = @NamaPaket,
                                       Destinasi = @Destinasi, Harga = @Harga, Durasi = @Durasi,
                                       Fasilitas = @Fasilitas, Kategori = @Kategori, Kuota = @Kuota,
                                       JadwalKeberangkatan = @Jadwal
                                   WHERE IDPaket = @ID";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@IDDriver", cmbDriver.SelectedValue);
                        cmd.Parameters.AddWithValue("@IDKendaraan", cmbKendaraan.SelectedValue);
                        cmd.Parameters.AddWithValue("@NamaPaket", txtNamaPaket.Text.Trim());
                        cmd.Parameters.AddWithValue("@Destinasi", txtDestinasi.Text.Trim());
                        cmd.Parameters.AddWithValue("@Harga", harga);
                        cmd.Parameters.AddWithValue("@Durasi", durasi);
                        cmd.Parameters.AddWithValue("@Fasilitas", txtFasilitas.Text.Trim());
                        // Ambil nilai dari ComboBox Kategori
                        cmd.Parameters.AddWithValue("@Kategori", comboBoxKategori.SelectedItem.ToString()); // <-- PERUBAHAN
                        cmd.Parameters.AddWithValue("@Kuota", kuota);
                        cmd.Parameters.AddWithValue("@Jadwal", dateTimePicker1.Value.Date);
                        cmd.Parameters.AddWithValue("@ID", selectedPaketId);

                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Paket wisata berhasil diubah!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            RefreshData();
                        }
                        else { MessageBox.Show("Data tidak berubah atau tidak ditemukan.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    }
                }
                catch (SqlException ex) { HandleSqlError(ex, "mengubah"); }
                catch (Exception ex) { MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        // ... (btnHapus_Click Anda yang sudah ada, tidak perlu diubah terkait Kategori) ...
        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (selectedPaketId < 0)
            { MessageBox.Show("Pilih paket wisata yang akan dihapus!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            if (MessageBox.Show("Yakin ingin menghapus paket wisata ini? Tindakan ini mungkin mempengaruhi data Pemesanan terkait.", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        string sql = "DELETE FROM PaketWisata WHERE IDPaket = @ID";
                        using (var cmd = new SqlCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("@ID", selectedPaketId);

                            if (cmd.ExecuteNonQuery() > 0)
                            {
                                MessageBox.Show("Paket wisata berhasil dihapus!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        private bool ValidateInputs(bool isUpdate) // Parameter isUpdate mungkin tidak relevan lagi di sini
        {
            // --- PERUBAHAN di ValidateInputs ---

            // Validasi ComboBox FK
            if (cmbDriver.SelectedValue == null || cmbDriver.SelectedValue == DBNull.Value)
            { MessageBox.Show("Pilih Driver yang valid!", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning); cmbDriver.Focus(); return false; }
            if (cmbKendaraan.SelectedValue == null || cmbKendaraan.SelectedValue == DBNull.Value)
            { MessageBox.Show("Pilih Kendaraan yang valid!", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning); cmbKendaraan.Focus(); return false; }

            // Validasi ComboBox Kategori
            if (comboBoxKategori.SelectedIndex <= 0) // Index 0 adalah placeholder "-- Pilih Kategori --"
            { MessageBox.Show("Pilih Kategori yang valid!", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning); comboBoxKategori.Focus(); return false; }

            // Validasi TextBox tidak boleh kosong
            if (string.IsNullOrWhiteSpace(txtNamaPaket.Text))
            { MessageBox.Show("Nama Paket tidak boleh kosong!", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtNamaPaket.Focus(); return false; }
            if (string.IsNullOrWhiteSpace(txtDestinasi.Text))
            { MessageBox.Show("Destinasi tidak boleh kosong!", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtDestinasi.Focus(); return false; }
            if (string.IsNullOrWhiteSpace(txtHarga.Text))
            { MessageBox.Show("Harga tidak boleh kosong!", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtHarga.Focus(); return false; }
            if (string.IsNullOrWhiteSpace(txtDurasi.Text))
            { MessageBox.Show("Durasi tidak boleh kosong!", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtDurasi.Focus(); return false; }
            if (string.IsNullOrWhiteSpace(txtFasilitas.Text))
            { MessageBox.Show("Fasilitas tidak boleh kosong!", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtFasilitas.Focus(); return false; }
            // Hapus validasi TextBox Kategori yang lama
            // if (string.IsNullOrWhiteSpace(txtKategori.Text)) ... (HAPUS)
            // if (txtKategori.Text.Trim() != "Luar Kota" && txtKategori.Text.Trim() != "Dalam Kota") ... (HAPUS)
            if (string.IsNullOrWhiteSpace(txtKuota.Text))
            { MessageBox.Show("Kuota tidak boleh kosong!", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtKuota.Focus(); return false; }

            // Validasi format angka
            if (!decimal.TryParse(txtHarga.Text, out _))
            { MessageBox.Show("Format Harga tidak valid!", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtHarga.Focus(); return false; }
            if (!int.TryParse(txtDurasi.Text, out _))
            { MessageBox.Show("Format Durasi tidak valid!", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtDurasi.Focus(); return false; }
            if (!int.TryParse(txtKuota.Text, out _))
            { MessageBox.Show("Format Kuota tidak valid!", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtKuota.Focus(); return false; }

            return true; // Jika semua valid
        }

        private void ClearInputs()
        {
            // --- PERUBAHAN di ClearInputs ---
            selectedPaketId = -1;
            cmbDriver.SelectedIndex = 0;
            cmbKendaraan.SelectedIndex = 0;
            comboBoxKategori.SelectedIndex = 0; // Reset ComboBox Kategori
            txtNamaPaket.Clear();
            txtDestinasi.Clear();
            txtHarga.Clear();
            txtDurasi.Clear();
            txtFasilitas.Clear();
            // Hapus txtKategori.Clear(); jika ada
            txtKuota.Clear();
            dateTimePicker1.Value = DateTime.Now;
            dgvPaketWisata.ClearSelection();
            txtNamaPaket.Focus();
        }

        // --- Tambahkan Helper untuk Error SQL (jika belum ada) ---
        private void HandleSqlError(SqlException ex, string operation)
        {
            MessageBox.Show($"Error Database saat {operation} paket: {ex.Message}\nNomor Error: {ex.Number}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if (ex.Number == 547) // Foreign Key constraint violation
            {
                // Pesan bisa lebih spesifik tergantung tabel mana yg FK nya error
                if (ex.Message.Contains("FK__PaketWisa__IDDri"))
                {
                    MessageBox.Show("Driver yang dipilih tidak valid atau tidak ada.", "Kesalahan Referensi Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (ex.Message.Contains("FK__PaketWisa__IDKen"))
                {
                    MessageBox.Show("Kendaraan yang dipilih tidak valid atau tidak ada.", "Kesalahan Referensi Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (ex.Message.Contains("FK__Pemesanan__IDPak")) // Error saat Hapus
                {
                    MessageBox.Show("Tidak dapat menghapus paket ini karena sudah digunakan dalam Pemesanan.", "Error Relasi Database", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (ex.Message.Contains("FK__Operasio__IDPak")) // Error saat Hapus
                {
                    MessageBox.Show("Tidak dapat menghapus paket ini karena sudah digunakan dalam data Operasional.", "Error Relasi Database", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Terjadi kesalahan referensi data. Pastikan data terkait valid.", "Kesalahan Referensi Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            // Bisa tambah penanganan CHECK constraint jika perlu (meski validasi UI harusnya mencegah)
        }


        // --- Event Handler Grid ---
        private void DgvPaketWisata_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // --- PERUBAHAN di DgvPaketWisata_CellClick ---
            if (e.RowIndex >= 0 && e.RowIndex < dgvPaketWisata.Rows.Count)
            {
                DataGridViewRow row = dgvPaketWisata.Rows[e.RowIndex];

                if (row.Cells["IDPaket"].Value != null && int.TryParse(row.Cells["IDPaket"].Value.ToString(), out int idPaket))
                { selectedPaketId = idPaket; }
                else { selectedPaketId = -1; ClearInputs(); return; }

                // Isi ComboBox FK
                cmbDriver.SelectedValue = row.Cells["IDDriver"].Value ?? DBNull.Value;
                cmbKendaraan.SelectedValue = row.Cells["IDKendaraan"].Value ?? DBNull.Value;

                // Isi ComboBox Kategori berdasarkan teks
                string kategoriValue = row.Cells["Kategori"].Value?.ToString();
                comboBoxKategori.SelectedItem = kategoriValue; // Cari item yang cocok
                if (comboBoxKategori.SelectedIndex < 0) // Jika tidak ada yang cocok
                {
                    comboBoxKategori.SelectedIndex = 0; // Set ke placeholder
                    // Optional: Beri tahu user jika data lama aneh
                    // if (!string.IsNullOrEmpty(kategoriValue))
                    //    MessageBox.Show($"Kategori '{kategoriValue}' tidak ada dalam pilihan.", "Info Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Isi TextBox
                txtNamaPaket.Text = row.Cells["NamaPaket"].Value?.ToString();
                txtDestinasi.Text = row.Cells["Destinasi"].Value?.ToString();
                txtHarga.Text = row.Cells["Harga"].Value?.ToString();
                txtDurasi.Text = row.Cells["Durasi"].Value?.ToString();
                txtFasilitas.Text = row.Cells["Fasilitas"].Value?.ToString();
                // Hapus txtKategori.Text = ... ;
                txtKuota.Text = row.Cells["Kuota"].Value?.ToString();

                // Isi DateTimePicker
                object jadwalValue = row.Cells["Jadwal"].Value;
                if (jadwalValue != null && jadwalValue != DBNull.Value)
                {
                    try { dateTimePicker1.Value = (DateTime)jadwalValue; }
                    catch (Exception) // Tangkap InvalidCast atau ArgumentOutOfRange
                    {
                        if (DateTime.TryParse(jadwalValue.ToString(), out DateTime parsedDate))
                        { dateTimePicker1.Value = parsedDate; }
                        else { dateTimePicker1.Value = DateTime.Now; } // Default jika parse gagal
                    }
                }
                else { dateTimePicker1.Value = DateTime.Now; } // Default jika null
            }
            else { ClearInputs(); }
        }

        // --- Event handler Load Form ---
        private void PaketWisata_Load(object sender, EventArgs e)
        {
            // Tidak perlu lagi jika sudah di constructor
        }
    }
}