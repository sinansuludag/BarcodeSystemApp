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
using BarkodluSatisProgrami1;
using BarkodluSatisProgrami1.Models;

namespace BarkodluSatisProgrami1
{
    public partial class HizliButonUrunEkle : Form
    {
        public HizliButonUrunEkle()
        {
            InitializeComponent();
        }

        DbBarkodEntities db=new DbBarkodEntities();

        //Giren karaktere gore urunleri listele
        private void txtUrunAra_TextChanged(object sender, EventArgs e)
        {
            if (txtUrunAra.Text != "")
            {
                string urunAd=txtUrunAra.Text;
                var urunler= db.Uruns.Where(a=>a.UrunAd.Contains(urunAd)).ToList();
                gridUrunler.DataSource = urunler;
                Islemler.GridDuzenle(gridUrunler);
            }
        }

        //İki kere tiklandiğinda hizli butona ekle
        private void gridUrunler_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gridUrunler.Rows.Count > 0)
            {
                string barkod = gridUrunler.CurrentRow.Cells["Barkod"].Value.ToString();
                string urunAd = gridUrunler.CurrentRow.Cells["UrunAd"].Value.ToString();
                double fiyat = Convert.ToDouble(gridUrunler.CurrentRow.Cells["SatisFiyati"].Value.ToString());
                int id=Convert.ToInt16(lblButonNo.Text);
                var guncellenecek=db.HizliUruns.Find(id);
                guncellenecek.Barkod=barkod;
                guncellenecek.UrunAd=urunAd;
                guncellenecek.Fiyat=fiyat;
                db.SaveChanges();
                MessageBox.Show("Buton tanımlanmıştır");
                fSatis satis = (fSatis)Application.OpenForms["fSatis"];
                if (satis != null)
                {
                    Button bH = satis.Controls.Find("bH" + id, true).FirstOrDefault() as Button;
                    bH.Text = urunAd + "\n" + fiyat.ToString("C2");
                }
            }
        }

        //Tümünü göstere tıkla
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (chUrunGoster.Checked)
            {
                gridUrunler.DataSource=db.Uruns.ToList();
                gridUrunler.Columns["AlisFiyati"].Visible = false;
                gridUrunler.Columns["Satisfiyati"].Visible = false;
                gridUrunler.Columns["KdvOrani"].Visible = false;
                gridUrunler.Columns["KdvTutari"].Visible = false;
                gridUrunler.Columns["Miktar"].Visible = false;
                Islemler.GridDuzenle(gridUrunler);
            }
            else
            {
                gridUrunler.DataSource = null;
            }
        }

        private void HizliButonUrunEkle_Load(object sender, EventArgs e)
        {

        }

        private void gridUrunler_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
