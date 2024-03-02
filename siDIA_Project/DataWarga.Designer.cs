namespace siDIA_Project
{
    partial class DataWarga
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataWarga));
            this.topBar = new System.Windows.Forms.FlowLayoutPanel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.tCari = new System.Windows.Forms.TextBox();
            this.fWarga = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Noreg = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.tStatusBPJS = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.tPLain = new System.Windows.Forms.TextBox();
            this.cPekerjaan = new System.Windows.Forms.ComboBox();
            this.cPendidikan = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.cAgama = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.cNoReg = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tRT = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.cRT = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.tNama = new System.Windows.Forms.TextBox();
            this.tStatusKawin = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tAlamat = new System.Windows.Forms.TextBox();
            this.cJK = new System.Windows.Forms.ComboBox();
            this.tTempat = new System.Windows.Forms.TextBox();
            this.cKK = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tKK = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cJabatan = new System.Windows.Forms.ComboBox();
            this.tNIK = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.PictureBox();
            this.btnEdit = new System.Windows.Forms.PictureBox();
            this.btnRefresh = new System.Windows.Forms.PictureBox();
            this.label19 = new System.Windows.Forms.Label();
            this.jmlWarga = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.btnSimpan = new System.Windows.Forms.PictureBox();
            this.btnDelete = new System.Windows.Forms.PictureBox();
            this.label12 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.topBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnAdd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRefresh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSimpan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDelete)).BeginInit();
            this.SuspendLayout();
            // 
            // topBar
            // 
            this.topBar.BackColor = System.Drawing.Color.Linen;
            this.topBar.Controls.Add(this.pictureBox2);
            this.topBar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.topBar.Dock = System.Windows.Forms.DockStyle.Right;
            this.topBar.Location = new System.Drawing.Point(1076, 0);
            this.topBar.Name = "topBar";
            this.topBar.Size = new System.Drawing.Size(52, 849);
            this.topBar.TabIndex = 0;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(3, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(45, 41);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click_1);
            // 
            // tCari
            // 
            this.tCari.Location = new System.Drawing.Point(347, 93);
            this.tCari.Name = "tCari";
            this.tCari.Size = new System.Drawing.Size(280, 22);
            this.tCari.TabIndex = 1;
            this.tCari.TextChanged += new System.EventHandler(this.tCari_TextChanged);
            // 
            // fWarga
            // 
            this.fWarga.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fWarga.FormattingEnabled = true;
            this.fWarga.Items.AddRange(new object[] {
            "",
            "Balita",
            "PUS",
            "WUS",
            "Lansia",
            "PNS",
            "Wirausaha",
            "Pelajar/Mahasiswa",
            "Buruh/Karyawan",
            "Lainnya"});
            this.fWarga.Location = new System.Drawing.Point(633, 93);
            this.fWarga.Name = "fWarga";
            this.fWarga.Size = new System.Drawing.Size(121, 24);
            this.fWarga.TabIndex = 2;
            this.fWarga.SelectedIndexChanged += new System.EventHandler(this.fWarga_SelectedIndexChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(595, 144);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(446, 693);
            this.dataGridView1.TabIndex = 3;
            this.dataGridView1.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentDoubleClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dateTimePicker1);
            this.groupBox1.Controls.Add(this.Noreg);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.tStatusBPJS);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.tPLain);
            this.groupBox1.Controls.Add(this.cPekerjaan);
            this.groupBox1.Controls.Add(this.cPendidikan);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.cAgama);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.cNoReg);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.tRT);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.cRT);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.tNama);
            this.groupBox1.Controls.Add(this.tStatusKawin);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tAlamat);
            this.groupBox1.Controls.Add(this.cJK);
            this.groupBox1.Controls.Add(this.tTempat);
            this.groupBox1.Controls.Add(this.cKK);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.tKK);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.cJabatan);
            this.groupBox1.Controls.Add(this.tNIK);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(13, 144);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(549, 642);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Form Pengisian";
            // 
            // Noreg
            // 
            this.Noreg.Enabled = false;
            this.Noreg.Location = new System.Drawing.Point(258, 39);
            this.Noreg.Margin = new System.Windows.Forms.Padding(4);
            this.Noreg.Name = "Noreg";
            this.Noreg.Size = new System.Drawing.Size(244, 22);
            this.Noreg.TabIndex = 51;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(23, 39);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(93, 17);
            this.label20.TabIndex = 50;
            this.label20.Text = "No Registrasi";
            // 
            // tStatusBPJS
            // 
            this.tStatusBPJS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tStatusBPJS.Enabled = false;
            this.tStatusBPJS.FormattingEnabled = true;
            this.tStatusBPJS.Items.AddRange(new object[] {
            "Aktif",
            "Tidak Aktif"});
            this.tStatusBPJS.Location = new System.Drawing.Point(258, 606);
            this.tStatusBPJS.Margin = new System.Windows.Forms.Padding(4);
            this.tStatusBPJS.Name = "tStatusBPJS";
            this.tStatusBPJS.Size = new System.Drawing.Size(244, 24);
            this.tStatusBPJS.TabIndex = 49;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(23, 606);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(86, 17);
            this.label18.TabIndex = 48;
            this.label18.Text = "Status BPJS";
            // 
            // tPLain
            // 
            this.tPLain.Enabled = false;
            this.tPLain.Location = new System.Drawing.Point(258, 543);
            this.tPLain.Margin = new System.Windows.Forms.Padding(4);
            this.tPLain.Name = "tPLain";
            this.tPLain.Size = new System.Drawing.Size(244, 22);
            this.tPLain.TabIndex = 47;
            this.tPLain.Visible = false;
            // 
            // cPekerjaan
            // 
            this.cPekerjaan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cPekerjaan.Enabled = false;
            this.cPekerjaan.FormattingEnabled = true;
            this.cPekerjaan.Items.AddRange(new object[] {
            "PNS",
            "Wirausaha",
            "Pelajar/Mahasiswa",
            "Buruh/Karyawan",
            "Lainnya"});
            this.cPekerjaan.Location = new System.Drawing.Point(258, 511);
            this.cPekerjaan.Margin = new System.Windows.Forms.Padding(4);
            this.cPekerjaan.Name = "cPekerjaan";
            this.cPekerjaan.Size = new System.Drawing.Size(244, 24);
            this.cPekerjaan.TabIndex = 46;
            this.cPekerjaan.TextChanged += new System.EventHandler(this.cPekerjaan_TextChanged);
            // 
            // cPendidikan
            // 
            this.cPendidikan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cPendidikan.Enabled = false;
            this.cPendidikan.FormattingEnabled = true;
            this.cPendidikan.Items.AddRange(new object[] {
            "Tdk tamat SD",
            "SD/MI",
            "SMP/Sederajat",
            "SMU/SMK/Sederajat",
            "Diploma",
            "S1",
            "S2",
            "S3"});
            this.cPendidikan.Location = new System.Drawing.Point(258, 479);
            this.cPendidikan.Margin = new System.Windows.Forms.Padding(4);
            this.cPendidikan.Name = "cPendidikan";
            this.cPendidikan.Size = new System.Drawing.Size(244, 24);
            this.cPendidikan.TabIndex = 45;
            this.cPendidikan.TextChanged += new System.EventHandler(this.cPendidikan_TextChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(23, 479);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(78, 17);
            this.label17.TabIndex = 44;
            this.label17.Text = "Pendidikan";
            // 
            // cAgama
            // 
            this.cAgama.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cAgama.Enabled = false;
            this.cAgama.FormattingEnabled = true;
            this.cAgama.Items.AddRange(new object[] {
            "Islam",
            "Kristen",
            "Katolik",
            "Hindu",
            "Budha",
            "Khonghuchu",
            "Kepercayaan Lain"});
            this.cAgama.Location = new System.Drawing.Point(258, 358);
            this.cAgama.Margin = new System.Windows.Forms.Padding(4);
            this.cAgama.Name = "cAgama";
            this.cAgama.Size = new System.Drawing.Size(244, 24);
            this.cAgama.TabIndex = 43;
            this.cAgama.TextChanged += new System.EventHandler(this.cAgama_TextChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(23, 358);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(52, 17);
            this.label16.TabIndex = 42;
            this.label16.Text = "Agama";
            // 
            // cNoReg
            // 
            this.cNoReg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cNoReg.Enabled = false;
            this.cNoReg.FormattingEnabled = true;
            this.cNoReg.Location = new System.Drawing.Point(258, 229);
            this.cNoReg.Margin = new System.Windows.Forms.Padding(4);
            this.cNoReg.Name = "cNoReg";
            this.cNoReg.Size = new System.Drawing.Size(244, 24);
            this.cNoReg.TabIndex = 41;
            this.cNoReg.Visible = false;
            this.cNoReg.TextChanged += new System.EventHandler(this.cNoReg_TextChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(23, 199);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(172, 17);
            this.label14.TabIndex = 40;
            this.label14.Text = "Jabatan diRumah Tangga";
            // 
            // tRT
            // 
            this.tRT.Enabled = false;
            this.tRT.Location = new System.Drawing.Point(258, 231);
            this.tRT.Margin = new System.Windows.Forms.Padding(4);
            this.tRT.Name = "tRT";
            this.tRT.Size = new System.Drawing.Size(244, 22);
            this.tRT.TabIndex = 39;
            this.tRT.TextChanged += new System.EventHandler(this.tRT_TextChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(23, 231);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(75, 17);
            this.label15.TabIndex = 38;
            this.label15.Text = "No Rumah";
            // 
            // cRT
            // 
            this.cRT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cRT.Enabled = false;
            this.cRT.FormattingEnabled = true;
            this.cRT.Items.AddRange(new object[] {
            "Kepala Rumah Tangga",
            "Anggota Rumah Tangga"});
            this.cRT.Location = new System.Drawing.Point(258, 199);
            this.cRT.Margin = new System.Windows.Forms.Padding(4);
            this.cRT.Name = "cRT";
            this.cRT.Size = new System.Drawing.Size(244, 24);
            this.cRT.TabIndex = 37;
            this.cRT.TextChanged += new System.EventHandler(this.cRT_TextChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(23, 299);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(96, 17);
            this.label13.TabIndex = 35;
            this.label13.Text = "Tanggal Lahir";
            // 
            // tNama
            // 
            this.tNama.Enabled = false;
            this.tNama.Location = new System.Drawing.Point(258, 103);
            this.tNama.Margin = new System.Windows.Forms.Padding(4);
            this.tNama.Name = "tNama";
            this.tNama.Size = new System.Drawing.Size(244, 22);
            this.tNama.TabIndex = 34;
            this.tNama.TextChanged += new System.EventHandler(this.tNama_TextChanged);
            this.tNama.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tNama_KeyPress);
            // 
            // tStatusKawin
            // 
            this.tStatusKawin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tStatusKawin.Enabled = false;
            this.tStatusKawin.FormattingEnabled = true;
            this.tStatusKawin.Items.AddRange(new object[] {
            "Menikah",
            "Cerai",
            "Lajang"});
            this.tStatusKawin.Location = new System.Drawing.Point(258, 574);
            this.tStatusKawin.Margin = new System.Windows.Forms.Padding(4);
            this.tStatusKawin.Name = "tStatusKawin";
            this.tStatusKawin.Size = new System.Drawing.Size(244, 24);
            this.tStatusKawin.TabIndex = 31;
            this.tStatusKawin.TextChanged += new System.EventHandler(this.tStatusKawin_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 574);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 17);
            this.label3.TabIndex = 30;
            this.label3.Text = "Status Perkawinan";
            // 
            // tAlamat
            // 
            this.tAlamat.Enabled = false;
            this.tAlamat.Location = new System.Drawing.Point(258, 390);
            this.tAlamat.Margin = new System.Windows.Forms.Padding(4);
            this.tAlamat.Multiline = true;
            this.tAlamat.Name = "tAlamat";
            this.tAlamat.Size = new System.Drawing.Size(244, 81);
            this.tAlamat.TabIndex = 28;
            this.tAlamat.TextChanged += new System.EventHandler(this.tAlamat_TextChanged);
            // 
            // cJK
            // 
            this.cJK.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cJK.Enabled = false;
            this.cJK.FormattingEnabled = true;
            this.cJK.Items.AddRange(new object[] {
            "L",
            "P"});
            this.cJK.Location = new System.Drawing.Point(258, 326);
            this.cJK.Margin = new System.Windows.Forms.Padding(4);
            this.cJK.Name = "cJK";
            this.cJK.Size = new System.Drawing.Size(244, 24);
            this.cJK.TabIndex = 27;
            this.cJK.TextChanged += new System.EventHandler(this.cJK_TextChanged);
            // 
            // tTempat
            // 
            this.tTempat.Enabled = false;
            this.tTempat.Location = new System.Drawing.Point(258, 265);
            this.tTempat.Margin = new System.Windows.Forms.Padding(4);
            this.tTempat.Name = "tTempat";
            this.tTempat.Size = new System.Drawing.Size(244, 22);
            this.tTempat.TabIndex = 26;
            this.tTempat.TextChanged += new System.EventHandler(this.tTempat_TextChanged);
            // 
            // cKK
            // 
            this.cKK.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cKK.Enabled = false;
            this.cKK.FormattingEnabled = true;
            this.cKK.Location = new System.Drawing.Point(258, 165);
            this.cKK.Margin = new System.Windows.Forms.Padding(4);
            this.cKK.Name = "cKK";
            this.cKK.Size = new System.Drawing.Size(244, 24);
            this.cKK.TabIndex = 25;
            this.cKK.Visible = false;
            this.cKK.TextChanged += new System.EventHandler(this.cKK_TextChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(23, 135);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(131, 17);
            this.label11.TabIndex = 24;
            this.label11.Text = "Jabatan diKeluarga";
            // 
            // tKK
            // 
            this.tKK.Enabled = false;
            this.tKK.Location = new System.Drawing.Point(258, 167);
            this.tKK.Margin = new System.Windows.Forms.Padding(4);
            this.tKK.Name = "tKK";
            this.tKK.Size = new System.Drawing.Size(244, 22);
            this.tKK.TabIndex = 23;
            this.tKK.TextChanged += new System.EventHandler(this.tKK_TextChanged);
            this.tKK.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tKK_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(23, 167);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(48, 17);
            this.label10.TabIndex = 22;
            this.label10.Text = "No KK";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(23, 390);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 17);
            this.label7.TabIndex = 21;
            this.label7.Text = "Alamat";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(23, 511);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(72, 17);
            this.label8.TabIndex = 20;
            this.label8.Text = "Pekerjaan";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(23, 326);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(95, 17);
            this.label9.TabIndex = 19;
            this.label9.Text = "Jenis Kelamin";
            // 
            // cJabatan
            // 
            this.cJabatan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cJabatan.Enabled = false;
            this.cJabatan.FormattingEnabled = true;
            this.cJabatan.Items.AddRange(new object[] {
            "Kepala Keluarga",
            "Istri",
            "Anak",
            "Famili Lain"});
            this.cJabatan.Location = new System.Drawing.Point(258, 135);
            this.cJabatan.Margin = new System.Windows.Forms.Padding(4);
            this.cJabatan.Name = "cJabatan";
            this.cJabatan.Size = new System.Drawing.Size(244, 24);
            this.cJabatan.TabIndex = 17;
            this.cJabatan.TextChanged += new System.EventHandler(this.cJabatan_TextChanged);
            // 
            // tNIK
            // 
            this.tNIK.Enabled = false;
            this.tNIK.Location = new System.Drawing.Point(258, 73);
            this.tNIK.Margin = new System.Windows.Forms.Padding(4);
            this.tNIK.Name = "tNIK";
            this.tNIK.Size = new System.Drawing.Size(244, 22);
            this.tNIK.TabIndex = 15;
            this.tNIK.TextChanged += new System.EventHandler(this.label6_TextChanged);
            this.tNIK.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tNIK_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 104);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 17);
            this.label5.TabIndex = 14;
            this.label5.Text = "Nama Warga";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 265);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 17);
            this.label4.TabIndex = 13;
            this.label4.Text = "Tempat Lahir";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(23, 73);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 17);
            this.label6.TabIndex = 11;
            this.label6.Text = "NIK";
            this.label6.TextChanged += new System.EventHandler(this.label6_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Perpetua Titling MT", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(248, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(495, 72);
            this.label1.TabIndex = 5;
            this.label1.Text = "DATA WARGA";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Perpetua Titling MT", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(250, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Cari Data";
            // 
            // btnAdd
            // 
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.Location = new System.Drawing.Point(172, 792);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(55, 45);
            this.btnAdd.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnAdd.TabIndex = 7;
            this.btnAdd.TabStop = false;
            this.btnAdd.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEdit.Enabled = false;
            this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
            this.btnEdit.Location = new System.Drawing.Point(248, 792);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(55, 45);
            this.btnEdit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnEdit.TabIndex = 8;
            this.btnEdit.TabStop = false;
            this.btnEdit.Click += new System.EventHandler(this.pictureBox3_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.Enabled = false;
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.Location = new System.Drawing.Point(396, 792);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(55, 45);
            this.btnRefresh.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnRefresh.TabIndex = 9;
            this.btnRefresh.TabStop = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Perpetua Titling MT", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(860, 121);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(130, 17);
            this.label19.TabIndex = 10;
            this.label19.Text = "Jumlah Warga";
            // 
            // jmlWarga
            // 
            this.jmlWarga.AutoSize = true;
            this.jmlWarga.Font = new System.Drawing.Font("Perpetua Titling MT", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.jmlWarga.Location = new System.Drawing.Point(1021, 121);
            this.jmlWarga.Name = "jmlWarga";
            this.jmlWarga.Size = new System.Drawing.Size(18, 17);
            this.jmlWarga.TabIndex = 11;
            this.jmlWarga.Text = "0";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Perpetua Titling MT", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(997, 119);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(16, 17);
            this.label21.TabIndex = 12;
            this.label21.Text = "=";
            // 
            // btnSimpan
            // 
            this.btnSimpan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSimpan.Enabled = false;
            this.btnSimpan.Image = ((System.Drawing.Image)(resources.GetObject("btnSimpan.Image")));
            this.btnSimpan.Location = new System.Drawing.Point(172, 792);
            this.btnSimpan.Name = "btnSimpan";
            this.btnSimpan.Size = new System.Drawing.Size(55, 45);
            this.btnSimpan.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnSimpan.TabIndex = 13;
            this.btnSimpan.TabStop = false;
            this.btnSimpan.Visible = false;
            this.btnSimpan.Click += new System.EventHandler(this.btnSimpan_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelete.Enabled = false;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.Location = new System.Drawing.Point(322, 792);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(55, 45);
            this.btnDelete.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnDelete.TabIndex = 14;
            this.btnDelete.TabStop = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Perpetua Titling MT", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Red;
            this.label12.Location = new System.Drawing.Point(347, 118);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(82, 12);
            this.label12.TabIndex = 15;
            this.label12.Text = "*Nama Warga";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Enabled = false;
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(258, 293);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(244, 22);
            this.dateTimePicker1.TabIndex = 52;
            this.dateTimePicker1.EnabledChanged += new System.EventHandler(this.dateTimePicker1_EnabledChanged);
            // 
            // DataWarga
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Bisque;
            this.ClientSize = new System.Drawing.Size(1128, 849);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSimpan);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.jmlWarga);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.fWarga);
            this.Controls.Add(this.tCari);
            this.Controls.Add(this.topBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(109, 51);
            this.Name = "DataWarga";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.topBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnAdd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRefresh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSimpan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDelete)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel topBar;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TextBox tCari;
        private System.Windows.Forms.ComboBox fWarga;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tKK;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cJabatan;
        private System.Windows.Forms.TextBox tNIK;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox tStatusKawin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tAlamat;
        private System.Windows.Forms.ComboBox cJK;
        private System.Windows.Forms.TextBox tTempat;
        private System.Windows.Forms.ComboBox cKK;
        private System.Windows.Forms.TextBox tNama;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.PictureBox btnAdd;
        private System.Windows.Forms.PictureBox btnEdit;
        private System.Windows.Forms.PictureBox btnRefresh;
        private System.Windows.Forms.ComboBox cPekerjaan;
        private System.Windows.Forms.ComboBox cPendidikan;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox cAgama;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox cNoReg;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox tRT;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cRT;
        private System.Windows.Forms.ComboBox tStatusBPJS;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox tPLain;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label jmlWarga;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox Noreg;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.PictureBox btnSimpan;
        private System.Windows.Forms.PictureBox btnDelete;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
    }
}