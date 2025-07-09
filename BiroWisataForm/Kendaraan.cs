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

        private void Kendaraan_Load(object sender, EventArgs e)
        {
            RefreshData();
            ClearFields();
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

        private bool ValidateInput()
        {
            this.errorProvider1.Clear();
            bool isValid = true;

            if (string.IsNullOrWhiteSpace(txtJenis.Text))
            {
                this.errorProvider1.SetError(txtJenis, "Jenis kendaraan harus diisi!");
                isValid = false;
            }
            // --- VALIDASI: JENIS KENDARAAN TIDAK BOLEH MENGANDUNG ANGKA ---
            else if (System.Text.RegularExpressions.Regex.IsMatch(txtJenis.Text, @"\d"))
            {
                this.errorProvider1.SetError(txtJenis, "Jenis kendaraan tidak boleh mengandung angka!");
                isValid = false;
            }
            // --- VALIDASI: HANYA HURUF, SPASI, DAN BEBERAPA KARAKTER KHUSUS ---
            else if (!System.Text.RegularExpressions.Regex.IsMatch(txtJenis.Text, @"^[a-zA-Z\s\-\.]+$"))
            {
                this.errorProvider1.SetError(txtJenis, "Jenis kendaraan hanya boleh berisi huruf, spasi, tanda hubung (-), dan titik (.)!");
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
                this.errorProvider1.SetError(txtKapasitas, "Kapasitas harus berupa angka positif (bilangan bulat kecil)!");
                isValid = false;
            }

            // --- STATUS VALIDATION ---
            if (this.Controls.Find("cmbStatus", true).FirstOrDefault() is ComboBox cmbStatusControl && cmbStatusControl.SelectedItem == null)
            {
                this.errorProvider1.SetError(cmbStatusControl, "Status harus dipilih!");
                isValid = false;
            }

            return isValid;
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            // 1. Validasi semua input terlebih dahulu
            if (!ValidateInput())
            {
                MessageBox.Show("Harap perbaiki kesalahan input yang ditandai dengan tanda seru merah!",
                                "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Lakukan pengecekan duplikasi Plat Nomor
            string platNomor = txtPlatNomor.Text.Trim();
            if (ApakahPlatNomorSudahAda(platNomor))
            {
                MessageBox.Show("Plat nomor yang sama sudah terdaftar. Silakan gunakan plat nomor yang lain.",
                                "Duplikasi Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                errorProvider1.SetError(txtPlatNomor, "Plat nomor ini sudah ada.");
                return;
            }

            // 3. Siapkan data (validasi sudah dilakukan di ValidateInput)
            short kapasitasVal = short.Parse(txtKapasitas.Text); // Aman karena sudah divalidasi
            string status = (this.Controls.Find("cmbStatus", true).FirstOrDefault() as ComboBox)?.SelectedItem.ToString() ?? "Aktif";

            // 4. Proses penyimpanan data ke database dengan transaksi
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
                        MessageBox.Show($"Error Database saat menambahkan data: {sqlEx.Message}\nNomor Kesalahan: {sqlEx.Number}", "Database Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show($"Error saat menambahkan data: {ex.Message}", "Error Umum",
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

            // 2. Lakukan validasi input
            if (!ValidateInput())
            {
                MessageBox.Show("Harap perbaiki kesalahan input yang ditandai dengan tanda seru merah!",
                                "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. Ambil ID dari baris yang dipilih dan data dari form
            int selectedId = Convert.ToInt32(dgvKendaraan.SelectedRows[0].Cells["colIDKendaraan"].Value);
            string platNomor = txtPlatNomor.Text.Trim();

            // 4. Cek duplikasi plat nomor (kecuali data itu sendiri)
            if (ApakahPlatNomorSudahAda(platNomor, selectedId))
            {
                MessageBox.Show("Plat nomor yang sama sudah terdaftar untuk kendaraan lain.",
                                "Duplikasi Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                errorProvider1.SetError(txtPlatNomor, "Plat nomor ini sudah digunakan kendaraan lain.");
                return;
            }

            // 5. BARU: Cek apakah ada perubahan data
            DataGridViewRow selectedRow = dgvKendaraan.SelectedRows[0];
            string jenisLama = selectedRow.Cells["colJenis"].Value?.ToString() ?? "";
            string platLama = selectedRow.Cells["colPlatNomor"].Value?.ToString() ?? "";
            string kapasitasLama = selectedRow.Cells["colKapasitas"].Value?.ToString() ?? "";
            string statusLama = selectedRow.Cells["colStatus"].Value?.ToString() ?? "";

            string jenisBaru = txtJenis.Text.Trim();
            string platBaru = txtPlatNomor.Text.Trim();
            string kapasitasBaru = txtKapasitas.Text.Trim();
            string statusBaru = (this.Controls.Find("cmbStatus", true).FirstOrDefault() as ComboBox)?.SelectedItem?.ToString() ?? "Aktif";

            // Bandingkan data lama dengan data baru
            if (jenisLama == jenisBaru &&
                platLama == platBaru &&
                kapasitasLama == kapasitasBaru &&
                statusLama == statusBaru)
            {
                MessageBox.Show("Tidak ada perubahan data yang terdeteksi. Silakan ubah minimal satu field jika ingin menyimpan perubahan.",
                                "Tidak Ada Perubahan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 6. Siapkan data (validasi sudah dilakukan di ValidateInput)
            short kapasitasVal = short.Parse(txtKapasitas.Text); // Aman karena sudah divalidasi

            // 7. Jalankan proses update ke database dengan transaksi
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
                        cmd.Parameters.AddWithValue("@Jenis", jenisBaru);
                        cmd.Parameters.AddWithValue("@PlatNomor", platBaru);
                        cmd.Parameters.AddWithValue("@Kapasitas", kapasitasVal);
                        cmd.Parameters.AddWithValue("@Status", statusBaru);
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

        private void dgvKendaraan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}