namespace BiroWisataForm
{
    partial class PaketWisata
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtNamaPaket;
        private System.Windows.Forms.TextBox txtDestinasi;
        private System.Windows.Forms.TextBox txtHarga;
        private System.Windows.Forms.TextBox txtDurasi;
        private System.Windows.Forms.TextBox txtFasilitas;
        private System.Windows.Forms.TextBox txtKategori;
        private System.Windows.Forms.TextBox txtKuota;
        private System.Windows.Forms.TextBox txtJadwal;
        private System.Windows.Forms.Button btnTambah;
        private System.Windows.Forms.Button btnHapus;
        private System.Windows.Forms.Button btnUbah;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridView dgvPaketWisata;
        private System.Windows.Forms.ComboBox cmbDriver;
        private System.Windows.Forms.ComboBox cmbKendaraan;

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

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtNamaPaket = new System.Windows.Forms.TextBox();
            this.txtDestinasi = new System.Windows.Forms.TextBox();
            this.txtHarga = new System.Windows.Forms.TextBox();
            this.txtDurasi = new System.Windows.Forms.TextBox();
            this.txtFasilitas = new System.Windows.Forms.TextBox();
            this.txtKategori = new System.Windows.Forms.TextBox();
            this.txtKuota = new System.Windows.Forms.TextBox();
            this.txtJadwal = new System.Windows.Forms.TextBox();
            this.btnTambah = new System.Windows.Forms.Button();
            this.btnHapus = new System.Windows.Forms.Button();
            this.btnUbah = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.dgvPaketWisata = new System.Windows.Forms.DataGridView();
            this.cmbDriver = new System.Windows.Forms.ComboBox();
            this.cmbKendaraan = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPaketWisata)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(60, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nama Paket : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(60, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Destinasi : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(60, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Harga : ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(60, 150);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Durasi : ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(60, 182);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "Fasilitas :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(60, 211);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 20);
            this.label6.TabIndex = 5;
            this.label6.Text = "Kategori :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(60, 243);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 20);
            this.label7.TabIndex = 6;
            this.label7.Text = "Kuota : ";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(60, 277);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(183, 20);
            this.label8.TabIndex = 7;
            this.label8.Text = "Jadwal Keberangkatan : ";
            // 
            // txtNamaPaket
            // 
            this.txtNamaPaket.Location = new System.Drawing.Point(246, 45);
            this.txtNamaPaket.Name = "txtNamaPaket";
            this.txtNamaPaket.Size = new System.Drawing.Size(448, 26);
            this.txtNamaPaket.TabIndex = 8;
            // 
            // txtDestinasi
            // 
            this.txtDestinasi.Location = new System.Drawing.Point(246, 83);
            this.txtDestinasi.Name = "txtDestinasi";
            this.txtDestinasi.Size = new System.Drawing.Size(448, 26);
            this.txtDestinasi.TabIndex = 9;
            // 
            // txtHarga
            // 
            this.txtHarga.Location = new System.Drawing.Point(246, 115);
            this.txtHarga.Name = "txtHarga";
            this.txtHarga.Size = new System.Drawing.Size(448, 26);
            this.txtHarga.TabIndex = 10;
            // 
            // txtDurasi
            // 
            this.txtDurasi.Location = new System.Drawing.Point(246, 147);
            this.txtDurasi.Name = "txtDurasi";
            this.txtDurasi.Size = new System.Drawing.Size(448, 26);
            this.txtDurasi.TabIndex = 11;
            // 
            // txtFasilitas
            // 
            this.txtFasilitas.Location = new System.Drawing.Point(246, 182);
            this.txtFasilitas.Name = "txtFasilitas";
            this.txtFasilitas.Size = new System.Drawing.Size(448, 26);
            this.txtFasilitas.TabIndex = 12;
            // 
            // txtKategori
            // 
            this.txtKategori.Location = new System.Drawing.Point(246, 215);
            this.txtKategori.Name = "txtKategori";
            this.txtKategori.Size = new System.Drawing.Size(448, 26);
            this.txtKategori.TabIndex = 13;
            // 
            // txtKuota
            // 
            this.txtKuota.Location = new System.Drawing.Point(246, 246);
            this.txtKuota.Name = "txtKuota";
            this.txtKuota.Size = new System.Drawing.Size(448, 26);
            this.txtKuota.TabIndex = 14;
            // 
            // txtJadwal
            // 
            this.txtJadwal.Location = new System.Drawing.Point(246, 278);
            this.txtJadwal.Name = "txtJadwal";
            this.txtJadwal.Size = new System.Drawing.Size(448, 26);
            this.txtJadwal.TabIndex = 15;
            // 
            // btnTambah
            // 
            this.btnTambah.Location = new System.Drawing.Point(798, 49);
            this.btnTambah.Name = "btnTambah";
            this.btnTambah.Size = new System.Drawing.Size(82, 38);
            this.btnTambah.TabIndex = 16;
            this.btnTambah.Text = "Tambah";
            this.btnTambah.UseVisualStyleBackColor = true;
            this.btnTambah.Click += new System.EventHandler(this.btnTambah_Click);
            // 
            // btnHapus
            // 
            this.btnHapus.Location = new System.Drawing.Point(798, 97);
            this.btnHapus.Name = "btnHapus";
            this.btnHapus.Size = new System.Drawing.Size(82, 38);
            this.btnHapus.TabIndex = 17;
            this.btnHapus.Text = "Hapus";
            this.btnHapus.UseVisualStyleBackColor = true;
            this.btnHapus.Click += new System.EventHandler(this.btnHapus_Click);
            // 
            // btnUbah
            // 
            this.btnUbah.Location = new System.Drawing.Point(798, 150);
            this.btnUbah.Name = "btnUbah";
            this.btnUbah.Size = new System.Drawing.Size(82, 38);
            this.btnUbah.TabIndex = 18;
            this.btnUbah.Text = "Ubah";
            this.btnUbah.UseVisualStyleBackColor = true;
            this.btnUbah.Click += new System.EventHandler(this.btnUbah_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(798, 199);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(82, 38);
            this.btnRefresh.TabIndex = 19;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // dgvPaketWisata
            // 
            this.dgvPaketWisata.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPaketWisata.Location = new System.Drawing.Point(26, 321);
            this.dgvPaketWisata.Name = "dgvPaketWisata";
            this.dgvPaketWisata.RowHeadersWidth = 62;
            this.dgvPaketWisata.RowTemplate.Height = 28;
            this.dgvPaketWisata.Size = new System.Drawing.Size(919, 288);
            this.dgvPaketWisata.TabIndex = 20;
            // 
            // cmbDriver
            // 
            this.cmbDriver.Location = new System.Drawing.Point(246, 620);
            this.cmbDriver.Name = "cmbDriver";
            this.cmbDriver.Size = new System.Drawing.Size(200, 28);
            this.cmbDriver.TabIndex = 21;
            // 
            // cmbKendaraan
            // 
            this.cmbKendaraan.Location = new System.Drawing.Point(460, 620);
            this.cmbKendaraan.Name = "cmbKendaraan";
            this.cmbKendaraan.Size = new System.Drawing.Size(200, 28);
            this.cmbKendaraan.TabIndex = 22;
            // 
            // PaketWisata
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 681);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtNamaPaket);
            this.Controls.Add(this.txtDestinasi);
            this.Controls.Add(this.txtHarga);
            this.Controls.Add(this.txtDurasi);
            this.Controls.Add(this.txtFasilitas);
            this.Controls.Add(this.txtKategori);
            this.Controls.Add(this.txtKuota);
            this.Controls.Add(this.txtJadwal);
            this.Controls.Add(this.btnTambah);
            this.Controls.Add(this.btnHapus);
            this.Controls.Add(this.btnUbah);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.dgvPaketWisata);
            this.Controls.Add(this.cmbDriver);
            this.Controls.Add(this.cmbKendaraan);
            this.Name = "PaketWisata";
            this.Text = "PaketWisata";
            this.Load += new System.EventHandler(this.PaketWisata_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPaketWisata)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}
