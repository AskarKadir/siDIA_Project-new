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
    public partial class splashscreen : Form
    {
        public splashscreen()
        {
            InitializeComponent();
            this.TransparencyKey = Color.Turquoise;
            this.BackColor = Color.Turquoise;
        }
        private int timeLeft { get; set; }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timeLeft == 0)
            {
                timer1.Stop();
                Login login = new Login();
                login.Show();
                this.Hide();

            }
            else
            {
                timeLeft = timeLeft - 1;
            }
        }

        private void splashscreen_Load(object sender, EventArgs e)
        {
            Koneksi kn = new Koneksi();
            SqlConnection koneksi = new SqlConnection();
            koneksi.ConnectionString = kn.strKoneksi();

            try
            {
                koneksi.Open();
                if (koneksi.State == ConnectionState.Open)
                {
                    koneksi.Close();
                }
                timeLeft = 20;
                timer1.Start();
                
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Koneksi Ke Database Gagal");
                //ex.ToString();
                MessageBox.Show(ex.Message.ToString());

                this.Close();
            }
        }
    }
}
