using BarkodluSatisProgrami1.APIService;
using BarkodluSatisProgrami1.Exceptions;
using BarkodluSatisProgrami1.Models;
using BarkodluSatisProgrami1.Models.FormDTO;
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
        SabitAPI sabitAPI;
        KullaniciAPI kullaniciAPI;
        TeraziAPI teraziAPI;
        public Ayarlar()
        {
            InitializeComponent();
            sabitAPI = new SabitAPI();
            kullaniciAPI = new KullaniciAPI();
            teraziAPI = new TeraziAPI();
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

        private async void btnKaydet_Click(object sender, EventArgs e)
        {
            if (btnKaydet.Text == "Kaydet")
            {
                if (txtAdSoyad.Text != "" && msTxtTelefon.Text != "" && txtKullaniciAdi.Text != "" && txtSifre.Text != "" && txtSifreTekrar.Text != "")
                {
                    if (txtSifre.Text == txtSifreTekrar.Text)
                    {
                        var kullanicis = await kullaniciAPI.KullaniciList();
                            if (!(kullanicis.Any(x => x.KullaniciAd == txtKullaniciAdi.Text && kullanicis!=null)))
                            {
                                KullaniciDTO kullanici = new KullaniciDTO();
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

                            try
                            {
                                await kullaniciAPI.KullaniciAdd(kullanici);

                                Doldur();
                                Temizle();
                            }
                            catch(CustomNotFoundException ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            catch(Exception ex)
                            {
                                MessageBox.Show("Beklenmedik bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                               
                            }
                            else
                            {
                                MessageBox.Show("Bu kullanıcı zaten kayıtlı!");
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
                        var kullanicis = await kullaniciAPI.KullaniciList();

                            var guncelle = kullanicis.Where(x => x.Id == id).FirstOrDefault();
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
                        try
                        {
                            await kullaniciAPI.KullaniciUpdate(id, guncelle);

                            MessageBox.Show("Güncelleme yapılmıştır");
                            btnKaydet.Text = "Kaydet";
                            Temizle();
                            Doldur();
                        }
                        catch(CustomNotFoundException ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show("Beklenmedik bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private async void düzenleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(gridListeKullanici.Rows.Count > 0)
            {
                int id = Convert.ToInt32(gridListeKullanici.CurrentRow.Cells["Id"].Value.ToString());
                lblId.Text = id.ToString();
                var kullanicis = await kullaniciAPI.KullaniciList();
                    var getir = kullanicis.Where(x => x.Id == id).FirstOrDefault();
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

        private void Ayarlar_Load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Doldur();
            Cursor.Current = Cursors.Default;
        }

        private async void Doldur()
        {
            var kullanicis = await kullaniciAPI.KullaniciList();
                if (kullanicis!=null && kullanicis.Any())
                {
                    gridListeKullanici.DataSource = kullanicis.Select(x => new { x.Id, x.AdSoyad, x.KullaniciAd, x.Telefon }).ToList();
                   
                }
                Islemler.SabitVarsayilan(sabitAPI);
                var sabits=await sabitAPI.SabitList();
                var yazici = sabits.FirstOrDefault();
                chYazmaDurumu.Checked = (bool)yazici.Yazici;

                var sabitler=sabits.FirstOrDefault();
                txtKartKomisyon.Text = sabitler.KartKomisyon.ToString();
               
                var terazis=await teraziAPI.TeraziList();
                cbTeraziOnEk.DisplayMember = "TeraziOnEk";
                cbTeraziOnEk.ValueMember = "Id";

                if(terazis!=null && terazis.Any())
                {
                  cbTeraziOnEk.DataSource = terazis;
                }
               else
               {
                cbTeraziOnEk.DataSource= null;
               }
               

                txtIsyeriAdSoyad.Text = sabitler.AdSoyad;
                txtIsyeriUnvan.Text = sabitler.Unvan;
                txtIsyeriAdres.Text = sabitler.Adres;
                msIsyeriTelefon.Text = sabitler.Telefon;
            txtIsyeriEposta.Text = sabitler.Eposta;
            
        }

        private async void chYazmaDurumu_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chYazmaDurumu.Checked)
                {
                    Islemler.SabitVarsayilan(sabitAPI);
                    var sabits = await sabitAPI.SabitList();
                    var sabit = sabits.FirstOrDefault();
                    sabit.Yazici = true;
                    await sabitAPI.SabitUpdate(sabit.Id, sabit);
                    chYazmaDurumu.Text = "Yazma Durumu AKTİF";
                }
                else
                {
                    Islemler.SabitVarsayilan(sabitAPI);
                    var sabits = await sabitAPI.SabitList();
                    var sabit = sabits.FirstOrDefault();
                    sabit.Yazici = false;
                    await sabitAPI.SabitUpdate(sabit.Id, sabit);
                    chYazmaDurumu.Text = "Yazma Durumu PASİF";
                }
            }
            catch(CustomNotFoundException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Beklenmedik bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
               
        }

        private async void btnKartKomisyon_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtKartKomisyon.Text != "")
                {
                    var sabits = await sabitAPI.SabitList();
                    var sabit = sabits.FirstOrDefault();
                    sabit.KartKomisyon = Convert.ToInt16(txtKartKomisyon.Text);
                    await sabitAPI.SabitUpdate(sabit.Id, sabit);
                    MessageBox.Show("Kart komisyonu ayarlandı");

                }
                else
                {
                    MessageBox.Show("Kart komisyon bilgisini giriniz!");
                }
            }
            catch (CustomNotFoundException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Beklenmedik bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnTeraziOnEk_Click(object sender, EventArgs e)
        {
            if (cbTeraziOnEk.Text != "")
            {
                int onek = Convert.ToInt16(txtTeraziOnEk.Text);
                 var terazis=await teraziAPI.TeraziList();
                try
                {
                    if (terazis != null && terazis.Any(x => x.TeraziOnEk == onek))
                    {
                        MessageBox.Show(onek.ToString() + " ön ek zaten kayıtlı");
                    }
                    else
                    {
                        TeraziDTO t = new TeraziDTO();
                        t.TeraziOnEk = onek;
                        await teraziAPI.TeraziAdd(t);
                        MessageBox.Show("Bilgiler kaydedilmiştir");
                        cbTeraziOnEk.ValueMember = "Id";
                        cbTeraziOnEk.DisplayMember = "TeraziOnEk";
                        cbTeraziOnEk.DataSource = terazis.ToList();
                        txtTeraziOnEk.Clear();
                    }
                } 
                catch(CustomNotFoundException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Beklenmedik bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }         
            }
            else
            {
                MessageBox.Show("Lütfen terazi ön ek bilgisini giriniz!");
            }
        }

        private async void btnTeraziOnEksil_Click(object sender, EventArgs e)
        {
            if (cbTeraziOnEk.Text != "")
            {
                int onEkId = Convert.ToInt16(cbTeraziOnEk.SelectedValue);
                DialogResult onay=MessageBox.Show(cbTeraziOnEk.Text+" ön ekini silmek istiyor musunuz?","Terazi Ön Ek Silme İşlemi",MessageBoxButtons.YesNo);
                if (onay == DialogResult.Yes)
                {
                    try
                    {
                        await teraziAPI.TeraziDelete(onEkId);
                        MessageBox.Show("Ön Ek silinmiştir");
                        cbTeraziOnEk.ValueMember = "Id";
                        cbTeraziOnEk.DisplayMember = "TeraziOnEk";
                        var terazis = await teraziAPI.TeraziList();
                        if(terazis!=null)
                        {
                            cbTeraziOnEk.DataSource = terazis.ToList();
                        }
                        else
                        {
                            cbTeraziOnEk.DataSource= null;
                        }       
                    }
                    catch(CustomNotFoundException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("Beklenmedik bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Silme işlemi iptal edildi");
                }
            }
        }

        private async void btnIsyeriKaydet_Click(object sender, EventArgs e)
        {
            if (txtIsyeriAdSoyad.Text != "" && txtIsyeriUnvan.Text != "" && txtIsyeriAdres.Text != "" && msIsyeriTelefon.Text != "")
            {
                    var sabits=await sabitAPI.SabitList();
                    var isyeri = sabits.FirstOrDefault();
                    isyeri.AdSoyad = txtAdSoyad.Text;
                    isyeri.Unvan = txtIsyeriUnvan.Text;
                    isyeri.Adres = txtIsyeriAdres.Text;
                    isyeri.Telefon = msIsyeriTelefon.Text;
                    isyeri.Eposta = txtEposta.Text;
                await sabitAPI.SabitUpdate(isyeri.Id, isyeri);
                    MessageBox.Show("İşyeri bilgileri kaydedilmiştir");
                    
                    var yeni=sabits.FirstOrDefault();
                    txtAdSoyad.Text = yeni.AdSoyad;
                    txtIsyeriUnvan.Text= yeni.Unvan;
                    txtIsyeriAdres.Text= yeni.Adres;
                    msIsyeriTelefon.Text=yeni.Telefon;
                    txtEposta.Text= yeni.Eposta;

                
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

