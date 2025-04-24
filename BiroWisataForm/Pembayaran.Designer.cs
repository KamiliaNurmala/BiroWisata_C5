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

        // --- PINDAHKAN DEKLARASI FILTER KE SINI ---
        private System.Windows.Forms.Button btnTambah;
        private System.Windows.Forms.Button btnHapus;
        private System.Windows.Forms.Button btnUbah;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblJumlahPembayaran;
        private System.Windows.Forms.Label lblTanggalPembayaran;
        private System.Windows.Forms.Label lblMetodePembayaran;
        private System.Windows.Forms.TextBox txtJumlah;
        private System.Windows.Forms.DataGridView dgvPembayaran;
        private System.Windows.Forms.ComboBox cmbPemesanan;
        private System.Windows.Forms.DateTimePicker dateTimePicker1; // Untuk input tanggal pembayaran

        // Kontrol Filter (sebelumnya di bawah #endregion)
        private System.Windows.Forms.DateTimePicker dtpPemesananFilterStart; // Ganti nama dari dateTimePicker2
        private System.Windows.Forms.Label lblPemesananFilterStart;
        private System.Windows.Forms.DateTimePicker dtpPemesananFilterEnd;
        private System.Windows.Forms.Label lblPemesananFilterEnd;
        private System.Windows.Forms.DateTimePicker dtpPembayaranFilterStart;
        private System.Windows.Forms.Label lblPembayaranFilterStart;
        private System.Windows.Forms.DateTimePicker dtpPembayaranFilterEnd;
        private System.Windows.Forms.Label lblPembayaranFilterEnd;
        private System.Windows.Forms.Button btnFilter;
        // ---------------------------------------------


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
            this.dgvPembayaran = new System.Windows.Forms.DataGridView();
            this.cmbPemesanan = new System.Windows.Forms.ComboBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dtpPemesananFilterStart = new System.Windows.Forms.DateTimePicker();
            this.lblPemesananFilterStart = new System.Windows.Forms.Label();
            this.dtpPemesananFilterEnd = new System.Windows.Forms.DateTimePicker();
            this.lblPemesananFilterEnd = new System.Windows.Forms.Label();
            this.dtpPembayaranFilterStart = new System.Windows.Forms.DateTimePicker();
            this.lblPembayaranFilterStart = new System.Windows.Forms.Label();
            this.dtpPembayaranFilterEnd = new System.Windows.Forms.DateTimePicker();
            this.lblPembayaranFilterEnd = new System.Windows.Forms.Label();
            this.btnFilter = new System.Windows.Forms.Button();
            this.comboBoxMetode = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPembayaran)).BeginInit();
            this.SuspendLayout();
            // 
            // btnTambah
            // 
            this.btnTambah.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTambah.Location = new System.Drawing.Point(729, 127);
            this.btnTambah.Name = "btnTambah";
            this.btnTambah.Size = new System.Drawing.Size(97, 33);
            this.btnTambah.TabIndex = 13;
            this.btnTambah.Text = "Tambah";
            this.btnTambah.UseVisualStyleBackColor = true;
            // 
            // btnHapus
            // 
            this.btnHapus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHapus.Location = new System.Drawing.Point(729, 166);
            this.btnHapus.Name = "btnHapus";
            this.btnHapus.Size = new System.Drawing.Size(97, 33);
            this.btnHapus.TabIndex = 14;
            this.btnHapus.Text = "Hapus";
            this.btnHapus.UseVisualStyleBackColor = true;
            // 
            // btnUbah
            // 
            this.btnUbah.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUbah.Location = new System.Drawing.Point(729, 205);
            this.btnUbah.Name = "btnUbah";
            this.btnUbah.Size = new System.Drawing.Size(97, 33);
            this.btnUbah.TabIndex = 15;
            this.btnUbah.Text = "Ubah";
            this.btnUbah.UseVisualStyleBackColor = true;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Location = new System.Drawing.Point(729, 244);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(97, 33);
            this.btnRefresh.TabIndex = 16;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            // 
            // lblJumlahPembayaran
            // 
            this.lblJumlahPembayaran.AutoSize = true;
            this.lblJumlahPembayaran.Location = new System.Drawing.Point(14, 135);
            this.lblJumlahPembayaran.Name = "lblJumlahPembayaran";
            this.lblJumlahPembayaran.Size = new System.Drawing.Size(165, 20);
            this.lblJumlahPembayaran.TabIndex = 4;
            this.lblJumlahPembayaran.Text = "Jumlah Pembayaran : ";
            // 
            // lblTanggalPembayaran
            // 
            this.lblTanggalPembayaran.AutoSize = true;
            this.lblTanggalPembayaran.Location = new System.Drawing.Point(14, 172);
            this.lblTanggalPembayaran.Name = "lblTanggalPembayaran";
            this.lblTanggalPembayaran.Size = new System.Drawing.Size(171, 20);
            this.lblTanggalPembayaran.TabIndex = 6;
            this.lblTanggalPembayaran.Text = "Tanggal Pembayaran : ";
            // 
            // lblMetodePembayaran
            // 
            this.lblMetodePembayaran.AutoSize = true;
            this.lblMetodePembayaran.Location = new System.Drawing.Point(14, 209);
            this.lblMetodePembayaran.Name = "lblMetodePembayaran";
            this.lblMetodePembayaran.Size = new System.Drawing.Size(168, 20);
            this.lblMetodePembayaran.TabIndex = 8;
            this.lblMetodePembayaran.Text = "Metode Pembayaran : ";
            // 
            // txtJumlah
            // 
            this.txtJumlah.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtJumlah.Location = new System.Drawing.Point(188, 132);
            this.txtJumlah.Name = "txtJumlah";
            this.txtJumlah.Size = new System.Drawing.Size(519, 26);
            this.txtJumlah.TabIndex = 5;
            // 
            // dgvPembayaran
            // 
            this.dgvPembayaran.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPembayaran.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPembayaran.Location = new System.Drawing.Point(18, 283);
            this.dgvPembayaran.Name = "dgvPembayaran";
            this.dgvPembayaran.RowHeadersWidth = 62;
            this.dgvPembayaran.RowTemplate.Height = 28;
            this.dgvPembayaran.Size = new System.Drawing.Size(808, 228);
            this.dgvPembayaran.TabIndex = 17;
            // 
            // cmbPemesanan
            // 
            this.cmbPemesanan.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbPemesanan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPemesanan.FormattingEnabled = true;
            this.cmbPemesanan.Location = new System.Drawing.Point(188, 94);
            this.cmbPemesanan.Name = "cmbPemesanan";
            this.cmbPemesanan.Size = new System.Drawing.Size(519, 28);
            this.cmbPemesanan.TabIndex = 3;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dateTimePicker1.Location = new System.Drawing.Point(188, 169);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(519, 26);
            this.dateTimePicker1.TabIndex = 7;
            // 
            // dtpPemesananFilterStart
            // 
            this.dtpPemesananFilterStart.Location = new System.Drawing.Point(188, 14);
            this.dtpPemesananFilterStart.Name = "dtpPemesananFilterStart";
            this.dtpPemesananFilterStart.Size = new System.Drawing.Size(200, 26);
            this.dtpPemesananFilterStart.TabIndex = 10;
            // 
            // lblPemesananFilterStart
            // 
            this.lblPemesananFilterStart.AutoSize = true;
            this.lblPemesananFilterStart.Location = new System.Drawing.Point(14, 19);
            this.lblPemesananFilterStart.Name = "lblPemesananFilterStart";
            this.lblPemesananFilterStart.Size = new System.Drawing.Size(163, 20);
            this.lblPemesananFilterStart.TabIndex = 18;
            this.lblPemesananFilterStart.Text = "Filter Tgl Pesan Dari : ";
            // 
            // dtpPemesananFilterEnd
            // 
            this.dtpPemesananFilterEnd.Location = new System.Drawing.Point(471, 14);
            this.dtpPemesananFilterEnd.Name = "dtpPemesananFilterEnd";
            this.dtpPemesananFilterEnd.Size = new System.Drawing.Size(200, 26);
            this.dtpPemesananFilterEnd.TabIndex = 11;
            // 
            // lblPemesananFilterEnd
            // 
            this.lblPemesananFilterEnd.AutoSize = true;
            this.lblPemesananFilterEnd.Location = new System.Drawing.Point(400, 19);
            this.lblPemesananFilterEnd.Name = "lblPemesananFilterEnd";
            this.lblPemesananFilterEnd.Size = new System.Drawing.Size(71, 20);
            this.lblPemesananFilterEnd.TabIndex = 19;
            this.lblPemesananFilterEnd.Text = "Sampai :";
            // 
            // dtpPembayaranFilterStart
            // 
            this.dtpPembayaranFilterStart.Location = new System.Drawing.Point(188, 50);
            this.dtpPembayaranFilterStart.Name = "dtpPembayaranFilterStart";
            this.dtpPembayaranFilterStart.Size = new System.Drawing.Size(200, 26);
            this.dtpPembayaranFilterStart.TabIndex = 12;
            // 
            // lblPembayaranFilterStart
            // 
            this.lblPembayaranFilterStart.AutoSize = true;
            this.lblPembayaranFilterStart.Location = new System.Drawing.Point(14, 55);
            this.lblPembayaranFilterStart.Name = "lblPembayaranFilterStart";
            this.lblPembayaranFilterStart.Size = new System.Drawing.Size(159, 20);
            this.lblPembayaranFilterStart.TabIndex = 20;
            this.lblPembayaranFilterStart.Text = "Filter Tgl Bayar Dari : ";
            // 
            // dtpPembayaranFilterEnd
            // 
            this.dtpPembayaranFilterEnd.Location = new System.Drawing.Point(471, 50);
            this.dtpPembayaranFilterEnd.Name = "dtpPembayaranFilterEnd";
            this.dtpPembayaranFilterEnd.Size = new System.Drawing.Size(200, 26);
            this.dtpPembayaranFilterEnd.TabIndex = 13;
            // 
            // lblPembayaranFilterEnd
            // 
            this.lblPembayaranFilterEnd.AutoSize = true;
            this.lblPembayaranFilterEnd.Location = new System.Drawing.Point(400, 55);
            this.lblPembayaranFilterEnd.Name = "lblPembayaranFilterEnd";
            this.lblPembayaranFilterEnd.Size = new System.Drawing.Size(71, 20);
            this.lblPembayaranFilterEnd.TabIndex = 21;
            this.lblPembayaranFilterEnd.Text = "Sampai :";
            // 
            // btnFilter
            // 
            this.btnFilter.Location = new System.Drawing.Point(681, 14);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(97, 62);
            this.btnFilter.TabIndex = 14;
            this.btnFilter.Text = "Filter";
            this.btnFilter.UseVisualStyleBackColor = true;
            // 
            // comboBoxMetode
            // 
            this.comboBoxMetode.FormattingEnabled = true;
            this.comboBoxMetode.Location = new System.Drawing.Point(188, 208);
            this.comboBoxMetode.Name = "comboBoxMetode";
            this.comboBoxMetode.Size = new System.Drawing.Size(519, 28);
            this.comboBoxMetode.TabIndex = 22;
            // 
            // Pembayaran
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(838, 523);
            this.Controls.Add(this.comboBoxMetode);
            this.Controls.Add(this.lblPemesananFilterStart);
            this.Controls.Add(this.dtpPemesananFilterStart);
            this.Controls.Add(this.lblPemesananFilterEnd);
            this.Controls.Add(this.dtpPemesananFilterEnd);
            this.Controls.Add(this.lblPembayaranFilterStart);
            this.Controls.Add(this.dtpPembayaranFilterStart);
            this.Controls.Add(this.lblPembayaranFilterEnd);
            this.Controls.Add(this.dtpPembayaranFilterEnd);
            this.Controls.Add(this.btnFilter);
            this.Controls.Add(this.cmbPemesanan);
            this.Controls.Add(this.lblJumlahPembayaran);
            this.Controls.Add(this.txtJumlah);
            this.Controls.Add(this.lblTanggalPembayaran);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.lblMetodePembayaran);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnUbah);
            this.Controls.Add(this.btnHapus);
            this.Controls.Add(this.btnTambah);
            this.Controls.Add(this.dgvPembayaran);
            this.Name = "Pembayaran";
            this.Text = "Form Pembayaran";
            this.Load += new System.EventHandler(this.Pembayaran_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPembayaran)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.ComboBox comboBoxMetode;

        #endregion

        // HAPUS DEKLARASI FILTER DARI SINI
        // private System.Windows.Forms.DateTimePicker dtpPemesananFilterStart; // Ganti nama dari dateTimePicker2
        // private System.Windows.Forms.Label lblPemesananFilterStart;
        // ... dan seterusnya untuk filter ...
    }
}