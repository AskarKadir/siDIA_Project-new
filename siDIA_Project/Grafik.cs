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
using System.Web.UI.WebControls;
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

            double total = 0;

            foreach (var js in hasil_arr)
            {
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
                total += hs;
                hasil_arrs.Add(hs);
            }

            // Filter out categories with null data
            List<string> categoriesWithData = new List<string>();
            List<double> countsWithData = new List<double>();
            for (int i = 0; i < hasil_arr.Count; i++)
            {
                if (hasil_arrs[i] > 0)
                {
                    categoriesWithData.Add(hasil_arr[i]);
                    countsWithData.Add(hasil_arrs[i]);
                }
            }

            // Calculate percentages
            List<double> percentages = countsWithData.Select(count => (count / total) * 100).ToList();

            koneksi.Close();
            ListViewItem temp;
            for (int i = 0; i < categoriesWithData.Count; i++)
            {
                temp = new ListViewItem(new string[] { categoriesWithData[i], countsWithData[i].ToString(), percentages[i].ToString("0.00") + "%" });
                listView1.Items.Add(temp);
            }

            if (listView1.Items.Count != 0)
            {
                chart1.Show(); ;
                listView1.Show();
                chart1.Series[0].ChartType = SeriesChartType.Pie;
                chart1.Series[0].Points.DataBindXY(categoriesWithData, percentages);
                chart1.Legends[0].Enabled = true;

                // Add percentage labels to the pie chart
                chart1.Series[0].IsValueShownAsLabel = true;
                chart1.Series[0]["PieLabelStyle"] = "Percent"; // Show percentage labels

                // Remove percentages from legend labels
                foreach (var point in chart1.Series[0].Points)
                {
                    point.LegendText = point.AxisLabel;
                }

                for (int i = 0; i < chart1.Series[0].Points.Count; i++)
                {
                    chart1.Series[0].Points[i].Label = $"{percentages[i]:0.00}%";
                }
            }
            else
            {
                chart1.Hide();
                listView1.Hide();
                MessageBox.Show("Data Tidak Ada", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void displayPekerjaan()
        {
            double hs;
            SqlConnection koneksi = new SqlConnection();
            koneksi.ConnectionString = kn.strKoneksi();
            koneksi.Open();
            List<string> hasil_arr = new List<string>();
            List<double> hasil_arrs = new List<double>();

            // Add known occupation categories
            hasil_arr.Add("Pelajar/Mahasiswa");
            hasil_arr.Add("IRT");
            hasil_arr.Add("PNS");
            hasil_arr.Add("Tidak Bekerja");
            hasil_arr.Add("Buruh/Karyawan");

            // Query database for other occupation categories dynamically
            string query = "SELECT DISTINCT pekerjaan FROM warga WHERE pekerjaan NOT IN ('Pelajar/Mahasiswa', 'IRT', 'PNS', 'Tidak Bekerja', 'Buruh/Karyawan')";
            SqlCommand cmd = new SqlCommand(query, koneksi);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string occupation = reader["pekerjaan"].ToString();
                    hasil_arr.Add(occupation);
                }
            }

            double total = 0;

            foreach (var js in hasil_arr)
            {
                string strs = $"select count(No_Reg) as totalWarga from warga where pekerjaan = '{js}'";
                SqlCommand cm = new SqlCommand(strs, koneksi);
                object res = cm.ExecuteScalar();
                hs = Convert.ToDouble(res);
                total += hs;
                hasil_arrs.Add(hs);
            }

            // Filter out categories with null data
            List<string> categoriesWithData = new List<string>();
            List<double> countsWithData = new List<double>();
            for (int i = 0; i < hasil_arr.Count; i++)
            {
                if (hasil_arrs[i] > 0)
                {
                    categoriesWithData.Add(hasil_arr[i]);
                    countsWithData.Add(hasil_arrs[i]);
                }
            }

            // Calculate percentages
            List<double> percentages = countsWithData.Select(count => (count / total) * 100).ToList();

            koneksi.Close();
            ListViewItem temp;
            for (int i = 0; i < categoriesWithData.Count; i++)
            {
                temp = new ListViewItem(new string[] { categoriesWithData[i], countsWithData[i].ToString(), percentages[i].ToString("0.00") + "%" });
                listView1.Items.Add(temp);
            }

            if (listView1.Items.Count != 0)
            {
                chart1.Show(); ;
                listView1.Show();
                chart1.Series[0].ChartType = SeriesChartType.Pie;
                chart1.Series[0].Points.DataBindXY(categoriesWithData, percentages);
                chart1.Legends[0].Enabled = true;

                // Add percentage labels to the pie chart
                chart1.Series[0].IsValueShownAsLabel = true;
                chart1.Series[0]["PieLabelStyle"] = "Percent"; // Show percentage labels

                // Remove percentages from legend labels
                foreach (var point in chart1.Series[0].Points)
                {
                    point.LegendText = point.AxisLabel;
                }

                for (int i = 0; i < chart1.Series[0].Points.Count; i++)
                {
                    chart1.Series[0].Points[i].Label = $"{percentages[i]:0.00}%";
                }
            }
            else
            {
                chart1.Hide();
                listView1.Hide();
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
            else if (fWarga.Text.Equals("Pekerjaan"))
            {
                try
                {
                    if (listView1.Items.Count == 0)
                    {
                        displayPekerjaan();
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
                        displayPekerjaan();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                //MessageBox.Show("Dalam Pengerjaan", listView1.Items.Count.ToString(), MessageBoxButtons.OK);
                
            }
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            //chart1.Series[0].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), comboBox1.SelectedItem.ToString());
        }
    }
}
