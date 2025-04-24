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
        private string connectionString =
            @"Data Source=KAMILIA\KAMILIANURMALA;Initial Catalog=BiroWisata;Integrated Security=True;";
        private int selectedDriverId = -1; // Untuk menyimpan ID driver terpilih

        public Driver()
        {
            InitializeComponent();
            InitializeDataGridView(); // Panggil inisialisasi grid DULU
            // Jangan panggil RefreshData di sini, biarkan Load event
        }

        // --- Ganti Nama Method agar Cocok dengan Designer ---
        private void Driver_Load_1(object sender, EventArgs e)
        {
            RefreshData(); // Muat data saat form load
        }

        // --- Perbaiki Inisialisasi DataGridView ---
        private void InitializeDataGridView()
        {
            dgvDriver.AutoGenerateColumns = false; // Set ke false untuk kolom manual
            dgvDriver.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDriver.MultiSelect = false;
            dgvDriver.ReadOnly = true;
            dgvDriver.AllowUserToAddRows = false;
            dgvDriver.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Atau AllCells
            dgvDriver.CellClick += DgvDriver_CellClick; // Gunakan CellClick

            // Definisikan Kolom Secara Manual
            dgvDriver.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IDDriver",
                HeaderText = "ID",
                DataPropertyName = "IDDriver", // Cocokkan dengan nama kolom dari SELECT
                Visible = false // Sembunyikan ID jika tak perlu
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

            // Hapus pengaturan Anchor, Location, Size dari sini
            // Biarkan Designer.cs yang mengatur layout awal
            // dgvDriver.Anchor = AnchorStyles.Top | AnchorStyles.Bottom |
            //        AnchorStyles.Left | AnchorStyles.Right;
            // dgvDriver.Location = new Point(10, 150);
            // dgvDriver.Size = new Size(this.ClientSize.Width - 20,
            //                         this.ClientSize.Height - 160);
        }

        private void RefreshData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    // Ambil Status juga jika perlu ditampilkan atau difilter
                    string query = "SELECT IDDriver, NamaDriver, NoTelp, NoSIM FROM Driver";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dgvDriver.DataSource = null;
                    dgvDriver.DataSource = dataTable;

                    // Pengaturan header sudah dilakukan di InitializeDataGridView saat AutoGenerateColumns=false
                    // if (dgvDriver.Columns.Count > 0 && dgvDriver.Columns.Contains("IDDriver")) ... (tidak perlu lagi)

                }
                catch (SqlException ex) // Lebih spesifik tangkap SqlException
                {
                    MessageBox.Show($"Error Database: {ex.Message}", "Database Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex) // Tangkap error umum lainnya
                {
                    MessageBox.Show($"Error: {ex.Message}", "General Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            ClearFields(); // Bersihkan input setelah refresh
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            if (!ValidateInputFields()) return; // Panggil validasi

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    // Tambahkan Status jika ada di tabel dan form Anda
                    string query = @"INSERT INTO Driver (NamaDriver, NoTelp, NoSIM)
                                   VALUES (@NamaDriver, @NoTelp, @NoSIM)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@NamaDriver", txtNama.Text.Trim());
                        cmd.Parameters.AddWithValue("@NoTelp", txtNoTel.Text.Trim());
                        cmd.Parameters.AddWithValue("@NoSIM", txtNoSim.Text.Trim());
                        // cmd.Parameters.AddWithValue("@Status", cmbStatus.SelectedItem.ToString()); // Jika ada ComboBox Status

                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("Data driver berhasil ditambahkan!", "Sukses",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            RefreshData(); // Refresh data & clear fields
                        }
                        else
                        {
                            MessageBox.Show("Gagal menambahkan data.", "Gagal",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Error Database: {ex.Message}\nNomor: {ex.Number}", "Database Error",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Tambahkan penanganan error spesifik jika perlu (misal UNIQUE constraint)
                    // if (ex.Number == 2627) { // Unique key violation }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "General Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // --- Sederhanakan Hapus (Tanpa Reseed) ---
        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (selectedDriverId < 0) // Gunakan ID terpilih
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
                        string deleteQuery = "DELETE FROM Driver WHERE IDDriver = @IDDriver";
                        using (SqlCommand cmdDelete = new SqlCommand(deleteQuery, conn))
                        {
                            cmdDelete.Parameters.AddWithValue("@IDDriver", selectedDriverId);
                            int result = cmdDelete.ExecuteNonQuery();

                            if (result > 0)
                            {
                                MessageBox.Show("Data driver berhasil dihapus!", "Sukses",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                RefreshData(); // Refresh data & clear fields
                            }
                            else
                            {
                                MessageBox.Show("Gagal menghapus data (mungkin sudah dihapus).", "Gagal",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                    catch (SqlException ex) // Tangani potensi error FK jika Driver dirujuk tabel lain
                    {
                        if (ex.Number == 547) // Foreign Key constraint violation
                        {
                            MessageBox.Show($"Error: Tidak dapat menghapus driver ini karena masih dirujuk oleh data lain (misalnya Paket Wisata atau Operasional).\nDetail: {ex.Message}", "Error Relasi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btnUbah_Click(object sender, EventArgs e)
        {
            if (selectedDriverId < 0) // Gunakan ID terpilih
            {
                MessageBox.Show("Pilih baris driver yang ingin diubah!", "Peringatan",
                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInputFields()) return; // Panggil validasi

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    // Tambahkan SET Status = @Status jika perlu
                    string query = @"UPDATE Driver
                                   SET NamaDriver = @Nama,
                                       NoTelp = @NoTelp,
                                       NoSIM = @NoSIM
                                   WHERE IDDriver = @Id";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", selectedDriverId); // ID dari baris terpilih
                        cmd.Parameters.AddWithValue("@Nama", txtNama.Text.Trim());
                        cmd.Parameters.AddWithValue("@NoTelp", txtNoTel.Text.Trim());
                        cmd.Parameters.AddWithValue("@NoSIM", txtNoSim.Text.Trim());
                        // cmd.Parameters.AddWithValue("@Status", cmbStatus.SelectedItem.ToString()); // Jika ada

                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("Data driver berhasil diubah!", "Sukses",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            RefreshData(); // Refresh data & clear fields
                        }
                        else
                        {
                            MessageBox.Show("Data tidak berubah atau tidak ditemukan.", "Informasi",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshData();
            MessageBox.Show("Data telah dimuat ulang.", "Refresh Selesai", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ClearFields()
        {
            txtNama.Clear();
            txtNoTel.Clear();
            txtNoSim.Clear();
            // cmbStatus.SelectedIndex = 0; // Jika ada ComboBox Status
            selectedDriverId = -1; // Reset ID terpilih
            dgvDriver.ClearSelection(); // Hapus seleksi di grid
            txtNama.Focus(); // Fokus ke field pertama
        }

        // --- Tambahkan Validasi ---
        private bool ValidateInputFields()
        {
            // Validasi field tidak boleh kosong
            if (string.IsNullOrWhiteSpace(txtNama.Text))
            { MessageBox.Show("Nama Driver tidak boleh kosong!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtNama.Focus(); return false; }
            if (string.IsNullOrWhiteSpace(txtNoTel.Text))
            { MessageBox.Show("Nomor Telepon tidak boleh kosong!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtNoTel.Focus(); return false; }
            if (string.IsNullOrWhiteSpace(txtNoSim.Text))
            { MessageBox.Show("Nomor SIM tidak boleh kosong!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtNoSim.Focus(); return false; }
            // if (cmbStatus.SelectedIndex <= 0) // Jika pakai ComboBox Status
            // { MessageBox.Show("Pilih Status Driver!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning); cmbStatus.Focus(); return false; }


            // Validasi format No Telepon
            string noTelp = txtNoTel.Text.Trim();
            if (!noTelp.StartsWith("08") || noTelp.Length < 10 || noTelp.Length > 13 || !noTelp.All(char.IsDigit))
            {
                MessageBox.Show("Nomor telepon harus dimulai dengan '08', panjang 10-13 digit, dan hanya berisi angka!",
                    "Format Salah", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNoTel.Focus();
                return false;
            }

            // Validasi format No SIM
            string noSim = txtNoSim.Text.Trim();
            if (noSim.Length != 14 || !noSim.All(char.IsDigit)) // Asumsi SIM C Indonesia 14 digit angka
            {
                MessageBox.Show("Nomor SIM harus 14 digit angka!",
                    "Format Salah", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNoSim.Focus();
                return false;
            }

            return true; // Jika semua valid
        }


        // --- Tambahkan Event Handler CellClick ---
        private void DgvDriver_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvDriver.Rows.Count)
            {
                DataGridViewRow row = dgvDriver.Rows[e.RowIndex];

                // Ambil ID Driver
                if (row.Cells["IDDriver"].Value != null && int.TryParse(row.Cells["IDDriver"].Value.ToString(), out int id))
                {
                    selectedDriverId = id;
                }
                else
                {
                    selectedDriverId = -1; // Gagal ambil ID, reset
                    ClearFields(); // Kosongkan field jika ID tidak valid
                    return;
                }

                // Isi TextBox dari data grid (handle null dengan ?.)
                txtNama.Text = row.Cells["NamaDriver"].Value?.ToString();
                txtNoTel.Text = row.Cells["NoTelp"].Value?.ToString();
                txtNoSim.Text = row.Cells["NoSIM"].Value?.ToString();
                // string status = row.Cells["Status"].Value?.ToString(); // Jika ada kolom status
                // cmbStatus.SelectedItem = status; // Jika pakai ComboBox Status
            }
            else
            {
                // Klik di header atau area kosong, bersihkan input
                ClearFields();
            }
        }


    }
}