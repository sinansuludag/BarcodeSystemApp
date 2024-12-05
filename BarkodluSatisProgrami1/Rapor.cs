using BarkodluSatisProgrami1;
using BarkodluSatisProgrami1.APIService;
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
        IslemOzetAPI islemOzetAPI;
        SatisAPI satisAPI;
        SabitAPI sabitAPI;
        public Rapor()
        {
            InitializeComponent();
            islemOzetAPI = new IslemOzetAPI();
            satisAPI = new SatisAPI();
            sabitAPI = new SabitAPI();
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
            txtKartKomisyon.Text = Islemler.KartKomisyon(sabitAPI).ToString();
            btnGoster_Click(null, null);
        }

        public async void btnGoster_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            DateTime baslangic = DateTime.Parse(dtBaslangic.Value.ToShortDateString());
            DateTime bitis = DateTime.Parse(dtBitis.Value.ToShortDateString());
            bitis = bitis.AddDays(1);
            var islemOzets = await islemOzetAPI.IslemOzetList();
            var satiss = await satisAPI.SatisList();
            if (listFiltrelemeTuru.SelectedIndex == 0)//Tümünü getir
            {
                if (islemOzets != null)
                {
                    var result1 = islemOzets.Where(x => x.Tarih >= baslangic && x.Tarih <= bitis).OrderByDescending(x => x.Tarih).ToList();
                    var islemOzet = result1;
                    gridListe.DataSource = islemOzet;
                    txtSatisNakit.Text = Convert.ToDouble(islemOzet.Where(x => x.Iade == false && x.Gelir == false && x.Gider == false).Sum(x => x.Nakit)).ToString("C2");
                    txtSatisKart.Text = Convert.ToDouble(islemOzet.Where(x => x.Iade == false && x.Gelir == false && x.Gider == false).Sum(x => x.Kart)).ToString("C2");

                    txtIadeNakit.Text = Convert.ToDouble(islemOzet.Where(x => x.Iade == true).Sum(x => x.Nakit)).ToString("C2");
                    txtIadeKart.Text = Convert.ToDouble(islemOzet.Where(x => x.Iade == true).Sum(x => x.Kart)).ToString("C2");

                    txtGelirNakit.Text = Convert.ToDouble(islemOzet.Where(x => x.Gelir == true).Sum(x => x.Nakit)).ToString("C2");
                    txtGelirKart.Text = Convert.ToDouble(islemOzet.Where(x => x.Gelir == true).Sum(x => x.Kart)).ToString("C2");

                    txtGiderNakit.Text = Convert.ToDouble(islemOzet.Where(x => x.Gider == true).Sum(x => x.Nakit)).ToString("C2");
                    txtGiderKart.Text = Convert.ToDouble(islemOzet.Where(x => x.Gider == true).Sum(x => x.Kart)).ToString("C2");

                    var result2 = satiss.Where(x => x.Tarih >= baslangic && x.Tarih <= bitis).ToList();
                    var satisTablosu = result2;
                    double kdvTutariSatis = Islemler.DoubleYap(satisTablosu.Where(x => x.Iade == false).Sum(x => x.KdvTutari).ToString());
                    double kdvTutariIade = Islemler.DoubleYap(satisTablosu.Where(x => x.Iade == true).Sum(x => x.KdvTutari).ToString());
                    txtKdvtoplam.Text = (kdvTutariSatis - kdvTutariIade).ToString("C2");
                }
                else
                {
                    gridListe.DataSource = null;
                    txtSatisNakit.Text = 0.ToString("C2");
                    txtSatisKart.Text = 0.ToString("C2");
                    txtIadeNakit.Text = 0.ToString("C2");
                    txtIadeKart.Text = 0.ToString("C2");
                    txtGelirNakit.Text = 0.ToString("C2");
                    txtGelirKart.Text = 0.ToString("C2");
                    txtGiderNakit.Text = 0.ToString("C2");
                    txtGiderKart.Text = 0.ToString("C2");
                    txtKdvtoplam.Text = 0.ToString("C2");
                }


            }
            else if (listFiltrelemeTuru.SelectedIndex == 1)//Satışlar
            {
                if (islemOzets != null)
                {
                    var result = islemOzets.Where(x => x.Tarih >= baslangic && x.Tarih <= bitis && x.Iade == false && x.Gelir == false && x.Gider == false).ToList();
                    gridListe.DataSource = result;
                }
                else
                {
                    gridListe.DataSource = null;
                }

            }
            else if (listFiltrelemeTuru.SelectedIndex == 2)//Iadeler
            {
                if (islemOzets != null)
                {
                    var result = islemOzets.Where(x => x.Tarih >= baslangic && x.Tarih <= bitis && x.Iade == true).ToList();
                    gridListe.DataSource = result;
                }
                else
                {
                    gridListe.DataSource = null;
                }

            }
            else if (listFiltrelemeTuru.SelectedIndex == 3)//Gelirler
            {
                if (islemOzets != null)
                {
                    var result = islemOzets.Where(x => x.Tarih >= baslangic && x.Tarih <= bitis && x.Gelir == true).ToList();
                    gridListe.DataSource = result;
                }
                else
                {
                    gridListe.DataSource = null;
                }

            }
            else if (listFiltrelemeTuru.SelectedIndex == 4)//Giderler
            {
                if (islemOzets != null)
                {
                    var result = islemOzets.Where(x => x.Tarih >= baslangic && x.Tarih <= bitis && x.Gider == true).ToList();
                    gridListe.DataSource = result;
                }
                else
                {
                    gridListe.DataSource = null;
                }


                Islemler.GridDuzenle(gridListe);
                Cursor.Current = Cursors.Default;
            }
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
