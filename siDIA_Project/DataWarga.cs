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
        Koneksi kn = new Koneksi();
        public DataWarga()
        {
            InitializeComponent();
            dgv();
            jmlWrg();
            //this.WindowState = FormWindowState.Minimized;
            //this.FormBorderStyle = FormBorderStyle.FixedDialog;
        }

        private void dgv()
        {
            SqlConnection koneksi = new SqlConnection();
            koneksi.ConnectionString = kn.strKoneksi();
            koneksi.Open();
            string str = "select No_Reg as 'Nomer Registrasi', No_KK as 'No Kartu Keluarga', Nama as 'Nama Warga', jenis_kelamin as 'Jenis Kelamin'," +
                " tempat_lahir as 'Tempat Lahir', tanggal_lahir as 'Tgl/Bln/Th Lahir', " +
                "agama as Agama, pendidikan as Pendidikan, " +
                "pekerjaan as Pekerjaan, status_perkawinan as 'Status Perkawinan', status_bpjs as 'Status BPJS' from warga";
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
            int hasil = 0;
            SqlConnection koneksi = new SqlConnection();
            koneksi.ConnectionString = kn.strKoneksi();
            koneksi.Open();
            SqlCommand cmd = new SqlCommand("select count(No_Reg) as totalWarga from warga", koneksi);
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                hasil = Convert.ToInt32(dr["totalWarga"])+1;
            }
            dr.Close();
            if(hasil < 10)
            {
                Noreg.Text = "3404012001" + DateTime.Now.Year + "AMXII" + "00" + hasil;
            }
            else if (hasil >= 10)
            {
                Noreg.Text = "3404012001" + DateTime.Now.Year + "AMXII" + "0" + hasil;
            }
            else if (hasil > 99)
            {
                Noreg.Text = "3404012001" + DateTime.Now.Year + "AMXII" + hasil;
            }
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
            tNama.Enabled = true;
        }

        private void tNama_TextChanged(object sender, EventArgs e)
        {
            cJabatan.Enabled = true;
        }

        private void cJabatan_TextChanged(object sender, EventArgs e)
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
            cRT.Enabled = true;
            if(cRT.Text.Equals("Anggota Rumah Tangga"))
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

        private void tKK_TextChanged(object sender, EventArgs e)
        {
            cRT.Enabled = true;
        }

        private void cRT_TextChanged(object sender, EventArgs e)
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
            else
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
            tTempat.Enabled = true;
        }

        private void tRT_TextChanged(object sender, EventArgs e)
        {
            tTempat.Enabled = true;
        }

        private void tTempat_TextChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = true;
            dateTimePicker1.MaxDate = DateTime.Today;
        }

        private void dateTimePicker1_EnabledChanged(object sender, EventArgs e)
        {
            cJK.Enabled = true;
        }

        private void cJK_TextChanged(object sender, EventArgs e)
        {
            cAgama.Enabled = true;
        }

        private void cAgama_TextChanged(object sender, EventArgs e)
        {
            tAlamat.Enabled = true;
        }

        private void tAlamat_TextChanged(object sender, EventArgs e)
        {
            cPendidikan.Enabled = true;
        }

        private void cPendidikan_TextChanged(object sender, EventArgs e)
        {
            cPekerjaan.Enabled = true;
        }

        private void cPekerjaan_TextChanged(object sender, EventArgs e)
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

        private void tStatusKawin_TextChanged(object sender, EventArgs e)
        {
            tStatusBPJS.Enabled = true;
            btnSimpan.Enabled = true;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            clearForm();
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
            string didik =cPendidikan.Text;
            string kerja = cPekerjaan.Text;
            string krjLn = tPLain.Text;
            string sKwin = tStatusKawin.Text;
            string sBPJS = tStatusBPJS.Text;
            DialogResult dg;
            dg = MessageBox.Show("Apakah data yang anda masukan sudah sesuai?", "Konfirmasi Tambah Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if(dg == DialogResult.Yes)
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
            }
            
        }

        private void pictureBox3_Click(object sender, EventArgs e)
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
            dg = MessageBox.Show("Apakah anda ingin mengubah data ini?", "Konfirmasi Ubah Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if(dg == DialogResult.Yes)
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

            }
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string str = "";
            SqlConnection koneksi = new SqlConnection();
            koneksi.ConnectionString = kn.strKoneksi();
            koneksi.Open();

            string noreg = Noreg.Text;
            DialogResult dg;
            dg = MessageBox.Show("Apakah anda ingin mengubah data ini?", "Konfirmasi Ubah Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

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
            string noreg = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[0].Value);

            string nik = "";
            string nama = "";
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
            SqlCommand cmd = new SqlCommand("select No_KK,NIK,Nama,status_dalam_keluarga,status_dalam_rumah_tangga, " +
                "jenis_kelamin,alamat,tempat_lahir,tanggal_lahir,agama,pendidikan,pekerjaan,status_perkawinan, " +
                "status_bpjs,id_rumah from warga where no_reg = @nReg", koneksi);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(new SqlParameter("@nReg", noreg));
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
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
                }
                else
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
                    cPekerjaan.Text = "Lainnya";
                    tPLain.Text = kerja;
                    tStatusKawin.Text = sKwin;
                    tStatusBPJS.Text = sBPJS;
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
                }
                else
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
                    cPekerjaan.Text = "Lainnya";
                    tPLain.Text = kerja;
                    tStatusKawin.Text = sKwin;
                    tStatusBPJS.Text = sBPJS;
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
                }
                else
                {
                    Noreg.Text = noreg;
                    tNIK.Text = nik;
                    tNama.Text = nama;
                    cJabatan.Text = sKK;
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
                }
            }
            btnAdd.Enabled = false;
            btnRefresh.Enabled = true;
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;
            dataGridView1.Enabled = false;
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
                str= "select No_Reg as 'Nomer Registrasi', No_KK as 'No Kartu Keluarga'," +
                    "Nama as 'Nama Warga', jenis_kelamin as 'Jenis Kelamin'," +
                    "tempat_lahir as 'Tempat Lahir', tanggal_lahir as 'Tgl/Bln/Th Lahir'," +
                    "agama as Agama, pendidikan as Pendidikan,pekerjaan as Pekerjaan, " +
                    "status_perkawinan as 'Status Perkawinan',status_bpjs as 'Status BPJS'from warga " +
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
                str = "select No_Reg as 'Nomer Registrasi', No_KK as 'No Kartu Keluarga'," +
                    "Nama as 'Nama Warga', jenis_kelamin as 'Jenis Kelamin'," +
                    "tempat_lahir as 'Tempat Lahir', tanggal_lahir as 'Tgl/Bln/Th Lahir'," +
                    "agama as Agama, pendidikan as Pendidikan,pekerjaan as Pekerjaan, " +
                    "status_perkawinan as 'Status Perkawinan',status_bpjs as 'Status BPJS'from warga " +
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
                str = "select No_Reg as 'Nomer Registrasi', No_KK as 'No Kartu Keluarga'," +
                    "Nama as 'Nama Warga', jenis_kelamin as 'Jenis Kelamin'," +
                    "tempat_lahir as 'Tempat Lahir', tanggal_lahir as 'Tgl/Bln/Th Lahir'," +
                    "agama as Agama, pendidikan as Pendidikan,pekerjaan as Pekerjaan, " +
                    "status_perkawinan as 'Status Perkawinan',status_bpjs as 'Status BPJS'from warga " +
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
                str = "select No_Reg as 'Nomer Registrasi', No_KK as 'No Kartu Keluarga'," +
                    "Nama as 'Nama Warga', jenis_kelamin as 'Jenis Kelamin'," +
                    "tempat_lahir as 'Tempat Lahir', tanggal_lahir as 'Tgl/Bln/Th Lahir'," +
                    "agama as Agama, pendidikan as Pendidikan,pekerjaan as Pekerjaan, " +
                    "status_perkawinan as 'Status Perkawinan',status_bpjs as 'Status BPJS'from warga " +
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
                str = "select No_Reg as 'Nomer Registrasi', No_KK as 'No Kartu Keluarga', " +
                "Nama as 'Nama Warga', jenis_kelamin as 'Jenis Kelamin'," +
                " tempat_lahir as 'Tempat Lahir', tanggal_lahir as 'Tgl/Bln/Th Lahir', " +
                "agama as Agama, pendidikan as Pendidikan, " +
                "pekerjaan as Pekerjaan, status_perkawinan as 'Status Perkawinan', " +
                "status_bpjs as 'Status BPJS' from warga where pekerjaan Like 'PNS'";
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
                    "where where pekerjaan Like 'PNS'", koneksi);
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
                str = "select No_Reg as 'Nomer Registrasi', No_KK as 'No Kartu Keluarga', " +
                "Nama as 'Nama Warga', jenis_kelamin as 'Jenis Kelamin'," +
                " tempat_lahir as 'Tempat Lahir', tanggal_lahir as 'Tgl/Bln/Th Lahir', " +
                "agama as Agama, pendidikan as Pendidikan, " +
                "pekerjaan as Pekerjaan, status_perkawinan as 'Status Perkawinan', " +
                "status_bpjs as 'Status BPJS' from warga where pekerjaan Like 'Wirausaha'";
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
                    "where where pekerjaan Like 'Wirausaha'", koneksi);
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
                str = "select No_Reg as 'Nomer Registrasi', No_KK as 'No Kartu Keluarga', " +
                "Nama as 'Nama Warga', jenis_kelamin as 'Jenis Kelamin'," +
                " tempat_lahir as 'Tempat Lahir', tanggal_lahir as 'Tgl/Bln/Th Lahir', " +
                "agama as Agama, pendidikan as Pendidikan, " +
                "pekerjaan as Pekerjaan, status_perkawinan as 'Status Perkawinan', " +
                "status_bpjs as 'Status BPJS' from warga where pekerjaan Like 'Pelajar/Mahasiswa'";
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
                    "where where pekerjaan Like 'Pelajar/Mahasiswa'", koneksi);
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
                str = "select No_Reg as 'Nomer Registrasi', No_KK as 'No Kartu Keluarga', " +
                "Nama as 'Nama Warga', jenis_kelamin as 'Jenis Kelamin'," +
                " tempat_lahir as 'Tempat Lahir', tanggal_lahir as 'Tgl/Bln/Th Lahir', " +
                "agama as Agama, pendidikan as Pendidikan, " +
                "pekerjaan as Pekerjaan, status_perkawinan as 'Status Perkawinan', " +
                "status_bpjs as 'Status BPJS' from warga where pekerjaan Like 'Buruh/Karyawan'";
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
                str = "select No_Reg as 'Nomer Registrasi', No_KK as 'No Kartu Keluarga', " +
                "Nama as 'Nama Warga', jenis_kelamin as 'Jenis Kelamin'," +
                " tempat_lahir as 'Tempat Lahir', tanggal_lahir as 'Tgl/Bln/Th Lahir', " +
                "agama as Agama, pendidikan as Pendidikan, " +
                "pekerjaan as Pekerjaan, status_perkawinan as 'Status Perkawinan', " +
                "status_bpjs as 'Status BPJS' from warga where pekerjaan != 'PNS' and " +
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
                    "where where pekerjaan != 'PNS' and " +
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
