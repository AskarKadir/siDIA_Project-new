namespace siDIA_Project
{
    partial class CatatanWarga
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CatatanWarga));
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.topBar = new System.Windows.Forms.FlowLayoutPanel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.NKRT = new System.Windows.Forms.ComboBox();
            this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.ReportCatatanWarga1 = new siDIA_Project.ReportCatatanWarga();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.topBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Perpetua Titling MT", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(213, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(245, 17);
            this.label2.TabIndex = 16;
            this.label2.Text = "Nama Kepala Rumah Tangga";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Perpetua Titling MT", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(193, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(628, 72);
            this.label1.TabIndex = 15;
            this.label1.Text = "catatan WARGA";
            // 
            // topBar
            // 
            this.topBar.BackColor = System.Drawing.Color.Linen;
            this.topBar.Controls.Add(this.pictureBox2);
            this.topBar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.topBar.Dock = System.Windows.Forms.DockStyle.Right;
            this.topBar.Location = new System.Drawing.Point(990, 0);
            this.topBar.Name = "topBar";
            this.topBar.Size = new System.Drawing.Size(52, 753);
            this.topBar.TabIndex = 10;
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
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // NKRT
            // 
            this.NKRT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.NKRT.FormattingEnabled = true;
            this.NKRT.Location = new System.Drawing.Point(474, 94);
            this.NKRT.Name = "NKRT";
            this.NKRT.Size = new System.Drawing.Size(240, 24);
            this.NKRT.TabIndex = 12;
            // 
            // crystalReportViewer1
            // 
            this.crystalReportViewer1.ActiveViewIndex = 0;
            this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer1.Location = new System.Drawing.Point(12, 143);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.ReportSource = this.ReportCatatanWarga1;
            this.crystalReportViewer1.Size = new System.Drawing.Size(972, 598);
            this.crystalReportViewer1.TabIndex = 17;
            this.crystalReportViewer1.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(732, 86);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 18;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // CatatanWarga
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Bisque;
            this.ClientSize = new System.Drawing.Size(1042, 753);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.crystalReportViewer1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.topBar);
            this.Controls.Add(this.NKRT);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CatatanWarga";
            this.Text = "CatatanWarga";
            this.topBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel topBar;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.ComboBox NKRT;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
        private ReportCatatanWarga ReportCatatanWarga1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}