using praktikum7;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing; // Perlu untuk Point dan Size jika digunakan (tapi kita hapus)
using System.Linq;
using System.Windows.Forms;

namespace BiroWisataForm
{
    public partial class Driver : Form
    {
        Koneksi kn = new Koneksi();
        private string connectionString = "";
        private int selectedDriverId = -1; // Untuk menyimpan ID driver terpilih

        // Add status tracking for the driver
        private enum DriverStatus
        {
            Aktif,
            TidakAktif
        }

        // Add a status field to the form
        //private ComboBox cmbStatus;

        public Driver()
        {
            InitializeComponent();

            // UBAH DI SINI: Inisialisasi connectionString sekali saja
            connectionString = kn.connectionString();

            ApplyCustomButtonStyles();
            InitializeDataGridView();
            // InitializeSearchBox();
            InitializeStatusComboBox();
        }

        private void ApplyCustomButtonStyles()
        {
            // Call the StyleButton method for each button
            StyleButton(this.btnTambah, Color.FromArgb(46, 204, 113), Color.White);
            StyleButton(this.btnUbah, Color.FromArgb(52, 152, 219), Color.White);
            StyleButton(this.btnHapus, Color.FromArgb(231, 76, 60), Color.White);
            StyleButton(this.btnRefresh, Color.FromArgb(149, 165, 166), Color.White);
        }

