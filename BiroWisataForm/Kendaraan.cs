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
        //private string connectionString =
        //    //@"Data Source=MSI;Initial Catalog=BiroWisata;Integrated Security=True";
        private string connectionString = @"Data Source=KAMILIA\KAMILIANURMALA;Initial Catalog=BiroWisata;Integrated Security=True;";

        public Kendaraan()
        {
            InitializeComponent();
            InitializeDataGridViewSettings();
            InitializeSearchBox();
            InitializeStatusComboBox(); // Tambahkan ini
        }

        private void Kendaraan_Load(object sender, EventArgs e)
        {
            RefreshData();
            // Ensure SelectionChanged is connected in designer, or do it here:
            // this.dgvKendaraan.SelectionChanged += new System.EventHandler(this.dgvKendaraan_SelectionChanged);
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
                if(cmb.Items.Count > 0) cmb.SelectedIndex = 0;
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
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT IDKendaraan, Jenis, PlatNomor, Kapasitas, Status FROM Kendaraan WHERE IsDeleted = 0";
                    
                    if(!string.IsNullOrWhiteSpace(searchTerm))
                    {
                        query += " AND (Jenis LIKE @SearchTerm OR PlatNomor LIKE @SearchTerm OR Status LIKE @SearchTerm)";
                    }
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if(!string.IsNullOrWhiteSpace(searchTerm))
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
            ClearFields();
        }

        private bool ApakahPlatNomorSudahAda(string platNomor, int idKendaraanToExclude = 0)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
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

        private bool ValidateInput()
        {
            this.errorProvider1.Clear();
            bool isValid = true;

            if (string.IsNullOrWhiteSpace(txtJenis.Text))
            {
                this.errorProvider1.SetError(txtJenis, "Jenis kendaraan harus diisi!");
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(txtPlatNomor.Text))
            {
                this.errorProvider1.SetError(txtPlatNomor, "Plat nomor harus diisi!");
                isValid = false;
            }
            else if (txtPlatNomor.Text.Trim().Length < 5 || txtPlatNomor.Text.Trim().Length > 12)
            {
                this.errorProvider1.SetError(txtPlatNomor, "Plat nomor harus antara 5 dan 12 karakter!");
                isValid = false;
            }

            // --- CORRECTED KAPASITAS VALIDATION ---
            if (string.IsNullOrWhiteSpace(txtKapasitas.Text))
            {
                this.errorProvider1.SetError(txtKapasitas, "Kapasitas harus diisi!");
                isValid = false;
            }
            else if (!short.TryParse(txtKapasitas.Text, out short kapasitasVal) || kapasitasVal <= 0)
            {
                // This covers two cases:
                // 1. Not a valid short number (e.g., text, too large/small for short)
                // 2. A valid short number, but it's not positive (e.g., 0 or -5)
                this.errorProvider1.SetError(txtKapasitas, "Kapasitas harus berupa angka positif (bilangan bulat kecil)!");
                isValid = false;
            }
            // --- END OF CORRECTED KAPASITAS VALIDATION ---

            if (this.Controls.Find("cmbStatus", true).FirstOrDefault() is ComboBox cmb && cmb.SelectedIndex < 0)
            {
                // It's better to check if SelectedIndex is the placeholder index (usually 0 if you have one)
                // or if SelectedItem is null, depending on how you set up your placeholder.
                // For a DropDownList, SelectedIndex < 0 is unlikely if it has items and one is selected.
                // If your placeholder is the first item (index 0):
                // if (this.Controls.Find("cmbStatus", true).FirstOrDefault() is ComboBox cmb && cmb.SelectedIndex == 0 && cmb.Items[0].ToString().StartsWith("--"))
                // Or more simply if no placeholder:
                if (this.Controls.Find("cmbStatus", true).FirstOrDefault() is ComboBox cmbStatusControl && cmbStatusControl.SelectedItem == null)
                {
                    this.errorProvider1.SetError(cmbStatusControl, "Status harus dipilih!");
                    isValid = false;
                }
                // However, since your InitializeStatusComboBox always selects an item if count > 0,
                // this check might only be relevant if cmbStatus is somehow cleared or fails to initialize.
                // A simpler check if you ensure it's always populated and selected:
                // if (this.Controls.Find("cmbStatus", true).FirstOrDefault() is ComboBox cmb && string.IsNullOrEmpty(cmb.SelectedItem?.ToString()))
                // {
                //     this.errorProvider1.SetError(cmb, "Status harus dipilih!"); 
                //     isValid = false; 
                // }
            }

            return isValid;
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            // 1. Validasi semua input terlebih dahulu
            if (!ValidateInput())
            {
                MessageBox.Show("Harap perbaiki kesalahan input sebelum menyimpan.", "Validasi Gagal",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Hentikan eksekusi jika validasi dasar gagal
            }

            // 2. Lakukan pengecekan duplikasi Plat Nomor
            string platNomor = txtPlatNomor.Text.Trim();
            if (ApakahPlatNomorSudahAda(platNomor))
            {
                MessageBox.Show("Plat nomor yang sama sudah terdaftar. Silakan gunakan plat nomor yang lain.",
                                "Duplikasi Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                errorProvider1.SetError(txtPlatNomor, "Plat nomor ini sudah ada.");
                return; // Hentikan eksekusi jika plat nomor sudah ada
            }

            // 3. Siapkan data lainnya
            // Parsing kapasitas, validasi ini sebenarnya sudah ada di ValidateInput, 
            // namun kita lakukan lagi untuk keamanan dan untuk mendapatkan nilai 'kapasitasVal'.
            if (!short.TryParse(txtKapasitas.Text, out short kapasitasVal) || kapasitasVal <= 0)
            {
                errorProvider1.SetError(txtKapasitas, "Kapasitas harus berupa angka positif yang valid.");
                MessageBox.Show("Kapasitas tidak valid. Harap masukkan angka positif.", "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string status = (this.Controls.Find("cmbStatus", true).FirstOrDefault() as ComboBox)?.SelectedItem.ToString() ?? "Aktif";


            // 4. Proses penyimpanan data ke database
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    // Menggunakan Stored Procedure 'sp_AddKendaraan'
                    using (SqlCommand cmd = new SqlCommand("sp_AddKendaraan", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Tambahkan parameter ke Stored Procedure
                        cmd.Parameters.AddWithValue("@Jenis", txtJenis.Text.Trim());
                        cmd.Parameters.AddWithValue("@PlatNomor", platNomor); // Gunakan variabel yang sudah di-trim
                        cmd.Parameters.AddWithValue("@Kapasitas", kapasitasVal);
                        cmd.Parameters.AddWithValue("@Status", status);
                        cmd.Parameters.AddWithValue("@CreatedBy", Environment.UserName); // Mencatat siapa yang membuat data

                        cmd.ExecuteNonQuery(); // Eksekusi perintah

                        MessageBox.Show("Data kendaraan berhasil ditambahkan!", "Sukses",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        RefreshData(); // Muat ulang data di tabel untuk menampilkan data baru
                    }
                }
                catch (SqlException ex) // Menangkap error spesifik dari SQL Server
                {
                    MessageBox.Show($"Error Database saat menambahkan data: {ex.Message}\nNomor Kesalahan: {ex.Number}", "Database Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex) // Menangkap error umum lainnya
                {
                    MessageBox.Show($"Error saat menambahkan data: {ex.Message}", "Error Umum",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                using (SqlConnection conn = new SqlConnection(connectionString))
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
            // 1. Pastikan ada baris data yang dipilih di tabel
            if (dgvKendaraan.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih kendaraan yang akan diubah.", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Hentikan jika tidak ada yang dipilih
            }

            // 2. Lakukan validasi input pada form
            if (!ValidateInput())
            {
                MessageBox.Show("Harap perbaiki kesalahan input sebelum menyimpan.", "Validasi Gagal",
                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Hentikan jika validasi gagal
            }

            // 3. Ambil ID dari baris yang dipilih dan data dari form
            int selectedId = Convert.ToInt32(dgvKendaraan.SelectedRows[0].Cells["colIDKendaraan"].Value);
            string platNomor = txtPlatNomor.Text.Trim();

            // 4. Cek duplikasi plat nomor, tapi KECUALIKAN data yang sedang kita edit.
            // Ini penting agar plat nomor yang tidak diubah tidak dianggap duplikat.
            if (ApakahPlatNomorSudahAda(platNomor, selectedId))
            {
                MessageBox.Show("Plat nomor yang sama sudah terdaftar untuk kendaraan lain. Silakan gunakan plat nomor yang lain.",
                                "Duplikasi Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                errorProvider1.SetError(txtPlatNomor, "Plat nomor ini sudah digunakan kendaraan lain.");
                return; // Hentikan jika duplikat ditemukan pada kendaraan lain
            }

            // 5. Siapkan data lainnya
            if (!short.TryParse(txtKapasitas.Text, out short kapasitasVal) || kapasitasVal <= 0)
            {
                errorProvider1.SetError(txtKapasitas, "Kapasitas harus berupa angka positif yang valid.");
                MessageBox.Show("Kapasitas tidak valid. Harap masukkan angka positif.", "Tipe Data Salah",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string status = (this.Controls.Find("cmbStatus", true).FirstOrDefault() as ComboBox)?.SelectedItem.ToString() ?? "Aktif";

            // 6. Jalankan proses update ke database
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    // Menggunakan Stored Procedure 'sp_UpdateKendaraan'
                    using (SqlCommand cmd = new SqlCommand("sp_UpdateKendaraan", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Tambahkan semua parameter, yang paling penting adalah @IDKendaraan
                        // untuk memastikan baris yang benar yang di-update.
                        cmd.Parameters.AddWithValue("@IDKendaraan", selectedId);
                        cmd.Parameters.AddWithValue("@Jenis", txtJenis.Text.Trim());
                        cmd.Parameters.AddWithValue("@PlatNomor", platNomor);
                        cmd.Parameters.AddWithValue("@Kapasitas", kapasitasVal);
                        cmd.Parameters.AddWithValue("@Status", status);
                        cmd.Parameters.AddWithValue("@UpdatedBy", Environment.UserName); // Mencatat siapa yang mengubah data

                        cmd.ExecuteNonQuery(); // Eksekusi perintah update

                        MessageBox.Show("Data kendaraan berhasil diubah.", "Sukses",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        RefreshData(); // Muat ulang data untuk menampilkan perubahan
                    }
                }
                catch (SqlException ex) // Tangkap error spesifik dari SQL
                {
                    MessageBox.Show($"Error Database saat mengubah data: {ex.Message}\nNomor Kesalahan: {ex.Number}", "Database Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex) // Tangkap error umum lainnya
                {
                    MessageBox.Show($"Error saat mengubah data: {ex.Message}", "Error Umum",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (this.Controls.Find("txtSearch", true).FirstOrDefault() is TextBox txtSearch)
            {
                txtSearch.Clear();
            }
            RefreshData();
            MessageBox.Show("Data berhasil dimuat ulang.", "Refresh", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ClearFields()
        {
            txtJenis.Clear();
            txtPlatNomor.Clear();
            txtKapasitas.Clear();
            if (this.Controls.Find("cmbStatus", true).FirstOrDefault() is ComboBox cmb && cmb.Items.Count > 0) cmb.SelectedIndex = 0;
            dgvKendaraan.ClearSelection();
            this.errorProvider1.Clear();
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
    }
}