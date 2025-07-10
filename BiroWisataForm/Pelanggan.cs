using praktikum7;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing; // For Color, Font
using System.Linq;    // For .All() in validation
using System.Net.Mail; // For MailAddress in validation
using System.Windows.Forms;

namespace BiroWisataForm
{
    public partial class Pelanggan : Form
    {
        Koneksi kn = new Koneksi();
        private string connectionString = "";

        // We will use errorProvider1 directly as it's declared in Pelanggan.Designer.cs
        // No need to declare a separate 'errorProvider' field here.

        // This field is optional. If you need to track the ID of the selected row for some specific
        // logic outside of just populating textboxes, you can use it.
        // For basic CRUD, getting it from dgvPelanggan.SelectedRows[0] when needed is often enough.
        // private int selectedPelangganId = -1;

        public Pelanggan()
        {
            connectionString = kn.connectionString();

            InitializeComponent(); // This initializes txtSearch and errorProvider1 from the designer
            InitializeDataGridViewSettings(); // Configure DGV properties
            InitializeSearchBox();         // Setup search functionality
            EnsureDatabaseIndexes();
        }


        private void EnsureDatabaseIndexes()
        {
            // Gabungkan script SQL Anda ke dalam satu string.
            // Gunakan GO sebagai pemisah logis, tetapi dalam C# kita eksekusi per-blok.
            string scriptIndex = @"
        IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_Pelanggan_IsDeleted' AND object_id = OBJECT_ID('dbo.Pelanggan'))
        BEGIN
            CREATE NONCLUSTERED INDEX IX_Pelanggan_IsDeleted ON dbo.Pelanggan(IsDeleted);
        END;

        IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_Pelanggan_NamaPelanggan' AND object_id = OBJECT_ID('dbo.Pelanggan'))
        BEGIN
            CREATE NONCLUSTERED INDEX IX_Pelanggan_NamaPelanggan ON dbo.Pelanggan(NamaPelanggan);
        END;
    ";

            try
            {
                using (SqlConnection conn = new SqlConnection(kn.connectionString()))
                {
                    conn.Open();
                    // Karena tidak ada 'GO', kita bisa eksekusi sebagai satu blok besar
                    using (SqlCommand cmd = new SqlCommand(scriptIndex, conn))
                    {
                        cmd.ExecuteNonQuery();
                        // Anda bisa menambahkan log atau notifikasi di sini jika perlu,
                        // tapi biasanya tidak ditampilkan ke user.
                        Console.WriteLine("Pengecekan indeks selesai.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Tampilkan pesan error jika GAGAL membuat indeks, karena ini masalah kritis.
                MessageBox.Show($"Gagal memverifikasi/membuat indeks database: {ex.Message}",
                                "Kesalahan Konfigurasi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Ganti metode ini di file Pelanggan.cs
        private void Pelanggan_Load(object sender, EventArgs e)
        {
            // Langkah 1: Muat data seperti biasa
            RefreshData();

            // Langkah 2: Panggil metode untuk mengosongkan semua field
            ClearFields();
        }

        private void InitializeDataGridViewSettings()
        {
            // Since columns are defined in the designer, ensure AutoGenerateColumns is false.
            dgvPelanggan.AutoGenerateColumns = false;

            dgvPelanggan.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPelanggan.MultiSelect = false;
            dgvPelanggan.ReadOnly = true; // Grid is for display; editing is via TextBoxes
            dgvPelanggan.AllowUserToAddRows = false;
            dgvPelanggan.AllowUserToDeleteRows = false; // Prevent accidental deletion from grid
            dgvPelanggan.RowHeadersVisible = false; // Cleaner look

            // Optional: Additional styling if not set in Designer
            dgvPelanggan.BackgroundColor = Color.White;
            dgvPelanggan.BorderStyle = BorderStyle.Fixed3D;
            dgvPelanggan.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);
            dgvPelanggan.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvPelanggan.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvPelanggan.EnableHeadersVisualStyles = false;
            dgvPelanggan.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219);
            dgvPelanggan.DefaultCellStyle.SelectionForeColor = Color.White;
            // dgvPelanggan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Or your preferred mode
        }

        private void InitializeSearchBox()
        {
            // The TextChanged event will now trigger a database query
            this.txtSearch.TextChanged += (s, e) =>
            {
                // Pass the search text to the data refresh method
                RefreshData(this.txtSearch.Text);
            };
        }

        private bool RefreshData(string searchTerm = null)
        {
            using (SqlConnection conn = new SqlConnection(kn.connectionString()))
            {
                try
                {
                    conn.Open();
                    // Base query selects non-deleted records
                    string query = "SELECT IDPelanggan, NamaPelanggan, Alamat, NoTelp, Email, CreatedAt, UpdatedAt FROM Pelanggan WHERE IsDeleted = 0";

                    // Dynamically add search conditions if a search term is provided
                    if (!string.IsNullOrWhiteSpace(searchTerm))
                    {
                        query += " AND (NamaPelanggan LIKE @SearchTerm OR Alamat LIKE @SearchTerm OR NoTelp LIKE @SearchTerm OR Email LIKE @SearchTerm)";
                    }

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add the search parameter only if it's needed
                        if (!string.IsNullOrWhiteSpace(searchTerm))
                        {
                            cmd.Parameters.AddWithValue("@SearchTerm", $"%{searchTerm}%");
                        }

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            dgvPelanggan.DataSource = dt;
                        }
                    }

                    // =================================================================
                    // --- TAMBAHAN BARU: OTOMATIS PILIH JIKA HASIL HANYA SATU ---
                    // =================================================================
                    // Setelah data dimuat ke grid, periksa jumlah barisnya.
                    if (dgvPelanggan.Rows.Count == 1)
                    {
                        // Jika hanya ada satu baris hasil pencarian,
                        // pilih baris tersebut secara otomatis.
                        dgvPelanggan.Rows[0].Selected = true;

                        // Dengan mengatur .Selected = true, event 'dgvPelanggan_SelectionChanged'
                        // akan terpicu secara otomatis, sehingga data langsung ditampilkan di textbox.
                    }
                    // Jika hasil lebih dari satu atau nol, biarkan pengguna memilih sendiri.
                    // =================================================================

                    // Jangan panggil ClearFields() di sini lagi agar data yang terpilih tidak langsung hilang
                    // ClearFields(); 

                    return true; // <-- KEMBALIKAN TRUE JIKA SUKSES
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading data: {ex.Message}", "Database Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false; // <-- KEMBALIKAN FALSE JIKA GAGAL
                }
            }
        }

        // Ganti metode ini di file Pelanggan.cs
        private bool IsDataDuplikat(string noTelp, string email, string alamat, int idPelangganToExclude = 0)
        {
            using (SqlConnection conn = new SqlConnection(kn.connectionString()))
            {
                try
                {
                    conn.Open();
                    // Query diubah untuk memeriksa NoTelp, Email, ATAU Alamat
                    string query = @"
                SELECT COUNT(1) FROM Pelanggan 
                WHERE (NoTelp = @NoTelp OR Email = @Email OR Alamat = @Alamat) 
                  AND IsDeleted = 0";

                    if (idPelangganToExclude > 0)
                    {
                        query += " AND IDPelanggan != @IDPelanggan";
                    }

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@NoTelp", noTelp);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Alamat", alamat); // Tambahkan parameter alamat

                        if (idPelangganToExclude > 0)
                        {
                            cmd.Parameters.AddWithValue("@IDPelanggan", idPelangganToExclude);
                        }

                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Terjadi kesalahan saat memeriksa duplikasi data: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return true;
                }
            }
        }

        // GANTI METODE INI DI FILE Pelanggan.cs
        // Ganti metode ini di file Pelanggan.cs
        // Ganti metode ini di file Pelanggan.cs
        // Ganti metode ini di file Pelanggan.cs
        // Ganti metode ini di file Pelanggan.cs
        private bool ValidateInput()
        {
            bool isValid = true;
            string errorMsg = "";

            // --- Validasi Nama Pelanggan ---
            string nama = txtNama.Text;
            if (string.IsNullOrWhiteSpace(nama))
            {
                errorMsg += "- Nama pelanggan harus diisi.\n";
                isValid = false;
            }
            else
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(nama, @"^[a-zA-Z0-9\s\.]+$"))
                {
                    errorMsg += "- Nama hanya boleh berisi huruf, angka, spasi, dan titik.\n";
                    isValid = false;
                }
                if (!nama.Any(char.IsLetter))
                {
                    errorMsg += "- Nama pelanggan harus mengandung setidaknya satu huruf.\n";
                    isValid = false;
                }
            }

