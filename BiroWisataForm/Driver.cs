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
        //private string connectionString =
        //    //@"Data Source=MSI;Initial Catalog=BiroWisata;Integrated Security=True";
        private string connectionString = @"Data Source=KAMILIA\KAMILIANURMALA;Initial Catalog=BiroWisata;Integrated Security=True;";
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
            ApplyCustomButtonStyles();
            InitializeDataGridView();
            InitializeSearchBox();
            InitializeStatusComboBox(); // Tambahkan ini untuk mengisi ComboBox Status
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
                if(this.cmbStatus.Items.Count > 0) this.cmbStatus.SelectedIndex = 0;
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
        private void Driver_Load_1(object sender, EventArgs e)
        {
            RefreshData(); // Muat data saat form load
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

        private void RefreshData(string searchTerm = null)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    // Query dasar untuk mengambil driver yang tidak dihapus
                    string query = "SELECT IDDriver, NamaDriver, NoTelp, NoSIM, Status FROM Driver WHERE IsDeleted = 0";

                    // Tambahkan kondisi pencarian jika ada
                    if (!string.IsNullOrWhiteSpace(searchTerm))
                    {
                        query += " AND (NamaDriver LIKE @SearchTerm OR NoTelp LIKE @SearchTerm OR NoSIM LIKE @SearchTerm OR Status LIKE @SearchTerm)";
                    }

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Tambahkan parameter hanya jika diperlukan
                        if (!string.IsNullOrWhiteSpace(searchTerm))
                        {
                            cmd.Parameters.AddWithValue("@SearchTerm", $"%{searchTerm}%");
                        }

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dgvDriver.DataSource = null;
                        dgvDriver.DataSource = dataTable;
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Error Database: {ex.Message}", "Database Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "General Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            ClearFields(); // Bersihkan input setelah refresh
        }

        private void BtnTambah_Click(object sender, EventArgs e)
        {
            if (!ValidateInputFields()) return;

            // --- BARIS BARU: Pengecekan duplikasi sebelum menambah data ---
            if (IsDriverDuplikat(txtNoTel.Text.Trim(), txtNoSim.Text.Trim()))
            {
                MessageBox.Show("Nomor Telepon atau Nomor SIM sudah terdaftar untuk driver lain.", "Data Duplikat",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Hentikan eksekusi
            }
            // -----------------------------------------------------------

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_AddDriver", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NamaDriver", txtNama.Text.Trim());
                        cmd.Parameters.AddWithValue("@NoTelp", txtNoTel.Text.Trim());
                        cmd.Parameters.AddWithValue("@NoSIM", txtNoSim.Text.Trim());
                        cmd.Parameters.AddWithValue("@CreatedBy", Environment.UserName);

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Data driver berhasil ditambahkan!", "Sukses",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RefreshData();
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Error Database: {ex.Message}\nNomor: {ex.Number}", "Database Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "General Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // --- Sederhanakan Hapus (Tanpa Reseed) ---
        private void BtnHapus_Click(object sender, EventArgs e)
        {
            if (selectedDriverId < 0)
            {
                MessageBox.Show("Pilih baris driver yang ingin dihapus!", "Peringatan",
                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Apakah Anda yakin ingin menghapus data driver ini?", "Konfirmasi Hapus",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        using (SqlCommand cmdDelete = new SqlCommand("sp_DeleteDriver", conn))
                        {
                            cmdDelete.CommandType = CommandType.StoredProcedure;
                            cmdDelete.Parameters.AddWithValue("@IDDriver", selectedDriverId);
                            cmdDelete.Parameters.AddWithValue("@UpdatedBy", Environment.UserName); // Assuming your SP takes this

                            cmdDelete.ExecuteNonQuery(); // Execute the SP

                            // If no exception was thrown by the SP, assume success
                            MessageBox.Show("Data driver berhasil dihapus!", "Sukses",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            RefreshData(); // Refresh data & clear fields
                        }
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Number == 547) // Foreign Key constraint violation
                        {
                            MessageBox.Show($"Error: Tidak dapat menghapus driver ini karena masih terhubung dengan data lain (misalnya Paket Wisata atau Operasional).\nDetail: {ex.Message}", "Error Relasi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            MessageBox.Show($"Error Database: {ex.Message}\nNomor: {ex.Number}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}", "General Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private bool IsDriverDuplikat(string noTelp, string noSim, int idDriverToExclude = 0)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
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
                MessageBox.Show("Pilih baris driver yang ingin diubah!", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInputFields()) return;

            // --- BARIS BARU: Pengecekan duplikasi dengan mengecualikan ID saat ini ---
            if (IsDriverDuplikat(txtNoTel.Text.Trim(), txtNoSim.Text.Trim(), selectedDriverId))
            {
                MessageBox.Show("Nomor Telepon atau Nomor SIM yang Anda masukkan sudah digunakan oleh driver lain.", "Data Duplikat",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Hentikan eksekusi
            }
            // -----------------------------------------------------------------------

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_UpdateDriver", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@IDDriver", selectedDriverId);
                        cmd.Parameters.AddWithValue("@NamaDriver", txtNama.Text.Trim());
                        cmd.Parameters.AddWithValue("@NoTelp", txtNoTel.Text.Trim());
                        cmd.Parameters.AddWithValue("@NoSIM", txtNoSim.Text.Trim());
                        cmd.Parameters.AddWithValue("@Status", cmbStatus.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@UpdatedBy", Environment.UserName);

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Data driver berhasil diubah!", "Sukses",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RefreshData();
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Error Database: {ex.Message}\nNomor: {ex.Number}", "Database Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "General Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            if (txtSearch != null)
            {
                txtSearch.Clear(); // Clear search term before refreshing
            }
            RefreshData(); // This already calls ClearFields() at the end
            MessageBox.Show("Data berhasil dimuat ulang.", "Refresh Selesai",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ClearFields()
        {
            txtNama.Clear();
            txtNoTel.Clear();
            txtNoSim.Clear();
            selectedDriverId = -1; // Reset ID terpilih
            if(cmbStatus.Items.Count > 0) cmbStatus.SelectedIndex = 0; // Reset status
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
        private void DgvDriver_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvDriver.Rows.Count) // Ensure valid row index
            {
                DataGridViewRow row = dgvDriver.Rows[e.RowIndex];

                selectedDriverId = Convert.ToInt32(row.Cells["IDDriver"].Value);
                txtNama.Text = row.Cells["NamaDriver"].Value?.ToString() ?? ""; // Add null check
                txtNoTel.Text = row.Cells["NoTelp"].Value?.ToString() ?? "";   // Add null check
                txtNoSim.Text = row.Cells["NoSIM"].Value?.ToString() ?? "";     // Add null check

                // Perbarui ComboBox Status - ROBUST VERSION
                string statusValueFromGrid = row.Cells["Status"].Value?.ToString()?.Trim();

                if (this.cmbStatus != null) // Ensure cmbStatus field is assigned
                {
                    if (!string.IsNullOrEmpty(statusValueFromGrid))
                    {
                        bool itemFound = false;
                        for (int i = 0; i < cmbStatus.Items.Count; i++)
                        {
                            if (string.Equals(cmbStatus.Items[i].ToString(), statusValueFromGrid, StringComparison.OrdinalIgnoreCase))
                            {
                                cmbStatus.SelectedIndex = i;
                                itemFound = true;
                                break;
                            }
                        }
                        if (!itemFound && cmbStatus.Items.Count > 0)
                        {
                            cmbStatus.SelectedIndex = 0; // Default or handle as error
                            Console.WriteLine($"Warning: Status '{statusValueFromGrid}' not found in ComboBox. Defaulting.");
                        }
                    }
                    else if (cmbStatus.Items.Count > 0) // If status from grid is null/empty
                    {
                        cmbStatus.SelectedIndex = 0; // Default to first item
                    }
                }
                else
                {
                    Console.WriteLine("Error: cmbStatus field in Driver.cs is null.");
                }
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