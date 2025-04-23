using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BiroWisataForm
{
    public partial class PaketWisata : Form
    {
        private string connectionString = @"Data Source=KAMILIA\KAMILIANURMALA;Initial Catalog=BiroWisata;Integrated Security=True;";
        private int selectedPaketId = -1;
        private DateTimePicker dtpJadwal;

        public PaketWisata()
        {
            InitializeComponent();
            InitializeDataGridView();
            InitializeDateTimePicker(); // Initialize the DateTimePicker
        }

        private void InitializeDateTimePicker()
        {
            dtpJadwal = new DateTimePicker
            {
                Format = DateTimePickerFormat.Short,
                Value = DateTime.Now
            };
            Controls.Add(dtpJadwal); // Add the DateTimePicker to the form
        }

        private void PaketWisata_Load(object sender, EventArgs e)
        {
            LoadDrivers();
            LoadKendaraan();
            RefreshData();
        }

        private void InitializeDataGridView()
        {
            dgvPaketWisata.AutoGenerateColumns = true;
            dgvPaketWisata.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPaketWisata.MultiSelect = false;
            dgvPaketWisata.ReadOnly = true;
            dgvPaketWisata.CellClick += DgvPaketWisata_CellClick;
        }

        private void LoadDrivers()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT IDDriver, NamaDriver FROM Driver WHERE Status = 'Aktif' ORDER BY NamaDriver";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Add a placeholder item
                    DataRow emptyRow = dataTable.NewRow();
                    emptyRow["IDDriver"] = DBNull.Value;
                    emptyRow["NamaDriver"] = "-- Pilih Driver --";
                    dataTable.Rows.InsertAt(emptyRow, 0);

                    cmbDriver.DataSource = dataTable;
                    cmbDriver.DisplayMember = "NamaDriver";
                    cmbDriver.ValueMember = "IDDriver";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading drivers: {ex.Message}", "Database Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void LoadKendaraan()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT IDKendaraan, Jenis FROM Kendaraan";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Add a placeholder item
                    DataRow emptyRow = dataTable.NewRow();
                    emptyRow["IDKendaraan"] = DBNull.Value;
                    emptyRow["Jenis"] = "-- Pilih Kendaraan --";
                    dataTable.Rows.InsertAt(emptyRow, 0);

                    cmbKendaraan.DataSource = dataTable;
                    cmbKendaraan.DisplayMember = "Jenis";
                    cmbKendaraan.ValueMember = "IDKendaraan";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading kendaraan: {ex.Message}", "Database Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void RefreshData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"SELECT p.IDPaket, d.IDDriver, k.IDKendaraan, d.NamaDriver, k.Jenis, 
                             p.NamaPaket, p.Destinasi, p.Harga, p.Durasi, p.Fasilitas, 
                             p.Kategori, p.Kuota, p.JadwalKeberangkatan 
                             FROM PaketWisata p
                             INNER JOIN Driver d ON p.IDDriver = d.IDDriver
                             INNER JOIN Kendaraan k ON p.IDKendaraan = k.IDKendaraan";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dgvPaketWisata.DataSource = dataTable;

                    if (dgvPaketWisata.Columns.Count > 0)
                    {
                        dgvPaketWisata.Columns["IDPaket"].HeaderText = "ID";
                        dgvPaketWisata.Columns["NamaDriver"].HeaderText = "Driver";
                        dgvPaketWisata.Columns["Jenis"].HeaderText = "Jenis Kendaraan"; // Correct header
                        dgvPaketWisata.Columns["NamaPaket"].HeaderText = "Nama Paket";
                        dgvPaketWisata.Columns["Destinasi"].HeaderText = "Destinasi";
                        dgvPaketWisata.Columns["Harga"].HeaderText = "Harga";
                        dgvPaketWisata.Columns["Durasi"].HeaderText = "Durasi (Hari)";
                        dgvPaketWisata.Columns["Fasilitas"].HeaderText = "Fasilitas";
                        dgvPaketWisata.Columns["Kategori"].HeaderText = "Kategori";
                        dgvPaketWisata.Columns["Kuota"].HeaderText = "Kuota";
                        dgvPaketWisata.Columns["JadwalKeberangkatan"].HeaderText = "Jadwal";
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
            if (!ValidateInputs())
            {
                MessageBox.Show("Semua field harus diisi dengan benar!", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"INSERT INTO PaketWisata (IDDriver, IDKendaraan, NamaPaket, Destinasi, 
                                     Harga, Durasi, Fasilitas, Kategori, Kuota, JadwalKeberangkatan)
                                     VALUES (@IDDriver, @IDKendaraan, @NamaPaket, @Destinasi, @Harga, 
                                     @Durasi, @Fasilitas, @Kategori, @Kuota, @JadwalKeberangkatan)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IDDriver", cmbDriver.SelectedValue);
                        cmd.Parameters.AddWithValue("@IDKendaraan", cmbKendaraan.SelectedValue);
                        cmd.Parameters.AddWithValue("@NamaPaket", txtNamaPaket.Text.Trim());
                        cmd.Parameters.AddWithValue("@Destinasi", txtDestinasi.Text.Trim());

                        if (!decimal.TryParse(txtHarga.Text.Trim(), out decimal harga))
                        {
                            MessageBox.Show("Harga tidak valid!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        cmd.Parameters.AddWithValue("@Harga", harga);

                        if (!int.TryParse(txtDurasi.Text.Trim(), out int durasi))
                        {
                            MessageBox.Show("Durasi tidak valid!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        cmd.Parameters.AddWithValue("@Durasi", durasi);

                        cmd.Parameters.AddWithValue("@Fasilitas", txtFasilitas.Text.Trim());
                        cmd.Parameters.AddWithValue("@Kategori", txtKategori.Text.Trim());

                        if (!int.TryParse(txtKuota.Text.Trim(), out int kuota))
                        {
                            MessageBox.Show("Kuota tidak valid!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        cmd.Parameters.AddWithValue("@Kuota", kuota);

                        cmd.Parameters.AddWithValue("@JadwalKeberangkatan", dtpJadwal.Value);

                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("Data berhasil ditambahkan!", "Sukses",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            RefreshData();
                            ClearInputs();
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

        // Deletes the selected paket
        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (selectedPaketId < 0)
            {
                MessageBox.Show("Please select a paket to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete this paket?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        string query = "DELETE FROM PaketWisata WHERE IDPaket = @IDPaket";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@IDPaket", selectedPaketId);
                            int result = cmd.ExecuteNonQuery();
                            if (result > 0)
                            {
                                MessageBox.Show("Data berhasil dihapus!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                RefreshData();
                                ClearInputs();
                                selectedPaketId = -1;
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

        // Updates the selected paket with new data from input controls
        private void btnUbah_Click(object sender, EventArgs e)
        {
            if (selectedPaketId < 0)
            {
                MessageBox.Show("Please select a paket to update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    string query = @"UPDATE PaketWisata 
                             SET IDDriver = @IDDriver, IDKendaraan = @IDKendaraan, NamaPaket = @NamaPaket, Destinasi = @Destinasi,
                                 Harga = @Harga, Durasi = @Durasi, Fasilitas = @Fasilitas, Kategori = @Kategori, 
                                 Kuota = @Kuota, JadwalKeberangkatan = @JadwalKeberangkatan
                             WHERE IDPaket = @IDPaket";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IDDriver", cmbDriver.SelectedValue);
                        cmd.Parameters.AddWithValue("@IDKendaraan", cmbKendaraan.SelectedValue);
                        cmd.Parameters.AddWithValue("@NamaPaket", txtNamaPaket.Text.Trim());
                        cmd.Parameters.AddWithValue("@Destinasi", txtDestinasi.Text.Trim());

                        if (!decimal.TryParse(txtHarga.Text.Trim(), out decimal harga))
                        {
                            MessageBox.Show("Harga tidak valid!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        cmd.Parameters.AddWithValue("@Harga", harga);

                        if (!int.TryParse(txtDurasi.Text.Trim(), out int durasi))
                        {
                            MessageBox.Show("Durasi tidak valid!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        cmd.Parameters.AddWithValue("@Durasi", durasi);

                        cmd.Parameters.AddWithValue("@Fasilitas", txtFasilitas.Text.Trim());
                        cmd.Parameters.AddWithValue("@Kategori", txtKategori.Text.Trim());

                        if (!int.TryParse(txtKuota.Text.Trim(), out int kuota))
                        {
                            MessageBox.Show("Kuota tidak valid!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        cmd.Parameters.AddWithValue("@Kuota", kuota);
                        cmd.Parameters.AddWithValue("@JadwalKeberangkatan", dtpJadwal.Value);
                        cmd.Parameters.AddWithValue("@IDPaket", selectedPaketId);

                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("Data berhasil diubah!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            RefreshData();
                            ClearInputs();
                            selectedPaketId = -1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Simply refreshes the data grid by calling RefreshData
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshData();
        }


        private bool ValidateInputs()
        {
            if (cmbDriver.SelectedValue == null || cmbDriver.SelectedValue == DBNull.Value)
                return false;
            if (cmbKendaraan.SelectedValue == null || cmbKendaraan.SelectedValue == DBNull.Value)
                return false;
            if (string.IsNullOrWhiteSpace(txtNamaPaket.Text))
                return false;
            if (string.IsNullOrWhiteSpace(txtDestinasi.Text))
                return false;
            if (string.IsNullOrWhiteSpace(txtHarga.Text))
                return false;
            if (string.IsNullOrWhiteSpace(txtDurasi.Text))
                return false;
            if (string.IsNullOrWhiteSpace(txtFasilitas.Text))
                return false;
            if (string.IsNullOrWhiteSpace(txtKategori.Text))
                return false;
            if (string.IsNullOrWhiteSpace(txtKuota.Text))
                return false;

            return true;
        }

        private void ClearInputs()
        {
            cmbDriver.SelectedIndex = 0;
            cmbKendaraan.SelectedIndex = 0;
            txtNamaPaket.Clear();
            txtDestinasi.Clear();
            txtHarga.Clear();
            txtDurasi.Clear();
            txtFasilitas.Clear();
            txtKategori.Clear();
            txtKuota.Clear();
            dtpJadwal.Value = DateTime.Now;
        }

        private void DgvPaketWisata_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvPaketWisata.Rows[e.RowIndex];
                selectedPaketId = Convert.ToInt32(row.Cells["IDPaket"].Value);
                // Assuming your query returns these IDs as well.
                cmbDriver.SelectedValue = row.Cells["IDDriver"].Value;
                cmbKendaraan.SelectedValue = row.Cells["IDKendaraan"].Value;
                txtNamaPaket.Text = row.Cells["NamaPaket"].Value.ToString();
                txtDestinasi.Text = row.Cells["Destinasi"].Value.ToString();
                txtHarga.Text = row.Cells["Harga"].Value.ToString();
                txtDurasi.Text = row.Cells["Durasi"].Value.ToString();
                txtFasilitas.Text = row.Cells["Fasilitas"].Value.ToString();
                txtKategori.Text = row.Cells["Kategori"].Value.ToString();
                txtKuota.Text = row.Cells["Kuota"].Value.ToString();
                dtpJadwal.Value = Convert.ToDateTime(row.Cells["JadwalKeberangkatan"].Value);
            }
        }
    }
}
