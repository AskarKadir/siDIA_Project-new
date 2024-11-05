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
            dtMati.MaxDate = DateTime.Now;
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
            SqlConnection koneksi = new SqlConnection();
            koneksi.ConnectionString = kn.strKoneksi();
            koneksi.Open();
            try
            {
                if (addstate == true)
                {
                    clearForm();
                    dgv();
                }
                else
                {
                    string str = "";

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
                            "jenis_kelamin,tempat_lahir,tanggal_lahir,agama,pendidikan,pekerjaan,status_perkawinan, " +
                            "status_bpjs,id_rumah) values (@nreg,@nkk,@nik,@nma,@sk,@srt,@jk,@tl,@tgl,@ag,@pdk,@pkj," +
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
                        //cm.Parameters.Add(new SqlParameter("almt", alamat));
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
                        jmlWrgM();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex);
            }
            finally
            {
                koneksi.Close();
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
            messageShown = false;
            jmlWrgM();
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
                    int datafound = 0;
                    SqlConnection cekkoneksi = new SqlConnection();
                    cekkoneksi.ConnectionString = kn.strKoneksi();
                    cekkoneksi.Open();

                    SqlTransaction transaction = cekkoneksi.BeginTransaction(); // Begin transaction

                    try
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
                            // Cek nama dan NIK apakah ada
                            SqlCommand cekcm = new SqlCommand("SELECT nik,nama FROM warga WHERE nik=@nik AND nama=@nama", cekkoneksi, transaction);
                            cekcm.Parameters.AddWithValue("@nik", nik);
                            cekcm.Parameters.AddWithValue("@nama", nama);
                            SqlDataReader cekdr = cekcm.ExecuteReader();

                            while (cekdr.Read())
                            {
                                datafound = 1;
                            }
                            cekdr.Close();

                            if (datafound == 1)
                            {
                                int maxNoKM = 0;
                                SqlCommand cm = new SqlCommand("SELECT MAX(CAST(SUBSTRING(No_Kematian, 3, LEN(No_Kematian) - 2) AS int)) AS 'MaxNoKM' FROM kematian", cekkoneksi, transaction);
                                SqlDataReader dr = cm.ExecuteReader();
                                if (dr.Read() && !dr.IsDBNull(0))
                                {
                                    maxNoKM = Convert.ToInt32(dr["MaxNoKM"]);
                                }
                                dr.Close();

                                noKM = "";

                                // Check if there are deleted entries with lower identifiers
                                for (int i = 1; i <= maxNoKM; i++)
                                {
                                    string potentialNoKM = "KM" + i.ToString().PadLeft(3, '0');
                                    SqlCommand checkCmd = new SqlCommand("SELECT COUNT(No_Kematian) FROM kematian WHERE No_Kematian = @No_Kematian", cekkoneksi, transaction);
                                    checkCmd.Parameters.AddWithValue("@No_Kematian", potentialNoKM);
                                    int count = (int)checkCmd.ExecuteScalar();
                                    if (count == 0)
                                    {
                                        noKM = potentialNoKM;
                                        break;
                                    }
                                }

                                if (noKM == "")
                                {
                                    int nextNoKM = maxNoKM + 1;
                                    noKM = "KM" + nextNoKM.ToString().PadLeft(3, '0');
                                }

                                string str = "INSERT INTO kematian (No_Kematian, no_Reg, No_KK, NIK, Nama, status_dalam_keluarga, status_dalam_rumah_tangga, jenis_kelamin, alamat, tempat_lahir, tanggal_lahir, agama, pendidikan, pekerjaan, status_perkawinan, status_bpjs, id_rumah, Tgl_Kematian) " +
                                             "VALUES (@nokm, @nreg, @nkk, @nik, @nma, @sk, @srt, @jk, @almt, @tl, @tglL, @ag, @pdk, @pkj, @sp, @sbpjs, @idR, @tglM)";

                                SqlCommand cmd = new SqlCommand(str, cekkoneksi, transaction);
                                cmd.Parameters.AddWithValue("@nokm", noKM);
                                cmd.Parameters.AddWithValue("@nreg", nreg);
                                cmd.Parameters.AddWithValue("@nkk", nKK);
                                cmd.Parameters.AddWithValue("@nik", nik);
                                cmd.Parameters.AddWithValue("@nma", nama);
                                cmd.Parameters.AddWithValue("@sk", sKK);
                                cmd.Parameters.AddWithValue("@srt", sRT);
                                cmd.Parameters.AddWithValue("@jk", JK);
                                cmd.Parameters.AddWithValue("@almt", alamat);
                                cmd.Parameters.AddWithValue("@tl", tmpt);
                                cmd.Parameters.AddWithValue("@tglL", tglL);
                                cmd.Parameters.AddWithValue("@ag", agama);
                                cmd.Parameters.AddWithValue("@pdk", didik);
                                cmd.Parameters.AddWithValue("@pkj", kerja);
                                cmd.Parameters.AddWithValue("@sp", sKwin);
                                cmd.Parameters.AddWithValue("@sbpjs", BPJS);
                                cmd.Parameters.AddWithValue("@idR", idRT);
                                cmd.Parameters.AddWithValue("@tglM", tglM);

                                cmd.ExecuteNonQuery();

                                // Delete from warga
                                string strd = "DELETE FROM warga WHERE no_reg = @nreg";
                                SqlCommand scmd = new SqlCommand(strd, cekkoneksi, transaction);
                                scmd.Parameters.AddWithValue("@nreg", nreg);
                                scmd.ExecuteNonQuery();

                                // Commit transaction
                                transaction.Commit();
                                MessageBox.Show("Data Berhasil Ditambahkan", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                defaultbuttonstate();
                                clearForm();
                                dgv();
                                jmlWrgM();
                            }
                            else
                            {
                                MessageBox.Show("Kesalahan Input Data, Mohon Diperiksa Kembali", "Tidak Dapat Menemukan Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Rollback transaction on error
                        transaction.Rollback();
                        MessageBox.Show("Gagal Menambahkan Data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        cekkoneksi.Close();
                    }

                }
                else
                {
                    SqlConnection koneksi = new SqlConnection();
                    koneksi.ConnectionString = kn.strKoneksi();
                    koneksi.Open();
                    SqlTransaction transaction = koneksi.BeginTransaction();

                    try
                    {
                        DateTime tglM = dtMati.Value.Date;

                        DialogResult dg = MessageBox.Show("Apakah anda ingin mengubah data ini?", "Konfirmasi Ubah Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (dg == DialogResult.Yes)
                        {
                            string str = "UPDATE kematian SET Tgl_Kematian = @tgM WHERE no_reg = @no_reg";
                            SqlCommand cmd = new SqlCommand(str, koneksi, transaction); // Tambahkan 'transaction' ke SqlCommand
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.Add(new SqlParameter("@no_reg", tNoreg.Text));
                            cmd.Parameters.Add(new SqlParameter("@tgM", tglM));

                            // Eksekusi perintah UPDATE
                            cmd.ExecuteNonQuery();

                            // Commit transaksi jika sukses
                            transaction.Commit();
                            MessageBox.Show("Data Berhasil Diperbarui", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnRefresh_Click(null, null);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Rollback transaksi jika terjadi kesalahan
                        transaction.Rollback();
                        MessageBox.Show("Gagal Mengubah Data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        // Pastikan koneksi selalu ditutup
                        koneksi.Close();
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
                    "status_dalam_rumah_tangga, jenis_kelamin,tempat_lahir,tanggal_lahir,agama," +
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
                    //alamat = dr["alamat"].ToString();
                    didik = dr["pendidikan"].ToString();
                    kerja = dr["pekerjaan"].ToString();
                    sKwin = dr["status_perkawinan"].ToString();
                    bpjs = dr["status_bpjs"].ToString();
                    tglM = Convert.ToDateTime(dr["Tgl_Kematian"].ToString());

                }
                dr.Close();

                SqlCommand cmdRumah = new SqlCommand("select alamat from rumah where id_rumah = @idRumah", koneksi);
                cmdRumah.CommandType = CommandType.Text;
                cmdRumah.Parameters.Add(new SqlParameter("@idRumah", idRT));
                SqlDataReader drRumah = cmdRumah.ExecuteReader();

                if (drRumah.Read())
                {
                    alamat = drRumah["alamat"].ToString();
                }
                drRumah.Close();

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
                    "status_dalam_rumah_tangga, jenis_kelamin,tempat_lahir,tanggal_lahir,agama," +
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
                    //alamat = dr["alamat"].ToString();
                    didik = dr["pendidikan"].ToString();
                    kerja = dr["pekerjaan"].ToString();
                    sKwin = dr["status_perkawinan"].ToString();
                    bpjs = dr["status_bpjs"].ToString();

                }
                dr.Close();

                SqlCommand cmdRumah = new SqlCommand("select alamat from rumah where id_rumah = @idRumah", koneksi);
                cmdRumah.CommandType = CommandType.Text;
                cmdRumah.Parameters.Add(new SqlParameter("@idRumah", idRT));
                SqlDataReader drRumah = cmdRumah.ExecuteReader();

                if (drRumah.Read())
                {
                    alamat = drRumah["alamat"].ToString();
                }
                drRumah.Close();

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
            dtMati.Value = dtMati.MaxDate;

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
        // btn tutup tampilan
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

        private bool messageShown;

        private void tCari_TextChanged(object sender, EventArgs e)
        {
            SqlConnection koneksi = new SqlConnection();
            koneksi.ConnectionString = kn.strKoneksi();
            koneksi.Open();
            string str = "select No_KK as 'No Kartu Keluarga', Nama as 'Nama Warga' from kematian where nama like @searchText";
            SqlDataAdapter ad = new SqlDataAdapter(str, koneksi);
            ad.SelectCommand.Parameters.AddWithValue("@searchText", tCari.Text + "%");
            DataSet ds = new DataSet();
            ad.Fill(ds);
            if (string.IsNullOrEmpty(tCari.Text))
            {
                str = "select No_KK as 'No Kartu Keluarga', Nama as 'Nama Warga' from kematian";
                ad = new SqlDataAdapter(str, koneksi);
                ds = new DataSet();
                ad.Fill(ds);
            }


            if (ds.Tables[0].Rows.Count == 0)
            {
                if (!messageShown)
                {
                    MessageBox.Show("Data Tidak Ditemukan", "Warning");
                    messageShown = true;
                    str = "select No_KK as 'No Kartu Keluarga', Nama as 'Nama Warga' from kematian";
                    ad = new SqlDataAdapter(str, koneksi);
                    ds = new DataSet();
                    ad.Fill(ds);
                }
            }
            else
            {
                dataGridView1.DataSource = ds.Tables[0];
                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    //col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                messageShown = false; // Reset the flag when data is found
            }

            koneksi.Close();
            //.Enabled = false;
        }

        static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var stringBuilder = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                stringBuilder.Append(chars[random.Next(chars.Length)]);
            }
            Console.WriteLine(stringBuilder.ToString());
            return stringBuilder.ToString();
        }
    }
}
