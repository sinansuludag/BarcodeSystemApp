using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarkodluSatisProgrami1.Models.FormDTO
{
    public class KullaniciDTO
    {
        public int Id { get; set; }
        public string AdSoyad { get; set; }
        public string Telefon { get; set; }
        public string Eposta { get; set; }
        public string KullaniciAd { get; set; }
        public string Sifre { get; set; }
        public Nullable<bool> Satis { get; set; }
        public Nullable<bool> Rapor { get; set; }
        public Nullable<bool> Stok { get; set; }
        public Nullable<bool> UrunGiris { get; set; }
        public Nullable<bool> Ayarlar { get; set; }
        public Nullable<bool> FiyatGuncelle { get; set; }
        public Nullable<bool> Yedekleme { get; set; }
    }
}
