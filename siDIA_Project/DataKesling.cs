using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace siDIA_Project
{
    public partial class DataKesling : Form
    {

        bool addstate = false;

        Koneksi kn = new Koneksi();
        public DataKesling()
        {
            InitializeComponent();
            dgv();
            clearForm();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgv()
        {
            SqlConnection koneksi = new SqlConnection();
            koneksi.ConnectionString = kn.strKoneksi();
            koneksi.Open();
            string str = "select w.nama as 'Nama Kepala Rumah Tangga', k.Jmlh_Total_Anggota_RT " +
                "as 'Jumal Anggota Keluarga',k.Jmlh_KK as 'Jumlah Kepala Keluarga' " +
                "from Kesling k join rumah r on k.id_rumah = k.id_rumah " +
                "join warga w on r.id_rumah = w.id_rumah where " +
                "w.status_dalam_rumah_tangga = 'Kepala Rumah Tangga' and r.id_rumah = k.id_rumah";
            SqlDataAdapter ad = new SqlDataAdapter(str, koneksi);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            koneksi.Close();
        }

        private void clearForm()
        {
            cNmRT.SelectedItem = null;
            tRmh.Text = "";
            tjART.Text = "";
            tJKK.Text = "";
            tBalita.Text = "";
            tPUS.Text = "";
            tWUS.Text = "";
            tButa.Text = "";
            tHamil.Text = "";
            tBuSui.Text = "";
            tLansia.Text = "";
            cJamban.SelectedItem = null;
            cAir.SelectedItem = null;
            tAir.Text = "";
            cSampah.SelectedItem = null;
            cLimbah.SelectedItem = null;
            cP4K.SelectedItem = null;
            cKRmh.SelectedItem = null;

            cNmRT.Enabled = false;
            tRmh.Enabled = false;
            tjART.Enabled = false;
            tJKK.Enabled = false;
            tBalita.Enabled = false;
            tPUS.Enabled = false;
            tWUS.Enabled = false;
            tButa.Enabled = false;
            tHamil.Enabled = false;
            tBuSui.Enabled = false;
            tLansia.Enabled = false;
            cJamban.Enabled = false;
            cAir.Enabled = false;
            tAir.Enabled = false;
            tAir.Visible = false;
            cSampah.Enabled = false;
            cLimbah.Enabled = false;
            cP4K.Enabled = false;
            cKRmh.Enabled = false;

            btnRefresh.Enabled = false;
            btnAdd.Enabled = true;
            btnAdd.Visible = true;
            dataGridView1.Enabled = true;
            btnSimpan.Enabled = false;
            btnSimpan.Visible = false;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            clearForm();
            addstate = false;
            textBoxCariNama.Visible = false;
            textBoxCariNama.Enabled = false;
            cNmRT.Visible = true;
            cNmRT.Enabled = false;
            dgv();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            addstate = true;
            SqlConnection koneksi = new SqlConnection();
            koneksi.ConnectionString = kn.strKoneksi();
            koneksi.Open();
            SqlDataAdapter da = new SqlDataAdapter("SELECT w.nama FROM rumah r " +
                "JOIN warga w ON r.id_rumah = w.id_rumah " +
                "LEFT JOIN kesling k ON r.id_rumah = k.id_rumah " +
                "WHERE w.status_dalam_rumah_tangga = 'Kepala Rumah Tangga' " +
                "AND k.id_rumah IS NULL", koneksi);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count != 0)
            {
                koneksi.Close();


                cNmRT.DisplayMember = "nama";
                cNmRT.ValueMember = "nama";
                cNmRT.DataSource = ds.Tables[0];

                cNmRT.Enabled = true;
                btnRefresh.Enabled = true;
                btnSimpan.Enabled = true;
                btnSimpan.Visible = true;
                btnAdd.Visible = false;
                btnAdd.Enabled = false;
            }
            else
            {
                MessageBox.Show("Semua Kepala Keluarga Telah Terdaftar di Kesling", "Pemberitahuan", MessageBoxButtons.OK);
            }
        }

        private void cNmRT_TextChanged(object sender, EventArgs e)
        {
            string kk = cNmRT.Text;
            string nRT = "";
            SqlConnection koneksi = new SqlConnection();
            koneksi.ConnectionString = kn.strKoneksi();
            koneksi.Open();
            SqlCommand cm = new SqlCommand("select r.id_rumah from rumah r " +
                "join warga w on r.id_rumah = w.id_rumah where w.nama = @nKRT", koneksi);
            cm.CommandType = CommandType.Text;
            cm.Parameters.Add(new SqlParameter("@nKRT", kk));
            SqlDataReader dr = cm.ExecuteReader();
            while (dr.Read())
            {
                nRT = dr["id_rumah"].ToString();

            }
            dr.Close();
            tRmh.Text = nRT;
        }

        private void tRmh_TextChanged(object sender, EventArgs e)
        {
            int hasil = 0;
            string noRmh = tRmh.Text;
            SqlConnection koneksi = new SqlConnection();
            koneksi.ConnectionString = kn.strKoneksi();
            koneksi.Open();
            SqlCommand cmd = new SqlCommand("select count(No_Reg) as totalWarga from warga " +
                "where id_rumah = '" + noRmh + "'", koneksi);
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                hasil = Convert.ToInt32(dr["totalWarga"]);
            }
            dr.Close();
            int jmlhKK = 0;
            SqlCommand cd = new SqlCommand("select count(No_Reg) as totalKK from warga " +
                "where id_rumah = '" + noRmh + "' and status_dalam_keluarga = 'Kepala Keluarga'", koneksi);
            cd.CommandType = CommandType.Text;
            SqlDataReader sdtr = cd.ExecuteReader();
            while (sdtr.Read())
            {
                jmlhKK = Convert.ToInt32(sdtr["totalKK"]);
            }
            sdtr.Close();
            int nRT = 0;
            string jk = "";
            int jB = 0;
            int jPUS = 0;
            int jWUS = 0;
            int jLansia = 0;
            SqlCommand cm = new SqlCommand("select CONVERT(int, YEAR(Getdate()),105) - " +
                "convert(int,year(tanggal_lahir),105) as Usia, jenis_kelamin " +
                "from warga where id_rumah = '" + noRmh + "'"
                , koneksi);
            cm.CommandType = CommandType.Text;
            SqlDataReader sdr = cm.ExecuteReader();
            while (sdr.Read())
            {
                nRT = Convert.ToInt32(sdr["Usia"]);
                jk = sdr["jenis_kelamin"].ToString();
                if ((nRT >= 0) && (nRT <= 4))
                {
                    jB += 1;
                }
                if ((nRT >= 15) && (nRT <= 49))
                {
                    jPUS += 1;
                    if ((nRT >= 20) && (nRT <= 45) && (jk.Equals("P")))
                    {
                        jWUS += 1;
                    }
                }
                if (nRT > 60)
                {
                    jLansia += 1;
                }
            }
            sdr.Close();
            koneksi.Close();
            tjART.Text = hasil.ToString();
            tJKK.Text = jmlhKK.ToString();
            tBalita.Text = jB.ToString();
            tPUS.Text = jPUS.ToString();
            tWUS.Text = jWUS.ToString();
            tLansia.Text = jLansia.ToString();
        }

        private void tWUS_TextChanged(object sender, EventArgs e)
        {
            tButa.Enabled = true;
        }

        private void tButa_TextChanged(object sender, EventArgs e)
        {
            tHamil.Enabled = true;
        }

        private void tHamil_TextChanged(object sender, EventArgs e)
        {
            tBuSui.Enabled = true;
        }

        private void tBuSui_TextChanged(object sender, EventArgs e)
        {
            cJamban.Enabled = true;
        }

        private void cJamban_TextChanged(object sender, EventArgs e)
        {
            cAir.Enabled = true;
        }

        private void cAir_TextChanged(object sender, EventArgs e)
        {
            if (cAir.Text.Equals("Lainnya"))
            {
                tAir.Enabled = true;
                tAir.Visible = true;
            }
            else
            {
                cSampah.Enabled = true;
            }
        }

        private void tAir_TextChanged(object sender, EventArgs e)
        {
            cSampah.Enabled = true;
        }

        private void cSampah_TextChanged(object sender, EventArgs e)
        {
            cLimbah.Enabled = true;
        }

        private void cLimbah_TextChanged(object sender, EventArgs e)
        {
            cP4K.Enabled = true;
        }

        private void cP4K_TextChanged(object sender, EventArgs e)
        {
            cKRmh.Enabled = true;
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            addstate = false;
            string nRmh = tRmh.Text;
            int jmlhA = int.Parse(tjART.Text);
            int jmlhK = int.Parse(tJKK.Text);
            int jBalita = int.Parse(tBalita.Text);
            int jPUS = int.Parse(tPUS.Text);
            int jWUS = int.Parse(tWUS.Text);
            int jButa = int.Parse(tButa.Text);
            int jHamil = int.Parse(tHamil.Text);
            int jBuSui = int.Parse(tBuSui.Text);
            int jLansia = int.Parse(tLansia.Text);
            string jamban = cJamban.Text;
            string air = cAir.Text;
            string Lair = tAir.Text;
            string sampah = cSampah.Text;
            string limbah = cLimbah.Text;
            string P4K = cP4K.Text;
            string kRmh = cKRmh.Text;


            DialogResult dg;
            dg = MessageBox.Show("Apakah data yang anda masukan sudah sesuai?", "Konfirmasi Tambah Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dg == DialogResult.Yes)
            {
                string str = "";
                SqlConnection koneksi = new SqlConnection();
                koneksi.ConnectionString = kn.strKoneksi();
                koneksi.Open();
                str = "insert into kesling (Jmlh_Total_Anggota_RT,Jmlh_KK,Balita,PUS,WUS,Buta,Ibu_Hamil," +
                    "Ibu_Menyusui,Lansia,Mempunyai_Jamban,Mempunyai_Sumber_Air,Mempunyai_Tmpt_Sampah," +
                    "Mempunyai_Saluran_Limbah,Stiker_P4K,KriteriaRumah,id_rumah) " +
                    "values (@jART,@jKK,@jB,@jP,@jW,@jBta,@jH,@jS,@jL,@jbn,@air,@smph,@lmbh,@p4k,@kR,@idR)";
                SqlCommand cmd = new SqlCommand(str, koneksi);
                cmd.CommandType = CommandType.Text;

                if (!air.Equals("Lainnya"))
                {
                    cmd.Parameters.Add(new SqlParameter("jART", jmlhA));
                    cmd.Parameters.Add(new SqlParameter("jKK", jmlhK));
                    cmd.Parameters.Add(new SqlParameter("jB", jBalita));
                    cmd.Parameters.Add(new SqlParameter("jBta", jButa));
                    cmd.Parameters.Add(new SqlParameter("jP", jPUS));
                    cmd.Parameters.Add(new SqlParameter("jW", jWUS));
                    cmd.Parameters.Add(new SqlParameter("jH", jHamil));
                    cmd.Parameters.Add(new SqlParameter("jS", jBuSui));
                    cmd.Parameters.Add(new SqlParameter("jL", jLansia));
                    cmd.Parameters.Add(new SqlParameter("jbn", jamban));
                    cmd.Parameters.Add(new SqlParameter("air", air));
                    cmd.Parameters.Add(new SqlParameter("smph", sampah));
                    cmd.Parameters.Add(new SqlParameter("lmbh", limbah));
                    cmd.Parameters.Add(new SqlParameter("p4k", P4K));
                    cmd.Parameters.Add(new SqlParameter("kR", kRmh));
                    cmd.Parameters.Add(new SqlParameter("idR", nRmh));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("jART", jmlhA));
                    cmd.Parameters.Add(new SqlParameter("jKK", jmlhK));
                    cmd.Parameters.Add(new SqlParameter("jB", jBalita));
                    cmd.Parameters.Add(new SqlParameter("jBta", jButa));
                    cmd.Parameters.Add(new SqlParameter("jP", jPUS));
                    cmd.Parameters.Add(new SqlParameter("jW", jWUS));
                    cmd.Parameters.Add(new SqlParameter("jH", jHamil));
                    cmd.Parameters.Add(new SqlParameter("jS", jBuSui));
                    cmd.Parameters.Add(new SqlParameter("jL", jLansia));
                    cmd.Parameters.Add(new SqlParameter("jbn", jamban));
                    cmd.Parameters.Add(new SqlParameter("air", Lair));
                    cmd.Parameters.Add(new SqlParameter("smph", sampah));
                    cmd.Parameters.Add(new SqlParameter("lmbh", limbah));
                    cmd.Parameters.Add(new SqlParameter("p4k", P4K));
                    cmd.Parameters.Add(new SqlParameter("kR", kRmh));
                    cmd.Parameters.Add(new SqlParameter("idR", nRmh));
                }
                cmd.ExecuteNonQuery();
                koneksi.Close();
                MessageBox.Show("Data Berhasil DiTambahkan", "Sukses", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                clearForm();
                dgv();

            }

        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Console.WriteLine("tes addstate = " + addstate);
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;
            SqlConnection koneksi = new SqlConnection();
            koneksi.ConnectionString = kn.strKoneksi();
            koneksi.Open();
            string nmKRT = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            string nRmh = "";
            int jmlhA = 0;
            int jmlhK = 0;
            int jBalita = 0;
            int jPUS = 0;
            int jWUS = 0;
            int jButa = 0;
            int jHamil = 0;
            int jBuSui = 0;
            int jLansia = 0;
            string jamban = "";
            string air = "";
            string sampah = "";
            string limbah = "";
            string P4K = "";
            string kRmh = "";

            if (addstate == true)
            {
                textBoxCariNama.Text = nmKRT;
                nRmh = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
                SqlDataAdapter da = new SqlDataAdapter("select id_rumah from rumah where id_rumah = '"+
                    nRmh + 
                    "'", koneksi);
                tRmh.Text = nRmh;
            }
            else
            {
                cNmRT.Items.Add(nmKRT); 
                SqlCommand cmd = new SqlCommand("select Jmlh_Total_Anggota_RT,Jmlh_KK,Balita,PUS,WUS,Buta,Ibu_Hamil," +
                        "Ibu_Menyusui,Lansia,Mempunyai_Jamban,Mempunyai_Sumber_Air,Mempunyai_Tmpt_Sampah," +
                        "Mempunyai_Saluran_Limbah,Stiker_P4K,KriteriaRumah,id_rumah " +
                        "from kesling where id_rumah = (select id_rumah from warga where nama = @nKRT)", koneksi);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("@nKRT", nmKRT));
                SqlDataReader dr = cmd.ExecuteReader();
                DataSet ds = new DataSet();
                while (dr.Read())
                {
                    jmlhA = int.Parse(dr["Jmlh_Total_Anggota_RT"].ToString());
                    jmlhK = int.Parse(dr["Jmlh_KK"].ToString());
                    jBalita = int.Parse(dr["Balita"].ToString());
                    jPUS = int.Parse(dr["PUS"].ToString());
                    jWUS = int.Parse(dr["WUS"].ToString());
                    jButa = int.Parse(dr["Buta"].ToString());
                    jHamil = int.Parse(dr["Ibu_Hamil"].ToString());
                    jBuSui = int.Parse(dr["Ibu_Menyusui"].ToString());
                    jLansia = int.Parse(dr["Lansia"].ToString());
                    jamban = dr["Mempunyai_Jamban"].ToString();
                    air = dr["Mempunyai_Sumber_Air"].ToString();
                    sampah = dr["Mempunyai_Tmpt_Sampah"].ToString();
                    limbah = dr["Mempunyai_Saluran_Limbah"].ToString();
                    P4K = dr["Stiker_P4K"].ToString();
                    kRmh = dr["KriteriaRumah"].ToString();
                    nRmh = dr["id_rumah"].ToString();

                }
                dr.Close();


                if (air.Equals("PDAM") || air.Equals("Sumur") ||
                        air.Equals("Sungai"))
                {
                    cNmRT.Text = nmKRT;
                    tRmh.Text = nRmh;
                    tjART.Text = jmlhA.ToString();
                    tJKK.Text = jmlhK.ToString();
                    tBalita.Text = jBalita.ToString();
                    tPUS.Text = jPUS.ToString();
                    tWUS.Text = jWUS.ToString();
                    tButa.Text = jButa.ToString();
                    tHamil.Text = jHamil.ToString();
                    tBuSui.Text = jBuSui.ToString();
                    tLansia.Text = jLansia.ToString();
                    cJamban.Text = jamban;
                    cAir.Text = air;
                    cSampah.Text = sampah;
                    cLimbah.Text = limbah;
                    cP4K.Text = P4K;
                    cKRmh.Text = kRmh;
                }
                else
                {
                    cNmRT.Text = nmKRT;
                    tRmh.Text = nRmh;
                    tjART.Text = jmlhA.ToString();
                    tJKK.Text = jmlhK.ToString();
                    tBalita.Text = jBalita.ToString();
                    tPUS.Text = jPUS.ToString();
                    tWUS.Text = jWUS.ToString();
                    tButa.Text = jButa.ToString();
                    tHamil.Text = jHamil.ToString();
                    tBuSui.Text = jBuSui.ToString();
                    tLansia.Text = jLansia.ToString();
                    cJamban.Text = jamban;
                    cAir.Text = "Lainnya";
                    tAir.Text = air;
                    cSampah.Text = sampah;
                    cLimbah.Text = limbah;
                    cP4K.Text = P4K;
                    cKRmh.Text = kRmh;
                }
                koneksi.Close();
                btnAdd.Enabled = false;
                btnRefresh.Enabled = true;
                btnEdit.Enabled = true;
                btnHapus.Enabled = true;
                dataGridView1.Enabled = false;
            }
            
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string nRmh = tRmh.Text;
            int jmlhA = int.Parse(tjART.Text);
            int jmlhK = int.Parse(tJKK.Text);
            int jBalita = int.Parse(tBalita.Text);
            int jPUS = int.Parse(tPUS.Text);
            int jWUS = int.Parse(tWUS.Text);
            int jButa = int.Parse(tButa.Text);
            int jHamil = int.Parse(tHamil.Text);
            int jBuSui = int.Parse(tBuSui.Text);
            int jLansia = int.Parse(tLansia.Text);
            string jamban = cJamban.Text;
            string air = cAir.Text;
            string Lair = tAir.Text;
            string sampah = cSampah.Text;
            string limbah = cLimbah.Text;
            string P4K = cP4K.Text;
            string kRmh = cKRmh.Text;

            DialogResult dg;
            dg = MessageBox.Show("Apakah anda ingin mengubah data ini?", "Konfirmasi Ubah Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dg == DialogResult.Yes)
            {
                string str = "";

                SqlConnection koneksi = new SqlConnection();
                koneksi.ConnectionString = kn.strKoneksi();
                koneksi.Open();
                str = "update kesling set Jmlh_Total_Anggota_RT = @jART,Jmlh_KK = @jKK,Balita = @jB," +
                    "PUS = @jP,WUS = @jW,Buta = @jBta,Ibu_Hamil = @jH,Ibu_Menyusui = @jS,Lansia = @jL," +
                    "Mempunyai_Jamban = @jbn,Mempunyai_Sumber_Air = @air,Mempunyai_Tmpt_Sampah = @smph," +
                    "Mempunyai_Saluran_Limbah = @lmbh,Stiker_P4K = @p4k,KriteriaRumah = @kR " +
                    "where id_rumah = @idR";
                SqlCommand cmd = new SqlCommand(str, koneksi);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("@idR", nRmh));

                if (!air.Equals("Lainnya"))
                {
                    cmd.Parameters.Add(new SqlParameter("jART", jmlhA));
                    cmd.Parameters.Add(new SqlParameter("jKK", jmlhK));
                    cmd.Parameters.Add(new SqlParameter("jB", jBalita));
                    cmd.Parameters.Add(new SqlParameter("jP", jPUS));
                    cmd.Parameters.Add(new SqlParameter("jW", jWUS));
                    cmd.Parameters.Add(new SqlParameter("jBta", jButa));
                    cmd.Parameters.Add(new SqlParameter("jH", jHamil));
                    cmd.Parameters.Add(new SqlParameter("jS", jBuSui));
                    cmd.Parameters.Add(new SqlParameter("jL", jLansia));
                    cmd.Parameters.Add(new SqlParameter("jbn", jamban));
                    cmd.Parameters.Add(new SqlParameter("air", air));
                    cmd.Parameters.Add(new SqlParameter("smph", sampah));
                    cmd.Parameters.Add(new SqlParameter("lmbh", limbah));
                    cmd.Parameters.Add(new SqlParameter("p4k", P4K));
                    cmd.Parameters.Add(new SqlParameter("kR", kRmh));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("jART", jmlhA));
                    cmd.Parameters.Add(new SqlParameter("jKK", jmlhK));
                    cmd.Parameters.Add(new SqlParameter("jB", jBalita));
                    cmd.Parameters.Add(new SqlParameter("jP", jPUS));
                    cmd.Parameters.Add(new SqlParameter("jW", jWUS));
                    cmd.Parameters.Add(new SqlParameter("jBta", jButa));
                    cmd.Parameters.Add(new SqlParameter("jH", jHamil));
                    cmd.Parameters.Add(new SqlParameter("jS", jBuSui));
                    cmd.Parameters.Add(new SqlParameter("jL", jLansia));
                    cmd.Parameters.Add(new SqlParameter("jbn", jamban));
                    cmd.Parameters.Add(new SqlParameter("air", Lair));
                    cmd.Parameters.Add(new SqlParameter("smph", sampah));
                    cmd.Parameters.Add(new SqlParameter("lmbh", limbah));
                    cmd.Parameters.Add(new SqlParameter("p4k", P4K));
                    cmd.Parameters.Add(new SqlParameter("kR", kRmh));
                }
                cmd.ExecuteNonQuery();
                koneksi.Close();
                MessageBox.Show("Data Berhasil Diperbaruhi", "Sukses", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                clearForm();
                dgv();
            }

        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            string str = "";
            SqlConnection koneksi = new SqlConnection();
            koneksi.ConnectionString = kn.strKoneksi();
            koneksi.Open();

            string noRmh = tRmh.Text;
            DialogResult dg;
            dg = MessageBox.Show("Apakah anda ingin menghapus data ini?", "Konfirmasi Ubah Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dg == DialogResult.Yes)
            {
                str = "delete from kesling where id_Rumah = @no_rmh";
                SqlCommand cmd = new SqlCommand(str, koneksi);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("no_rmh", noRmh));
                cmd.ExecuteNonQuery();
                koneksi.Close();
                MessageBox.Show("Data Berhasil Dihapus", "Sukses", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                clearForm();
                dgv();
            }
        }

        private void tCari_TextChanged(object sender, EventArgs e)
        {
            if (!tCari.Text.Equals(""))
            {
                SqlConnection koneksi = new SqlConnection();
                koneksi.ConnectionString = kn.strKoneksi();
                koneksi.Open();
                string str = "select w.nama as 'Nama Kepala Rumah Tangga', k.Jmlh_Total_Anggota_RT " +
                "as 'Jumal Anggota Keluarga',k.Jmlh_KK as 'Jumlah Kepala Keluarga' " +
                "from Kesling k join rumah r on k.id_rumah = k.id_rumah " +
                "join warga w on r.id_rumah = w.id_rumah where " +
                "w.status_dalam_rumah_tangga = 'Kepala Rumah Tangga' and r.id_rumah = k.id_rumah" +
                " and Nama Like '%" + tCari.Text + "%'";
                SqlDataAdapter ad = new SqlDataAdapter(str, koneksi);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    //col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                koneksi.Close();
            }
            else
            {
                dgv();
            }
        }

        private void searchicon_Click(object sender, EventArgs e)
        {
            cNmRT.Enabled = false;
            cNmRT.Visible = false;

            textBoxCariNama.Visible = true;
            textBoxCariNama.Enabled = true;

            string nRmh = "";
            int jmlhA = 0;
            int jmlhK = 0;
            int jBalita = 0;
            int jPUS = 0;
            int jWUS = 0;
            int jButa = 0;
            int jHamil = 0;
            int jBuSui = 0;
            int jLansia = 0;
            string jamban = "";
            string air = "";
            string sampah = "";
            string limbah = "";
            string P4K = "";
            string kRmh = "";

            cNmRT.Text = "";
            tRmh.Text = nRmh;
            tjART.Text = jmlhA.ToString();
            tJKK.Text = jmlhK.ToString();
            tBalita.Text = jBalita.ToString();
            tPUS.Text = jPUS.ToString();
            tWUS.Text = jWUS.ToString();
            tButa.Text = jButa.ToString();
            tHamil.Text = jHamil.ToString();
            tBuSui.Text = jBuSui.ToString();
            tLansia.Text = jLansia.ToString();
            cJamban.Text = jamban;
            cAir.Text = air;
            cSampah.Text = sampah;
            cLimbah.Text = limbah;
            cP4K.Text = P4K;
            cKRmh.Text = kRmh;

            SqlConnection koneksi = new SqlConnection();
            koneksi.ConnectionString = kn.strKoneksi();
            koneksi.Open();
            SqlDataAdapter da = new SqlDataAdapter("SELECT w.nama, w.id_rumah FROM rumah r " +
                   "JOIN warga w ON r.id_rumah = w.id_rumah " +
                   "LEFT JOIN kesling k ON r.id_rumah = k.id_rumah " +
                   "WHERE w.status_dalam_rumah_tangga = 'Kepala Rumah Tangga' " +
                   "AND k.id_rumah IS NULL", koneksi);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void textBoxCariNama_TextChanged(object sender, EventArgs e)
        {
            SqlConnection koneksi = new SqlConnection();
            koneksi.ConnectionString = kn.strKoneksi();
            koneksi.Open();

            if (textBoxCariNama.Text == "")
            {

                SqlDataAdapter da = new SqlDataAdapter("SELECT w.nama, w.id_rumah FROM rumah r " +
                    "JOIN warga w ON r.id_rumah = w.id_rumah " +
                    "LEFT JOIN kesling k ON r.id_rumah = k.id_rumah " +
                    "WHERE w.status_dalam_rumah_tangga = 'Kepala Rumah Tangga' " +
                    "AND k.id_rumah IS NULL", koneksi);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
            }
            else
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT w.nama, w.id_rumah FROM rumah r " +
                    "JOIN warga w ON r.id_rumah = w.id_rumah " +
                    "LEFT JOIN kesling k ON r.id_rumah = k.id_rumah " +
                    "WHERE w.status_dalam_rumah_tangga = 'Kepala Rumah Tangga' " +
                    "AND k.id_rumah IS NULL and w.nama like '"
                    + textBoxCariNama.Text + "%'", koneksi);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
            }
        }
    }
}
