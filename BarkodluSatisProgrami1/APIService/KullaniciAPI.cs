using BarkodluSatisProgrami1.Models.FormDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarkodluSatisProgrami1.APIService
{
    public class KullaniciAPI
    {
        private readonly ApiServices<KullaniciDTO> _apiService;

        public KullaniciAPI()
        {
            _apiService = new ApiServices<KullaniciDTO>();
        }

        public async Task<List<KullaniciDTO>> KullaniciList()
        {
            string apiUrl = "https://localhost:7109/api/Kullanici/KullaniciList";
            var response = await _apiService.GetList(apiUrl);

            if (response.IsSuccess)
            {
                return response.Data;
            }

            throw new Exception($"Hata :{response.ErrorMessage}");
        }

        public async Task<KullaniciDTO> KullaniciGetById(int id)
        {
            string apiUrl = "https://localhost:7109/api/Kullanici";
            var response = await _apiService.GetById(apiUrl, id);

            if (response.IsSuccess)
            {
                return response.Data;
            }

            throw new Exception($"Hata :{response.ErrorMessage}");
        }

        public async Task<bool> KullaniciAdd(KullaniciDTO kullanici)
        {
            string apiUrl = "https://localhost:7109/api/Kullanici/KullaniciAdd";
            var response = await _apiService.Add(apiUrl, kullanici);

            if (response.IsSuccess)
            {
                return response.Data;
            }

            throw new Exception($"Hata :{response.ErrorMessage}");
        }

        public async Task<bool> KullaniciUpdate(int id, KullaniciDTO kullanici)
        {
            string apiUrl = "https://localhost:7109/api/Kullanici";
            var response = await _apiService.Update(apiUrl, id, kullanici);

            if (response.IsSuccess)
            {
                return response.Data;
            }

            throw new Exception($"Hata :{response.ErrorMessage}");
        }

        public async Task<bool> KullaniciDelete(int id)
        {
            string apiUrl = "https://localhost:7109/api/Kullanici";
            var response = await _apiService.Delete(apiUrl, id);

            if (response.IsSuccess)
            {
                return response.Data;
            }
            throw new Exception($"Hata :{response.ErrorMessage}");
        }
    }
}
