using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace siDIA_Project
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine(textBox1.Text);
            Console.WriteLine(textBox1.Text.ToString());
            if (textBox1.Text == "Admin" &&  textBox2.Text == "Transformasi03")
            {
                home home = new home();
                home.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Username / Password yang anda masukkan salah","Login Gagal",MessageBoxButtons.OK);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dlg = MessageBox.Show("Apakah Anda Yakin Keluar dari Aplikasi ini?",
                "Informasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlg == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
