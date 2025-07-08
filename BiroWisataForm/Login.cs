using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace BiroWisataForm
{
    public partial class Login : Form
    {
        //private string connectionString =
        //    //@"Data Source=MSI;Initial Catalog=BiroWisata;Integrated Security=True";
        private string connectionString = @"Data Source=KAMILIA\KAMILIANURMALA;Initial Catalog=BiroWisata;Integrated Security=True;";
        private int loginAttempts = 0;
        private const int MAX_ATTEMPTS = 3;

        public Login()
        {
            InitializeComponent();

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Username dan Password harus diisi!", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    // --- AWAL PERUBAHAN ---
                    // 1. Menggunakan nama Stored Procedure, bukan query SELECT
                    using (SqlCommand cmd = new SqlCommand("sp_VerifyAdminLogin", conn))
                    {
                        // 2. Memberi tahu bahwa ini adalah Stored Procedure
                        cmd.CommandType = CommandType.StoredProcedure;

                        // 3. Menambahkan KEDUA parameter yang dibutuhkan oleh SP
                        cmd.Parameters.AddWithValue("@Username", txtUsername.Text.Trim());
                        cmd.Parameters.AddWithValue("@Password", txtPassword.Text.Trim());

                        // 4. Menggunakan ExecuteScalar() untuk mendapatkan kolom pertama dari hasil (IDAdmin)
                        // Jika tidak ada hasil (login gagal), result akan menjadi null.
                        var result = cmd.ExecuteScalar();

                        // 5. Logika verifikasi diubah. Kita tidak lagi membandingkan password di C#.
                        // Cukup periksa apakah SP mengembalikan sesuatu atau tidak.
                        if (result != null)
                        {
                            // Login Berhasil
                            loginAttempts = 0;
                            MessageBox.Show("Login berhasil!", "Sukses",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                            MenuAdmin mainMenu = new MenuAdmin();
                            this.Hide();
                            mainMenu.ShowDialog();
                            this.Show();
                            this.txtPassword.Clear();
                            this.txtUsername.Focus();
                        }
                        else
                        {
                            // Login Gagal
                            HandleFailedLogin();
                        }
                    }
                    // --- AKHIR PERUBAHAN ---
                }
                catch (SqlException ex) // Lebih spesifik menangani error SQL
                {
                    MessageBox.Show($"Error database: {ex.Message}", "Database Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex) // Menangani error umum lainnya
                {
                    MessageBox.Show($"Error: {ex.Message}", "General Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void HandleFailedLogin()
        {
            loginAttempts++;
            if (loginAttempts >= MAX_ATTEMPTS)
            {
                MessageBox.Show("Terlalu banyak percobaan gagal. Silakan coba lagi nanti.",
                    "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.Exit();
            }
            else
            {
                MessageBox.Show($"Login gagal! Sisa percobaan: {MAX_ATTEMPTS - loginAttempts}",
                    "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = chkShowPassword.Checked ? '\0' : '•';
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
