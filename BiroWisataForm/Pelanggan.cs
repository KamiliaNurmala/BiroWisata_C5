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
        private string connectionString =
            @"Data Source=KAMILIA\KAMILIANURMALA;Initial Catalog=BiroWisata;Integrated Security=True;";

        // We will use errorProvider1 directly as it's declared in Pelanggan.Designer.cs
        // No need to declare a separate 'errorProvider' field here.

        // This field is optional. If you need to track the ID of the selected row for some specific
        // logic outside of just populating textboxes, you can use it.
        // For basic CRUD, getting it from dgvPelanggan.SelectedRows[0] when needed is often enough.
        // private int selectedPelangganId = -1;

        public Pelanggan()
        {
            InitializeComponent(); // This initializes txtSearch and errorProvider1 from the designer
            InitializeDataGridViewSettings(); // Configure DGV properties
            InitializeSearchBox();         // Setup search functionality
        }

        private void Pelanggan_Load(object sender, EventArgs e)
        {
            RefreshData();
            // Ensure the SelectionChanged event is connected.
            // If you haven't done it in the designer, you can do it here:
            // this.dgvPelanggan.SelectionChanged += new System.EventHandler(this.dgvPelanggan_SelectionChanged);
            // However, it's best practice to do it in the designer.
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
            using (SqlConnection conn = new SqlConnection(connectionString))
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
                    ClearFields();
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

        private bool ValidateInput()
        {
            // Use errorProvider1 (the one from the designer)
            errorProvider1.Clear();
            bool isValid = true;

            if (string.IsNullOrWhiteSpace(txtNama.Text))
            {
                errorProvider1.SetError(txtNama, "Nama pelanggan harus diisi!");
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(txtAlamat.Text)) // Added validation for Alamat
            {
                errorProvider1.SetError(txtAlamat, "Alamat pelanggan harus diisi!");
                isValid = false;
            }

            string phoneNumber = txtNoTelp.Text.Trim();
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                errorProvider1.SetError(txtNoTelp, "Nomor telepon harus diisi!");
                isValid = false;
            }
            else if (!phoneNumber.StartsWith("08") || phoneNumber.Length < 10 ||
                phoneNumber.Length > 13 || !phoneNumber.All(char.IsDigit))
            {
                errorProvider1.SetError(txtNoTelp,
                    "Nomor telepon harus dimulai dengan '08' dan berisi 10-13 digit angka!");
                isValid = false;
            }

            string email = txtEmail.Text.Trim();
            if (string.IsNullOrWhiteSpace(email))
            {
                errorProvider1.SetError(txtEmail, "Email harus diisi!");
                isValid = false;
            }
            else
            {
                try
                {
                    var addr = new MailAddress(email); // Validates email format
                }
                catch
                {
                    errorProvider1.SetError(txtEmail, "Format email tidak valid!");
                    isValid = false;
                }
            }
            return isValid;
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
            {
                MessageBox.Show("Harap perbaiki kesalahan input sebelum menyimpan.", "Validasi Gagal",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    // Ganti dari inline SQL menjadi pemanggilan Stored Procedure
                    using (SqlCommand cmd = new SqlCommand("sp_AddPelanggan", conn))
                    {
                        // Set tipe command menjadi StoredProcedure
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Tambahkan parameter sesuai yang ada di SP
                        cmd.Parameters.AddWithValue("@NamaPelanggan", txtNama.Text.Trim());
                        cmd.Parameters.AddWithValue("@Alamat", txtAlamat.Text.Trim());
                        cmd.Parameters.AddWithValue("@NoTelp", txtNoTelp.Text.Trim());
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());

                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Data pelanggan berhasil ditambahkan!", "Sukses",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            RefreshData();
                        }
                    }
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show($"Database Error: {sqlEx.Message}\n(Error Number: {sqlEx.Number})", "SQL Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                    using (SqlConnection conn = new SqlConnection(connectionString))
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

        private void btnUbah_Click(object sender, EventArgs e)
        {
            int selectedId = 0;
            if (dgvPelanggan.SelectedRows.Count > 0)
            {
                // Ambil ID dari kolom yang sesuai (pastikan nama kolom benar)
                var idCell = dgvPelanggan.SelectedRows[0].Cells["colIDPelanggan"];
                if (idCell?.Value != null)
                {
                    selectedId = Convert.ToInt32(idCell.Value);
                }
            }

            if (selectedId == 0)
            {
                MessageBox.Show("Pilih pelanggan yang akan diubah!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInput())
            {
                 MessageBox.Show("Harap perbaiki kesalahan input sebelum menyimpan.", "Validasi Gagal",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                     // Ganti dari inline SQL menjadi pemanggilan Stored Procedure
                    using (SqlCommand cmd = new SqlCommand("sp_UpdatePelanggan", conn))
                    {
                        // Set tipe command menjadi StoredProcedure
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Tambahkan parameter sesuai yang ada di SP
                        cmd.Parameters.AddWithValue("@IDPelanggan", selectedId);
                        cmd.Parameters.AddWithValue("@NamaPelanggan", txtNama.Text.Trim());
                        cmd.Parameters.AddWithValue("@Alamat", txtAlamat.Text.Trim());
                        cmd.Parameters.AddWithValue("@NoTelp", txtNoTelp.Text.Trim());
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());

                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Data pelanggan berhasil diubah!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            RefreshData();
                        }
                        else
                        {
                             MessageBox.Show("Data pelanggan tidak ditemukan atau tidak ada perubahan.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show($"Database Error: {sqlEx.Message}\n(Error Number: {sqlEx.Number})", "SQL Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();

            // Panggil RefreshData dan simpan hasilnya
            bool isSuccess = RefreshData();

            // Hanya tampilkan pesan jika refresh berhasil
            if (isSuccess)
            {
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
            errorProvider1.Clear(); // Clear errors from the designer's error provider
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

                errorProvider1.Clear(); // Clear any previous validation errors
            }
            else
            {
                // Optional: If no row is selected, you might want to clear fields
                // ClearFields(); // This might be too aggressive depending on UX preference.
            }
        }
    }
}