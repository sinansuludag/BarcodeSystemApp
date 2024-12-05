using BarkodluSatisProgrami1.Exceptions;
using BarkodluSatisProgrami1.Models.FormDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarkodluSatisProgrami1.APIService
{
    public class SabitAPI
    {
        private readonly ApiServices<SabitDTO> _apiService;

        public SabitAPI()
        {
            _apiService = new ApiServices<SabitDTO>();
        }

        public async Task<List<SabitDTO>> SabitList()
        {
            string apiUrl = "https://localhost:7109/api/Sabit/SabitList";
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

        public async Task<SabitDTO> SabitGetById(int id)
        {
            string apiUrl = "https://localhost:7109/api/Sabit";
            var response = await _apiService.GetById(apiUrl, id);

            if (response.IsSuccess)
            {
                return response.Data;
            }

            throw new CustomNotFoundException($"{response.ErrorMessage}");
        }

        public async Task<bool> SabitAdd(SabitDTO sabit)
        {
            string apiUrl = "https://localhost:7109/api/Sabit/SabitAdd";
            var response = await _apiService.Add(apiUrl, sabit);

            if (response.IsSuccess)
            {
                return response.Data;
            }

            throw new CustomNotFoundException($"{response.ErrorMessage}");
        }

        public async Task<bool> SabitUpdate(int id, SabitDTO sabit)
        {
            string apiUrl = "https://localhost:7109/api/Sabit";
            var response = await _apiService.Update(apiUrl, id, sabit);

            if (response.IsSuccess)
            {
                return response.Data;
            }

            throw new CustomNotFoundException($"{response.ErrorMessage}");
        }

        public async Task<bool> SabitDelete(int id)
        {
            string apiUrl = "https://localhost:7109/api/Sabit";
            var response = await _apiService.Delete(apiUrl, id);

            if (response.IsSuccess)
            {
                return response.Data;
            }
            throw new CustomNotFoundException($"{response.ErrorMessage}");
        }
    }
}
