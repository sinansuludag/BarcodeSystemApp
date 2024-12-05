using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using BarkodluSatisProgrami1.Models;
using BarkodluSatisProgrami1.APIService;
using BarkodluSatisProgrami1.Models.FormDTO;
using BarkodluSatisProgrami1.Exceptions;

namespace BarkodluSatisProgrami1
{
    public partial class UrunGiris : Form
    {
        UrunAPI urunAPI;
        BarkodAPI barkodAPI;
        UrunGrupAPI urunGrupAPI;
        HizliUrunAPI hizliUrunAPI;
        public UrunGiris()
        {
            InitializeComponent();
            urunAPI = new UrunAPI();
            barkodAPI = new BarkodAPI();
            urunGrupAPI = new UrunGrupAPI();
            hizliUrunAPI = new HizliUrunAPI();
        }



        //Barkod oku varsa güncelle yoksa kaydet
        private async void txtBarkod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var uruns = await urunAPI.UrunList();
                string barkod=txtUrunGirisBarkod.Text.Trim();
                if (uruns != null && uruns.Any(a => a.Barkod == barkod))
                {
                    var urun =uruns.Where(a=>a.Barkod==barkod).SingleOrDefault();
                    txtUrunAdi.Text = urun.UrunAd;
                    txtAciklama.Text = urun.Aciklama;
                    cbUrunGrubu.Text = urun.UrunGrup;
                    txtAlisFiyati.Text = urun.AlisFiyati.ToString();
                    txtSatisFiyati.Text = urun.SatisFiyati.ToString();
                    txtMiktar.Text = urun.Miktar.ToString();
                    txtKdvOrani.Text=urun.KdvOrani.ToString();
                    if (urun.Birim == "Kg")
                    {
                        chUrunTipi.Checked = true;
                    }
                    else
                    {
                        chUrunTipi.Checked=false;
                    }
                }
                else
                {
                    MessageBox.Show("Ürün kayıtlı değil, kaydedebilirsiniz!");
                }

            }
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            Temizle();
            MessageBox.Show("İşleminiz iptal edilmiştir");
        }


        private void Temizle()
        {
            txtUrunGirisBarkod.Clear();
            txtAciklama.Clear();
            txtAlisFiyati.Text = "0";
            txtSatisFiyati.Text = "0";
            txtKdvOrani.Text = "8";
            txtUrunAdi.Clear();
            txtMiktar.Text = "0";
            cbUrunGrubu.Text = "";
            txtUrunAra.Clear();
            txtUrunSayisi.Clear();
            txtUrunGirisBarkod.Focus();
            chUrunTipi.Checked=false ;
        }

        private void btnUrunGrupEkle_Click(object sender, EventArgs e)
        {
            UrunGrubuEkle urunGrubuEkle = new UrunGrubuEkle();
            urunGrubuEkle.ShowDialog();
        }

        //harf karakterlerini almasın
        private void txtAlisFiyati_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == false && e.KeyChar != (char)08 && e.KeyChar != (char)44)
            {
                e.Handled = true;
            }
        }

        private async void silToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(gridUrunler.Rows.Count > 0)
            {
                int urunId = Convert.ToInt32(gridUrunler.CurrentRow.Cells["UrunId"].Value.ToString());
                string urunAd = gridUrunler.CurrentRow.Cells["UrunAd"].Value.ToString();
                string barkod=gridUrunler.CurrentRow.Cells["Barkod"].ToString();
                DialogResult dialog = MessageBox.Show(urunAd + " ürününü silmek istiyor musunuz?", "Ürün silme işlemi", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                {
                    try
                    {
                        await urunAPI.UrunDelete(urunId);
                        MessageBox.Show("Ürün silinmiştir");

                        var hizliUruns = await hizliUrunAPI.HizliUrunList();
                        if (hizliUruns != null)
                        {
                            var hizliUrun = hizliUruns.Where(a => a.Barkod == barkod).SingleOrDefault();
                            hizliUrun.Barkod = "-";
                            hizliUrun.UrunAd = "-";
                            hizliUrun.Fiyat = 0;
                            await hizliUrunAPI.HizliUrunUpdate(urunId, hizliUrun);
                        }



                        gridUrunler.DataSource = await urunAPI.UrunList();
                        Islemler.GridDuzenle(gridUrunler);
                        txtUrunGirisBarkod.Focus();
                    }
                    catch (CustomNotFoundException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show($"Ürün silinmedi ,HATA :{ex.Message}");
                    }

                   
                }
            }
            
        }

        private async void btnKaydet_Click(object sender, EventArgs e)
        {
            if (txtUrunGirisBarkod.Text != "" && txtUrunAdi.Text != "" && cbUrunGrubu.Text != "" && txtAlisFiyati.Text != "" && txtSatisFiyati.Text != "" && txtKdvOrani.Text != "" && txtMiktar.Text != "")
            {
                var uruns=await urunAPI.UrunList();
                //APIde barkoda gore getir fonkunu ekleyebiliriz(id dışında)
                if (uruns!=null && uruns.Any(a => a.Barkod == txtUrunGirisBarkod.Text))
                {
                    var guncelle = uruns.Where(a => a.Barkod == txtUrunGirisBarkod.Text).SingleOrDefault();
                    guncelle.UrunAd = txtUrunAdi.Text;
                    guncelle.Aciklama = txtAciklama.Text;
                    guncelle.UrunGrup = cbUrunGrubu.Text;
                    guncelle.AlisFiyati = Convert.ToDouble(txtAlisFiyati.Text);
                    guncelle.SatisFiyati = Convert.ToDouble(txtSatisFiyati.Text);
                    guncelle.KdvOrani = Convert.ToInt32(txtKdvOrani.Text);
                    guncelle.KdvTutari = Math.Round(Islemler.DoubleYap(txtSatisFiyati.Text) * Convert.ToInt32(txtKdvOrani.Text) / 100, 2);
                    guncelle.Miktar += Convert.ToDouble(txtMiktar.Text);
                    if (chUrunTipi.Checked)
                    {
                        guncelle.Birim = "Kg";
                    }
                    else
                    {
                        guncelle.Birim = "Adet";
                    }
                    guncelle.Tarih = DateTime.Now;
                    guncelle.Kullanici = lblKullanici.Text;
                    try
                    {
                        await urunAPI.UrunUpdate(guncelle.UrunId, guncelle);

                        MessageBox.Show("Ürün güncellenmiştir");
                        gridUrunler.DataSource = await urunAPI.UrunList();

                    }
                    catch (CustomNotFoundException ex)
                    {
                        MessageBox.Show(ex.Message); 
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show($"Güncelleme başarısız ,HATA :{ex.Message}");
                    }
                    
                    //gridUrunler.DataSource = db.Uruns.OrderByDescending(a => a.UrunId).Take(10).ToList();
                    

                }
                else
                {
                    UrunDTO urun = new UrunDTO();
                    urun.Barkod = txtUrunGirisBarkod.Text;
                    urun.UrunAd = txtUrunAdi.Text;
                    urun.Aciklama = txtAciklama.Text;
                    urun.UrunGrup = cbUrunGrubu.Text;
                    urun.AlisFiyati = Convert.ToDouble(txtAlisFiyati.Text);
                    urun.SatisFiyati = Convert.ToDouble(txtSatisFiyati.Text);
                    urun.KdvOrani = Convert.ToInt32(txtKdvOrani.Text);
                    urun.KdvTutari = Math.Round(Islemler.DoubleYap(txtSatisFiyati.Text) * Convert.ToInt32(txtKdvOrani.Text) / 100, 2);
                    urun.Miktar = Convert.ToDouble(txtMiktar.Text);
                    if (chUrunTipi.Checked)
                    {
                        urun.Birim = "Kg";
                    }
                    else
                    {
                        urun.Birim = "Adet";
                    }
                    urun.Tarih = DateTime.Now;
                    urun.Kullanici = lblKullanici.Text;
                    try
                    {
                        await urunAPI.UrunAdd(urun);
                        MessageBox.Show("Ürün eklenmiştir.");
                        if (txtUrunGirisBarkod.Text.Length == 8)
                        {
                            var ozelBarkod =await barkodAPI.BarkodList();
                            if (ozelBarkod!=null && ozelBarkod.Count == 1)
                            {
                                var id = ozelBarkod[0].Id;
                                var barkodNo=ozelBarkod[0].BarkodNo++;
                                BarkodDTO barkodDTO = new BarkodDTO();
                                barkodDTO.BarkodNo= barkodNo;
                                await barkodAPI.BarkodUpdate(id,barkodDTO);
                            }
                            else if (ozelBarkod ==null)
                            {
                                BarkodDTO barkodDTO=new BarkodDTO();
                                barkodDTO.BarkodNo = 1;
                                await barkodAPI.BarkodAdd(barkodDTO);
                            }
                        }

                        gridUrunler.DataSource = await urunAPI.UrunList();

                    }
                    catch(CustomNotFoundException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    catch(Exception ex)
                    {
                        MessageBox.Show("Beklenmedik bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                  

                    //gridUrunler.DataSource = db.Uruns.OrderByDescending(a => a.UrunId).Take(20).ToList();
                    
                    Islemler.GridDuzenle(gridUrunler);
                }
                Islemler.StokHareketAsync(txtUrunGirisBarkod.Text, txtUrunAdi.Text, "Adet", Convert.ToDouble(txtMiktar.Text), cbUrunGrubu.Text, lblKullanici.Text);
                Temizle();
            }
            else
            {
                MessageBox.Show("Lütfen boş alanları doldurunuz!");
                txtUrunGirisBarkod.Focus();
            }
        }



        private async void UrunGiris_Load(object sender, EventArgs e)
        {
            
            try
            {
                var urunList = await urunAPI.UrunList();
                if (urunList == null)
                    txtUrunSayisi.Text = "0";
                else
                    txtUrunSayisi.Text = urunList.Count.ToString();

                gridUrunler.DataSource = urunList;
                Islemler.GridDuzenle(gridUrunler);
                GrupDoldur();
            }
            catch (CustomNotFoundException ex) // Özel exception için
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex) // Diğer genel hatalar için
            {
                MessageBox.Show("Beklenmedik bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public async void GrupDoldur()
        {
            try
            {
                var urunGrups = await urunGrupAPI.UrunGrupList();
                cbUrunGrubu.DisplayMember = "UrunGrupAd";
                cbUrunGrubu.ValueMember = "Id";
                if (urunGrups == null)
                {
                    cbUrunGrubu.DataSource = null;
                }     
                else
                {
                    cbUrunGrubu.DataSource = urunGrups.OrderBy(a => a.UrunGrupAd).ToList();
                    cbUrunGrubu.SelectedIndex = 0;
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

        private async void btnBarkodOlustur_Click(object sender, EventArgs e)
        {
            try
            {
                var barkods = await barkodAPI.BarkodList();
                if (barkods !=null && barkods.Count == 1)
                {
                    var barkodno = barkods[0].BarkodNo;
                    int karakter = barkodno.ToString().Length;
                    string sifirlar = string.Empty;
                    for (int i = 0; i < 8 - karakter; i++)
                    {
                        sifirlar = sifirlar + "0";
                    }
                    string olusanBarkod = sifirlar + barkodno.ToString();
                    txtUrunGirisBarkod.Text = olusanBarkod;
                    txtUrunAdi.Focus();
                }
                else if (barkods==null)
                {
                    BarkodDTO barkodDTO = new BarkodDTO();
                    barkodDTO.BarkodNo = 1;
                    await barkodAPI.BarkodAdd(barkodDTO);
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

        private void chUrunTipi_CheckedChanged(object sender, EventArgs e)
        {
            if(chUrunTipi.Checked)
            {
                chUrunTipi.Text = "Gramajlı Ürün İşlemi";
                btnBarkodOlustur.Enabled = false;
            }
            else
            {
                chUrunTipi.Text = "Barkodlu Ürün İşlemi";
                btnBarkodOlustur.Enabled=true;
            }
        }

        private void düzenleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(gridUrunler.Rows.Count > 0)
            {
                txtUrunGirisBarkod.Text = gridUrunler.CurrentRow.Cells["Barkod"].Value.ToString();
                txtUrunAdi.Text= gridUrunler.CurrentRow.Cells["UrunAd"].Value.ToString();
                txtAciklama.Text= gridUrunler.CurrentRow.Cells["Aciklama"].Value.ToString();
                cbUrunGrubu.Text= gridUrunler.CurrentRow.Cells["UrunGrup"].Value.ToString();
                txtAlisFiyati.Text= gridUrunler.CurrentRow.Cells["AlisFiyati"].Value.ToString();
                txtSatisFiyati.Text= gridUrunler.CurrentRow.Cells["SatisFiyati"].Value.ToString();
                txtKdvOrani.Text= gridUrunler.CurrentRow.Cells["KdvOrani"].Value.ToString();
                txtMiktar.Text= gridUrunler.CurrentRow.Cells["Miktar"].Value.ToString();
                string birim= gridUrunler.CurrentRow.Cells["Birim"].Value.ToString();
                if (birim == "Kg")
                {
                    chUrunTipi.Checked = true;
                }
                else
                {
                    chUrunTipi.Checked= false;
                }
            }
        }

        private void btnRaporAl_Click(object sender, EventArgs e)
        {

        }

        private async void txtUrunAra_TextChanged(object sender, EventArgs e)
        {

            if (txtUrunAra.Text.Length > 2)
            {
                try
                {
                    var uruns = await urunAPI.UrunList();
                    string urunAd = txtUrunAra.Text;
                    if(uruns!=null)
                    {
                        gridUrunler.DataSource = uruns.Where(a => a.UrunAd.Contains(urunAd)).ToList();
                    }
                    else
                    {
                        gridUrunler.DataSource = null;
                    }
                    
                    Islemler.GridDuzenle(gridUrunler);
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
    }
}
