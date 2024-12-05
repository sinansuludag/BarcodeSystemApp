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
using BarkodluSatisProgrami1.APIService;
using BarkodluSatisProgrami1.Exceptions;

namespace BarkodluSatisProgrami1
{
    public partial class HizliButonUrunEkle : Form
    {
        UrunAPI urunAPI;
        HizliUrunAPI hizliUrunAPI;
        public HizliButonUrunEkle()
        {
            InitializeComponent();
            urunAPI = new UrunAPI();
            hizliUrunAPI = new HizliUrunAPI();
        }



        //Giren karaktere gore urunleri listele
        private async void txtUrunAra_TextChanged(object sender, EventArgs e)
        {
            if (txtUrunAra.Text != "")
            {
                var uruns = await urunAPI.UrunList();
                string urunAd=txtUrunAra.Text;

                if (uruns != null)
                {
                    var urunler = uruns.Where(a => a.UrunAd.Contains(urunAd)).ToList();
                    gridUrunler.DataSource = urunler;
                }
                else
                {
                    gridUrunler.DataSource = null;
                }
                
                Islemler.GridDuzenle(gridUrunler);
            }
        }

        //İki kere tiklandiğinda hizli butona ekle
        private async void gridUrunler_CellContentDoubleClickAsync(object sender, DataGridViewCellEventArgs e)
        {
            if (gridUrunler.Rows.Count > 0)
            {
                string barkod = gridUrunler.CurrentRow.Cells["Barkod"].Value.ToString();
                string urunAd = gridUrunler.CurrentRow.Cells["UrunAd"].Value.ToString();
                double fiyat = Convert.ToDouble(gridUrunler.CurrentRow.Cells["SatisFiyati"].Value.ToString());
                int id=Convert.ToInt16(lblButonNo.Text);

                var guncellenecek=await hizliUrunAPI.HizliUrunGetById(id);
                guncellenecek.Barkod=barkod;
                guncellenecek.UrunAd=urunAd;
                guncellenecek.Fiyat=fiyat;
                try
                {
                    await hizliUrunAPI.HizliUrunUpdate(id, guncellenecek);
                }
                catch(CustomNotFoundException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Beklenmedik bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

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
        private async void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (chUrunGoster.Checked)
            {
                var uruns = await urunAPI.UrunList();
                if (uruns != null)
                {
                    gridUrunler.DataSource = uruns;
                    gridUrunler.Columns["AlisFiyati"].Visible = false;
                    gridUrunler.Columns["Satisfiyati"].Visible = false;
                    gridUrunler.Columns["KdvOrani"].Visible = false;
                    gridUrunler.Columns["KdvTutari"].Visible = false;
                    gridUrunler.Columns["Miktar"].Visible = false;
                }
                else
                {
                    gridUrunler.DataSource=null;
                }
                
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
