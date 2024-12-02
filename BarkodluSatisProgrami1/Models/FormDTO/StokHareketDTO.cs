using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarkodluSatisProgrami1.Models.FormDTO
{
    public class StokHareketDTO
    {
        public int Id { get; set; }
        public string Barkod { get; set; }
        public string UrunAd { get; set; }
        public string Birim { get; set; }
        public Nullable<double> Miktar { get; set; }
        public string UrunGrup { get; set; }
        public string Kullanici { get; set; }
        public Nullable<System.DateTime> Tarih { get; set; }
    }
}
