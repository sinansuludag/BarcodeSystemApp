using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarkodluSatisProgrami1.Models.FormDTO
{
    public class SatisDTO
    {
        public int SatisId { get; set; }
        public Nullable<int> IslemNo { get; set; }
        public string UrunAd { get; set; }
        public string Barkod { get; set; }
        public string UrunGrup { get; set; }
        public string Birim { get; set; }
        public Nullable<double> AlisFiyat { get; set; }
        public Nullable<double> SatisFiyat { get; set; }
        public Nullable<double> Miktar { get; set; }
        public Nullable<double> Toplam { get; set; }
        public Nullable<double> KdvTutari { get; set; }
        public string OdemeSekli { get; set; }
        public Nullable<bool> Iade { get; set; }
        public Nullable<System.DateTime> Tarih { get; set; }
        public string Kullanici { get; set; }
    }
}
