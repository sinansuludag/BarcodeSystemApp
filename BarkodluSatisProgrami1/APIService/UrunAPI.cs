using BarkodluSatisProgrami1.Exceptions;
using BarkodluSatisProgrami1.Models;
using BarkodluSatisProgrami1.Models.FormDTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BarkodluSatisProgrami1.APIService
{
    public class UrunAPI
    {
        private readonly ApiServices<UrunDTO> _apiService;

        public UrunAPI()
        {
            _apiService = new ApiServices<UrunDTO>();
        }

        public async Task<List<UrunDTO>> UrunList()
        {
            string apiUrl = "https://localhost:7109/api/Urun/UrunList";
            var response= await _apiService.GetList(apiUrl);

            if (response.IsSuccess)
            {
                return response.Data;
            }
            else { return null; }

        }

        public async Task<UrunDTO> UrunGetById(int id)
        {
            string apiUrl = "https://localhost:7109/api/Urun";
            var response= await _apiService.GetById(apiUrl, id);

            if (response.IsSuccess)
            {
                return response.Data;
            }

            throw new CustomNotFoundException($"{response.ErrorMessage}");
        }

        public async Task<bool> UrunAdd(UrunDTO urun)
        {
            string apiUrl = "https://localhost:7109/api/Urun/UrunAdd";
            var response= await _apiService.Add(apiUrl, urun);

            if (response.IsSuccess)
            {
                return response.Data;
            }

            throw new CustomNotFoundException($"{response.ErrorMessage}");
        }

        public async Task<bool> UrunUpdate(int id, UrunDTO urun)
        {
            string apiUrl = "https://localhost:7109/api/Urun";
            var response= await _apiService.Update(apiUrl, id, urun);

            if (response.IsSuccess)
            {
                return response.Data;
            }

            throw new CustomNotFoundException($"{response.ErrorMessage}");
        }

        public async Task<bool> UrunDelete(int id)
        {
            string apiUrl = "https://localhost:7109/api/Urun";
            var response = await _apiService.Delete(apiUrl, id);

            if (response.IsSuccess)
            {
                return response.Data;
            }
            throw new CustomNotFoundException($"{response.ErrorMessage}");
        }
    }

}
