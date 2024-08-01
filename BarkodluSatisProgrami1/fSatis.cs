using BarkodluSatisProgrami1;
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
    public partial class fSatis : Form
    {
        public fSatis()
        {
            InitializeComponent();
        }

        DbBarkodEntities db=new DbBarkodEntities();

        private void fSatis_Load(object sender, EventArgs e)
        {
            HizliButonDoldur();
            btn5.Text = 5.ToString("C2");
            btn10.Text = 10.ToString("C2");
            btn20.Text = 20.ToString("C2");
            btn50.Text = 50.ToString("C2");
            btn100.Text = 100.ToString("C2");
            btn200.Text = 200.ToString("C2");
            txtMiktar.Text = 1.ToString();

            using(var db=new DbBarkodEntities())
            {
                var sabit = db.Sabits.FirstOrDefault();
                chYazdirmaDurumu.Checked=Convert.ToBoolean(sabit.Yazici);
            }
        }

        private void txtBarkod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string barkod = txtBarkod.Text.Trim();

                if (barkod.Length <= 3)
                {
                    txtMiktar.Text = barkod;
                    txtBarkod.Clear();
                    txtBarkod.Focus();
                }
                else
                {
                    if (db.Uruns.Any(x => x.Barkod == barkod))
                    {
                        var urun = db.Uruns.Where(a => a.Barkod == barkod).FirstOrDefault();
                        UrunGetirListeye(urun, barkod, Convert.ToDouble(txtMiktar.Text));
                    }
                    else
                    {
                        int onek=Convert.ToInt32(barkod.Substring(0, 2));
                        if (db.Terazis.Any(a => a.TeraziOnEk == onek))
                        {
                            string teraziUrunNo=barkod.Substring(2, 5);
                            if (db.Uruns.Any(a => a.Barkod == teraziUrunNo))
                            {
                                var urunTerazi = db.Uruns.Where(x => x.Barkod == teraziUrunNo).FirstOrDefault();
                                double miktarKg=Convert.ToDouble(barkod.Substring(7, 5))/1000;
                                UrunGetirListeye(urunTerazi,teraziUrunNo,miktarKg);
                            }
                            else
                            {
                                Console.Beep(900, 700);
                                MessageBox.Show("Urun ekleme sayfası");
                            }
                            
                        }
                        else
                        {
                            Console.Beep(900, 700);
                            UrunGiris urunGiris = new UrunGiris();
                            urunGiris.txtUrunGirisBarkod.Text = barkod;
                            urunGiris.ShowDialog();
                        }
                    }
                }
                gridSatisListesi.ClearSelection();
                Geneltoplam();
            }
        }

        private void UrunGetirListeye(Urun urun,string barkod,double miktar)
        {
            
            int satirSayisi = gridSatisListesi.Rows.Count;
            bool eklenmisMi = false;

            if (satirSayisi > 0)
            {
                for (int i = 0; i < satirSayisi; i++)
                {
                    if (gridSatisListesi.Rows[i].Cells["Barkod"].Value.ToString() == barkod)
                    {
                        gridSatisListesi.Rows[i].Cells["Miktar"].Value = miktar + Convert.ToDouble(gridSatisListesi.Rows[i].Cells["Miktar"].Value);
                        gridSatisListesi.Rows[i].Cells["Toplam"].Value = Math.Round(Convert.ToDouble(gridSatisListesi.Rows[i].Cells["Miktar"].Value) * Convert.ToDouble(gridSatisListesi.Rows[i].Cells["Fiyat"].Value), 2);
                        double kdvTutari = (double)urun.KdvTutari;
                        gridSatisListesi.Rows[i].Cells["KdvTutari"].Value = Convert.ToDouble(gridSatisListesi.Rows[i].Cells["Miktar"].Value) * kdvTutari;
                        eklenmisMi = true;
                    }
                }
            }
            if (!eklenmisMi)
            {
                gridSatisListesi.Rows.Add();
                gridSatisListesi.Rows[satirSayisi].Cells["Barkod"].Value = barkod;
                gridSatisListesi.Rows[satirSayisi].Cells["UrunAdi"].Value = urun.UrunAd;
                gridSatisListesi.Rows[satirSayisi].Cells["UrunGrup"].Value = urun.UrunGrup;
                gridSatisListesi.Rows[satirSayisi].Cells["Birim"].Value = urun.Birim;
                gridSatisListesi.Rows[satirSayisi].Cells["Fiyat"].Value = urun.SatisFiyati;
                gridSatisListesi.Rows[satirSayisi].Cells["Miktar"].Value = miktar;
                gridSatisListesi.Rows[satirSayisi].Cells["Toplam"].Value = Math.Round(miktar * (double)urun.SatisFiyati, 2);
                gridSatisListesi.Rows[satirSayisi].Cells["AlisFiyati"].Value = urun.AlisFiyati;
                gridSatisListesi.Rows[satirSayisi].Cells["KdvTutari"].Value = urun.KdvTutari;
            }
            Geneltoplam();
        }

        private void HizliButonDoldur()
        {
            var hizliUrun=db.HizliUruns.ToList();
            foreach(var item in hizliUrun)
            {
                Button bH=this.Controls.Find("bH"+item.Id,true).FirstOrDefault() as Button;
                if(bH != null)
                {
                    double fiyat=Islemler.DoubleYap(item.Fiyat.ToString());
                    bH.Text = item.UrunAd + "\n" + fiyat.ToString("C2");
                }
            }
        }

        private void Geneltoplam()
        {
            double toplam = 0;
            for (int i = 0; i < gridSatisListesi.Rows.Count; i++)
            {
                toplam += Convert.ToDouble(gridSatisListesi.Rows[i].Cells["Toplam"].Value);
            }
            txtGenelToplam.Text = toplam.ToString("C2");
            txtMiktar.Text = "1";
            txtBarkod.Clear();
            txtBarkod.Focus();
        }

        private void btnNakit_Click(object sender, EventArgs e)
        {
            SatisYap("Nakit");
        }

        //Gridview satır silme
        private void gridSatisListesi_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 9)
            {
                gridSatisListesi.Rows.Remove(gridSatisListesi.CurrentRow);
                gridSatisListesi.ClearSelection();
                Geneltoplam();
                txtBarkod.Focus();
            }
        }

        //Hizli buton 
        private void bHizli1_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            int butonid = Convert.ToInt16(b.Name.ToString().Substring(2, b.Name.Length - 2));

            if (b.Text.ToString().StartsWith("-"))
            {
                HizliButonUrunEkle hizliButonUrunEkle = new HizliButonUrunEkle();
                hizliButonUrunEkle.lblButonNo.Text = butonid.ToString();
                hizliButonUrunEkle.ShowDialog();
            }
            else
            {
                var urunBarkod = db.HizliUruns.Where(a => a.Id == butonid).Select(x => x.Barkod).FirstOrDefault();
                var urun = db.Uruns.Where(a => a.Barkod == urunBarkod).FirstOrDefault();
                UrunGetirListeye(urun, urunBarkod, Convert.ToDouble(txtMiktar.Text));
                Geneltoplam();
            }

        }

        //Hizli butona sağ tiklayinca içindeki urunu sil
        private void bH1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Button btn = (Button)sender;
                if (!btn.Text.StartsWith("-"))
                {
                    int butonid = Convert.ToInt16(btn.Name.ToString().Substring(2, btn.Name.Length - 2));
                    ContextMenuStrip s = new ContextMenuStrip();
                    ToolStripMenuItem sil = new ToolStripMenuItem();
                    sil.Text = "Temizle - Buton No:" + butonid.ToString();
                    sil.Click += Sil_Click;
                    s.Items.Add(sil);
                    this.ContextMenuStrip = s;
                }
            }
        }

        //Hizli butonu eski haline getir
        private void Sil_Click(object sender, EventArgs e)
        {
            int butonid = Convert.ToInt16(sender.ToString().Substring(19, sender.ToString().Length - 19));
            var guncelle=db.HizliUruns.Find(butonid);
            guncelle.Barkod = "-";
            guncelle.UrunAd = "-";
            guncelle.Fiyat = 0;
            db.SaveChanges();
            double fiyat = 0;
            Button btn = this.Controls.Find("bH"+butonid,true).FirstOrDefault() as Button;
            btn.Text = "-" + "\n" + fiyat.ToString("C2");
        }

        //Numarator işlevli yap
        private void bN1_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (b.Text == ",")
            {
                int virgul = txtNumarator.Text.Count(x => x == ',');
                if (virgul < 1)
                {
                    txtNumarator.Text += b.Text;
                }
            }
            else if (b.Text == "<")
            {
                if (txtNumarator.Text.Length > 0)
                {
                    txtNumarator.Text = txtNumarator.Text.Substring(0, txtNumarator.Text.Length - 1);
                }
            }
            else
            {
                txtNumarator.Text += b.Text;
            }
        }

        //Adet sayisi
        private void btnAdet_Click(object sender, EventArgs e)
        {
            if (txtNumarator.Text != "")
            {
                txtMiktar.Text = txtNumarator.Text;
                txtNumarator.Clear();
                txtBarkod.Clear();
                txtBarkod.Focus();
            }
        }

        //Odenen para miktari
        private void btnOdenen_Click(object sender, EventArgs e)
        {
            if (txtNumarator.Text != "")
            {
                double sonuc = Islemler.DoubleYap(txtNumarator.Text) - Islemler.DoubleYap(txtGenelToplam.Text);
                txtParaUstu.Text = sonuc.ToString("C2");
                txtOdenen.Text = Islemler.DoubleYap(txtNumarator.Text).ToString("C2");
                txtNumarator.Clear();
                txtBarkod.Focus();
            }
        }

        //Barkodlu urunu getir
        private void btnBarkod_Click_1(object sender, EventArgs e)
        {
            if (txtNumarator.Text != "")
            {
                if (db.Uruns.Any(x => x.Barkod == txtNumarator.Text))
                {
                     var urun=db.Uruns.Where(a=>a.Barkod==txtNumarator.Text).FirstOrDefault();
                    UrunGetirListeye(urun,txtNumarator.Text,Convert.ToDouble(txtMiktar.Text));
                    txtNumarator.Clear();
                    txtBarkod.Focus();
                }
                else
                {

                }
            }
        }

        //Para ustunu hesapla
        private void btn5_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            double sonuc = Islemler.DoubleYap(b.Text) - Islemler.DoubleYap(txtGenelToplam.Text);
            txtParaUstu.Text = sonuc.ToString("C2");
            txtOdenen.Text = Islemler.DoubleYap(b.Text).ToString("C2");
        }

        //Barkodsuz diğer urunleri getir
        private void btnDigerUrun_Click(object sender, EventArgs e)
        {
            if (txtNumarator.Text != "")
            {
                int satirSayisi = gridSatisListesi.Rows.Count;
                gridSatisListesi.Rows.Add();
                gridSatisListesi.Rows[satirSayisi].Cells["Barkod"].Value = 1111111111116;
                gridSatisListesi.Rows[satirSayisi].Cells["UrunAdi"].Value = "Barkodsuz Ürün";
                gridSatisListesi.Rows[satirSayisi].Cells["UrunGrup"].Value = "Barkodsuz Ürün";
                gridSatisListesi.Rows[satirSayisi].Cells["Birim"].Value = "Adet";
                gridSatisListesi.Rows[satirSayisi].Cells["Miktar"].Value = 1;
                gridSatisListesi.Rows[satirSayisi].Cells["AlisFiyati"].Value = 0;
                gridSatisListesi.Rows[satirSayisi].Cells["Fiyat"].Value = Convert.ToDouble(txtNumarator.Text);
                gridSatisListesi.Rows[satirSayisi].Cells["KdvTutari"].Value = 0;
                gridSatisListesi.Rows[satirSayisi].Cells["Toplam"].Value = Convert.ToDouble(txtNumarator.Text);
                txtNumarator.Text = "";
                Geneltoplam();
                txtBarkod.Focus();
            }
        }

        // Iade islemi
        private void btnIade_Click(object sender, EventArgs e)
        {
            if (chSatisIadeIslemi.Checked)
            {
                chSatisIadeIslemi.Checked = false;
                chSatisIadeIslemi.Text = "Satış Yapılıyor";
            }
            else
            {
                chSatisIadeIslemi.Checked = true;
                chSatisIadeIslemi.Text = "İade işlemi";
            }
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            Temizle();

        }

        private void Temizle()
        {
            txtMiktar.Text = "1";
            txtBarkod.Clear();
            txtOdenen.Clear();
            txtParaUstu.Clear();
            txtGenelToplam.Text = 0.ToString("C2");
            chSatisIadeIslemi.Checked = false;
            txtNumarator.Clear();
            gridSatisListesi.Rows.Clear();
            txtBarkod.Focus();
        }

        public void SatisYap(string odemeSekli)
        {
            int satirSayisi=gridSatisListesi.Rows.Count;
            bool satisIade=chSatisIadeIslemi.Checked;
            double alisFiyatToplami = 0;
            if (satirSayisi > 0)
            {
                int? islemNo = db.Islems.First().IslemNo;
                Sati satis = new Sati();
                for(int i = 0; i < satirSayisi; i++)
                {
                    satis.IslemNo = islemNo;
                    satis.UrunAd = gridSatisListesi.Rows[i].Cells["UrunAdi"].Value.ToString();
                    satis.UrunGrup = gridSatisListesi.Rows[i].Cells["UrunGrup"].Value.ToString();
                    satis.Barkod = gridSatisListesi.Rows[i].Cells["Barkod"].Value.ToString();
                    satis.Birim = gridSatisListesi.Rows[i].Cells["Birim"].Value.ToString();
                    satis.AlisFiyat= Islemler.DoubleYap(gridSatisListesi.Rows[i].Cells["AlisFiyati"].Value.ToString());
                    satis.SatisFiyat = Islemler.DoubleYap(gridSatisListesi.Rows[i].Cells["Fiyat"].Value.ToString());
                    satis.Miktar = Islemler.DoubleYap(gridSatisListesi.Rows[i].Cells["Miktar"].Value.ToString());
                    satis.Toplam = Islemler.DoubleYap(gridSatisListesi.Rows[i].Cells["Toplam"].Value.ToString());
                    satis.KdvTutari = Islemler.DoubleYap(gridSatisListesi.Rows[i].Cells["KdvTutari"].Value.ToString())*Islemler.DoubleYap(gridSatisListesi.Rows[i].Cells["Miktar"].Value.ToString());
                    satis.OdemeSekli = odemeSekli;
                    satis.Iade = satisIade;
                    satis.Tarih = DateTime.Now;
                    satis.Kullanici = lblKullanici.Text;
                    db.Satis.Add(satis);
                    db.SaveChanges();
                    if (!satisIade)
                    {
                        string barkod = gridSatisListesi.Rows[i].Cells["Barkod"].Value.ToString();
                        Islemler.StokAzalt(barkod, Islemler.DoubleYap(gridSatisListesi.Rows[i].Cells["Miktar"].Value.ToString()));
                        
                    }
                    else
                    {
                        Islemler.StokArtir(gridSatisListesi.Rows[i].Cells["Barkod"].Value.ToString(), Islemler.DoubleYap(gridSatisListesi.Rows[i].Cells["Miktar"].Value.ToString()));
                    }
                    alisFiyatToplami += Islemler.DoubleYap(gridSatisListesi.Rows[i].Cells["AlisFiyati"].Value.ToString())* Islemler.DoubleYap(gridSatisListesi.Rows[i].Cells["Miktar"].Value.ToString());

                }
                MessageBox.Show("Satış yapılmıştır");
                IslemOzet io=new IslemOzet();
                io.IslemNo=islemNo;
                io.Iade = satisIade;
                io.AlisFiyatToplam = alisFiyatToplami;
                io.Gelir = false;
                io.Gider = false;
                if (!satisIade)
                {
                    io.Aciklama = odemeSekli + "Satış";
                }
                else
                {
                    io.Aciklama = "Iade işlemi (" + odemeSekli + ")";
                }
                io.OdemeSekli= odemeSekli;
                io.Kullanici = lblKullanici.Text;
                io.Tarih=DateTime.Now;
                switch (odemeSekli)
                {
                    case "Nakit":
                        io.Nakit=Islemler.DoubleYap(txtGenelToplam.Text);
                        io.Kart = 0;
                        break;
                    case "Kart":
                        io.Nakit = 0;
                        io.Kart= Islemler.DoubleYap(txtGenelToplam.Text);
                        break;
                    case "Kart-Nakit":
                        io.Nakit = Islemler.DoubleYap(lblNakit.Text);
                        io.Kart= Islemler.DoubleYap(lblKart.Text);
                        break;
                }
                db.IslemOzets.Add(io);
                db.SaveChanges();

                var islemNoArttir=db.Islems.First();
                islemNoArttir.IslemNo += 1;
                db.SaveChanges();
                if (chYazdirmaDurumu.Checked)
                {
                    Yazdir yazdir = new Yazdir(islemNo);
                    yazdir.YazdirmayaBasla();
                }
                Temizle();
            }
        }

        private void btnKart_Click(object sender, EventArgs e)
        {
            SatisYap("Kart");
        }

        private void btnNakitKart_Click(object sender, EventArgs e)
        {
            NakitKart nakitKart = new NakitKart();
            nakitKart.ShowDialog();
        }

        private void txtBarkod_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == false && e.KeyChar != (char)08)
            {
                e.Handled = true;
            }
        }

        private void txtMiktar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == false && e.KeyChar != (char)08)
            {
                e.Handled = true;
            }
        }

        private void Satis_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                SatisYap("Nakit");
            }
            if (e.KeyCode == Keys.F2)
            {
                SatisYap("Kart");
            }
            if (e.KeyCode == Keys.F3)
            {
                NakitKart nakitKart = new NakitKart();
                nakitKart.ShowDialog();
            }

        }

        private void btnIslemBeklet_Click(object sender, EventArgs e)
        {
            if (btnIslemBeklet.Text == "İşlem Beklet")
            {
                Bekle();
                btnIslemBeklet.BackColor = System.Drawing.Color.OrangeRed;
                btnIslemBeklet.Text = "İşlem Bekliyor";
                gridSatisListesi.Rows.Clear();
            }
            else
            {
                BeklemedenCik();
                btnIslemBeklet.BackColor = System.Drawing.Color.DimGray;
                btnIslemBeklet.Text = "İşlem Beklet";
                gridBekle.Rows.Clear();
            }

        }

        private void Bekle()
        {
            int satir = gridSatisListesi.Rows.Count;
            int sutun = gridSatisListesi.Columns.Count;
            if (satir > 0)
            {
                for (int i = 0; i < satir; i++)
                {
                    gridBekle.Rows.Add();
                    for (int j = 0; j < sutun - 1; j++)
                    {
                        gridBekle.Rows[i].Cells[j].Value = gridSatisListesi.Rows[i].Cells[j].Value;

                    }
                }
            }
        }

        private void BeklemedenCik()
        {
            int satir = gridBekle.Rows.Count;
            int sutun = gridBekle.Columns.Count;
            if (satir > 0)
            {
                for (int i = 0; i < satir; i++)
                {
                    gridSatisListesi.Rows.Add();
                    for (int j = 0; j < sutun - 1; j++)
                    {
                        gridSatisListesi.Rows[i].Cells[j].Value = gridBekle.Rows[i].Cells[j].Value;
                    }
                }
                Geneltoplam();
            }
        }

        private void chSatisIadeIslemi_CheckedChanged(object sender, EventArgs e)
        {
            if (chSatisIadeIslemi.Checked)
            {
                chSatisIadeIslemi.Text = "İade Yapılıyor";
            }
            else
            {
                chSatisIadeIslemi.Text = "Satış Yapılıyor";
            }
        }

        private void btnFisYazdir_Click(object sender, EventArgs e)
        {
            int? islemNo = db.Islems.First().IslemNo;
            Yazdir yazdir = new Yazdir(islemNo);
            yazdir.YazdirmayaBasla();
        }


    }
}
