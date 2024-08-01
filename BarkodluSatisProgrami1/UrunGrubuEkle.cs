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
        public UrunGrubuEkle()
        {
            InitializeComponent();
        }

        DbBarkodEntities db=new DbBarkodEntities();

        private void UrunGrubuEkle_Load(object sender, EventArgs e)
        {
            GrupDoldur();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            if (txtUrunGrubuAdi.Text != "")
            {
                UrunGrup urunGrup = new UrunGrup();
                urunGrup.UrunGrupAd = txtUrunGrubuAdi.Text;
                db.UrunGrups.Add(urunGrup);
                db.SaveChanges();
                GrupDoldur();
                txtUrunGrubuAdi.Clear();
                MessageBox.Show("Ürün grubu eklenmiştir");
                UrunGiris urunGiris = (UrunGiris)Application.OpenForms["UrunGiris"];
                if(urunGiris != null)
                {
                    urunGiris.GrupDoldur();
                }
                

            }
            else
            {
                MessageBox.Show("Ürün grubu bilgisini ekleyiniz!");
            }
        }

        private void GrupDoldur()
        {
            listBoxUrunGrupAdi.DisplayMember = "UrunGrupAd";
            listBoxUrunGrupAdi.ValueMember = "Id";
            listBoxUrunGrupAdi.DataSource = db.UrunGrups.OrderBy(a => a.UrunGrupAd).ToList();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            int grupId=Convert.ToInt32(listBoxUrunGrupAdi.SelectedValue.ToString());
            string grupAd = listBoxUrunGrupAdi.Text;
            DialogResult dialog = MessageBox.Show(grupAd + " grubunu silmek istiyor musunuz?", "Silme işlemi", MessageBoxButtons.YesNo);
            if(dialog == DialogResult.Yes)
            {
                var grup=db.UrunGrups.FirstOrDefault(a=>a.Id==grupId);
                db.UrunGrups.Remove(grup);
                db.SaveChanges();
                GrupDoldur();    
                txtUrunGrubuAdi.Focus();
                MessageBox.Show(grupAd + " ürün grubu silindi.");
                UrunGiris urunGiris = (UrunGiris)Application.OpenForms["UrunGiris"];
                urunGiris.GrupDoldur();

            }
        }
    }
}
