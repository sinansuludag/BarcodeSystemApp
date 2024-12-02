using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarkodluSatisProgrami1.APIService
{
    public class APIResult<T>
    {
        public T Data { get; set; } // Başarılıysa dönen veri
        public bool IsSuccess { get; set; } // İşlemin başarı durumu
        public string ErrorMessage { get; set; } // Hata mesajı (varsa)
    }

}
