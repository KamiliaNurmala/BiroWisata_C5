using System.Windows.Forms;
using System.Drawing;

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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.inputPanel = new System.Windows.Forms.Panel();
            this.lblNama = new System.Windows.Forms.Label();
            this.txtNama = new System.Windows.Forms.TextBox();
            this.lblAlamat = new System.Windows.Forms.Label();
            this.txtAlamat = new System.Windows.Forms.TextBox();
            this.lblNoTelp = new System.Windows.Forms.Label();
            this.txtNoTelp = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.dgvPelanggan = new System.Windows.Forms.DataGridView();
            this.colIDPelanggan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNamaPelanggan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAlamat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNoTelp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEmail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnTambah = new System.Windows.Forms.Button();
            this.btnHapus = new System.Windows.Forms.Button();
            this.btnUbah = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnKembali = new System.Windows.Forms.Button();
            this.inputPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPelanggan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // inputPanel
            // 
            this.inputPanel.BackColor = System.Drawing.Color.White;
            this.inputPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inputPanel.Controls.Add(this.lblNama);
            this.inputPanel.Controls.Add(this.txtNama);
            this.inputPanel.Controls.Add(this.lblAlamat);
            this.inputPanel.Controls.Add(this.txtAlamat);
            this.inputPanel.Controls.Add(this.lblNoTelp);
            this.inputPanel.Controls.Add(this.txtNoTelp);
            this.inputPanel.Controls.Add(this.lblEmail);
            this.inputPanel.Controls.Add(this.txtEmail);
            this.inputPanel.Location = new System.Drawing.Point(13, 13);
            this.inputPanel.Margin = new System.Windows.Forms.Padding(2);
            this.inputPanel.Name = "inputPanel";
            this.inputPanel.Size = new System.Drawing.Size(334, 124);
            this.inputPanel.TabIndex = 0;
            // 
            // lblNama
            // 
            this.lblNama.AutoSize = true;
            this.lblNama.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNama.Location = new System.Drawing.Point(10, 12);
            this.lblNama.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNama.Name = "lblNama";
            this.lblNama.Size = new System.Drawing.Size(101, 15);
            this.lblNama.TabIndex = 0;
            this.lblNama.Text = "Nama Pelanggan:";
            // 
            // txtNama
            // 
            this.txtNama.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtNama.Location = new System.Drawing.Point(113, 10);
            this.txtNama.Margin = new System.Windows.Forms.Padding(2);
            this.txtNama.Name = "txtNama";
            this.txtNama.Size = new System.Drawing.Size(208, 23);
            this.txtNama.TabIndex = 1;
            // 
            // lblAlamat
            // 
            this.lblAlamat.AutoSize = true;
            this.lblAlamat.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblAlamat.Location = new System.Drawing.Point(10, 38);
            this.lblAlamat.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAlamat.Name = "lblAlamat";
            this.lblAlamat.Size = new System.Drawing.Size(48, 15);
            this.lblAlamat.TabIndex = 2;
            this.lblAlamat.Text = "Alamat:";
            // 
            // txtAlamat
            // 
            this.txtAlamat.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtAlamat.Location = new System.Drawing.Point(113, 36);
            this.txtAlamat.Margin = new System.Windows.Forms.Padding(2);
            this.txtAlamat.Multiline = true;
            this.txtAlamat.Name = "txtAlamat";
            this.txtAlamat.Size = new System.Drawing.Size(208, 34);
            this.txtAlamat.TabIndex = 2;
            this.txtAlamat.TextChanged += new System.EventHandler(this.txtAlamat_TextChanged);
            // 
            // lblNoTelp
            // 
            this.lblNoTelp.AutoSize = true;
            this.lblNoTelp.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNoTelp.Location = new System.Drawing.Point(10, 77);
            this.lblNoTelp.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNoTelp.Name = "lblNoTelp";
            this.lblNoTelp.Size = new System.Drawing.Size(74, 15);
            this.lblNoTelp.TabIndex = 3;
            this.lblNoTelp.Text = "No. Telepon:";
            // 
            // txtNoTelp
            // 
            this.txtNoTelp.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtNoTelp.Location = new System.Drawing.Point(113, 75);
            this.txtNoTelp.Margin = new System.Windows.Forms.Padding(2);
            this.txtNoTelp.Name = "txtNoTelp";
            this.txtNoTelp.Size = new System.Drawing.Size(208, 23);
            this.txtNoTelp.TabIndex = 3;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblEmail.Location = new System.Drawing.Point(10, 103);
            this.lblEmail.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(39, 15);
            this.lblEmail.TabIndex = 5;
            this.lblEmail.Text = "Email:";
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtEmail.Location = new System.Drawing.Point(113, 101);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(2);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(208, 23);
            this.txtEmail.TabIndex = 4;
            // 
            // dgvPelanggan
            // 
            this.dgvPelanggan.AllowUserToAddRows = false;
            this.dgvPelanggan.AllowUserToDeleteRows = false;
            this.dgvPelanggan.BackgroundColor = System.Drawing.Color.White;
            this.dgvPelanggan.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPelanggan.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPelanggan.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPelanggan.ColumnHeadersHeight = 34;
            this.dgvPelanggan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvPelanggan.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colIDPelanggan,
            this.colNamaPelanggan,
            this.colAlamat,
            this.colNoTelp,
            this.colEmail});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPelanggan.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvPelanggan.EnableHeadersVisualStyles = false;
            this.dgvPelanggan.GridColor = System.Drawing.Color.Gainsboro;
            this.dgvPelanggan.Location = new System.Drawing.Point(17, 174);
            this.dgvPelanggan.Margin = new System.Windows.Forms.Padding(2);
            this.dgvPelanggan.MultiSelect = false;
            this.dgvPelanggan.Name = "dgvPelanggan";
            this.dgvPelanggan.ReadOnly = true;
            this.dgvPelanggan.RowHeadersVisible = false;
            this.dgvPelanggan.RowHeadersWidth = 62;
            this.dgvPelanggan.RowTemplate.Height = 28;
            this.dgvPelanggan.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPelanggan.Size = new System.Drawing.Size(560, 221);
            this.dgvPelanggan.TabIndex = 10;
            this.dgvPelanggan.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPelanggan_CellContentClick_1);
            this.dgvPelanggan.SelectionChanged += new System.EventHandler(this.dgvPelanggan_SelectionChanged);
            // 
            // colIDPelanggan
            // 
            this.colIDPelanggan.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.colIDPelanggan.DataPropertyName = "IDPelanggan";
            this.colIDPelanggan.HeaderText = "ID Pelanggan";
            this.colIDPelanggan.MinimumWidth = 8;
            this.colIDPelanggan.Name = "colIDPelanggan";
            this.colIDPelanggan.ReadOnly = true;
            this.colIDPelanggan.Visible = false;
            this.colIDPelanggan.Width = 97;
            // 
            // colNamaPelanggan
            // 
            this.colNamaPelanggan.DataPropertyName = "NamaPelanggan";
            this.colNamaPelanggan.HeaderText = "Nama Pelanggan";
            this.colNamaPelanggan.MinimumWidth = 8;
            this.colNamaPelanggan.Name = "colNamaPelanggan";
            this.colNamaPelanggan.ReadOnly = true;
            this.colNamaPelanggan.Width = 150;
            // 
            // colAlamat
            // 
            this.colAlamat.DataPropertyName = "Alamat";
            this.colAlamat.HeaderText = "Alamat";
            this.colAlamat.MinimumWidth = 8;
            this.colAlamat.Name = "colAlamat";
            this.colAlamat.ReadOnly = true;
            this.colAlamat.Width = 150;
            // 
            // colNoTelp
            // 
            this.colNoTelp.DataPropertyName = "NoTelp";
            this.colNoTelp.HeaderText = "No Telp";
            this.colNoTelp.MinimumWidth = 8;
            this.colNoTelp.Name = "colNoTelp";
            this.colNoTelp.ReadOnly = true;
            this.colNoTelp.Width = 150;
            // 
            // colEmail
            // 
            this.colEmail.DataPropertyName = "Email";
            this.colEmail.HeaderText = "Email";
            this.colEmail.MinimumWidth = 8;
            this.colEmail.Name = "colEmail";
            this.colEmail.ReadOnly = true;
            this.colEmail.Width = 150;
            // 
            // btnTambah
            // 
            this.btnTambah.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnTambah.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTambah.FlatAppearance.BorderSize = 0;
            this.btnTambah.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTambah.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnTambah.ForeColor = System.Drawing.Color.White;
            this.btnTambah.Location = new System.Drawing.Point(360, 13);
            this.btnTambah.Margin = new System.Windows.Forms.Padding(2);
            this.btnTambah.Name = "btnTambah";
            this.btnTambah.Size = new System.Drawing.Size(80, 26);
            this.btnTambah.TabIndex = 5;
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
            this.btnHapus.Location = new System.Drawing.Point(453, 13);
            this.btnHapus.Margin = new System.Windows.Forms.Padding(2);
            this.btnHapus.Name = "btnHapus";
            this.btnHapus.Size = new System.Drawing.Size(80, 26);
            this.btnHapus.TabIndex = 7;
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
            this.btnUbah.Location = new System.Drawing.Point(360, 46);
            this.btnUbah.Margin = new System.Windows.Forms.Padding(2);
            this.btnUbah.Name = "btnUbah";
            this.btnUbah.Size = new System.Drawing.Size(80, 26);
            this.btnUbah.TabIndex = 6;
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
            this.btnRefresh.Location = new System.Drawing.Point(453, 46);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(2);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(80, 26);
            this.btnRefresh.TabIndex = 8;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSearch.Location = new System.Drawing.Point(53, 144);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(2);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(295, 23);
            this.txtSearch.TabIndex = 9;
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSearch.Location = new System.Drawing.Point(13, 146);
            this.lblSearch.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(31, 15);
            this.lblSearch.TabIndex = 9;
            this.lblSearch.Text = "Cari:";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // btnKembali
            // 
            this.btnKembali.Location = new System.Drawing.Point(374, 89);
            this.btnKembali.Name = "btnKembali";
            this.btnKembali.Size = new System.Drawing.Size(173, 53);
            this.btnKembali.TabIndex = 13;
            this.btnKembali.Text = "Kembali";
            this.btnKembali.UseVisualStyleBackColor = true;
            this.btnKembali.Click += new System.EventHandler(this.btnKembali_Click);
            // 
            // Pelanggan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(587, 403);
            this.Controls.Add(this.btnKembali);
            this.Controls.Add(this.inputPanel);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnUbah);
            this.Controls.Add(this.btnHapus);
            this.Controls.Add(this.btnTambah);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.dgvPelanggan);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "Pelanggan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manajemen Data Pelanggan";
            this.Load += new System.EventHandler(this.Pelanggan_Load);
            this.inputPanel.ResumeLayout(false);
            this.inputPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPelanggan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel inputPanel;
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
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ErrorProvider errorProvider1;


        // Need to declare these if not already done (they should be for designer variables)
        //private System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1;
        //private System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2;
        private DataGridViewTextBoxColumn colIDPelanggan;
        private DataGridViewTextBoxColumn colNamaPelanggan;
        private DataGridViewTextBoxColumn colAlamat;
        private DataGridViewTextBoxColumn colNoTelp;
        private DataGridViewTextBoxColumn colEmail;
        private Button btnKembali;
    }
}