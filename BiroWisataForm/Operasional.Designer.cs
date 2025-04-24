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
            this.dgvOperasional = new System.Windows.Forms.DataGridView();
            this.txtBiaya = new System.Windows.Forms.TextBox();
            this.lblJenisPengeluaran = new System.Windows.Forms.Label();
            this.lblBiayaBBM = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnUbah = new System.Windows.Forms.Button();
            this.btnHapus = new System.Windows.Forms.Button();
            this.btnTambah = new System.Windows.Forms.Button();
            this.cmbPaket = new System.Windows.Forms.ComboBox();
            this.cmbDriver = new System.Windows.Forms.ComboBox();
            this.cmbKendaraan = new System.Windows.Forms.ComboBox();
            this.cmbJenisPengeluaran = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOperasional)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvOperasional
            // 
            this.dgvOperasional.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOperasional.Location = new System.Drawing.Point(31, 211);
            this.dgvOperasional.Name = "dgvOperasional";
            this.dgvOperasional.RowHeadersWidth = 62;
            this.dgvOperasional.RowTemplate.Height = 28;
            this.dgvOperasional.Size = new System.Drawing.Size(768, 255);
            this.dgvOperasional.TabIndex = 23;
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
            this.lblBiayaBBM.Size = new System.Drawing.Size(64, 20);
            this.lblBiayaBBM.TabIndex = 18;
            this.lblBiayaBBM.Text = "Biaya  : ";
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
            // cmbPaket
            // 
            this.cmbPaket.FormattingEnabled = true;
            this.cmbPaket.Location = new System.Drawing.Point(56, 487);
            this.cmbPaket.Name = "cmbPaket";
            this.cmbPaket.Size = new System.Drawing.Size(121, 28);
            this.cmbPaket.TabIndex = 24;
            // 
            // cmbDriver
            // 
            this.cmbDriver.FormattingEnabled = true;
            this.cmbDriver.Location = new System.Drawing.Point(239, 487);
            this.cmbDriver.Name = "cmbDriver";
            this.cmbDriver.Size = new System.Drawing.Size(121, 28);
            this.cmbDriver.TabIndex = 25;
            // 
            // cmbKendaraan
            // 
            this.cmbKendaraan.FormattingEnabled = true;
            this.cmbKendaraan.Location = new System.Drawing.Point(411, 487);
            this.cmbKendaraan.Name = "cmbKendaraan";
            this.cmbKendaraan.Size = new System.Drawing.Size(121, 28);
            this.cmbKendaraan.TabIndex = 26;
            // 
            // cmbJenisPengeluaran
            // 
            this.cmbJenisPengeluaran.FormattingEnabled = true;
            this.cmbJenisPengeluaran.Location = new System.Drawing.Point(239, 101);
            this.cmbJenisPengeluaran.Name = "cmbJenisPengeluaran";
            this.cmbJenisPengeluaran.Size = new System.Drawing.Size(454, 28);
            this.cmbJenisPengeluaran.TabIndex = 27;
            this.cmbJenisPengeluaran.SelectedIndexChanged += new System.EventHandler(this.cmbJenisPengeluaran_SelectedIndexChanged);
            // 
            // Operasional
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 534);
            this.Controls.Add(this.cmbJenisPengeluaran);
            this.Controls.Add(this.cmbKendaraan);
            this.Controls.Add(this.cmbDriver);
            this.Controls.Add(this.cmbPaket);
            this.Controls.Add(this.dgvOperasional);
            this.Controls.Add(this.txtBiaya);
            this.Controls.Add(this.lblJenisPengeluaran);
            this.Controls.Add(this.lblBiayaBBM);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnUbah);
            this.Controls.Add(this.btnHapus);
            this.Controls.Add(this.btnTambah);
            this.Name = "Operasional";
            this.Text = "Operasional";
            this.Load += new System.EventHandler(this.Operasional_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOperasional)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvOperasional;
        private System.Windows.Forms.TextBox txtBiaya;
        private System.Windows.Forms.Label lblJenisPengeluaran;
        private System.Windows.Forms.Label lblBiayaBBM;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnUbah;
        private System.Windows.Forms.Button btnHapus;
        private System.Windows.Forms.Button btnTambah;
        private System.Windows.Forms.ComboBox cmbPaket;
        private System.Windows.Forms.ComboBox cmbDriver;
        private System.Windows.Forms.ComboBox cmbKendaraan;
        private System.Windows.Forms.ComboBox cmbJenisPengeluaran;
    }
}