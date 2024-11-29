using BarkodluSatisProgrami1.Models;
using BarkodluSatisProgrami1.Models.FormDTO;
using Microsoft.Reporting.Map.WebForms.BingMaps;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BarkodluSatisProgrami1.APIService
{
    public class UrunAPI
    {
        private readonly HttpClient _httpClient;

        public UrunAPI()
        {
            _httpClient = new HttpClient();
        }
        public async Task<List<UrunDTO>> UrunList()
        {
            string apiURL = "https://localhost:7109/api/Urun/UrunList";

            try
            {
                var response = await _httpClient.GetAsync(apiURL);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();

                    // API yanıtını doğrudan listeye deserialize et
                    var urunListesi = JsonConvert.DeserializeObject<List<UrunDTO>>(jsonResponse);

                    return urunListesi;
                }
                else
                {
                    throw new Exception($"API Hatası: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata: {ex.Message}");
                return new List<UrunDTO>();
            }
        }

    }
}