        private void InitializeStatusComboBox()
        {
            // Cari kontrol ComboBox dengan nama "cmbStatus" di form
            var foundControls = this.Controls.Find("cmbStatus", true);
            if (foundControls.Length > 0 && foundControls[0] is ComboBox)
            {
                // Jika ditemukan, hubungkan ke variabel cmbStatus kita
                this.cmbStatus = (ComboBox)foundControls[0];

                // Sekarang aman untuk menggunakannya
                this.cmbStatus.Items.Clear();
                this.cmbStatus.Items.Add("Aktif");
                this.cmbStatus.Items.Add("Tidak Aktif");
                this.cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
                if (this.cmbStatus.Items.Count > 0) this.cmbStatus.SelectedIndex = 0;
            }
            else
            {
                // Opsional: Beri peringatan jika ComboBox tidak ditemukan
                MessageBox.Show("Kontrol 'cmbStatus' tidak ditemukan di form designer.", "Peringatan UI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Helper method for styling buttons - Stays in Driver.cs
        private void StyleButton(Button btn, Color backColor, Color foreColor)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = backColor;
            btn.ForeColor = foreColor;
            // You might want to match the font from your other forms if it's bold:
            btn.Font = new Font("Segoe UI", 9F, FontStyle.Bold); // Or FontStyle.Regular
            btn.Size = new Size(120, 40);
            btn.Cursor = Cursors.Hand;
            btn.UseVisualStyleBackColor = false; // Crucial for custom BackColor with FlatStyle.Flat
        }

        // --- Ganti Nama Method agar Cocok dengan Designer ---
        // GANTI METODE INI
        private void Driver_Load_1(object sender, EventArgs e)
        {
            // Panggil RefreshData tanpa parameter kedua agar isSearching bernilai false
            RefreshData();
        }

        // --- Perbaiki Inisialisasi DataGridView ---
        private void InitializeDataGridView()
        {
            dgvDriver.AutoGenerateColumns = false;
            dgvDriver.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDriver.MultiSelect = false;
            dgvDriver.ReadOnly = true;
            dgvDriver.AllowUserToAddRows = false;
            dgvDriver.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDriver.CellClick += DgvDriver_CellClick;

            // Definisikan Kolom Secara Manual
            dgvDriver.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IDDriver",
                HeaderText = "ID",
                DataPropertyName = "IDDriver",
                Visible = false
            });
            dgvDriver.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NamaDriver",
                HeaderText = "Nama Driver",
                DataPropertyName = "NamaDriver"
            });
            dgvDriver.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NoTelp",
                HeaderText = "No. Telepon",
                DataPropertyName = "NoTelp"
            });
            dgvDriver.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NoSIM",
                HeaderText = "No. SIM",
                DataPropertyName = "NoSIM"
            });
            // ***** THIS IS THE LINE THAT NEEDS TO BE ACTUAL CODE *****
            dgvDriver.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Status",             // The programmatic name for the DGV column
                HeaderText = "Status",          // The text displayed in the column header
                DataPropertyName = "Status"     // Links to the "Status" column in your DataTable
            });
            // ***** END OF CRUCIAL LINE *****
        }

        // GANTI METODE INI
        private void RefreshData(string searchTerm = null, bool isSearching = false) // TAMBAHKAN parameter isSearching
        {
            using (SqlConnection conn = new SqlConnection(kn.connectionString()))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT IDDriver, NamaDriver, NoTelp, NoSIM, Status FROM Driver WHERE IsDeleted = 0";

                    if (!string.IsNullOrWhiteSpace(searchTerm))
                    {
                        query += " AND (NamaDriver LIKE @SearchTerm OR NoTelp LIKE @SearchTerm OR NoSIM LIKE @SearchTerm OR Status LIKE @SearchTerm)";
                    }

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);

                    if (!string.IsNullOrWhiteSpace(searchTerm))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@SearchTerm", $"%{searchTerm}%");
                    }

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dgvDriver.DataSource = dataTable;

                    // --- PERUBAHAN UTAMA DIMULAI DI SINI ---
                    // Logika ini hanya akan berjalan jika kita TIDAK sedang mencari (isSearching == false)
                    if (!isSearching)
                    {
                        if (dataTable.Rows.Count == 1 && string.IsNullOrWhiteSpace(searchTerm))
                        {
                            dgvDriver.Rows[0].Selected = true;
                            PopulateFieldsFromRow(dgvDriver.Rows[0]);
                        }
                        else
                        {
                            ClearFields();
                        }
                    }
                    // --- AKHIR PERUBAHAN UTAMA ---
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saat memuat data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnTambah_Click(object sender, EventArgs e)
        {
            if (!ValidateInputFields()) return;
            if (IsDriverDuplikat(txtNoTel.Text.Trim(), txtNoSim.Text.Trim()))
            {
                MessageBox.Show("Nomor Telepon atau Nomor SIM sudah terdaftar.", "Data Duplikat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Gunakan 'using' untuk memastikan koneksi selalu ditutup, bahkan saat error
            using (SqlConnection conn = new SqlConnection(kn.connectionString()))
            {
                SqlTransaction transaction = null; // Inisialisasi transaksi di luar try-catch

                try
                {
                    conn.Open();
                    transaction = conn.BeginTransaction(); // 1. Mulai transaksi

                    using (SqlCommand cmd = new SqlCommand("sp_AddDriver", conn, transaction)) // 2. Sertakan transaksi di command
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NamaDriver", txtNama.Text.Trim());
                        cmd.Parameters.AddWithValue("@NoTelp", txtNoTel.Text.Trim());
                        cmd.Parameters.AddWithValue("@NoSIM", txtNoSim.Text.Trim());
                        cmd.Parameters.AddWithValue("@CreatedBy", Environment.UserName);
                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit(); // 3. Jika semua berhasil, commit transaksi
                    MessageBox.Show("Data driver berhasil ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    RefreshData();
                }
                catch (Exception ex)
                {
                    transaction?.Rollback(); // 4. Jika terjadi error, rollback semua perubahan
                    MessageBox.Show($"Operasi gagal, semua perubahan dibatalkan.\n\nError: {ex.Message}", "Transaksi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // --- Sederhanakan Hapus (Tanpa Reseed) ---
        private void BtnHapus_Click(object sender, EventArgs e)
        {
            if (selectedDriverId < 0)
            {
                MessageBox.Show("Pilih driver yang ingin dihapus!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Apakah Anda yakin ingin menghapus data driver ini?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(kn.connectionString()))
                {
                    SqlTransaction transaction = null;

                    try
                    {
                        conn.Open();
                        transaction = conn.BeginTransaction(); // 1. Mulai transaksi

                        using (SqlCommand cmdDelete = new SqlCommand("sp_DeleteDriver", conn, transaction)) // 2. Sertakan transaksi
                        {
                            cmdDelete.CommandType = CommandType.StoredProcedure;
                            cmdDelete.Parameters.AddWithValue("@IDDriver", selectedDriverId);
                            cmdDelete.Parameters.AddWithValue("@UpdatedBy", Environment.UserName);
                            cmdDelete.ExecuteNonQuery();
                        }

                        transaction.Commit(); // 3. Jika sukses, commit
                        MessageBox.Show("Data driver berhasil dihapus.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        RefreshData();
                    }
                    catch (SqlException ex)
                    {
                        transaction?.Rollback(); // 4. Jika gagal, rollback
                        if (ex.Number == 547)
                        {
                            MessageBox.Show("Gagal menghapus. Driver ini masih terhubung dengan data lain (misal: Paket Wisata).", "Error Relasi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            MessageBox.Show($"Error Database: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction?.Rollback(); // 4. Jika gagal, rollback
                        MessageBox.Show($"Operasi gagal, semua perubahan dibatalkan.\n\nError: {ex.Message}", "Transaksi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private bool IsDriverDuplikat(string noTelp, string noSim, int idDriverToExclude = 0)
        {
            using (SqlConnection conn = new SqlConnection(kn.connectionString()))
            {
                try
                {
                    conn.Open();
                    // Query untuk menghitung driver dengan NoTelp ATAU NoSIM yang sama
                    // dan belum di "soft delete".
                    string query = @"
                SELECT COUNT(*) 
                FROM Driver 
                WHERE (NoTelp = @NoTelp OR NoSIM = @NoSIM) 
                  AND IsDeleted = 0";

                    // Saat mode 'Ubah', kecualikan ID driver yang sedang diedit
                    if (idDriverToExclude > 0)
                    {
                        query += " AND IDDriver != @IDDriver";
                    }

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@NoTelp", noTelp);
                        cmd.Parameters.AddWithValue("@NoSIM", noSim);

                        if (idDriverToExclude > 0)
                        {
                            cmd.Parameters.AddWithValue("@IDDriver", idDriverToExclude);
                        }

                        int count = (int)cmd.ExecuteScalar();

                        // Jika count > 0, berarti ada duplikat.
                        return count > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Terjadi kesalahan saat memeriksa duplikasi data driver: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Anggap true (ada duplikat) jika terjadi error untuk keamanan data.
                    return true;
                }
            }
        }

        private void BtnUbah_Click(object sender, EventArgs e)
        {
            if (selectedDriverId < 0)
            {
                MessageBox.Show("Pilih driver yang ingin diubah!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!ValidateInputFields()) return;
            if (IsDriverDuplikat(txtNoTel.Text.Trim(), txtNoSim.Text.Trim(), selectedDriverId))
            {
                MessageBox.Show("Nomor Telepon atau Nomor SIM sudah digunakan oleh driver lain.", "Data Duplikat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(kn.connectionString()))
            {
                SqlTransaction transaction = null;

                try
                {
                    conn.Open();
                    transaction = conn.BeginTransaction(); // 1. Mulai transaksi

                    using (SqlCommand cmd = new SqlCommand("sp_UpdateDriver", conn, transaction)) // 2. Sertakan transaksi
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IDDriver", selectedDriverId);
                        cmd.Parameters.AddWithValue("@NamaDriver", txtNama.Text.Trim());
                        cmd.Parameters.AddWithValue("@NoTelp", txtNoTel.Text.Trim());
                        cmd.Parameters.AddWithValue("@NoSIM", txtNoSim.Text.Trim());
                        cmd.Parameters.AddWithValue("@Status", this.cmbStatus.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@UpdatedBy", Environment.UserName);
                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit(); // 3. Jika sukses, commit
                    MessageBox.Show("Data driver berhasil diubah!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    RefreshData();
                }
                catch (Exception ex)
                {
                    transaction?.Rollback(); // 4. Jika gagal, rollback
                    MessageBox.Show($"Operasi gagal, semua perubahan dibatalkan.\n\nError: {ex.Message}", "Transaksi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // GANTI METODE INI
        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            // Periksa apakah kontrol txtSearch sudah diinisialisasi
            if (this.Controls.Find("textBox1", true).FirstOrDefault() is TextBox searchBox)
            {
                searchBox.Clear(); // Ganti txtSearch menjadi nama yang benar jika berbeda
            }

            // Panggil RefreshData tanpa parameter kedua agar isSearching bernilai false
            RefreshData();

            MessageBox.Show("Data berhasil dimuat ulang.", "Refresh Selesai",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ClearFields()
        {
            txtNama.Clear();
            txtNoTel.Clear();
            txtNoSim.Clear();
            selectedDriverId = -1; // Reset ID terpilih
            if (cmbStatus.Items.Count > 0) cmbStatus.SelectedIndex = 0; // Reset status
            dgvDriver.ClearSelection();
            txtNama.Focus();
        }

        // --- Tambahkan Validasi ---
        // --- Perbarui method ValidateInputFields ---
        private bool ValidateInputFields()
        {
            // Validasi Nama Driver (TIDAK BOLEH KOSONG)
            if (string.IsNullOrWhiteSpace(txtNama.Text))
            {
                ShowError("Nama Driver tidak boleh kosong!", txtNama);
                return false;
            }

            // --- TAMBAHAN: Validasi Nama Driver (HANYA HURUF DAN SPASI) ---
            // Pola Regex: ^[a-zA-Z\s]+$
            // ^      : Awal string
            // [a-zA-Z\s]+ : Satu atau lebih karakter yang merupakan huruf (a-z, A-Z) atau spasi (\s)
            // $      : Akhir string
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtNama.Text, @"^[a-zA-Z\s]+$"))
            {
                ShowError("Nama Driver hanya boleh berisi huruf dan spasi!", txtNama);
                return false;
            }
            // --- AKHIR TAMBAHAN ---


            // Validasi Nomor Telepon (TIDAK BOLEH KOSONG)
            if (string.IsNullOrWhiteSpace(txtNoTel.Text))
            {
                ShowError("Nomor Telepon tidak boleh kosong!", txtNoTel);
                return false;
            }

            // (Kode validasi No. Telepon dan No. SIM Anda yang lain tetap di sini)
            // ...
            string noTelp = txtNoTel.Text.Trim();
            if (!System.Text.RegularExpressions.Regex.IsMatch(noTelp, @"^08[0-9]{8,11}$"))
            {
                ShowError("Nomor telepon harus dimulai dengan '08' dan panjang 10-13 digit!", txtNoTel);
                return false;
            }

            string noSim = txtNoSim.Text.Trim();
            if (!System.Text.RegularExpressions.Regex.IsMatch(noSim, @"^[0-9]{14}$")) // No SIM di Indonesia 12 digit
            {
                ShowError("Nomor SIM harus 14 digit angka!", txtNoSim);
                return false;
            }


            return true;
        }

        private void ShowError(string message, Control control)
        {
            MessageBox.Show(message, "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            control.Focus();
        }

        // --- Tambahkan Event Handler CellClick ---
        // --- GANTI metode DgvDriver_CellClick menjadi seperti ini ---
        private void DgvDriver_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Panggil metode baru kita
                PopulateFieldsFromRow(dgvDriver.Rows[e.RowIndex]);
            }
        }

        // --- TAMBAHKAN metode baru ini di bawah DgvDriver_CellClick ---
        private void PopulateFieldsFromRow(DataGridViewRow row)
        {
            // Logika ini dipindahkan dari DgvDriver_CellClick
            selectedDriverId = Convert.ToInt32(row.Cells["IDDriver"].Value);
            txtNama.Text = row.Cells["NamaDriver"].Value?.ToString() ?? "";
            txtNoTel.Text = row.Cells["NoTelp"].Value?.ToString() ?? "";
            txtNoSim.Text = row.Cells["NoSIM"].Value?.ToString() ?? "";

            string statusValue = row.Cells["Status"].Value?.ToString() ?? "Aktif";

            // Pastikan cmbStatus tidak null sebelum digunakan
            if (this.cmbStatus != null)
            {
                cmbStatus.SelectedItem = statusValue;
            }
        }

        // Add search functionality
        private TextBox txtSearch;
        private void InitializeSearchBox()
        {
            // Hapus implementasi lama dan cukup pastikan txtSearch sudah ada di form designer
            if (this.Controls.Find("txtSearch", true).FirstOrDefault() is TextBox searchBox)
            {
                this.txtSearch = searchBox;
                this.txtSearch.TextChanged += (s, e) => RefreshData(this.txtSearch.Text);
            }
        }

        private void dgvDriver_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        // Letakkan method ini bersama dengan event handler tombol lainnya (seperti btnTambah_Click)

        private void btnKembali_Click(object sender, EventArgs e)
        {
            // Perintah ini akan menutup form 'Kendaraan' saat ini,
            // dan mengembalikan kontrol ke form yang membukanya (yaitu MenuAdmin).
            this.Close();
        }

        // GANTI METODE INI
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            TextBox searchBox = sender as TextBox;
            if (searchBox != null)
            {
                // Panggil RefreshData dengan parameter kedua true untuk menandakan ini adalah proses pencarian
                RefreshData(searchBox.Text, true);
            }
        }

        // private void TxtSearch_TextChanged(object sender, EventArgs e)
        // {
        //     if (dgvDriver.DataSource is DataTable dt)
        //     {
        //         string searchText = txtSearch.Text.ToLower();
        //         dt.DefaultView.RowFilter = $"NamaDriver LIKE '%{searchText}%' OR NoTelp LIKE '%{searchText}%' OR NoSIM LIKE '%{searchText}%'";
        //     }
        // }
    }
}