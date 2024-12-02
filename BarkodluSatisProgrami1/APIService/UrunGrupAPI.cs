using BarkodluSatisProgrami1.Models.FormDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarkodluSatisProgrami1.APIService
{
    public class UrunGrupAPI
    {
        private readonly ApiServices<UrunGrupDTO> _apiService;

        public UrunGrupAPI()
        {
            _apiService = new ApiServices<UrunGrupDTO>();
        }

        public async Task<List<UrunGrupDTO>> UrunGrupList()
        {
            string apiUrl = "https://localhost:7109/api/UrunGrup/UrunGrupList";
            var response = await _apiService.GetList(apiUrl);

            if (response.IsSuccess)
            {
                return response.Data;
            }

            throw new Exception($"Hata :{response.ErrorMessage}");
        }

        public async Task<UrunGrupDTO> UrunGrupGetById(int id)
        {
            string apiUrl = "https://localhost:7109/api/UrunGrup";
            var response = await _apiService.GetById(apiUrl, id);

            if (response.IsSuccess)
            {
                return response.Data;
            }

            throw new Exception($"Hata :{response.ErrorMessage}");
        }

        public async Task<bool> UrunGrupAdd(UrunGrupDTO urunGrup)
        {
            string apiUrl = "https://localhost:7109/api/UrunGrup/UrunGrupAdd";
            var response = await _apiService.Add(apiUrl, urunGrup);

            if (response.IsSuccess)
            {
                return response.Data;
            }

            throw new Exception($"Hata :{response.ErrorMessage}");
        }

        public async Task<bool> UrunGrupUpdate(int id, UrunGrupDTO urunGrup)
        {
            string apiUrl = "https://localhost:7109/api/UrunGrup";
            var response = await _apiService.Update(apiUrl, id, urunGrup);

            if (response.IsSuccess)
            {
                return response.Data;
            }

            throw new Exception($"Hata :{response.ErrorMessage}");
        }

        public async Task<bool> UrunGrupDelete(int id)
        {
            string apiUrl = "https://localhost:7109/api/UrunGrup";
            var response = await _apiService.Delete(apiUrl, id);

            if (response.IsSuccess)
            {
                return response.Data;
            }
            throw new Exception($"Hata :{response.ErrorMessage}");
        }
    }
}
