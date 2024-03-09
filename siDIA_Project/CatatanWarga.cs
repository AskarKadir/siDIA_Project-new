using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace siDIA_Project
{
    public partial class CatatanWarga : Form
    {
        Koneksi kn = new Koneksi();
        QReport qr = new QReport();
        QReportKesling qrk = new QReportKesling();

        ReportCatatanWarga cr = new ReportCatatanWarga();
        public CatatanWarga()
        {
            InitializeComponent();
            NamaKRT();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                //MessageBox.Show("Dalam Proses Perbaikan", "Pemberitahuan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                string noRmh = "";
                string hasil = "";
                //string tahun = DateTime.Now.Year.ToString();
                //Console.WriteLine(tahun);
                string nmKRT = NKRT.Text;
                //Console.WriteLine(nmKRT);
                SqlConnection koneksi = new SqlConnection();
                koneksi.ConnectionString = kn.strKoneksi();
                koneksi.Open();
                SqlCommand cmd = new SqlCommand("Select id_rumah from warga where nama = @nama", koneksi);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("@nama", nmKRT));
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    hasil = dr["id_rumah"].ToString();
                }
                dr.Close();
                noRmh = hasil;
                DataTable ds = new DataTable();
                DataTable dt = new DataTable();
                SqlDataAdapter kesling = new SqlDataAdapter(qrk.strDataKesling(noRmh,nmKRT), koneksi);
                SqlDataAdapter ad = new SqlDataAdapter(qr.strData(noRmh), koneksi);
                kesling.Fill(ds);
                ad.Fill(ds);
                //Console.WriteLine(qr.strData(noRmh));
                
                koneksi.Close();
                cr.SetDataSource(ds);
                crystalReportViewer1.ReportSource = cr;
                crystalReportViewer1.Refresh();
                crystalReportViewer1.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void NamaKRT()
        {
            SqlConnection koneksi = new SqlConnection();
            koneksi.ConnectionString = kn.strKoneksi();
            koneksi.Open();
            SqlCommand cmd = new SqlCommand("select nama from warga where status_dalam_rumah_tangga like 'Kepala Rumah Tangga'", koneksi);
            SqlDataAdapter da = new SqlDataAdapter("select nama from warga where status_dalam_rumah_tangga like 'Kepala Rumah Tangga'", koneksi);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cmd.ExecuteReader();
            koneksi.Close();

            NKRT.DisplayMember = "nama";
            NKRT.ValueMember = "no_reg";
            NKRT.DataSource = ds.Tables[0];
        }
    }
}
