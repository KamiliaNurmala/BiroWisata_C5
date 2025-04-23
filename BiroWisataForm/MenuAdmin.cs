using System;
using System.Windows.Forms;

namespace BiroWisataForm
{
    public partial class MenuAdmin : Form
    {
        public MenuAdmin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Open the Pelanggan form
            Pelanggan pelangganForm = new Pelanggan();
            pelangganForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
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

        private void button4_Click(object sender, EventArgs e)
        {
            // Open the PaketWisata form
            PaketWisata paketWisataForm = new PaketWisata();
            paketWisataForm.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Open the Pembayaran form
            Pembayaran pembayaranForm = new Pembayaran();
            pembayaranForm.Show();
        }

        private void MenuAdmin_Load(object sender, EventArgs e)
        {

        }
    }
}
