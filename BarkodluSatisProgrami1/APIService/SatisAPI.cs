using BarkodluSatisProgrami1.Models.FormDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarkodluSatisProgrami1.APIService
{
    public class SatisAPI
    {
        private readonly ApiServices<SatisDTO> _apiService;

        public SatisAPI()
        {
            _apiService = new ApiServices<SatisDTO>();
        }

        public async Task<List<SatisDTO>> SatisList()
        {
            string apiUrl = "https://localhost:7109/api/Satis/SatisList";
            var response = await _apiService.GetList(apiUrl);

            if (response.IsSuccess)
            {
                return response.Data;
            }

            throw new Exception($"Hata :{response.ErrorMessage}");
        }

        public async Task<SatisDTO> SatisGetById(int id)
        {
            string apiUrl = "https://localhost:7109/api/Satis";
            var response = await _apiService.GetById(apiUrl, id);

            if (response.IsSuccess)
            {
                return response.Data;
            }

            throw new Exception($"Hata :{response.ErrorMessage}");
        }

        public async Task<bool> SatisAdd(SatisDTO satis)
        {
            string apiUrl = "https://localhost:7109/api/Satis/SatisAdd";
            var response = await _apiService.Add(apiUrl, satis);

            if (response.IsSuccess)
            {
                return response.Data;
            }

            throw new Exception($"Hata :{response.ErrorMessage}");
        }

        public async Task<bool> SatisUpdate(int id, SatisDTO satis)
        {
            string apiUrl = "https://localhost:7109/api/Satis";
            var response = await _apiService.Update(apiUrl, id, satis);

            if (response.IsSuccess)
            {
                return response.Data;
            }

            throw new Exception($"Hata :{response.ErrorMessage}");
        }

        public async Task<bool> SatisDelete(int id)
        {
            string apiUrl = "https://localhost:7109/api/Satis";
            var response = await _apiService.Delete(apiUrl, id);

            if (response.IsSuccess)
            {
                return response.Data;
            }
            throw new Exception($"Hata :{response.ErrorMessage}");
        }
    }
}
