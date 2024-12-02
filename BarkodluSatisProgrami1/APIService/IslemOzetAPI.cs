using BarkodluSatisProgrami1.Models;
using BarkodluSatisProgrami1.Models.FormDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarkodluSatisProgrami1.APIService
{
    public class IslemOzetAPI
    {
        private readonly ApiServices<IslemOzetDTO> _apiService;

        public IslemOzetAPI()
        {
            _apiService = new ApiServices<IslemOzetDTO>();
        }

        public async Task<List<IslemOzetDTO>> IslemOzetList()
        {
            string apiUrl = "https://localhost:7109/api/IslemOzet/IslemOzetList";
            var response = await _apiService.GetList(apiUrl);

            if (response.IsSuccess)
            {
                return response.Data;
            }

            throw new Exception($"Hata :{response.ErrorMessage}");
        }

        public async Task<IslemOzetDTO> IslemOzetGetById(int id)
        {
            string apiUrl = "https://localhost:7109/api/IslemOzet";
            var response = await _apiService.GetById(apiUrl, id);

            if (response.IsSuccess)
            {
                return response.Data;
            }

            throw new Exception($"Hata :{response.ErrorMessage}");
        }

        public async Task<bool> IslemOzetAdd(IslemOzetDTO islemOzet)
        {
            string apiUrl = "https://localhost:7109/api/IslemOzet/IslemOzetAdd";
            var response = await _apiService.Add(apiUrl, islemOzet);

            if (response.IsSuccess)
            {
                return response.Data;
            }

            throw new Exception($"Hata :{response.ErrorMessage}");
        }

        public async Task<bool> IslemOzetUpdate(int id, IslemOzetDTO islemOzet)
        {
            string apiUrl = "https://localhost:7109/api/IslemOzet";
            var response = await _apiService.Update(apiUrl, id, islemOzet);

            if (response.IsSuccess)
            {
                return response.Data;
            }

            throw new Exception($"Hata :{response.ErrorMessage}");
        }

        public async Task<bool> IslemOzetDelete(int id)
        {
            string apiUrl = "https://localhost:7109/api/IslemOzet";
            var response = await _apiService.Delete(apiUrl, id);

            if (response.IsSuccess)
            {
                return response.Data;
            }
            throw new Exception($"Hata :{response.ErrorMessage}");
        }
    }
}
