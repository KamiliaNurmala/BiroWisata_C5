using System.Windows.Forms;
using System.Drawing;

namespace BiroWisataForm
{
    partial class PaketWisata
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // --- Corrected Field Declarations (ONLY THESE ONES) ---
        private System.Windows.Forms.Label lblNamaPaket;
        private System.Windows.Forms.Label lblDestinasi;
        private System.Windows.Forms.Label lblHarga;
        private System.Windows.Forms.Label lblDurasi;
        private System.Windows.Forms.Label lblFasilitas;
        private System.Windows.Forms.Label lblKategori;
        private System.Windows.Forms.Label lblKuota;
        private System.Windows.Forms.Label lblJadwal;
        private System.Windows.Forms.Label lblDriver;
        private System.Windows.Forms.Label lblKendaraan;
        private System.Windows.Forms.TextBox txtNamaPaket;
        private System.Windows.Forms.TextBox txtDestinasi;
        private System.Windows.Forms.TextBox txtHarga;
        private System.Windows.Forms.TextBox txtDurasi;
        private System.Windows.Forms.TextBox txtFasilitas;
        private System.Windows.Forms.TextBox txtKuota;
        private System.Windows.Forms.Button btnTambah;
        private System.Windows.Forms.Button btnHapus;
        private System.Windows.Forms.Button btnUbah;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridView dgvPaketWisata;
        private System.Windows.Forms.ComboBox cmbDriver;
        private System.Windows.Forms.ComboBox cmbKendaraan;
        private System.Windows.Forms.DateTimePicker dtpJadwalKeberangkatan;
        private System.Windows.Forms.ComboBox cmbKategori;
        private System.Windows.Forms.Panel inputPanel;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNamaDriver; // Use this consistent name


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

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.inputPanel = new System.Windows.Forms.Panel();
            this.lblNamaPaket = new System.Windows.Forms.Label();
            this.txtNamaPaket = new System.Windows.Forms.TextBox();
            this.lblDriver = new System.Windows.Forms.Label();
            this.cmbDriver = new System.Windows.Forms.ComboBox();
            this.lblKendaraan = new System.Windows.Forms.Label();
            this.cmbKendaraan = new System.Windows.Forms.ComboBox();
            this.lblDestinasi = new System.Windows.Forms.Label();
            this.txtDestinasi = new System.Windows.Forms.TextBox();
            this.lblHarga = new System.Windows.Forms.Label();
            this.txtHarga = new System.Windows.Forms.TextBox();
            this.lblDurasi = new System.Windows.Forms.Label();
            this.txtDurasi = new System.Windows.Forms.TextBox();
            this.lblFasilitas = new System.Windows.Forms.Label();
            this.txtFasilitas = new System.Windows.Forms.TextBox();
            this.lblKategori = new System.Windows.Forms.Label();
            this.cmbKategori = new System.Windows.Forms.ComboBox();
            this.lblKuota = new System.Windows.Forms.Label();
            this.txtKuota = new System.Windows.Forms.TextBox();
            this.lblJadwal = new System.Windows.Forms.Label();
            this.dtpJadwalKeberangkatan = new System.Windows.Forms.DateTimePicker();
            this.btnTambah = new System.Windows.Forms.Button();
            this.btnUbah = new System.Windows.Forms.Button();
            this.btnHapus = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.dgvPaketWisata = new System.Windows.Forms.DataGridView();
            this.colIDPaket = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNamaPaket = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colKendaraanInfo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDestinasi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHarga = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDurasi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFasilitas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colKategori = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colKuota = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colJadwal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIDDriver_hidden = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIDKendaraan_hidden = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnKembali = new System.Windows.Forms.Button();
            this.inputPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPaketWisata)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // inputPanel
            // 
            this.inputPanel.BackColor = System.Drawing.Color.White;
            this.inputPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inputPanel.Controls.Add(this.lblNamaPaket);
            this.inputPanel.Controls.Add(this.txtNamaPaket);
            this.inputPanel.Controls.Add(this.lblDriver);
            this.inputPanel.Controls.Add(this.cmbDriver);
            this.inputPanel.Controls.Add(this.lblKendaraan);
            this.inputPanel.Controls.Add(this.cmbKendaraan);
            this.inputPanel.Controls.Add(this.lblDestinasi);
            this.inputPanel.Controls.Add(this.txtDestinasi);
            this.inputPanel.Controls.Add(this.lblHarga);
            this.inputPanel.Controls.Add(this.txtHarga);
            this.inputPanel.Controls.Add(this.lblDurasi);
            this.inputPanel.Controls.Add(this.txtDurasi);
            this.inputPanel.Controls.Add(this.lblFasilitas);
            this.inputPanel.Controls.Add(this.txtFasilitas);
            this.inputPanel.Controls.Add(this.lblKategori);
            this.inputPanel.Controls.Add(this.cmbKategori);
            this.inputPanel.Controls.Add(this.lblKuota);
            this.inputPanel.Controls.Add(this.txtKuota);
            this.inputPanel.Controls.Add(this.lblJadwal);
            this.inputPanel.Controls.Add(this.dtpJadwalKeberangkatan);
            this.inputPanel.Location = new System.Drawing.Point(20, 20);
            this.inputPanel.Name = "inputPanel";
            this.inputPanel.Size = new System.Drawing.Size(900, 290);
            this.inputPanel.TabIndex = 0;
            this.inputPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.inputPanel_Paint);
            // 
            // lblNamaPaket
            // 
            this.lblNamaPaket.AutoSize = true;
            this.lblNamaPaket.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNamaPaket.Location = new System.Drawing.Point(15, 18);
            this.lblNamaPaket.Name = "lblNamaPaket";
            this.lblNamaPaket.Size = new System.Drawing.Size(74, 15);
            this.lblNamaPaket.TabIndex = 0;
            this.lblNamaPaket.Text = "Nama Paket:";
            // 
            // txtNamaPaket
            // 
            this.txtNamaPaket.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtNamaPaket.Location = new System.Drawing.Point(180, 15);
            this.txtNamaPaket.Name = "txtNamaPaket";
            this.txtNamaPaket.Size = new System.Drawing.Size(280, 23);
            this.txtNamaPaket.TabIndex = 1;
            // 
            // lblDriver
            // 
            this.lblDriver.AutoSize = true;
            this.lblDriver.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDriver.Location = new System.Drawing.Point(480, 18);
            this.lblDriver.Name = "lblDriver";
            this.lblDriver.Size = new System.Drawing.Size(41, 15);
            this.lblDriver.TabIndex = 20;
            this.lblDriver.Text = "Driver:";
            // 
            // cmbDriver
            // 
            this.cmbDriver.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDriver.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbDriver.FormattingEnabled = true;
            this.cmbDriver.Location = new System.Drawing.Point(600, 15);
            this.cmbDriver.Name = "cmbDriver";
            this.cmbDriver.Size = new System.Drawing.Size(280, 23);
            this.cmbDriver.TabIndex = 2;
            // 
            // lblKendaraan
            // 
            this.lblKendaraan.AutoSize = true;
            this.lblKendaraan.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblKendaraan.Location = new System.Drawing.Point(480, 58);
            this.lblKendaraan.Name = "lblKendaraan";
            this.lblKendaraan.Size = new System.Drawing.Size(66, 15);
            this.lblKendaraan.TabIndex = 22;
            this.lblKendaraan.Text = "Kendaraan:";
            // 
            // cmbKendaraan
            // 
            this.cmbKendaraan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbKendaraan.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbKendaraan.FormattingEnabled = true;
            this.cmbKendaraan.Location = new System.Drawing.Point(600, 55);
            this.cmbKendaraan.Name = "cmbKendaraan";
            this.cmbKendaraan.Size = new System.Drawing.Size(280, 23);
            this.cmbKendaraan.TabIndex = 3;
            // 
            // lblDestinasi
            // 
            this.lblDestinasi.AutoSize = true;
            this.lblDestinasi.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDestinasi.Location = new System.Drawing.Point(15, 58);
            this.lblDestinasi.Name = "lblDestinasi";
            this.lblDestinasi.Size = new System.Drawing.Size(57, 15);
            this.lblDestinasi.TabIndex = 2;
            this.lblDestinasi.Text = "Destinasi:";
            // 
            // txtDestinasi
            // 
            this.txtDestinasi.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtDestinasi.Location = new System.Drawing.Point(180, 55);
            this.txtDestinasi.Multiline = true;
            this.txtDestinasi.Name = "txtDestinasi";
            this.txtDestinasi.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDestinasi.Size = new System.Drawing.Size(280, 60);
            this.txtDestinasi.TabIndex = 4;
            // 
            // lblHarga
            // 
            this.lblHarga.AutoSize = true;
            this.lblHarga.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblHarga.Location = new System.Drawing.Point(15, 128);
            this.lblHarga.Name = "lblHarga";
            this.lblHarga.Size = new System.Drawing.Size(42, 15);
            this.lblHarga.TabIndex = 4;
            this.lblHarga.Text = "Harga:";
            // 
            // txtHarga
            // 
            this.txtHarga.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtHarga.Location = new System.Drawing.Point(180, 125);
            this.txtHarga.Name = "txtHarga";
            this.txtHarga.Size = new System.Drawing.Size(280, 23);
            this.txtHarga.TabIndex = 5;
            // 
            // lblDurasi
            // 
            this.lblDurasi.AutoSize = true;
            this.lblDurasi.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDurasi.Location = new System.Drawing.Point(480, 98);
            this.lblDurasi.Name = "lblDurasi";
            this.lblDurasi.Size = new System.Drawing.Size(76, 15);
            this.lblDurasi.TabIndex = 6;
            this.lblDurasi.Text = "Durasi (Hari):";
            // 
            // txtDurasi
            // 
            this.txtDurasi.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtDurasi.Location = new System.Drawing.Point(600, 95);
            this.txtDurasi.Name = "txtDurasi";
            this.txtDurasi.Size = new System.Drawing.Size(280, 23);
            this.txtDurasi.TabIndex = 6;
            // 
            // lblFasilitas
            // 
            this.lblFasilitas.AutoSize = true;
            this.lblFasilitas.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblFasilitas.Location = new System.Drawing.Point(15, 168);
            this.lblFasilitas.Name = "lblFasilitas";
            this.lblFasilitas.Size = new System.Drawing.Size(51, 15);
            this.lblFasilitas.TabIndex = 8;
            this.lblFasilitas.Text = "Fasilitas:";
            // 
            // txtFasilitas
            // 
            this.txtFasilitas.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtFasilitas.Location = new System.Drawing.Point(180, 165);
            this.txtFasilitas.Multiline = true;
            this.txtFasilitas.Name = "txtFasilitas";
            this.txtFasilitas.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtFasilitas.Size = new System.Drawing.Size(280, 60);
            this.txtFasilitas.TabIndex = 7;
            this.txtFasilitas.TextChanged += new System.EventHandler(this.txtFasilitas_TextChanged);
            // 
            // lblKategori
            // 
            this.lblKategori.AutoSize = true;
            this.lblKategori.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblKategori.Location = new System.Drawing.Point(480, 138);
            this.lblKategori.Name = "lblKategori";
            this.lblKategori.Size = new System.Drawing.Size(54, 15);
            this.lblKategori.TabIndex = 10;
            this.lblKategori.Text = "Kategori:";
            // 
            // cmbKategori
            // 
            this.cmbKategori.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbKategori.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbKategori.FormattingEnabled = true;
            this.cmbKategori.Location = new System.Drawing.Point(600, 135);
            this.cmbKategori.Name = "cmbKategori";
            this.cmbKategori.Size = new System.Drawing.Size(280, 23);
            this.cmbKategori.TabIndex = 8;
            // 
            // lblKuota
            // 
            this.lblKuota.AutoSize = true;
            this.lblKuota.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblKuota.Location = new System.Drawing.Point(480, 178);
            this.lblKuota.Name = "lblKuota";
            this.lblKuota.Size = new System.Drawing.Size(41, 15);
            this.lblKuota.TabIndex = 12;
            this.lblKuota.Text = "Kuota:";
            // 
            // txtKuota
            // 
            this.txtKuota.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtKuota.Location = new System.Drawing.Point(600, 175);
            this.txtKuota.Name = "txtKuota";
            this.txtKuota.Size = new System.Drawing.Size(280, 23);
            this.txtKuota.TabIndex = 9;
            this.txtKuota.TextChanged += new System.EventHandler(this.txtKuota_TextChanged);
            // 
            // lblJadwal
            // 
            this.lblJadwal.AutoSize = true;
            this.lblJadwal.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblJadwal.Location = new System.Drawing.Point(15, 238);
            this.lblJadwal.Name = "lblJadwal";
            this.lblJadwal.Size = new System.Drawing.Size(127, 15);
            this.lblJadwal.TabIndex = 14;
            this.lblJadwal.Text = "Jadwal Keberangkatan:";
            // 
            // dtpJadwalKeberangkatan
            // 
            this.dtpJadwalKeberangkatan.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpJadwalKeberangkatan.Location = new System.Drawing.Point(180, 235);
            this.dtpJadwalKeberangkatan.Name = "dtpJadwalKeberangkatan";
            this.dtpJadwalKeberangkatan.Size = new System.Drawing.Size(280, 23);
            this.dtpJadwalKeberangkatan.TabIndex = 10;
            // 
            // btnTambah
            // 
            this.btnTambah.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnTambah.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTambah.FlatAppearance.BorderSize = 0;
            this.btnTambah.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTambah.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnTambah.ForeColor = System.Drawing.Color.White;
            this.btnTambah.Location = new System.Drawing.Point(940, 20);
            this.btnTambah.Name = "btnTambah";
            this.btnTambah.Size = new System.Drawing.Size(120, 40);
            this.btnTambah.TabIndex = 11;
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
            this.btnUbah.Location = new System.Drawing.Point(940, 70);
            this.btnUbah.Name = "btnUbah";
            this.btnUbah.Size = new System.Drawing.Size(120, 40);
            this.btnUbah.TabIndex = 12;
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
            this.btnHapus.Location = new System.Drawing.Point(940, 120);
            this.btnHapus.Name = "btnHapus";
            this.btnHapus.Size = new System.Drawing.Size(120, 40);
            this.btnHapus.TabIndex = 13;
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
            this.btnRefresh.Location = new System.Drawing.Point(940, 170);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(120, 40);
            this.btnRefresh.TabIndex = 14;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // dgvPaketWisata
            // 
            this.dgvPaketWisata.AllowUserToAddRows = false;
            this.dgvPaketWisata.AllowUserToDeleteRows = false;
            this.dgvPaketWisata.BackgroundColor = System.Drawing.Color.White;
            this.dgvPaketWisata.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPaketWisata.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPaketWisata.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPaketWisata.ColumnHeadersHeight = 34;
            this.dgvPaketWisata.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvPaketWisata.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colIDPaket,
            this.colNamaPaket,
            this.colKendaraanInfo,
            this.colDestinasi,
            this.colHarga,
            this.colDurasi,
            this.colFasilitas,
            this.colKategori,
            this.colKuota,
            this.colJadwal,
            this.colIDDriver_hidden,
            this.colIDKendaraan_hidden});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPaketWisata.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvPaketWisata.EnableHeadersVisualStyles = false;
            this.dgvPaketWisata.GridColor = System.Drawing.Color.Gainsboro;
            this.dgvPaketWisata.Location = new System.Drawing.Point(20, 360);
            this.dgvPaketWisata.MultiSelect = false;
            this.dgvPaketWisata.Name = "dgvPaketWisata";
            this.dgvPaketWisata.ReadOnly = true;
            this.dgvPaketWisata.RowHeadersVisible = false;
            this.dgvPaketWisata.RowHeadersWidth = 62;
            this.dgvPaketWisata.RowTemplate.Height = 28;
            this.dgvPaketWisata.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPaketWisata.Size = new System.Drawing.Size(1140, 269);
            this.dgvPaketWisata.TabIndex = 16;
            this.dgvPaketWisata.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvPaketWisata_CellClick);
            this.dgvPaketWisata.SelectionChanged += new System.EventHandler(this.DgvPaketWisata_SelectionChanged);
            // 
            // colIDPaket
            // 
            this.colIDPaket.DataPropertyName = "IDPaket";
            this.colIDPaket.HeaderText = "ID";
            this.colIDPaket.MinimumWidth = 8;
            this.colIDPaket.Name = "colIDPaket";
            this.colIDPaket.ReadOnly = true;
            this.colIDPaket.Visible = false;
            this.colIDPaket.Width = 150;
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
            // colKendaraanInfo
            // 
            this.colKendaraanInfo.DataPropertyName = "KendaraanInfo";
            this.colKendaraanInfo.HeaderText = "Kendaraan";
            this.colKendaraanInfo.MinimumWidth = 8;
            this.colKendaraanInfo.Name = "colKendaraanInfo";
            this.colKendaraanInfo.ReadOnly = true;
            this.colKendaraanInfo.Width = 150;
            // 
            // colDestinasi
            // 
            this.colDestinasi.DataPropertyName = "Destinasi";
            this.colDestinasi.HeaderText = "Destinasi";
            this.colDestinasi.MinimumWidth = 8;
            this.colDestinasi.Name = "colDestinasi";
            this.colDestinasi.ReadOnly = true;
            this.colDestinasi.Width = 180;
            // 
            // colHarga
            // 
            this.colHarga.DataPropertyName = "Harga";
            dataGridViewCellStyle2.Format = "N0";
            dataGridViewCellStyle2.NullValue = "0";
            this.colHarga.DefaultCellStyle = dataGridViewCellStyle2;
            this.colHarga.HeaderText = "Harga (Rp)";
            this.colHarga.MinimumWidth = 8;
            this.colHarga.Name = "colHarga";
            this.colHarga.ReadOnly = true;
            // 
            // colDurasi
            // 
            this.colDurasi.DataPropertyName = "Durasi";
            this.colDurasi.HeaderText = "Durasi (Hari)";
            this.colDurasi.MinimumWidth = 8;
            this.colDurasi.Name = "colDurasi";
            this.colDurasi.ReadOnly = true;
            this.colDurasi.Width = 80;
            // 
            // colFasilitas
            // 
            this.colFasilitas.DataPropertyName = "Fasilitas";
            this.colFasilitas.HeaderText = "Fasilitas";
            this.colFasilitas.MinimumWidth = 8;
            this.colFasilitas.Name = "colFasilitas";
            this.colFasilitas.ReadOnly = true;
            this.colFasilitas.Width = 230;
            // 
            // colKategori
            // 
            this.colKategori.DataPropertyName = "Kategori";
            this.colKategori.HeaderText = "Kategori";
            this.colKategori.MinimumWidth = 8;
            this.colKategori.Name = "colKategori";
            this.colKategori.ReadOnly = true;
            this.colKategori.Width = 120;
            // 
            // colKuota
            // 
            this.colKuota.DataPropertyName = "Kuota";
            this.colKuota.HeaderText = "Kuota";
            this.colKuota.MinimumWidth = 8;
            this.colKuota.Name = "colKuota";
            this.colKuota.ReadOnly = true;
            this.colKuota.Width = 70;
            // 
            // colJadwal
            // 
            this.colJadwal.DataPropertyName = "JadwalKeberangkatan";
            dataGridViewCellStyle3.Format = "dd/MM/yyyy";
            this.colJadwal.DefaultCellStyle = dataGridViewCellStyle3;
            this.colJadwal.HeaderText = "Jadwal";
            this.colJadwal.MinimumWidth = 8;
            this.colJadwal.Name = "colJadwal";
            this.colJadwal.ReadOnly = true;
            // 
            // colIDDriver_hidden
            // 
            this.colIDDriver_hidden.DataPropertyName = "IDDriver";
            this.colIDDriver_hidden.HeaderText = "IDDriver_H";
            this.colIDDriver_hidden.MinimumWidth = 8;
            this.colIDDriver_hidden.Name = "colIDDriver_hidden";
            this.colIDDriver_hidden.ReadOnly = true;
            this.colIDDriver_hidden.Visible = false;
            this.colIDDriver_hidden.Width = 150;
            // 
            // colIDKendaraan_hidden
            // 
            this.colIDKendaraan_hidden.DataPropertyName = "IDKendaraan";
            this.colIDKendaraan_hidden.HeaderText = "IDKendaraan_H";
            this.colIDKendaraan_hidden.MinimumWidth = 8;
            this.colIDKendaraan_hidden.Name = "colIDKendaraan_hidden";
            this.colIDKendaraan_hidden.ReadOnly = true;
            this.colIDKendaraan_hidden.Visible = false;
            this.colIDKendaraan_hidden.Width = 150;
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSearch.Location = new System.Drawing.Point(20, 325);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(31, 15);
            this.lblSearch.TabIndex = 15;
            this.lblSearch.Text = "Cari:";
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSearch.Location = new System.Drawing.Point(80, 322);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(400, 23);
            this.txtSearch.TabIndex = 15;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // btnKembali
            // 
            this.btnKembali.Location = new System.Drawing.Point(940, 226);
            this.btnKembali.Name = "btnKembali";
            this.btnKembali.Size = new System.Drawing.Size(173, 53);
            this.btnKembali.TabIndex = 17;
            this.btnKembali.Text = "Kembali";
            this.btnKembali.UseVisualStyleBackColor = true;
            this.btnKembali.Click += new System.EventHandler(this.btnKembali_Click);
            // 
            // PaketWisata
            // 
            this.ClientSize = new System.Drawing.Size(1188, 641);
            this.Controls.Add(this.btnKembali);
            this.Controls.Add(this.inputPanel);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnUbah);
            this.Controls.Add(this.btnHapus);
            this.Controls.Add(this.btnTambah);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.dgvPaketWisata);
            this.Name = "PaketWisata";
            this.Text = "Manajemen Data Paket Wisata";
            this.Load += new System.EventHandler(this.PaketWisata_Load);
            this.inputPanel.ResumeLayout(false);
            this.inputPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPaketWisata)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private DataGridViewTextBoxColumn IDKendaraan;
        private DataGridViewTextBoxColumn colIDPaket;
        private DataGridViewTextBoxColumn colNamaPaket;
        private DataGridViewTextBoxColumn colKendaraanInfo;
        private DataGridViewTextBoxColumn colDestinasi;
        private DataGridViewTextBoxColumn colHarga;
        private DataGridViewTextBoxColumn colDurasi;
        private DataGridViewTextBoxColumn colFasilitas;
        private DataGridViewTextBoxColumn colKategori;
        private DataGridViewTextBoxColumn colKuota;
        private DataGridViewTextBoxColumn colJadwal;
        private DataGridViewTextBoxColumn colIDDriver_hidden;
        private DataGridViewTextBoxColumn colIDKendaraan_hidden;
        private Button btnKembali;
        #endregion

        // --- Field Declarations for DGV Columns (Removed the duplicate block after #endregion) ---
    }
}
