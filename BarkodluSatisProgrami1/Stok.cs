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
using System.Data.Entity;
using BarkodluSatisProgrami1.Models;
using BarkodluSatisProgrami1.APIService;

namespace BarkodluSatisProgrami1
{
    public partial class Stok : Form
    {
        UrunAPI urunAPI;
        StokHareketAPI stokHareketAPI;
       UrunGrupAPI urunGrupAPI;
        public Stok()
        {
            InitializeComponent();
            urunAPI = new UrunAPI();
            stokHareketAPI = new StokHareketAPI();
            urunGrupAPI = new UrunGrupAPI();
        }


        private async void btnEkle_Click(object sender, EventArgs e)
        {
            gridListe.DataSource = null;

                if (cbIslemTuru.Text != "")
                {
                    string urungrubu=cbUrunGrubu.Text; ;
                    if(cbIslemTuru.SelectedIndex == 0)
                    {
                    var uruns = await urunAPI.UrunList();
                    if (rdTumu.Checked)
                        {
                        
                        if(uruns != null)
                        {
                         var result= uruns.OrderBy(a => a.Miktar).ToList();
                            gridListe.DataSource = result;
                        }
                        else
                        {
                            gridListe.DataSource = null;
                        }  
                        }
                        else if (rdUrunGrubunaGore.Checked)
                        {
                        if (uruns != null)
                        {
                            var result = uruns.Where(a => a.UrunGrup == urungrubu).OrderBy(x => x.Miktar).ToList();
                            gridListe.DataSource = result;
                        }
                        else
                        {
                            gridListe.DataSource = null; 
                        }
                        }
                        else
                        {
                            MessageBox.Show("Lütfen Filtreleme Türünü seçiniz!");
                        }
                    }
                    else if (cbIslemTuru.SelectedIndex == 1)
                    {
                        DateTime baslangic=DateTime.Parse(dtPickerBaslangicTarihi.Value.ToShortDateString());
                        DateTime bitis=DateTime.Parse(dtPickerBitisTarihi.Value.ToShortDateString());
                        bitis=bitis.AddDays(1);
                    var stokHarekets = await stokHareketAPI.StokHareketList();
                        if (rdTumu.Checked)
                        {
                        if (stokHarekets != null)
                        {
                          var result=  stokHarekets.OrderByDescending(a => a.Tarih).Where(x => x.Tarih >= baslangic && x.Tarih <= bitis).ToList();
                            gridListe.DataSource = result;
                        }
                        else
                        {
                            gridListe.DataSource = null; 
                        }
                            
                        }
                        else if (rdUrunGrubunaGore.Checked)
                        {
                        if (stokHarekets != null)
                        {
                            var result = stokHarekets.OrderByDescending(x => x.Tarih).Where(x => x.Tarih >= baslangic && x.Tarih <= bitis && x.UrunGrup.Contains(urungrubu)).ToList();
                            gridListe.DataSource = result;
                        }
                        else 
                        { 
                            gridListe.DataSource = null; 
                        }
                        }
                        else
                        {
                            MessageBox.Show("Lütfen Filtreleme Türünü seçiniz!");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen bir İşlem Türü seçiniz!");
                }
            
            Islemler.GridDuzenle(gridListe);
        }

   
        private async void Stok_Load(object sender, EventArgs e)
        {
            var urunGrups = await urunGrupAPI.UrunGrupList();
            cbUrunGrubu.DisplayMember = "UrunGrupAd";
            cbUrunGrubu.ValueMember = "Id";

            if(urunGrups != null)
            {
                cbUrunGrubu.DataSource = urunGrups.OrderBy(a => a.UrunGrupAd).ToList();
            }
            else
            {
                cbUrunGrubu.DataSource= null;
            }
            
        }

        private async void txtUrunAra_TextChanged(object sender, EventArgs e)
        {
            if(txtUrunAra.Text.Length > 2)
            {
                string urunad = txtUrunAra.Text;
                    var uruns=await urunAPI.UrunList();
                     var stokHarekets=await stokHareketAPI.StokHareketList();
                    if(cbIslemTuru.SelectedIndex == 0)
                    {
                    if (uruns != null)
                    {
                       var result= uruns.Where(x => x.UrunAd.Contains(urunad)).ToList();
                        gridListe.DataSource = result;
                    }
                    else
                    {
                        gridListe.DataSource = null;
                    }
                    }
                    else if (cbIslemTuru.SelectedIndex == 1)
                    {
                    if (stokHarekets != null)
                    {
                       var result= stokHarekets.Where(x => x.UrunAd.Contains(urunad)).ToList();
                        gridListe.DataSource = result;
                    }
                    else
                    {

                    gridListe.DataSource = null; }
                        
                    }
                
                Islemler.GridDuzenle(gridListe);
            }
        }

        private void btnRaporAl_Click(object sender, EventArgs e)
        {
            if (cbIslemTuru.SelectedIndex == 0)
            {
                Raporlar.Baslik = cbIslemTuru.Text + " RAPORU";
                Raporlar.TarihBaslangic=dtPickerBaslangicTarihi.Value.ToShortDateString();
                Raporlar.TarihBitis=dtPickerBitisTarihi.Value.ToShortDateString();
                Raporlar.StokRaporu(gridListe);
            }
            else if(cbIslemTuru.SelectedIndex== 1)
            {
                Raporlar.Baslik = cbIslemTuru.Text + " RAPORU";
                Raporlar.TarihBaslangic = dtPickerBaslangicTarihi.Value.ToShortDateString();
                Raporlar.TarihBitis = dtPickerBitisTarihi.Value.ToShortDateString();
                Raporlar.StokIzlemeRaporu(gridListe);
            }
        }
    }
}
