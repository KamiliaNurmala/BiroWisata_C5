using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BiroWisataForm
{
    public partial class Pembayaran : Form
    {
        private string connectionString = @"Data Source=KAMILIA\KAMILIANURMALA;Initial Catalog=BiroWisata;Integrated Security=True;";
        private int selectedPembayaranId = -1;

        public Pembayaran()
        {
            InitializeComponent();
            InitializeDataGridView();
            InitializeDateTimePicker();
            LoadPemesanan();
        }

        // Initialize the DateTimePicker for payment date
        private void InitializeDateTimePicker()
        {
            dtpTanggalPembayaran = new DateTimePicker
            {
                Format = DateTimePickerFormat.Short,
                Value = DateTime.Now
            };
            // Add the DateTimePicker to the form if not added via designer
            Controls.Add(dtpTanggalPembayaran);
        }

        // Load orders (Pemesanan) into a combo box
        private void LoadPemesanan()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    // Adjust the query as necessary if you need more descriptive text
                    string query = "SELECT IDPemesanan FROM Pemesanan";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Add placeholder row
                    DataRow row = dt.NewRow();
                    row["IDPemesanan"] = DBNull.Value;
                    dt.Rows.InsertAt(row, 0);

                    cmbPemesanan.DataSource = dt;
                    cmbPemesanan.DisplayMember = "IDPemesanan";
                    cmbPemesanan.ValueMember = "IDPemesanan";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading pemesanan: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Refresh the Pembayaran data grid from the database
        private void RefreshData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"SELECT p.IDPembayaran, p.IDPemesanan, p.JumlahPembayaran, 
                                            p.TanggalPembayaran, p.MetodePembayaran
                                     FROM Pembayaran p";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvPembayaran.DataSource = dt;

                    if (dgvPembayaran.Columns.Count > 0)
                    {
                        dgvPembayaran.Columns["IDPembayaran"].HeaderText = "ID";
                        dgvPembayaran.Columns["IDPemesanan"].HeaderText = "Pemesanan";
                        dgvPembayaran.Columns["JumlahPembayaran"].HeaderText = "Jumlah";
                        dgvPembayaran.Columns["TanggalPembayaran"].HeaderText = "Tanggal";
                        dgvPembayaran.Columns["MetodePembayaran"].HeaderText = "Metode";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Configures the DataGridView (set to auto-generate columns)
        private void InitializeDataGridView()
        {
            dgvPembayaran.AutoGenerateColumns = true;
            dgvPembayaran.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPembayaran.MultiSelect = false;
            dgvPembayaran.ReadOnly = true;
            dgvPembayaran.CellClick += DgvPembayaran_CellClick;
        }

        // When a grid row is clicked, load the data into controls for editing
        private void DgvPembayaran_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvPembayaran.Rows[e.RowIndex];
                selectedPembayaranId = Convert.ToInt32(row.Cells["IDPembayaran"].Value);
                cmbPemesanan.SelectedValue = row.Cells["IDPemesanan"].Value;
                txtJumlahPembayaran.Text = row.Cells["JumlahPembayaran"].Value.ToString();
                dtpTanggalPembayaran.Value = Convert.ToDateTime(row.Cells["TanggalPembayaran"].Value);
                // Since only 'Transfer' is allowed, you may simply display it as a fixed value.
            }
        }

        // Adds a new payment record
        private void btnTambah_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
            {
                MessageBox.Show("Semua field harus diisi dengan benar!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"INSERT INTO Pembayaran (IDPemesanan, JumlahPembayaran, TanggalPembayaran, MetodePembayaran)
                                     VALUES (@IDPemesanan, @JumlahPembayaran, @TanggalPembayaran, @MetodePembayaran)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IDPemesanan", cmbPemesanan.SelectedValue);
                        if (!decimal.TryParse(txtJumlahPembayaran.Text.Trim(), out decimal jumlah) || jumlah <= 0)
                        {
                            MessageBox.Show("Jumlah pembayaran harus bernilai positif!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        cmd.Parameters.AddWithValue("@JumlahPembayaran", jumlah);
                        cmd.Parameters.AddWithValue("@TanggalPembayaran", dtpTanggalPembayaran.Value);
                        // Only 'Transfer' is allowed as per table definition.
                        cmd.Parameters.AddWithValue("@MetodePembayaran", "Transfer");

                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("Data pembayaran berhasil ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            RefreshData();
                            ClearInputs();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Deletes the selected payment record
        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (selectedPembayaranId < 0)
            {
                MessageBox.Show("Please select a pembayaran record to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete this pembayaran?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        string query = "DELETE FROM Pembayaran WHERE IDPembayaran = @IDPembayaran";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@IDPembayaran", selectedPembayaranId);
                            int result = cmd.ExecuteNonQuery();
                            if (result > 0)
                            {
                                MessageBox.Show("Data pembayaran berhasil dihapus!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                RefreshData();
                                ClearInputs();
                                selectedPembayaranId = -1;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // Updates the selected payment record
        private void btnUbah_Click(object sender, EventArgs e)
        {
            if (selectedPembayaranId < 0)
            {
                MessageBox.Show("Please select a pembayaran record to update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInputs())
            {
                MessageBox.Show("Semua field harus diisi dengan benar!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"UPDATE Pembayaran 
                                     SET IDPemesanan = @IDPemesanan, 
                                         JumlahPembayaran = @JumlahPembayaran, 
                                         TanggalPembayaran = @TanggalPembayaran,
                                         MetodePembayaran = @MetodePembayaran
                                     WHERE IDPembayaran = @IDPembayaran";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IDPemesanan", cmbPemesanan.SelectedValue);
                        if (!decimal.TryParse(txtJumlahPembayaran.Text.Trim(), out decimal jumlah) || jumlah <= 0)
                        {
                            MessageBox.Show("Jumlah pembayaran harus bernilai positif!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        cmd.Parameters.AddWithValue("@JumlahPembayaran", jumlah);
                        cmd.Parameters.AddWithValue("@TanggalPembayaran", dtpTanggalPembayaran.Value);
                        cmd.Parameters.AddWithValue("@MetodePembayaran", "Transfer");
                        cmd.Parameters.AddWithValue("@IDPembayaran", selectedPembayaranId);

                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("Data pembayaran berhasil diubah!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            RefreshData();
                            ClearInputs();
                            selectedPembayaranId = -1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Refresh button simply calls RefreshData
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        // Validate inputs: check if an order is selected and payment amount is provided
        private bool ValidateInputs()
        {
            if (cmbPemesanan.SelectedValue == null || cmbPemesanan.SelectedValue == DBNull.Value)
                return false;
            if (string.IsNullOrWhiteSpace(txtJumlahPembayaran.Text))
                return false;
            return true;
        }

        // Clears the input controls
        private void ClearInputs()
        {
            cmbPemesanan.SelectedIndex = 0;
            txtJumlahPembayaran.Clear();
            dtpTanggalPembayaran.Value = DateTime.Now;
        }
    }
}
