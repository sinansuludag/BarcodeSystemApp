using BarkodluSatisProgrami1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BarkodluSatisProgrami1
{
    public partial class Ayarlar : Form
    {
        public Ayarlar()
        {
            InitializeComponent();
        }



        private void Temizle()
        {
            txtAdSoyad.Clear();
            msTxtTelefon.Clear();
            txtEposta.Clear();
            txtKullaniciAdi.Clear();
            txtSifre.Clear();
            chSatisIslemi.Checked = false;
            chRaporIslemi.Checked = false;
            chStokIslemi.Checked = false;
            chUrunGisisIslemi.Checked = false;
            chAyarlar.Checked = false;
            chFiyatGuncelle.Checked = false;
            chYedeklemeIslemi.Checked = false;
            txtSifreTekrar.Clear();

        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (btnKaydet.Text == "Kaydet")
            {
                if (txtAdSoyad.Text != "" && msTxtTelefon.Text != "" && txtKullaniciAdi.Text != "" && txtSifre.Text != "" && txtSifreTekrar.Text != "")
                {
                    if (txtSifre.Text == txtSifreTekrar.Text)
                    {
                        using (var db = new DbBarkodEntities())
                        {
                            if (!db.Kullanicis.Any(x => x.KullaniciAd == txtKullaniciAdi.Text))
                            {
                                Kullanici kullanici = new Kullanici();
                                kullanici.AdSoyad = txtAdSoyad.Text;
                                kullanici.Telefon = msTxtTelefon.Text;
                                kullanici.Eposta = txtEposta.Text;
                                kullanici.KullaniciAd = txtKullaniciAdi.Text;
                                kullanici.Sifre = txtSifre.Text;
                                kullanici.Satis = chSatisIslemi.Checked;
                                kullanici.Rapor = chRaporIslemi.Checked;
                                kullanici.Stok = chStokIslemi.Checked;
                                kullanici.UrunGiris = chUrunGisisIslemi.Checked;
                                kullanici.Ayarlar = chAyarlar.Checked;
                                kullanici.FiyatGuncelle = chFiyatGuncelle.Checked;
                                kullanici.Yedekleme = chYedeklemeIslemi.Checked;
                                db.Kullanicis.Add(kullanici);
                                db.SaveChanges();
                                Doldur();
                                Temizle();
                            }
                            else
                            {
                                MessageBox.Show("Bu kullanıcı zaten kayıtlı!");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Şifreler uyuşmuyor, lütfen doğru şifre giriniz!");
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen zorunlu alanları doldurunuz!" + "\nAd Soyad \nTelefon \nKullanici Adı \nŞifre \nŞifre Tekrar");
                }
            }
            else if (btnKaydet.Text == "Düzenle/Kaydet")
            {
                if (txtAdSoyad.Text != "" && msTxtTelefon.Text != "" && txtKullaniciAdi.Text != "" && txtSifre.Text != "" && txtSifreTekrar.Text != "")
                {
                    if (txtSifre.Text == txtSifreTekrar.Text)
                    {
                        int id = Convert.ToInt32(lblId.Text);
                        using(var db=new DbBarkodEntities())
                        {
                            var guncelle = db.Kullanicis.Where(x => x.Id == id).FirstOrDefault();
                            guncelle.AdSoyad = txtAdSoyad.Text;
                            guncelle.Telefon = msTxtTelefon.Text;
                            guncelle.Eposta = txtEposta.Text;
                            guncelle.KullaniciAd = txtKullaniciAdi.Text;
                            guncelle.Sifre = txtSifre.Text;
                            guncelle.Satis = chSatisIslemi.Checked;
                            guncelle.Rapor = chRaporIslemi.Checked;
                            guncelle.Stok = chStokIslemi.Checked;
                            guncelle.UrunGiris = chUrunGisisIslemi.Checked;
                            guncelle.Ayarlar = chAyarlar.Checked;
                            guncelle.FiyatGuncelle = chFiyatGuncelle.Checked;
                            guncelle.Yedekleme = chYedeklemeIslemi.Checked;
                            db.SaveChanges();
                            MessageBox.Show("Güncelleme yapılmıştır");
                            btnKaydet.Text = "Kaydet";
                            Temizle();
                            Doldur();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Şifreler uyuşmuyor, lütfen doğru şifre giriniz!");
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen zorunlu alanları doldurunuz!" + "\nAd Soyad \nTelefon \nKullanici Adı \nŞifre \nŞifre Tekrar");
                }
            }
        }

        private void düzenleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(gridListeKullanici.Rows.Count > 0)
            {
                int id = Convert.ToInt32(gridListeKullanici.CurrentRow.Cells["Id"].Value.ToString());
                lblId.Text = id.ToString();
                using(var db=new DbBarkodEntities())
                {
                    var getir = db.Kullanicis.Where(x => x.Id == id).FirstOrDefault();
                    txtAdSoyad.Text = getir.AdSoyad;
                    msTxtTelefon.Text = getir.Telefon;
                    txtEposta.Text = getir.Eposta;
                    txtKullaniciAdi.Text = getir.KullaniciAd;
                    txtSifre.Text = getir.Sifre;
                    txtSifreTekrar.Text = getir.Sifre;
                    chSatisIslemi.Checked=(bool)getir.Satis;
                    chRaporIslemi.Checked=(bool)getir.Rapor;
                    chStokIslemi.Checked=(bool)getir.Stok;
                    chUrunGisisIslemi.Checked = (bool)getir.UrunGiris;
                    chAyarlar.Checked=(bool)getir.Ayarlar;
                    chFiyatGuncelle.Checked = (bool)getir.FiyatGuncelle;
                    chYedeklemeIslemi.Checked = (bool)getir.Yedekleme;
                    btnKaydet.Text = "Düzenle/Kaydet";
                    Doldur();
                }
            }
 
        }

        private void Ayarlar_Load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Doldur();
            Cursor.Current = Cursors.Default;
        }

        private void Doldur()
        {
            using (var db = new DbBarkodEntities())
            {
                if (db.Kullanicis.Any())
                {
                    gridListeKullanici.DataSource = db.Kullanicis.Select(x => new { x.Id, x.AdSoyad, x.KullaniciAd, x.Telefon }).ToList();
                   
                }
                Islemler.SabitVarsayilan();
                var yazici = db.Sabits.FirstOrDefault();
                chYazmaDurumu.Checked = (bool)yazici.Yazici;

                var sabitler=db.Sabits.FirstOrDefault();
                txtKartKomisyon.Text = sabitler.KartKomisyon.ToString();

                var teraziOnEk = db.Terazis.ToList();
                cbTeraziOnEk.DisplayMember = "TeraziOnEk";
                cbTeraziOnEk.ValueMember = "Id";
                cbTeraziOnEk.DataSource = teraziOnEk;

                txtIsyeriAdSoyad.Text = sabitler.AdSoyad;
                txtIsyeriUnvan.Text = sabitler.Unvan;
                txtIsyeriAdres.Text = sabitler.Adres;
                msIsyeriTelefon.Text = sabitler.Telefon;
                txtIsyeriEposta.Text= sabitler.Eposta;

               /* int id = Convert.ToInt32(gridListeKullanici.CurrentRow.Cells["Id"].Value.ToString());
                lblId.Text = id.ToString();              
                var bilgileriGetir=db.Kullanicis.Where(x=>x.Id==id).FirstOrDefault();
                txtAdSoyad.Text = bilgileriGetir.AdSoyad;
                msIsyeriTelefon.Text = bilgileriGetir.Telefon;
                txtEposta.Text = bilgileriGetir.Eposta;
                txtKullaniciAdi.Text = bilgileriGetir.KullaniciAd;
                txtSifre.Text = bilgileriGetir.Sifre;
                txtSifreTekrar.Text = bilgileriGetir.Sifre;*/
            }
        }

        private void chYazmaDurumu_CheckedChanged(object sender, EventArgs e)
        {
            using (var db = new DbBarkodEntities())
            {
                if (chYazmaDurumu.Checked)
                {
                
                    Islemler.SabitVarsayilan();
                    var ayarlar = db.Sabits.FirstOrDefault();
                    ayarlar.Yazici = true;
                    db.SaveChanges();
                    chYazmaDurumu.Text = "Yazma Durumu AKTİF";


                }
                else
                {
                    Islemler.SabitVarsayilan();
                    var ayarlar = db.Sabits.FirstOrDefault();
                    ayarlar.Yazici = false;
                    db.SaveChanges();
                    chYazmaDurumu.Text = "Yazma Durumu PASİF";
                }
            }
        }

        private void btnKartKomisyon_Click(object sender, EventArgs e)
        {
            if (txtKartKomisyon.Text != "")
            {
                using (var db = new DbBarkodEntities())
                {
                    var sabit = db.Sabits.FirstOrDefault();
                    sabit.KartKomisyon = Convert.ToInt16(txtKartKomisyon.Text);
                    db.SaveChanges();
                    MessageBox.Show("Kart komisyonu ayarlandı");
                }
            }
            else
            {
                MessageBox.Show("Kart komisyon bilgisini giriniz!");
            }
            
        }

        private void btnTeraziOnEk_Click(object sender, EventArgs e)
        {
            if (cbTeraziOnEk.Text != "")
            {
                int onek = Convert.ToInt16(txtTeraziOnEk.Text);
                using(var db=new DbBarkodEntities())
                {
                    if (db.Terazis.Any(x=>x.TeraziOnEk==onek))
                    {
                        MessageBox.Show(onek.ToString() + " ön ek zaten kayıtlı");
                    }
                    else
                    {
                        Terazi t = new Terazi();
                        t.TeraziOnEk = onek;
                        db.Terazis.Add(t);
                        db.SaveChanges();
                        MessageBox.Show("Bilgiler kaydedilmiştir");
                        cbTeraziOnEk.ValueMember = "Id";
                        cbTeraziOnEk.DisplayMember = "TeraziOnEk";
                        cbTeraziOnEk.DataSource = db.Terazis.ToList();
                        txtTeraziOnEk.Clear();
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen terazi ön ek bilgisini giriniz!");
            }
        }

        private void btnTeraziOnEksil_Click(object sender, EventArgs e)
        {
            if (cbTeraziOnEk.Text != "")
            {
                int onEkId = Convert.ToInt16(cbTeraziOnEk.SelectedValue);
                DialogResult onay=MessageBox.Show(cbTeraziOnEk.Text+" ön ekini silmek istiyor musunuz?","Terazi Ön Ek Silme İşlemi",MessageBoxButtons.YesNo);
                if (onay == DialogResult.Yes)
                {
                    using(var db=new DbBarkodEntities())
                    {
                        var onek=db.Terazis.Find(onEkId);
                        db.Terazis.Remove(onek);
                        db.SaveChanges();
                        cbTeraziOnEk.ValueMember = "Id";
                        cbTeraziOnEk.DisplayMember = "TeraziOnEk";
                        cbTeraziOnEk.DataSource = db.Terazis.ToList();
                        MessageBox.Show("Ön Ek silinmiştir");
                    }
                }
                else
                {
                    MessageBox.Show("Silme işlemi iptal edildi");
                }
            }
        }

        private void btnIsyeriKaydet_Click(object sender, EventArgs e)
        {
            if (txtIsyeriAdSoyad.Text != "" && txtIsyeriUnvan.Text != "" && txtIsyeriAdres.Text != "" && msIsyeriTelefon.Text != "")
            {
                using(var db=new DbBarkodEntities())
                {
                    var isyeri = db.Sabits.FirstOrDefault();
                    isyeri.AdSoyad = txtAdSoyad.Text;
                    isyeri.Unvan = txtIsyeriUnvan.Text;
                    isyeri.Adres = txtIsyeriAdres.Text;
                    isyeri.Telefon = msIsyeriTelefon.Text;
                    isyeri.Eposta = txtEposta.Text;
                    db.SaveChanges();
                    MessageBox.Show("İşyeri bilgileri kaydedilmiştir");
                    var yeni=db.Sabits.FirstOrDefault();
                    txtAdSoyad.Text = yeni.AdSoyad;
                    txtIsyeriUnvan.Text= yeni.Unvan;
                    txtIsyeriAdres.Text= yeni.Adres;
                    msIsyeriTelefon.Text=yeni.Telefon;
                    txtEposta.Text= yeni.Eposta;

                }
            }
        }

        private void btnYedektenYukle_Click(object sender, EventArgs e)
        {
            Process.Start(Application.StartupPath + @"\ProgramRestore.exe");
            Application.Exit();
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            MessageBox.Show("İşlem iptal edildi");
            using (var db = new DbBarkodEntities())
            {
                var yeni = db.Sabits.FirstOrDefault();
                txtAdSoyad.Text = yeni.AdSoyad;
                txtIsyeriUnvan.Text = yeni.Unvan;
                txtIsyeriAdres.Text = yeni.Adres;
                msIsyeriTelefon.Text = yeni.Telefon;
                txtEposta.Text = yeni.Eposta;
            }
        }
    }
}

