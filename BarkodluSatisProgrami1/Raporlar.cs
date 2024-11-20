using BarkodluSatisProgrami1.Models;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BarkodluSatisProgrami1
{
    static class Raporlar
    {
        public static string Baslik { get; set; }
        public static string TarihBaslangic { get; set; }
        public static string TarihBitis { get; set; }
        public static string SatisNakit { get; set; }
        public static string SatisKart { get; set; }
        public static string IadeNakit { get; set; }
        public static string IadeKart { get; set; }
        public static string GelirNakit { get; set; }
        public static string GelirKart { get; set; }
        public static string GiderNakit { get; set; }
        public static string GiderKart { get; set; }
        public static string KdvToplam { get; set; }
        public static string KartKomisyon { get; set; }


        public static void RaporSayfasiRaporu(DataGridView dgv)
        {
            Cursor.Current = Cursors.WaitCursor;
            List<IslemOzet> list = new List<IslemOzet>();
            list.Clear();
            for(int i = 0; i < dgv.Rows.Count; i++)
            {
                list.Add(new IslemOzet
                {
                    IslemNo = Convert.ToInt32(dgv.Rows[i].Cells["IslemNo"].Value.ToString()),
                    Iade = Convert.ToBoolean(dgv.Rows[i].Cells["Iade"].Value),
                    OdemeSekli= dgv.Rows[i].Cells["OdemeSekli"].Value.ToString(),
                    Nakit=Islemler.DoubleYap(dgv.Rows[i].Cells["Nakit"].Value.ToString()),
                    Kart= Islemler.DoubleYap(dgv.Rows[i].Cells["Kart"].Value.ToString()),
                    Gelir=Convert.ToBoolean(dgv.Rows[i].Cells["Gelir"].Value.ToString()),
                    Gider= Convert.ToBoolean(dgv.Rows[i].Cells["Gider"].Value.ToString()),
                    AlisFiyatToplam = Islemler.DoubleYap(dgv.Rows[i].Cells["AlisFiyatToplam"].Value.ToString()),
                    Aciklama = dgv.Rows[i].Cells["Aciklama"].Value.ToString(),
                    Tarih = Convert.ToDateTime(dgv.Rows[i].Cells["Tarih"].Value.ToString()),
                    Kullanici = dgv.Rows[i].Cells["Kullanici"].Value.ToString()

                });

            }
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "dsGenelRapor";
            rds.Value = list;

            RaporGoster raporGoster = new RaporGoster();
            raporGoster.reportViewer1.LocalReport.DataSources.Clear();
            raporGoster.reportViewer1.LocalReport.DataSources.Add(rds);
            raporGoster.reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\rpGenelRapor.rdlc";
            ReportParameter[] reportParameter = new ReportParameter[13];
            reportParameter[0]=new ReportParameter("Baslik",Baslik);
            reportParameter[1] = new ReportParameter("TarihBaslangic", TarihBaslangic);
            reportParameter[2] = new ReportParameter("TarihBitis", TarihBitis);
            reportParameter[3] = new ReportParameter("SatisNakit", SatisNakit);
            reportParameter[4] = new ReportParameter("SatisKart", SatisKart);
            reportParameter[5] = new ReportParameter("IadeNakit", IadeNakit);
            reportParameter[6] = new ReportParameter("IadeKart", IadeKart);
            reportParameter[7] = new ReportParameter("GelirNakit", GelirNakit);
            reportParameter[8] = new ReportParameter("GelirKart", GelirKart);
            reportParameter[9] = new ReportParameter("GiderNakit", GiderNakit);
            reportParameter[10] = new ReportParameter("GiderKart", GiderKart);
            reportParameter[11] = new ReportParameter("KdvToplam", KdvToplam);
            reportParameter[12] = new ReportParameter("KartKomisyon", KartKomisyon);
            raporGoster.reportViewer1.LocalReport.SetParameters(reportParameter);
            raporGoster.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            raporGoster.reportViewer1.ZoomMode = ZoomMode.PageWidth;

            raporGoster.ShowDialog();
            Cursor.Current = Cursors.Default;
        }

        public static void StokRaporu(DataGridView dgv)
        {
            Cursor.Current = Cursors.WaitCursor;
            List<Urun> list = new List<Urun>();
            list.Clear();
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                list.Add(new Urun
                {
                    Barkod = dgv.Rows[i].Cells["Barkod"].Value.ToString(),
                    UrunAd= dgv.Rows[i].Cells["UrunAd"].Value.ToString(),
                    Birim= dgv.Rows[i].Cells["Birim"].Value.ToString(),
                    SatisFiyati=Islemler.DoubleYap(dgv.Rows[i].Cells["SatisFiyati"].Value.ToString()),
                    Miktar= Islemler.DoubleYap(dgv.Rows[i].Cells["Miktar"].Value.ToString()),
                    Aciklama = dgv.Rows[i].Cells["Aciklama"].Value.ToString()
                });

            }
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "dsStokUrun";
            rds.Value = list;

            RaporGoster raporGoster = new RaporGoster();
            raporGoster.reportViewer1.LocalReport.DataSources.Clear();
            raporGoster.reportViewer1.LocalReport.DataSources.Add(rds);
            raporGoster.reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\rpStokUrun.rdlc";
            ReportParameter[] reportParameter = new ReportParameter[3];
            reportParameter[0] = new ReportParameter("Baslik", Baslik);
            reportParameter[1] = new ReportParameter("TarihBaslangic", TarihBaslangic);
            reportParameter[2] = new ReportParameter("TarihBitis", TarihBitis);
            raporGoster.reportViewer1.LocalReport.SetParameters(reportParameter);
            raporGoster.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            raporGoster.reportViewer1.ZoomMode = ZoomMode.PageWidth;

            raporGoster.ShowDialog();
            Cursor.Current = Cursors.Default;
        }

        public static void StokIzlemeRaporu(DataGridView dgv)
        {
            Cursor.Current = Cursors.WaitCursor;
            List<StokHareket> list = new List<StokHareket>();
            list.Clear();
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                list.Add(new StokHareket
                {
                    Barkod = dgv.Rows[i].Cells["Barkod"].Value.ToString(),
                    UrunAd = dgv.Rows[i].Cells["UrunAd"].Value.ToString(),
                    UrunGrup= dgv.Rows[i].Cells["UrunGrup"].Value.ToString(),
                    Birim = dgv.Rows[i].Cells["Birim"].Value.ToString(),
                    Miktar = Islemler.DoubleYap(dgv.Rows[i].Cells["Miktar"].Value.ToString()),
                    Kullanici= dgv.Rows[i].Cells["Kullanici"].Value.ToString(),
                    Tarih= Convert.ToDateTime(dgv.Rows[i].Cells["Tarih"].Value.ToString())
                });

            }
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "dsStokIzleme";
            rds.Value = list;

            RaporGoster raporGoster = new RaporGoster();
            raporGoster.reportViewer1.LocalReport.DataSources.Clear();
            raporGoster.reportViewer1.LocalReport.DataSources.Add(rds);
            raporGoster.reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\rpStokIzleme.rdlc";
            ReportParameter[] reportParameter = new ReportParameter[3];
            reportParameter[0] = new ReportParameter("Baslik", Baslik);
            reportParameter[1] = new ReportParameter("TarihBaslangic", TarihBaslangic);
            reportParameter[2] = new ReportParameter("TarihBitis", TarihBitis);
            raporGoster.reportViewer1.LocalReport.SetParameters(reportParameter);
            raporGoster.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            raporGoster.reportViewer1.ZoomMode = ZoomMode.PageWidth;

            raporGoster.ShowDialog();
            Cursor.Current = Cursors.Default;
        }
    }
}
