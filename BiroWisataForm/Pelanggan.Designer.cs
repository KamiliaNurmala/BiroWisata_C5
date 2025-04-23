namespace BiroWisataForm
{
    partial class Pelanggan
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
            this.lblNama = new System.Windows.Forms.Label();
            this.lblNoTelp = new System.Windows.Forms.Label();
            this.lblAlamat = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtNama = new System.Windows.Forms.TextBox();
            this.dgvPelanggan = new System.Windows.Forms.DataGridView();
            this.btnTambah = new System.Windows.Forms.Button();
            this.btnHapus = new System.Windows.Forms.Button();
            this.btnUbah = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.txtAlamat = new System.Windows.Forms.TextBox();
            this.txtNoTelp = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPelanggan)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNama
            // 
            this.lblNama.AutoSize = true;
            this.lblNama.Location = new System.Drawing.Point(48, 26);
            this.lblNama.Name = "lblNama";
            this.lblNama.Size = new System.Drawing.Size(143, 20);
            this.lblNama.TabIndex = 0;
            this.lblNama.Text = "Nama Pelanggan : ";
            // 
            // lblNoTelp
            // 
            this.lblNoTelp.AutoSize = true;
            this.lblNoTelp.Location = new System.Drawing.Point(48, 110);
            this.lblNoTelp.Name = "lblNoTelp";
            this.lblNoTelp.Size = new System.Drawing.Size(102, 20);
            this.lblNoTelp.TabIndex = 1;
            this.lblNoTelp.Text = "No Telepon : ";
            // 
            // lblAlamat
            // 
            this.lblAlamat.AutoSize = true;
            this.lblAlamat.Location = new System.Drawing.Point(48, 66);
            this.lblAlamat.Name = "lblAlamat";
            this.lblAlamat.Size = new System.Drawing.Size(67, 20);
            this.lblAlamat.TabIndex = 2;
            this.lblAlamat.Text = "Alamat :";
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(48, 149);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(56, 20);
            this.lblEmail.TabIndex = 3;
            this.lblEmail.Text = "Email :";
            // 
            // txtNama
            // 
            this.txtNama.Location = new System.Drawing.Point(234, 23);
            this.txtNama.Name = "txtNama";
            this.txtNama.Size = new System.Drawing.Size(301, 26);
            this.txtNama.TabIndex = 4;
            // 
            // dgvPelanggan
            // 
            this.dgvPelanggan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPelanggan.Location = new System.Drawing.Point(41, 201);
            this.dgvPelanggan.Name = "dgvPelanggan";
            this.dgvPelanggan.RowHeadersWidth = 62;
            this.dgvPelanggan.RowTemplate.Height = 28;
            this.dgvPelanggan.Size = new System.Drawing.Size(706, 223);
            this.dgvPelanggan.TabIndex = 8;
            // 
            // btnTambah
            // 
            this.btnTambah.Location = new System.Drawing.Point(634, 24);
            this.btnTambah.Name = "btnTambah";
            this.btnTambah.Size = new System.Drawing.Size(113, 32);
            this.btnTambah.TabIndex = 9;
            this.btnTambah.Text = "Tambah";
            this.btnTambah.UseVisualStyleBackColor = true;
            this.btnTambah.Click += new System.EventHandler(this.btnTambah_Click);
            // 
            // btnHapus
            // 
            this.btnHapus.Location = new System.Drawing.Point(634, 66);
            this.btnHapus.Name = "btnHapus";
            this.btnHapus.Size = new System.Drawing.Size(113, 32);
            this.btnHapus.TabIndex = 10;
            this.btnHapus.Text = "Hapus";
            this.btnHapus.UseVisualStyleBackColor = true;
            this.btnHapus.Click += new System.EventHandler(this.btnHapus_Click);
            // 
            // btnUbah
            // 
            this.btnUbah.Location = new System.Drawing.Point(634, 110);
            this.btnUbah.Name = "btnUbah";
            this.btnUbah.Size = new System.Drawing.Size(113, 32);
            this.btnUbah.TabIndex = 11;
            this.btnUbah.Text = "Ubah";
            this.btnUbah.UseVisualStyleBackColor = true;
            this.btnUbah.Click += new System.EventHandler(this.btnUbah_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(634, 149);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(113, 32);
            this.btnRefresh.TabIndex = 12;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // txtAlamat
            // 
            this.txtAlamat.Location = new System.Drawing.Point(234, 60);
            this.txtAlamat.Name = "txtAlamat";
            this.txtAlamat.Size = new System.Drawing.Size(301, 26);
            this.txtAlamat.TabIndex = 13;
            // 
            // txtNoTelp
            // 
            this.txtNoTelp.Location = new System.Drawing.Point(234, 104);
            this.txtNoTelp.Name = "txtNoTelp";
            this.txtNoTelp.Size = new System.Drawing.Size(301, 26);
            this.txtNoTelp.TabIndex = 14;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(234, 149);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(301, 26);
            this.txtEmail.TabIndex = 15;
            // 
            // Pelanggan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtNoTelp);
            this.Controls.Add(this.txtAlamat);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnUbah);
            this.Controls.Add(this.btnHapus);
            this.Controls.Add(this.btnTambah);
            this.Controls.Add(this.dgvPelanggan);
            this.Controls.Add(this.txtNama);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.lblAlamat);
            this.Controls.Add(this.lblNoTelp);
            this.Controls.Add(this.lblNama);
            this.Name = "Pelanggan";
            this.Text = "Pelanggan";
            this.Load += new System.EventHandler(this.Pelanggan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPelanggan)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNama;
        private System.Windows.Forms.Label lblNoTelp;
        private System.Windows.Forms.Label lblAlamat;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtNama;
        private System.Windows.Forms.DataGridView dgvPelanggan;
        private System.Windows.Forms.Button btnTambah;
        private System.Windows.Forms.Button btnHapus;
        private System.Windows.Forms.Button btnUbah;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.TextBox txtAlamat;
        private System.Windows.Forms.TextBox txtNoTelp;
        private System.Windows.Forms.TextBox txtEmail;
    }
}