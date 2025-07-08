// Operasional.Designer.cs
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.inputPanel = new System.Windows.Forms.Panel();
            this.lblPaket = new System.Windows.Forms.Label();
            this.cmbPaket = new System.Windows.Forms.ComboBox();
            this.lblDriver = new System.Windows.Forms.Label();
            this.cmbDriver = new System.Windows.Forms.ComboBox();
            this.lblKendaraan = new System.Windows.Forms.Label();
            this.cmbKendaraan = new System.Windows.Forms.ComboBox();
            this.lblJenisPengeluaran = new System.Windows.Forms.Label();
            this.cmbJenisPengeluaran = new System.Windows.Forms.ComboBox();
            this.lblBiaya = new System.Windows.Forms.Label();
            this.txtBiaya = new System.Windows.Forms.TextBox();
            this.btnTambah = new System.Windows.Forms.Button();
            this.btnHapus = new System.Windows.Forms.Button();
            this.btnUbah = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblCari = new System.Windows.Forms.Label();
            this.txtCari = new System.Windows.Forms.TextBox();
            this.dgvOperasional = new System.Windows.Forms.DataGridView();
            this.colIDOperasional = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNamaPaket = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNamaDriver = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colKendaraanInfo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colJenisPengeluaran = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBiayaOperasional = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIDPaket = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIDDriver = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIDKendaraan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnKembali = new System.Windows.Forms.Button();
            this.inputPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOperasional)).BeginInit();
            this.SuspendLayout();
            // 
            // inputPanel
            // 
            this.inputPanel.BackColor = System.Drawing.Color.White;
            this.inputPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inputPanel.Controls.Add(this.lblPaket);
            this.inputPanel.Controls.Add(this.cmbPaket);
            this.inputPanel.Controls.Add(this.lblDriver);
            this.inputPanel.Controls.Add(this.cmbDriver);
            this.inputPanel.Controls.Add(this.lblKendaraan);
            this.inputPanel.Controls.Add(this.cmbKendaraan);
            this.inputPanel.Controls.Add(this.lblJenisPengeluaran);
            this.inputPanel.Controls.Add(this.cmbJenisPengeluaran);
            this.inputPanel.Controls.Add(this.lblBiaya);
            this.inputPanel.Controls.Add(this.txtBiaya);
            this.inputPanel.Location = new System.Drawing.Point(20, 20);
            this.inputPanel.Name = "inputPanel";
            this.inputPanel.Size = new System.Drawing.Size(540, 230);
            this.inputPanel.TabIndex = 0;
            this.inputPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.inputPanel_Paint);
            // 
            // lblPaket
            // 
            this.lblPaket.AutoSize = true;
            this.lblPaket.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPaket.Location = new System.Drawing.Point(15, 23);
            this.lblPaket.Name = "lblPaket";
            this.lblPaket.Size = new System.Drawing.Size(83, 15);
            this.lblPaket.TabIndex = 0;
            this.lblPaket.Text = "Paket Wisata : ";
            // 
            // cmbPaket
            // 
            this.cmbPaket.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPaket.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbPaket.FormattingEnabled = true;
            this.cmbPaket.Location = new System.Drawing.Point(190, 20);
            this.cmbPaket.Name = "cmbPaket";
            this.cmbPaket.Size = new System.Drawing.Size(330, 23);
            this.cmbPaket.TabIndex = 1;
            // 
            // lblDriver
            // 
            this.lblDriver.AutoSize = true;
            this.lblDriver.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDriver.Location = new System.Drawing.Point(15, 63);
            this.lblDriver.Name = "lblDriver";
            this.lblDriver.Size = new System.Drawing.Size(47, 15);
            this.lblDriver.TabIndex = 2;
            this.lblDriver.Text = "Driver : ";
            // 
            // cmbDriver
            // 
            this.cmbDriver.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDriver.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbDriver.FormattingEnabled = true;
            this.cmbDriver.Location = new System.Drawing.Point(190, 60);
            this.cmbDriver.Name = "cmbDriver";
            this.cmbDriver.Size = new System.Drawing.Size(330, 23);
            this.cmbDriver.TabIndex = 3;
            // 
            // lblKendaraan
            // 
            this.lblKendaraan.AutoSize = true;
            this.lblKendaraan.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblKendaraan.Location = new System.Drawing.Point(15, 103);
            this.lblKendaraan.Name = "lblKendaraan";
            this.lblKendaraan.Size = new System.Drawing.Size(72, 15);
            this.lblKendaraan.TabIndex = 4;
            this.lblKendaraan.Text = "Kendaraan : ";
            // 
            // cmbKendaraan
            // 
            this.cmbKendaraan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbKendaraan.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbKendaraan.FormattingEnabled = true;
            this.cmbKendaraan.Location = new System.Drawing.Point(190, 100);
            this.cmbKendaraan.Name = "cmbKendaraan";
            this.cmbKendaraan.Size = new System.Drawing.Size(330, 23);
            this.cmbKendaraan.TabIndex = 5;
            // 
            // lblJenisPengeluaran
            // 
            this.lblJenisPengeluaran.AutoSize = true;
            this.lblJenisPengeluaran.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblJenisPengeluaran.Location = new System.Drawing.Point(15, 143);
            this.lblJenisPengeluaran.Name = "lblJenisPengeluaran";
            this.lblJenisPengeluaran.Size = new System.Drawing.Size(110, 15);
            this.lblJenisPengeluaran.TabIndex = 6;
            this.lblJenisPengeluaran.Text = "Jenis Pengeluaran : ";
            // 
            // cmbJenisPengeluaran
            // 
            this.cmbJenisPengeluaran.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbJenisPengeluaran.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbJenisPengeluaran.FormattingEnabled = true;
            this.cmbJenisPengeluaran.Location = new System.Drawing.Point(190, 140);
            this.cmbJenisPengeluaran.Name = "cmbJenisPengeluaran";
            this.cmbJenisPengeluaran.Size = new System.Drawing.Size(330, 23);
            this.cmbJenisPengeluaran.TabIndex = 7;
            this.cmbJenisPengeluaran.SelectedIndexChanged += new System.EventHandler(this.cmbJenisPengeluaran_SelectedIndexChanged);
            // 
            // lblBiaya
            // 
            this.lblBiaya.AutoSize = true;
            this.lblBiaya.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblBiaya.Location = new System.Drawing.Point(15, 183);
            this.lblBiaya.Name = "lblBiaya";
            this.lblBiaya.Size = new System.Drawing.Size(44, 15);
            this.lblBiaya.TabIndex = 8;
            this.lblBiaya.Text = "Biaya : ";
            // 
            // txtBiaya
            // 
            this.txtBiaya.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtBiaya.Location = new System.Drawing.Point(190, 180);
            this.txtBiaya.Name = "txtBiaya";
            this.txtBiaya.Size = new System.Drawing.Size(330, 23);
            this.txtBiaya.TabIndex = 9;
            // 
            // btnTambah
            // 
            this.btnTambah.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnTambah.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTambah.FlatAppearance.BorderSize = 0;
            this.btnTambah.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTambah.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnTambah.ForeColor = System.Drawing.Color.White;
            this.btnTambah.Location = new System.Drawing.Point(580, 24);
            this.btnTambah.Name = "btnTambah";
            this.btnTambah.Size = new System.Drawing.Size(120, 40);
            this.btnTambah.TabIndex = 10;
            this.btnTambah.Text = "Tambah";
            this.btnTambah.UseVisualStyleBackColor = false;
            this.btnTambah.Click += new System.EventHandler(this.btnTambah_Click);
            // 
            // btnHapus
            // 
            this.btnHapus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnHapus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHapus.FlatAppearance.BorderSize = 0;
            this.btnHapus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHapus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnHapus.ForeColor = System.Drawing.Color.White;
            this.btnHapus.Location = new System.Drawing.Point(580, 121);
            this.btnHapus.Name = "btnHapus";
            this.btnHapus.Size = new System.Drawing.Size(120, 40);
            this.btnHapus.TabIndex = 11;
            this.btnHapus.Text = "Hapus";
            this.btnHapus.UseVisualStyleBackColor = false;
            this.btnHapus.Click += new System.EventHandler(this.btnHapus_Click);
            // 
            // btnUbah
            // 
            this.btnUbah.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnUbah.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUbah.FlatAppearance.BorderSize = 0;
            this.btnUbah.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUbah.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnUbah.ForeColor = System.Drawing.Color.White;
            this.btnUbah.Location = new System.Drawing.Point(580, 71);
            this.btnUbah.Name = "btnUbah";
            this.btnUbah.Size = new System.Drawing.Size(120, 40);
            this.btnUbah.TabIndex = 12;
            this.btnUbah.Text = "Ubah";
            this.btnUbah.UseVisualStyleBackColor = false;
            this.btnUbah.Click += new System.EventHandler(this.btnUbah_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(580, 167);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(120, 40);
            this.btnRefresh.TabIndex = 13;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lblCari
            // 
            this.lblCari.AutoSize = true;
            this.lblCari.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCari.Location = new System.Drawing.Point(17, 268);
            this.lblCari.Name = "lblCari";
            this.lblCari.Size = new System.Drawing.Size(34, 15);
            this.lblCari.TabIndex = 15;
            this.lblCari.Text = "Cari :";
            // 
            // txtCari
            // 
            this.txtCari.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtCari.Location = new System.Drawing.Point(80, 265);
            this.txtCari.Name = "txtCari";
            this.txtCari.Size = new System.Drawing.Size(480, 23);
            this.txtCari.TabIndex = 16;
            // 
            // dgvOperasional
            // 
            this.dgvOperasional.AllowUserToAddRows = false;
            this.dgvOperasional.AllowUserToDeleteRows = false;
            this.dgvOperasional.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvOperasional.BackgroundColor = System.Drawing.Color.White;
            this.dgvOperasional.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvOperasional.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvOperasional.ColumnHeadersHeight = 34;
            this.dgvOperasional.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvOperasional.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colIDOperasional,
            this.colNamaPaket,
            this.colNamaDriver,
            this.colKendaraanInfo,
            this.colJenisPengeluaran,
            this.colBiayaOperasional,
            this.colIDPaket,
            this.colIDDriver,
            this.colIDKendaraan});
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvOperasional.DefaultCellStyle = dataGridViewCellStyle9;
            this.dgvOperasional.EnableHeadersVisualStyles = false;
            this.dgvOperasional.GridColor = System.Drawing.Color.Gainsboro;
            this.dgvOperasional.Location = new System.Drawing.Point(20, 308);
            this.dgvOperasional.MultiSelect = false;
            this.dgvOperasional.Name = "dgvOperasional";
            this.dgvOperasional.ReadOnly = true;
            this.dgvOperasional.RowHeadersVisible = false;
            this.dgvOperasional.RowTemplate.Height = 28;
            this.dgvOperasional.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOperasional.Size = new System.Drawing.Size(676, 216);
            this.dgvOperasional.TabIndex = 17;
            // 
            // colIDOperasional
            // 
            this.colIDOperasional.DataPropertyName = "IDOperasional";
            this.colIDOperasional.HeaderText = "ID";
            this.colIDOperasional.Name = "colIDOperasional";
            this.colIDOperasional.ReadOnly = true;
            this.colIDOperasional.Visible = false;
            // 
            // colNamaPaket
            // 
            this.colNamaPaket.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colNamaPaket.DataPropertyName = "NamaPaket";
            this.colNamaPaket.HeaderText = "Paket Wisata";
            this.colNamaPaket.MinimumWidth = 150;
            this.colNamaPaket.Name = "colNamaPaket";
            this.colNamaPaket.ReadOnly = true;
            // 
            // colNamaDriver
            // 
            this.colNamaDriver.DataPropertyName = "NamaDriver";
            this.colNamaDriver.HeaderText = "Driver";
            this.colNamaDriver.Name = "colNamaDriver";
            this.colNamaDriver.ReadOnly = true;
            this.colNamaDriver.Width = 120;
            // 
            // colKendaraanInfo
            // 
            this.colKendaraanInfo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colKendaraanInfo.DataPropertyName = "KendaraanInfo";
            this.colKendaraanInfo.HeaderText = "Kendaraan";
            this.colKendaraanInfo.MinimumWidth = 150;
            this.colKendaraanInfo.Name = "colKendaraanInfo";
            this.colKendaraanInfo.ReadOnly = true;
            // 
            // colJenisPengeluaran
            // 
            this.colJenisPengeluaran.DataPropertyName = "JenisPengeluaran";
            this.colJenisPengeluaran.HeaderText = "Jenis";
            this.colJenisPengeluaran.Name = "colJenisPengeluaran";
            this.colJenisPengeluaran.ReadOnly = true;
            this.colJenisPengeluaran.Width = 110;
            // 
            // colBiayaOperasional
            // 
            this.colBiayaOperasional.DataPropertyName = "BiayaOperasional";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.Format = "N0";
            dataGridViewCellStyle8.NullValue = null;
            this.colBiayaOperasional.DefaultCellStyle = dataGridViewCellStyle8;
            this.colBiayaOperasional.HeaderText = "Biaya (Rp)";
            this.colBiayaOperasional.Name = "colBiayaOperasional";
            this.colBiayaOperasional.ReadOnly = true;
            this.colBiayaOperasional.Width = 120;
            // 
            // colIDPaket
            // 
            this.colIDPaket.DataPropertyName = "IDPaket";
            this.colIDPaket.HeaderText = "IDPaket";
            this.colIDPaket.Name = "colIDPaket";
            this.colIDPaket.ReadOnly = true;
            this.colIDPaket.Visible = false;
            // 
            // colIDDriver
            // 
            this.colIDDriver.DataPropertyName = "IDDriver";
            this.colIDDriver.HeaderText = "IDDriver";
            this.colIDDriver.Name = "colIDDriver";
            this.colIDDriver.ReadOnly = true;
            this.colIDDriver.Visible = false;
            // 
            // colIDKendaraan
            // 
            this.colIDKendaraan.DataPropertyName = "IDKendaraan";
            this.colIDKendaraan.HeaderText = "IDKendaraan";
            this.colIDKendaraan.Name = "colIDKendaraan";
            this.colIDKendaraan.ReadOnly = true;
            this.colIDKendaraan.Visible = false;
            // 
            // btnKembali
            // 
            this.btnKembali.Location = new System.Drawing.Point(602, 230);
            this.btnKembali.Name = "btnKembali";
            this.btnKembali.Size = new System.Drawing.Size(77, 53);
            this.btnKembali.TabIndex = 12;
            this.btnKembali.Text = "Kembali";
            this.btnKembali.UseVisualStyleBackColor = true;
            this.btnKembali.Click += new System.EventHandler(this.btnKembali_Click);
            // 
            // Operasional
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(708, 547);
            this.Controls.Add(this.btnKembali);
            this.Controls.Add(this.dgvOperasional);
            this.Controls.Add(this.txtCari);
            this.Controls.Add(this.lblCari);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnUbah);
            this.Controls.Add(this.btnHapus);
            this.Controls.Add(this.btnTambah);
            this.Controls.Add(this.inputPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "Operasional";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manajemen Data Biaya Operasional";
            this.Load += new System.EventHandler(this.Operasional_Load);
            this.inputPanel.ResumeLayout(false);
            this.inputPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOperasional)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel inputPanel;
        private System.Windows.Forms.Label lblPaket;
        private System.Windows.Forms.ComboBox cmbPaket;
        private System.Windows.Forms.Label lblDriver;
        private System.Windows.Forms.ComboBox cmbDriver;
        private System.Windows.Forms.Label lblKendaraan;
        private System.Windows.Forms.ComboBox cmbKendaraan;
        private System.Windows.Forms.Label lblJenisPengeluaran;
        private System.Windows.Forms.ComboBox cmbJenisPengeluaran;
        private System.Windows.Forms.Label lblBiaya;
        private System.Windows.Forms.TextBox txtBiaya;
        private System.Windows.Forms.Button btnTambah;
        private System.Windows.Forms.Button btnHapus;
        private System.Windows.Forms.Button btnUbah;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblCari;
        private System.Windows.Forms.TextBox txtCari;
        private System.Windows.Forms.DataGridView dgvOperasional;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIDOperasional;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNamaPaket;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNamaDriver;
        private System.Windows.Forms.DataGridViewTextBoxColumn colKendaraanInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colJenisPengeluaran;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBiayaOperasional;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIDPaket;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIDDriver;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIDKendaraan;
        private System.Windows.Forms.Button btnKembali;
    }
}