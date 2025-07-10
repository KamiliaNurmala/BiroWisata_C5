// Pembayaran.Designer.cs
namespace BiroWisataForm
{
    partial class Pembayaran
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

        // --- CONTROL DECLARATIONS ---
        private System.Windows.Forms.Button btnTambah;
        private System.Windows.Forms.Button btnHapus;
        private System.Windows.Forms.Button btnUbah;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblPemesanan; // Added Label for cmbPemesanan
        private System.Windows.Forms.Label lblJumlahPembayaran;
        private System.Windows.Forms.Label lblTanggalPembayaran;
        private System.Windows.Forms.Label lblMetodePembayaran;
        private System.Windows.Forms.TextBox txtJumlah;
        private System.Windows.Forms.DataGridView dgvPembayaran;
        private System.Windows.Forms.ComboBox cmbPemesanan;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.ComboBox comboBoxMetode;
        private System.Windows.Forms.DateTimePicker dtpPemesananFilterStart;
        private System.Windows.Forms.Label lblPemesananFilterStart;
        private System.Windows.Forms.DateTimePicker dtpPemesananFilterEnd;
        private System.Windows.Forms.Label lblPemesananFilterEnd;
        private System.Windows.Forms.DateTimePicker dtpPembayaranFilterStart;
        private System.Windows.Forms.Label lblPembayaranFilterStart;
        private System.Windows.Forms.DateTimePicker dtpPembayaranFilterEnd;
        private System.Windows.Forms.Label lblPembayaranFilterEnd;
        private System.Windows.Forms.Button btnFilter;


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnTambah = new System.Windows.Forms.Button();
            this.btnHapus = new System.Windows.Forms.Button();
            this.btnUbah = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblJumlahPembayaran = new System.Windows.Forms.Label();
            this.lblTanggalPembayaran = new System.Windows.Forms.Label();
            this.lblMetodePembayaran = new System.Windows.Forms.Label();
            this.txtJumlah = new System.Windows.Forms.TextBox();
            this.dgvPembayaran = new System.Windows.Forms.DataGridView();
            this.colIDPembayaran = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIDPemesanan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNamaPelanggan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNamaPaket = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colJumlahPembayaran = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTangggalPembayaran = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMetodePembayaran = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTanggalPemesanan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmbPemesanan = new System.Windows.Forms.ComboBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dtpPemesananFilterStart = new System.Windows.Forms.DateTimePicker();
            this.lblPemesananFilterStart = new System.Windows.Forms.Label();
            this.dtpPemesananFilterEnd = new System.Windows.Forms.DateTimePicker();
            this.lblPemesananFilterEnd = new System.Windows.Forms.Label();
            this.dtpPembayaranFilterStart = new System.Windows.Forms.DateTimePicker();
            this.lblPembayaranFilterStart = new System.Windows.Forms.Label();
            this.dtpPembayaranFilterEnd = new System.Windows.Forms.DateTimePicker();
            this.lblPembayaranFilterEnd = new System.Windows.Forms.Label();
            this.btnFilter = new System.Windows.Forms.Button();
            this.comboBoxMetode = new System.Windows.Forms.ComboBox();
            this.lblPemesanan = new System.Windows.Forms.Label();
            this.btnKembali = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPembayaran)).BeginInit();
            this.SuspendLayout();
            // 
            // btnTambah
            // 
            this.btnTambah.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnTambah.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTambah.FlatAppearance.BorderSize = 0;
            this.btnTambah.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTambah.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnTambah.ForeColor = System.Drawing.Color.White;
            this.btnTambah.Location = new System.Drawing.Point(700, 90);
            this.btnTambah.Name = "btnTambah";
            this.btnTambah.Size = new System.Drawing.Size(120, 40);
            this.btnTambah.TabIndex = 10;
            this.btnTambah.Text = "Tambah";
            this.btnTambah.UseVisualStyleBackColor = false;
            // 
            // btnHapus
            // 
            this.btnHapus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnHapus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHapus.FlatAppearance.BorderSize = 0;
            this.btnHapus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHapus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnHapus.ForeColor = System.Drawing.Color.White;
            this.btnHapus.Location = new System.Drawing.Point(841, 90);
            this.btnHapus.Name = "btnHapus";
            this.btnHapus.Size = new System.Drawing.Size(120, 40);
            this.btnHapus.TabIndex = 11;
            this.btnHapus.Text = "Hapus";
            this.btnHapus.UseVisualStyleBackColor = false;
            // 
            // btnUbah
            // 
            this.btnUbah.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnUbah.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUbah.FlatAppearance.BorderSize = 0;
            this.btnUbah.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUbah.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnUbah.ForeColor = System.Drawing.Color.White;
            this.btnUbah.Location = new System.Drawing.Point(700, 140);
            this.btnUbah.Name = "btnUbah";
            this.btnUbah.Size = new System.Drawing.Size(120, 40);
            this.btnUbah.TabIndex = 12;
            this.btnUbah.Text = "Ubah";
            this.btnUbah.UseVisualStyleBackColor = false;
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(841, 140);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(120, 40);
            this.btnRefresh.TabIndex = 13;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            // 
            // lblJumlahPembayaran
            // 
            this.lblJumlahPembayaran.AutoSize = true;
            this.lblJumlahPembayaran.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblJumlahPembayaran.Location = new System.Drawing.Point(20, 140);
            this.lblJumlahPembayaran.Name = "lblJumlahPembayaran";
            this.lblJumlahPembayaran.Size = new System.Drawing.Size(183, 25);
            this.lblJumlahPembayaran.TabIndex = 5;
            this.lblJumlahPembayaran.Text = "Jumlah Pembayaran : ";
            // 
            // lblTanggalPembayaran
            // 
            this.lblTanggalPembayaran.AutoSize = true;
            this.lblTanggalPembayaran.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTanggalPembayaran.Location = new System.Drawing.Point(20, 177);
            this.lblTanggalPembayaran.Name = "lblTanggalPembayaran";
            this.lblTanggalPembayaran.Size = new System.Drawing.Size(189, 25);
            this.lblTanggalPembayaran.TabIndex = 7;
            this.lblTanggalPembayaran.Text = "Tanggal Pembayaran : ";
            // 
            // lblMetodePembayaran
            // 
            this.lblMetodePembayaran.AutoSize = true;
            this.lblMetodePembayaran.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblMetodePembayaran.Location = new System.Drawing.Point(20, 214);
            this.lblMetodePembayaran.Name = "lblMetodePembayaran";
            this.lblMetodePembayaran.Size = new System.Drawing.Size(190, 25);
            this.lblMetodePembayaran.TabIndex = 9;
            this.lblMetodePembayaran.Text = "Metode Pembayaran : ";
            // 
            // txtJumlah
            // 
            this.txtJumlah.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtJumlah.Location = new System.Drawing.Point(190, 137);
            this.txtJumlah.Name = "txtJumlah";
            this.txtJumlah.ReadOnly = true;
            this.txtJumlah.Size = new System.Drawing.Size(480, 31);
            this.txtJumlah.TabIndex = 6;
            this.txtJumlah.Click += new System.EventHandler(this.CmbPemesanan_SelectedIndexChanged);
            // 
            // dgvPembayaran
            // 
            this.dgvPembayaran.AllowUserToAddRows = false;
            this.dgvPembayaran.AllowUserToDeleteRows = false;
            this.dgvPembayaran.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPembayaran.BackgroundColor = System.Drawing.Color.White;
            this.dgvPembayaran.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPembayaran.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPembayaran.ColumnHeadersHeight = 34;
            this.dgvPembayaran.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvPembayaran.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colIDPembayaran,
            this.colIDPemesanan,
            this.colNamaPelanggan,
            this.colNamaPaket,
            this.colJumlahPembayaran,
            this.colTangggalPembayaran,
            this.colMetodePembayaran,
            this.colTanggalPemesanan});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPembayaran.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvPembayaran.EnableHeadersVisualStyles = false;
            this.dgvPembayaran.GridColor = System.Drawing.Color.Gainsboro;
            this.dgvPembayaran.Location = new System.Drawing.Point(23, 306);
            this.dgvPembayaran.MultiSelect = false;
            this.dgvPembayaran.Name = "dgvPembayaran";
            this.dgvPembayaran.ReadOnly = true;
            this.dgvPembayaran.RowHeadersVisible = false;
            this.dgvPembayaran.RowHeadersWidth = 62;
            this.dgvPembayaran.RowTemplate.Height = 28;
            this.dgvPembayaran.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPembayaran.Size = new System.Drawing.Size(888, 365);
            this.dgvPembayaran.TabIndex = 16;
            this.dgvPembayaran.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPembayaran_CellContentClick);
            // 
            // colIDPembayaran
            // 
            this.colIDPembayaran.DataPropertyName = "IDPembayaran";
            this.colIDPembayaran.HeaderText = "ID";
            this.colIDPembayaran.MinimumWidth = 8;
            this.colIDPembayaran.Name = "colIDPembayaran";
            this.colIDPembayaran.ReadOnly = true;
            this.colIDPembayaran.Visible = false;
            this.colIDPembayaran.Width = 50;
            // 
            // colIDPemesanan
            // 
            this.colIDPemesanan.DataPropertyName = "IDPemesanan";
            this.colIDPemesanan.HeaderText = "ID Pesan";
            this.colIDPemesanan.MinimumWidth = 8;
            this.colIDPemesanan.Name = "colIDPemesanan";
            this.colIDPemesanan.ReadOnly = true;
            this.colIDPemesanan.Width = 80;
            // 
            // colNamaPelanggan
            // 
            this.colNamaPelanggan.DataPropertyName = "NamaPelanggan";
            this.colNamaPelanggan.HeaderText = "Pelanggan";
            this.colNamaPelanggan.MinimumWidth = 8;
            this.colNamaPelanggan.Name = "colNamaPelanggan";
            this.colNamaPelanggan.ReadOnly = true;
            this.colNamaPelanggan.Width = 150;
            // 
            // colNamaPaket
            // 
            this.colNamaPaket.DataPropertyName = "NamaPaket";
            this.colNamaPaket.HeaderText = "Paket Wisata";
            this.colNamaPaket.MinimumWidth = 8;
            this.colNamaPaket.Name = "colNamaPaket";
            this.colNamaPaket.ReadOnly = true;
            this.colNamaPaket.Width = 150;
            // 
            // colJumlahPembayaran
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N0";
            dataGridViewCellStyle2.NullValue = null;
            this.colJumlahPembayaran.DefaultCellStyle = dataGridViewCellStyle2;
            this.colJumlahPembayaran.HeaderText = "Jumlah Bayar";
            this.colJumlahPembayaran.MinimumWidth = 8;
            this.colJumlahPembayaran.Name = "colJumlahPembayaran";
            this.colJumlahPembayaran.ReadOnly = true;
            this.colJumlahPembayaran.Width = 150;
            // 
            // colTangggalPembayaran
            // 
            this.colTangggalPembayaran.DataPropertyName = "TanggalPembayaran";
            dataGridViewCellStyle3.Format = "f";
            dataGridViewCellStyle3.NullValue = null;
            this.colTangggalPembayaran.DefaultCellStyle = dataGridViewCellStyle3;
            this.colTangggalPembayaran.HeaderText = "Tanggal Bayar";
            this.colTangggalPembayaran.MinimumWidth = 8;
            this.colTangggalPembayaran.Name = "colTangggalPembayaran";
            this.colTangggalPembayaran.ReadOnly = true;
            this.colTangggalPembayaran.Width = 130;
            // 
            // colMetodePembayaran
            // 
            this.colMetodePembayaran.DataPropertyName = "MetodePembayaran";
            this.colMetodePembayaran.HeaderText = "Metode";
            this.colMetodePembayaran.MinimumWidth = 8;
            this.colMetodePembayaran.Name = "colMetodePembayaran";
            this.colMetodePembayaran.ReadOnly = true;
            this.colMetodePembayaran.Width = 150;
            // 
            // colTanggalPemesanan
            // 
            this.colTanggalPemesanan.DataPropertyName = "TanggalPemesanan";
            this.colTanggalPemesanan.HeaderText = "Tgl Pesanan";
            this.colTanggalPemesanan.MinimumWidth = 8;
            this.colTanggalPemesanan.Name = "colTanggalPemesanan";
            this.colTanggalPemesanan.ReadOnly = true;
            this.colTanggalPemesanan.Visible = false;
            this.colTanggalPemesanan.Width = 150;
            // 
            // cmbPemesanan
            // 
            this.cmbPemesanan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPemesanan.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbPemesanan.FormattingEnabled = true;
            this.cmbPemesanan.Location = new System.Drawing.Point(190, 100);
            this.cmbPemesanan.Name = "cmbPemesanan";
            this.cmbPemesanan.Size = new System.Drawing.Size(480, 33);
            this.cmbPemesanan.TabIndex = 4;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dateTimePicker1.Location = new System.Drawing.Point(190, 174);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(480, 31);
            this.dateTimePicker1.TabIndex = 8;
            // 
            // dtpPemesananFilterStart
            // 
            this.dtpPemesananFilterStart.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpPemesananFilterStart.Location = new System.Drawing.Point(160, 20);
            this.dtpPemesananFilterStart.Name = "dtpPemesananFilterStart";
            this.dtpPemesananFilterStart.Size = new System.Drawing.Size(220, 31);
            this.dtpPemesananFilterStart.TabIndex = 1;
            // 
            // lblPemesananFilterStart
            // 
            this.lblPemesananFilterStart.AutoSize = true;
            this.lblPemesananFilterStart.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPemesananFilterStart.Location = new System.Drawing.Point(20, 24);
            this.lblPemesananFilterStart.Name = "lblPemesananFilterStart";
            this.lblPemesananFilterStart.Size = new System.Drawing.Size(178, 25);
            this.lblPemesananFilterStart.TabIndex = 0;
            this.lblPemesananFilterStart.Text = "Filter Tgl Pesan Dari : ";
            // 
            // dtpPemesananFilterEnd
            // 
            this.dtpPemesananFilterEnd.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpPemesananFilterEnd.Location = new System.Drawing.Point(450, 20);
            this.dtpPemesananFilterEnd.Name = "dtpPemesananFilterEnd";
            this.dtpPemesananFilterEnd.Size = new System.Drawing.Size(220, 31);
            this.dtpPemesananFilterEnd.TabIndex = 2;
            // 
            // lblPemesananFilterEnd
            // 
            this.lblPemesananFilterEnd.AutoSize = true;
            this.lblPemesananFilterEnd.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPemesananFilterEnd.Location = new System.Drawing.Point(390, 24);
            this.lblPemesananFilterEnd.Name = "lblPemesananFilterEnd";
            this.lblPemesananFilterEnd.Size = new System.Drawing.Size(80, 25);
            this.lblPemesananFilterEnd.TabIndex = 0;
            this.lblPemesananFilterEnd.Text = "Sampai :";
            // 
            // dtpPembayaranFilterStart
            // 
            this.dtpPembayaranFilterStart.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpPembayaranFilterStart.Location = new System.Drawing.Point(160, 55);
            this.dtpPembayaranFilterStart.Name = "dtpPembayaranFilterStart";
            this.dtpPembayaranFilterStart.Size = new System.Drawing.Size(220, 31);
            this.dtpPembayaranFilterStart.TabIndex = 3;
            // 
            // lblPembayaranFilterStart
            // 
            this.lblPembayaranFilterStart.AutoSize = true;
            this.lblPembayaranFilterStart.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPembayaranFilterStart.Location = new System.Drawing.Point(20, 59);
            this.lblPembayaranFilterStart.Name = "lblPembayaranFilterStart";
            this.lblPembayaranFilterStart.Size = new System.Drawing.Size(176, 25);
            this.lblPembayaranFilterStart.TabIndex = 0;
            this.lblPembayaranFilterStart.Text = "Filter Tgl Bayar Dari : ";
            // 
            // dtpPembayaranFilterEnd
            // 
            this.dtpPembayaranFilterEnd.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpPembayaranFilterEnd.Location = new System.Drawing.Point(450, 55);
            this.dtpPembayaranFilterEnd.Name = "dtpPembayaranFilterEnd";
            this.dtpPembayaranFilterEnd.Size = new System.Drawing.Size(220, 31);
            this.dtpPembayaranFilterEnd.TabIndex = 4;
            // 
            // lblPembayaranFilterEnd
            // 
            this.lblPembayaranFilterEnd.AutoSize = true;
            this.lblPembayaranFilterEnd.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPembayaranFilterEnd.Location = new System.Drawing.Point(390, 59);
            this.lblPembayaranFilterEnd.Name = "lblPembayaranFilterEnd";
            this.lblPembayaranFilterEnd.Size = new System.Drawing.Size(80, 25);
            this.lblPembayaranFilterEnd.TabIndex = 0;
            this.lblPembayaranFilterEnd.Text = "Sampai :";
            // 
            // btnFilter
            // 
            this.btnFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnFilter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFilter.FlatAppearance.BorderSize = 0;
            this.btnFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFilter.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnFilter.ForeColor = System.Drawing.Color.White;
            this.btnFilter.Location = new System.Drawing.Point(777, 20);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(120, 58);
            this.btnFilter.TabIndex = 5;
            this.btnFilter.Text = "Filter";
            this.btnFilter.UseVisualStyleBackColor = false;
            // 
            // comboBoxMetode
            // 
            this.comboBoxMetode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMetode.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.comboBoxMetode.FormattingEnabled = true;
            this.comboBoxMetode.Location = new System.Drawing.Point(190, 211);
            this.comboBoxMetode.Name = "comboBoxMetode";
            this.comboBoxMetode.Size = new System.Drawing.Size(480, 33);
            this.comboBoxMetode.TabIndex = 9;
            // 
            // lblPemesanan
            // 
            this.lblPemesanan.AutoSize = true;
            this.lblPemesanan.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPemesanan.Location = new System.Drawing.Point(20, 103);
            this.lblPemesanan.Name = "lblPemesanan";
            this.lblPemesanan.Size = new System.Drawing.Size(110, 25);
            this.lblPemesanan.TabIndex = 3;
            this.lblPemesanan.Text = "Pemesanan :";
            // 
            // btnKembali
            // 
            this.btnKembali.Location = new System.Drawing.Point(791, 195);
            this.btnKembali.Name = "btnKembali";
            this.btnKembali.Size = new System.Drawing.Size(173, 53);
            this.btnKembali.TabIndex = 17;
            this.btnKembali.Text = "Kembali";
            this.btnKembali.UseVisualStyleBackColor = true;
            this.btnKembali.Click += new System.EventHandler(this.btnKembali_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(190, 259);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(398, 31);
            this.textBox1.TabIndex = 20;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSearch.Location = new System.Drawing.Point(139, 262);
            this.lblSearch.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(46, 25);
            this.lblSearch.TabIndex = 21;
            this.lblSearch.Text = "Cari:";
            // 
            // Pembayaran
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(976, 692);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnKembali);
            this.Controls.Add(this.lblPemesanan);
            this.Controls.Add(this.comboBoxMetode);
            this.Controls.Add(this.lblPemesananFilterStart);
            this.Controls.Add(this.dtpPemesananFilterStart);
            this.Controls.Add(this.lblPemesananFilterEnd);
            this.Controls.Add(this.dtpPemesananFilterEnd);
            this.Controls.Add(this.lblPembayaranFilterStart);
            this.Controls.Add(this.dtpPembayaranFilterStart);
            this.Controls.Add(this.lblPembayaranFilterEnd);
            this.Controls.Add(this.dtpPembayaranFilterEnd);
            this.Controls.Add(this.btnFilter);
            this.Controls.Add(this.cmbPemesanan);
            this.Controls.Add(this.lblJumlahPembayaran);
            this.Controls.Add(this.txtJumlah);
            this.Controls.Add(this.lblTanggalPembayaran);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.lblMetodePembayaran);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnUbah);
            this.Controls.Add(this.btnHapus);
            this.Controls.Add(this.btnTambah);
            this.Controls.Add(this.dgvPembayaran);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "Pembayaran";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manajemen Data Pembayaran";
            this.Load += new System.EventHandler(this.Pembayaran_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPembayaran)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private System.Windows.Forms.DataGridViewTextBoxColumn colIDPembayaran;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIDPemesanan;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNamaPelanggan;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNamaPaket;
        private System.Windows.Forms.DataGridViewTextBoxColumn colJumlahPembayaran;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMetodePembayaran;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTanggalPemesanan;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTangggalPembayaran;
        private System.Windows.Forms.Button btnKembali;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label lblSearch;
    }
}