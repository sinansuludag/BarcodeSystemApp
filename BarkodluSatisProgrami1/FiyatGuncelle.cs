using BarkodluSatisProgrami1.APIService;
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
    public partial class FiyatGuncelle : Form
    {
        UrunAPI urunAPI;
        HizliUrunAPI hizliUrunAPI;
        public FiyatGuncelle()
        {
            InitializeComponent();
            urunAPI = new UrunAPI();
            hizliUrunAPI=new HizliUrunAPI();    
        }

        private async void txtBarkod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Enter'ın varsayılan davranışını engelle
                e.SuppressKeyPress = true;

                var uruns = await urunAPI.UrunList();
                if (uruns != null && uruns.Any(x => x.Barkod == txtBarkod.Text))
                {
                    var getir = uruns.Where(x => x.Barkod == txtBarkod.Text).SingleOrDefault();
                    lblBarkod.Text = getir.Barkod;
                    lblUrunAd.Text = getir.UrunAd;
                    double mevcutfiyat = Convert.ToDouble(getir.SatisFiyati);
                    lblFiyat.Text = mevcutfiyat.ToString("C2");
                }
                else
                {
                    MessageBox.Show(txtBarkod.Text + " barkodlu ürün kayıtlı değil!");
                }
            }
        }


        private async void btnKaydet_Click(object sender, EventArgs e)
        {
            if(txtYeniFiyat.Text!="" && lblBarkod.Text != "")
            {
                var hizliUruns = await hizliUrunAPI.HizliUrunList();
                var uruns=await urunAPI.UrunList();
                    var guncellenecek=uruns.Where(x=>x.Barkod==lblBarkod.Text).SingleOrDefault();
                    guncellenecek.SatisFiyati = Islemler.DoubleYap(txtYeniFiyat.Text);
                    int kdvOrani = Convert.ToInt16(guncellenecek.KdvOrani);
                    Math.Round(Islemler.DoubleYap(txtYeniFiyat.Text) *kdvOrani / 100, 2);
                await urunAPI.UrunUpdate(guncellenecek.UrunId,guncellenecek);
                    var guncelleHizliButon = hizliUruns.Where(x => x.Barkod == lblBarkod.Text).SingleOrDefault();
                    guncelleHizliButon.Fiyat= Islemler.DoubleYap(txtYeniFiyat.Text);
                await hizliUrunAPI.HizliUrunUpdate(guncelleHizliButon.Id,guncelleHizliButon);
                    MessageBox.Show("Ürün fiyati güncellenmiştir");
                    lblBarkod.Text = "";
                    lblUrunAd.Text = "";
                    lblFiyat.Text = "";
                    txtYeniFiyat.Clear();
                    txtBarkod.Clear();
                    txtBarkod.Focus();
            }
            else
            {
                MessageBox.Show("Lütfen ürün barkodunu giriniz!");
                txtBarkod.Focus();
            }
        }

        private void FiyatGuncelle_Load(object sender, EventArgs e)
        {
            lblBarkod.Text = "";
            lblUrunAd.Text = "";
            lblFiyat.Text = "";
        }
    }
}
