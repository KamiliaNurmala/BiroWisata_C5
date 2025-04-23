namespace BiroWisataForm
{
    partial class Pembayaran
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
            this.btnTambah = new System.Windows.Forms.Button();
            this.btnHapus = new System.Windows.Forms.Button();
            this.btnUbah = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblJumlahPembayaran = new System.Windows.Forms.Label();
            this.lblTanggalPembayaran = new System.Windows.Forms.Label();
            this.lblMetodePembayaran = new System.Windows.Forms.Label();
            this.txtJumlah = new System.Windows.Forms.TextBox();
            this.txtTanggal = new System.Windows.Forms.TextBox();
            this.txtMetode = new System.Windows.Forms.TextBox();
            this.dgvPembayaran = new System.Windows.Forms.DataGridView();
            this.dtpTanggalPembayaran = new System.Windows.Forms.DateTimePicker();
            this.cmbPemesanan = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPembayaran)).BeginInit();
            this.SuspendLayout();
            // 
            // btnTambah
            // 
            this.btnTambah.Location = new System.Drawing.Point(739, 44);
            this.btnTambah.Name = "btnTambah";
            this.btnTambah.Size = new System.Drawing.Size(87, 33);
            this.btnTambah.TabIndex = 0;
            this.btnTambah.Text = "Tambah";
            this.btnTambah.UseVisualStyleBackColor = true;
            this.btnTambah.Click += new System.EventHandler(this.btnTambah_Click);
            // 
            // btnHapus
            // 
            this.btnHapus.Location = new System.Drawing.Point(739, 83);
            this.btnHapus.Name = "btnHapus";
            this.btnHapus.Size = new System.Drawing.Size(87, 33);
            this.btnHapus.TabIndex = 1;
            this.btnHapus.Text = "Hapus";
            this.btnHapus.UseVisualStyleBackColor = true;
            this.btnHapus.Click += new System.EventHandler(this.btnHapus_Click);
            // 
            // btnUbah
            // 
            this.btnUbah.Location = new System.Drawing.Point(739, 122);
            this.btnUbah.Name = "btnUbah";
            this.btnUbah.Size = new System.Drawing.Size(87, 33);
            this.btnUbah.TabIndex = 2;
            this.btnUbah.Text = "Ubah";
            this.btnUbah.UseVisualStyleBackColor = true;
            this.btnUbah.Click += new System.EventHandler(this.btnUbah_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(739, 161);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(87, 33);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lblJumlahPembayaran
            // 
            this.lblJumlahPembayaran.AutoSize = true;
            this.lblJumlahPembayaran.Location = new System.Drawing.Point(77, 51);
            this.lblJumlahPembayaran.Name = "lblJumlahPembayaran";
            this.lblJumlahPembayaran.Size = new System.Drawing.Size(165, 20);
            this.lblJumlahPembayaran.TabIndex = 4;
            this.lblJumlahPembayaran.Text = "Jumlah Pembayaran : ";
            // 
            // lblTanggalPembayaran
            // 
            this.lblTanggalPembayaran.AutoSize = true;
            this.lblTanggalPembayaran.Location = new System.Drawing.Point(77, 108);
            this.lblTanggalPembayaran.Name = "lblTanggalPembayaran";
            this.lblTanggalPembayaran.Size = new System.Drawing.Size(171, 20);
            this.lblTanggalPembayaran.TabIndex = 5;
            this.lblTanggalPembayaran.Text = "Tanggal Pembayaran : ";
            // 
            // lblMetodePembayaran
            // 
            this.lblMetodePembayaran.AutoSize = true;
            this.lblMetodePembayaran.Location = new System.Drawing.Point(77, 160);
            this.lblMetodePembayaran.Name = "lblMetodePembayaran";
            this.lblMetodePembayaran.Size = new System.Drawing.Size(168, 20);
            this.lblMetodePembayaran.TabIndex = 6;
            this.lblMetodePembayaran.Text = "Metode Pembayaran : ";
            // 
            // txtJumlah
            // 
            this.txtJumlah.Location = new System.Drawing.Point(247, 48);
            this.txtJumlah.Name = "txtJumlah";
            this.txtJumlah.Size = new System.Drawing.Size(454, 26);
            this.txtJumlah.TabIndex = 7;
            // 
            // txtTanggal
            // 
            this.txtTanggal.Location = new System.Drawing.Point(247, 102);
            this.txtTanggal.Name = "txtTanggal";
            this.txtTanggal.Size = new System.Drawing.Size(454, 26);
            this.txtTanggal.TabIndex = 8;
            // 
            // txtMetode
            // 
            this.txtMetode.Location = new System.Drawing.Point(247, 157);
            this.txtMetode.Name = "txtMetode";
            this.txtMetode.Size = new System.Drawing.Size(454, 26);
            this.txtMetode.TabIndex = 9;
            // 
            // dgvPembayaran
            // 
            this.dgvPembayaran.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPembayaran.Location = new System.Drawing.Point(37, 232);
            this.dgvPembayaran.Name = "dgvPembayaran";
            this.dgvPembayaran.RowHeadersWidth = 62;
            this.dgvPembayaran.RowTemplate.Height = 28;
            this.dgvPembayaran.Size = new System.Drawing.Size(768, 255);
            this.dgvPembayaran.TabIndex = 10;
            // 
            // dtpTanggalPembayaran
            // 
            this.dtpTanggalPembayaran.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTanggalPembayaran.Location = new System.Drawing.Point(24, 12);
            this.dtpTanggalPembayaran.Name = "dtpTanggalPembayaran";
            this.dtpTanggalPembayaran.Size = new System.Drawing.Size(150, 26);
            this.dtpTanggalPembayaran.TabIndex = 0;
            // 
            // cmbPemesanan
            // 
            this.cmbPemesanan.FormattingEnabled = true;
            this.cmbPemesanan.Location = new System.Drawing.Point(198, 12);
            this.cmbPemesanan.Name = "cmbPemesanan";
            this.cmbPemesanan.Size = new System.Drawing.Size(150, 28);
            this.cmbPemesanan.TabIndex = 1;
            // 
            // Pembayaran
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(838, 523);
            this.Controls.Add(this.dgvPembayaran);
            this.Controls.Add(this.txtMetode);
            this.Controls.Add(this.txtTanggal);
            this.Controls.Add(this.txtJumlah);
            this.Controls.Add(this.lblMetodePembayaran);
            this.Controls.Add(this.lblTanggalPembayaran);
            this.Controls.Add(this.lblJumlahPembayaran);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnUbah);
            this.Controls.Add(this.btnHapus);
            this.Controls.Add(this.btnTambah);
            this.Controls.Add(this.dtpTanggalPembayaran);
            this.Controls.Add(this.cmbPemesanan);
            this.Name = "Pembayaran";
            this.Text = "Pembayaran";
            ((System.ComponentModel.ISupportInitialize)(this.dgvPembayaran)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTambah;
        private System.Windows.Forms.Button btnHapus;
        private System.Windows.Forms.Button btnUbah;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblJumlahPembayaran;
        private System.Windows.Forms.Label lblTanggalPembayaran;
        private System.Windows.Forms.Label lblMetodePembayaran;
        private System.Windows.Forms.TextBox txtJumlah;
        private System.Windows.Forms.TextBox txtTanggal;
        private System.Windows.Forms.TextBox txtMetode;
        private System.Windows.Forms.DataGridView dgvPembayaran;
        private System.Windows.Forms.DateTimePicker dtpTanggalPembayaran;
        private System.Windows.Forms.ComboBox cmbPemesanan;

    }
}