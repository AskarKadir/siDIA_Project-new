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
    public partial class CatatanKematian : Form
    {
        bool addstate = false;
        Koneksi kn = new Koneksi();
        public CatatanKematian()
        {
            InitializeComponent();
            dgv();
            jmlWrgM();
            defaultbuttonstate();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            clearForm();
            addstate = true;
            SqlConnection koneksi = new SqlConnection();
            koneksi.ConnectionString = kn.strKoneksi();
            koneksi.Open();
            SqlDataAdapter da = new SqlDataAdapter("SELECT No_KK as 'No Kartu Keluarga', nama as 'Nama Warga' from warga", koneksi);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.ClearSelection();
            koneksi.Close();

            cNama.Enabled = true;
            btnRefresh.Enabled = true;
            searchicon.Enabled = true;
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            tCari.Enabled = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (addstate == true)
            {
                clearForm();
                dgv();
            }
            else
            {
                string str = "";
                SqlConnection koneksi = new SqlConnection();
                koneksi.ConnectionString = kn.strKoneksi();
                koneksi.Open();

                string nma = cNama.Text;
                string nik = tNIK.Text;
                string nreg = tNoreg.Text;
                string sKK = tJKK.Text;
                string nKK = tKK.Text;
                string sRT = tJRT.Text;
                string idRT = tRT.Text;
                string tmpt = tTempat.Text;
                DateTime tglL = dateTimePicker1.Value.Date;
                string JK = tJK.Text;
                string agama = tAgama.Text;
                string alamat = tAlamat.Text;
                string didik = tdidik.Text;
                string kerja = tKerja.Text;
                string sKwin = tKawin.Text;
                string BPJS = tBPJS.Text;

                DialogResult dg;
                dg = MessageBox.Show("Apakah anda ingin menghapus data ini?", "Konfirmasi Hapus Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dg == DialogResult.Yes)
                {
                    str = "delete from kematian where nama = @Nm";
                    SqlCommand cmd = new SqlCommand(str, koneksi);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SqlParameter("Nm", nma));
                    cmd.ExecuteNonQuery();

                    string Istr = "insert into warga (no_reg,No_KK,NIK,Nama,status_dalam_keluarga,status_dalam_rumah_tangga, " +
                        "jenis_kelamin,alamat,tempat_lahir,tanggal_lahir,agama,pendidikan,pekerjaan,status_perkawinan, " +
                        "status_bpjs,id_rumah) values (@nreg,@nkk,@nik,@nma,@sk,@srt,@jk,@almt,@tl,@tgl,@ag,@pdk,@pkj," +
                        "@sp,@sbpjs,@idR)";
                    SqlCommand cm = new SqlCommand(Istr, koneksi);
                    cm.CommandType = CommandType.Text;

                    cm.Parameters.Add(new SqlParameter("nreg", nreg));
                    cm.Parameters.Add(new SqlParameter("nkk", nKK));
                    cm.Parameters.Add(new SqlParameter("nik", nik));
                    cm.Parameters.Add(new SqlParameter("nma", nma));
                    cm.Parameters.Add(new SqlParameter("sk", sKK));
                    cm.Parameters.Add(new SqlParameter("srt", sRT));
                    cm.Parameters.Add(new SqlParameter("jk", JK));
                    cm.Parameters.Add(new SqlParameter("almt", alamat));
                    cm.Parameters.Add(new SqlParameter("tl", tmpt));
                    cm.Parameters.Add(new SqlParameter("tgl", tglL));
                    cm.Parameters.Add(new SqlParameter("ag", agama));
                    cm.Parameters.Add(new SqlParameter("pdk", didik));
                    cm.Parameters.Add(new SqlParameter("pkj", kerja));
                    cm.Parameters.Add(new SqlParameter("sp", sKwin));
                    cm.Parameters.Add(new SqlParameter("sbpjs", BPJS));
                    cm.Parameters.Add(new SqlParameter("idR", idRT));

                    cm.ExecuteNonQuery();

                    koneksi.Close();
                    MessageBox.Show("Data Berhasil Dihapus", "Sukses", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                    clearForm();
                    dgv();
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            btnAdd.Enabled = false;
            btnAdd.Visible = false;
            btnSimpan.Enabled = true;
            btnSimpan.Visible = true;
            dtMati.Enabled = true;
            
            //    clearForm();
            //    dgv();
            //}
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            dgv();
            defaultbuttonstate();
            clearForm();
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                if (addstate == true)
                {
                    Console.WriteLine("selected cell add state");

                    string noKM = "";
                    string nik = tNIK.Text;
                    string nreg = tNoreg.Text;
                    string nama = cNama.Text;
                    string sKK = tJKK.Text;
                    string nKK = tKK.Text;
                    string sRT = tJRT.Text;
                    string idRT = tRT.Text;
                    string tmpt = tTempat.Text;
                    DateTime tglL = dateTimePicker1.Value.Date;
                    string JK = tJK.Text;
                    string agama = tAgama.Text;
                    string alamat = tAlamat.Text;
                    string didik = tdidik.Text;
                    string kerja = tKerja.Text;
                    string sKwin = tKawin.Text;
                    string BPJS = tBPJS.Text;
                    DateTime tglM = dtMati.Value.Date;

                    DialogResult dg;
                    dg = MessageBox.Show("Apakah data yang anda masukan sudah sesuai?", "Konfirmasi Tambah Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dg == DialogResult.Yes)
                    {
                        //cek nama dan nik apakah ada
                        int datafound = 0;
                        SqlConnection cekkoneksi = new SqlConnection();
                        cekkoneksi.ConnectionString = kn.strKoneksi();
                        cekkoneksi.Open();
                        SqlCommand cekcm = new SqlCommand("select nik,nama from warga where nik='" +
                            nik+
                            "' and " +
                            "nama='" +
                            nama +
                            "'", cekkoneksi);
                        SqlDataReader cekdr = cekcm.ExecuteReader();
                        while (cekdr.Read())
                        {
                            datafound = 1;
                        }
                        if (datafound == 1)
                        {
                            int hasil = 0;
                            SqlConnection koneksi = new SqlConnection();
                            koneksi.ConnectionString = kn.strKoneksi();
                            koneksi.Open();
                            SqlCommand cm = new SqlCommand("select count(No_Kematian) as 'JmlhM' from kematian", koneksi);
                            cm.CommandType = CommandType.Text;
                            SqlDataReader dr = cm.ExecuteReader();
                            while (dr.Read())
                            {
                                hasil = Convert.ToInt32(dr["JmlhM"]) + 1;
                            }
                            dr.Close();
                            if (hasil < 10)
                            {
                                noKM = "KM" + "00" + hasil;
                            }
                            else if (hasil >= 10)
                            {
                                noKM = "KM" + "0" + hasil;
                            }
                            else if (hasil > 99)
                            {
                                noKM = "KM" + hasil;
                            }

                            string str = "";

                            str = "insert into kematian (No_Kematian,no_Reg, No_KK,NIK,Nama,status_dalam_keluarga," +
                                "status_dalam_rumah_tangga, jenis_kelamin,alamat,tempat_lahir,tanggal_lahir,agama," +
                                "pendidikan,pekerjaan,status_perkawinan,status_bpjs,id_rumah,Tgl_Kematian) " +
                                "values (@nokm,@nreg,@nkk,@nik,@nma,@sk,@srt,@jk,@almt,@tl,@tglL,@ag,@pdk,@pkj," +
                                "@sp,@sbpjs,@idR,@tglM)";
                            SqlCommand cmd = new SqlCommand(str, koneksi);
                            cmd.CommandType = CommandType.Text;

                            cmd.Parameters.Add(new SqlParameter("nokm", noKM));
                            cmd.Parameters.Add(new SqlParameter("nreg", nreg));
                            cmd.Parameters.Add(new SqlParameter("nkk", nKK));
                            cmd.Parameters.Add(new SqlParameter("nik", nik));
                            cmd.Parameters.Add(new SqlParameter("nma", nama));
                            cmd.Parameters.Add(new SqlParameter("sk", sKK));
                            cmd.Parameters.Add(new SqlParameter("srt", sRT));
                            cmd.Parameters.Add(new SqlParameter("jk", JK));
                            cmd.Parameters.Add(new SqlParameter("almt", alamat));
                            cmd.Parameters.Add(new SqlParameter("tl", tmpt));
                            cmd.Parameters.Add(new SqlParameter("tglL", tglL));
                            cmd.Parameters.Add(new SqlParameter("ag", agama));
                            cmd.Parameters.Add(new SqlParameter("pdk", didik));
                            cmd.Parameters.Add(new SqlParameter("pkj", kerja));
                            cmd.Parameters.Add(new SqlParameter("sp", sKwin));
                            cmd.Parameters.Add(new SqlParameter("sbpjs", BPJS));
                            cmd.Parameters.Add(new SqlParameter("idR", idRT));
                            cmd.Parameters.Add(new SqlParameter("tglM", tglM));

                            cmd.ExecuteNonQuery();

                            string strd = "delete from warga where no_reg = @nreg";
                            SqlCommand scmd = new SqlCommand(strd, koneksi);
                            scmd.CommandType = CommandType.Text;
                            scmd.Parameters.Add(new SqlParameter("nreg", nreg));
                            scmd.ExecuteNonQuery();

                            koneksi.Close();
                            MessageBox.Show("Data Berhasil DiTambahkan", "Sukses", MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                            defaultbuttonstate();
                            clearForm();
                            dgv();
                            jmlWrgM();
                        }
                        else
                        {
                            MessageBox.Show("Kesalahan Input Data, Mohon Diperiksa Kembali","Tidak Dapat Menemukan Data",MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    string Nma = cNama.Text;
                    DateTime tglM = dtMati.Value.Date;

                    DialogResult dg;
                    dg = MessageBox.Show("Apakah anda ingin mengubah data ini?", "Konfirmasi Ubah Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dg == DialogResult.Yes)
                    {
                        string str = "";

                        SqlConnection koneksi = new SqlConnection();
                        koneksi.ConnectionString = kn.strKoneksi();
                        koneksi.Open();
                        str = "update kematian set Tgl_Kematian = @tgM where Nama = @nm";
                        SqlCommand cmd = new SqlCommand(str, koneksi);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@nm", Nma));
                        cmd.Parameters.Add(new SqlParameter("@tgM", tglM));

                        cmd.ExecuteNonQuery();
                        koneksi.Close();
                        MessageBox.Show("Data Berhasil Diperbaruhi", "Sukses", MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("Tidak Ada Data Yang Dipilih", "Pembertahuan", MessageBoxButtons.OK);
            }
            //defaultbuttonstate();

            
            //    clearForm();
            //    dgv();
            //    jmlWrgM();
            //}
        }

        private void clearForm()
        {
            cNama.Text = "";
            tNoreg.Text = "";
            tNIK.Text = "";
            tKK.Text = "";
            tJKK.Text = "";
            tJRT.Text = "";
            tRT.Text = "";
            tTempat.Text = "";
            tJK.Text = "";
            tAgama.Text = "";
            tAlamat.Text = "";
            tdidik.Text = "";
            tKerja.Text = "";
            tKawin.Text = "";
            tBPJS.Text = "";
        }

        //    dtMati.MaxDate = DateTime.Today;
        //    dtMati.Enabled=true;
        //}
        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;
            string nma = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[1].Value);

            string nik = "";
            string nreg = "";
            string nama = nma;
            string sKK = "";
            string nKK = "";
            string sRT = "";
            string idRT = "";
            string tmpt = "";
            DateTime tglL = dateTimePicker1.Value.Date;
            string JK = "";
            string agama = "";
            string alamat = "";
            string didik = "";
            string kerja = "";
            string sKwin = "";
            string bpjs = "";
            DateTime tglM = dtMati.Value.Date;

            SqlConnection koneksi = new SqlConnection();
            koneksi.ConnectionString = kn.strKoneksi();
            koneksi.Open();
            SqlDataAdapter da = new SqlDataAdapter("select nama from warga", koneksi);
            DataSet ds = new DataSet();
            da.Fill(ds);

            //cNama.DisplayMember = "nama";
            //cNama.ValueMember = "nama";
            //cNama.DataSource = ds.Tables[0];
            if (addstate == false)
            {
                SqlCommand cmd = new SqlCommand("select no_Reg, No_KK,NIK,status_dalam_keluarga," +
                    "status_dalam_rumah_tangga, jenis_kelamin,alamat,tempat_lahir,tanggal_lahir,agama," +
                    "pendidikan,pekerjaan,status_perkawinan,status_bpjs,id_rumah,Tgl_Kematian" +
                    " from kematian where nama = @Nm", koneksi);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("@Nm", nama));
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    nreg = dr["no_reg"].ToString();
                    nik = dr["NIK"].ToString();
                    sKK = dr["status_dalam_keluarga"].ToString();
                    nKK = dr["No_KK"].ToString();
                    sRT = dr["status_dalam_rumah_tangga"].ToString();
                    idRT = dr["id_rumah"].ToString();
                    tmpt = dr["tempat_lahir"].ToString();
                    tglL = Convert.ToDateTime(dr["tanggal_lahir"].ToString());
                    JK = dr["jenis_kelamin"].ToString();
                    agama = dr["agama"].ToString();
                    alamat = dr["alamat"].ToString();
                    didik = dr["pendidikan"].ToString();
                    kerja = dr["pekerjaan"].ToString();
                    sKwin = dr["status_perkawinan"].ToString();
                    bpjs = dr["status_bpjs"].ToString();
                    tglM = Convert.ToDateTime(dr["Tgl_Kematian"].ToString());

                }
                dr.Close();
                koneksi.Close();

                cNama.Text = nama;
                tNIK.Text = nik;
                tNoreg.Text = nreg;
                tKK.Text = nKK;
                tJKK.Text = sKK;
                tJRT.Text = sRT;
                tRT.Text = idRT;
                tTempat.Text = tmpt;
                dateTimePicker1.Value = tglL;
                tJK.Text = JK;
                tAgama.Text = agama;
                tAlamat.Text = alamat;
                tdidik.Text = didik;
                tKerja.Text = kerja;
                tKawin.Text = sKwin;
                tBPJS.Text = bpjs;
                dtMati.Value = tglM;

                btnDelete.Enabled = true;
                btnEdit.Enabled = true;
            }
            else
            {
                SqlCommand cmd = new SqlCommand("select no_Reg, No_KK,NIK,status_dalam_keluarga," +
                    "status_dalam_rumah_tangga, jenis_kelamin,alamat,tempat_lahir,tanggal_lahir,agama," +
                    "pendidikan,pekerjaan,status_perkawinan,status_bpjs,id_rumah" +
                    " from warga where nama = @Nm", koneksi);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("@Nm", nama));
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    nreg = dr["no_reg"].ToString();
                    nik = dr["NIK"].ToString();
                    sKK = dr["status_dalam_keluarga"].ToString();
                    nKK = dr["No_KK"].ToString();
                    sRT = dr["status_dalam_rumah_tangga"].ToString();
                    idRT = dr["id_rumah"].ToString();
                    tmpt = dr["tempat_lahir"].ToString();
                    tglL = Convert.ToDateTime(dr["tanggal_lahir"].ToString());
                    JK = dr["jenis_kelamin"].ToString();
                    agama = dr["agama"].ToString();
                    alamat = dr["alamat"].ToString();
                    didik = dr["pendidikan"].ToString();
                    kerja = dr["pekerjaan"].ToString();
                    sKwin = dr["status_perkawinan"].ToString();
                    bpjs = dr["status_bpjs"].ToString();

                }
                dr.Close();
                koneksi.Close();

                cNama.Text = nama;
                tNIK.Text = nik;
                tNoreg.Text = nreg;
                tKK.Text = nKK;
                tJKK.Text = sKK;
                tJRT.Text = sRT;
                tRT.Text = idRT;
                tTempat.Text = tmpt;
                dateTimePicker1.Value = tglL;
                tJK.Text = JK;
                tAgama.Text = agama;
                tAlamat.Text = alamat;
                tdidik.Text = didik;
                tKerja.Text = kerja;
                tKawin.Text = sKwin;
                tBPJS.Text = bpjs;
                dtMati.Value = tglM;

                btnAdd.Enabled = false;
                btnAdd.Visible = false;
                btnSimpan.Enabled = true;
                btnSimpan.Visible = true;
                dtMati.Enabled = true;
            }


        }

        private void defaultbuttonstate()
        {
            addstate = false;
            tCari.Enabled = true;
            cNama.Enabled = false;
            searchicon.Enabled = false;
            tNoreg.Enabled = false;
            tNIK.Enabled = false;
            tKK.Enabled = false;
            tJKK.Enabled = false;
            tJRT.Enabled = false;
            tRT.Enabled = false;
            tTempat.Enabled = false;
            dateTimePicker1.Enabled = false;
            dateTimePicker1.Value = DateTime.Now;
            tJK.Enabled = false;
            tAgama.Enabled = false;
            tAlamat.Enabled = false;
            tdidik.Enabled = false;
            tKerja.Enabled = false;
            tKawin.Enabled = false;
            tBPJS.Enabled = false;
            dtMati.Enabled = false;
            dtMati.Value = DateTime.Now;

            btnAdd.Visible = true;
            btnAdd.Enabled = true;
            btnDelete.Enabled = false;
            btnEdit.Enabled = false;
            btnRefresh.Enabled = true;

            btnSimpan.Visible = false;

            dataGridView1.Enabled = true;
            dataGridView1.ClearSelection();
        }

        private void dgv()
        {
            SqlConnection koneksi = new SqlConnection();
            koneksi.ConnectionString = kn.strKoneksi();
            koneksi.Open(); 
            string str = "select No_KK as 'No Kartu Keluarga', Nama as 'Nama Warga' from kematian";
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
            dataGridView1.ClearSelection();
        }

        private void jmlWrgM()
        {
            string hasil = "";
            SqlConnection koneksi = new SqlConnection();
            koneksi.ConnectionString = kn.strKoneksi();
            koneksi.Open();
            SqlCommand cmd = new SqlCommand("select count(No_Kematian) as totalWarga from kematian", koneksi);
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                hasil = dr["totalWarga"].ToString();
            }
            dr.Close();
            jmlWarga.Text = hasil;
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //private void cNama_TextChanged(object sender, EventArgs e)
        //{
        //    string nik = "";
        //    string nreg = "";
        //    string nama = cNama.Text;
        //    string sKK = "";
        //    string nKK = "";
        //    string sRT = "";
        //    string idRT = "";
        //    string tmpt = "";
        //    DateTime tglL = dateTimePicker1.Value.Date;
        //    string JK = "";
        //    string agama = "";
        //    string alamat = "";
        //    string didik = "";
        //    string kerja = "";
        //    string sKwin = "";

        //    SqlConnection koneksi = new SqlConnection();
        //    koneksi.ConnectionString = kn.strKoneksi();
        //    koneksi.Open();
        //    SqlCommand cmd = new SqlCommand("select no_reg,No_KK,NIK,status_dalam_keluarga,status_dalam_rumah_tangga, " +
        //        "jenis_kelamin,alamat,tempat_lahir,tanggal_lahir,agama,pendidikan,pekerjaan,status_perkawinan, " +
        //        "id_rumah from warga where nama = @nReg", koneksi);
        //    cmd.CommandType = CommandType.Text;
        //    cmd.Parameters.Add(new SqlParameter("@nReg", nama));
        //    SqlDataReader dr = cmd.ExecuteReader();
        //    while (dr.Read())
        //    {
        //        nreg = dr["no_reg"].ToString();
        //        nik = dr["NIK"].ToString();
        //        sKK = dr["status_dalam_keluarga"].ToString();
        //        nKK = dr["No_KK"].ToString();
        //        sRT = dr["status_dalam_rumah_tangga"].ToString();
        //        idRT = dr["id_rumah"].ToString();
        //        tmpt = dr["tempat_lahir"].ToString();
        //        tglL = Convert.ToDateTime( dr["tanggal_lahir"].ToString());
        //        JK = dr["jenis_kelamin"].ToString();
        //        agama = dr["agama"].ToString();
        //        alamat = dr["alamat"].ToString();
        //        didik = dr["pendidikan"].ToString();
        //        kerja = dr["pekerjaan"].ToString();
        //        sKwin = dr["status_perkawinan"].ToString();

        //    }
        //    dr.Close();

        private void searchicon_Click(object sender, EventArgs e)
        {
            if (cNama.Text == "")
            {
                SqlConnection koneksi = new SqlConnection();
                koneksi.ConnectionString = kn.strKoneksi();
                koneksi.Open();

                string str = "SELECT No_KK as 'No Kartu Keluarga',nama as 'Nama Warga' FROM warga";

                SqlDataAdapter adapter = new SqlDataAdapter(str, koneksi);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                koneksi.Close();
            }
            else
            {
                SqlConnection koneksi = new SqlConnection();
                koneksi.ConnectionString = kn.strKoneksi();
                koneksi.Open();

                string str = "SELECT No_KK as 'No Kartu Keluarga',nama as 'Nama Warga' FROM warga WHERE nama LIKE '" +
                    cNama.Text +
                    "%'";

                SqlDataAdapter adapter = new SqlDataAdapter(str, koneksi);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                koneksi.Close();
            }
        }

        //    tNIK.Text = nik;
        //    tNoreg.Text = nreg;
        //    tKK.Text = nKK;
        //    tJKK.Text = sKK;
        //    tJRT.Text = sRT;
        //    tRT.Text = idRT;
        //    tTempat.Text = tmpt;
        //    dateTimePicker1.Value = tglL;
        //    tJK.Text = JK;
        //    tAgama.Text = agama;
        //    tAlamat.Text = alamat;
        //    tdidik.Text = didik;
        //    tKerja.Text = kerja;
        //    tKawin.Text = sKwin;
        //    tBPJS.Text = "Tidak Aktif";
        private void tCari_TextChanged(object sender, EventArgs e)
        {
            SqlConnection koneksi = new SqlConnection();
            koneksi.ConnectionString = kn.strKoneksi();
            koneksi.Open();
            string str = "select NIK, Nama as 'Nama Warga', No_KK as 'No Kartu Keluarga', " +
                "jenis_kelamin as 'Jenis Kelamin', tempat_lahir as 'Tempat Lahir', " +
                "tanggal_lahir as 'Tgl/Bln/Th Lahir', Tgl_Kematian as 'Tanggal Meninggal', " +
                "CONVERT(int, YEAR(Tgl_Kematian),105) - Convert(int, year(tanggal_lahir), 105) as " +
                "'Usia saat Meninggal' from kematian where Nama Like '%" + tCari.Text + "%'";
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
    }
}
