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
    public partial class Anasayfa : Form
    {
        public Anasayfa()
        {
            InitializeComponent();
        }

        private void btnSatisIslemi_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            fSatis satis = new fSatis();
            satis.lblKullanici.Text = lblKullanici.Text;
            satis.ShowDialog();
            Cursor.Current = Cursors.Default;
        }

        private void btnGenelRapor_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Rapor rapor = new Rapor();
            rapor.lblKullanici.Text = lblKullanici.Text;
            rapor.ShowDialog();
            Cursor.Current = Cursors.Default;
        }

        private void btnStokTakibi_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Stok stok = new Stok();
            stok.lblKullaniciStok.Text = lblKullanici.Text;
            stok.ShowDialog();
            Cursor.Current = Cursors.Default;
        }

        private void btnUrunGiris_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            UrunGiris urunGiris = new UrunGiris();
            urunGiris.lblKullanici.Text = lblKullanici.Text;
            urunGiris.ShowDialog();
            Cursor.Current = Cursors.Default;
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnFiyatGuncelle_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            FiyatGuncelle fiyatGuncelle = new FiyatGuncelle();
            fiyatGuncelle.lblKullanici.Text = lblKullanici.Text;
            fiyatGuncelle.ShowDialog();
            Cursor.Current = Cursors.Default;
        }

        private void btnAyarlar_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Ayarlar ayarlar = new Ayarlar();
            ayarlar.lblKullanici.Text=lblKullanici.Text;
            ayarlar.ShowDialog();
            Cursor.Current = Cursors.Default;
        }

        private void btnYedekleme_Click(object sender, EventArgs e)
        {
            Islemler.Backup();
        }

        private void btnKullaniciDegistir_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }
    }
}
