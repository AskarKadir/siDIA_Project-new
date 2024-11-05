using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Markup;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ComboBox = System.Windows.Forms.ComboBox;
using TextBox = System.Windows.Forms.TextBox;
using Timer = System.Windows.Forms.Timer;
using ToolTip = System.Windows.Forms.ToolTip;

namespace siDIA_Project
{
    public partial class DataWarga : Form
    {
        private Timer searchTimer;
        bool searchFound = false;
        bool isTextChangedHandlerAdded = false;
        bool addstate = false;
        bool editstate = false;
        bool keluargaValid = false;
        private bool messageShown = false;
        string no_registrasi;
        string nomor_induk_kependudukan;
        string nama;
        string status_dalam_keluarga;
        string no_kartu_keluarga;
        string status_dalam_rumah_tangga;
        string no_rumah;
        string tempat_lahir;
        string tanggal_lahir;
        string jenis_kelamin;
        string agama;
        string pendidikan;
        string pekerjaan;
        string status_kawin;
        string status_bpjs;
        string alamat;
        Koneksi kn = new Koneksi();
        public DataWarga()
        {
            InitializeComponent();
            // Inisialisasi timer
            searchTimer = new Timer();
            searchTimer.Interval = 1000; // 1 detik
            searchTimer.Tick += SearchTimer_Tick;
            ToolTip tool = new ToolTip();
            tool.SetToolTip(btnRefresh, "Refresh");
            tool.SetToolTip(btnSwitchNoRumah, "Tombol No Rumah");
            tool.SetToolTip(btnAdd, "Tambah Data");
            tool.SetToolTip(btnSimpan, "Simpan");
            tool.SetToolTip(btnEdit, "Ubah");
            tool.SetToolTip(btnDelete, "Hapus");
            DefaultStateButton();
            LoadDataTable();
            LoadTotalWargaTerdaftar();
            LoadDataNoKartuKeluarga();
            LoadDataNoRumah();
            SwitchReadOnlyTable();
            EnableVisibleControls();
            //Console.WriteLine("NO RUMAH BUG CNOREG: " + cNoReg.Text);
            //Console.WriteLine("NO RUMAH BUG TRT: " + tRT.Text);
            //this.WindowState = FormWindowState.Minimized;
            //this.FormBorderStyle = FormBorderStyle.FixedDialog;
        }

        private void OpenKoneksi()
        {
            SqlConnection koneksi = new SqlConnection();
            koneksi.ConnectionString = kn.strKoneksi();
            koneksi.Open();
        }

