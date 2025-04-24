namespace BiroWisataForm
{
    partial class Driver
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
            this.lblNamaDriver = new System.Windows.Forms.Label();
            this.lblNoTel = new System.Windows.Forms.Label();
            this.lblNoSIM = new System.Windows.Forms.Label();
            this.txtNama = new System.Windows.Forms.TextBox();
            this.txtNoTel = new System.Windows.Forms.TextBox();
            this.txtNoSim = new System.Windows.Forms.TextBox();
            this.btnTambah = new System.Windows.Forms.Button();
            this.btnHapus = new System.Windows.Forms.Button();
            this.btnUbah = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.dgvDriver = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDriver)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNamaDriver
            // 
            this.lblNamaDriver.AutoSize = true;
            this.lblNamaDriver.Location = new System.Drawing.Point(68, 37);
            this.lblNamaDriver.Name = "lblNamaDriver";
            this.lblNamaDriver.Size = new System.Drawing.Size(104, 20);
            this.lblNamaDriver.TabIndex = 0;
            this.lblNamaDriver.Text = "Nama Driver :";
            // 
            // lblNoTel
            // 
            this.lblNoTel.AutoSize = true;
            this.lblNoTel.Location = new System.Drawing.Point(68, 78);
            this.lblNoTel.Name = "lblNoTel";
            this.lblNoTel.Size = new System.Drawing.Size(98, 20);
            this.lblNoTel.TabIndex = 1;
            this.lblNoTel.Text = "No Telepon :";
            // 
            // lblNoSIM
            // 
            this.lblNoSIM.AutoSize = true;
            this.lblNoSIM.Location = new System.Drawing.Point(68, 122);
            this.lblNoSIM.Name = "lblNoSIM";
            this.lblNoSIM.Size = new System.Drawing.Size(70, 20);
            this.lblNoSIM.TabIndex = 2;
            this.lblNoSIM.Text = "No SIM :";
            // 
            // txtNama
            // 
            this.txtNama.Location = new System.Drawing.Point(220, 34);
            this.txtNama.Name = "txtNama";
            this.txtNama.Size = new System.Drawing.Size(328, 26);
            this.txtNama.TabIndex = 3;
            // 
            // txtNoTel
            // 
            this.txtNoTel.Location = new System.Drawing.Point(220, 78);
            this.txtNoTel.Name = "txtNoTel";
            this.txtNoTel.Size = new System.Drawing.Size(328, 26);
            this.txtNoTel.TabIndex = 4;
            // 
            // txtNoSim
            // 
            this.txtNoSim.Location = new System.Drawing.Point(220, 122);
            this.txtNoSim.Name = "txtNoSim";
            this.txtNoSim.Size = new System.Drawing.Size(328, 26);
            this.txtNoSim.TabIndex = 5;
            // 
            // btnTambah
            // 
            this.btnTambah.Location = new System.Drawing.Point(644, 12);
            this.btnTambah.Name = "btnTambah";
            this.btnTambah.Size = new System.Drawing.Size(84, 39);
            this.btnTambah.TabIndex = 6;
            this.btnTambah.Text = "Tambah";
            this.btnTambah.UseVisualStyleBackColor = true;
            this.btnTambah.Click += new System.EventHandler(this.btnTambah_Click);
            // 
            // btnHapus
            // 
            this.btnHapus.Location = new System.Drawing.Point(644, 57);
            this.btnHapus.Name = "btnHapus";
            this.btnHapus.Size = new System.Drawing.Size(84, 39);
            this.btnHapus.TabIndex = 7;
            this.btnHapus.Text = "Hapus";
            this.btnHapus.UseVisualStyleBackColor = true;
            this.btnHapus.Click += new System.EventHandler(this.btnHapus_Click);
            // 
            // btnUbah
            // 
            this.btnUbah.Location = new System.Drawing.Point(644, 102);
            this.btnUbah.Name = "btnUbah";
            this.btnUbah.Size = new System.Drawing.Size(84, 39);
            this.btnUbah.TabIndex = 8;
            this.btnUbah.Text = "Ubah";
            this.btnUbah.UseVisualStyleBackColor = true;
            this.btnUbah.Click += new System.EventHandler(this.btnUbah_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(644, 147);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(84, 39);
            this.btnRefresh.TabIndex = 9;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // dgvDriver
            // 
            this.dgvDriver.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDriver.Location = new System.Drawing.Point(29, 206);
            this.dgvDriver.Name = "dgvDriver";
            this.dgvDriver.RowHeadersWidth = 62;
            this.dgvDriver.RowTemplate.Height = 28;
            this.dgvDriver.Size = new System.Drawing.Size(698, 230);
            this.dgvDriver.TabIndex = 10;
            // 
            // Driver
            // 
            this.ClientSize = new System.Drawing.Size(752, 460);
            this.Controls.Add(this.dgvDriver);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnUbah);
            this.Controls.Add(this.btnHapus);
            this.Controls.Add(this.btnTambah);
            this.Controls.Add(this.txtNoSim);
            this.Controls.Add(this.txtNoTel);
            this.Controls.Add(this.txtNama);
            this.Controls.Add(this.lblNoSIM);
            this.Controls.Add(this.lblNoTel);
            this.Controls.Add(this.lblNamaDriver);
            this.Name = "Driver";
            this.Text = "Driver";
            this.Load += new System.EventHandler(this.Driver_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDriver)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNamaDriver;
        private System.Windows.Forms.Label lblNoTel;
        private System.Windows.Forms.Label lblNoSIM;
        private System.Windows.Forms.TextBox txtNama;
        private System.Windows.Forms.TextBox txtNoTel;
        private System.Windows.Forms.TextBox txtNoSim;
        private System.Windows.Forms.Button btnTambah;
        private System.Windows.Forms.Button btnHapus;
        private System.Windows.Forms.Button btnUbah;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridView dgvDriver;
    }
}