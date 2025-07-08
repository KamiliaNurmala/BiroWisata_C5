using System.Windows.Forms;
using System.Drawing;

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

        // Helper method for styling buttons (can be kept here or moved to the main .cs file if preferred)
        //private void StyleButton(Button btn, Color backColor, Color foreColor)
        //{
        //    btn.FlatStyle = FlatStyle.Flat;
        //    btn.FlatAppearance.BorderSize = 0;
        //    btn.BackColor = backColor;
        //    btn.ForeColor = foreColor;
        //    btn.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
        //    btn.Size = new Size(120, 40); // Consistent button size
        //    btn.Cursor = Cursors.Hand;
        //}

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.inputPanel = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.btnKembali = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDriver)).BeginInit();
            this.inputPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblNamaDriver
            // 
            this.lblNamaDriver.AutoSize = true;
            this.lblNamaDriver.Location = new System.Drawing.Point(15, 18);
            this.lblNamaDriver.Name = "lblNamaDriver";
            this.lblNamaDriver.Size = new System.Drawing.Size(72, 13);
            this.lblNamaDriver.TabIndex = 0;
            this.lblNamaDriver.Text = "Nama Driver :";
            // 
            // lblNoTel
            // 
            this.lblNoTel.AutoSize = true;
            this.lblNoTel.Location = new System.Drawing.Point(15, 58);
            this.lblNoTel.Name = "lblNoTel";
            this.lblNoTel.Size = new System.Drawing.Size(69, 13);
            this.lblNoTel.TabIndex = 2;
            this.lblNoTel.Text = "No Telepon :";
            // 
            // lblNoSIM
            // 
            this.lblNoSIM.AutoSize = true;
            this.lblNoSIM.Location = new System.Drawing.Point(15, 98);
            this.lblNoSIM.Name = "lblNoSIM";
            this.lblNoSIM.Size = new System.Drawing.Size(49, 13);
            this.lblNoSIM.TabIndex = 4;
            this.lblNoSIM.Text = "No SIM :";
            // 
            // txtNama
            // 
            this.txtNama.Location = new System.Drawing.Point(130, 15);
            this.txtNama.Name = "txtNama";
            this.txtNama.Size = new System.Drawing.Size(300, 20);
            this.txtNama.TabIndex = 1;
            // 
            // txtNoTel
            // 
            this.txtNoTel.Location = new System.Drawing.Point(130, 55);
            this.txtNoTel.Name = "txtNoTel";
            this.txtNoTel.Size = new System.Drawing.Size(300, 20);
            this.txtNoTel.TabIndex = 3;
            // 
            // txtNoSim
            // 
            this.txtNoSim.Location = new System.Drawing.Point(130, 95);
            this.txtNoSim.Name = "txtNoSim";
            this.txtNoSim.Size = new System.Drawing.Size(300, 20);
            this.txtNoSim.TabIndex = 5;
            // 
            // btnTambah
            // 
            this.btnTambah.Location = new System.Drawing.Point(490, 20);
            this.btnTambah.Name = "btnTambah";
            this.btnTambah.Size = new System.Drawing.Size(75, 23);
            this.btnTambah.TabIndex = 6;
            this.btnTambah.Text = "Tambah";
            this.btnTambah.UseVisualStyleBackColor = true;
            this.btnTambah.Click += new System.EventHandler(this.BtnTambah_Click);
            // 
            // btnHapus
            // 
            this.btnHapus.Location = new System.Drawing.Point(490, 70);
            this.btnHapus.Name = "btnHapus";
            this.btnHapus.Size = new System.Drawing.Size(75, 23);
            this.btnHapus.TabIndex = 7;
            this.btnHapus.Text = "Hapus";
            this.btnHapus.UseVisualStyleBackColor = true;
            this.btnHapus.Click += new System.EventHandler(this.BtnHapus_Click);
            // 
            // btnUbah
            // 
            this.btnUbah.Location = new System.Drawing.Point(620, 20);
            this.btnUbah.Name = "btnUbah";
            this.btnUbah.Size = new System.Drawing.Size(75, 23);
            this.btnUbah.TabIndex = 8;
            this.btnUbah.Text = "Ubah";
            this.btnUbah.UseVisualStyleBackColor = true;
            this.btnUbah.Click += new System.EventHandler(this.BtnUbah_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(620, 70);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 9;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // dgvDriver
            // 
            this.dgvDriver.AllowUserToAddRows = false;
            this.dgvDriver.AllowUserToDeleteRows = false;
            this.dgvDriver.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDriver.BackgroundColor = System.Drawing.Color.White;
            this.dgvDriver.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDriver.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDriver.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDriver.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvDriver.EnableHeadersVisualStyles = false;
            this.dgvDriver.Location = new System.Drawing.Point(20, 190);
            this.dgvDriver.MultiSelect = false;
            this.dgvDriver.Name = "dgvDriver";
            this.dgvDriver.ReadOnly = true;
            this.dgvDriver.RowHeadersVisible = false;
            this.dgvDriver.RowHeadersWidth = 62;
            this.dgvDriver.RowTemplate.Height = 28;
            this.dgvDriver.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDriver.Size = new System.Drawing.Size(720, 270);
            this.dgvDriver.TabIndex = 10;
            this.dgvDriver.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDriver_CellContentClick);
            // 
            // inputPanel
            // 
            this.inputPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inputPanel.Controls.Add(this.lblNamaDriver);
            this.inputPanel.Controls.Add(this.txtNama);
            this.inputPanel.Controls.Add(this.lblNoTel);
            this.inputPanel.Controls.Add(this.txtNoTel);
            this.inputPanel.Controls.Add(this.lblNoSIM);
            this.inputPanel.Controls.Add(this.txtNoSim);
            this.inputPanel.Controls.Add(this.lblStatus);
            this.inputPanel.Controls.Add(this.cmbStatus);
            this.inputPanel.Location = new System.Drawing.Point(20, 20);
            this.inputPanel.Name = "inputPanel";
            this.inputPanel.Size = new System.Drawing.Size(450, 168);
            this.inputPanel.TabIndex = 11;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(15, 138);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(43, 13);
            this.lblStatus.TabIndex = 6;
            this.lblStatus.Text = "Status :";
            // 
            // cmbStatus
            // 
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(130, 135);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(300, 21);
            this.cmbStatus.TabIndex = 7;
            // 
            // btnKembali
            // 
            this.btnKembali.Location = new System.Drawing.Point(510, 116);
            this.btnKembali.Name = "btnKembali";
            this.btnKembali.Size = new System.Drawing.Size(173, 53);
            this.btnKembali.TabIndex = 12;
            this.btnKembali.Text = "Kembali";
            this.btnKembali.UseVisualStyleBackColor = true;
            this.btnKembali.Click += new System.EventHandler(this.btnKembali_Click);
            // 
            // Driver
            // 
            this.ClientSize = new System.Drawing.Size(760, 500);
            this.Controls.Add(this.btnKembali);
            this.Controls.Add(this.inputPanel);
            this.Controls.Add(this.dgvDriver);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnUbah);
            this.Controls.Add(this.btnHapus);
            this.Controls.Add(this.btnTambah);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Driver";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manajemen Data Driver";
            this.Load += new System.EventHandler(this.Driver_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDriver)).EndInit();
            this.inputPanel.ResumeLayout(false);
            this.inputPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel inputPanel; // Declare the panel as a field
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
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cmbStatus;
        private Button btnKembali;
    }
}