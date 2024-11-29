using BarkodluSatisProgrami1.Models;
using BarkodluSatisProgrami1.Models.FormDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BarkodluSatisProgrami1.APIService
{
    public class BarkodAPI
    {
        private readonly HttpClient _httpClient;

        public BarkodAPI(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<BarkodDTO> BarkodAddAsync(BarkodDTO barkoddto)
        {
            var jsonContent = JsonConvert.SerializeObject(barkoddto);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("Barkod/Add", httpContent);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<BarkodDTO>(jsonResponse);
            }
            else
            {
                throw new Exception($"API çağrısı başarısız oldu: {response.StatusCode}");
            }
        }


        public async Task<List<Barkod>> BarkodList()
        {
            var response = await _httpClient.GetAsync("Barkod/List");
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Barkod>>(jsonResponse);
            }
            return new List<Barkod>();
        }

        public async Task BarkodDelete(int id)
        {
            var response = await _httpClient.DeleteAsync($"Barkod/Delete/{id}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Barkod silinirken hata oluştu.");
            }
        }

        public async Task BarkodUpdate(BarkodDTO barkoddto)
        {
            var jsonContent = JsonConvert.SerializeObject(barkoddto);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync("Barkod/Update", httpContent);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Barkod güncellenirken hata oluştu.");
            }
        }
    }

}
