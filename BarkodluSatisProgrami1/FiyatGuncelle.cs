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
        public FiyatGuncelle()
        {
            InitializeComponent();
        }

        private void txtBarkod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                using(var db =new DbBarkodEntities())
                {
                    if (db.Uruns.Any(x => x.Barkod == txtBarkod.Text))
                    {
                        var getir = db.Uruns.Where(x => x.Barkod == txtBarkod.Text).SingleOrDefault();
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
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if(txtYeniFiyat.Text!="" && lblBarkod.Text != "")
            {
                using (var db = new DbBarkodEntities())
                {
                    var guncellenecek=db.Uruns.Where(x=>x.Barkod==lblBarkod.Text).SingleOrDefault();
                    guncellenecek.SatisFiyati = Islemler.DoubleYap(txtYeniFiyat.Text);
                    int kdvOrani = Convert.ToInt16(guncellenecek.KdvOrani);
                    Math.Round(Islemler.DoubleYap(txtYeniFiyat.Text) *kdvOrani / 100, 2);
                    db.SaveChanges();
                    var guncelleHizliButon = db.HizliUruns.Where(x => x.Barkod == lblBarkod.Text).SingleOrDefault();
                    guncelleHizliButon.Fiyat= Islemler.DoubleYap(txtYeniFiyat.Text);
                    db.SaveChanges();
                    MessageBox.Show("Ürün fiyati güncellenmiştir");
                    lblBarkod.Text = "";
                    lblUrunAd.Text = "";
                    lblFiyat.Text = "";
                    txtYeniFiyat.Clear();
                    txtBarkod.Clear();
                    txtBarkod.Focus();
                }
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
