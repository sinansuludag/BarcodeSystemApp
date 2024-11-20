using BarkodluSatisProgrami1;
using BarkodluSatisProgrami1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BarkodluSatisProgrami1
{
    public partial class Rapor : Form
    {
        public Rapor()
        {
            InitializeComponent();
        }

        private void detayGösterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gridListe.Rows.Count > 0)
            {
                int islemno = Convert.ToInt32(gridListe.CurrentRow.Cells["IslemNo"].Value.ToString());
                if (islemno != 0)
                {
                    DetayGoster detayGoster = new DetayGoster();
                    detayGoster.islemno = islemno;
                    detayGoster.ShowDialog();
                }
            }
        }

        private void btnRaporAl_Click(object sender, EventArgs e)
        {
            Raporlar.Baslik = "GENEL RAPOR";
            Raporlar.SatisKart=txtSatisKart.Text;
            Raporlar.SatisNakit=txtSatisNakit.Text;
            Raporlar.IadeKart=txtIadeKart.Text;
            Raporlar.IadeNakit=txtIadeNakit.Text;
            Raporlar.GelirKart=txtGelirKart.Text;
            Raporlar.GelirNakit = txtGelirNakit.Text;
            Raporlar.GiderKart=txtGiderKart.Text;
            Raporlar.GiderNakit=txtGiderNakit.Text;
            Raporlar.TarihBaslangic = dtBaslangic.Value.ToShortDateString();
            Raporlar.TarihBitis = dtBitis.Value.ToShortDateString();
            Raporlar.KdvToplam = txtKdvtoplam.Text;
            Raporlar.KartKomisyon = txtKartKomisyon.Text;
            Raporlar.RaporSayfasiRaporu(gridListe);
        }

        private void Rapor_Load(object sender, EventArgs e)
        {
            listFiltrelemeTuru.SelectedIndex = 0;
            txtKartKomisyon.Text = Islemler.KartKomisyon().ToString();
            btnGoster_Click(null, null);
        }

        public void btnGoster_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            DateTime baslangic = DateTime.Parse(dtBaslangic.Value.ToShortDateString());
            DateTime bitis= DateTime.Parse(dtBitis.Value.ToShortDateString());
            bitis=bitis.AddDays(1);
            using(var db=new DbBarkodEntities())
            {
                if (listFiltrelemeTuru.SelectedIndex == 0)//Tümünü getir
                {
                    db.IslemOzets.Where(x => x.Tarih >= baslangic && x.Tarih <= bitis).OrderByDescending(x => x.Tarih).Load();
                    var islemOzet=db.IslemOzets.Local.ToBindingList();
                    gridListe.DataSource = islemOzet;
                    txtSatisNakit.Text = Convert.ToDouble(islemOzet.Where(x => x.Iade == false && x.Gelir == false && x.Gider == false).Sum(x => x.Nakit)).ToString("C2");
                    txtSatisKart.Text= Convert.ToDouble(islemOzet.Where(x => x.Iade == false && x.Gelir == false && x.Gider == false).Sum(x => x.Kart)).ToString("C2");

                    txtIadeNakit.Text = Convert.ToDouble(islemOzet.Where(x => x.Iade == true).Sum(x => x.Nakit)).ToString("C2");
                    txtIadeKart.Text = Convert.ToDouble(islemOzet.Where(x => x.Iade == true).Sum(x => x.Kart)).ToString("C2");

                    txtGelirNakit.Text = Convert.ToDouble(islemOzet.Where(x => x.Gelir == true).Sum(x => x.Nakit)).ToString("C2");
                    txtGelirKart.Text = Convert.ToDouble(islemOzet.Where(x => x.Gelir == true).Sum(x => x.Kart)).ToString("C2");

                    txtGiderNakit.Text = Convert.ToDouble(islemOzet.Where(x => x.Gider == true).Sum(x => x.Nakit)).ToString("C2");
                    txtGiderKart.Text = Convert.ToDouble(islemOzet.Where(x => x.Gider == true).Sum(x => x.Kart)).ToString("C2");

                    db.Satiss.Where(x => x.Tarih >= baslangic && x.Tarih <= bitis).Load();
                    var satisTablosu = db.Satiss.Local.ToBindingList();
                    double kdvTutariSatis = Islemler.DoubleYap(satisTablosu.Where(x => x.Iade == false).Sum(x => x.KdvTutari).ToString());
                    double kdvTutariIade = Islemler.DoubleYap(satisTablosu.Where(x => x.Iade == true).Sum(x => x.KdvTutari).ToString());
                    txtKdvtoplam.Text = (kdvTutariSatis - kdvTutariIade).ToString("C2");

                }
                else if (listFiltrelemeTuru.SelectedIndex == 1)//Satışlar
                {
                    db.IslemOzets.Where(x=>x.Tarih>=baslangic && x.Tarih<=bitis && x.Iade==false && x.Gelir==false && x.Gider==false).Load();
                    var islemozet=db.IslemOzets.Local.ToBindingList();
                    gridListe.DataSource = islemozet;
                }
                else if(listFiltrelemeTuru.SelectedIndex== 2)//Iadeler
                {
                    db.IslemOzets.Where(x=>x.Tarih>=baslangic && x.Tarih<=bitis && x.Iade==true).Load();
                    gridListe.DataSource = db.IslemOzets.Local.ToBindingList();
                }
                else if (listFiltrelemeTuru.SelectedIndex == 3)//Gelirler
                {
                    db.IslemOzets.Where(x => x.Tarih >= baslangic && x.Tarih <= bitis && x.Gelir == true).Load();
                    gridListe.DataSource = db.IslemOzets.Local.ToBindingList();
                }
                else if (listFiltrelemeTuru.SelectedIndex == 4)//Giderler
                {
                    db.IslemOzets.Where(x => x.Tarih >= baslangic && x.Tarih <= bitis && x.Gider == true).Load();
                    gridListe.DataSource = db.IslemOzets.Local.ToBindingList();
                }

            }
            Islemler.GridDuzenle(gridListe);
            Cursor.Current = Cursors.Default;
        }

        private void gridListe_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if(e.ColumnIndex == 2 || e.ColumnIndex==6 || e.ColumnIndex==7)
            {
                if(e.Value is bool)
                {
                    bool value= (bool)e.Value;
                    e.Value = (value) ? "Evet" : "Hayır";
                    e.FormattingApplied = true;
                }
            }
        }

        private void btnGelirEkle_Click(object sender, EventArgs e)
        {
            GelirGider gd = new GelirGider();
            gd.gelirgider = "GELİR";
            gd.kullanici = lblKullanici.Text;
            gd.ShowDialog();
        }

        private void btnGiderEkle_Click(object sender, EventArgs e)
        {
            GelirGider gd = new GelirGider();
            gd.gelirgider = "GİDER";
            gd.kullanici = lblKullanici.Text;
            gd.ShowDialog();
        }
    }
}
