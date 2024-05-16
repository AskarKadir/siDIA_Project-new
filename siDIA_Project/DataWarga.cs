using CrystalDecisions.CrystalReports.Engine;
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
    public partial class DataWarga : Form
    {
        bool addstate = false;
        bool editstate = false;
        Koneksi kn = new Koneksi();
        public DataWarga()
        {
            InitializeComponent();
            dgv();
            jmlWrg();
            defaultstatebutton();
            //this.WindowState = FormWindowState.Minimized;
            //this.FormBorderStyle = FormBorderStyle.FixedDialog;
        }

        private void enableallform()
        {
            //enableallform exept noreg
            tKK.Enabled = true;
            cRT.Enabled = true;
            cKK.Enabled = true;
            cNoReg.Enabled = true;
            tNIK.Enabled = true;
            tNama.Enabled = true;
            cJabatan.Enabled = true;
            tTempat.Enabled = true;
            dateTimePicker1.Enabled = true;
            cJK.Enabled = true;
            cAgama.Enabled = true;
            tAlamat.Enabled = true;
            cPendidikan.Enabled = true;
            cPekerjaan.Enabled = true;
            tStatusKawin.Enabled = true;
            tStatusBPJS.Enabled = true;
        }

        private void disableallform()
        {
            //enableallform exept noreg
            tKK.Enabled = false;
            tRT.Enabled = false;
            cKK.Enabled = false;
            cNoReg.Enabled = false;
            tNIK.Enabled = false;
            tNama.Enabled = false;
            cJabatan.Enabled = false;
            cJabatan.Enabled = false;
            tTempat.Enabled = false;
            dateTimePicker1.Enabled = false;
            cJK.Enabled = false;
            cAgama.Enabled = false;
            tAlamat.Enabled = false;
            cPendidikan.Enabled = false;
            cPekerjaan.Enabled = false;
            tStatusKawin.Enabled = false;
            tStatusBPJS.Enabled = false;
        }

        private void defaultstatebutton()
        {
            tNIK.Enabled = false;
            tNama.Enabled = false;
            cJabatan.Enabled = false;
            tKK.Enabled = false;
            cJabatan.Enabled = false;
            tRT.Enabled = false;
            cNoReg.Enabled = false;
            tTempat.Enabled = false;
            dateTimePicker1.Enabled = false;
            cJK.Enabled = false;
            cAgama.Enabled = false;
            tAlamat.Enabled = false;
            cPendidikan.Enabled = false;
            cPekerjaan.Enabled = false;
            tStatusKawin.Enabled = false;
            tStatusBPJS.Enabled = false;

            btnAdd.Enabled =true;
            btnSimpan.Visible = false;
            btnSimpan.Enabled = false;
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            btnRefresh.Enabled = false;
        }

        private void dgv()
        {
            SqlConnection koneksi = new SqlConnection();
            koneksi.ConnectionString = kn.strKoneksi();
            koneksi.Open();
            string str = "select No_KK as 'No Kartu Keluarga', Nama as 'Nama Warga' from warga";
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

        private void jmlWrg()
        {
            string hasil = "";
            SqlConnection koneksi = new SqlConnection();
            koneksi.ConnectionString = kn.strKoneksi();
            koneksi.Open();
            SqlCommand cmd = new SqlCommand("select count(No_Reg) as totalWarga from warga", koneksi);
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                hasil = dr["totalWarga"].ToString();
            }
            dr.Close();
            jmlWarga.Text = hasil;
        }


        //button tambah data warga
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            clearForm();
            tCari.Enabled = false;
            addstate = true;
            int totalWarga = 0;
            int totalKematian = 0;
            int totalData = 0;

            // Menghitung total entri dalam tabel warga
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = kn.strKoneksi();
                connection.Open();

                SqlCommand cmdWarga = new SqlCommand("SELECT COUNT(*) AS totalWarga FROM warga", connection);
                using (SqlDataReader readerWarga = cmdWarga.ExecuteReader())
                {
                    if (readerWarga.Read())
                    {
                        totalWarga = Convert.ToInt32(readerWarga["totalWarga"]);
                    }
                }
            }

            // Menghitung total entri dalam tabel kematian
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = kn.strKoneksi();
                connection.Open();

                SqlCommand cmdKematian = new SqlCommand("SELECT COUNT(*) AS totalKematian FROM kematian", connection);
                using (SqlDataReader readerKematian = cmdKematian.ExecuteReader())
                {
                    if (readerKematian.Read())
                    {
                        totalKematian = Convert.ToInt32(readerKematian["totalKematian"]);
                    }
                }
            }

            // Menghitung total data dari kedua tabel
            totalData = totalWarga + totalKematian;

            // Generate nomor registrasi progresif
            string noregPrefix = "3404012001" + DateTime.Now.Year + "AMXII";
            string hasil = (totalData + 1).ToString().PadLeft(3, '0');
            string NoregText = noregPrefix + hasil;

            Noreg.Text = NoregText;

            tNIK.Enabled = true;
            btnRefresh.Enabled = true;
            btnAdd.Enabled = false;
            btnAdd.Visible = false;
            btnSimpan.Visible = true;
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label6_TextChanged(object sender, EventArgs e)
        {
            if (addstate == true)
                tNama.Enabled = true;
        }

        private void tNama_TextChanged(object sender, EventArgs e)
        {
            if (addstate == true)
                cJabatan.Enabled = true;
        }

        private void cJabatan_TextChanged(object sender, EventArgs e)
        {
            if(addstate == true)
            {
                if (cJabatan.Text.Equals("Kepala Keluarga"))
                {
                    tKK.Enabled = true;
                    tKK.Visible = true;
                    cKK.Visible = false;
                }
                else
                {
                    SqlConnection koneksi = new SqlConnection();
                    koneksi.ConnectionString = kn.strKoneksi();
                    koneksi.Open();
                    SqlDataAdapter da = new SqlDataAdapter("select Distinct No_KK from warga", koneksi);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    koneksi.Close();

                    cKK.DisplayMember = "No_KK";
                    cKK.ValueMember = "No_KK";
                    cKK.DataSource = ds.Tables[0];

                    cKK.Enabled = true;
                    cKK.Visible = true;
                    tKK.Visible = false;
                }
            }
            else {
                if (cJabatan.Text.Equals("Kepala Keluarga"))
                {
                    //tKK.Enabled = true;
                    tKK.Visible = true;
                    cKK.Visible = false;
                }
                else
                {
                    SqlConnection koneksi = new SqlConnection();
                    koneksi.ConnectionString = kn.strKoneksi();
                    koneksi.Open();
                    SqlDataAdapter da = new SqlDataAdapter("select Distinct No_KK from warga", koneksi);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    koneksi.Close();

                    cKK.DisplayMember = "No_KK";
                    cKK.ValueMember = "No_KK";
                    cKK.DataSource = ds.Tables[0];

                    //cKK.Enabled = true;
                    cKK.Visible = true;
                    tKK.Visible = false;
                }
            }
        }


        private void tNIK_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
            (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void tKK_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;   
            }
        }

        private void tNama_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && 
            !char.IsSeparator(e.KeyChar) &&
            !char.IsPunctuation(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void cKK_TextChanged(object sender, EventArgs e)
        {
            if (addstate == true)
                cRT.Enabled = true;
            if (editstate == true)
                if (cJabatan.Text.Equals("Kepala Keluarga"))
                {
                    SqlConnection koneksi = new SqlConnection();
                    koneksi.ConnectionString = kn.strKoneksi();
                    koneksi.Open();
                    SqlDataAdapter da = new SqlDataAdapter("select Distinct id_rumah from rumah", koneksi);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    koneksi.Close();

                    cNoReg.DisplayMember = "id_rumah";
                    cNoReg.ValueMember = "id_rumah";
                    cNoReg.DataSource = ds.Tables[0];
                    //cNoReg.Enabled = true;
                    cNoReg.Visible = true;
                    tRT.Visible = false;
                }
                else
                {
                    string kk = cKK.Text;
                    string nRT = "";
                    SqlConnection koneksi = new SqlConnection();
                    koneksi.ConnectionString = kn.strKoneksi();
                    koneksi.Open();
                    SqlCommand cm = new SqlCommand("select id_rumah from warga where no_KK = @nKK", koneksi);
                    cm.CommandType = CommandType.Text;
                    cm.Parameters.Add(new SqlParameter("@nKK", kk));
                    SqlDataReader dr = cm.ExecuteReader();
                    while (dr.Read())
                    {
                        nRT = dr["id_rumah"].ToString();

                    }
                    dr.Close();
                    tRT.Text = nRT;
                    tRT.Visible = true;
                    tRT.Enabled = false;
                    cNoReg.Enabled = false;
                    cNoReg.Visible = false;
                }

            if(cRT.Text.Equals("Anggota Rumah Tangga"))
            {
                cKK.Enabled = true;
            }
        }

        private void tKK_TextChanged(object sender, EventArgs e)
        {
            if (addstate == true)
                cRT.Enabled = true;
            if (cRT.Text.Equals("Anggota Rumah Tangga"))
            {
                tKK.Enabled = true;
            }
            if (cJabatan.Text.Equals("Kepala Keluarga"))
            {
                SqlConnection koneksi = new SqlConnection();
                koneksi.ConnectionString = kn.strKoneksi();
                koneksi.Open();
                SqlDataAdapter da = new SqlDataAdapter("select Distinct id_rumah from rumah", koneksi);
                DataSet ds = new DataSet();
                da.Fill(ds);
                koneksi.Close();

                cNoReg.DisplayMember = "id_rumah";
                cNoReg.ValueMember = "id_rumah";
                cNoReg.DataSource = ds.Tables[0];
                //cNoReg.Enabled = true;
                cNoReg.Visible = true;
                tRT.Visible = false;
            }
            else
            {
                string kk = cKK.Text;
                string nRT = "";
                SqlConnection koneksi = new SqlConnection();
                koneksi.ConnectionString = kn.strKoneksi();
                koneksi.Open();
                SqlCommand cm = new SqlCommand("select id_rumah from warga where no_KK = @nKK", koneksi);
                cm.CommandType = CommandType.Text;
                cm.Parameters.Add(new SqlParameter("@nKK", kk));
                SqlDataReader dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    nRT = dr["id_rumah"].ToString();

                }
                dr.Close();
                tRT.Text = nRT;
                tRT.Visible = true;
                tRT.Enabled = false;
                cNoReg.Enabled = false;
                cNoReg.Visible = false;
            }
        }

        private void cRT_TextChanged(object sender, EventArgs e)
        {
            if (addstate == true && editstate == false)
            {
                if (cRT.Text.Equals("Kepala Rumah Tangga"))
                {
                    int hasil = 0;
                    SqlConnection koneksi = new SqlConnection();
                    koneksi.ConnectionString = kn.strKoneksi();
                    koneksi.Open();
                    SqlCommand cmd = new SqlCommand("select count(id_rumah) as totalRumah from rumah", koneksi);
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        hasil = Convert.ToInt32(dr["totalRumah"]) + 1;
                    }
                    dr.Close();
                    if (hasil < 10)
                    {
                        tRT.Text = "RMH" + "00" + hasil + "-" + tNama.Text;
                    }
                    else if (hasil >= 10)
                    {
                        tRT.Text = "RMH" + "00" + hasil + "-" + tNama.Text;
                    }
                    else if (hasil > 99)
                    {
                        tRT.Text = "RMH" + hasil + "-" + tNama.Text;
                    }
                    tRT.Visible = true;
                    cNoReg.Visible = false;
                }
            }
            else if (addstate == false && editstate == true)
            {
                if (cJabatan.Text.Equals("Kepala Keluarga"))
                {
                    SqlConnection koneksi = new SqlConnection();
                    koneksi.ConnectionString = kn.strKoneksi();
                    koneksi.Open();
                    SqlDataAdapter da = new SqlDataAdapter("select Distinct id_rumah from rumah", koneksi);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    koneksi.Close();

                    cNoReg.DisplayMember = "id_rumah";
                    cNoReg.ValueMember = "id_rumah";
                    cNoReg.DataSource = ds.Tables[0];
                    cNoReg.Enabled = true;
                    cNoReg.Visible = true;
                    tRT.Visible = false;
                }
                else
                {
                    string kk = cKK.Text;
                    string nRT = "";
                    SqlConnection koneksi = new SqlConnection();
                    koneksi.ConnectionString = kn.strKoneksi();
                    koneksi.Open();
                    SqlCommand cm = new SqlCommand("select id_rumah from warga where no_KK = @nKK", koneksi);
                    cm.CommandType = CommandType.Text;
                    cm.Parameters.Add(new SqlParameter("@nKK", kk));
                    SqlDataReader dr = cm.ExecuteReader();
                    while (dr.Read())
                    {
                        nRT = dr["id_rumah"].ToString();

                    }
                    dr.Close();
                    tRT.Text = nRT;
                    tRT.Visible = true;
                    tRT.Enabled = false;
                    cNoReg.Enabled = false;
                    cNoReg.Visible = false;
                }
            }

        }

            private void cNoReg_TextChanged(object sender, EventArgs e)
        {
            if (addstate == true)
                tTempat.Enabled = true;
        }

        private void tRT_TextChanged(object sender, EventArgs e)
        {
            if (addstate == true)
                tTempat.Enabled = true;
        }

        private void tTempat_TextChanged(object sender, EventArgs e)
        {
            if (addstate == true)
                dateTimePicker1.Enabled = true;
            dateTimePicker1.MaxDate = DateTime.Today;
        }

        private void dateTimePicker1_EnabledChanged(object sender, EventArgs e)
        {
            if (addstate == true)
                cJK.Enabled = true;
        }

        private void cJK_TextChanged(object sender, EventArgs e)
        {
            if (addstate == true)
                cAgama.Enabled = true;
        }

        private void cAgama_TextChanged(object sender, EventArgs e)
        {
            if (addstate == true)
                tAlamat.Enabled = true;
        }

        private void tAlamat_TextChanged(object sender, EventArgs e)
        {
            if (addstate == true)
                cPendidikan.Enabled = true;
        }

        private void cPendidikan_TextChanged(object sender, EventArgs e)
        {
            if (addstate == true)
                cPekerjaan.Enabled = true;
        }

        private void cPekerjaan_TextChanged(object sender, EventArgs e)
        {
            if (addstate == true)
            {
                    if (cPekerjaan.Text.Equals("Lainnya"))
                {
                    tPLain.Enabled = true;
                    tPLain.Visible = true;
                    tStatusKawin.Enabled = true;
                }
                else
                {
                    tStatusKawin.Enabled = true;
                }

            }
        }

        private void tStatusKawin_TextChanged(object sender, EventArgs e)
        {
            if (addstate == true)
                tStatusBPJS.Enabled = true;
                btnSimpan.Enabled = true;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            clearForm();
            defaultstatebutton();
            jmlWrg();
            dgv();
            addstate = false;
            editstate = false;
        }

        private void clearForm()
        {
            Noreg.Text = "";
            tNIK.Text = "";
            tNama.Text = "";
            cJabatan.SelectedItem = null;
            tKK.Text = "";
            cKK.SelectedItem = null;
            cRT.SelectedItem = null;
            tRT.Text = "";
            cNoReg.SelectedItem = null;
            tTempat.Text = "";
            dateTimePicker1.Value = DateTime.Today;
            cJK.SelectedItem = null;
            cAgama.SelectedItem = null;
            tAlamat.Text = "";
            cPendidikan.SelectedItem = null;
            cPekerjaan.SelectedItem = null;
            tPLain.Text = "";
            tStatusKawin.SelectedItem = null;
            tStatusBPJS.SelectedItem = null;

            Noreg.Enabled = false;
            tNIK.Enabled = false;
            tNama.Enabled = false;
            cJabatan.Enabled = false;
            tKK.Enabled = false;
            tKK.Visible = false;
            cKK.Enabled = false;
            cKK.Visible = true;
            cRT.Enabled = false;
            tRT.Enabled = false;
            tRT.Visible = false;
            cNoReg.Enabled = false;
            cNoReg.Visible = true;
            tTempat.Enabled = false;
            dateTimePicker1.Enabled = false;
            cJK.Enabled = false;
            cAgama.Enabled = false;
            tAlamat.Enabled = false;
            cPendidikan.Enabled = false;
            cPekerjaan.Enabled = false;
            tPLain.Enabled = false;
            tPLain.Visible = false;
            tStatusKawin.Enabled = false;
            tStatusBPJS.Enabled = false;

            btnRefresh.Enabled = false;
            btnAdd.Enabled = true;
            btnAdd.Visible = true;
            btnSimpan.Enabled = false;
            btnSimpan.Visible = false;
            dataGridView1.Enabled = true;
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
                string str = "";
                string str1 = "";

                string noreg = Noreg.Text;
                string nik = tNIK.Text;
                string nama = tNama.Text;
                string sKK = cJabatan.Text;
                string nKK = tKK.Text;
                string sRT = cRT.Text;
                string idRT = tRT.Text;
                string tmpt = tTempat.Text;
                DateTime tglL = dateTimePicker1.Value.Date;
                string JK = cJK.Text;
                string agama = cAgama.Text;
                string alamat = tAlamat.Text;
                string didik = cPendidikan.Text;
                string kerja = cPekerjaan.Text;
                string krjLn = tPLain.Text;
                string sKwin = tStatusKawin.Text;
                string sBPJS = tStatusBPJS.Text;
                DialogResult dg;
            if (editstate == true && addstate == false)
            {

                dg = MessageBox.Show("Apakah anda ingin mengubah data ini?", "Konfirmasi Ubah Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dg == DialogResult.Yes)
                {
                    SqlConnection koneksi = new SqlConnection();
                    koneksi.ConnectionString = kn.strKoneksi();
                    koneksi.Open();
                    str = "update warga set No_KK = @nkk,NIK = @nik,Nama = @nma,status_dalam_keluarga = @sk," +
                        "status_dalam_rumah_tangga = @srt,jenis_kelamin = @jk,alamat = @almt,tempat_lahir = @tl," +
                        "tanggal_lahir = @tgl,agama = @ag,pendidikan = @pdk,pekerjaan = @pkj,status_perkawinan = @sp,"
                        + "status_bpjs = @sbpjs,id_rumah = @idR where no_reg = @nreg";
                    SqlCommand cmd = new SqlCommand(str, koneksi);
                    cmd.CommandType = CommandType.Text;

                    if (cJabatan.Text.Equals("Kepala Keluarga") && cRT.Text.Equals("Kepala Rumah Tangga"))
                    {
                        string nRT = "";
                        SqlCommand cm = new SqlCommand("select id_rumah from warga where no_reg = @nReg", koneksi);
                        cm.CommandType = CommandType.Text;
                        cm.Parameters.Add(new SqlParameter("@nReg", noreg));
                        SqlDataReader dr = cm.ExecuteReader();
                        while (dr.Read())
                        {
                            nRT = dr["id_rumah"].ToString();

                        }
                        dr.Close();
                        if (!nRT.Equals(idRT))
                        {
                            str1 = "insert into rumah (id_rumah) values (@idRmh)";
                            SqlCommand cRmh = new SqlCommand(str1, koneksi);
                            cRmh.CommandType = CommandType.Text;
                            cRmh.Parameters.Add(new SqlParameter("idRmh", idRT));
                            cRmh.ExecuteNonQuery();
                        }
                        if (!kerja.Equals("Lainnya"))
                        {
                            cmd.Parameters.Add(new SqlParameter("nreg", noreg));
                            cmd.Parameters.Add(new SqlParameter("nkk", nKK));
                            cmd.Parameters.Add(new SqlParameter("nik", nik));
                            cmd.Parameters.Add(new SqlParameter("nma", nama));
                            cmd.Parameters.Add(new SqlParameter("sk", sKK));
                            cmd.Parameters.Add(new SqlParameter("srt", sRT));
                            cmd.Parameters.Add(new SqlParameter("jk", JK));
                            cmd.Parameters.Add(new SqlParameter("almt", alamat));
                            cmd.Parameters.Add(new SqlParameter("tl", tmpt));
                            cmd.Parameters.Add(new SqlParameter("tgl", tglL));
                            cmd.Parameters.Add(new SqlParameter("ag", agama));
                            cmd.Parameters.Add(new SqlParameter("pdk", didik));
                            cmd.Parameters.Add(new SqlParameter("pkj", kerja));
                            cmd.Parameters.Add(new SqlParameter("sp", sKwin));
                            cmd.Parameters.Add(new SqlParameter("sbpjs", sBPJS));
                            cmd.Parameters.Add(new SqlParameter("idR", idRT));
                        }
                        else
                        {
                            cmd.Parameters.Add(new SqlParameter("nreg", noreg));
                            cmd.Parameters.Add(new SqlParameter("nkk", nKK));
                            cmd.Parameters.Add(new SqlParameter("nik", nik));
                            cmd.Parameters.Add(new SqlParameter("nma", nama));
                            cmd.Parameters.Add(new SqlParameter("sk", sKK));
                            cmd.Parameters.Add(new SqlParameter("srt", sRT));
                            cmd.Parameters.Add(new SqlParameter("jk", JK));
                            cmd.Parameters.Add(new SqlParameter("almt", alamat));
                            cmd.Parameters.Add(new SqlParameter("tl", tmpt));
                            cmd.Parameters.Add(new SqlParameter("tgl", tglL));
                            cmd.Parameters.Add(new SqlParameter("ag", agama));
                            cmd.Parameters.Add(new SqlParameter("pdk", didik));
                            cmd.Parameters.Add(new SqlParameter("pkj", krjLn));
                            cmd.Parameters.Add(new SqlParameter("sp", sKwin));
                            cmd.Parameters.Add(new SqlParameter("sbpjs", sBPJS));
                            cmd.Parameters.Add(new SqlParameter("idR", idRT));
                        }


                    }
                    else if (cJabatan.Text.Equals("Kepala Keluarga"))
                    {
                        string idaRT = cNoReg.SelectedValue.ToString();
                        if (!kerja.Equals("Lainnya"))
                        {
                            cmd.Parameters.Add(new SqlParameter("nreg", noreg));
                            cmd.Parameters.Add(new SqlParameter("nkk", nKK));
                            cmd.Parameters.Add(new SqlParameter("nik", nik));
                            cmd.Parameters.Add(new SqlParameter("nma", nama));
                            cmd.Parameters.Add(new SqlParameter("sk", sKK));
                            cmd.Parameters.Add(new SqlParameter("srt", sRT));
                            cmd.Parameters.Add(new SqlParameter("jk", JK));
                            cmd.Parameters.Add(new SqlParameter("almt", alamat));
                            cmd.Parameters.Add(new SqlParameter("tl", tmpt));
                            cmd.Parameters.Add(new SqlParameter("tgl", tglL));
                            cmd.Parameters.Add(new SqlParameter("ag", agama));
                            cmd.Parameters.Add(new SqlParameter("pdk", didik));
                            cmd.Parameters.Add(new SqlParameter("pkj", kerja));
                            cmd.Parameters.Add(new SqlParameter("sp", sKwin));
                            cmd.Parameters.Add(new SqlParameter("sbpjs", sBPJS));
                            cmd.Parameters.Add(new SqlParameter("idR", idaRT));
                        }
                        else
                        {
                            cmd.Parameters.Add(new SqlParameter("nreg", noreg));
                            cmd.Parameters.Add(new SqlParameter("nkk", nKK));
                            cmd.Parameters.Add(new SqlParameter("nik", nik));
                            cmd.Parameters.Add(new SqlParameter("nma", nama));
                            cmd.Parameters.Add(new SqlParameter("sk", sKK));
                            cmd.Parameters.Add(new SqlParameter("srt", sRT));
                            cmd.Parameters.Add(new SqlParameter("jk", JK));
                            cmd.Parameters.Add(new SqlParameter("almt", alamat));
                            cmd.Parameters.Add(new SqlParameter("tl", tmpt));
                            cmd.Parameters.Add(new SqlParameter("tgl", tglL));
                            cmd.Parameters.Add(new SqlParameter("ag", agama));
                            cmd.Parameters.Add(new SqlParameter("pdk", didik));
                            cmd.Parameters.Add(new SqlParameter("pkj", krjLn));
                            cmd.Parameters.Add(new SqlParameter("sp", sKwin));
                            cmd.Parameters.Add(new SqlParameter("sbpjs", sBPJS));
                            cmd.Parameters.Add(new SqlParameter("idR", idaRT));
                        }
                    }
                    else
                    {
                        string naKK = cKK.SelectedValue.ToString();
                        if (!kerja.Equals("Lainnya"))
                        {
                            cmd.Parameters.Add(new SqlParameter("nreg", noreg));
                            cmd.Parameters.Add(new SqlParameter("nkk", naKK));
                            cmd.Parameters.Add(new SqlParameter("nik", nik));
                            cmd.Parameters.Add(new SqlParameter("nma", nama));
                            cmd.Parameters.Add(new SqlParameter("sk", sKK));
                            cmd.Parameters.Add(new SqlParameter("srt", sRT));
                            cmd.Parameters.Add(new SqlParameter("jk", JK));
                            cmd.Parameters.Add(new SqlParameter("almt", alamat));
                            cmd.Parameters.Add(new SqlParameter("tl", tmpt));
                            cmd.Parameters.Add(new SqlParameter("tgl", tglL));
                            cmd.Parameters.Add(new SqlParameter("ag", agama));
                            cmd.Parameters.Add(new SqlParameter("pdk", didik));
                            cmd.Parameters.Add(new SqlParameter("pkj", kerja));
                            cmd.Parameters.Add(new SqlParameter("sp", sKwin));
                            cmd.Parameters.Add(new SqlParameter("sbpjs", sBPJS));
                            cmd.Parameters.Add(new SqlParameter("idR", idRT));
                        }
                        else
                        {
                            cmd.Parameters.Add(new SqlParameter("nreg", noreg));
                            cmd.Parameters.Add(new SqlParameter("nkk", naKK));
                            cmd.Parameters.Add(new SqlParameter("nik", nik));
                            cmd.Parameters.Add(new SqlParameter("nma", nama));
                            cmd.Parameters.Add(new SqlParameter("sk", sKK));
                            cmd.Parameters.Add(new SqlParameter("srt", sRT));
                            cmd.Parameters.Add(new SqlParameter("jk", JK));
                            cmd.Parameters.Add(new SqlParameter("almt", alamat));
                            cmd.Parameters.Add(new SqlParameter("tl", tmpt));
                            cmd.Parameters.Add(new SqlParameter("tgl", tglL));
                            cmd.Parameters.Add(new SqlParameter("ag", agama));
                            cmd.Parameters.Add(new SqlParameter("pdk", didik));
                            cmd.Parameters.Add(new SqlParameter("pkj", krjLn));
                            cmd.Parameters.Add(new SqlParameter("sp", sKwin));
                            cmd.Parameters.Add(new SqlParameter("sbpjs", sBPJS));
                            cmd.Parameters.Add(new SqlParameter("idR", idRT));
                        }
                    }
                    cmd.ExecuteNonQuery();
                    koneksi.Close();
                    MessageBox.Show("Data Berhasil Diperbaruhi", "Sukses", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    clearForm();
                    dgv();
                    editstate = false;
                }
                }
            else if (addstate == true && editstate == false)
            {
                //string str = "";
                //string str1 = "";

                //string noreg = Noreg.Text;
                //string nik = tNIK.Text;
                //string nama = tNama.Text;
                //string sKK = cJabatan.Text;
                //string nKK = tKK.Text;
                //string sRT = cRT.Text;
                //string idRT = tRT.Text;
                //string tmpt = tTempat.Text;
                //DateTime tglL = dateTimePicker1.Value.Date;
                //string JK = cJK.Text;
                //string agama = cAgama.Text;
                //string alamat = tAlamat.Text;
                //string didik = cPendidikan.Text;
                //string kerja = cPekerjaan.Text;
                //string krjLn = tPLain.Text;
                //string sKwin = tStatusKawin.Text;
                //string sBPJS = tStatusBPJS.Text;
                //DialogResult dg;
                dg = MessageBox.Show("Apakah data yang anda masukan sudah sesuai?", "Konfirmasi Tambah Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dg == DialogResult.Yes)
                {
                    SqlConnection koneksi = new SqlConnection();
                    koneksi.ConnectionString = kn.strKoneksi();
                    koneksi.Open();
                    str = "insert into warga (no_reg,No_KK,NIK,Nama,status_dalam_keluarga,status_dalam_rumah_tangga, " +
                        "jenis_kelamin,alamat,tempat_lahir,tanggal_lahir,agama,pendidikan,pekerjaan,status_perkawinan, " +
                        "status_bpjs,id_rumah) values (@nreg,@nkk,@nik,@nma,@sk,@srt,@jk,@almt,@tl,@tgl,@ag,@pdk,@pkj," +
                        "@sp,@sbpjs,@idR)";
                    SqlCommand cmd = new SqlCommand(str, koneksi);
                    cmd.CommandType = CommandType.Text;

                    if (cJabatan.Text.Equals("Kepala Keluarga") && cRT.Text.Equals("Kepala Rumah Tangga"))
                    {
                        str1 = "insert into rumah (id_rumah) values (@idRmh)";
                        SqlCommand cRmh = new SqlCommand(str1, koneksi);
                        cRmh.CommandType = CommandType.Text;
                        cRmh.Parameters.Add(new SqlParameter("idRmh", idRT));
                        cRmh.ExecuteNonQuery();
                        if (!kerja.Equals("Lainnya"))
                        {
                            cmd.Parameters.Add(new SqlParameter("nreg", noreg));
                            cmd.Parameters.Add(new SqlParameter("nkk", nKK));
                            cmd.Parameters.Add(new SqlParameter("nik", nik));
                            cmd.Parameters.Add(new SqlParameter("nma", nama));
                            cmd.Parameters.Add(new SqlParameter("sk", sKK));
                            cmd.Parameters.Add(new SqlParameter("srt", sRT));
                            cmd.Parameters.Add(new SqlParameter("jk", JK));
                            cmd.Parameters.Add(new SqlParameter("almt", alamat));
                            cmd.Parameters.Add(new SqlParameter("tl", tmpt));
                            cmd.Parameters.Add(new SqlParameter("tgl", tglL));
                            cmd.Parameters.Add(new SqlParameter("ag", agama));
                            cmd.Parameters.Add(new SqlParameter("pdk", didik));
                            cmd.Parameters.Add(new SqlParameter("pkj", kerja));
                            cmd.Parameters.Add(new SqlParameter("sp", sKwin));
                            cmd.Parameters.Add(new SqlParameter("sbpjs", sBPJS));
                            cmd.Parameters.Add(new SqlParameter("idR", idRT));
                        }
                        else
                        {
                            cmd.Parameters.Add(new SqlParameter("nreg", noreg));
                            cmd.Parameters.Add(new SqlParameter("nkk", nKK));
                            cmd.Parameters.Add(new SqlParameter("nik", nik));
                            cmd.Parameters.Add(new SqlParameter("nma", nama));
                            cmd.Parameters.Add(new SqlParameter("sk", sKK));
                            cmd.Parameters.Add(new SqlParameter("srt", sRT));
                            cmd.Parameters.Add(new SqlParameter("jk", JK));
                            cmd.Parameters.Add(new SqlParameter("almt", alamat));
                            cmd.Parameters.Add(new SqlParameter("tl", tmpt));
                            cmd.Parameters.Add(new SqlParameter("tgl", tglL));
                            cmd.Parameters.Add(new SqlParameter("ag", agama));
                            cmd.Parameters.Add(new SqlParameter("pdk", didik));
                            cmd.Parameters.Add(new SqlParameter("pkj", krjLn));
                            cmd.Parameters.Add(new SqlParameter("sp", sKwin));
                            cmd.Parameters.Add(new SqlParameter("sbpjs", sBPJS));
                            cmd.Parameters.Add(new SqlParameter("idR", idRT));
                        }


                    }
                    else if (cJabatan.Text.Equals("Kepala Keluarga"))
                    {
                        string idaRT = cNoReg.SelectedValue.ToString();
                        if (!kerja.Equals("Lainnya"))
                        {
                            cmd.Parameters.Add(new SqlParameter("nreg", noreg));
                            cmd.Parameters.Add(new SqlParameter("nkk", nKK));
                            cmd.Parameters.Add(new SqlParameter("nik", nik));
                            cmd.Parameters.Add(new SqlParameter("nma", nama));
                            cmd.Parameters.Add(new SqlParameter("sk", sKK));
                            cmd.Parameters.Add(new SqlParameter("srt", sRT));
                            cmd.Parameters.Add(new SqlParameter("jk", JK));
                            cmd.Parameters.Add(new SqlParameter("almt", alamat));
                            cmd.Parameters.Add(new SqlParameter("tl", tmpt));
                            cmd.Parameters.Add(new SqlParameter("tgl", tglL));
                            cmd.Parameters.Add(new SqlParameter("ag", agama));
                            cmd.Parameters.Add(new SqlParameter("pdk", didik));
                            cmd.Parameters.Add(new SqlParameter("pkj", kerja));
                            cmd.Parameters.Add(new SqlParameter("sp", sKwin));
                            cmd.Parameters.Add(new SqlParameter("sbpjs", sBPJS));
                            cmd.Parameters.Add(new SqlParameter("idR", idaRT));
                        }
                        else
                        {
                            cmd.Parameters.Add(new SqlParameter("nreg", noreg));
                            cmd.Parameters.Add(new SqlParameter("nkk", nKK));
                            cmd.Parameters.Add(new SqlParameter("nik", nik));
                            cmd.Parameters.Add(new SqlParameter("nma", nama));
                            cmd.Parameters.Add(new SqlParameter("sk", sKK));
                            cmd.Parameters.Add(new SqlParameter("srt", sRT));
                            cmd.Parameters.Add(new SqlParameter("jk", JK));
                            cmd.Parameters.Add(new SqlParameter("almt", alamat));
                            cmd.Parameters.Add(new SqlParameter("tl", tmpt));
                            cmd.Parameters.Add(new SqlParameter("tgl", tglL));
                            cmd.Parameters.Add(new SqlParameter("ag", agama));
                            cmd.Parameters.Add(new SqlParameter("pdk", didik));
                            cmd.Parameters.Add(new SqlParameter("pkj", krjLn));
                            cmd.Parameters.Add(new SqlParameter("sp", sKwin));
                            cmd.Parameters.Add(new SqlParameter("sbpjs", sBPJS));
                            cmd.Parameters.Add(new SqlParameter("idR", idaRT));
                        }
                    }
                    else
                    {
                        string naKK = cKK.SelectedValue.ToString();
                        if (!kerja.Equals("Lainnya"))
                        {
                            cmd.Parameters.Add(new SqlParameter("nreg", noreg));
                            cmd.Parameters.Add(new SqlParameter("nkk", naKK));
                            cmd.Parameters.Add(new SqlParameter("nik", nik));
                            cmd.Parameters.Add(new SqlParameter("nma", nama));
                            cmd.Parameters.Add(new SqlParameter("sk", sKK));
                            cmd.Parameters.Add(new SqlParameter("srt", sRT));
                            cmd.Parameters.Add(new SqlParameter("jk", JK));
                            cmd.Parameters.Add(new SqlParameter("almt", alamat));
                            cmd.Parameters.Add(new SqlParameter("tl", tmpt));
                            cmd.Parameters.Add(new SqlParameter("tgl", tglL));
                            cmd.Parameters.Add(new SqlParameter("ag", agama));
                            cmd.Parameters.Add(new SqlParameter("pdk", didik));
                            cmd.Parameters.Add(new SqlParameter("pkj", kerja));
                            cmd.Parameters.Add(new SqlParameter("sp", sKwin));
                            cmd.Parameters.Add(new SqlParameter("sbpjs", sBPJS));
                            cmd.Parameters.Add(new SqlParameter("idR", idRT));
                        }
                        else
                        {
                            cmd.Parameters.Add(new SqlParameter("nreg", noreg));
                            cmd.Parameters.Add(new SqlParameter("nkk", naKK));
                            cmd.Parameters.Add(new SqlParameter("nik", nik));
                            cmd.Parameters.Add(new SqlParameter("nma", nama));
                            cmd.Parameters.Add(new SqlParameter("sk", sKK));
                            cmd.Parameters.Add(new SqlParameter("srt", sRT));
                            cmd.Parameters.Add(new SqlParameter("jk", JK));
                            cmd.Parameters.Add(new SqlParameter("almt", alamat));
                            cmd.Parameters.Add(new SqlParameter("tl", tmpt));
                            cmd.Parameters.Add(new SqlParameter("tgl", tglL));
                            cmd.Parameters.Add(new SqlParameter("ag", agama));
                            cmd.Parameters.Add(new SqlParameter("pdk", didik));
                            cmd.Parameters.Add(new SqlParameter("pkj", krjLn));
                            cmd.Parameters.Add(new SqlParameter("sp", sKwin));
                            cmd.Parameters.Add(new SqlParameter("sbpjs", sBPJS));
                            cmd.Parameters.Add(new SqlParameter("idR", idRT));
                        }
                    }
                    cmd.ExecuteNonQuery();
                    koneksi.Close();
                    MessageBox.Show("Data Berhasil DiTambahkan", "Sukses", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                    clearForm();
                    dgv();
                    jmlWrg();
                    addstate = false;
                }
            }
            
        }

        //edit button 
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            editstate = true;
            btnAdd.Visible = false;
            btnAdd.Enabled = false;
            btnSimpan.Enabled = true;
            btnSimpan.Visible = true;
            enableallform();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string str = "";
            SqlConnection koneksi = new SqlConnection();
            koneksi.ConnectionString = kn.strKoneksi();
            koneksi.Open();

            string noreg = Noreg.Text;
            DialogResult dg;
            dg = MessageBox.Show("Apakah anda ingin menghapus data ini?", "Konfirmasi Ubah Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dg == DialogResult.Yes) {
                str = "delete from warga where no_reg = @nreg";
                SqlCommand cmd = new SqlCommand(str, koneksi);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("nreg", noreg));
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
                string str = "select No_Reg as 'Nomer Registrasi', No_KK as 'No Kartu Keluarga', " +
                    "Nama as 'Nama Warga', jenis_kelamin as 'Jenis Kelamin'," +
                    " tempat_lahir as 'Tempat Lahir', tanggal_lahir as 'Tgl/Bln/Th Lahir', " +
                    "agama as Agama, pendidikan as Pendidikan, " +
                    "pekerjaan as Pekerjaan, status_perkawinan as 'Status Perkawinan', " +
                    "status_bpjs as 'Status BPJS' from warga where Nama Like '%" + tCari.Text + "%'";
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
                fWarga.Enabled = false;
            }
            else
            {
                fWarga.Enabled = true;
                dgv();
            }
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            disableallform();
            clearForm();

            string nokk = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            string nama = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[1].Value);

            string noreg = "";
            string nik = "";
            //string nama = "";
            string sKK = "";
            string nKK = "";
            string sRT = "";
            string idRT = "";
            string tmpt = "";
            string tglL = "";
            string JK = "";
            string agama = "";
            string alamat = "";
            string didik = "";
            string kerja = "";
            string sKwin = "";
            string sBPJS = "";

            SqlConnection koneksi = new SqlConnection();
            koneksi.ConnectionString = kn.strKoneksi();
            koneksi.Open();
            SqlCommand cmd = new SqlCommand("select no_reg,No_KK,NIK,Nama,status_dalam_keluarga,status_dalam_rumah_tangga, " +
                "jenis_kelamin,alamat,tempat_lahir,tanggal_lahir,agama,pendidikan,pekerjaan,status_perkawinan, " +
                "status_bpjs,id_rumah from warga where no_kk = @nokk and Nama = @nm", koneksi);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(new SqlParameter("@nokk", nokk));
            cmd.Parameters.Add(new SqlParameter("@nm", nama));
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                noreg = dr["No_Reg"].ToString();
                nik = dr["NIK"].ToString();
                nama = dr["Nama"].ToString();
                sKK = dr["status_dalam_keluarga"].ToString();
                nKK = dr["No_KK"].ToString();
                sRT = dr["status_dalam_rumah_tangga"].ToString();
                idRT = dr["id_rumah"].ToString();
                tmpt = dr["tempat_lahir"].ToString();
                tglL = dr["tanggal_lahir"].ToString();
                JK = dr["jenis_kelamin"].ToString();
                agama = dr["agama"].ToString();
                alamat = dr["alamat"].ToString();
                didik = dr["pendidikan"].ToString();
                kerja = dr["pekerjaan"].ToString();
                sKwin = dr["status_perkawinan"].ToString();
                sBPJS = dr["status_bpjs"].ToString();

            }
            dr.Close();

            Console.WriteLine(noreg + "bla blas noreg out before if else");
            Console.WriteLine(nik + "bla blas nik out before if else"); 
            Console.WriteLine(nama + "bla blas nama out before if else");
            Console.WriteLine(sKK + "bla blas sKK out before if else"); 
            Console.WriteLine(nKK + "bla blas nKK out before if else");
            Console.WriteLine(sRT + "bla blas sRT out before if else"); 
            Console.WriteLine(idRT + "bla blas idrt out before if else");
            Console.WriteLine(tmpt + "bla blas tmpt out before if else");
            Console.WriteLine(tglL + "bla blas tglL out before if else");
            Console.WriteLine(JK + "bla blas JK out before if else"); 
            Console.WriteLine(agama + "bla blas agama out before if else");
            Console.WriteLine(alamat + "bla blas alamat out before if else"); 
            Console.WriteLine(didik + "bla blas didik out before if else");
            Console.WriteLine(kerja + "bla blas kerja out before if else");
            Console.WriteLine(sKwin + "bla blas sKwin out before if else"); 
            Console.WriteLine(sKwin + "bla blas sKwin out before if else");
            Console.WriteLine(tRT.Text + "bla blas Text RT out before if else");
            Console.WriteLine(cNoReg.Text + "bla blas Combo RT out before if else");

            if (sKK.Equals("Kepala Keluarga") && sRT.Equals("Kepala Rumah Tangga"))
            {
                if (kerja.Equals("PNS") || kerja.Equals("Wirausaha") ||
                    kerja.Equals("Pelajar/Mahasiswa") || kerja.Equals("Buruh/Karyawan"))
                {
                    Noreg.Text = noreg;
                    tNIK.Text = nik;
                    tNama.Text = nama;
                    cJabatan.Text = sKK;
                    tKK.Text = nKK;
                    cRT.Text = sRT;
                    tRT.Text = idRT;
                    tTempat.Text = tmpt;
                    dateTimePicker1.Value = Convert.ToDateTime(tglL);
                    cJK.Text = JK;
                    cAgama.Text = agama;
                    tAlamat.Text = alamat;
                    cPendidikan.Text = didik;
                    cPekerjaan.Text = kerja;
                    tStatusKawin.Text = sKwin;
                    tStatusBPJS.Text = sBPJS;
                    Console.WriteLine(tRT.Text + "bla blas Text RT Kepala & Kepala");
                    Console.WriteLine(cNoReg.Text + "bla blas Combo RT Kepala & Kepala");
                }
                else
                {
                    Noreg.Text = noreg;
                    tNIK.Text = nik;
                    tNama.Text = nama;
                    cJabatan.Text = sKK;
                    tKK.Text = nKK;
                    cKK.Text = nKK;
                    cRT.Text = sRT;
                    tRT.Text = idRT;
                    tTempat.Text = tmpt;
                    dateTimePicker1.Value = Convert.ToDateTime(tglL);
                    cJK.Text = JK;
                    cAgama.Text = agama;
                    tAlamat.Text = alamat;
                    cPendidikan.Text = didik;
                    cPekerjaan.Text = "Lainnya";
                    tPLain.Text = kerja;
                    tStatusKawin.Text = sKwin;
                    tStatusBPJS.Text = sBPJS;
                    Console.WriteLine(tRT.Text + "bla blas Text RT !!Kepala & Kepala");
                    Console.WriteLine(cNoReg.Text + "bla blas Combo RT !!Kepala & Kepala");
                }


            }
            else if (sKK.Equals("Kepala Keluarga"))
            {

                if (kerja.Equals("PNS") || kerja.Equals("Wirausaha") ||
                    kerja.Equals("Pelajar/Mahasiswa") || kerja.Equals("Buruh/Karyawan"))
                {
                    Noreg.Text = noreg;
                    tNIK.Text = nik;
                    tNama.Text = nama;
                    cJabatan.Text = sKK;
                    tKK.Text = nKK;
                    cRT.Text = sRT;
                    cNoReg.Text = idRT;
                    tTempat.Text = tmpt;
                    dateTimePicker1.Value = Convert.ToDateTime(tglL);
                    cJK.Text = JK;
                    cAgama.Text = agama;
                    tAlamat.Text = alamat;
                    cPendidikan.Text = didik;
                    cPekerjaan.Text = kerja;
                    tStatusKawin.Text = sKwin;
                    tStatusBPJS.Text = sBPJS;
                    Console.WriteLine(tRT.Text + "bla blas Text RT Status KK");
                    Console.WriteLine(cNoReg.Text + "bla blas Combo RT Status KK");
                }
                else
                {
                    Noreg.Text = noreg;
                    tNIK.Text = nik;
                    tNama.Text = nama;
                    cJabatan.Text = sKK;
                    tKK.Text = nKK;
                    cKK.Text = nKK;
                    cRT.Text = sRT;
                    cNoReg.Text = idRT;
                    tTempat.Text = tmpt;
                    dateTimePicker1.Value = Convert.ToDateTime(tglL);
                    cJK.Text = JK;
                    cAgama.Text = agama;
                    tAlamat.Text = alamat;
                    cPendidikan.Text = didik;
                    cPekerjaan.Text = "Lainnya";
                    tPLain.Text = kerja;
                    tStatusKawin.Text = sKwin;
                    tStatusBPJS.Text = sBPJS;
                    Console.WriteLine(tRT.Text + "bla blas Text RT !Status KK");
                    Console.WriteLine(cNoReg.Text + "bla blas Combo RT !Status KK");
                }
            }
            else
            {
                if (kerja.Equals("PNS") || kerja.Equals("Wirausaha") ||
                    kerja.Equals("Pelajar/Mahasiswa") || kerja.Equals("Buruh/Karyawan"))
                {
                    Noreg.Text = noreg;
                    tNIK.Text = nik;
                    tNama.Text = nama;
                    cJabatan.Text = sKK;
                    cKK.Text = nKK;
                    cRT.Text = sRT;
                    cNoReg.Text = idRT;
                    tTempat.Text = tmpt;
                    dateTimePicker1.Value = Convert.ToDateTime(tglL);
                    cJK.Text = JK;
                    cAgama.Text = agama;
                    tAlamat.Text = alamat;
                    cPendidikan.Text = didik;
                    cPekerjaan.Text = kerja;
                    tStatusKawin.Text = sKwin;
                    tStatusBPJS.Text = sBPJS;
                    Console.WriteLine(tRT.Text + "bla blas Text RT Pekerjaan ");
                    Console.WriteLine(cNoReg.Text + "bla blas Combo RT Pekerjaan");
                }
                else
                {
                    Noreg.Text = noreg;
                    tNIK.Text = nik;
                    tNama.Text = nama;
                    cJabatan.Text = sKK;
                    cKK.Text = nKK;
                    cRT.Text = sRT;
                    cNoReg.Text = idRT;
                    tTempat.Text = tmpt;
                    dateTimePicker1.Value = Convert.ToDateTime(tglL);
                    cJK.Text = JK;
                    cAgama.Text = agama;
                    tAlamat.Text = alamat;
                    cPendidikan.Text = didik;
                    cPekerjaan.Text = "Lainnya";
                    tPLain.Text = kerja;
                    tStatusKawin.Text = sKwin;
                    tStatusBPJS.Text = sBPJS;
                    Console.WriteLine(tRT.Text + "bla blas Text RT Pekerjaan Lainnya");
                    Console.WriteLine(cNoReg.Text + "bla blas Combo RT Pekerjaan Lainnya");
                }
            }
            btnAdd.Enabled = true;
            btnRefresh.Enabled = true;
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;
            Console.WriteLine(tRT.Text + "bla blas Text RT out");
            Console.WriteLine(cNoReg.Text + "bla blas Combo RT out");
        }

        private void fWarga_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!fWarga.Text.Equals(""))
            {
                tCari.Enabled = false;
            }
            string str = "";
            SqlConnection koneksi = new SqlConnection();
            koneksi.ConnectionString = kn.strKoneksi();
            koneksi.Open();
            if (fWarga.Text.Equals("Balita"))
            {
                str= "select No_KK as 'No Kartu Keluarga', Nama as 'Nama Warga' from warga " +
                    "where 0 <= CONVERT(int, YEAR(Getdate()), 105) - convert(int, year(tanggal_lahir), 105) " +
                    "and CONVERT(int, YEAR(Getdate()),105) -convert(int, year(tanggal_lahir), 105) <= 4";
                SqlDataAdapter ad = new SqlDataAdapter(str, koneksi);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    //col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                string hasil = "";
                SqlCommand cmd = new SqlCommand("select count(No_Reg) as totalWarga from warga " +
                    "where 0 <= CONVERT(int, YEAR(Getdate()), 105) - convert(int, year(tanggal_lahir), 105) " +
                    "and CONVERT(int, YEAR(Getdate()),105) -convert(int, year(tanggal_lahir), 105) <= 4", 
                    koneksi);
                cmd.CommandType = CommandType.Text;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    hasil = dr["totalWarga"].ToString();
                }
                dr.Close();
                jmlWarga.Text = hasil;
                koneksi.Close();
            }
            else if (fWarga.Text.Equals("PUS"))
            {
                str = "select No_KK as 'No Kartu Keluarga', Nama as 'Nama Warga' from warga " +
                    "where 15 <= CONVERT(int, YEAR(Getdate()), 105) - convert(int, year(tanggal_lahir), 105) " +
                    "and CONVERT(int, YEAR(Getdate()),105) -convert(int, year(tanggal_lahir), 105) <= 49";
                SqlDataAdapter ad = new SqlDataAdapter(str, koneksi);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    //col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                string hasil = "";
                SqlCommand cmd = new SqlCommand("select count(No_Reg) as totalWarga from warga " +
                    "where 15 <= CONVERT(int, YEAR(Getdate()), 105) - convert(int, year(tanggal_lahir), 105) " +
                    "and CONVERT(int, YEAR(Getdate()),105) -convert(int, year(tanggal_lahir), 105) <= 49",
                    koneksi);
                cmd.CommandType = CommandType.Text;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    hasil = dr["totalWarga"].ToString();
                }
                dr.Close();
                jmlWarga.Text = hasil;
                koneksi.Close();
            }
            else if (fWarga.Text.Equals("WUS"))
            {
                str = "select No_KK as 'No Kartu Keluarga', Nama as 'Nama Warga' from warga " +
                    "where 20 <= CONVERT(int, YEAR(Getdate()), 105) - convert(int, year(tanggal_lahir), 105) " +
                    "and CONVERT(int, YEAR(Getdate()),105) -convert(int, year(tanggal_lahir), 105) <= 45 " +
                    "and jenis_kelamin = 'P'";
                SqlDataAdapter ad = new SqlDataAdapter(str, koneksi);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    //col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                string hasil = "";
                SqlCommand cmd = new SqlCommand("select count(No_Reg) as totalWarga from warga " +
                    "where 20 <= CONVERT(int, YEAR(Getdate()), 105) - convert(int, year(tanggal_lahir), 105) " +
                    "and CONVERT(int, YEAR(Getdate()),105) -convert(int, year(tanggal_lahir), 105) <= 45 " +
                    "and jenis_kelamin = 'P'",
                    koneksi);
                cmd.CommandType = CommandType.Text;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    hasil = dr["totalWarga"].ToString();
                }
                dr.Close();
                jmlWarga.Text = hasil;
                koneksi.Close();
            }
            else if (fWarga.Text.Equals("Lansia"))
            {
                str = "select No_KK as 'No Kartu Keluarga', Nama as 'Nama Warga' from warga " +
                    "where 60 < CONVERT(int, YEAR(Getdate()), 105) - convert(int, year(tanggal_lahir), 105)";
                SqlDataAdapter ad = new SqlDataAdapter(str, koneksi);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    //col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                string hasil = "";
                SqlCommand cmd = new SqlCommand("select count(No_Reg) as totalWarga from warga " +
                    "where 60 < CONVERT(int, YEAR(Getdate()), 105) - convert(int, year(tanggal_lahir), 105)",
                    koneksi);
                cmd.CommandType = CommandType.Text;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    hasil = dr["totalWarga"].ToString();
                }
                dr.Close();
                jmlWarga.Text = hasil;
                koneksi.Close();
            }
            else if (fWarga.Text.Equals("PNS"))
            {
                str = "select No_KK as 'No Kartu Keluarga', Nama as 'Nama Warga' from warga " + " where pekerjaan Like 'PNS'";
                SqlDataAdapter ad = new SqlDataAdapter(str, koneksi);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    //col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                string hasil = "";
                SqlCommand cmd = new SqlCommand("select count(No_Reg) as totalWarga from warga " +
                    "where pekerjaan Like 'PNS'", koneksi);
                cmd.CommandType = CommandType.Text;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    hasil = dr["totalWarga"].ToString();
                }
                dr.Close();
                jmlWarga.Text = hasil;
                koneksi.Close();
            }
            else if (fWarga.Text.Equals("Wirausaha"))
            {
                str = "select No_KK as 'No Kartu Keluarga', Nama as 'Nama Warga' from warga " + "where pekerjaan Like 'Wirausaha'";
                SqlDataAdapter ad = new SqlDataAdapter(str, koneksi);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    //col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                string hasil = "";
                SqlCommand cmd = new SqlCommand("select count(No_Reg) as totalWarga from warga " +
                    "where pekerjaan Like 'Wirausaha'", koneksi);
                cmd.CommandType = CommandType.Text;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    hasil = dr["totalWarga"].ToString();
                }
                dr.Close();
                jmlWarga.Text = hasil;
                koneksi.Close();
            }
            else if (fWarga.Text.Equals("Pelajar/Mahasiswa"))
            {
                str = "select No_KK as 'No Kartu Keluarga', Nama as 'Nama Warga' from warga " + " where pekerjaan Like 'Pelajar/Mahasiswa'";
                SqlDataAdapter ad = new SqlDataAdapter(str, koneksi);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    //col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                string hasil = "";
                SqlCommand cmd = new SqlCommand("select count(No_Reg) as totalWarga from warga " +
                    "where pekerjaan Like 'Pelajar/Mahasiswa'", koneksi);
                cmd.CommandType = CommandType.Text;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    hasil = dr["totalWarga"].ToString();
                }
                dr.Close();
                jmlWarga.Text = hasil;
                koneksi.Close();
            }
            else if (fWarga.Text.Equals("Buruh/Karyawan"))
            {
                str = "select No_KK as 'No Kartu Keluarga', Nama as 'Nama Warga' from warga " + "where pekerjaan Like 'Buruh/Karyawan'";
                SqlDataAdapter ad = new SqlDataAdapter(str, koneksi);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    //col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                string hasil = "";
                SqlCommand cmd = new SqlCommand("select count(No_Reg) as totalWarga from warga " +
                    "where pekerjaan Like 'Buruh/Karyawan'", koneksi);
                cmd.CommandType = CommandType.Text;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    hasil = dr["totalWarga"].ToString();
                }
                dr.Close();
                jmlWarga.Text = hasil;
                koneksi.Close();
            }
            else if(fWarga.Text.Equals("Lainnya"))
            {
                str = "select No_KK as 'No Kartu Keluarga', Nama as 'Nama Warga' from warga " + " where pekerjaan != 'PNS' and " +
                "pekerjaan != 'Wirausaha' and pekerjaan != 'Pelajar/Mahasiswa' " +
                "and pekerjaan != 'Buruh/Karyawan'";
                SqlDataAdapter ad = new SqlDataAdapter(str, koneksi);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    //col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                string hasil = "";
                SqlCommand cmd = new SqlCommand("select count(No_Reg) as totalWarga from warga " +
                    "where pekerjaan != 'PNS' and " +
                    "pekerjaan != 'Wirausaha' and pekerjaan != 'Pelajar/Mahasiswa' " +
                    "and pekerjaan != 'Buruh/Karyawan'", koneksi);
                cmd.CommandType = CommandType.Text;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    hasil = dr["totalWarga"].ToString();
                }
                dr.Close();
                jmlWarga.Text = hasil;
                koneksi.Close();
            }
            else
            {
                dgv();
                jmlWrg();
                tCari.Enabled = true;
            }
            
        }
    }
}
