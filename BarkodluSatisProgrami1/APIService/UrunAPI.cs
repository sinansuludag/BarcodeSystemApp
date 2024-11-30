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
            return await _apiService.GetList(apiUrl);
        }

        public async Task<UrunDTO> UrunGetById(int id)
        {
            string apiUrl = "https://localhost:7109/api/Urun";
            return await _apiService.GetById(apiUrl, id);
        }

        public async Task<bool> UrunEkle(UrunDTO urun)
        {
            string apiUrl = "https://localhost:7109/api/Urun/UrunAdd";
            return await _apiService.Add(apiUrl, urun);
        }

        public async Task<bool> UrunGuncelle(int id, UrunDTO urun)
        {
            string apiUrl = "https://localhost:7109/api/Urun";
            return await _apiService.Update(apiUrl, id, urun);
        }

        public async Task<bool> UrunSil(int id)
        {
            string apiUrl = "https://localhost:7109/api/Urun";
            return await _apiService.Delete(apiUrl, id);
        }
    }

}
