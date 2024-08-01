using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BarkodluSatisProgrami1
{
    public partial class NakitKart : Form
    {
        public NakitKart()
        {
            InitializeComponent();
        }

        private void Hesapla()
        {
            fSatis satis = (fSatis)Application.OpenForms["fSatis"];
            double nakit = Islemler.DoubleYap(txtNakit.Text);
            double geneltoplam = Islemler.DoubleYap(satis.txtGenelToplam.Text);
            double kart = geneltoplam - nakit;
            satis.lblNakit.Text = nakit.ToString("C2");
            satis.lblKart.Text = kart.ToString("C2");
            satis.SatisYap("Kart-Nakit");
            this.Hide();
        }

        private void txtNakit_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtNakit.Text != "")
            {
                if (e.KeyCode == Keys.Enter)
                {
                    Hesapla();
                    MessageBox.Show("Satış yapılmıştır");
                }
            }
        }

        //Numarator işlevli yap
        private void bN1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Button b = (System.Windows.Forms.Button)sender;
            if (b.Text == ",")
            {
                int virgul = txtNakit.Text.Count(x => x == ',');
                if (virgul < 1)
                {
                    txtNakit.Text += b.Text;
                }
            }
            else if (b.Text == "<")
            {
                if (txtNakit.Text.Length > 0)
                {
                    txtNakit.Text = txtNakit.Text.Substring(0, txtNakit.Text.Length - 1);
                }
            }
            else
            {
                txtNakit.Text += b.Text;
            }
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            if (txtNakit.Text != "")
            {
                Hesapla();
                MessageBox.Show("Satış yapılmıştır");
            }
        }

        //Sadece sayilari alabilsin diğer karakterleri almasın
        private void txtNakit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == false && e.KeyChar != (char)08)
            {
                e.Handled = true;
            }
        }
    }
}
