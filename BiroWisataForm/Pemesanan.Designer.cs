namespace BiroWisataForm
{
    partial class Pemesanan
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
            this.dgvPemesanan = new System.Windows.Forms.DataGridView();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.txtTanggal = new System.Windows.Forms.TextBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblTanggal = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnUbah = new System.Windows.Forms.Button();
            this.btnHapus = new System.Windows.Forms.Button();
            this.btnTambah = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPemesanan)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvPemesanan
            // 
            this.dgvPemesanan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPemesanan.Location = new System.Drawing.Point(12, 208);
            this.dgvPemesanan.Name = "dgvPemesanan";
            this.dgvPemesanan.RowHeadersWidth = 62;
            this.dgvPemesanan.RowTemplate.Height = 28;
            this.dgvPemesanan.Size = new System.Drawing.Size(768, 255);
            this.dgvPemesanan.TabIndex = 23;
            // 
            // txtTotal
            // 
            this.txtTotal.Location = new System.Drawing.Point(222, 133);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Size = new System.Drawing.Size(454, 26);
            this.txtTotal.TabIndex = 22;
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(222, 78);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(454, 26);
            this.txtStatus.TabIndex = 21;
            // 
            // txtTanggal
            // 
            this.txtTanggal.Location = new System.Drawing.Point(222, 24);
            this.txtTanggal.Name = "txtTanggal";
            this.txtTanggal.Size = new System.Drawing.Size(454, 26);
            this.txtTanggal.TabIndex = 20;
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(52, 136);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(224, 30);
            this.lblTotal.TabIndex = 19;
            this.lblTotal.Text = "Total Pembayaran : ";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(52, 84);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(242, 30);
            this.lblStatus.TabIndex = 18;
            this.lblStatus.Text = "Status Pembayaran : ";
            // 
            // lblTanggal
            // 
            this.lblTanggal.AutoSize = true;
            this.lblTanggal.Location = new System.Drawing.Point(52, 27);
            this.lblTanggal.Name = "lblTanggal";
            this.lblTanggal.Size = new System.Drawing.Size(251, 30);
            this.lblTanggal.TabIndex = 17;
            this.lblTanggal.Text = "Tanggal Pemesanan : ";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(714, 137);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(87, 33);
            this.btnRefresh.TabIndex = 16;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            // 
            // btnUbah
            // 
            this.btnUbah.Location = new System.Drawing.Point(714, 98);
            this.btnUbah.Name = "btnUbah";
            this.btnUbah.Size = new System.Drawing.Size(87, 33);
            this.btnUbah.TabIndex = 15;
            this.btnUbah.Text = "Ubah";
            this.btnUbah.UseVisualStyleBackColor = true;
            // 
            // btnHapus
            // 
            this.btnHapus.Location = new System.Drawing.Point(714, 59);
            this.btnHapus.Name = "btnHapus";
            this.btnHapus.Size = new System.Drawing.Size(87, 33);
            this.btnHapus.TabIndex = 13;
            this.btnHapus.Text = "Hapus";
            this.btnHapus.UseVisualStyleBackColor = true;
            // 
            // btnTambah
            // 
            this.btnTambah.Location = new System.Drawing.Point(714, 20);
            this.btnTambah.Name = "btnTambah";
            this.btnTambah.Size = new System.Drawing.Size(87, 33);
            this.btnTambah.TabIndex = 11;
            this.btnTambah.Text = "Tambah";
            this.btnTambah.UseVisualStyleBackColor = true;
            // 
            // Pemesanan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(825, 524);
            this.Controls.Add(this.dgvPemesanan);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.txtTanggal);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblTanggal);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnUbah);
            this.Controls.Add(this.btnHapus);
            this.Controls.Add(this.btnTambah);
            this.Name = "Pemesanan";
            this.Text = "Pemesanan";
            ((System.ComponentModel.ISupportInitialize)(this.dgvPemesanan)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvPemesanan;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.TextBox txtTanggal;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblTanggal;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnUbah;
        private System.Windows.Forms.Button btnHapus;
        private System.Windows.Forms.Button btnTambah;
    }
}