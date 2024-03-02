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
    public partial class home : Form
    {
        bool dmCollapse;
        public home()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DMtimer.Start();
        }


        private void DMtimer_Tick(object sender, EventArgs e)
        {
            if (dmCollapse)
            {
                panel3.Height -= 10;
                if (panel3.Height == panel3.MinimumSize.Height)
                {
                    dmCollapse = false;
                    DMtimer.Stop();
                }
            }
            else
            {
                panel3.Height += 10;
                if (panel3.Height == panel3.MaximumSize.Height)
                {
                    dmCollapse = true;
                    DMtimer.Stop();
                }
            }
        }

        private Form activeForm = null;
        private void openChildeForm (Form cform)
        {
            if(activeForm != null)
            {
                activeForm.Close ();
            }
            activeForm = cform;
            cform.TopLevel = false;
            cform.Dock = DockStyle.Fill;
            childPanel.Controls.Add(cform);
            childPanel.Tag = cform;
            cform.BringToFront();
            cform.Show ();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openChildeForm(new DataWarga());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            openChildeForm(new DataKesling());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            openChildeForm(new CatatanWarga());
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult dlg = MessageBox.Show("Apakah Anda Yakin Keluar dari Aplikasi ini?",
                "Informasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlg == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            openChildeForm(new CatatanKematian());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openChildeForm(new Grafik());
        }
    }
}
