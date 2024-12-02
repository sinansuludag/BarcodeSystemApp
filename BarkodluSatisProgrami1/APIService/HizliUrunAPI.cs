using BarkodluSatisProgrami1.Models.FormDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarkodluSatisProgrami1.APIService
{
    public class HizliUrunAPI
    {
        private readonly ApiServices<HizliUrunDTO> _apiService;

        public HizliUrunAPI()
        {
            _apiService = new ApiServices<HizliUrunDTO>();
        }

        public async Task<List<HizliUrunDTO>> HizliUrunList()
        {
            string apiUrl= "https://localhost:7109/api/HizliUrun/HizliUrunList";
            var response=await _apiService.GetList(apiUrl);

            if (response.IsSuccess)
            {
                return response.Data;
            }

            throw new Exception($"Hata :{response.ErrorMessage}");
        }

        public async Task<HizliUrunDTO> HizliUrunGetById(int id)
        {
            string apiUrl = "https://localhost:7109/api/HizliUrun";
            var response = await _apiService.GetById(apiUrl, id);

            if (response.IsSuccess)
            {
                return response.Data;
            }

            throw new Exception($"Hata :{response.ErrorMessage}");
        }

        public async Task<bool> HizliUrunAdd(HizliUrunDTO hizliUrun)
        {
            string apiUrl = "https://localhost:7109/api/HizliUrun/HizliUrunAdd";
            var response = await _apiService.Add(apiUrl, hizliUrun);

            if (response.IsSuccess)
            {
                return response.Data;
            }

            throw new Exception($"Hata :{response.ErrorMessage}");
        }

        public async Task<bool> HizliUrunUpdate(int id, HizliUrunDTO hizliUrun)
        {
            string apiUrl = "https://localhost:7109/api/HizliUrun";
            var response = await _apiService.Update(apiUrl, id, hizliUrun);

            if (response.IsSuccess)
            {
                return response.Data;
            }

            throw new Exception($"Hata :{response.ErrorMessage}");
        }

        public async Task<bool> HizliUrunDelete(int id)
        {
            string apiUrl = "https://localhost:7109/api/HizliUrun";
            var response = await _apiService.Delete(apiUrl, id);

            if (response.IsSuccess)
            {
                return response.Data;
            }
            throw new Exception($"Hata :{response.ErrorMessage}");
        }

    }
}
