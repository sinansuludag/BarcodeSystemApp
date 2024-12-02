using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarkodluSatisProgrami1.Models.FormDTO
{
    public class SabitDTO
    {
        public int Id { get; set; }
        public Nullable<int> KartKomisyon { get; set; }
        public Nullable<bool> Yazici { get; set; }
        public string AdSoyad { get; set; }
        public string Unvan { get; set; }
        public string Adres { get; set; }
        public string Telefon { get; set; }
        public string Eposta { get; set; }
    }
}
