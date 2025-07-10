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




        /// <summary>
        /// Memeriksa apakah ada data operasional duplikat berdasarkan kombinasi kunci.
        /// </summary>
        /// <param name="idPaket">ID Paket Wisata yang dipilih.</param>
        /// <param name="idDriver">ID Driver yang dipilih.</param>
        /// <param name="idKendaraan">ID Kendaraan yang dipilih.</param>
        /// <param name="jenisPengeluaran">Jenis pengeluaran yang dipilih.</param>
        /// <param name="currentOperasionalId">ID operasional saat ini (digunakan saat update untuk mengecualikan data itu sendiri dari pengecekan).</param>
        /// <returns>True jika duplikat ditemukan, False jika tidak.</returns>
        private bool CheckForDuplicate(int idPaket, int idDriver, int idKendaraan, string jenisPengeluaran, int currentOperasionalId = -1)
        {
            using (var conn = new SqlConnection(kn.connectionString()))
            {
                try
                {
                    conn.Open();
                    // Query ini menghitung jumlah record yang cocok dengan kombinasi input,
                    // dan mengabaikan ID data yang sedang diedit (jika ada).
                    string query = @"
                SELECT COUNT(1) 
                FROM Operasional 
                WHERE IDPaket = @IDPaket 
                  AND IDDriver = @IDDriver 
                  AND IDKendaraan = @IDKendaraan 
                  AND JenisPengeluaran = @JenisPengeluaran
                  AND IDOperasional <> @IDOperasional"; // <> artinya "tidak sama dengan"

                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IDPaket", idPaket);
                        cmd.Parameters.AddWithValue("@IDDriver", idDriver);
                        cmd.Parameters.AddWithValue("@IDKendaraan", idKendaraan);
                        cmd.Parameters.AddWithValue("@JenisPengeluaran", jenisPengeluaran);
                        cmd.Parameters.AddWithValue("@IDOperasional", currentOperasionalId);

                        // ExecuteScalar() sangat efisien untuk mendapatkan satu nilai tunggal (dalam hal ini, jumlah data).
                        int count = Convert.ToInt32(cmd.ExecuteScalar());

                        // Jika hitungan lebih dari 0, berarti ada duplikat.
                        return count > 0;
                    }
                }
                catch (Exception ex)
                {
                    HandleGeneralError("memeriksa duplikasi data", ex);
                    // Jika terjadi error saat pengecekan, kita anggap ada duplikat untuk mencegah kesalahan.
                    return true;
                }
            }
        }


        /// <summary>
        /// Memeriksa apakah sebuah kendaraan sudah ditugaskan ke driver yang berbeda di data operasional lain.
        /// </summary>
        /// <param name="idKendaraan">ID Kendaraan yang akan diperiksa.</param>
        /// <param name="idDriver">ID Driver yang akan ditugaskan.</param>
        /// <param name="currentOperasionalId">ID operasional saat ini (untuk mode Ubah).</param>
        /// <returns>True jika kendaraan sudah digunakan driver lain, False jika belum.</returns>
        private bool IsKendaraanAssignedToDifferentDriver(int idKendaraan, int idDriver, int currentOperasionalId = -1)
        {
            using (var conn = new SqlConnection(kn.connectionString()))
            {
                try
                {
                    conn.Open();
                    // Query untuk mencari apakah kendaraan ini sudah dipakai oleh driver lain
                    // pada data operasional yang BUKAN yang sedang kita edit.
                    string query = @"
                SELECT COUNT(1) 
                FROM Operasional 
                WHERE IDKendaraan = @IDKendaraan 
                  AND IDDriver <> @IDDriver 
                  AND IDOperasional <> @IDOperasional";

                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IDKendaraan", idKendaraan);
                        cmd.Parameters.AddWithValue("@IDDriver", idDriver);
                        cmd.Parameters.AddWithValue("@IDOperasional", currentOperasionalId);

                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                }
                catch (Exception ex)
                {
                    HandleGeneralError("memeriksa tugas kendaraan", ex);
                    // Gagal aman, anggap ada konflik untuk mencegah data tidak konsisten.
                    return true;
                }
            }
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
            ClearInputs();
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
        // GANTI metode RefreshData Anda dengan yang ini
        private bool RefreshData(string searchTerm = null)
        {
            using (var conn = new SqlConnection(kn.connectionString()))
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
                        // --- PERUBAHAN UTAMA DI SINI ---
                        // Menambahkan pencarian berdasarkan BiayaOperasional
                        whereClause = @" WHERE p.NamaPaket LIKE @SearchTerm 
                                 OR d.NamaDriver LIKE @SearchTerm 
                                 OR (k.Jenis + ' (' + k.PlatNomor + ')') LIKE @SearchTerm 
                                 OR o.JenisPengeluaran LIKE @SearchTerm
                                 OR CAST(o.BiayaBBM AS VARCHAR(20)) LIKE @SearchTerm"; // <-- BARIS INI DITAMBAHKAN
                                                                                       // --- AKHIR PERUBAHAN ---
                    }
                    query += whereClause;

                    string orderByClause;
                    if (!string.IsNullOrWhiteSpace(searchTerm))
                    {
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
                        orderByClause = " ORDER BY o.IDOperasional DESC";
                    }
                    query += orderByClause;

                    var dt = new DataTable();
                    var adapter = new SqlDataAdapter(query, conn);

                    if (!string.IsNullOrWhiteSpace(searchTerm))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@SearchTerm", $"%{searchTerm}%");
                    }

                    adapter.Fill(dt);
                    dgvOperasional.DataSource = dt;

                    // Logika ini sudah benar, akan mengisi form jika hanya ada 1 hasil
                    if (dgvOperasional.Rows.Count == 1)
                    {
                        dgvOperasional.Rows[0].Selected = true;
                    }
                    else
                    {
                        // Jika hasil lebih dari 1 atau 0, bersihkan input
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

            int idPaket = Convert.ToInt32(cmbPaket.SelectedValue);
            int idDriver = Convert.ToInt32(cmbDriver.SelectedValue);
            int idKendaraan = Convert.ToInt32(cmbKendaraan.SelectedValue);
            string jenis = cmbJenisPengeluaran.SelectedItem.ToString();

            // Pengecekan aturan bisnis tetap di luar transaksi untuk efisiensi
            if (IsKendaraanAssignedToDifferentDriver(idKendaraan, idDriver))
            {
                MessageBox.Show("Kendaraan ini sudah ditugaskan untuk driver lain. Satu kendaraan hanya boleh dipegang oleh satu driver.", "Aturan Bisnis Dilanggar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (CheckForDuplicate(idPaket, idDriver, idKendaraan, jenis))
            {
                MessageBox.Show("Data operasional dengan kombinasi Paket, Driver, Kendaraan, dan Jenis Pengeluaran yang sama sudah ada.", "Data Duplikat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // --- PERUBAHAN DIMULAI DI SINI ---
            using (var conn = new SqlConnection(kn.connectionString()))
            {
                conn.Open();
                // 1. Memulai transaksi
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    using (var cmd = new SqlCommand("sp_AddOperasional", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        // 2. Mengaitkan command dengan transaksi
                        cmd.Transaction = transaction;

                        cmd.Parameters.AddWithValue("@IDPaket", cmbPaket.SelectedValue);
                        cmd.Parameters.AddWithValue("@IDDriver", cmbDriver.SelectedValue);
                        cmd.Parameters.AddWithValue("@IDKendaraan", cmbKendaraan.SelectedValue);
                        cmd.Parameters.AddWithValue("@BiayaOperasional", biaya);
                        cmd.Parameters.AddWithValue("@JenisPengeluaran", cmbJenisPengeluaran.SelectedItem.ToString());

                        cmd.ExecuteNonQuery();
                    }

                    // 3. Jika semua berhasil, commit transaksi
                    transaction.Commit();
                    MessageBox.Show("Data operasional berhasil ditambahkan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshData();
                }
                catch (Exception ex)
                {
                    // 4. Jika terjadi error, batalkan semua perubahan (rollback)
                    transaction.Rollback();
                    // Menampilkan pesan error yang lebih spesifik
                    HandleGeneralError("menambahkan data (transaksi dibatalkan)", ex);
                }
            }
            // --- AKHIR PERUBAHAN ---
        }

        private void btnUbah_Click(object sender, EventArgs e)
        {
            if (selectedOperasionalId < 0) { MessageBox.Show("Pilih data yang akan diubah.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (!ValidateInputs(out decimal biaya)) return;

            int idPaket = Convert.ToInt32(cmbPaket.SelectedValue);
            int idDriver = Convert.ToInt32(cmbDriver.SelectedValue);
            int idKendaraan = Convert.ToInt32(cmbKendaraan.SelectedValue);
            string jenis = cmbJenisPengeluaran.SelectedItem.ToString();

            if (IsKendaraanAssignedToDifferentDriver(idKendaraan, idDriver, selectedOperasionalId))
            {
                MessageBox.Show("Kendaraan ini sudah ditugaskan untuk driver lain. Satu kendaraan hanya boleh dipegang oleh satu driver.", "Aturan Bisnis Dilanggar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (CheckForDuplicate(idPaket, idDriver, idKendaraan, jenis, selectedOperasionalId))
            {
                MessageBox.Show("Kombinasi Paket, Driver, Kendaraan, dan Jenis Pengeluaran ini sudah digunakan oleh data lain.", "Data Duplikat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // --- PERUBAHAN DIMULAI DI SINI ---
            using (var conn = new SqlConnection(kn.connectionString()))
            {
                conn.Open();
                // 1. Memulai transaksi
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    using (var cmd = new SqlCommand("sp_UpdateOperasional", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        // 2. Mengaitkan command dengan transaksi
                        cmd.Transaction = transaction;

                        cmd.Parameters.AddWithValue("@IDOperasional", selectedOperasionalId);
                        cmd.Parameters.AddWithValue("@IDPaket", cmbPaket.SelectedValue);
                        cmd.Parameters.AddWithValue("@IDDriver", cmbDriver.SelectedValue);
                        cmd.Parameters.AddWithValue("@IDKendaraan", cmbKendaraan.SelectedValue);
                        cmd.Parameters.AddWithValue("@BiayaOperasional", biaya);
                        cmd.Parameters.AddWithValue("@JenisPengeluaran", cmbJenisPengeluaran.SelectedItem.ToString());

                        cmd.ExecuteNonQuery();
                    }

                    // 3. Jika semua berhasil, commit transaksi
                    transaction.Commit();
                    MessageBox.Show("Data operasional berhasil diubah.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshData();
                }
                catch (Exception ex)
                {
                    // 4. Jika terjadi error, batalkan semua perubahan (rollback)
                    transaction.Rollback();
                    // Menampilkan pesan error yang lebih spesifik
                    HandleGeneralError("mengubah data (transaksi dibatalkan)", ex);
                }
            }
            // --- AKHIR PERUBAHAN ---
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (selectedOperasionalId < 0) { MessageBox.Show("Pilih data yang akan dihapus.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            if (MessageBox.Show("Yakin ingin menghapus data ini?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (var conn = new SqlConnection(kn.connectionString()))
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

            bool isValid = true;
            string errorMsg = "";

            // Validasi Pilihan ComboBox
            if (cmbPaket.SelectedValue == null || cmbPaket.SelectedValue == DBNull.Value)
            {
                errorMsg += "- Paket Wisata harus dipilih.\n";
                isValid = false;
            }

            if (cmbDriver.SelectedValue == null || cmbDriver.SelectedValue == DBNull.Value)
            {
                errorMsg += "- Driver harus dipilih.\n";
                isValid = false;
            }

            if (cmbKendaraan.SelectedValue == null || cmbKendaraan.SelectedValue == DBNull.Value)
            {
                errorMsg += "- Kendaraan harus dipilih.\n";
                isValid = false;
            }

            if (cmbJenisPengeluaran.SelectedIndex <= 0)
            {
                errorMsg += "- Jenis Pengeluaran harus dipilih.\n";
                isValid = false;
            }

            // Validasi Input Biaya
            if (string.IsNullOrWhiteSpace(txtBiaya.Text))
            {
                errorMsg += "- Biaya tidak boleh kosong.\n";
                isValid = false;
            }
            else if (!decimal.TryParse(txtBiaya.Text.Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out biaya) || biaya <= 0)
            {
                errorMsg += "- Biaya harus berupa angka positif.\n";
                isValid = false;
            }
            else
            {
                // Validasi panjang desimal (10, 2)
                string biayaText = txtBiaya.Text.Trim();
                int decimalPointIndex = biayaText.IndexOf('.');
                int integerPartLength = (decimalPointIndex == -1) ? biayaText.Length : decimalPointIndex;
                int scalePartLength = (decimalPointIndex == -1) ? 0 : biayaText.Length - decimalPointIndex - 1;

                if (integerPartLength > 8 || scalePartLength > 2)
                {
                    errorMsg += "- Format biaya tidak sesuai (maksimal 8 digit sebelum koma dan 2 setelahnya).\n";
                    isValid = false;
                }
            }

            // Jika ada error, tampilkan semua pesan yang terkumpul
            if (!isValid)
            {
                MessageBox.Show("Harap perbaiki input yang tidak valid:\n\n" + errorMsg, "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

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

        private void txtCari_TextChanged(object sender, EventArgs e)
        {
            // Panggil metode RefreshData dengan teks dari kotak pencarian
            RefreshData(txtCari.Text);
        }
    }
}