            // --- PERUBAHAN VALIDASI ALAMAT DI SINI ---
            if (string.IsNullOrWhiteSpace(txtAlamat.Text))
            {
                errorMsg += "- Alamat pelanggan harus diisi.\n";
                isValid = false;
            }
            // Cek apakah input hanya berisi huruf, angka, spasi, titik, dan koma
            else if (!System.Text.RegularExpressions.Regex.IsMatch(txtAlamat.Text, @"^[a-zA-Z0-9\s\.,]+$"))
            {
                errorMsg += "- Alamat hanya boleh berisi huruf, angka, spasi, titik (.), dan koma (,).\n";
                isValid = false;
            }
            // --- AKHIR PERUBAHAN ---

            // --- Validasi Nomor Telepon ---
            string phoneNumber = txtNoTelp.Text.Trim();
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                errorMsg += "- Nomor telepon harus diisi.\n";
                isValid = false;
            }
            else if (!phoneNumber.StartsWith("08") || phoneNumber.Length < 10 ||
                     phoneNumber.Length > 13 || !phoneNumber.All(char.IsDigit))
            {
                errorMsg += "- Nomor telepon harus dimulai dengan '08' dan berisi 10-13 digit angka.\n";
                isValid = false;
            }

            // --- Validasi Email ---
            string email = txtEmail.Text.Trim();
            if (string.IsNullOrWhiteSpace(email))
            {
                errorMsg += "- Email harus diisi.\n";
                isValid = false;
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(email, @"^[a-zA-Z][a-zA-Z0-9.]*@[a-zA-Z0-9]+\.(?:com|co\.id)$"))
            {
                errorMsg += "- Format email tidak valid. Hanya boleh diawali huruf dulu baru angka, dan hanya bisa @domain.com atau .co.id, boleh menggunakan (.).\n";
                isValid = false;
            }

