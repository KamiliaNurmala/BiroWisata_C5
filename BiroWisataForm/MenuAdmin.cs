using praktikum7;
using System;
using System.Windows.Forms;

namespace BiroWisataForm
{
    public partial class MenuAdmin : Form
    {
        Koneksi kn = new Koneksi();
        public MenuAdmin()
        {
            InitializeComponent();
        }

        private void buttonPelanggan_Click(object sender, EventArgs e)
        {
            // Open the Pelanggan form
            Pelanggan pelangganForm = new Pelanggan();
            pelangganForm.Show();
        }

        private void buttonDriver_Click(object sender, EventArgs e)
        {
            // Open the Driver form
            Driver driverForm = new Driver();
            driverForm.Show();
        }

        private void btnKendaraan_Click(object sender, EventArgs e)
        {
            Kendaraan kendaraanForm = new Kendaraan();
            kendaraanForm.Show();
        }

        private void buttonPaketWisata_Click(object sender, EventArgs e)
        {
            // Open the PaketWisata form
            PaketWisata paketWisataForm = new PaketWisata();
            paketWisataForm.Show();
        }

        private void buttonPembayaran_Click(object sender, EventArgs e)
        {
            // Open the Pembayaran form
            Pembayaran pembayaranForm = new Pembayaran();
            pembayaranForm.Show();
        }

        private void btnPemesanan_Click(object sender, EventArgs e)
        {
            Pemesanan pemesananForm = new Pemesanan();
            pemesananForm.Show();
        }

        private void btnOperasional_Click(object sender, EventArgs e)
        {
            Operasional operasionalForm = new Operasional();
            operasionalForm.Show();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Apakah Anda yakin ingin keluar?",
                "Konfirmasi Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close(); // Just close MenuAdmin. Form1 will take care of reappearing.
            }
        }

        private void btnLaporan_Click(object sender, EventArgs e)
        {
            // Membuat instance baru dari form LaporanTransaksi
            LaporanTransaksi formLaporan = new LaporanTransaksi();

            // Menampilkan form tersebut
            formLaporan.Show();
        }

        private void panelBottom_Paint(object sender, PaintEventArgs e)
        {

        }

        private void MenuAdmin_Load(object sender, EventArgs e)
        {

        }
    }
}
