using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarkodluSatisProgrami1.Models.FormDTO
{
    public class UrunDTO
    {
        public int UrunId { get; set; }
        public string Barkod { get; set; }
        public string UrunAd { get; set; }
        public string Aciklama { get; set; }
        public string UrunGrup { get; set; }
        public Nullable<double> AlisFiyati { get; set; }
        public Nullable<double> SatisFiyati { get; set; }
        public Nullable<int> KdvOrani { get; set; }
        public Nullable<double> KdvTutari { get; set; }
        public string Birim { get; set; }
        public Nullable<double> Miktar { get; set; }
        public Nullable<System.DateTime> Tarih { get; set; }
        public string Kullanici { get; set; }
    }
}