            // Jika ada satu atau lebih kesalahan, tampilkan semua dalam satu pesan
            if (!isValid)
            {
                MessageBox.Show("Harap perbaiki input yang tidak valid:\n\n" + errorMsg, "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return isValid;
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            // 1. Validasi format semua input.
            if (!ValidateInput())
            {
                // Pesan error sudah ditampilkan dari dalam ValidateInput.
                return;
            }

            string nama = txtNama.Text.Trim();
            string alamat = txtAlamat.Text.Trim();
            string noTelp = txtNoTelp.Text.Trim();
            string email = txtEmail.Text.Trim();

            // 2. Cek duplikasi untuk Alamat, No. Telp, dan Email.
            if (IsDataDuplikat(noTelp, email, alamat))
            {
                MessageBox.Show("Nomor Telepon, Email, atau Alamat sudah terdaftar.", "Data Duplikat",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. Lanjutkan proses penyimpanan ke database.
            using (SqlConnection conn = new SqlConnection(kn.connectionString()))
            {
                SqlTransaction transaction = null;
                try
                {
                    conn.Open();
                    transaction = conn.BeginTransaction();

                    using (SqlCommand cmd = new SqlCommand("sp_AddPelanggan", conn, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NamaPelanggan", nama);
                        cmd.Parameters.AddWithValue("@Alamat", alamat);
                        cmd.Parameters.AddWithValue("@NoTelp", noTelp);
                        cmd.Parameters.AddWithValue("@Email", email);

                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            transaction.Commit();
                            MessageBox.Show("Data pelanggan berhasil ditambahkan!", "Sukses",
                                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                            RefreshData();
                        }
                        else
                        {
                            throw new Exception("Gagal menambahkan data pelanggan ke database.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    transaction?.Rollback();
                    string title = (ex is SqlException) ? "SQL Error" : "Error";
                    MessageBox.Show(ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (dgvPelanggan.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("Apakah Anda yakin ingin menghapus data pelanggan ini?", "Konfirmasi Hapus",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // IMPORTANT: Replace "colIDPelanggan" with the Name you gave your ID column in the designer.
                    // If you haven't defined columns in the designer, and AutoGenerateColumns is true,
                    // it would be "IDPelanggan" (matching the database column name).
                    // Assuming you WILL define columns in the designer and name the ID column "colIDPelanggan".
                    int id;
                    if (dgvPelanggan.Columns.Contains("colIDPelanggan")) // Check if designer column exists
                    {
                        id = Convert.ToInt32(dgvPelanggan.SelectedRows[0].Cells["colIDPelanggan"].Value);
                    }
                    else if (dgvPelanggan.Columns.Contains("IDPelanggan")) // Fallback to DB column name if auto-generated
                    {
                        id = Convert.ToInt32(dgvPelanggan.SelectedRows[0].Cells["IDPelanggan"].Value);
                    }
                    else
                    {
                        MessageBox.Show("Kolom ID Pelanggan tidak ditemukan di DataGridView.", "Kesalahan Konfigurasi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    using (SqlConnection conn = new SqlConnection(kn.connectionString()))
                    {
                        try
                        {
                            conn.Open();
                            // Soft delete: Update IsDeleted flag and audit columns
                            string query = @"UPDATE Pelanggan 
                                           SET IsDeleted = 1, UpdatedAt = @UpdatedAt, UpdatedBy = @UpdatedBy 
                                           WHERE IDPelanggan = @IDPelanggan";
                            using (SqlCommand cmd = new SqlCommand(query, conn))
                            {
                                cmd.Parameters.AddWithValue("@IDPelanggan", id);
                                cmd.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);
                                cmd.Parameters.AddWithValue("@UpdatedBy", Environment.UserName); // Or logged-in user

                                int result = cmd.ExecuteNonQuery();
                                if (result > 0)
                                {
                                    MessageBox.Show("Data pelanggan berhasil dihapus.", "Sukses",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    RefreshData(); // This also calls ClearFields()
                                }
                                else
                                {
                                    MessageBox.Show("Data tidak ditemukan atau tidak dapat dihapus.", "Gagal",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error: {ex.Message}", "Database Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Pilih baris data pelanggan yang ingin dihapus!", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // GANTI METODE INI DI FILE Pelanggan.cs
        private void btnUbah_Click(object sender, EventArgs e)
        {
            // 1. Pastikan ada baris yang dipilih.
            if (dgvPelanggan.SelectedRows.Count == 0 || dgvPelanggan.SelectedRows[0].Cells["colIDPelanggan"].Value == null)
            {
                MessageBox.Show("Pilih pelanggan yang akan diubah!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Cek apakah ada perubahan data.
            DataGridViewRow selectedRow = dgvPelanggan.SelectedRows[0];
            bool dataTelahBerubah =
                selectedRow.Cells["colNamaPelanggan"].Value.ToString().Trim() != txtNama.Text.Trim() ||
                selectedRow.Cells["colAlamat"].Value.ToString().Trim() != txtAlamat.Text.Trim() ||
                selectedRow.Cells["colNoTelp"].Value.ToString().Trim() != txtNoTelp.Text.Trim() ||
                selectedRow.Cells["colEmail"].Value.ToString().Trim() != txtEmail.Text.Trim();

            if (!dataTelahBerubah)
            {
                MessageBox.Show("Tidak ada perubahan data yang dilakukan.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 3. Validasi format input jika ada perubahan.
            if (!ValidateInput())
            {
                return;
            }

            // 4. Cek duplikasi jika data unik diubah.
            int selectedId = Convert.ToInt32(selectedRow.Cells["colIDPelanggan"].Value);
            string noTelpBaru = txtNoTelp.Text.Trim();
            string emailBaru = txtEmail.Text.Trim();
            string alamatBaru = txtAlamat.Text.Trim();

            if (IsDataDuplikat(noTelpBaru, emailBaru, alamatBaru, selectedId))
            {
                MessageBox.Show("Nomor Telepon, Email, atau Alamat sudah digunakan oleh pelanggan lain.", "Data Duplikat",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 5. Lanjutkan proses update.
            using (SqlConnection conn = new SqlConnection(kn.connectionString()))
            {
                SqlTransaction transaction = null;
                try
                {
                    conn.Open();
                    transaction = conn.BeginTransaction();
                    using (SqlCommand cmd = new SqlCommand("sp_UpdatePelanggan", conn, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IDPelanggan", selectedId);
                        cmd.Parameters.AddWithValue("@NamaPelanggan", txtNama.Text.Trim());
                        cmd.Parameters.AddWithValue("@Alamat", alamatBaru);
                        cmd.Parameters.AddWithValue("@NoTelp", noTelpBaru);
                        cmd.Parameters.AddWithValue("@Email", emailBaru);
                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            transaction.Commit();
                            MessageBox.Show("Data pelanggan berhasil diubah!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            RefreshData();
                        }
                        else
                        {
                            throw new Exception("Data pelanggan tidak ditemukan atau tidak ada perubahan data.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    transaction?.Rollback();
                    string title = (ex is SqlException) ? "SQL Error" : "Error";
                    MessageBox.Show(ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Ganti juga metode ini agar konsisten
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();

            // Panggil RefreshData dan simpan hasilnya
            bool isSuccess = RefreshData();

            // Jika refresh berhasil, bersihkan juga isian form
            if (isSuccess)
            {
                ClearFields(); // Tambahkan baris ini
                MessageBox.Show("Data berhasil di-refresh.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            // Jika gagal, pesan error sudah ditampilkan dari dalam method RefreshData
        }


        private void ClearFields()
        {
            txtNama.Clear();
            txtAlamat.Clear();
            txtNoTelp.Clear();
            txtEmail.Clear();
            // if (selectedPelangganId != -1) selectedPelangganId = -1; // Reset if you use this field
            dgvPelanggan.ClearSelection();
            txtNama.Focus(); // Set focus to the first input field
        }

        // This event is fired when a cell is clicked, or content within a cell is clicked.
        // For row selection, SelectionChanged is usually better.
        private void dgvPelanggan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // You might not need this if you use dgvPelanggan_SelectionChanged.
            // If you do use it, make sure e.RowIndex >= 0 to avoid header clicks.
        }

        // Add this event handler for when the selected row changes
        private void dgvPelanggan_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPelanggan.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvPelanggan.SelectedRows[0];

                // IMPORTANT: Replace these with the 'Name' of your columns from the designer.
                // Or, if AutoGenerateColumns is true (not recommended here), use database column names.
                // Example assuming you named your designer columns: colNamaPelanggan, colAlamat, etc.
                string namaColName = dgvPelanggan.Columns.Contains("colNamaPelanggan") ? "colNamaPelanggan" : "NamaPelanggan";
                string alamatColName = dgvPelanggan.Columns.Contains("colAlamat") ? "colAlamat" : "Alamat";
                string noTelpColName = dgvPelanggan.Columns.Contains("colNoTelp") ? "colNoTelp" : "NoTelp";
                string emailColName = dgvPelanggan.Columns.Contains("colEmail") ? "colEmail" : "Email";

                txtNama.Text = selectedRow.Cells[namaColName].Value?.ToString() ?? "";
                txtAlamat.Text = selectedRow.Cells[alamatColName].Value?.ToString() ?? "";
                txtNoTelp.Text = selectedRow.Cells[noTelpColName].Value?.ToString() ?? "";
                txtEmail.Text = selectedRow.Cells[emailColName].Value?.ToString() ?? "";

                // Optionally, store the ID if needed elsewhere
                // string idColName = dgvPelanggan.Columns.Contains("colIDPelanggan") ? "colIDPelanggan" : "IDPelanggan";
                // selectedPelangganId = Convert.ToInt32(selectedRow.Cells[idColName].Value);
            }
            else
            {
                // Optional: If no row is selected, you might want to clear fields
                // ClearFields(); // This might be too aggressive depending on UX preference.
            }
        }

        private void txtAlamat_TextChanged(object sender, EventArgs e)
        {

        }

        // Letakkan method ini bersama dengan event handler tombol lainnya (seperti btnTambah_Click)

        private void btnKembali_Click(object sender, EventArgs e)
        {
            // Perintah ini akan menutup form 'Kendaraan' saat ini,
            // dan mengembalikan kontrol ke form yang membukanya (yaitu MenuAdmin).
            this.Close();
        }

        private void dgvPelanggan_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}