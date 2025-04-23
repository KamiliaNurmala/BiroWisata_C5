namespace BiroWisataForm
{
    partial class Kendaraan
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
            this.dgvKendaraan = new System.Windows.Forms.DataGridView();
            this.lblJenis = new System.Windows.Forms.Label();
            this.lblPlatNomor = new System.Windows.Forms.Label();
            this.lblKapasitas = new System.Windows.Forms.Label();
            this.txtJenis = new System.Windows.Forms.TextBox();
            this.txtPlatNomor = new System.Windows.Forms.TextBox();
            this.txtKapasitas = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKendaraan)).BeginInit();
            this.SuspendLayout();
            // 
            // btnTambah
            // 
            this.btnTambah.Location = new System.Drawing.Point(673, 24);
            this.btnTambah.Name = "btnTambah";
            this.btnTambah.Size = new System.Drawing.Size(86, 31);
            this.btnTambah.TabIndex = 0;
            this.btnTambah.Text = "Tambah";
            this.btnTambah.UseVisualStyleBackColor = true;
            this.btnTambah.Click += new System.EventHandler(this.btnTambah_Click);
            // 
            // btnHapus
            // 
            this.btnHapus.Location = new System.Drawing.Point(673, 61);
            this.btnHapus.Name = "btnHapus";
            this.btnHapus.Size = new System.Drawing.Size(86, 31);
            this.btnHapus.TabIndex = 1;
            this.btnHapus.Text = "Hapus";
            this.btnHapus.UseVisualStyleBackColor = true;
            this.btnHapus.Click += new System.EventHandler(this.btnHapus_Click);
            // 
            // btnUbah
            // 
            this.btnUbah.Location = new System.Drawing.Point(673, 98);
            this.btnUbah.Name = "btnUbah";
            this.btnUbah.Size = new System.Drawing.Size(86, 31);
            this.btnUbah.TabIndex = 2;
            this.btnUbah.Text = "Ubah";
            this.btnUbah.UseVisualStyleBackColor = true;
            this.btnUbah.Click += new System.EventHandler(this.btnUbah_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(673, 135);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(86, 31);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // dgvKendaraan
            // 
            this.dgvKendaraan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKendaraan.Location = new System.Drawing.Point(51, 192);
            this.dgvKendaraan.Name = "dgvKendaraan";
            this.dgvKendaraan.RowHeadersWidth = 62;
            this.dgvKendaraan.RowTemplate.Height = 28;
            this.dgvKendaraan.Size = new System.Drawing.Size(708, 232);
            this.dgvKendaraan.TabIndex = 4;
            // 
            // lblJenis
            // 
            this.lblJenis.AutoSize = true;
            this.lblJenis.Location = new System.Drawing.Point(47, 32);
            this.lblJenis.Name = "lblJenis";
            this.lblJenis.Size = new System.Drawing.Size(87, 30);
            this.lblJenis.TabIndex = 5;
            this.lblJenis.Text = "Jenis : ";
            // 
            // lblPlatNomor
            // 
            this.lblPlatNomor.AutoSize = true;
            this.lblPlatNomor.Location = new System.Drawing.Point(47, 80);
            this.lblPlatNomor.Name = "lblPlatNomor";
            this.lblPlatNomor.Size = new System.Drawing.Size(143, 30);
            this.lblPlatNomor.TabIndex = 6;
            this.lblPlatNomor.Text = "Plat Nomor :";
            // 
            // lblKapasitas
            // 
            this.lblKapasitas.AutoSize = true;
            this.lblKapasitas.Location = new System.Drawing.Point(47, 130);
            this.lblKapasitas.Name = "lblKapasitas";
            this.lblKapasitas.Size = new System.Drawing.Size(137, 30);
            this.lblKapasitas.TabIndex = 7;
            this.lblKapasitas.Text = "Kapasitas : ";
            // 
            // txtJenis
            // 
            this.txtJenis.Location = new System.Drawing.Point(149, 26);
            this.txtJenis.Name = "txtJenis";
            this.txtJenis.Size = new System.Drawing.Size(483, 26);
            this.txtJenis.TabIndex = 8;
            // 
            // txtPlatNomor
            // 
            this.txtPlatNomor.Location = new System.Drawing.Point(149, 77);
            this.txtPlatNomor.Name = "txtPlatNomor";
            this.txtPlatNomor.Size = new System.Drawing.Size(483, 26);
            this.txtPlatNomor.TabIndex = 9;
            // 
            // txtKapasitas
            // 
            this.txtKapasitas.Location = new System.Drawing.Point(149, 130);
            this.txtKapasitas.Name = "txtKapasitas";
            this.txtKapasitas.Size = new System.Drawing.Size(483, 26);
            this.txtKapasitas.TabIndex = 10;
            // 
            // Kendaraan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtKapasitas);
            this.Controls.Add(this.txtPlatNomor);
            this.Controls.Add(this.txtJenis);
            this.Controls.Add(this.lblKapasitas);
            this.Controls.Add(this.lblPlatNomor);
            this.Controls.Add(this.lblJenis);
            this.Controls.Add(this.dgvKendaraan);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnUbah);
            this.Controls.Add(this.btnHapus);
            this.Controls.Add(this.btnTambah);
            this.Name = "Kendaraan";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Kendaraan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvKendaraan)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTambah;
        private System.Windows.Forms.Button btnHapus;
        private System.Windows.Forms.Button btnUbah;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridView dgvKendaraan;
        private System.Windows.Forms.Label lblJenis;
        private System.Windows.Forms.Label lblPlatNomor;
        private System.Windows.Forms.Label lblKapasitas;
        private System.Windows.Forms.TextBox txtJenis;
        private System.Windows.Forms.TextBox txtPlatNomor;
        private System.Windows.Forms.TextBox txtKapasitas;
    }
}