using System.Windows.Forms;
using System.Drawing;

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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.inputPanel = new System.Windows.Forms.Panel();
            this.lblJenis = new System.Windows.Forms.Label();
            this.txtJenis = new System.Windows.Forms.TextBox();
            this.lblPlatNomor = new System.Windows.Forms.Label();
            this.txtPlatNomor = new System.Windows.Forms.TextBox();
            this.lblKapasitas = new System.Windows.Forms.Label();
            this.txtKapasitas = new System.Windows.Forms.TextBox();
            this.btnTambah = new System.Windows.Forms.Button();
            this.btnUbah = new System.Windows.Forms.Button();
            this.btnHapus = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.dgvKendaraan = new System.Windows.Forms.DataGridView();
            this.colIDKendaraan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colJenis = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPlatNomor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colKapasitas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnKembali = new System.Windows.Forms.Button();
            this.inputPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKendaraan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // inputPanel
            // 
            this.inputPanel.BackColor = System.Drawing.Color.White;
            this.inputPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inputPanel.Controls.Add(this.lblJenis);
            this.inputPanel.Controls.Add(this.txtJenis);
            this.inputPanel.Controls.Add(this.lblPlatNomor);
            this.inputPanel.Controls.Add(this.txtPlatNomor);
            this.inputPanel.Controls.Add(this.lblKapasitas);
            this.inputPanel.Controls.Add(this.txtKapasitas);
            this.inputPanel.Location = new System.Drawing.Point(13, 13);
            this.inputPanel.Margin = new System.Windows.Forms.Padding(2);
            this.inputPanel.Name = "inputPanel";
            this.inputPanel.Size = new System.Drawing.Size(321, 98);
            this.inputPanel.TabIndex = 0;
            // 
            // lblJenis
            // 
            this.lblJenis.AutoSize = true;
            this.lblJenis.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblJenis.Location = new System.Drawing.Point(10, 12);
            this.lblJenis.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblJenis.Name = "lblJenis";
            this.lblJenis.Size = new System.Drawing.Size(35, 15);
            this.lblJenis.TabIndex = 0;
            this.lblJenis.Text = "Jenis:";
            // 
            // txtJenis
            // 
            this.txtJenis.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtJenis.Location = new System.Drawing.Point(100, 10);
            this.txtJenis.Margin = new System.Windows.Forms.Padding(2);
            this.txtJenis.Name = "txtJenis";
            this.txtJenis.Size = new System.Drawing.Size(208, 23);
            this.txtJenis.TabIndex = 1;
            // 
            // lblPlatNomor
            // 
            this.lblPlatNomor.AutoSize = true;
            this.lblPlatNomor.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPlatNomor.Location = new System.Drawing.Point(10, 38);
            this.lblPlatNomor.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPlatNomor.Name = "lblPlatNomor";
            this.lblPlatNomor.Size = new System.Drawing.Size(71, 15);
            this.lblPlatNomor.TabIndex = 2;
            this.lblPlatNomor.Text = "Plat Nomor:";
            // 
            // txtPlatNomor
            // 
            this.txtPlatNomor.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtPlatNomor.Location = new System.Drawing.Point(100, 36);
            this.txtPlatNomor.Margin = new System.Windows.Forms.Padding(2);
            this.txtPlatNomor.Name = "txtPlatNomor";
            this.txtPlatNomor.Size = new System.Drawing.Size(208, 23);
            this.txtPlatNomor.TabIndex = 2;
            // 
            // lblKapasitas
            // 
            this.lblKapasitas.AutoSize = true;
            this.lblKapasitas.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblKapasitas.Location = new System.Drawing.Point(10, 64);
            this.lblKapasitas.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblKapasitas.Name = "lblKapasitas";
            this.lblKapasitas.Size = new System.Drawing.Size(59, 15);
            this.lblKapasitas.TabIndex = 4;
            this.lblKapasitas.Text = "Kapasitas:";
            // 
            // txtKapasitas
            // 
            this.txtKapasitas.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtKapasitas.Location = new System.Drawing.Point(100, 62);
            this.txtKapasitas.Margin = new System.Windows.Forms.Padding(2);
            this.txtKapasitas.Name = "txtKapasitas";
            this.txtKapasitas.Size = new System.Drawing.Size(208, 23);
            this.txtKapasitas.TabIndex = 3;
            // 
            // btnTambah
            // 
            this.btnTambah.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnTambah.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTambah.FlatAppearance.BorderSize = 0;
            this.btnTambah.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTambah.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnTambah.ForeColor = System.Drawing.Color.White;
            this.btnTambah.Location = new System.Drawing.Point(347, 13);
            this.btnTambah.Margin = new System.Windows.Forms.Padding(2);
            this.btnTambah.Name = "btnTambah";
            this.btnTambah.Size = new System.Drawing.Size(80, 26);
            this.btnTambah.TabIndex = 4;
            this.btnTambah.Text = "Tambah";
            this.btnTambah.UseVisualStyleBackColor = false;
            this.btnTambah.Click += new System.EventHandler(this.btnTambah_Click);
            // 
            // btnUbah
            // 
            this.btnUbah.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnUbah.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUbah.FlatAppearance.BorderSize = 0;
            this.btnUbah.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUbah.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnUbah.ForeColor = System.Drawing.Color.White;
            this.btnUbah.Location = new System.Drawing.Point(347, 46);
            this.btnUbah.Margin = new System.Windows.Forms.Padding(2);
            this.btnUbah.Name = "btnUbah";
            this.btnUbah.Size = new System.Drawing.Size(80, 26);
            this.btnUbah.TabIndex = 5;
            this.btnUbah.Text = "Ubah";
            this.btnUbah.UseVisualStyleBackColor = false;
            this.btnUbah.Click += new System.EventHandler(this.btnUbah_Click);
            // 
            // btnHapus
            // 
            this.btnHapus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnHapus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHapus.FlatAppearance.BorderSize = 0;
            this.btnHapus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHapus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnHapus.ForeColor = System.Drawing.Color.White;
            this.btnHapus.Location = new System.Drawing.Point(440, 13);
            this.btnHapus.Margin = new System.Windows.Forms.Padding(2);
            this.btnHapus.Name = "btnHapus";
            this.btnHapus.Size = new System.Drawing.Size(80, 26);
            this.btnHapus.TabIndex = 6;
            this.btnHapus.Text = "Hapus";
            this.btnHapus.UseVisualStyleBackColor = false;
            this.btnHapus.Click += new System.EventHandler(this.btnHapus_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(440, 46);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(2);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(80, 26);
            this.btnRefresh.TabIndex = 7;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // dgvKendaraan
            // 
            this.dgvKendaraan.AllowUserToAddRows = false;
            this.dgvKendaraan.AllowUserToDeleteRows = false;
            this.dgvKendaraan.BackgroundColor = System.Drawing.Color.White;
            this.dgvKendaraan.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvKendaraan.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvKendaraan.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvKendaraan.ColumnHeadersHeight = 34;
            this.dgvKendaraan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvKendaraan.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colIDKendaraan,
            this.colJenis,
            this.colPlatNomor,
            this.colKapasitas});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvKendaraan.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvKendaraan.EnableHeadersVisualStyles = false;
            this.dgvKendaraan.GridColor = System.Drawing.Color.Gainsboro;
            this.dgvKendaraan.Location = new System.Drawing.Point(13, 143);
            this.dgvKendaraan.Margin = new System.Windows.Forms.Padding(2);
            this.dgvKendaraan.MultiSelect = false;
            this.dgvKendaraan.Name = "dgvKendaraan";
            this.dgvKendaraan.ReadOnly = true;
            this.dgvKendaraan.RowHeadersVisible = false;
            this.dgvKendaraan.RowHeadersWidth = 62;
            this.dgvKendaraan.RowTemplate.Height = 28;
            this.dgvKendaraan.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvKendaraan.Size = new System.Drawing.Size(560, 221);
            this.dgvKendaraan.TabIndex = 9;
            this.dgvKendaraan.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvKendaraan_CellClick);
            this.dgvKendaraan.SelectionChanged += new System.EventHandler(this.dgvKendaraan_SelectionChanged);
            // 
            // colIDKendaraan
            // 
            this.colIDKendaraan.DataPropertyName = "IDKendaraan";
            this.colIDKendaraan.HeaderText = "ID";
            this.colIDKendaraan.MinimumWidth = 8;
            this.colIDKendaraan.Name = "colIDKendaraan";
            this.colIDKendaraan.ReadOnly = true;
            this.colIDKendaraan.Visible = false;
            this.colIDKendaraan.Width = 50;
            // 
            // colJenis
            // 
            this.colJenis.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colJenis.DataPropertyName = "Jenis";
            this.colJenis.HeaderText = "Jenis Kendaraan";
            this.colJenis.MinimumWidth = 8;
            this.colJenis.Name = "colJenis";
            this.colJenis.ReadOnly = true;
            // 
            // colPlatNomor
            // 
            this.colPlatNomor.DataPropertyName = "PlatNomor";
            this.colPlatNomor.HeaderText = "Plat Nomor";
            this.colPlatNomor.MinimumWidth = 8;
            this.colPlatNomor.Name = "colPlatNomor";
            this.colPlatNomor.ReadOnly = true;
            this.colPlatNomor.Width = 200;
            // 
            // colKapasitas
            // 
            this.colKapasitas.DataPropertyName = "Kapasitas";
            this.colKapasitas.HeaderText = "Kapasitas";
            this.colKapasitas.MinimumWidth = 8;
            this.colKapasitas.Name = "colKapasitas";
            this.colKapasitas.ReadOnly = true;
            this.colKapasitas.Width = 120;
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSearch.Location = new System.Drawing.Point(13, 120);
            this.lblSearch.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(31, 15);
            this.lblSearch.TabIndex = 8;
            this.lblSearch.Text = "Cari:";
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSearch.Location = new System.Drawing.Point(53, 118);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(2);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(281, 23);
            this.txtSearch.TabIndex = 8;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // btnKembali
            // 
            this.btnKembali.Location = new System.Drawing.Point(347, 78);
            this.btnKembali.Name = "btnKembali";
            this.btnKembali.Size = new System.Drawing.Size(173, 53);
            this.btnKembali.TabIndex = 10;
            this.btnKembali.Text = "Kembali";
            this.btnKembali.UseVisualStyleBackColor = true;
            this.btnKembali.Click += new System.EventHandler(this.btnKembali_Click);
            // 
            // Kendaraan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(587, 377);
            this.Controls.Add(this.btnKembali);
            this.Controls.Add(this.inputPanel);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnUbah);
            this.Controls.Add(this.btnHapus);
            this.Controls.Add(this.btnTambah);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.dgvKendaraan);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "Kendaraan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manajemen Data Kendaraan";
            this.Load += new System.EventHandler(this.Kendaraan_Load);
            this.inputPanel.ResumeLayout(false);
            this.inputPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKendaraan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel inputPanel;
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
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        // Explicitly declared DataGridView columns
        private System.Windows.Forms.DataGridViewTextBoxColumn colIDKendaraan;
        private System.Windows.Forms.DataGridViewTextBoxColumn colJenis;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPlatNomor;
        private System.Windows.Forms.DataGridViewTextBoxColumn colKapasitas;
        private Button btnKembali;
    }
}