using BarkodluSatisProgrami1.APIService;
using BarkodluSatisProgrami1.Exceptions;
using BarkodluSatisProgrami1.Models;
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
        KullaniciAPI kullaniciAPI;
        SabitAPI sabitAPI;
        public Login()
        {
            InitializeComponent();
            kullaniciAPI = new KullaniciAPI();
            sabitAPI = new SabitAPI();
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            GirisYap();
        }

        private async void GirisYap()
        {
            if (txtKullaniciAdi.Text != "" && txtSifre.Text != "")
            {
                try
                {
                    var kullanicis = await kullaniciAPI.KullaniciList();
                    var sabits=await sabitAPI.SabitList();

                        if (kullanicis!=null && kullanicis.Any())
                        {
                            var bak = kullanicis.Where(x => x.KullaniciAd == txtKullaniciAdi.Text && x.Sifre == txtSifre.Text).FirstOrDefault();
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

                            if (sabits != null)
                            {
                                var kullaniciUnvan = sabits.FirstOrDefault(s => s.Id == bak.Id)?.Unvan;
                                anasayfa.lblIsyeri.Text = kullaniciUnvan;
                            }
                            else
                            {
                                anasayfa.lblIsyeri.Text = "admin";
                            }
                                
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
                catch(CustomNotFoundException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Beklenmedik bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
