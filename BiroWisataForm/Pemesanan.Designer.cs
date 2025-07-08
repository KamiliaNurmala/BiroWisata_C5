using System.Windows.Forms;
using System.Drawing;

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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.inputPanel = new System.Windows.Forms.Panel();
            this.lblPelanggan = new System.Windows.Forms.Label();
            this.cmbPelanggan = new System.Windows.Forms.ComboBox();
            this.lblPaketWisata = new System.Windows.Forms.Label();
            this.cmbPaketWisata = new System.Windows.Forms.ComboBox();
            this.lblTanggal = new System.Windows.Forms.Label();
            this.dtpTanggalPesan = new System.Windows.Forms.DateTimePicker();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.lblUbahStatusPemesanan = new System.Windows.Forms.Label();
            this.cmbUbahStatusPemesanan = new System.Windows.Forms.ComboBox();
            this.dgvPemesanan = new System.Windows.Forms.DataGridView();
            this.colIDPemesanan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNamaPelanggan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNamaPaket = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTanggalPemesanan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatusPembayaran = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatusPemesanan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTotalPembayaran = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIDPelanggan_hidden = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIDPaket_hidden = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnTambah = new System.Windows.Forms.Button();
            this.btnUbah = new System.Windows.Forms.Button();
            this.btnHapus = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnProsesStatusPemesanan = new System.Windows.Forms.Button();
            this.btnKembali = new System.Windows.Forms.Button();
            this.inputPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPemesanan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // inputPanel
            // 
            this.inputPanel.BackColor = System.Drawing.Color.White;
            this.inputPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inputPanel.Controls.Add(this.lblPelanggan);
            this.inputPanel.Controls.Add(this.cmbPelanggan);
            this.inputPanel.Controls.Add(this.lblPaketWisata);
            this.inputPanel.Controls.Add(this.cmbPaketWisata);
            this.inputPanel.Controls.Add(this.lblTanggal);
            this.inputPanel.Controls.Add(this.dtpTanggalPesan);
            this.inputPanel.Controls.Add(this.lblStatus);
            this.inputPanel.Controls.Add(this.cmbStatus);
            this.inputPanel.Controls.Add(this.lblTotal);
            this.inputPanel.Controls.Add(this.txtTotal);
            this.inputPanel.Controls.Add(this.lblUbahStatusPemesanan);
            this.inputPanel.Controls.Add(this.cmbUbahStatusPemesanan);
            this.inputPanel.Location = new System.Drawing.Point(13, 13);
            this.inputPanel.Margin = new System.Windows.Forms.Padding(2);
            this.inputPanel.Name = "inputPanel";
            this.inputPanel.Size = new System.Drawing.Size(361, 170);
            this.inputPanel.TabIndex = 0;
            // 
            // lblPelanggan
            // 
            this.lblPelanggan.AutoSize = true;
            this.lblPelanggan.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPelanggan.Location = new System.Drawing.Point(10, 12);
            this.lblPelanggan.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPelanggan.Name = "lblPelanggan";
            this.lblPelanggan.Size = new System.Drawing.Size(66, 15);
            this.lblPelanggan.TabIndex = 0;
            this.lblPelanggan.Text = "Pelanggan:";
            // 
            // cmbPelanggan
            // 
            this.cmbPelanggan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPelanggan.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbPelanggan.FormattingEnabled = true;
            this.cmbPelanggan.Location = new System.Drawing.Point(127, 10);
            this.cmbPelanggan.Margin = new System.Windows.Forms.Padding(2);
            this.cmbPelanggan.Name = "cmbPelanggan";
            this.cmbPelanggan.Size = new System.Drawing.Size(221, 23);
            this.cmbPelanggan.TabIndex = 1;
            // 
            // lblPaketWisata
            // 
            this.lblPaketWisata.AutoSize = true;
            this.lblPaketWisata.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPaketWisata.Location = new System.Drawing.Point(10, 38);
            this.lblPaketWisata.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPaketWisata.Name = "lblPaketWisata";
            this.lblPaketWisata.Size = new System.Drawing.Size(77, 15);
            this.lblPaketWisata.TabIndex = 2;
            this.lblPaketWisata.Text = "Paket Wisata:";
            // 
            // cmbPaketWisata
            // 
            this.cmbPaketWisata.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPaketWisata.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbPaketWisata.FormattingEnabled = true;
            this.cmbPaketWisata.Location = new System.Drawing.Point(127, 36);
            this.cmbPaketWisata.Margin = new System.Windows.Forms.Padding(2);
            this.cmbPaketWisata.Name = "cmbPaketWisata";
            this.cmbPaketWisata.Size = new System.Drawing.Size(221, 23);
            this.cmbPaketWisata.TabIndex = 3;
            this.cmbPaketWisata.SelectedIndexChanged += new System.EventHandler(this.cmbPaketWisata_SelectedIndexChanged);
            // 
            // lblTanggal
            // 
            this.lblTanggal.AutoSize = true;
            this.lblTanggal.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTanggal.Location = new System.Drawing.Point(10, 66);
            this.lblTanggal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTanggal.Name = "lblTanggal";
            this.lblTanggal.Size = new System.Drawing.Size(116, 15);
            this.lblTanggal.TabIndex = 4;
            this.lblTanggal.Text = "Tanggal Pemesanan:";
            // 
            // dtpTanggalPesan
            // 
            this.dtpTanggalPesan.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpTanggalPesan.Location = new System.Drawing.Point(127, 64);
            this.dtpTanggalPesan.Margin = new System.Windows.Forms.Padding(2);
            this.dtpTanggalPesan.Name = "dtpTanggalPesan";
            this.dtpTanggalPesan.Size = new System.Drawing.Size(221, 23);
            this.dtpTanggalPesan.TabIndex = 5;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatus.Location = new System.Drawing.Point(10, 92);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(111, 15);
            this.lblStatus.TabIndex = 6;
            this.lblStatus.Text = "Status Pembayaran:";
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(127, 90);
            this.cmbStatus.Margin = new System.Windows.Forms.Padding(2);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(221, 23);
            this.cmbStatus.TabIndex = 7;
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTotal.Location = new System.Drawing.Point(10, 118);
            this.lblTotal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(105, 15);
            this.lblTotal.TabIndex = 8;
            this.lblTotal.Text = "Total Pembayaran:";
            // 
            // txtTotal
            // 
            this.txtTotal.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTotal.Location = new System.Drawing.Point(127, 116);
            this.txtTotal.Margin = new System.Windows.Forms.Padding(2);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(221, 23);
            this.txtTotal.TabIndex = 9;
            // 
            // lblUbahStatusPemesanan
            // 
            this.lblUbahStatusPemesanan.AutoSize = true;
            this.lblUbahStatusPemesanan.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblUbahStatusPemesanan.Location = new System.Drawing.Point(10, 142);
            this.lblUbahStatusPemesanan.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblUbahStatusPemesanan.Name = "lblUbahStatusPemesanan";
            this.lblUbahStatusPemesanan.Size = new System.Drawing.Size(89, 15);
            this.lblUbahStatusPemesanan.TabIndex = 19;
            this.lblUbahStatusPemesanan.Text = "Ubah Status Ke:";
            // 
            // cmbUbahStatusPemesanan
            // 
            this.cmbUbahStatusPemesanan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUbahStatusPemesanan.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbUbahStatusPemesanan.FormattingEnabled = true;
            this.cmbUbahStatusPemesanan.Location = new System.Drawing.Point(127, 140);
            this.cmbUbahStatusPemesanan.Margin = new System.Windows.Forms.Padding(2);
            this.cmbUbahStatusPemesanan.Name = "cmbUbahStatusPemesanan";
            this.cmbUbahStatusPemesanan.Size = new System.Drawing.Size(221, 23);
            this.cmbUbahStatusPemesanan.TabIndex = 20;
            // 
            // dgvPemesanan
            // 
            this.dgvPemesanan.AllowUserToAddRows = false;
            this.dgvPemesanan.AllowUserToDeleteRows = false;
            this.dgvPemesanan.BackgroundColor = System.Drawing.Color.White;
            this.dgvPemesanan.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPemesanan.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPemesanan.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPemesanan.ColumnHeadersHeight = 34;
            this.dgvPemesanan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvPemesanan.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colIDPemesanan,
            this.colNamaPelanggan,
            this.colNamaPaket,
            this.colTanggalPemesanan,
            this.colStatusPembayaran,
            this.colStatusPemesanan,
            this.colTotalPembayaran,
            this.colIDPelanggan_hidden,
            this.colIDPaket_hidden});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPemesanan.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvPemesanan.EnableHeadersVisualStyles = false;
            this.dgvPemesanan.GridColor = System.Drawing.Color.Gainsboro;
            this.dgvPemesanan.Location = new System.Drawing.Point(13, 223);
            this.dgvPemesanan.Margin = new System.Windows.Forms.Padding(2);
            this.dgvPemesanan.MultiSelect = false;
            this.dgvPemesanan.Name = "dgvPemesanan";
            this.dgvPemesanan.ReadOnly = true;
            this.dgvPemesanan.RowHeadersVisible = false;
            this.dgvPemesanan.RowHeadersWidth = 62;
            this.dgvPemesanan.RowTemplate.Height = 28;
            this.dgvPemesanan.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPemesanan.Size = new System.Drawing.Size(627, 208);
            this.dgvPemesanan.TabIndex = 14;
            this.dgvPemesanan.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvPemesanan_CellClick);
            this.dgvPemesanan.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPemesanan_CellContentClick);
            this.dgvPemesanan.SelectionChanged += new System.EventHandler(this.DgvPemesanan_SelectionChanged);
            // 
            // colIDPemesanan
            // 
            this.colIDPemesanan.DataPropertyName = "IDPemesanan";
            this.colIDPemesanan.HeaderText = "ID Pemesanan";
            this.colIDPemesanan.MinimumWidth = 8;
            this.colIDPemesanan.Name = "colIDPemesanan";
            this.colIDPemesanan.ReadOnly = true;
            this.colIDPemesanan.Visible = false;
            this.colIDPemesanan.Width = 150;
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
            // colNamaPaket
            // 
            this.colNamaPaket.DataPropertyName = "NamaPaket";
            this.colNamaPaket.HeaderText = "Nama Paket";
            this.colNamaPaket.MinimumWidth = 8;
            this.colNamaPaket.Name = "colNamaPaket";
            this.colNamaPaket.ReadOnly = true;
            this.colNamaPaket.Width = 150;
            // 
            // colTanggalPemesanan
            // 
            this.colTanggalPemesanan.DataPropertyName = "TanggalPemesanan";
            this.colTanggalPemesanan.HeaderText = "Tanggal Pemesanan";
            this.colTanggalPemesanan.MinimumWidth = 8;
            this.colTanggalPemesanan.Name = "colTanggalPemesanan";
            this.colTanggalPemesanan.ReadOnly = true;
            this.colTanggalPemesanan.Width = 150;
            // 
            // colStatusPembayaran
            // 
            this.colStatusPembayaran.DataPropertyName = "StatusPembayaran";
            this.colStatusPembayaran.HeaderText = "Status Pembayaran";
            this.colStatusPembayaran.MinimumWidth = 8;
            this.colStatusPembayaran.Name = "colStatusPembayaran";
            this.colStatusPembayaran.ReadOnly = true;
            this.colStatusPembayaran.Width = 150;
            // 
            // colStatusPemesanan
            // 
            this.colStatusPemesanan.DataPropertyName = "StatusPemesanan";
            this.colStatusPemesanan.HeaderText = "Status Pesanan";
            this.colStatusPemesanan.MinimumWidth = 8;
            this.colStatusPemesanan.Name = "colStatusPemesanan";
            this.colStatusPemesanan.ReadOnly = true;
            this.colStatusPemesanan.Width = 150;
            // 
            // colTotalPembayaran
            // 
            this.colTotalPembayaran.DataPropertyName = "TotalPembayaran";
            this.colTotalPembayaran.HeaderText = "Total Pembayaran";
            this.colTotalPembayaran.MinimumWidth = 8;
            this.colTotalPembayaran.Name = "colTotalPembayaran";
            this.colTotalPembayaran.ReadOnly = true;
            this.colTotalPembayaran.Width = 150;
            // 
            // colIDPelanggan_hidden
            // 
            this.colIDPelanggan_hidden.DataPropertyName = "IDPelanggan";
            this.colIDPelanggan_hidden.HeaderText = "IDPelanggan_H";
            this.colIDPelanggan_hidden.MinimumWidth = 8;
            this.colIDPelanggan_hidden.Name = "colIDPelanggan_hidden";
            this.colIDPelanggan_hidden.ReadOnly = true;
            this.colIDPelanggan_hidden.Visible = false;
            this.colIDPelanggan_hidden.Width = 150;
            // 
            // colIDPaket_hidden
            // 
            this.colIDPaket_hidden.DataPropertyName = "IDPaket";
            this.colIDPaket_hidden.HeaderText = "IDPaket_H";
            this.colIDPaket_hidden.MinimumWidth = 8;
            this.colIDPaket_hidden.Name = "colIDPaket_hidden";
            this.colIDPaket_hidden.ReadOnly = true;
            this.colIDPaket_hidden.Visible = false;
            this.colIDPaket_hidden.Width = 150;
            // 
            // btnTambah
            // 
            this.btnTambah.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnTambah.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTambah.FlatAppearance.BorderSize = 0;
            this.btnTambah.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTambah.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnTambah.ForeColor = System.Drawing.Color.White;
            this.btnTambah.Location = new System.Drawing.Point(387, 13);
            this.btnTambah.Margin = new System.Windows.Forms.Padding(2);
            this.btnTambah.Name = "btnTambah";
            this.btnTambah.Size = new System.Drawing.Size(80, 26);
            this.btnTambah.TabIndex = 10;
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
            this.btnUbah.Location = new System.Drawing.Point(387, 46);
            this.btnUbah.Margin = new System.Windows.Forms.Padding(2);
            this.btnUbah.Name = "btnUbah";
            this.btnUbah.Size = new System.Drawing.Size(80, 26);
            this.btnUbah.TabIndex = 11;
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
            this.btnHapus.Location = new System.Drawing.Point(480, 13);
            this.btnHapus.Margin = new System.Windows.Forms.Padding(2);
            this.btnHapus.Name = "btnHapus";
            this.btnHapus.Size = new System.Drawing.Size(80, 26);
            this.btnHapus.TabIndex = 12;
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
            this.btnRefresh.Location = new System.Drawing.Point(480, 46);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(2);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(80, 26);
            this.btnRefresh.TabIndex = 13;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSearch.Location = new System.Drawing.Point(11, 196);
            this.lblSearch.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(31, 15);
            this.lblSearch.TabIndex = 17;
            this.lblSearch.Text = "Cari:";
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSearch.Location = new System.Drawing.Point(53, 194);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(2);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(321, 23);
            this.txtSearch.TabIndex = 18;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // btnProsesStatusPemesanan
            // 
            this.btnProsesStatusPemesanan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(156)))), ((int)(((byte)(18)))));
            this.btnProsesStatusPemesanan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnProsesStatusPemesanan.FlatAppearance.BorderSize = 0;
            this.btnProsesStatusPemesanan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProsesStatusPemesanan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnProsesStatusPemesanan.ForeColor = System.Drawing.Color.White;
            this.btnProsesStatusPemesanan.Location = new System.Drawing.Point(387, 80);
            this.btnProsesStatusPemesanan.Margin = new System.Windows.Forms.Padding(2);
            this.btnProsesStatusPemesanan.Name = "btnProsesStatusPemesanan";
            this.btnProsesStatusPemesanan.Size = new System.Drawing.Size(173, 25);
            this.btnProsesStatusPemesanan.TabIndex = 21;
            this.btnProsesStatusPemesanan.Text = "Proses Status Pemesanan";
            this.btnProsesStatusPemesanan.UseVisualStyleBackColor = true;
            this.btnProsesStatusPemesanan.Click += new System.EventHandler(this.btnProsesStatusPemesanan_Click);
            // 
            // btnKembali
            // 
            this.btnKembali.Location = new System.Drawing.Point(387, 118);
            this.btnKembali.Name = "btnKembali";
            this.btnKembali.Size = new System.Drawing.Size(173, 53);
            this.btnKembali.TabIndex = 22;
            this.btnKembali.Text = "Kembali";
            this.btnKembali.UseVisualStyleBackColor = true;
            this.btnKembali.Click += new System.EventHandler(this.btnKembali_Click);
            // 
            // Pemesanan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(653, 439);
            this.Controls.Add(this.btnKembali);
            this.Controls.Add(this.inputPanel);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnUbah);
            this.Controls.Add(this.btnHapus);
            this.Controls.Add(this.btnTambah);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.dgvPemesanan);
            this.Controls.Add(this.btnProsesStatusPemesanan);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "Pemesanan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manajemen Data Pemesanan";
            this.Load += new System.EventHandler(this.Pemesanan_Load);
            this.inputPanel.ResumeLayout(false);
            this.inputPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPemesanan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        // --- Corrected Field Declarations ---
        private System.Windows.Forms.Panel inputPanel;
        private System.Windows.Forms.Label lblPelanggan;
        private System.Windows.Forms.ComboBox cmbPelanggan;
        private System.Windows.Forms.Label lblPaketWisata;
        private System.Windows.Forms.ComboBox cmbPaketWisata;
        private System.Windows.Forms.Label lblTanggal;
        private System.Windows.Forms.DateTimePicker dtpTanggalPesan; // Renamed
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cmbStatus; // Renamed
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.DataGridView dgvPemesanan;
        private System.Windows.Forms.Button btnTambah;
        private System.Windows.Forms.Button btnUbah;
        private System.Windows.Forms.Button btnHapus;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private DataGridViewTextBoxColumn colIDPemesanan;
        private DataGridViewTextBoxColumn colNamaPelanggan;
        private DataGridViewTextBoxColumn colNamaPaket;
        private DataGridViewTextBoxColumn colTanggalPemesanan;
        private DataGridViewTextBoxColumn colStatusPembayaran;
        private DataGridViewTextBoxColumn colStatusPemesanan;
        private DataGridViewTextBoxColumn colTotalPembayaran;
        private DataGridViewTextBoxColumn colIDPelanggan_hidden;
        private DataGridViewTextBoxColumn colIDPaket_hidden;
        private Label lblUbahStatusPemesanan;
        private ComboBox cmbUbahStatusPemesanan;
        private Button btnProsesStatusPemesanan;
        private Button btnKembali;
    }
}