namespace BiroWisataForm
{
    partial class MenuAdmin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnPelanggan = new System.Windows.Forms.Button();
            this.btnDriver = new System.Windows.Forms.Button();
            this.btnKendaraan = new System.Windows.Forms.Button();
            this.btnPemesanan = new System.Windows.Forms.Button();
            this.btnPaketWisata = new System.Windows.Forms.Button();
            this.btnPembayaran = new System.Windows.Forms.Button();
            this.btnOperasional = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnPelanggan
            // 
            this.btnPelanggan.Location = new System.Drawing.Point(51, 46);
            this.btnPelanggan.Name = "btnPelanggan";
            this.btnPelanggan.Size = new System.Drawing.Size(111, 57);
            this.btnPelanggan.TabIndex = 0;
            this.btnPelanggan.Text = "Pelanggan";
            this.btnPelanggan.UseVisualStyleBackColor = true;
            this.btnPelanggan.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnDriver
            // 
            this.btnDriver.Location = new System.Drawing.Point(246, 46);
            this.btnDriver.Name = "btnDriver";
            this.btnDriver.Size = new System.Drawing.Size(98, 57);
            this.btnDriver.TabIndex = 1;
            this.btnDriver.Text = "Driver";
            this.btnDriver.UseVisualStyleBackColor = true;
            this.btnDriver.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnKendaraan
            // 
            this.btnKendaraan.Location = new System.Drawing.Point(426, 48);
            this.btnKendaraan.Name = "btnKendaraan";
            this.btnKendaraan.Size = new System.Drawing.Size(105, 52);
            this.btnKendaraan.TabIndex = 2;
            this.btnKendaraan.Text = "Kendaraan";
            this.btnKendaraan.UseVisualStyleBackColor = true;
            this.btnKendaraan.Click += new System.EventHandler(this.btnKendaraan_Click);
            // 
            // btnPemesanan
            // 
            this.btnPemesanan.Location = new System.Drawing.Point(136, 187);
            this.btnPemesanan.Name = "btnPemesanan";
            this.btnPemesanan.Size = new System.Drawing.Size(116, 52);
            this.btnPemesanan.TabIndex = 3;
            this.btnPemesanan.Text = "Pemesanan";
            this.btnPemesanan.UseVisualStyleBackColor = true;
            this.btnPemesanan.Click += new System.EventHandler(this.btnPemesanan_Click);
            // 
            // btnPaketWisata
            // 
            this.btnPaketWisata.Location = new System.Drawing.Point(624, 51);
            this.btnPaketWisata.Name = "btnPaketWisata";
            this.btnPaketWisata.Size = new System.Drawing.Size(105, 52);
            this.btnPaketWisata.TabIndex = 4;
            this.btnPaketWisata.Text = "Paket Wisata";
            this.btnPaketWisata.UseVisualStyleBackColor = true;
            this.btnPaketWisata.Click += new System.EventHandler(this.button4_Click);
            // 
            // btnPembayaran
            // 
            this.btnPembayaran.Location = new System.Drawing.Point(324, 187);
            this.btnPembayaran.Name = "btnPembayaran";
            this.btnPembayaran.Size = new System.Drawing.Size(111, 52);
            this.btnPembayaran.TabIndex = 5;
            this.btnPembayaran.Text = "Pembayaran";
            this.btnPembayaran.UseVisualStyleBackColor = true;
            this.btnPembayaran.Click += new System.EventHandler(this.button5_Click);
            // 
            // btnOperasional
            // 
            this.btnOperasional.Location = new System.Drawing.Point(514, 187);
            this.btnOperasional.Name = "btnOperasional";
            this.btnOperasional.Size = new System.Drawing.Size(118, 52);
            this.btnOperasional.TabIndex = 6;
            this.btnOperasional.Text = "Operasional";
            this.btnOperasional.UseVisualStyleBackColor = true;
            this.btnOperasional.Click += new System.EventHandler(this.btnOperasional_Click);
            // 
            // MenuAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnOperasional);
            this.Controls.Add(this.btnPembayaran);
            this.Controls.Add(this.btnPaketWisata);
            this.Controls.Add(this.btnPemesanan);
            this.Controls.Add(this.btnKendaraan);
            this.Controls.Add(this.btnDriver);
            this.Controls.Add(this.btnPelanggan);
            this.Name = "MenuAdmin";
            this.Text = "MenuAdmin";
            this.Click += new System.EventHandler(this.button5_Click);
            this.ResumeLayout(false);

        }

        #endregion


        private System.Windows.Forms.Button btnPelanggan;
        private System.Windows.Forms.Button btnDriver;
        private System.Windows.Forms.Button btnKendaraan;
        private System.Windows.Forms.Button btnPemesanan;
        private System.Windows.Forms.Button btnPaketWisata;
        private System.Windows.Forms.Button btnPembayaran;
        private System.Windows.Forms.Button btnOperasional;
    }
}