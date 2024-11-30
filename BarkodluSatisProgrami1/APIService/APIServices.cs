using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BarkodluSatisProgrami1.APIService
{
    public class ApiServices<T> where T : class
    {
        private readonly HttpClient _httpClient;

        public ApiServices()
        {
            _httpClient = new HttpClient();
        }

        // GET: Tüm öğeleri listele
        public async Task<List<T>> GetList(string apiUrl)
        {
            try
            {
                var response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<List<T>>(jsonResponse);
                    return result;
                }
                else
                {
                    throw new Exception($"API Hatası: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata: {ex.Message}");
                return new List<T>();
            }
        }

        // GET: ID'ye göre öğe al
        public async Task<T> GetById(string apiUrl, int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{apiUrl}/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<T>(jsonResponse);
                    return result;
                }
                else
                {
                    throw new Exception($"API Hatası: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata: {ex.Message}");
                return null;
            }
        }

        // POST: Yeni öğe ekle
        public async Task<bool> Add(string apiUrl, T entity)
        {
            try
            {
                var jsonContent = JsonConvert.SerializeObject(entity);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(apiUrl, content);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata: {ex.Message}");
                return false;
            }
        }

        // PUT: Öğeyi güncelle
        public async Task<bool> Update(string apiUrl, int id, T entity)
        {
            try
            {
                var jsonContent = JsonConvert.SerializeObject(entity);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"{apiUrl}/{id}", content);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata: {ex.Message}");
                return false;
            }
        }

        // DELETE: Öğeyi sil
        public async Task<bool> Delete(string apiUrl, int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{apiUrl}/{id}");

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata: {ex.Message}");
                return false;
            }
        }
    }
}
