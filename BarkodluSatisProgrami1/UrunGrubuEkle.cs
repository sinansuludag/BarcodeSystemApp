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
    public partial class UrunGrubuEkle : Form
    {
        UrunGrupAPI urunGrupAPI;
        public UrunGrubuEkle()
        {
            InitializeComponent();
            urunGrupAPI = new UrunGrupAPI();
        }


        private void UrunGrubuEkle_Load(object sender, EventArgs e)
        {
            GrupDoldur();
        }

        private async void btnEkle_Click(object sender, EventArgs e)
        {
            if (txtUrunGrubuAdi.Text != "")
            {
                try
                {
                    UrunGrupDTO urunGrupDTO = new UrunGrupDTO();
                    urunGrupDTO.UrunGrupAd = txtUrunGrubuAdi.Text;
                    await urunGrupAPI.UrunGrupAdd(urunGrupDTO);

                    GrupDoldur();
                    txtUrunGrubuAdi.Clear();
                    MessageBox.Show("Ürün grubu eklenmiştir");
                    UrunGiris urunGiris = (UrunGiris)Application.OpenForms["UrunGiris"];
                    if (urunGiris != null)
                    {
                        urunGiris.GrupDoldur();
                    }
                }
                catch(CustomNotFoundException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Beklenmedik bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Ürün grubu bilgisini ekleyiniz!");
            }
        }

        private async void GrupDoldur()
        {
            try
            {
                var urunGrups = await urunGrupAPI.UrunGrupList();
                listBoxUrunGrupAdi.DisplayMember = "UrunGrupAd";
                listBoxUrunGrupAdi.ValueMember = "Id";
                if(urunGrups != null)
                {
                    listBoxUrunGrupAdi.DataSource = urunGrups.OrderBy(a => a.UrunGrupAd).ToList();
                }
                else
                {
                    listBoxUrunGrupAdi.DataSource = null;
                }
               
            }
            catch (CustomNotFoundException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Beklenmedik bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnSil_Click(object sender, EventArgs e)
        {
            int grupId=Convert.ToInt32(listBoxUrunGrupAdi.SelectedValue.ToString());
            string grupAd = listBoxUrunGrupAdi.Text;
            DialogResult dialog = MessageBox.Show(grupAd + " grubunu silmek istiyor musunuz?", "Silme işlemi", MessageBoxButtons.YesNo);
            if(dialog == DialogResult.Yes)
            {
                try
                {
                    await urunGrupAPI.UrunGrupDelete(grupId);
                    GrupDoldur();
                    txtUrunGrubuAdi.Focus();
                    MessageBox.Show(grupAd + " ürün grubu silindi.");
                    UrunGiris urunGiris = (UrunGiris)Application.OpenForms["UrunGiris"];
                    if( urunGiris != null )
                    {
                        urunGiris.GrupDoldur();
                    }     
                }
                catch(CustomNotFoundException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch(Exception ex)
                {
                     MessageBox.Show("Beklenmedik bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }
    }
}
