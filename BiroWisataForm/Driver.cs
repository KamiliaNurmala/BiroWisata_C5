using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BiroWisataForm
{
    public partial class Driver : Form
    {
        private string connectionString =
            @"Data Source=KAMILIA\KAMILIANURMALA;Initial Catalog=BiroWisata;Integrated Security=True;";

        public Driver()
        {
            InitializeComponent();
            InitializeDataGridView();
        }

        private void Driver_Load(object sender, EventArgs e)
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
                    string query = "SELECT IDDriver, NamaDriver, NoTelp, NoSIM FROM Driver";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dgvDriver.DataSource = null;
                    dgvDriver.DataSource = dataTable;

                    // Format the DataGridView columns
                    if (dgvDriver.Columns.Count > 0)
                    {
                        dgvDriver.Columns["IDDriver"].HeaderText = "ID";
                        dgvDriver.Columns["NamaDriver"].HeaderText = "Nama";
                        dgvDriver.Columns["NoTelp"].HeaderText = "No. Telepon";
                        dgvDriver.Columns["NoSIM"].HeaderText = "No. SIM";
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
                string.IsNullOrWhiteSpace(txtNoTel.Text) ||
                string.IsNullOrWhiteSpace(txtNoSim.Text))
            {
                MessageBox.Show("Semua field harus diisi!", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate phone number format
            if (!txtNoTel.Text.StartsWith("08") ||
                txtNoTel.Text.Length < 10 ||
                txtNoTel.Text.Length > 13)
            {
                MessageBox.Show("Nomor telepon harus dimulai dengan '08' dan panjang 10-13 digit!",
                    "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate SIM number length
            if (txtNoSim.Text.Length != 14)
            {
                MessageBox.Show("Nomor SIM harus 14 digit!",
                    "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"INSERT INTO Driver (NamaDriver, NoTelp, NoSIM) 
                           VALUES (@NamaDriver, @NoTelp, @NoSIM)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@NamaDriver", txtNama.Text.Trim());
                        cmd.Parameters.AddWithValue("@NoTelp", txtNoTel.Text.Trim());
                        cmd.Parameters.AddWithValue("@NoSIM", txtNoSim.Text.Trim());

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
            if (dgvDriver.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("Apakah Anda yakin ingin menghapus data ini?", "Konfirmasi",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(dgvDriver.SelectedRows[0].Cells["IDDriver"].Value);
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        try
                        {
                            conn.Open();
                            using (SqlTransaction transaction = conn.BeginTransaction())
                            {
                                try
                                {
                                    string deleteQuery = "DELETE FROM Driver WHERE IDDriver = @IDDriver";
                                    using (SqlCommand cmdDelete = new SqlCommand(deleteQuery, conn, transaction))
                                    {
                                        cmdDelete.Parameters.AddWithValue("@IDDriver", id);
                                        cmdDelete.ExecuteNonQuery();
                                    }

                                    string maxIdQuery = "SELECT ISNULL(MAX(IDDriver), 0) FROM Driver";
                                    int maxId;
                                    using (SqlCommand cmdMax = new SqlCommand(maxIdQuery, conn, transaction))
                                    {
                                        maxId = Convert.ToInt32(cmdMax.ExecuteScalar());
                                    }

                                    string reseedQuery = $"DBCC CHECKIDENT ('Driver', RESEED, {maxId})";
                                    using (SqlCommand cmdReseed = new SqlCommand(reseedQuery, conn, transaction))
                                    {
                                        cmdReseed.ExecuteNonQuery();
                                    }

                                    transaction.Commit();
                                    MessageBox.Show("Data berhasil dihapus!", "Sukses",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    ClearFields();
                                    RefreshData();
                                }
                                catch (Exception)
                                {
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
            if (dgvDriver.SelectedRows.Count > 0)
            {
                // Validate phone number format
                if (!txtNoTel.Text.StartsWith("08") ||
                    txtNoTel.Text.Length < 10 ||
                    txtNoTel.Text.Length > 13)
                {
                    MessageBox.Show("Nomor telepon harus dimulai dengan '08' dan panjang 10-13 digit!",
                        "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validate SIM number length
                if (txtNoSim.Text.Length != 14)
                {
                    MessageBox.Show("Nomor SIM harus 14 digit!",
                        "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int id = Convert.ToInt32(dgvDriver.SelectedRows[0].Cells["IDDriver"].Value);
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        string query = @"UPDATE Driver 
                               SET NamaDriver = @Nama, 
                                   NoTelp = @NoTelp, 
                                   NoSIM = @NoSIM 
                               WHERE IDDriver = @Id";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Id", id);
                            cmd.Parameters.AddWithValue("@Nama", txtNama.Text.Trim());
                            cmd.Parameters.AddWithValue("@NoTelp", txtNoTel.Text.Trim());
                            cmd.Parameters.AddWithValue("@NoSIM", txtNoSim.Text.Trim());
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
            txtNoTel.Clear();
            txtNoSim.Clear();
        }

        private void InitializeDataGridView()
        {
            dgvDriver.AutoGenerateColumns = true;
            dgvDriver.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDriver.MultiSelect = false;
            dgvDriver.ReadOnly = true;
        }

        private void Driver_Load_1(object sender, EventArgs e)
        {

        }
    }
}
