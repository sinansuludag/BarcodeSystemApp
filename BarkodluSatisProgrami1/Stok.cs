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

namespace BarkodluSatisProgrami1
{
    public partial class Stok : Form
    {
        public Stok()
        {
            InitializeComponent();
        }


        private void btnEkle_Click(object sender, EventArgs e)
        {
            gridListe.DataSource = null;
            using(var db=new DbBarkodEntities())
            {
                if (cbIslemTuru.Text != "")
                {
                    string urungrubu=cbUrunGrubu.Text; ;
                    if(cbIslemTuru.SelectedIndex == 0)
                    {
                        if (rdTumu.Checked)
                        {
                            db.Uruns.OrderBy(a => a.Miktar).Load();
                            gridListe.DataSource = db.Uruns.Local.ToBindingList();
                        }
                        else if (rdUrunGrubunaGore.Checked)
                        {
                            db.Uruns.Where(a=>a.UrunGrup==urungrubu).OrderBy(x=>x.Miktar).Load();
                            gridListe.DataSource=db.Uruns.Local.ToBindingList();
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
                        if (rdTumu.Checked)
                        {
                            db.StokHarekets.OrderByDescending(a=>a.Tarih).Where(x=>x.Tarih>=baslangic && x.Tarih<=bitis).Load();
                            gridListe.DataSource=db.StokHarekets.Local.ToBindingList();
                        }
                        else if (rdUrunGrubunaGore.Checked)
                        {
                            db.StokHarekets.OrderByDescending(x=>x.Tarih).Where(x=>x.Tarih>=baslangic && x.Tarih<=bitis && x.UrunGrup.Contains(urungrubu)).Load();
                            gridListe.DataSource = db.StokHarekets.Local.ToBindingList();
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
            }
            Islemler.GridDuzenle(gridListe);
        }

        DbBarkodEntities dbx= new DbBarkodEntities();
        private void Stok_Load(object sender, EventArgs e)
        {
            cbUrunGrubu.DisplayMember = "UrunGrupAd";
            cbUrunGrubu.ValueMember = "Id";
            cbUrunGrubu.DataSource = dbx.UrunGrups.OrderBy(a => a.UrunGrupAd).ToList();
        }

        private void txtUrunAra_TextChanged(object sender, EventArgs e)
        {
            if(txtUrunAra.Text.Length > 2)
            {
                string urunad = txtUrunAra.Text;
                using(var db= new DbBarkodEntities())
                {
                    if(cbIslemTuru.SelectedIndex == 0)
                    {
                        db.Uruns.Where(x=>x.UrunAd.Contains(urunad)).Load();
                        gridListe.DataSource=db.Uruns.Local.ToBindingList();
                    }
                    else if (cbIslemTuru.SelectedIndex == 1)
                    {
                        db.StokHarekets.Where(x => x.UrunAd.Contains(urunad)).Load();
                        gridListe.DataSource=db.StokHarekets.Local.ToBindingList();
                    }
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
