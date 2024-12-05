using BarkodluSatisProgrami1;
using BarkodluSatisProgrami1.APIService;
using BarkodluSatisProgrami1.Exceptions;
using BarkodluSatisProgrami1.Models;
using BarkodluSatisProgrami1.Models.FormDTO;
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
    public partial class GelirGider : Form
    {
        IslemOzetAPI islemOzetAPI;
        public GelirGider()
        {
            InitializeComponent();
            islemOzetAPI = new IslemOzetAPI();
        }

        public string gelirgider { get; set; }
        public string kullanici { get; set; }

        private void GelirGider_Load(object sender, EventArgs e)
        {
            lblGelirGider.Text = gelirgider+" İŞLEMİ YAPILIYOR";
        }

        private void cbOdemeTuru_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbOdemeTuru.SelectedIndex == 0)
            {
                txtNakit.Enabled=true;
                txtKart.Enabled=false;
            }
            else if(cbOdemeTuru.SelectedIndex == 1)
            {
                txtNakit.Enabled = false;
                txtKart.Enabled = true;
            }
            else if (cbOdemeTuru.SelectedIndex == 2)
            {
                txtNakit.Enabled = true;
                txtKart.Enabled = true;
            }
            txtNakit.Text = "0";
            txtKart.Text = "0";
        }

        private async void btnEkle_Click(object sender, EventArgs e)
        {
            if(cbOdemeTuru.Text != "")
            {
                if(txtNakit.Text != "" && txtKart.Text != "" )
                {
                        IslemOzetDTO io = new IslemOzetDTO();
                        io.IslemNo = 0;
                        io.Iade = false;
                        io.OdemeSekli=cbOdemeTuru.Text;
                        io.Nakit=Islemler.DoubleYap(txtNakit.Text);
                        io.Kart=Islemler.DoubleYap(txtKart.Text);
                        if (gelirgider == "GELİR")
                        {
                            io.Gelir = true;
                            io.Gider = false;
                        }
                        else
                        {
                            io.Gelir = false;
                            io.Gider = true;
                        }
                        io.AlisFiyatToplam = 0;
                        io.Aciklama=gelirgider+" - İşlemi "+txtAciklama.Text;
                        io.Tarih=dtTarih.Value;
                        io.Kullanici = kullanici;
                    try
                    {
                        await islemOzetAPI.IslemOzetAdd(io);
                    }
                    catch(CustomNotFoundException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Beklenmedik bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                        

                        MessageBox.Show(gelirgider + " işlemi kaydedildi");
                        txtNakit.Text = "0";
                        txtKart.Text = "0";
                        txtAciklama.Clear();
                        cbOdemeTuru.Text = "";
                        Rapor rapor = (Rapor)Application.OpenForms["Rapor"];
                        if (rapor != null)
                        {
                            rapor.btnGoster_Click(null, null);
                        }
                        this.Hide();
                   
                }
            }
            else
            {
                MessageBox.Show("Lütfen ödeme türünü seçiniz!");
            }
        }
    }
}
