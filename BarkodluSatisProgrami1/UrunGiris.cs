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

namespace BarkodluSatisProgrami1
{
    public partial class UrunGiris : Form
    {
        public UrunGiris()
        {
            InitializeComponent();
        }

        DbBarkodEntities db=new DbBarkodEntities();

        //Barkod oku varsa güncelle yoksa kaydet
        private void txtBarkod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string barkod=txtUrunGirisBarkod.Text.Trim();
                if (db.Uruns.Any(a => a.Barkod == barkod))
                {
                    var urun =db.Uruns.Where(a=>a.Barkod==barkod).SingleOrDefault();
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

        private void silToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(gridUrunler.Rows.Count > 0)
            {
                int urunId = Convert.ToInt32(gridUrunler.CurrentRow.Cells["UrunId"].Value.ToString());
                string urunAd = gridUrunler.CurrentRow.Cells["UrunAd"].Value.ToString();
                string barkod=gridUrunler.CurrentRow.Cells["Barkod"].ToString();
                DialogResult dialog = MessageBox.Show(urunAd + " ürününü silmek istiyor musunuz?", "Ürün silme işlemi", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                {
                    var urun = db.Uruns.Find(urunId);
                    db.Uruns.Remove(urun);
                    db.SaveChanges();
                    var hizliUrun = db.HizliUruns.Where(a => a.Barkod == barkod).SingleOrDefault();
                    hizliUrun.Barkod = "-";
                    hizliUrun.UrunAd = "-";
                    hizliUrun.Fiyat = 0;
                    db.SaveChanges();

                    MessageBox.Show("Ürün silinmiştir");
                    gridUrunler.DataSource = db.Uruns.OrderByDescending(a => a.UrunId).Take(20).ToList();
                    Islemler.GridDuzenle(gridUrunler);
                    txtUrunGirisBarkod.Focus();
                }
            }
            
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (txtUrunGirisBarkod.Text != "" && txtUrunAdi.Text != "" && cbUrunGrubu.Text != "" && txtAlisFiyati.Text != "" && txtSatisFiyati.Text != "" && txtKdvOrani.Text != "" && txtMiktar.Text != "")
            {
                if (db.Uruns.Any(a => a.Barkod == txtUrunGirisBarkod.Text))
                {
                    var guncelle = db.Uruns.Where(a => a.Barkod == txtUrunGirisBarkod.Text).SingleOrDefault();
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
                    db.SaveChanges();
                    MessageBox.Show("Ürün güncellenmiştir");
                    gridUrunler.DataSource = db.Uruns.OrderByDescending(a => a.UrunId).Take(10).ToList();

                }
                else
                {
                    Urun urun = new Urun();
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
                    db.Uruns.Add(urun);
                    db.SaveChanges();
                    MessageBox.Show("Ürün eklenmiştir.");
                    if (txtUrunGirisBarkod.Text.Length == 8)
                    {
                        var ozelBarkod=db.Barkods.First();
                        ozelBarkod.BarkodNo += 1;
                        db.SaveChanges();
                    }

                    gridUrunler.DataSource = db.Uruns.OrderByDescending(a => a.UrunId).Take(20).ToList();
                    Islemler.GridDuzenle(gridUrunler);
                }
                Islemler.StokHareket(txtUrunGirisBarkod.Text, txtUrunAdi.Text, "Adet", Convert.ToDouble(txtMiktar.Text), cbUrunGrubu.Text, lblKullanici.Text);
                Temizle();
            }
            else
            {
                MessageBox.Show("Lütfen boş alanları doldurunuz!");
                txtUrunGirisBarkod.Focus();
            }
        }

        private void txtUrunAra_TextChanged(object sender, EventArgs e)
        {
            if(txtUrunAra.Text.Length > 2)
            {
                string urunAd=txtUrunAra.Text;
                gridUrunler.DataSource=db.Uruns.Where(a=>a.UrunAd.Contains(urunAd)).ToList();
                Islemler.GridDuzenle(gridUrunler);
            }
        }

        private void UrunGiris_Load(object sender, EventArgs e)
        {
            txtUrunSayisi.Text=db.Uruns.Count().ToString();
            gridUrunler.DataSource = db.Uruns.OrderByDescending(a => a.UrunId).Take(20).ToList();
            Islemler.GridDuzenle(gridUrunler) ;
            GrupDoldur();
        }

        public void GrupDoldur()
        {
            cbUrunGrubu.DisplayMember = "UrunGrupAd";
            cbUrunGrubu.ValueMember = "Id";
            cbUrunGrubu.DataSource = db.UrunGrups.OrderBy(a => a.UrunGrupAd).ToList();
        }

        private void btnBarkodOlustur_Click(object sender, EventArgs e)
        {
            var barkodno=db.Barkods.First();
            int karakter=barkodno.BarkodNo.ToString().Length;
            string sifirlar=string.Empty;
            for(int i = 0; i < 8 - karakter; i++)
            {
                sifirlar = sifirlar + "0";
            }
            string olusanBarkod = sifirlar + barkodno.BarkodNo.ToString();
            txtUrunGirisBarkod.Text = olusanBarkod;
            txtUrunAdi.Focus();
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
                txtAlisFiyati.Text= gridUrunler.CurrentRow.Cells["AlisFiyat"].Value.ToString();
                txtSatisFiyati.Text= gridUrunler.CurrentRow.Cells["SatisFiyat"].Value.ToString();
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
    }
}
