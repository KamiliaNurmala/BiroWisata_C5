namespace BiroWisataForm
{
    partial class Operasional
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
            this.dgvPembayaran = new System.Windows.Forms.DataGridView();
            this.txtJenisPengeluaran = new System.Windows.Forms.TextBox();
            this.txtBiaya = new System.Windows.Forms.TextBox();
            this.lblJenisPengeluaran = new System.Windows.Forms.Label();
            this.lblBiayaBBM = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnUbah = new System.Windows.Forms.Button();
            this.btnHapus = new System.Windows.Forms.Button();
            this.btnTambah = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPembayaran)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvPembayaran
            // 
            this.dgvPembayaran.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPembayaran.Location = new System.Drawing.Point(31, 211);
            this.dgvPembayaran.Name = "dgvPembayaran";
            this.dgvPembayaran.RowHeadersWidth = 62;
            this.dgvPembayaran.RowTemplate.Height = 28;
            this.dgvPembayaran.Size = new System.Drawing.Size(768, 255);
            this.dgvPembayaran.TabIndex = 23;
            // 
            // txtJenisPengeluaran
            // 
            this.txtJenisPengeluaran.Location = new System.Drawing.Point(239, 104);
            this.txtJenisPengeluaran.Name = "txtJenisPengeluaran";
            this.txtJenisPengeluaran.Size = new System.Drawing.Size(454, 26);
            this.txtJenisPengeluaran.TabIndex = 22;
            // 
            // txtBiaya
            // 
            this.txtBiaya.Location = new System.Drawing.Point(239, 49);
            this.txtBiaya.Name = "txtBiaya";
            this.txtBiaya.Size = new System.Drawing.Size(454, 26);
            this.txtBiaya.TabIndex = 21;
            // 
            // lblJenisPengeluaran
            // 
            this.lblJenisPengeluaran.AutoSize = true;
            this.lblJenisPengeluaran.Location = new System.Drawing.Point(69, 107);
            this.lblJenisPengeluaran.Name = "lblJenisPengeluaran";
            this.lblJenisPengeluaran.Size = new System.Drawing.Size(152, 20);
            this.lblJenisPengeluaran.TabIndex = 19;
            this.lblJenisPengeluaran.Text = "Jenis Pengeluaran : ";
            // 
            // lblBiayaBBM
            // 
            this.lblBiayaBBM.AutoSize = true;
            this.lblBiayaBBM.Location = new System.Drawing.Point(69, 55);
            this.lblBiayaBBM.Name = "lblBiayaBBM";
            this.lblBiayaBBM.Size = new System.Drawing.Size(99, 20);
            this.lblBiayaBBM.TabIndex = 18;
            this.lblBiayaBBM.Text = "Biaya BBM : ";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(733, 140);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(87, 33);
            this.btnRefresh.TabIndex = 16;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            // 
            // btnUbah
            // 
            this.btnUbah.Location = new System.Drawing.Point(733, 101);
            this.btnUbah.Name = "btnUbah";
            this.btnUbah.Size = new System.Drawing.Size(87, 33);
            this.btnUbah.TabIndex = 15;
            this.btnUbah.Text = "Ubah";
            this.btnUbah.UseVisualStyleBackColor = true;
            // 
            // btnHapus
            // 
            this.btnHapus.Location = new System.Drawing.Point(733, 62);
            this.btnHapus.Name = "btnHapus";
            this.btnHapus.Size = new System.Drawing.Size(87, 33);
            this.btnHapus.TabIndex = 13;
            this.btnHapus.Text = "Hapus";
            this.btnHapus.UseVisualStyleBackColor = true;
            // 
            // btnTambah
            // 
            this.btnTambah.Location = new System.Drawing.Point(733, 23);
            this.btnTambah.Name = "btnTambah";
            this.btnTambah.Size = new System.Drawing.Size(87, 33);
            this.btnTambah.TabIndex = 11;
            this.btnTambah.Text = "Tambah";
            this.btnTambah.UseVisualStyleBackColor = true;
            // 
            // Operasional
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 534);
            this.Controls.Add(this.dgvPembayaran);
            this.Controls.Add(this.txtJenisPengeluaran);
            this.Controls.Add(this.txtBiaya);
            this.Controls.Add(this.lblJenisPengeluaran);
            this.Controls.Add(this.lblBiayaBBM);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnUbah);
            this.Controls.Add(this.btnHapus);
            this.Controls.Add(this.btnTambah);
            this.Name = "Operasional";
            this.Text = "Operasional";
            ((System.ComponentModel.ISupportInitialize)(this.dgvPembayaran)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvPembayaran;
        private System.Windows.Forms.TextBox txtJenisPengeluaran;
        private System.Windows.Forms.TextBox txtBiaya;
        private System.Windows.Forms.Label lblJenisPengeluaran;
        private System.Windows.Forms.Label lblBiayaBBM;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnUbah;
        private System.Windows.Forms.Button btnHapus;
        private System.Windows.Forms.Button btnTambah;
    }
}