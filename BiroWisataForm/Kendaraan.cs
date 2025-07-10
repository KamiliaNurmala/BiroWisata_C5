using praktikum7;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace BiroWisataForm
{
    public partial class Kendaraan : Form
    {
        Koneksi kn = new Koneksi();
        private string connectionString = "";

        public Kendaraan()
        {
            connectionString = kn.connectionString();

            InitializeComponent();
            InitializeDataGridViewSettings();
            InitializeSearchBox();
            InitializeStatusComboBox(); // Tambahkan ini
        }

        // Ganti metode ini di file Kendaraan.cs
        private void Kendaraan_Load(object sender, EventArgs e)
        {
            // 1. Muat data ke dalam tabel
            RefreshData();

            // 2. Kosongkan semua isian form
            ClearFields();
        }

        private void InitializeStatusComboBox()
        {
            // Pastikan Anda memiliki ComboBox bernama 'cmbStatus' pada form designer
            if (this.Controls.Find("cmbStatus", true).FirstOrDefault() is ComboBox cmb)
            {
                cmb.Items.Clear();
                cmb.Items.Add("Aktif");
                cmb.Items.Add("Tidak Aktif");
                cmb.Items.Add("Dalam Perbaikan");
                cmb.DropDownStyle = ComboBoxStyle.DropDownList;
                if (cmb.Items.Count > 0) cmb.SelectedIndex = 0;
            }
        }

        private void InitializeDataGridViewSettings()
        {
            dgvKendaraan.AutoGenerateColumns = false; // VERY IMPORTANT
            dgvKendaraan.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvKendaraan.MultiSelect = false;
            dgvKendaraan.ReadOnly = true;
            dgvKendaraan.AllowUserToAddRows = false;
            dgvKendaraan.AllowUserToDeleteRows = false;
            dgvKendaraan.RowHeadersVisible = false;

            // Styling from your Designer.cs can remain, or be set here.
            // For example:
            dgvKendaraan.BackgroundColor = System.Drawing.Color.White;
            dgvKendaraan.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dgvKendaraan.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            dgvKendaraan.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dgvKendaraan.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dgvKendaraan.EnableHeadersVisualStyles = false;
            dgvKendaraan.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            dgvKendaraan.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            dgvKendaraan.GridColor = System.Drawing.Color.Gainsboro;
            dgvKendaraan.ColumnHeadersHeight = 34;
            dgvKendaraan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;


            // --- Programmatically define and add columns ---
            dgvKendaraan.Columns.Clear(); // Clear any existing columns (e.g., from designer)

            var colID = new DataGridViewTextBoxColumn
            {
                Name = "colIDKendaraan", // Used in C# code to access this column's cells
                DataPropertyName = "IDKendaraan", // Must match DataTable/SQL column name
                HeaderText = "ID",
                Visible = false, // Typically hidden
                ReadOnly = true
            };

            var colJenis = new DataGridViewTextBoxColumn
            {
                Name = "colJenis",
                DataPropertyName = "Jenis",
                HeaderText = "Jenis Kendaraan", // More descriptive header
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, // Fills available space
                ReadOnly = true
            };

            var colPlat = new DataGridViewTextBoxColumn
            {
                Name = "colPlatNomor",
                DataPropertyName = "PlatNomor",
                HeaderText = "Plat Nomor",
                Width = 180, // Example fixed width, adjust as needed
                // AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, // Can't have multiple Fill columns easily
                ReadOnly = true
            };

            var colKapasitas = new DataGridViewTextBoxColumn
            {
                Name = "colKapasitas",
                DataPropertyName = "Kapasitas",
                HeaderText = "Kapasitas",
                Width = 100, // Fixed width
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight }, // Align numbers to right
                ReadOnly = true
            };

            // Add column for Status (as per your database schema)
            var colStatus = new DataGridViewTextBoxColumn
            {
                Name = "colStatus",
                DataPropertyName = "Status", // Must match DataTable/SQL column name
                HeaderText = "Status",
                Width = 120,
                ReadOnly = true
            };

            dgvKendaraan.Columns.AddRange(new DataGridViewColumn[] {
                colID,
                colJenis,
                colPlat,
                colKapasitas,
                colStatus // Added Status column
            });
        }

        private void InitializeSearchBox()
        {
            if (this.Controls.Find("txtSearch", true).FirstOrDefault() is TextBox txtSearchInstance)
            {
                txtSearchInstance.TextChanged += (s, e) =>
                {
                    RefreshData(txtSearchInstance.Text);
                };
            }
        }

        private void RefreshData(string searchTerm = null)
        {
            using (SqlConnection conn = new SqlConnection(kn.connectionString()))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT IDKendaraan, Jenis, PlatNomor, Kapasitas, Status FROM Kendaraan WHERE IsDeleted = 0";

                    if (!string.IsNullOrWhiteSpace(searchTerm))
                    {
                        query += " AND (Jenis LIKE @SearchTerm OR PlatNomor LIKE @SearchTerm OR Status LIKE @SearchTerm)";
                    }

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (!string.IsNullOrWhiteSpace(searchTerm))
                        {
                            cmd.Parameters.AddWithValue("@SearchTerm", $"%{searchTerm}%");
                        }

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dgvKendaraan.DataSource = dataTable;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading data: {ex.Message}", "Database Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            // ClearFields();  <--- HAPUS ATAU BERI KOMENTAR PADA BARIS INI
        }

        private bool ApakahPlatNomorSudahAda(string platNomor, int idKendaraanToExclude = 0)
        {
            using (SqlConnection conn = new SqlConnection(kn.connectionString()))
            {
                try
                {
                    conn.Open();
                    // Query dasar untuk menghitung plat nomor yang cocok
                    string query = "SELECT COUNT(1) FROM Kendaraan WHERE PlatNomor = @PlatNomor AND IsDeleted = 0";

                    // Jika kita sedang mengedit (ID > 0), kecualikan data saat ini dari pengecekan
                    if (idKendaraanToExclude > 0)
                    {
                        query += " AND IDKendaraan != @IDKendaraan";
                    }

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@PlatNomor", platNomor);
                        if (idKendaraanToExclude > 0)
                        {
                            cmd.Parameters.AddWithValue("@IDKendaraan", idKendaraanToExclude);
                        }

                        // ExecuteScalar sangat efisien untuk mengambil satu nilai (jumlah)
                        int count = Convert.ToInt32(cmd.ExecuteScalar());

                        // Jika count > 0, berarti plat nomor sudah ada
                        return count > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saat memeriksa duplikasi plat nomor: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Asumsikan ada error, anggap saja sudah ada untuk mencegah entri yang salah
                    return true;
                }
            }
        }

        private bool ValidateInputs(out short kapasitas)
        {
            kapasitas = 0;
            string errorMsg = "";
            bool isValid = true;

            // --- PERUBAHAN VALIDASI JENIS DI SINI ---
            if (string.IsNullOrWhiteSpace(txtJenis.Text))
            {
                errorMsg += "- Jenis kendaraan harus diisi.\n";
                isValid = false;
            }
            // Tambahan: Cek apakah hanya berisi huruf dan spasi
            else if (!System.Text.RegularExpressions.Regex.IsMatch(txtJenis.Text, @"^[a-zA-Z\s]+$"))
            {
                errorMsg += "- Jenis kendaraan hanya boleh berisi huruf dan spasi.\n";
                isValid = false;
            }
            // --- AKHIR PERUBAHAN ---

            // Validasi Plat Nomor
            if (string.IsNullOrWhiteSpace(txtPlatNomor.Text))
            {
                errorMsg += "- Plat nomor harus diisi.\n";
                isValid = false;
            }
            else if (txtPlatNomor.Text.Trim().Length < 5 || txtPlatNomor.Text.Trim().Length > 12)
            {
                errorMsg += "- Plat nomor harus antara 5 dan 12 karakter.\n";
                isValid = false;
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(txtPlatNomor.Text, @"^[a-zA-Z0-9\s]+$"))
            {
                errorMsg += "- Plat nomor hanya boleh berisi huruf, angka, dan spasi.\n";
                isValid = false;
            }

            // Validasi Kapasitas
            if (string.IsNullOrWhiteSpace(txtKapasitas.Text))
            {
                errorMsg += "- Kapasitas harus diisi.\n";
                isValid = false;
            }
            else if (!short.TryParse(txtKapasitas.Text, out kapasitas) || kapasitas <= 0)
            {
                errorMsg += "- Kapasitas harus berupa angka positif.\n";
                isValid = false;
            }

            // Validasi Status ComboBox
            if (this.Controls.Find("cmbStatus", true).FirstOrDefault() is ComboBox cmb)
            {
                if (cmb.SelectedItem == null)
                {
                    errorMsg += "- Status harus dipilih.\n";
                    isValid = false;
                }
            }

            if (!isValid)
            {
                MessageBox.Show("Harap perbaiki input yang tidak valid:\n\n" + errorMsg, "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return isValid;
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            // 1. Panggil metode validasi utama.
            // Metode ini akan menampilkan MessageBox jika ada input yang tidak valid.
            // Parameter 'out' digunakan untuk mendapatkan nilai kapasitas yang sudah divalidasi.
            if (!ValidateInputs(out short kapasitasVal))
            {
                return; // Hentikan proses jika validasi dasar gagal.
            }

            // 2. Lakukan pengecekan duplikasi Plat Nomor setelah validasi dasar lolos.
            string platNomor = txtPlatNomor.Text.Trim();
            if (ApakahPlatNomorSudahAda(platNomor))
            {
                MessageBox.Show("Plat nomor yang sama sudah terdaftar. Silakan gunakan plat nomor yang lain.",
                                "Duplikasi Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 3. Ambil data status dari ComboBox.
            string status = (this.Controls.Find("cmbStatus", true).FirstOrDefault() as ComboBox)?.SelectedItem.ToString() ?? "Aktif";

            // 4. Proses penyimpanan data ke database.
            using (SqlConnection conn = new SqlConnection(kn.connectionString()))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    using (SqlCommand cmd = new SqlCommand("sp_AddKendaraan", conn, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Jenis", txtJenis.Text.Trim());
                        cmd.Parameters.AddWithValue("@PlatNomor", platNomor);
                        cmd.Parameters.AddWithValue("@Kapasitas", kapasitasVal);
                        cmd.Parameters.AddWithValue("@Status", status);
                        cmd.Parameters.AddWithValue("@CreatedBy", Environment.UserName);
                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();

                    MessageBox.Show("Data kendaraan berhasil ditambahkan!", "Sukses",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                    RefreshData();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    if (ex is SqlException sqlEx)
                    {
                        MessageBox.Show($"Error Database: {sqlEx.Message}\nNomor Kesalahan: {sqlEx.Number}", "Database Error",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show($"Error: {ex.Message}", "Error Umum",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (dgvKendaraan.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih kendaraan yang akan dihapus.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Apakah Anda yakin ingin menghapus data kendaraan ini?", "Konfirmasi Hapus",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int selectedId = Convert.ToInt32(dgvKendaraan.SelectedRows[0].Cells["colIDKendaraan"].Value);

                using (SqlConnection conn = new SqlConnection(kn.connectionString()))
                {
                    try
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand("sp_DeleteKendaraan", conn)) // Assuming sp_DeleteKendaraan does a soft delete
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@IDKendaraan", selectedId);
                            cmd.Parameters.AddWithValue("@UpdatedBy", Environment.UserName);

                            cmd.ExecuteNonQuery(); // Execute the SP

                            // If no exception, assume success
                            MessageBox.Show("Data kendaraan berhasil dihapus.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            RefreshData();
                        }
                    }
                    catch (SqlException ex)
                    {
                        // Handle specific FK violation if Kendaraan is referenced elsewhere and cannot be soft-deleted
                        if (ex.Number == 547)
                        {
                            MessageBox.Show($"Error: Kendaraan ini tidak dapat dihapus karena masih terhubung dengan data lain (misalnya Paket Wisata).\nDetail: {ex.Message}", "Error Relasi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            MessageBox.Show($"Error Database saat menghapus data: {ex.Message}\nNomor Kesalahan: {ex.Number}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error saat menghapus data: {ex.Message}", "Error Umum",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnUbah_Click(object sender, EventArgs e)
        {
            // 1. Pastikan ada baris data yang dipilih
            if (dgvKendaraan.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih kendaraan yang akan diubah.", "Peringatan",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Cek apakah ada data yang diubah oleh pengguna
            DataGridViewRow selectedRow = dgvKendaraan.SelectedRows[0];
            string statusTerpilih = (this.Controls.Find("cmbStatus", true).FirstOrDefault() as ComboBox)?.SelectedItem.ToString() ?? "Aktif";

            bool dataTelahBerubah =
                selectedRow.Cells["colJenis"].Value.ToString().Trim() != txtJenis.Text.Trim() ||
                selectedRow.Cells["colPlatNomor"].Value.ToString().Trim() != txtPlatNomor.Text.Trim() ||
                selectedRow.Cells["colKapasitas"].Value.ToString().Trim() != txtKapasitas.Text.Trim() ||
                selectedRow.Cells["colStatus"].Value.ToString().Trim() != statusTerpilih;

            if (!dataTelahBerubah)
            {
                MessageBox.Show("Tidak ada perubahan data yang dilakukan.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 3. Lakukan validasi input jika ada perubahan
            if (!ValidateInputs(out short kapasitasVal))
            {
                // Pesan error sudah ditampilkan dari dalam ValidateInputs
                return;
            }

            // 4. Jalankan proses update ke database
            int selectedId = Convert.ToInt32(selectedRow.Cells["colIDKendaraan"].Value);
            using (SqlConnection conn = new SqlConnection(kn.connectionString()))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    using (SqlCommand cmd = new SqlCommand("sp_UpdateKendaraan", conn, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IDKendaraan", selectedId);
                        cmd.Parameters.AddWithValue("@Jenis", txtJenis.Text.Trim());
                        cmd.Parameters.AddWithValue("@PlatNomor", txtPlatNomor.Text.Trim());
                        cmd.Parameters.AddWithValue("@Kapasitas", kapasitasVal);
                        cmd.Parameters.AddWithValue("@Status", statusTerpilih);
                        cmd.Parameters.AddWithValue("@UpdatedBy", Environment.UserName);
                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    MessageBox.Show("Data kendaraan berhasil diubah.", "Sukses",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshData();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    if (ex is SqlException sqlEx)
                    {
                        MessageBox.Show($"Error Database saat mengubah data: {sqlEx.Message}\nNomor Kesalahan: {sqlEx.Number}", "Database Error",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show($"Error saat mengubah data: {ex.Message}", "Error Umum",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }


        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (this.Controls.Find("txtSearch", true).FirstOrDefault() is TextBox txtSearch)
            {
                txtSearch.Clear();
            }
            RefreshData(); // Memuat ulang data di tabel
            ClearFields(); // <-- TAMBAHKAN BARIS INI untuk membersihkan form
            MessageBox.Show("Data berhasil dimuat ulang.", "Refresh", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ClearFields()
        {
            txtJenis.Clear();
            txtPlatNomor.Clear();
            txtKapasitas.Clear();
            if (this.Controls.Find("cmbStatus", true).FirstOrDefault() is ComboBox cmb && cmb.Items.Count > 0) cmb.SelectedIndex = 0;
            dgvKendaraan.ClearSelection();
            txtJenis.Focus();
        }

        // Combined logic for populating fields from selected row
        private void PopulateFieldsFromSelectedRow()
        {
            if (dgvKendaraan.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvKendaraan.SelectedRows[0];
                txtJenis.Text = row.Cells["colJenis"].Value.ToString();
                txtPlatNomor.Text = row.Cells["colPlatNomor"].Value.ToString();
                txtKapasitas.Text = row.Cells["colKapasitas"].Value.ToString();

                if (this.Controls.Find("cmbStatus", true).FirstOrDefault() is ComboBox cmb)
                {
                    cmb.SelectedItem = row.Cells["colStatus"].Value?.ToString() ?? "Aktif";
                }
            }
        }

        // Connect this to dgvKendaraan.CellClick event in the designer
        private void dgvKendaraan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // e.RowIndex >= 0 ensures it's not a header click
            if (e.RowIndex >= 0)
            {
                PopulateFieldsFromSelectedRow();
            }
        }

        // Connect this to dgvKendaraan.SelectionChanged event in the designer
        private void dgvKendaraan_SelectionChanged(object sender, EventArgs e)
        {
            PopulateFieldsFromSelectedRow();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        // Letakkan method ini bersama dengan event handler tombol lainnya (seperti btnTambah_Click)

        private void btnKembali_Click(object sender, EventArgs e)
        {
            // Perintah ini akan menutup form 'Kendaraan' saat ini,
            // dan mengembalikan kontrol ke form yang membukanya (yaitu MenuAdmin).
            this.Close();
        }

        private void dgvKendaraan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}