        // Load Data 
        private void LoadDataTable()
        {
            SqlConnection koneksi = new SqlConnection();
            koneksi.ConnectionString = kn.strKoneksi();
            koneksi.Open();
            try
            {
                string query = "select no_reg as 'Nomor Registrasi', Nama as 'Nama Warga', No_KK as 'No Kartu Keluarga' from warga ORDER BY no_reg DESC";
                SqlDataAdapter ad = new SqlDataAdapter(query, koneksi);
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
            catch (Exception ex)
            {
                MessageBox.Show("Gagal Mengambil Data Warga. " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                koneksi.Close();
            }
        }

        private void LoadTotalWargaTerdaftar()
        {
            string hasil = "";
            SqlConnection koneksi = new SqlConnection();
            koneksi.ConnectionString = kn.strKoneksi();
            koneksi.Open();
            try
            {
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
            catch (Exception ex)
            {
                MessageBox.Show("Gagal Mengambil Total Warga Terdaftar. " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                koneksi.Close();
            }
        }

        private void LoadDataNoKartuKeluarga()
        {
            SqlConnection koneksi = new SqlConnection(kn.strKoneksi());
            try
            {
                koneksi.Open();
                // Mengambil No_KK unik dari tabel warga
                SqlCommand cmd = new SqlCommand("SELECT DISTINCT No_KK FROM warga", koneksi);
                SqlDataReader reader = cmd.ExecuteReader();

                // Bersihkan ComboBox sebelum mengisi data baru
                cKK.Items.Clear();

                // Menambahkan data No_KK ke dalam ComboBox
                while (reader.Read())
                {
                    cKK.Items.Add(reader["No_KK"].ToString());
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat No Kartu Keluarga. " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                koneksi.Close();
            }
        }

        private void LoadDataNoRumah()
        {
            SqlConnection koneksi = new SqlConnection(kn.strKoneksi());
            try
            {
                koneksi.Open();
                // Mengambil semua id Rumah dari tabel rumah
                SqlCommand cmd = new SqlCommand("SELECT id_rumah FROM rumah", koneksi);
                SqlDataReader reader = cmd.ExecuteReader();

                // Bersihkan ComboBox sebelum mengisi data baru
                cNoReg.Items.Clear();

                // Menambahkan data id Rumah ke dalam ComboBox
                while (reader.Read())
                {
                    cNoReg.Items.Add(reader["id_rumah"].ToString());
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat No Rumah. " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                koneksi.Close();
            }
        }

        // Klik Data Pada Tabel
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            if (dataGridView1.ReadOnly)
            {
                return;
            }

            EnableStateButtonEdit();
            EnableStateButtonDelete();

            no_registrasi = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[0].Value); ;

            nama = "";
            no_kartu_keluarga = "";
            nomor_induk_kependudukan = "";
            status_dalam_keluarga = "";
            status_dalam_rumah_tangga = "";
            no_rumah = "";
            tempat_lahir= "";
            tanggal_lahir= "";
            jenis_kelamin= "";
            agama = "";
            pendidikan= "";
            pekerjaan= "";
            status_kawin= "";
            status_bpjs= "";
            alamat = ""; // Tambahkan variabel alamat

            // Console WriteLine sebelum try
            Console.WriteLine("\n========================= BATAS =========================\n");
            // Console WriteLine setelah try
            Console.WriteLine("Sebelum try harus berisi");
            Console.WriteLine("no_registrasi: " + no_registrasi);
            Console.WriteLine("nomor_induk_kependudukan: " + nomor_induk_kependudukan);
            Console.WriteLine("nama: " + nama);
            Console.WriteLine("no_kartu_keluarga: " + no_kartu_keluarga);
            Console.WriteLine("status_dalam_keluarga: " + status_dalam_keluarga);
            Console.WriteLine("status_dalam_rumah_tangga: " + status_dalam_rumah_tangga);
            Console.WriteLine("no_rumah: " + no_rumah);
            Console.WriteLine("tempat lahir: " + tempat_lahir);
            Console.WriteLine("tanggal lahir: " + tanggal_lahir);
            Console.WriteLine("jenis kelamin: " + jenis_kelamin);
            Console.WriteLine("alamat: " + alamat); // Cetak alamat
            Console.WriteLine("agama: " + agama);
            Console.WriteLine("pendidikan: " + pendidikan);
            Console.WriteLine("pekerjaan: " + pekerjaan);
            Console.WriteLine("status kawin: " + status_kawin);
            Console.WriteLine("status bpjs: " + status_bpjs);
            Console.WriteLine("========================= BATAS =========================\n");
            SqlConnection koneksi = new SqlConnection();
            koneksi.ConnectionString = kn.strKoneksi();
            koneksi.Open();

            try
            {
                SqlCommand cmd = new SqlCommand("SELECT w.no_reg, w.No_KK, w.NIK, w.Nama, w.status_dalam_keluarga, w.status_dalam_rumah_tangga, " +
                                "w.jenis_kelamin, w.tempat_lahir, w.tanggal_lahir, w.agama, w.pendidikan, w.pekerjaan, " +
                                "w.status_perkawinan, w.status_bpjs, w.id_rumah, r.alamat " +
                                "FROM warga w " +
                                "INNER JOIN rumah r ON w.id_rumah = r.id_rumah " +
                                "WHERE w.no_reg = @noreg", koneksi);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("@noreg", no_registrasi));
                //cmd.Parameters.Add(new SqlParameter("@nm", nama));
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    no_registrasi = dr["no_reg"].ToString();
                    nomor_induk_kependudukan = dr["NIK"].ToString();
                    nama = dr["Nama"].ToString();
                    status_dalam_keluarga = dr["status_dalam_keluarga"].ToString();
                    no_kartu_keluarga = dr["No_KK"].ToString();
                    Console.WriteLine("TEsting ajaa : " + dr["No_KK"].ToString());
                    status_dalam_rumah_tangga = dr["status_dalam_rumah_tangga"].ToString();
                    no_rumah = dr["id_rumah"].ToString();
                    tempat_lahir= dr["tempat_lahir"].ToString();
                    tanggal_lahir= dr["tanggal_lahir"].ToString();
                    jenis_kelamin= dr["jenis_kelamin"].ToString();
                    agama = dr["agama"].ToString();
                    pendidikan = dr["pendidikan"].ToString();
                    pekerjaan = dr["pekerjaan"].ToString();
                    status_kawin = dr["status_perkawinan"].ToString();
                    status_bpjs= dr["status_bpjs"].ToString();
                    alamat = dr["alamat"].ToString(); // Ambil data alamat dari tabel rumah
                }
                dr.Close();
            }
            catch (SqlException  ex) 
            {
                MessageBox.Show($"Terjadi Kesalahan Koneksi Ke Database. {ex.Message}"
                    , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi Kesalahan Dalam Mengambil Data Warga {nama}, No Registrasi {no_registrasi}. {ex.Message}"
                    , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                koneksi.Close();
            }

            // Menampilkan Data
            TampilkanDataClick();
            // Menampilkan Data



            // Console WriteLine setelah try
            Console.WriteLine("Setelah try harus berisi");
            Console.WriteLine("no_registrasi: " + no_registrasi);
            Console.WriteLine("nomor_induk_kependudukan: " + nomor_induk_kependudukan);
            Console.WriteLine("nama: " + nama);
            Console.WriteLine("no_kartu_keluarga: " + no_kartu_keluarga);
            Console.WriteLine("status_dalam_keluarga: " + status_dalam_keluarga);
            Console.WriteLine("status_dalam_rumah_tangga: " + status_dalam_rumah_tangga);
            Console.WriteLine("no_rumah: " + no_rumah);
            Console.WriteLine("tempat lahir: " + tempat_lahir);
            Console.WriteLine("tanggal lahir: " + tanggal_lahir);
            Console.WriteLine("jenis kelamin: " + jenis_kelamin);
            Console.WriteLine("alamat: " + alamat); // Cetak alamat
            Console.WriteLine("agama: " + agama);
            Console.WriteLine("pendidikan: " + pendidikan);
            Console.WriteLine("pekerjaan: " + pekerjaan);
            Console.WriteLine("status kawin: " + status_kawin);
            Console.WriteLine("status bpjs: " + status_bpjs);
            Console.WriteLine("\n========================= BATAS =========================\n");
        }

        private void TampilkanDataClick()
        {
            Console.WriteLine("NO KK TAMPIL DATA : " + no_kartu_keluarga);
            try
            {
                Noreg.Text = no_registrasi;
                tNIK.Text = nomor_induk_kependudukan;
                tNama.Text = nama;
                cJabatan.Text = status_dalam_keluarga;
                SwitchComboBoxTextBoxNoKartuKeluarga();
                cRT.Text = status_dalam_rumah_tangga;
                SwitchComboBoxTextBoxNoRumah();
                tTempat.Text = tempat_lahir;
                // Tanggal Lahir
                dateTimePicker1.Text = tanggal_lahir;
                // Tanggal Lahir
                cJK.Text = jenis_kelamin;
                tAlamat.Text = alamat;
                cAgama.Text = agama;
                cPendidikan.Text = pendidikan;
                if (!cPekerjaan.Items.Contains(pekerjaan))
                {
                    cPekerjaan.Text = "Lainnya";
                    tPLain.Visible = true;
                    tPLain.Text = pekerjaan;
                }
                else
                {
                    tPLain.Visible = false;
                    cPekerjaan.Text = pekerjaan;
                }
                tStatusKawin.Text = status_kawin;
                tStatusBPJS.Text = status_bpjs;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi Kesalahan Menampilkan Data. {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Form Control
        private void EnableAddForm()
        {
            tNIK.Enabled = true;
            tNama.Enabled = true;
            cJabatan.Enabled = true;
            //cKK.Enabled = true;
            //tKK.Enabled = true;
            //cNoReg.Enabled = true;
            //tRT.Enabled= true;
            //if (cJabatan.Text != "Kepala Keluarga")
            //{
            //    cKK.Enabled = true;
            //}
            //else
            //{
            //    tKK.Enabled = true;
            //}
            cRT.Enabled = true;
            //if (!string.IsNullOrEmpty(cNoReg.Text))
            //{
            //    cNoReg.Enabled = true;
            //}
            //else
            //{
            //    tRT.Enabled = true;
            //}
            tTempat.Enabled = true;
            dateTimePicker1.Enabled = true;
            cJK.Enabled = true;
            cAgama.Enabled = true;
            tAlamat.Enabled = true;
            cPendidikan.Enabled = true;
            cPekerjaan.Enabled = true;
            tPLain.Enabled = true;
            tStatusKawin.Enabled = true;
            tStatusBPJS.Enabled = true;
        }
        private void EnableAllFormEdit()
        {
            tNIK.Enabled = true ;
            tNama.Enabled = true ;
            cJabatan.Enabled = true ;
            // tambahan sementara
            //cKK.Visible = true;
            //tKK.Visible = true;
            if (!string.IsNullOrEmpty(cKK.Text))
            {
                cKK.Enabled = true;
            }
            else
            {
                tKK.Enabled = true;
            }
            cRT.Enabled = true;
            // tambahan sementara
            //cNoReg.Visible = true;
            //tRT.Visible = true;
            if (!string.IsNullOrEmpty(cNoReg.Text))
            {
                cNoReg.Enabled = true;
            }
            else
            {
                tRT.Enabled = true;
            }
            tTempat.Enabled = true;
            dateTimePicker1.Enabled = true;
            cJK.Enabled = true;
            cAgama.Enabled = true;
            if (cJabatan.SelectedIndex == 0 && cRT.SelectedIndex == 0)
            {
                tAlamat.Enabled = true;
            }
            else
            {
                tAlamat.Enabled = false;
            }
            cPendidikan.Enabled = true;
            cPekerjaan.Enabled = true;
            tPLain.Enabled = true;
            tStatusKawin.Enabled = true;
            tStatusBPJS.Enabled = true;
        }

        private void DisableAllForm()
        {
            // Optionally, disable the controls after clearing (depending on your needs)
            tNIK.Enabled = false;
            tNama.Enabled = false;
            cJabatan.Enabled = false;
            cKK.Enabled = false;
            tKK.Enabled = false;
            cRT.Enabled = false;
            tRT.Enabled = false;
            cNoReg.Enabled = false;
            tTempat.Enabled = false;
            dateTimePicker1.Enabled = false;
            cJK.Enabled = false;
            cAgama.Enabled = false;
            tAlamat.Enabled = false;
            cPendidikan.Enabled = false;
            cPekerjaan.Enabled = false;
            tPLain.Enabled = false;
            tStatusKawin.Enabled = false;
            tStatusBPJS.Enabled = false;
        }

        private void EnableVisibleControls()
        {
            // Cek setiap kontrol di form
            tNIK.Visible = true;
            tNama.Visible = true;
            cJabatan.Visible = true;
            cKK.Visible = true;
            tKK.Visible = true;
            cRT.Visible = true;
            cNoReg.Visible = true;
            tRT.Visible = true;
            tTempat.Visible = true;
            dateTimePicker1.Visible = true;
            cJK.Visible = true;
            cAgama.Visible = true;
            tAlamat.Visible = true;
            cPendidikan.Visible = true;
            cPekerjaan.Visible = true;
            //if (!tPLain.Visible) tPLain.Visible = true;
            tStatusKawin.Visible = true;
            tStatusBPJS.Visible = true;
        }


        private void ClearForm()
        {
            // Clear all TextBox controls
            Noreg.Text = string.Empty;
            tNIK.Text = string.Empty;
            tNama.Text = string.Empty;
            tKK.Text = string.Empty;
            tRT.Text = string.Empty;
            tTempat.Text = string.Empty;
            tAlamat.Text = string.Empty;
            tPLain.Text = string.Empty;

            // Reset ComboBox selections
            tStatusKawin.SelectedIndex = -1;
            tStatusBPJS.SelectedIndex = -1;
            cJabatan.SelectedIndex = -1;
            cKK.SelectedIndex = -1;
            cRT.SelectedIndex = -1;
            cNoReg.SelectedIndex = -1;
            cJK.SelectedIndex = -1;
            cAgama.SelectedIndex = -1;
            cPendidikan.SelectedIndex = -1;
            cPekerjaan.SelectedIndex = -1;

            // Reset DateTimePicker to current date or a default value
            dateTimePicker1.Value = DateTime.Now;
        }

        // CTRL + C Tabel / Salin Data Tabel
        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            // Check if the user presses Ctrl + C
            if (e.Control && e.KeyCode == Keys.C)
            {
                CopySelectedCellsToClipboard();
                e.Handled = true; // Prevent further handling of the key press event
            }
        }

        private void CopySelectedCellsToClipboard()
        {
            // Check if any cells are selected
            if (dataGridView1.GetCellCount(DataGridViewElementStates.Selected) > 0)
            {
                try
                {
                    // Copy the selected cells to the clipboard as text
                    Clipboard.SetText(dataGridView1.GetClipboardContent().GetText());
                }
                catch (ExternalException ex)
                {
                    MessageBox.Show("Terdapat Kesalahan Salin Data : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Klik Tombol / Button

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if(editstate == false)
            {
                keluargaValid = true;
                dataGridView1.ReadOnly = true;
                SwitchEditState();
                EnableAllFormEdit();
                SwitchSaveButton();
            }
            if (btnDelete.Enabled == true)
            {
                btnDelete.Enabled = false;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDataTable();
            tCari.Text = null;
            fWarga.SelectedIndex = 0;
            // Switch Edit
            if (btnEdit.Enabled)
            {
                btnEdit.Enabled = false;
            }

            // Reset form
            DefaultStateButton();
            SwitchResetState();
            ClearForm();
            DisableAllForm();
            SwitchSaveButton();
            EnableVisibleControls();
            dataGridView1.ReadOnly = false;

            keluargaValid = false;
            // Nonaktifkan sementara event handler TextChanged pada tNama
            tNama.TextChanged -= tNama_TextChanged;

            // Nonaktifkan sementara event handler SelectedIndexChanged
            cJabatan.SelectedIndexChanged -= cJabatan_SelectedIndexChanged;
            cRT.SelectedIndexChanged -= cRT_SelectedIndexChanged;

            // Aktifkan kembali event handler SelectedIndexChanged
            cRT.SelectedIndexChanged += cRT_SelectedIndexChanged;
            cJabatan.SelectedIndexChanged += cJabatan_SelectedIndexChanged;

            // Aktifkan kembali event handler TextChanged pada tNama
            tNama.TextChanged += tNama_TextChanged;
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {

            Console.WriteLine($"add ({addstate}) , edit ({editstate})");

            // Memanggil event handler tKK_Validating secara manual
            CancelEventArgs cancelEventArgs = new CancelEventArgs();

            // Mengambil nilai dari kontrol input
            no_registrasi = Noreg.Text;
            nomor_induk_kependudukan = tNIK.Text;
            nama = tNama.Text;
            status_dalam_keluarga = cJabatan.SelectedItem?.ToString();
            no_kartu_keluarga = tKK.Text;

            if (string.IsNullOrEmpty(no_kartu_keluarga))
            {
                no_kartu_keluarga = cKK.SelectedItem?.ToString();
            }

            status_dalam_rumah_tangga = cRT.SelectedItem?.ToString();
            no_rumah = tRT.Text;

            if (string.IsNullOrEmpty(no_rumah))
            {
                no_rumah = cNoReg.SelectedItem?.ToString();
            }

            tempat_lahir = tTempat.Text;
            tanggal_lahir = dateTimePicker1.Value.ToString();
            jenis_kelamin = cJK.SelectedItem?.ToString();
            agama = cAgama.SelectedItem?.ToString();
            alamat = tAlamat.Text;
            pendidikan = cPendidikan.SelectedItem?.ToString();
            pekerjaan = cPekerjaan.SelectedItem?.ToString();

            if (cPekerjaan.SelectedIndex == 4)
            {
                pekerjaan = tPLain.Text;
            }

            status_kawin = tStatusKawin.SelectedItem?.ToString();
            status_bpjs = tStatusBPJS.SelectedItem?.ToString();

            // Validasi setiap kolom
            if (string.IsNullOrEmpty(no_registrasi))
            {
                MessageBox.Show("No Registrasi harus diisi!", "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Noreg.Focus();
                return;
            }

            if (string.IsNullOrEmpty(nomor_induk_kependudukan))
            {
                MessageBox.Show("Nomor Induk Kependudukan harus diisi!", "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tNIK.Focus();
                return;
            }

            if (string.IsNullOrEmpty(nama))
            {
                MessageBox.Show("Nama harus diisi!", "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tNama.Focus();
                return;
            }

            if (keluargaValid == false)
            {
                if (string.IsNullOrEmpty(status_dalam_keluarga))
                {
                    MessageBox.Show("Status dalam keluarga harus dipilih!", "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cJabatan.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(no_kartu_keluarga))
                {
                    MessageBox.Show("No Kartu Keluarga harus diisi atau dipilih!", "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if (cKK.SelectedIndex == -1)
                    {
                        cKK.Focus();
                    }
                    tKK.Focus();
                    return;
                }

                if (no_kartu_keluarga.Length < 16)
                {
                    MessageBox.Show("No Kartu Keluarga harus 16 Angka!", "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if (cKK.SelectedIndex == -1)
                    {
                        cKK.Focus();
                    }
                    tKK.Focus();
                    return;
                }

                MessageBox.Show("Data Keluarga Belum Valid.", "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(status_dalam_rumah_tangga))
            {
                MessageBox.Show("Status dalam rumah tangga harus dipilih!", "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cRT.Focus();
                return;
            }

            if (string.IsNullOrEmpty(no_rumah))
            {
                MessageBox.Show("No Rumah harus diisi atau dipilih!", "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (cNoReg.SelectedIndex == -1)
                {
                    cNoReg.Focus();
                }
                tRT.Focus();
                return;
            }

            if (CekKepalaRumahTangga(no_rumah))
            {
                return;
            }

            if (string.IsNullOrEmpty(tempat_lahir))
            {
                MessageBox.Show("Tempat Lahir harus diisi!", "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tTempat.Focus();
                return;
            }

            if (string.IsNullOrEmpty(jenis_kelamin))
            {
                MessageBox.Show("Jenis Kelamin harus dipilih!", "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cJK.Focus();
                return;
            }

            if (string.IsNullOrEmpty(agama))
            {
                MessageBox.Show("Agama harus dipilih!", "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cAgama.Focus();
                return;
            }

            if (string.IsNullOrEmpty(alamat))
            {
                MessageBox.Show("Alamat harus diisi!", "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tAlamat.Focus();
                return;
            }

            if (string.IsNullOrEmpty(pendidikan))
            {
                MessageBox.Show("Pendidikan harus dipilih!", "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cPendidikan.Focus();
                return;
            }

            if (string.IsNullOrEmpty(pekerjaan))
            {
                MessageBox.Show("Pekerjaan harus diisi!", "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (cPekerjaan.SelectedIndex == 4)
                {
                    tPLain.Focus();
                }
                else
                {
                    cPekerjaan.Focus();
                }
                return;
            }

            if (string.IsNullOrEmpty(status_kawin))
            {
                MessageBox.Show("Status kawin harus dipilih!", "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tStatusKawin.Focus();
                return;
            }

            if (string.IsNullOrEmpty(status_bpjs))
            {
                MessageBox.Show("Status BPJS harus dipilih!", "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tStatusBPJS.Focus();
                return;
            }

            // Jika semua validasi lulus, proses penyimpanan dapat dilanjutkan
            {
                if (addstate == true)
                {
                    DialogResult dialog = MessageBox.Show("Apakah Data Sudah Sesuai?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialog == DialogResult.Yes)
                    {
                        using (SqlConnection connection = new SqlConnection(kn.strKoneksi()))
                        {
                            connection.Open();
                            SqlTransaction sqlTransaction = connection.BeginTransaction();
                            try
                            {
                                if ((cJabatan.SelectedIndex == 0 && !string.IsNullOrEmpty(tKK.Text)) && (cRT.SelectedIndex == 0 && !string.IsNullOrEmpty(tRT.Text)))
                                {
                                    insertDataRumah(sqlTransaction);
                                }
                                insertDataWarga(sqlTransaction);

                                // Commit transaksi jika berhasil
                                sqlTransaction.Commit();
                                MessageBox.Show("Data berhasil disimpan.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                btnRefresh_Click(null,null);
                            }
                            catch (Exception ex)
                            {
                                // Rollback transaksi jika terjadi kesalahan
                                sqlTransaction.Rollback();
                                MessageBox.Show("Terjadi kesalahan: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                else if (editstate == true)
                {
                    DialogResult dialog = MessageBox.Show("Apakah Data Sudah Sesuai?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialog == DialogResult.Yes)
                    {
                        using (SqlConnection connection = new SqlConnection(kn.strKoneksi()))
                        {
                            connection.Open();
                            SqlTransaction sqlTransaction = connection.BeginTransaction();
                            try
                            {
                                if ((cJabatan.SelectedIndex == 0 && !string.IsNullOrEmpty(tKK.Text)) && (cRT.SelectedIndex == 0 && !string.IsNullOrEmpty(tRT.Text)))
                                {
                                    if (isIdRumahExist(no_rumah))
                                    {
                                        updateDataRumah(sqlTransaction);
                                    }
                                    else
                                    {
                                        insertDataRumah(sqlTransaction);
                                    }
                                }
                                updateDataWarga(sqlTransaction);

                                // Commit transaksi jika berhasil
                                sqlTransaction.Commit();
                                MessageBox.Show("Data berhasil disimpan.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                btnRefresh_Click(null, null);
                            }
                            catch (Exception ex)
                            {
                                // Rollback transaksi jika terjadi kesalahan
                                sqlTransaction.Rollback();
                                MessageBox.Show("Terjadi kesalahan: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
        }

        private bool isIdRumahExist(string idRumah)
        {
            bool exists = false;

            try
            {
                using (SqlConnection koneksi = new SqlConnection(kn.strKoneksi()))
                {
                    koneksi.Open();

                    SqlCommand queryCheckIdRumah = new SqlCommand("SELECT COUNT(1) FROM rumah WHERE id_rumah = @idRumah", koneksi);
                    queryCheckIdRumah.Parameters.AddWithValue("@idRumah", idRumah);

                    int count = (int)queryCheckIdRumah.ExecuteScalar();

                    exists = (count > 0);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return exists;
        }

        private void insertDataRumah(SqlTransaction sqlTransaction)
        {
            {
                string queryWarga = "INSERT INTO rumah (id_rumah, alamat) VALUES (@no_rumah, @alamat)";

                using (SqlCommand cmd = new SqlCommand(queryWarga, sqlTransaction.Connection, sqlTransaction))
                {
                    cmd.Parameters.AddWithValue("@no_rumah", no_rumah);
                    cmd.Parameters.AddWithValue("@alamat", alamat);

                    cmd.ExecuteNonQuery();
                }
            }

        }
        
        private void updateDataRumah(SqlTransaction sqlTransaction)
        {
            {
                string queryWarga = "update rumah set alamat = @alamat where id_rumah = @no_rumah";

                using (SqlCommand cmd = new SqlCommand(queryWarga, sqlTransaction.Connection, sqlTransaction))
                {
                    cmd.Parameters.AddWithValue("@no_rumah", no_rumah);
                    cmd.Parameters.AddWithValue("@alamat", alamat);

                    cmd.ExecuteNonQuery();
                }
            }

        }

        private void updateDataWarga(SqlTransaction sqlTransaction)
        {
            Console.WriteLine("\n========================= COBA SIMPAN UPDATE =========================\n");
            Console.WriteLine("No Registrasi :" + no_registrasi);
            Console.WriteLine("nomor_induk_kependudukan: " + nomor_induk_kependudukan);
            Console.WriteLine("nama : " + nama);
            Console.WriteLine("status_dalam_keluarga : " + status_dalam_keluarga);
            Console.WriteLine("no_kartu_keluarga : " + no_kartu_keluarga);
            Console.WriteLine("status_dalam_rumah_tangga : " + status_dalam_rumah_tangga);
            Console.WriteLine("no_rumah : " + no_rumah);
            Console.WriteLine("tempat lahir : " + tempat_lahir);
            Console.WriteLine("tanggal lahir : " + tanggal_lahir);
            Console.WriteLine("jenis kelamin : " + jenis_kelamin);
            Console.WriteLine("agama : " + agama);
            Console.WriteLine("alamat : " + alamat);
            Console.WriteLine("pendidikan : " + pendidikan);
            Console.WriteLine("pekerjaan : " + pekerjaan);
            Console.WriteLine("status kawin : " + status_kawin);
            Console.WriteLine("status bpjs : " + status_bpjs);
            Console.WriteLine("========================= BATAS =========================\n");

            string queryWarga = "UPDATE warga SET No_KK = @no_kartu_keluarga, NIK = @nomor_induk_kependudukan, Nama = @nama, " +
            "status_dalam_keluarga = @status_dalam_keluarga, status_dalam_rumah_tangga = @status_dalam_rumah_tangga, " +
            "jenis_kelamin = @jenis_kelamin, tempat_lahir = @tempat_lahir, tanggal_lahir = @tanggal_lahir, agama = @agama, " +
            "pendidikan = @pendidikan, pekerjaan = @pekerjaan, status_perkawinan = @status_perkawinan, " +
            "status_bpjs = @status_bpjs, id_rumah = @no_rumah " +
            "WHERE no_reg = @no_registrasi";

            using (SqlCommand cmd = new SqlCommand(queryWarga, sqlTransaction.Connection, sqlTransaction))
            {
                cmd.Parameters.AddWithValue("@no_registrasi", no_registrasi.Trim());
                cmd.Parameters.AddWithValue("@nomor_induk_kependudukan", nomor_induk_kependudukan.Trim());
                cmd.Parameters.AddWithValue("@nama", nama.Trim());
                cmd.Parameters.AddWithValue("@status_dalam_keluarga", status_dalam_keluarga.Trim());
                cmd.Parameters.AddWithValue("@no_kartu_keluarga", no_kartu_keluarga.Trim());
                cmd.Parameters.AddWithValue("@status_dalam_rumah_tangga", status_dalam_rumah_tangga.Trim());
                cmd.Parameters.AddWithValue("@jenis_kelamin", jenis_kelamin.Trim());
                cmd.Parameters.AddWithValue("@tempat_lahir", tempat_lahir.Trim());
                cmd.Parameters.AddWithValue("@tanggal_lahir", Convert.ToDateTime(tanggal_lahir));
                cmd.Parameters.AddWithValue("@agama", agama.Trim());
                cmd.Parameters.AddWithValue("@pendidikan", pendidikan.Trim());
                cmd.Parameters.AddWithValue("@pekerjaan", pekerjaan.Trim());
                cmd.Parameters.AddWithValue("@status_perkawinan", status_kawin.Trim());
                cmd.Parameters.AddWithValue("@status_bpjs", status_bpjs.Trim());
                cmd.Parameters.AddWithValue("@no_rumah", no_rumah.Trim());

                cmd.ExecuteNonQuery();
            }
        }

        private void insertDataWarga(SqlTransaction sqlTransaction)
        {

            Console.WriteLine("\n========================= COBA SIMPAN INSERT =========================\n");
            Console.WriteLine("No Registrasi :" + no_registrasi);
            Console.WriteLine("nomor_induk_kependudukan: " + nomor_induk_kependudukan);
            Console.WriteLine("nama : " + nama);
            Console.WriteLine("status_dalam_keluarga : " + status_dalam_keluarga);
            Console.WriteLine("no_kartu_keluarga : " + no_kartu_keluarga);
            Console.WriteLine("status_dalam_rumah_tangga : " + status_dalam_rumah_tangga);
            Console.WriteLine("no_rumah : " + no_rumah);
            Console.WriteLine("tempat lahir : " + tempat_lahir);
            Console.WriteLine("tanggal lahir : " + Convert.ToDateTime(tanggal_lahir));
            Console.WriteLine("jenis kelamin : " + jenis_kelamin);
            Console.WriteLine("agama : " + agama);
            Console.WriteLine("alamat : " + alamat);
            Console.WriteLine("pendidikan : " + pendidikan);
            Console.WriteLine("pekerjaan : " + pekerjaan);
            Console.WriteLine("status kawin : " + status_kawin);
            Console.WriteLine("status bpjs : " + status_bpjs);
            Console.WriteLine("========================= BATAS =========================\n");
            string queryWarga = "INSERT INTO warga (no_reg, No_KK, NIK, Nama, status_dalam_keluarga, status_dalam_rumah_tangga, " +
                        "jenis_kelamin, tempat_lahir, tanggal_lahir, agama, pendidikan, pekerjaan, status_perkawinan, " +
                        "status_bpjs, id_rumah) VALUES (@no_registrasi, @no_kartu_keluarga, @nomor_induk_kependudukan, @nama, @status_dalam_keluarga, " +
                        "@status_dalam_rumah_tangga, @jenis_kelamin, @tempat_lahir, @tanggal_lahir, " +
                        "@agama, @pendidikan, @pekerjaan, @status_perkawinan, @status_bpjs, @no_rumah)";

            using (SqlCommand cmd = new SqlCommand(queryWarga, sqlTransaction.Connection, sqlTransaction))
            {
                cmd.Parameters.AddWithValue("@no_registrasi", no_registrasi.Trim());
                cmd.Parameters.AddWithValue("@nomor_induk_kependudukan", nomor_induk_kependudukan.Trim());
                cmd.Parameters.AddWithValue("@nama", nama.Trim());
                cmd.Parameters.AddWithValue("@status_dalam_keluarga", status_dalam_keluarga.Trim());
                cmd.Parameters.AddWithValue("@no_kartu_keluarga", no_kartu_keluarga.Trim());
                cmd.Parameters.AddWithValue("@status_dalam_rumah_tangga", status_dalam_rumah_tangga.Trim());
                cmd.Parameters.AddWithValue("@jenis_kelamin", jenis_kelamin.Trim());
                cmd.Parameters.AddWithValue("@tempat_lahir", tempat_lahir.Trim());
                cmd.Parameters.AddWithValue("@tanggal_lahir", Convert.ToDateTime(tanggal_lahir));
                cmd.Parameters.AddWithValue("@agama", agama.Trim());
                cmd.Parameters.AddWithValue("@pendidikan", pendidikan.Trim());
                cmd.Parameters.AddWithValue("@pekerjaan", pekerjaan.Trim());
                cmd.Parameters.AddWithValue("@status_perkawinan", status_kawin.Trim());
                cmd.Parameters.AddWithValue("@status_bpjs", status_bpjs.Trim());
                cmd.Parameters.AddWithValue("@no_rumah", no_rumah.Trim());

                cmd.ExecuteNonQuery();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ClearForm();
            SwitchAddState();
            EnableVisibleControls();
            SwitchSaveButton();
            SwitchReadOnlyTable();
            EnableAddForm();
            GenerateNoRegistrasi();
            if (btnDelete.Enabled == true)
            {
                btnDelete.Enabled = false;
            }
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            // Close Menu Data Warga
            this.Close();
        }

        // Ganti State 
        private void DefaultStateButton()
        {
            tNIK.Enabled = false;
            tNama.Enabled = false;

            // Bagian No Kartu Keluarga
            cKK.Enabled = false;
            tKK.Enabled = false;
            // Bagian No Kartu Keluarga

            // Bagian Jabatan di Keluarga
            cJabatan.Enabled = false;
            // Bagian Jabatan di Keluarga

            // Bagian Jabatan di Rumah Tangga
            cRT.Enabled = false;
            // Bagian Jabatan di Rumah Tangga

            // Bagian No Rumah
            tRT.Enabled = false;
            cNoReg.Enabled = false;
            // Bagian No Rumah

            tTempat.Enabled = false;
            dateTimePicker1.Enabled = false;
            cJK.Enabled = false;
            cAgama.Enabled = false;
            tAlamat.Enabled = false;
            cPendidikan.Enabled = false;
            cPekerjaan.Enabled = false;
            tStatusKawin.Enabled = false;
            tStatusBPJS.Enabled = false;
            btnAdd.Enabled = true;
            btnSimpan.Visible = false;
            btnSimpan.Enabled = false;
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            btnRefresh.Enabled = true;
        }

        private bool SwitchAddState()
        {
            return addstate = !addstate;
        }

        private bool SwitchReadOnlyTable()
        {
            return dataGridView1.ReadOnly = !dataGridView1.ReadOnly;
        }

        private bool EnableStateButtonEdit()
        {

            return btnEdit.Enabled = true;
        }

        private bool SwitchEditState()
        {
            return editstate = !editstate;
        }

        private bool EnableStateButtonDelete()
        {
            return btnDelete.Enabled = true;
        }

        private void SwitchSaveButton()
        {
            bool stateActive = addstate || editstate;
            btnAdd.Visible = btnAdd.Enabled = !stateActive;
            btnSimpan.Visible = btnSimpan.Enabled = stateActive;
        }

        private bool SwitchResetState()
        {
            return addstate = editstate = false;
        }

        private void SwitchComboBoxTextBoxNoKartuKeluarga()
        {

            if (addstate == false && editstate == false)
            {
                // Jika Jabatan Keluarga Kepala Keluarga
                if (cJabatan.Text == "Kepala Keluarga")
                {
                    // Combo Box Tidak Muncul
                    cKK.Visible = false;
                    // Disable Combo Box
                    cKK.Enabled = false;
                    // Reset Combo Box
                    cKK.SelectedIndex = -1;
                    // Tampilkan Text Box No Kartu Keluarga
                    tKK.Visible = true;
                    // Tampilkan Data No Kartu Keluarga Pada Text Box
                    tKK.Text = no_kartu_keluarga;
                }
                // Jika Jabatan Keluarga Selain Kepala Keluarga
                else
                {
                    Console.WriteLine("NO KK TAMPIL DATA SWITCH : " + no_kartu_keluarga);
                    // Combo Box Muncul
                    cKK.Visible = true;
                    // Menghilangkan Text Box No Kartu Keluarga
                    tKK.Visible = false;
                    // Text Box No Kartu Keluarga Null
                    tKK.Text = null;
                    // Tampilkan No Kartu Keluarga Pada Combo Box
                    cKK.Text = no_kartu_keluarga;
                }
            }
            else if (addstate || editstate)
            {
                if (cJabatan.Text == "Kepala Keluarga")
                {
                    if (cKK.SelectedIndex != -1)
                    {
                        cKK.SelectedIndex = -1;
                    }
                    if (tKK.Visible == false)
                    {
                        tKK.Visible = true;
                    }
                    tKK.Enabled = true;
                    cKK.Enabled = false;
                    cKK.Visible = false;
                }
                else
                {
                    if (!string.IsNullOrEmpty(tKK.Text))
                    {
                        tKK.Text = null;
                    }
                    cKK.Enabled = true;
                    if (cKK.Visible == false)
                    {
                        cKK.Visible = true;
                    }
                    tKK.Enabled = false;
                    tKK.Visible = false;
                }
            }
        }

        private void SwitchComboBoxTextBoxNoRumah()
        {
            // Jika Jabatan Rumah Tangga Kepala Rumah Tangga
            if (cRT.SelectedIndex == 0)
            {
                // Combo Box Tidak Muncul
                cNoReg.Visible = false;
                // Reset Combo Box
                cNoReg.SelectedIndex = -1;
                // Tampilkan Text Box No Rumah
                tRT.Visible = true;
                // Tampilkan Data No Rumah Pada Text Box
                tRT.Text = no_rumah;
            }
            // Jika Jabatan Rumah Tangga Selain Kepala Rumah Tangga
            else
            {
                // Combo Box Muncul
                cNoReg.Visible = true;
                // Menghilangkan Text Box No Rumah 
                tRT.Visible = false;
                // Text Box No Rumah Null
                tRT.Text = null;
                // Tampilkan No Rumah Pada Combo Box
                cNoReg.Text = no_rumah;
            }
        }

        private void OlahDataComboBoxNoKartuKeluarga()
        {
            // Pastikan semua item di ComboBox ditrim untuk menghindari masalah whitespace
            for (int i = 0; i < cKK.Items.Count; i++)
            {
                cKK.Items[i] = cKK.Items[i].ToString().Trim();
            }

            // Lakukan pencarian secara manual untuk memastikan pencocokan string case-insensitive
            int index = -1;
            for (int i = 0; i < cKK.Items.Count; i++)
            {
                if (string.Equals(cKK.Items[i].ToString().Trim(), tKK.Text.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    index = i;
                    break;
                }
            }

            // Jika nomor KK ditemukan dalam ComboBox, pilih item tersebut
            if (index >= 0)
            {
                //MessageBox.Show("Lanjut Ke Berikutnya");
                //tKK.Text = null;
                //tKK.Enabled = false;
                //cKK.Visible = true;
                //cKK.Enabled = true;
                // Pilih nomor KK yang sesuai
                //cKK.SelectedIndex = index;  
            }
            else
            {
                MessageBox.Show("Tidak Menemukan Data No Kartu Keluarga Yang Sesuai");
            }
        }

        // Cek Isian Data
        private bool CekKepalaRumahTangga(string idrumah)
        {
            if (cNoReg.Enabled == true)
            {
                idrumah = cNoReg.Text.ToString().Trim();
            }
            else if (tRT.Enabled == true)
            {
                idrumah = tRT.Text;
            }
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = kn.strKoneksi(); // Mengambil string koneksi dari sumber Anda
                    connection.Open();

                    // Query untuk cek apakah No KK sudah ada
                    SqlCommand cmdCekKepalaRumahTangga = new SqlCommand("SELECT count(status_dalam_rumah_tangga) as umlah_Kepala_Rumah_Tangga " +
                        "FROM warga WHERE id_rumah = @idRumah and status_dalam_rumah_tangga = 'Kepala Rumah Tangga' " +
                        "and no_reg != @no_reg", connection);
                    cmdCekKepalaRumahTangga.Parameters.AddWithValue("@idRumah", idrumah);
                    cmdCekKepalaRumahTangga.Parameters.AddWithValue("@no_reg", Noreg.Text.Trim());
                    // Menggunakan parameter No KK dan trim spasi

                    // Mengeksekusi query
                    int result = (int)cmdCekKepalaRumahTangga.ExecuteScalar();
                    // Bandingkan string dengan ignore case
                    if (result > 0 && cRT.SelectedIndex == 0 && (editstate == true || addstate == true))
                    {
                        DialogResult konfirmasi = MessageBox.Show($"Id Rumah {idrumah} Telah Telah Memiliki Kepala Rumah Tangga. Mohon Untuk Mengganti Jabatan Rumah Tangga atau Mengganti Id Rumah", "Peringatan", MessageBoxButtons.OK);
                        if (konfirmasi == DialogResult.OK)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        //MessageBox.Show("Tidak Ada Kesamaan Data.");
                        return false;
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Terjadi kesalahan saat memeriksa No KK. " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan. " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return false;
        }


        private bool CekNoKartuKeluarga(string noKK)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = kn.strKoneksi(); // Mengambil string koneksi dari sumber Anda
                    connection.Open();

                    // Query untuk cek apakah No KK sudah ada
                    SqlCommand cmdCekNoKK = new SqlCommand("SELECT distinct No_KK FROM warga WHERE No_KK = @NoKK", connection);
                    cmdCekNoKK.Parameters.AddWithValue("@NoKK", noKK); // Menggunakan parameter No KK dan trim spasi

                    // Mengeksekusi query
                    string result = (string)cmdCekNoKK.ExecuteScalar();
                    // Bandingkan string dengan ignore case
                    if (result != null && string.Equals(result.ToString().Trim(), tKK.Text.Trim(),StringComparison.OrdinalIgnoreCase))
                    {
                        DialogResult konfirmasiNoKartuKeluarga = MessageBox.Show($"Nomor Kartu Keluarga {noKK} Telah Terdaftar. Apakah Nomor Kartu Keluarga Telah Sesuai?", "Peringatan", MessageBoxButtons.YesNo);
                        if (konfirmasiNoKartuKeluarga == DialogResult.Yes)
                        {
                            OlahDataComboBoxNoKartuKeluarga();
                            Console.WriteLine("Lanjut Cek Kepala Keluarga Dari No KK : " + noKK);
                            return true;
                        }
                        else
                        {
                            Console.WriteLine("No KK Belum Ada Di Database: " + noKK);
                            return false;
                        }
                    }
                    else
                    {
                        //MessageBox.Show("Tidak Ada Kesamaan Data No KK");
                        return false;
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Terjadi kesalahan saat memeriksa No KK. " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan. " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private bool CekStatusKepalaKeluarga(string noKK)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = kn.strKoneksi();
                    connection.Open();

                    SqlCommand queryStatusKeluarga = new SqlCommand("Select count(status_dalam_keluarga) as Jumlah_Kepala_Keluarga from warga where status_dalam_keluarga = 'Kepala Keluarga' and No_KK = @NoKK", connection);
                    queryStatusKeluarga.Parameters.AddWithValue("@NoKK",noKK);

                    int result = (int)queryStatusKeluarga.ExecuteScalar();

                    if (result >= 1)
                    {
                        //Console.WriteLine("Ada Kepala Keluarga Dalam No KK");
                        MessageBox.Show($"Kepala Keluarga Dalam Nomor Kartu Keluarga {noKK} Sudah Ada. Mohon Untuk Mengganti Status Dalam Keluarga atau Mengganti No Kartu Keluarga.","Peringatan",MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return true;
                    }
                    else
                    {
                        MessageBox.Show($"Kepala Keluarga Dalam Nomor Kartu Keluarga {noKK} Belum Ada.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Terdapat Error Dalam Koneksi Database. " + ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terdapat Error. " + ex.Message);
                return false;
            }
        }

        // Code Generator
        private void GenerateNoRegistrasi()
        {
            // Mencari nomor registrasi terakhir di tabel warga dan kematian
            int maxNoreg = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = kn.strKoneksi();
                    connection.Open();

                    SqlCommand cmdMaxWarga = new SqlCommand("SELECT MAX(CAST(SUBSTRING(no_reg, LEN(no_reg) - 2, 3) AS INT)) AS maxNoreg FROM warga", connection);
                    SqlCommand cmdMaxKematian = new SqlCommand("SELECT MAX(CAST(SUBSTRING(no_reg, LEN(no_reg) - 2, 3) AS INT)) AS maxNoreg FROM kematian", connection);

                    object wargaNoregObj = cmdMaxWarga.ExecuteScalar();
                    object kematianNoregObj = cmdMaxKematian.ExecuteScalar();

                    if (wargaNoregObj != DBNull.Value)
                    {
                        maxNoreg = Math.Max(maxNoreg, Convert.ToInt32(wargaNoregObj));
                    }

                    if (kematianNoregObj != DBNull.Value)
                    {
                        maxNoreg = Math.Max(maxNoreg, Convert.ToInt32(kematianNoregObj));
                    }
                }

                // Generate nomor registrasi progresif berdasarkan nomor maksimum yang ada
                string noregPrefix = "3404012001" + DateTime.Now.Year + "AMXII";
                string hasil = (maxNoreg + 1).ToString().PadLeft(3, '0');
                string NoregText = noregPrefix + hasil;

                Noreg.Text = NoregText;

                tNIK.Enabled = true;
                btnRefresh.Enabled = true;
                btnAdd.Enabled = false;
                btnAdd.Visible = false;
                btnSimpan.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kesalahan generate data no registrasi. " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Validasi
        private void tNIK_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(tNIK.Text) || tNIK.Text.Length != 16)
            {
                MessageBox.Show("NIK harus diisi dan harus 16 karakter.", "Validasi NIK", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Text Changed
        private void cPekerjaan_TextChanged(object sender, EventArgs e)
        {
            if (cPekerjaan.Text == "Lainnya")
            {
                tPLain.Visible = true;
            }
            else
            {
                tPLain.Visible = false;
                tPLain.Text = string.Empty;
            }
        }

        private void tNama_TextChanged(object sender, EventArgs e)
        {
            nama = tNama.Text;
        }

        // Message Box 
        private object WarningKepalaRumahTangga()
        {
            return MessageBox.Show($"Kepala Rumah Tangga Dapat Dipilih Jika {nama} Merupakan Kepala Keluarga", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        // Combo Box Index Changed

        // Key Press
        private void tNIK_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Memeriksa apakah karakter yang dimasukkan adalah digit (0-9) atau kontrol (seperti backspace)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Menolak input jika bukan angka atau karakter kontrol
            }
        }

        private void tNama_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Memeriksa apakah karakter yang dimasukkan adalah huruf atau karakter kontrol (seperti backspace)
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true; // Menolak input jika bukan huruf atau karakter kontrol
            }
        }

        private void tKK_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void tTempat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true; // Menolak input jika bukan huruf atau karakter kontrol
            }
        }

        private void tPLain_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true; // Menolak input jika bukan huruf atau karakter kontrol
            }
        }

        private void tAlamat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != '/'  && e.KeyChar != '.' && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true; // Menolak input jika bukan angka, huruf, '/' atau karakter kontrol
            }
        }

        // 4 Sekawan Jabatan Keluarga, No KK, Jabatan Rumah Tangga, No Rumah
        // Cjabatan
        private void cJabatan_SelectedIndexChanged(object sender, EventArgs e)
        {
            SwitchComboBoxTextBoxNoKartuKeluarga();
            if (cRT.SelectedIndex == 0 && cJabatan.SelectedIndex != 0 && (editstate == true || addstate == true))
            {
                WarningKepalaRumahTangga();
                cRT.SelectedIndex = -1;
                if (!string.IsNullOrEmpty(tRT.Text))
                {
                    tRT.Text = null;
                }
                if (!string.IsNullOrEmpty(tAlamat.Text))
                {
                    tAlamat.Text = null;
                }
            }
        }
        // tKK
        private void tKK_TextChanged(object sender, EventArgs e)
        {
            keluargaValid = false;
            if (tKK.Text.Length == 16)
            {
                if (CekNoKartuKeluarga(tKK.Text))
                {
                    if (CekStatusKepalaKeluarga(tKK.Text))
                    {
                    }
                    else
                    {
                        keluargaValid = true;
                    }
                }
                else
                {
                    keluargaValid = true;
                }
                Console.WriteLine("Validasi Jabatan Keluarga & No Keluarga " + keluargaValid);
            }
        }

        private void tKK_Validating(object sender, CancelEventArgs e)
        {
            
        }
        // cKK
        private void cKK_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cKK.SelectedIndex != -1 && (addstate == true || editstate == true))
            {
                //MessageBox.Show("Silakan pilih data yang valid dari ComboBox.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                keluargaValid = true;
                return; // Keluar dari event handler jika tidak ada item yang dipilih
            }
            //int index = cKK.Items.IndexOf(cKK.SelectedItem?.ToString());
            //// Cek Jika Selected Index Text Memiliki Kepala Keluarga
            //try
            //{
            //    using (SqlConnection connection = new SqlConnection())
            //    {
            //        connection.ConnectionString = kn.strKoneksi();
            //        connection.Open();

            //        SqlCommand queryStatusKeluarga = new SqlCommand("Select count(status_dalam_keluarga) as Jumlah_Kepala_Keluarga from warga where status_dalam_keluarga = 'Kepala Keluarga' and No_KK = @NoKK", connection);
            //        queryStatusKeluarga.Parameters.AddWithValue("@NoKK", cKK.Text);

            //        int result = (int)queryStatusKeluarga.ExecuteScalar();

            //        if (result >= 1)
            //        {
            //            if (cJabatan.SelectedIndex == 0)
            //            {
            //                MessageBox.Show($"Kepala Keluarga Dari Nomor Kartu Keluarga {cKK.SelectedItem.ToString()} Sudah Ada, Mohon Untuk Memilih Nomor Kartu Keluarga Yang Lain.","Peringatan",MessageBoxButtons.OK,MessageBoxIcon.Information);
            //                cKK.DroppedDown = true;
            //                cKK.Focus();
            //            }
            //        }
            //    }
            //}
            //catch (SqlException ex)
            //{
            //    MessageBox.Show("Kesalahan Database : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Kesalahan Aplikasi : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void cKK_Validating(object sender, CancelEventArgs e)
        {
            Console.WriteLine("valid Keluarga auto true : " + keluargaValid);
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = kn.strKoneksi();
                    connection.Open();

                    SqlCommand queryStatusKeluarga = new SqlCommand("Select count(status_dalam_keluarga) as Jumlah_Kepala_Keluarga from warga where status_dalam_keluarga = 'Kepala Keluarga' and No_KK = @NoKK", connection);
                    queryStatusKeluarga.Parameters.AddWithValue("@NoKK", cKK.Text);

                    int result = (int)queryStatusKeluarga.ExecuteScalar();

                    if (result >= 1)
                    {
                        if (cJabatan.SelectedIndex == 0)
                        {
                            MessageBox.Show($"Kepala Keluarga Dari Nomor Kartu Keluarga {cKK.SelectedItem.ToString()} Sudah Ada, Mohon Untuk Memilih Nomor Kartu Keluarga Yang Lain atau Memilih Jabatan Keluarga Lain.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            e.Cancel = true;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Kesalahan Database : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kesalahan Aplikasi : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // cRT
        private void cRT_SelectedIndexChanged(object sender, EventArgs e)
        {
            nama = tNama.Text;
            try
            {
                if (cRT.SelectedIndex == 0 && nama != "" && cJabatan.SelectedIndex == 0)
                {
                    if (tRT.Visible == false)
                    {
                        tRT.Visible = true;
                    }
                    string idrumah = GenerateIdRumah(nama);
                    tRT.Text = idrumah;

                    // Pastikan event handler TextChanged hanya diaktifkan jika kondisi terpenuhi
                    if (!isTextChangedHandlerAdded)
                    {
                        tNama.TextChanged += new EventHandler((s, ev) =>
                        {
                            if (cRT.SelectedIndex == 0 && cJabatan.SelectedIndex == 0)
                            {
                                string idr = GenerateIdRumah(tNama.Text);
                                tRT.Text = idr;
                            }
                        });
                        isTextChangedHandlerAdded = true;
                    }
                    if (cNoReg.Enabled)
                    {
                        cNoReg.Enabled = false;
                    }
                    if (tAlamat.Enabled == false && (editstate == true || addstate == true))
                    {
                        tAlamat.Enabled = true;
                    }
                    if (cNoReg.SelectedIndex != -1)
                    {
                        cNoReg.SelectedIndex = -1;
                        cNoReg.Enabled = false;
                    }
                    if (!string.IsNullOrEmpty(tAlamat.Text))
                    {
                        tAlamat.Text = null;
                    }
                }
                else if (cRT.SelectedIndex == 0 && nama == "")
                {
                    cRT.SelectedIndex = -1;
                    MessageBox.Show("Mohon Untuk Mengisi Nama Terlebih Dahulu","Peringatan",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    tNama.Focus();
                }
                else if (cRT.SelectedIndex == 0 && nama != "" && cJabatan.SelectedIndex != 0 && (editstate == true || addstate == true))
                {
                    //MessageBox.Show($"Kepala Rumah Tangga Dapat Dipilih Jika {nama} Merupakan Kepala Keluarga", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    WarningKepalaRumahTangga();
                    cRT.SelectedIndex = -1;
                    cNoReg.SelectedIndex = -1;
                    if (!string.IsNullOrEmpty(tAlamat.Text))
                    {
                        tAlamat.Text = null;
                    }
                }
                //else if (cJabatan.SelectedIndex != 0 && cRT.SelectedIndex ){

                //}
                else if (cRT.SelectedIndex == 1 && (editstate == true || addstate == true))
                {
                    tRT.Text = null;
                    cNoReg.Enabled = true;
                    if (tRT.Enabled)
                    {
                        tRT.Enabled = false;
                    }
                    if (tRT.Visible == true)
                    {
                        tRT.Visible = false;
                    }
                    if (!string.IsNullOrEmpty(tAlamat.Text))
                    {
                        tAlamat.Text = null;
                    }
                    if (tAlamat.Enabled == true)
                    {
                        tAlamat.Enabled = false;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi Kesalahan : " + ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        // tRT

        private string GenerateIdRumah(string namaWarga)
        {
            int hasil = 0;
            string idRumah = "";

            try
            {
                // Koneksi ke database untuk menghitung total rumah
                using (SqlConnection koneksi = new SqlConnection(kn.strKoneksi()))
                {
                    koneksi.Open();

                    // Query untuk menghitung jumlah id_rumah
                    SqlCommand cmd = new SqlCommand("SELECT COUNT(id_rumah) as totalRumah FROM rumah", koneksi);
                    cmd.CommandType = CommandType.Text;

                    // Eksekusi query dan ambil hasilnya
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        hasil = dr.GetInt32(0) + 1; // Ambil jumlah id_rumah dari query
                    }
                    dr.Close();
                }

                // Tentukan format idRumah berdasarkan nilai hasil
                if (hasil < 10)
                {
                    idRumah = "RMH00" + hasil + "-" + namaWarga;
                }
                else if (hasil >= 10 && hasil < 100)
                {
                    idRumah = "RMH0" + hasil + "-" + namaWarga;
                }
                else if (hasil >= 100 && hasil < 1000)
                {
                    idRumah = "RMH" + hasil + "-" + namaWarga;
                }
                else
                {
                    idRumah = "RMH" + hasil + "-" + namaWarga;
                }
                //// Tampilkan idRumah di TextBox tRT
                //tRT.Text = idRumah;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan dalam menghasilkan ID Rumah: " + ex.Message);
            }

            return idRumah;
        }

        // cNoReg
        private void cNoReg_SelectedIndexChanged(object sender, EventArgs e)
        {
            string idrumah = cNoReg.SelectedItem?.ToString();
            if (cNoReg.SelectedIndex != -1)
            {
                // Get Data Alamat Berdasarkan No.Rumah
                try
                {
                    using (SqlConnection koneksi = new SqlConnection(kn.strKoneksi()))
                    {
                        koneksi.Open();

                        SqlCommand queryGetAlamat = new SqlCommand("SELECT alamat FROM rumah WHERE id_rumah = @idRumah", koneksi);
                        queryGetAlamat.Parameters.AddWithValue("@idRumah", idrumah);

                        SqlDataReader reader = queryGetAlamat.ExecuteReader();

                        if (reader.Read())
                        {
                            // Set talamat dengan data alamat yang diambil
                            tAlamat.Text = reader["alamat"].ToString();
                            tAlamat.Enabled = false;
                        }
                        else
                        {
                            // Jika tidak ada data ditemukan
                            tAlamat.Text = "Alamat tidak ditemukan";
                        }

                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    // Menangani kesalahan
                    MessageBox.Show("Terjadi kesalahan: " + ex.Message);
                }
            }
        }

        private void btnSwitchNoRumah_Click(object sender, EventArgs e)
        {
            if (cNoReg.Enabled == false && !string.IsNullOrEmpty(tRT.Text) && (editstate == true || addstate == true))
            {
                DialogResult result = MessageBox.Show("Apakah Anda Ingin Memilih No Rumah Yang Telah Terdaftar?","Pemberitahuan",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if (result.ToString() == "Yes")
                {
                    LoadDataNoRumahTanpaKRT();
                    cNoReg.Enabled = true;
                    tRT.Text = null;
                    tRT.Enabled = false;
                }
            }
            else if (cNoReg.Enabled == true && (editstate == true || addstate == true))
            {
                LoadDataNoRumah();
                DialogResult result = MessageBox.Show("Apakah Anda Ingin Membuat No Rumah Baru?", "Pemberitahuan", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result.ToString() == "Yes")
                {
                    if (cRT.SelectedIndex != 0 && cJabatan.SelectedIndex != 0)
                    {
                        MessageBox.Show("Nomor Rumah Baru Harus Merupakan Kepala Keluarga Dan Kepala Rumah Tangga","Pemberitahuan",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        return;
                    }
                    if (string.IsNullOrEmpty(tAlamat.Text))
                    {
                        tAlamat.Text = null;
                    }
                    tRT.Text  = GenerateIdRumah(tNama.Text);
                    cNoReg.Enabled = false;
                    cNoReg.SelectedIndex = -1;
                }
            }
        }

        private void LoadDataNoRumahTanpaKRT()
        {
            try
            {
                using (SqlConnection koneksi = new SqlConnection(kn.strKoneksi()))
                {
                    koneksi.Open();

                    SqlCommand queryGetData = new SqlCommand("SELECT r.id_rumah " +
                        "FROM rumah r " +
                        "LEFT JOIN warga w ON r.id_rumah = w.id_rumah " +
                        "GROUP BY r.id_rumah " +
                        "HAVING COUNT(CASE WHEN w.status_dalam_rumah_tangga = 'Kepala Rumah Tangga' THEN 1 END) = 0", koneksi);

                    SqlDataReader reader = queryGetData.ExecuteReader();
                    cNoReg.Items.Clear();

                    if (reader.HasRows) // Cek apakah ada hasil dari query
                    {
                        while (reader.Read())
                        {
                            cNoReg.Items.Add(reader["id_rumah"].ToString());
                        }
                    }
                    else
                    {
                        MessageBox.Show("Tidak ada rumah tanpa Kepala Rumah Tangga yang tersedia.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                // Menangani kesalahan
                MessageBox.Show("Terjadi kesalahan: " + ex.Message);
            }
        }

        private void tCari_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tCari.Text))
            {
                if (fWarga.Enabled == true)
                {
                    fWarga.Enabled = false;
                }
            }
            else
            {
                if (fWarga.Enabled == false)
                {
                    fWarga.Enabled = true;
                }
            }
            // Restart timer setiap kali teks berubah
            searchTimer.Stop();
            searchTimer.Start();
        }

        private void SearchTimer_Tick(object sender, EventArgs e)
        {
            // Hentikan timer setelah jeda waktu selesai
            searchTimer.Stop();

            // Lakukan pencarian
            SearchDataByName(tCari.Text);
        }


        private void SearchDataByName(string nama)
        {
            searchFound = false;

            using (SqlConnection koneksi = new SqlConnection(kn.strKoneksi()))
            {
                koneksi.Open();
                try
                {
                    // Query pencarian berdasarkan nama
                    string query = "SELECT no_reg AS 'Nomor Registrasi', Nama AS 'Nama Warga', No_KK AS 'No Kartu Keluarga' " +
                                   "FROM warga WHERE Nama LIKE @nama ORDER BY no_reg DESC";
                    SqlDataAdapter ad = new SqlDataAdapter(query, koneksi);
                    ad.SelectCommand.Parameters.AddWithValue("@nama", "%" + nama + "%");

                    DataSet ds = new DataSet();
                    ad.Fill(ds);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        dataGridView1.DataSource = ds.Tables[0];
                        searchFound = true;
                    }
                    else
                    {
                        dataGridView1.DataSource = null;
                        MessageBox.Show("Data tidak ditemukan.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal Mengambil Data Warga. " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void fWarga_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!fWarga.Text.Equals(""))
            {
                
            }

            string query = "";
            string countQuery = "";

            tCari.Enabled = false;

            // Membuat query dan countQuery berdasarkan pilihan filter
            switch (fWarga.Text)
            {
                case "Balita":
                    query = "SELECT No_Reg AS 'No Registrasi', Nama AS 'Nama Warga', No_KK AS 'No Kartu Keluarga' FROM warga " +
                            "WHERE 0 <= DATEDIFF(year, tanggal_lahir, GETDATE()) AND DATEDIFF(year, tanggal_lahir, GETDATE()) <= 4";
                    countQuery = "SELECT COUNT(No_Reg) AS totalWarga FROM warga " +
                                 "WHERE 0 <= DATEDIFF(year, tanggal_lahir, GETDATE()) AND DATEDIFF(year, tanggal_lahir, GETDATE()) <= 4";
                    break;

                case "PUS":
                    query = "SELECT No_Reg AS 'No Registrasi', Nama AS 'Nama Warga', No_KK AS 'No Kartu Keluarga' FROM warga " +
                            "WHERE 15 <= DATEDIFF(year, tanggal_lahir, GETDATE()) AND DATEDIFF(year, tanggal_lahir, GETDATE()) <= 49";
                    countQuery = "SELECT COUNT(No_Reg) AS totalWarga FROM warga " +
                                 "WHERE 15 <= DATEDIFF(year, tanggal_lahir, GETDATE()) AND DATEDIFF(year, tanggal_lahir, GETDATE()) <= 49";
                    break;

                case "WUS":
                    query = "SELECT No_Reg AS 'No Registrasi', Nama AS 'Nama Warga', No_KK AS 'No Kartu Keluarga' FROM warga " +
                            "WHERE 20 <= DATEDIFF(year, tanggal_lahir, GETDATE()) AND DATEDIFF(year, tanggal_lahir, GETDATE()) <= 45 " +
                            "AND jenis_kelamin = 'P'";
                    countQuery = "SELECT COUNT(No_Reg) AS totalWarga FROM warga " +
                                 "WHERE 20 <= DATEDIFF(year, tanggal_lahir, GETDATE()) AND DATEDIFF(year, tanggal_lahir, GETDATE()) <= 45 " +
                                 "AND jenis_kelamin = 'P'";
                    break;

                case "Lansia":
                    query = "SELECT No_Reg AS 'No Registrasi', Nama AS 'Nama Warga', No_KK AS 'No Kartu Keluarga' FROM warga " +
                            "WHERE 60 < DATEDIFF(year, tanggal_lahir, GETDATE())";
                    countQuery = "SELECT COUNT(No_Reg) AS totalWarga FROM warga " +
                                 "WHERE 60 < DATEDIFF(year, tanggal_lahir, GETDATE())";
                    break;

                case "PNS":
                    query = "SELECT No_Reg AS 'No Registrasi', Nama AS 'Nama Warga', No_KK AS 'No Kartu Keluarga' FROM warga WHERE pekerjaan LIKE 'PNS'";
                    countQuery = "SELECT COUNT(No_Reg) AS totalWarga FROM warga WHERE pekerjaan LIKE 'PNS'";
                    break;

                case "Wirausaha":
                    query = "SELECT No_Reg AS 'No Registrasi', Nama AS 'Nama Warga', No_KK AS 'No Kartu Keluarga' FROM warga WHERE pekerjaan LIKE 'Wirausaha'";
                    countQuery = "SELECT COUNT(No_Reg) AS totalWarga FROM warga WHERE pekerjaan LIKE 'Wirausaha'";
                    break;

                case "Pelajar/Mahasiswa":
                    query = "SELECT No_Reg AS 'No Registrasi', Nama AS 'Nama Warga', No_KK AS 'No Kartu Keluarga' FROM warga WHERE pekerjaan LIKE 'Pelajar/Mahasiswa'";
                    countQuery = "SELECT COUNT(No_Reg) AS totalWarga FROM warga WHERE pekerjaan LIKE 'Pelajar/Mahasiswa'";
                    break;

                case "Buruh/Karyawan":
                    query = "SELECT No_Reg AS 'No Registrasi', Nama AS 'Nama Warga', No_KK AS 'No Kartu Keluarga' FROM warga WHERE pekerjaan LIKE 'Buruh/Karyawan'";
                    countQuery = "SELECT COUNT(No_Reg) AS totalWarga FROM warga WHERE pekerjaan LIKE 'Buruh/Karyawan'";
                    break;

                case "Lainnya":
                    query = "SELECT No_Reg AS 'No Registrasi', Nama AS 'Nama Warga', No_KK AS 'No Kartu Keluarga' FROM warga " +
                            "WHERE pekerjaan NOT IN ('PNS', 'Wirausaha', 'Pelajar/Mahasiswa', 'Buruh/Karyawan')";
                    countQuery = "SELECT COUNT(No_Reg) AS totalWarga FROM warga " +
                                 "WHERE pekerjaan NOT IN ('PNS', 'Wirausaha', 'Pelajar/Mahasiswa', 'Buruh/Karyawan')";
                    break;

                default:
                    LoadDataTable();
                    LoadTotalWargaTerdaftar();
                    tCari.Enabled = true;
                    return;
            }

            // Memanggil metode untuk mengisi DataGridView
            FillDataGrid(query);
            // Memanggil metode untuk menampilkan jumlah warga sesuai filter
            jmlWarga.Text = GetWargaCount(countQuery);
        }

        private void FillDataGrid(string query)
        {
            using (SqlConnection koneksi = new SqlConnection(kn.strKoneksi()))
            {
                koneksi.Open();
                SqlDataAdapter ad = new SqlDataAdapter(query, koneksi);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];

                // Set alignment
                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
        }

        private string GetWargaCount(string countQuery)
        {
            string hasil = "";
            using (SqlConnection koneksi = new SqlConnection(kn.strKoneksi()))
            {
                koneksi.Open();
                SqlCommand cmd = new SqlCommand(countQuery, koneksi);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    hasil = dr["totalWarga"].ToString();
                }
                dr.Close();
            }
            return hasil;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Apakah Anda yakin ingin menghapus data warga ini?", "Konfirmasi Hapus", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                // Membuka koneksi dan transaksi SQL
                using (SqlConnection conn = new SqlConnection(kn.strKoneksi()))
                {
                    conn.Open();
                    SqlTransaction sqlTransaction = conn.BeginTransaction();

                    try
                    {
                        string queryDelete = "DELETE FROM warga WHERE no_reg = @no_registrasi";

                        using (SqlCommand cmd = new SqlCommand(queryDelete, conn, sqlTransaction))
                        {
                            cmd.Parameters.AddWithValue("@no_registrasi", no_registrasi.Trim());

                            // Eksekusi perintah DELETE
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Data berhasil dihapus.","Berhasil",MessageBoxButtons.OK,MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Data tidak ditemukan atau gagal dihapus.", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }

                        // Commit transaksi jika sukses
                        sqlTransaction.Commit();
                        btnRefresh_Click(null, null);
                    }
                    catch (Exception ex)
                    {
                        // Rollback transaksi jika terjadi error
                        sqlTransaction.Rollback();
                        MessageBox.Show("Terjadi kesalahan: " + ex.Message);
                    }
                }
            }
        }
    }
}
