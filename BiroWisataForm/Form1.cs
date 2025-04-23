using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace BiroWisataForm
{
    public partial class Form1 : Form
    {
        private string connectionString =
            @"Data Source=KAMILIA\KAMILIANURMALA;Initial Catalog=BiroWisata;Integrated Security=True;";

        public Form1()
        {
            InitializeComponent();
            // Test connection on form load
            TestDatabaseConnection();
        }

        private void TestDatabaseConnection()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    // Verify if Admin table exists and has data
                    string query = "SELECT COUNT(*) FROM Admin";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        int adminCount = (int)cmd.ExecuteScalar();
                        if (adminCount == 0)
                        {
                            MessageBox.Show("No admin records found in database.",
                                "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        // Connection successful, but no message shown if admin exists
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Database connection failed: {ex.Message}",
                        "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
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
                    // First, check if the username exists
                    string checkUserQuery = "SELECT Password FROM Admin WHERE Username = @Username";

                    using (SqlCommand cmd = new SqlCommand(checkUserQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);

                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            string storedPassword = result.ToString();

                            if (storedPassword == password)
                            {
                                MessageBox.Show("Login berhasil!", "Sukses",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                                MenuAdmin MA = new MenuAdmin();
                                MA.Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Password salah!", "Gagal",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Username tidak ditemukan!", "Gagal",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}\nConnection String: {connectionString}",
                        "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
            }
        }

        private void label1_Click(object sender, EventArgs e) { }
        private void label1_Click_1(object sender, EventArgs e) { }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
