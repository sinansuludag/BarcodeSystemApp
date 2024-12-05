using BarkodluSatisProgrami1.Exceptions;
using BarkodluSatisProgrami1.Models.FormDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarkodluSatisProgrami1.APIService
{
    public class StokHareketAPI
    {
        private readonly ApiServices<StokHareketDTO> _apiService;

        public StokHareketAPI()
        {
            _apiService = new ApiServices<StokHareketDTO>();
        }

        public async Task<List<StokHareketDTO>> StokHareketList()
        {
            string apiUrl = "https://localhost:7109/api/StokHareket/StokHareketList";
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

        public async Task<StokHareketDTO> StokHareketGetById(int id)
        {
            string apiUrl = "https://localhost:7109/api/StokHareket";
            var response = await _apiService.GetById(apiUrl, id);

            if (response.IsSuccess)
            {
                return response.Data;
            }

            throw new CustomNotFoundException($"{response.ErrorMessage}");
        }

        public async Task<bool> StokHareketAdd(StokHareketDTO stokHareket)
        {
            string apiUrl = "https://localhost:7109/api/StokHareket/StokHareketAdd";
            var response = await _apiService.Add(apiUrl, stokHareket);

            if (response.IsSuccess)
            {
                return response.Data;
            }

            throw new CustomNotFoundException($"{response.ErrorMessage}");
        }

        public async Task<bool> StokHareketUpdate(int id, StokHareketDTO stokHareket)
        {
            string apiUrl = "https://localhost:7109/api/StokHareket";
            var response = await _apiService.Update(apiUrl, id, stokHareket);

            if (response.IsSuccess)
            {
                return response.Data;
            }

            throw new CustomNotFoundException($"{response.ErrorMessage}");
        }

        public async Task<bool> StokHareketDelete(int id)
        {
            string apiUrl = "https://localhost:7109/api/StokHareket";
            var response = await _apiService.Delete(apiUrl, id);

            if (response.IsSuccess)
            {
                return response.Data;
            }
            throw new CustomNotFoundException($"{response.ErrorMessage}");
        }
    }
}
