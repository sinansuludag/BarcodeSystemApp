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
    public partial class DetayGoster : Form
    {
        public DetayGoster()
        {
            InitializeComponent();
        }

        public int islemno { get; set; }

        private void DetayGoster_Load(object sender, EventArgs e)
        {
            lblIslemNo.Text = islemno.ToString();
            using(var db=new DbBarkodEntities())
            {
                gridListe.DataSource = db.Satis.Select(a=> new {a.IslemNo,a.UrunAd,a.UrunGrup,a.Miktar,a.Toplam}).Where(x => x.IslemNo == islemno).ToList();
                Islemler.GridDuzenle(gridListe);
            }
        }
    }
}
