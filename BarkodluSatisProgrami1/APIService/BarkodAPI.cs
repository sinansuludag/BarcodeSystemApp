using BarkodluSatisProgrami1.Models;
using BarkodluSatisProgrami1.Models.FormDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BarkodluSatisProgrami1.APIService
{
    public class BarkodAPI
    {

        private readonly HttpClient _httpClient;

        public BarkodAPI(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async void BarkodAdd(BarkodDTO barkoddto)
        {
            var response =await  _httpClient.PostAsync("Barkod/Add", barkoddto);
        }


        public void BarkodRemove() { }
        public void BarkodUpdate() { }
        public void BarkodDelete() { }
        public void BarkodGet() { }

    }
}
