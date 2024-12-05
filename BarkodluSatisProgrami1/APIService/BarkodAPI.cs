using BarkodluSatisProgrami1.Models;
using BarkodluSatisProgrami1.Models.FormDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using BarkodluSatisProgrami1.Exceptions;

namespace BarkodluSatisProgrami1.APIService
{
    public class BarkodAPI
    {
        private readonly ApiServices<BarkodDTO> _apiService;

        public BarkodAPI()
        {
            _apiService = new ApiServices<BarkodDTO>();
        }

        public async Task<List<BarkodDTO>> BarkodList()
        {
            string apiUrl = "https://localhost:7109/api/Barkod/BarkodList";
            var response = await _apiService.GetList(apiUrl);

            if (response.IsSuccess)
            {
                return response.Data;
            }
            else
            {
                return null;
            }
            
        }

        public async Task<BarkodDTO> BarkodGetById(int id)
        {
            string apiUrl = "https://localhost:7109/api/Barkod";
            var response = await _apiService.GetById(apiUrl, id);

            if (response.IsSuccess)
            {
                return response.Data;
            }

            throw new CustomNotFoundException($"{response.ErrorMessage}");
        }

        public async Task<bool> BarkodAdd(BarkodDTO barkod)
        {
            string apiUrl = "https://localhost:7109/api/Barkod/BarkodAdd";
            var response = await _apiService.Add(apiUrl, barkod);

            if (response.IsSuccess)
            {
                return response.Data;
            }

            throw new CustomNotFoundException($"{response.ErrorMessage}");

        }

        public async Task<bool> BarkodUpdate(int id, BarkodDTO barkod)
        {
            string apiUrl = "https://localhost:7109/api/Barkod";
            var response = await _apiService.Update(apiUrl, id, barkod);

            if (response.IsSuccess)
            {
                return response.Data;
            }

            throw new CustomNotFoundException($"{response.ErrorMessage}");

        }

        public async Task<bool> BarkodDelete(int id)
        {
            string apiUrl = "https://localhost:7109/api/Barkod";
            var response = await _apiService.Delete(apiUrl, id);

            if (response.IsSuccess)
            {
                return response.Data;
            }
            throw new CustomNotFoundException($"{response.ErrorMessage}");

        }
    }

}
