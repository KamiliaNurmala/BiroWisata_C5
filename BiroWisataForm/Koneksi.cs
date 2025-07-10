using System;
using System.Net;
using System.Net.Sockets;

namespace praktikum7
{
    internal class Koneksi
    {
        public string connectionString()
        {
            try
            {
                string localIP = GetLocalIPAddress();
                return $@"Data Source={localIP}\KAMILIANURMALA;Initial Catalog=BiroWisataTry;Integrated Security=True;TrustServerCertificate=True;";
                //return $"Server={localIP}; Initial Catalog=BiroWisataTry;" +
                //             $"Integrated Security=True;";

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public static string GetLocalIPAddress() //untuk mengambil IP Address pada PC yang menjalankan aplikasi
        {
            //mengambil infromasi tentang local host
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork) // Mengambil IPv4
                    return ip.ToString();
            }
            throw new Exception("Tidak ada alamat IP yang ditemukan.");
        }
    }
}