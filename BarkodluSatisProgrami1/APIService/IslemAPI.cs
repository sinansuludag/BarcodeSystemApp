using BarkodluSatisProgrami1.Models.FormDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarkodluSatisProgrami1.APIService
{
    public class IslemAPI
    {
        private readonly ApiServices<IslemDTO> _apiService;

        public IslemAPI()
        {
            _apiService = new ApiServices<IslemDTO>();
        }

        public async Task<List<IslemDTO>> IslemList()
        {
            string apiUrl = "https://localhost:7109/api/Islem/IslemList";
            var response = await _apiService.GetList(apiUrl);

            if (response.IsSuccess)
            {
                return response.Data;
            }

            throw new Exception($"Hata :{response.ErrorMessage}");
        }

        public async Task<IslemDTO> IslemGetById(int id)
        {
            string apiUrl = "https://localhost:7109/api/Islem";
            var response = await _apiService.GetById(apiUrl, id);

            if (response.IsSuccess)
            {
                return response.Data;
            }

            throw new Exception($"Hata :{response.ErrorMessage}");
        }

        public async Task<bool> IslemAdd(IslemDTO islem)
        {
            string apiUrl = "https://localhost:7109/api/Islem/IslemAdd";
            var response = await _apiService.Add(apiUrl, islem);

            if (response.IsSuccess)
            {
                return response.Data;
            }

            throw new Exception($"Hata :{response.ErrorMessage}");
        }

        public async Task<bool> IslemUpdate(int id, IslemDTO islem)
        {
            string apiUrl = "https://localhost:7109/api/Islem";
            var response = await _apiService.Update(apiUrl, id, islem);

            if (response.IsSuccess)
            {
                return response.Data;
            }

            throw new Exception($"Hata :{response.ErrorMessage}");
        }

        public async Task<bool> IslemDelete(int id)
        {
            string apiUrl = "https://localhost:7109/api/Islem";
            var response = await _apiService.Delete(apiUrl, id);

            if (response.IsSuccess)
            {
                return response.Data;
            }
            throw new Exception($"Hata :{response.ErrorMessage}");
        }
    }
}
