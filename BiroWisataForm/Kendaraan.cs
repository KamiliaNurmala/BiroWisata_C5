using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BiroWisataForm
{
    public partial class Kendaraan : Form
    {
        private string connectionString =
            @"Data Source=KAMILIA\KAMILIANURMALA;Initial Catalog=BiroWisata;Integrated Security=True;";

        public Kendaraan()
        {
            InitializeComponent();
            InitializeDataGridView();
        }

        private void Kendaraan_Load(object sender, EventArgs e)
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
                    string query = "SELECT IDKendaraan, Jenis, PlatNomor, Kapasitas FROM Kendaraan";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dgvKendaraan.DataSource = null;
                    dgvKendaraan.DataSource = dataTable;

                    // Format the DataGridView columns
                    if (dgvKendaraan.Columns.Count > 0)
                    {
                        dgvKendaraan.Columns["IDKendaraan"].HeaderText = "ID";
                        dgvKendaraan.Columns["Jenis"].HeaderText = "Jenis";
                        dgvKendaraan.Columns["PlatNomor"].HeaderText = "Plat Nomor";
                        dgvKendaraan.Columns["Kapasitas"].HeaderText = "Kapasitas";
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
            if (string.IsNullOrWhiteSpace(txtJenis.Text) ||
                string.IsNullOrWhiteSpace(txtPlatNomor.Text) ||
                string.IsNullOrWhiteSpace(txtKapasitas.Text))
            {
                MessageBox.Show("Semua field harus diisi!", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate capacity (must be a positive integer)
            if (!int.TryParse(txtKapasitas.Text, out int kapasitas) || kapasitas <= 0)
            {
                MessageBox.Show("Kapasitas harus berupa angka positif!",
                    "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate plate number format (can be customized based on your requirements)
            if (txtPlatNomor.Text.Length < 4 || txtPlatNomor.Text.Length > 12)
            {
                MessageBox.Show("Format plat nomor tidak valid!",
                    "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"INSERT INTO Kendaraan (Jenis, PlatNomor, Kapasitas) 
                           VALUES (@Jenis, @PlatNomor, @Kapasitas)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Jenis", txtJenis.Text.Trim());
                        cmd.Parameters.AddWithValue("@PlatNomor", txtPlatNomor.Text.Trim());
                        cmd.Parameters.AddWithValue("@Kapasitas", kapasitas);

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
            if (dgvKendaraan.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("Apakah Anda yakin ingin menghapus data ini?", "Konfirmasi",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(dgvKendaraan.SelectedRows[0].Cells["IDKendaraan"].Value);
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        try
                        {
                            conn.Open();
                            using (SqlTransaction transaction = conn.BeginTransaction())
                            {
                                try
                                {
                                    string deleteQuery = "DELETE FROM Kendaraan WHERE IDKendaraan = @IDKendaraan";
                                    using (SqlCommand cmdDelete = new SqlCommand(deleteQuery, conn, transaction))
                                    {
                                        cmdDelete.Parameters.AddWithValue("@IDKendaraan", id);
                                        cmdDelete.ExecuteNonQuery();
                                    }

                                    string maxIdQuery = "SELECT ISNULL(MAX(IDKendaraan), 0) FROM Kendaraan";
                                    int maxId;
                                    using (SqlCommand cmdMax = new SqlCommand(maxIdQuery, conn, transaction))
                                    {
                                        maxId = Convert.ToInt32(cmdMax.ExecuteScalar());
                                    }

                                    string reseedQuery = $"DBCC CHECKIDENT ('Kendaraan', RESEED, {maxId})";
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
            if (dgvKendaraan.SelectedRows.Count > 0)
            {
                // Validate capacity (must be a positive integer)
                if (!int.TryParse(txtKapasitas.Text, out int kapasitas) || kapasitas <= 0)
                {
                    MessageBox.Show("Kapasitas harus berupa angka positif!",
                        "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validate plate number format
                if (txtPlatNomor.Text.Length < 4 || txtPlatNomor.Text.Length > 12)
                {
                    MessageBox.Show("Format plat nomor tidak valid!",
                        "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int id = Convert.ToInt32(dgvKendaraan.SelectedRows[0].Cells["IDKendaraan"].Value);
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        string query = @"UPDATE Kendaraan 
                               SET Jenis = @Jenis, 
                                   PlatNomor = @PlatNomor, 
                                   Kapasitas = @Kapasitas 
                               WHERE IDKendaraan = @Id";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Id", id);
                            cmd.Parameters.AddWithValue("@Jenis", txtJenis.Text.Trim());
                            cmd.Parameters.AddWithValue("@PlatNomor", txtPlatNomor.Text.Trim());
                            cmd.Parameters.AddWithValue("@Kapasitas", kapasitas);
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
            txtJenis.Clear();
            txtPlatNomor.Clear();
            txtKapasitas.Clear();
        }

        private void InitializeDataGridView()
        {
            dgvKendaraan.AutoGenerateColumns = true;
            dgvKendaraan.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvKendaraan.MultiSelect = false;
            dgvKendaraan.ReadOnly = true;
        }

        private void dgvKendaraan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvKendaraan.Rows[e.RowIndex];
                txtJenis.Text = row.Cells["Jenis"].Value.ToString();
                txtPlatNomor.Text = row.Cells["PlatNomor"].Value.ToString();
                txtKapasitas.Text = row.Cells["Kapasitas"].Value.ToString();
            }
        }
    }
}