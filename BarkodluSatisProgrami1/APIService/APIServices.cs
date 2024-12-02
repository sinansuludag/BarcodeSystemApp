using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BarkodluSatisProgrami1.APIService;
using Newtonsoft.Json;

public class ApiServices<T> where T : class
{
    private readonly HttpClient _httpClient;

    public ApiServices()
    {
        _httpClient = new HttpClient();
    }

    
    public async Task<APIResult<List<T>>> GetList(string apiUrl)
    {
        try
        {
            var response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<T>>(jsonResponse);

                if (result == null || result.Count == 0)
                {
                    return new APIResult<List<T>>
                    {
                        IsSuccess = false,
                        ErrorMessage = "Kayıt bulunamadı."
                    };
                }

                return new APIResult<List<T>> { IsSuccess = true, Data = result };
            }
            else
            {
                return new APIResult<List<T>>
                {
                    IsSuccess = false,
                    ErrorMessage = $"API Hatası: {response.StatusCode}"
                };
            }
        }
        catch (Exception ex)
        {
            return new APIResult<List<T>>
            {
                IsSuccess = false,
                ErrorMessage = $"Beklenmeyen hata({typeof(T).Name}): {ex.Message}"
            };
        }
    }

    
    public async Task<APIResult<T>> GetById(string apiUrl, int id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{apiUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<T>(jsonResponse);

                if (result == null)
                {
                    return new APIResult<T>
                    {
                        IsSuccess = false,
                        ErrorMessage = "ID ile eşleşen kayıt bulunamadı."
                    };
                }

                return new APIResult<T> { IsSuccess = true, Data = result };
            }
            else
            {
                return new APIResult<T>
                {
                    IsSuccess = false,
                    ErrorMessage = $"API Hatası: {response.StatusCode}"
                };
            }
        }
        catch (Exception ex)
        {
            return new APIResult<T>
            {
                IsSuccess = false,
                ErrorMessage = $"Beklenmeyen hata( {typeof(T).Name} ): {ex.Message}"
            };
        }
    }


    public async Task<APIResult<bool>> Add(string apiUrl, T entity)
    {
        try
        {
            var jsonContent = JsonConvert.SerializeObject(entity);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                return new APIResult<bool> { IsSuccess = true, Data = true };
            }

            return new APIResult<bool>
            {
                IsSuccess = false,
                ErrorMessage = $"API Hatası: {response.StatusCode}"
            };
        }
        catch (Exception ex)
        {
            return new APIResult<bool>
            {
                IsSuccess = false,
                ErrorMessage = $"Beklenmeyen hata( {typeof(T).Name} ): {ex.Message}"
            };
        }
    }


    public async Task<APIResult<bool>> Update(string apiUrl, int id, T entity)
    {
        try
        {
            var jsonContent = JsonConvert.SerializeObject(entity);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{apiUrl}/{id}", content);

            if (response.IsSuccessStatusCode)
            {
                return new APIResult<bool> { IsSuccess = true, Data = true };
            }

            return new APIResult<bool>
            {
                IsSuccess = false,
                ErrorMessage = $"API Hatası: {response.StatusCode}"
            };
        }
        catch (Exception ex)
        {
            return new APIResult<bool>
            {
                IsSuccess = false,
                ErrorMessage = $"Beklenmeyen hata(  {typeof(T).Name}  ): {ex.Message}"
            };
        }
    }


    public async Task<APIResult<bool>> Delete(string apiUrl, int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"{apiUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                return new APIResult<bool> { IsSuccess = true, Data = true };
            }

            return new APIResult<bool>
            {
                IsSuccess = false,
                ErrorMessage = $"API Hatası: {response.StatusCode}"
            };
        }
        catch (Exception ex)
        {
            return new APIResult<bool>
            {
                IsSuccess = false,
                ErrorMessage = $"Beklenmeyen hata(  {typeof(T).Name}  ): {ex.Message}"
            };
        }
    }
}
