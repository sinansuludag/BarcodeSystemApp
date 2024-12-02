using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarkodluSatisProgrami1.Models.FormDTO
{
    public class HizliUrunDTO
    {
        public int Id { get; set; }
        public string Barkod { get; set; }
        public string UrunAd { get; set; }
        public Nullable<double> Fiyat { get; set; }
    }
}
