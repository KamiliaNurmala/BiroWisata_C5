using Microsoft.Reporting.WinForms;
using praktikum7;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BiroWisataForm
{
    public partial class LaporanTransaksi : Form
    {
        string connectionString = "";
        Koneksi kn = new Koneksi();
        public LaporanTransaksi()
        {
            connectionString = kn.connectionString();

            InitializeComponent();
        }

        private void LaporanTransaksi_Load(object sender, EventArgs e)
        {
            // Setup ReportViewer data
            SetupReportViewer();
            // Refresh report to display data
            this.reportViewer1.RefreshReport();
        }

        private void SetupReportViewer()
        {
            // SQL query untuk mengambil data yang dibutuhkan
            string query = @"
        SELECT
            p.IDPemesanan,
            p.TanggalPemesanan,
            pl.NamaPelanggan,
            pw.NamaPaket,
            p.TotalPembayaran AS TotalTagihan,
            ISNULL(SUM(pb.JumlahPembayaran), 0) AS TotalDibayar,
            p.TotalPembayaran - ISNULL(SUM(pb.JumlahPembayaran), 0) AS SisaTagihan,
            p.StatusPembayaran,
            p.StatusPemesanan,
            MAX(pb.TanggalPembayaran) AS TanggalLunas
        FROM
            dbo.Pemesanan p
        INNER JOIN
            dbo.Pelanggan pl ON p.IDPelanggan = pl.IDPelanggan
        INNER JOIN
            dbo.PaketWisata pw ON p.IDPaket = pw.IDPaket
        LEFT JOIN
            dbo.Pembayaran pb ON p.IDPemesanan = pb.IDPemesanan
        GROUP BY
            p.IDPemesanan,
            p.TanggalPemesanan,
            pl.NamaPelanggan,
            pw.NamaPaket,
            p.TotalPembayaran,
            p.StatusPembayaran,
            p.StatusPemesanan
        ORDER BY
            p.TanggalPemesanan ASC;"; // DIUBAH MENJADI ASC (terlama ke terbaru)

            // Create a DataTable to store the data
            DataTable dt = new DataTable();

            // Use SqlDataAdapter to fill the DataTable with data from the database
            using (SqlConnection conn = new SqlConnection(kn.connectionString()))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(dt);
            }

            // Create a ReportDataSource
            ReportDataSource rds = new ReportDataSource("DataSet1", dt);

            // Clear any existing data sources and add the new data source
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);

            // Set the path to the report (.rdlc file)
            //reportViewer1.LocalReport.ReportPath = @"D:\Kuliah\semester4\Pengembangan Aplikasi Basis Data\IniKelompok\BiroWisataForm-Final_Perbaikan\BiroWisataForm-Final2\BiroWisataForm-Final\BiroWisataForm\BiroWisataForm\Report1.rdlc";

            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Report1.rdlc");
            reportViewer1.LocalReport.ReportPath = path;

            // Refresh the ReportViewer to show the updated report
            reportViewer1.RefreshReport();
        }

        // THIS IS THE CORRECT PLACEMENT FOR reportViewer1_Load
        private void reportViewer1_Load(object sender, EventArgs e)
        {
            if (!kn.CheckConnection())
            {
                return; // Hentikan jika koneksi bermasalah
            }
            // This method is called when the ReportViewer control loads.
            // You typically don't need to put much code here if SetupReportViewer() is called in Form_Load.
            // It can be left empty or used for specific ReportViewer initialization not handled elsewhere.
        }

        // Letakkan method ini bersama dengan event handler tombol lainnya (seperti btnTambah_Click)

        private void btnKembali_Click(object sender, EventArgs e)
        {
            if (!kn.CheckConnection())
            {
                return; // Hentikan jika koneksi bermasalah
            }
            // Perintah ini akan menutup form 'Kendaraan' saat ini,
            // dan mengembalikan kontrol ke form yang membukanya (yaitu MenuAdmin).
            this.Close();
        }
    }
}