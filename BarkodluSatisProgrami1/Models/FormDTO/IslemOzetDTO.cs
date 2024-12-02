using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarkodluSatisProgrami1.Models.FormDTO
{
    public class IslemOzetDTO
    {
        public int Id { get; set; }
        public Nullable<int> IslemNo { get; set; }
        public Nullable<bool> Iade { get; set; }
        public string OdemeSekli { get; set; }
        public Nullable<double> Nakit { get; set; }
        public Nullable<double> Kart { get; set; }
        public Nullable<bool> Gelir { get; set; }
        public Nullable<bool> Gider { get; set; }
        public Nullable<double> AlisFiyatToplam { get; set; }
        public string Aciklama { get; set; }
        public Nullable<System.DateTime> Tarih { get; set; }
        public string Kullanici { get; set; }
    }
}
