using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BiroWisataForm
{
    public partial class Pelanggan : Form
    {
        private string connectionString =
            @"Data Source=KAMILIA\KAMILIANURMALA;Initial Catalog=BiroWisata;Integrated Security=True;";

        public Pelanggan()
        {
            InitializeComponent();
            InitializeDataGridView();
        }


        private void Pelanggan_Load(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void RefreshData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT IDPelanggan, NamaPelanggan, Alamat, NoTelp, Email FROM Pelanggan";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dgvPelanggan.DataSource = null;
                    dgvPelanggan.DataSource = dataTable;

                    // Format the DataGridView columns
                    if (dgvPelanggan.Columns.Count > 0)
                    {
                        dgvPelanggan.Columns["IDPelanggan"].HeaderText = "ID";
                        dgvPelanggan.Columns["NamaPelanggan"].HeaderText = "Nama";
                        dgvPelanggan.Columns["Alamat"].HeaderText = "Alamat";
                        dgvPelanggan.Columns["NoTelp"].HeaderText = "No. Telepon";
                        dgvPelanggan.Columns["Email"].HeaderText = "Email";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Database Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            // Validate input fields
            if (string.IsNullOrWhiteSpace(txtNama.Text) ||
                string.IsNullOrWhiteSpace(txtAlamat.Text) ||
                string.IsNullOrWhiteSpace(txtNoTelp.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Semua field harus diisi!", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate phone number format
            if (!txtNoTelp.Text.StartsWith("08") ||
                txtNoTelp.Text.Length < 10 ||
                txtNoTelp.Text.Length > 13)
            {
                MessageBox.Show("Nomor telepon harus dimulai dengan '08' dan panjang 10-13 digit!",
                    "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"INSERT INTO Pelanggan (NamaPelanggan, Alamat, NoTelp, Email) 
                           VALUES (@NamaPelanggan, @Alamat, @NoTelp, @Email)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@NamaPelanggan", txtNama.Text.Trim());
                        cmd.Parameters.AddWithValue("@Alamat", txtAlamat.Text.Trim());
                        cmd.Parameters.AddWithValue("@NoTelp", txtNoTelp.Text.Trim());
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());

                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("Data berhasil ditambahkan!", "Sukses",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearFields();
                            RefreshData();
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


        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (dgvPelanggan.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("Apakah Anda yakin ingin menghapus data ini?", "Konfirmasi",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(dgvPelanggan.SelectedRows[0].Cells["IDPelanggan"].Value);
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        try
                        {
                            conn.Open();
                            // Start a transaction to ensure both operations complete
                            using (SqlTransaction transaction = conn.BeginTransaction())
                            {
                                try
                                {
                                    // Delete the record
                                    string deleteQuery = "DELETE FROM Pelanggan WHERE IDPelanggan = @IDPelanggan";
                                    using (SqlCommand cmdDelete = new SqlCommand(deleteQuery, conn, transaction))
                                    {
                                        cmdDelete.Parameters.AddWithValue("@IDPelanggan", id);
                                        cmdDelete.ExecuteNonQuery();
                                    }

                                    // Get the maximum ID after deletion
                                    string maxIdQuery = "SELECT ISNULL(MAX(IDPelanggan), 0) FROM Pelanggan";
                                    int maxId;
                                    using (SqlCommand cmdMax = new SqlCommand(maxIdQuery, conn, transaction))
                                    {
                                        maxId = Convert.ToInt32(cmdMax.ExecuteScalar());
                                    }

                                    // Reset the identity to the maximum ID
                                    string reseedQuery = $"DBCC CHECKIDENT ('Pelanggan', RESEED, {maxId})";
                                    using (SqlCommand cmdReseed = new SqlCommand(reseedQuery, conn, transaction))
                                    {
                                        cmdReseed.ExecuteNonQuery();
                                    }

                                    // Commit the transaction
                                    transaction.Commit();

                                    MessageBox.Show("Data berhasil dihapus!", "Sukses",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    ClearFields();
                                    RefreshData();
                                }
                                catch (Exception ex)
                                {
                                    // If any error occurs, roll back the transaction
                                    transaction.Rollback();
                                    throw;
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
                MessageBox.Show("Pilih baris yang ingin dihapus!", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnUbah_Click(object sender, EventArgs e)
        {
            if (dgvPelanggan.SelectedRows.Count > 0)
            {
                // Validate phone number format
                if (!txtNoTelp.Text.StartsWith("08") ||
                    txtNoTelp.Text.Length < 10 ||
                    txtNoTelp.Text.Length > 13)
                {
                    MessageBox.Show("Nomor telepon harus dimulai dengan '08' dan panjang 10-13 digit!",
                        "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int id = Convert.ToInt32(dgvPelanggan.SelectedRows[0].Cells["IDPelanggan"].Value);
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        string query = @"UPDATE Pelanggan 
                               SET NamaPelanggan = @Nama, 
                                   Alamat = @Alamat, 
                                   NoTelp = @NoTelp, 
                                   Email = @Email 
                               WHERE IDPelanggan = @Id";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Id", id);
                            cmd.Parameters.AddWithValue("@Nama", txtNama.Text.Trim());
                            cmd.Parameters.AddWithValue("@Alamat", txtAlamat.Text.Trim());
                            cmd.Parameters.AddWithValue("@NoTelp", txtNoTelp.Text.Trim());
                            cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Data berhasil diubah!", "Sukses",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            RefreshData();
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
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void ClearFields()
        {
            txtNama.Clear();
            txtAlamat.Clear();
            txtNoTelp.Clear();
            txtEmail.Clear();
        }

        private void InitializeDataGridView()
        {
            dgvPelanggan.AutoGenerateColumns = true;
            dgvPelanggan.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPelanggan.MultiSelect = false;
            dgvPelanggan.ReadOnly = true;
        }

    }
}
