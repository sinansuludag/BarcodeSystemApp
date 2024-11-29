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
        //public async Task<List<UrunDTO>> UrunList()
        public async Task UrunList()
        {
            string apiURL = "https://localhost:7109/api/Urun/UrunList";
            //var response = await _httpClient.GetStreamAsync<APIResponseDTO< apiURL>>();
            //if (response.IsSuccessStatusCode)
            //{
            //    var jsonResponse = await response.Content.ReadAsStringAsync();
            //    return JsonConvert.DeserializeObject<List<UrunDTO>>(jsonResponse);
            //}
            //return new List<UrunDTO>();
        }

    }
}
