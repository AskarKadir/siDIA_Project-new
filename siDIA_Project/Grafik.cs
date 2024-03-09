using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace siDIA_Project
{
    public partial class Grafik : Form
    {
        Koneksi kn = new Koneksi();
        public Grafik()
        {
            InitializeComponent();

            chart1.Hide();
            listView1.Hide();
            comboBox1.Hide();
            label3.Hide();

            comboBox1.Items.Add("Pie");
            comboBox1.Items.Add("Line");
            comboBox1.Items.Add("Bar");
            comboBox1.Items.Add("Column");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void displayUsia()
        {
            double hs;
            SqlConnection koneksi = new SqlConnection();
            koneksi.ConnectionString = kn.strKoneksi();
            koneksi.Open();
            List<string> hasil_arr = new List<string>();
            List<double> hasil_arrs = new List<double>();

            hasil_arr.Add("Balita");
            hasil_arr.Add("Anak");
            hasil_arr.Add("PUS");
            hasil_arr.Add("WUS");
            hasil_arr.Add("Lansia");
            hasil_arr.Add("Pra-Lansia");


            for (int i = 0; i < (hasil_arr.Count); i++)
            {
                string js = hasil_arr[i];
                string strs = "";

                if (js.Equals("Balita"))
                {
                    strs = "select count(No_Reg) as totalWarga from warga " +
                    "where 0 <= CONVERT(int, YEAR(Getdate()), 105) - convert(int, year(tanggal_lahir), 105) " +
                    "and CONVERT(int, YEAR(Getdate()),105) -convert(int, year(tanggal_lahir), 105) <= 4";
                }
                else if (js.Equals("Anak"))
                {
                    strs = "select count(No_Reg) as totalWarga from warga " +
                    "where 4 < CONVERT(int, YEAR(Getdate()), 105) - convert(int, year(tanggal_lahir), 105) " +
                    "and CONVERT(int, YEAR(Getdate()),105) -convert(int, year(tanggal_lahir), 105) < 15";
                }
                else if (js.Equals("PUS"))
                {
                    strs = "select count(No_Reg) as totalWarga from warga " +
                    "where 15 <= CONVERT(int, YEAR(Getdate()), 105) - convert(int, year(tanggal_lahir), 105) " +
                    "and CONVERT(int, YEAR(Getdate()),105) -convert(int, year(tanggal_lahir), 105) <= 49";
                }
                else if (js.Equals("WUS"))
                {
                    strs = "select count(No_Reg) as totalWarga from warga " +
                    "where 20 <= CONVERT(int, YEAR(Getdate()), 105) - convert(int, year(tanggal_lahir), 105) " +
                    "and CONVERT(int, YEAR(Getdate()),105) -convert(int, year(tanggal_lahir), 105) <= 45 " +
                    "and jenis_kelamin = 'P'";
                }
                else if (js.Equals("Pra-Lansia"))
                {
                    strs = "select count(No_Reg) as totalWarga from warga " +
                    "where 45 <= CONVERT(int, YEAR(Getdate()), 105) - convert(int, year(tanggal_lahir), 105) " +
                    "and CONVERT(int, YEAR(Getdate()),105) -convert(int, year(tanggal_lahir), 105) <= 59";
                }
                else
                {
                    strs = "select count(No_Reg) as totalWarga from warga " +
                    "where 60 < CONVERT(int, YEAR(Getdate()), 105) - convert(int, year(tanggal_lahir), 105)";
                }
                SqlCommand cm = new SqlCommand(strs, koneksi);
                object res = cm.ExecuteScalar();
                hs = Convert.ToDouble(res);
                hasil_arrs.Add(hs);
            }

            //Get the names of Cities.
            string[] x = hasil_arr.ToArray();
            double[] y = new double[hasil_arrs.Count];
            for (int i = 0; i < (hasil_arr.Count); i++)
            {
                string js = hasil_arr[i];
                string strq = "";
                if (js.Equals("Balita"))
                {
                    strq = "select count(No_Reg) as totalWarga from warga " +
                    "where 0 <= CONVERT(int, YEAR(Getdate()), 105) - convert(int, year(tanggal_lahir), 105) " +
                    "and CONVERT(int, YEAR(Getdate()),105) -convert(int, year(tanggal_lahir), 105) <= 4";
                }
                else if (js.Equals("Anak"))
                {
                    strq = "select count(No_Reg) as totalWarga from warga " +
                    "where 4 < CONVERT(int, YEAR(Getdate()), 105) - convert(int, year(tanggal_lahir), 105) " +
                    "and CONVERT(int, YEAR(Getdate()),105) -convert(int, year(tanggal_lahir), 105) < 15";
                }
                else if (js.Equals("PUS"))
                {
                    strq = "select count(No_Reg) as totalWarga from warga " +
                    "where 15 <= CONVERT(int, YEAR(Getdate()), 105) - convert(int, year(tanggal_lahir), 105) " +
                    "and CONVERT(int, YEAR(Getdate()),105) -convert(int, year(tanggal_lahir), 105) <= 49";
                }
                else if (js.Equals("WUS"))
                {
                    strq = "select count(No_Reg) as totalWarga from warga " +
                    "where 20 <= CONVERT(int, YEAR(Getdate()), 105) - convert(int, year(tanggal_lahir), 105) " +
                    "and CONVERT(int, YEAR(Getdate()),105) -convert(int, year(tanggal_lahir), 105) <= 45 " +
                    "and jenis_kelamin = 'P'";
                }
                else if (js.Equals("Pra-Lansia"))
                {
                    strq = "select count(No_Reg) as totalWarga from warga " +
                    "where 45 <= CONVERT(int, YEAR(Getdate()), 105) - convert(int, year(tanggal_lahir), 105) " +
                    "and CONVERT(int, YEAR(Getdate()),105) -convert(int, year(tanggal_lahir), 105) <= 59";
                }
                else
                {
                    strq = "select count(No_Reg) as totalWarga from warga " +
                    "where 60 < CONVERT(int, YEAR(Getdate()), 105) - convert(int, year(tanggal_lahir), 105)";
                }
                SqlCommand cm = new SqlCommand(strq, koneksi);
                object res = cm.ExecuteScalar();
                hs = Convert.ToDouble(res);
                hasil_arrs.Add(hs);
                //Get the Total of Orders for each City.
                y[i] = hasil_arrs[i];
            }

            //testc git

            koneksi.Close();
            ListViewItem temp;
            var n = 0;
            foreach (var i in hasil_arr)
            {
                temp = new ListViewItem(new string[] { i, hasil_arrs[n++].ToString()});
                listView1.Items.Add(temp);
            }


            if (listView1.Items.Count != 0)
            {
                chart1.Show();
                label3.Show();
                comboBox1.Show();
                listView1.Show();
                chart1.Series[0].ChartType = SeriesChartType.Pie;
                chart1.Series[0].Points.DataBindXY(x, y);
                chart1.Legends[0].Enabled = true;
            }
            else
            {
                chart1.Hide();
                listView1.Hide();
                comboBox1.Hide();
                label3.Hide();
                MessageBox.Show("Data Tidak Ada", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (fWarga.Text.Equals("Usia"))
            {
                try
                {
                    if (listView1.Items.Count == 0)
                    {
                        displayUsia();
                    }
                    else
                    {
                        for (int i = 0; i < listView1.Items.Count; i++)
                        {
                            if (listView1.Items[i].Selected)
                            {
                                listView1.Items[i].Remove();
                                i--;
                            }
                        }
                        listView1.Items.Clear();
                        displayUsia();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            chart1.Series[0].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), comboBox1.SelectedItem.ToString());
        }
    }
}
