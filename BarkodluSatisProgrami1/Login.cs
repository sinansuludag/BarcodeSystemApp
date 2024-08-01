using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BarkodluSatisProgrami1
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            GirisYap();
        }

        private void GirisYap()
        {
            if (txtKullaniciAdi.Text != "" && txtSifre.Text != "")
            {
                try
                {
                    using (var db = new DbBarkodEntities())
                    {
                        if (db.Kullanicis.Any())
                        {
                            var bak = db.Kullanicis.Where(x => x.KullaniciAd == txtKullaniciAdi.Text && x.Sifre == txtSifre.Text).FirstOrDefault();
                            if (bak != null)
                            {
                                Cursor.Current = Cursors.WaitCursor;
                                Anasayfa anasayfa = new Anasayfa();
                                anasayfa.btnSatisIslemi.Enabled = (bool)bak.Satis;
                                anasayfa.btnGenelRapor.Enabled = (bool)bak.Rapor;
                                anasayfa.btnStokTakibi.Enabled = (bool)bak.Stok;
                                anasayfa.btnUrunGiris.Enabled = (bool)bak.UrunGiris;
                                anasayfa.btnAyarlar.Enabled = (bool)bak.Ayarlar;
                                anasayfa.btnFiyatGuncelle.Enabled = (bool)bak.FiyatGuncelle;
                                anasayfa.btnYedekleme.Enabled = (bool)bak.Yedekleme;
                                anasayfa.lblKullanici.Text = bak.AdSoyad;
                                var isyeri = db.Sabits.FirstOrDefault();
                                anasayfa.lblIsyeri.Text = isyeri.Unvan;
                                anasayfa.Show();
                                this.Hide();
                                Cursor.Current = Cursors.Default;
                            }
                            else
                            {
                                MessageBox.Show("Hatalı giriş");
                                txtKullaniciAdi.Clear();
                                txtSifre.Clear();
                                txtKullaniciAdi.Focus();

                            }

                        }
                        else
                        {
                            MessageBox.Show("Hatalı giriş");
                            txtKullaniciAdi.Clear();
                            txtSifre.Clear();
                            txtKullaniciAdi.Focus();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata :" + ex.Message);
                }
            }
        }

        private void Login_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                GirisYap();
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            txtKullaniciAdi.Focus();
        }
    }
}
