using BarkodluSatisProgrami1.Exceptions;
using BarkodluSatisProgrami1.Models.FormDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarkodluSatisProgrami1.APIService
{
    public class TeraziAPI
    {
        private readonly ApiServices<TeraziDTO> _apiService;

        public TeraziAPI()
        {
            _apiService = new ApiServices<TeraziDTO>();
        }

        public async Task<List<TeraziDTO>> TeraziList()
        {
            string apiUrl = "https://localhost:7109/api/Terazi/TeraziList";
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

        public async Task<TeraziDTO> TeraziGetById(int id)
        {
            string apiUrl = "https://localhost:7109/api/Terazi";
            var response = await _apiService.GetById(apiUrl, id);

            if (response.IsSuccess)
            {
                return response.Data;
            }

            throw new CustomNotFoundException($"{response.ErrorMessage}");
        }

        public async Task<bool> TeraziAdd(TeraziDTO terazi)
        {
            string apiUrl = "https://localhost:7109/api/Terazi/TeraziAdd";
            var response = await _apiService.Add(apiUrl, terazi);

            if (response.IsSuccess)
            {
                return response.Data;
            }

            throw new CustomNotFoundException($"{response.ErrorMessage}");
        }

        public async Task<bool> TeraziUpdate(int id, TeraziDTO terazi)
        {
            string apiUrl = "https://localhost:7109/api/Terazi";
            var response = await _apiService.Update(apiUrl, id, terazi);

            if (response.IsSuccess)
            {
                return response.Data;
            }

            throw new CustomNotFoundException($"{response.ErrorMessage}");
        }

        public async Task<bool> TeraziDelete(int id)
        {
            string apiUrl = "https://localhost:7109/api/Terazi";
            var response = await _apiService.Delete(apiUrl, id);

            if (response.IsSuccess)
            {
                return response.Data;
            }
            throw new CustomNotFoundException($"{response.ErrorMessage}");
        }
    }
}
