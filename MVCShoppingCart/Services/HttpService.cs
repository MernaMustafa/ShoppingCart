using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MVCShoppingCart.Services
{
    public class HttpService<T> where T : class
    {
        private readonly HttpClient _httpClient;
        public HttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T> HttpGet(string uri)
        {
            HttpResponseMessage result = await _httpClient.GetAsync(uri);
            if (!result.IsSuccessStatusCode)
            {
                return null;
            }

            return await Deserialize(result);
        }

        public async Task<List<T>> HttpGetList(string uri)
        {
            HttpResponseMessage result = await _httpClient.GetAsync(uri);
            System.Diagnostics.Debug.WriteLine("---------------- Headers" + result.Headers);
            if (!result.IsSuccessStatusCode)
            {
                return null;
            }
            return await DeserializeList(result);
        }

        public async Task<bool> HttpDelete(string uri)
        {
            HttpResponseMessage result = await _httpClient.DeleteAsync(uri);
            if (!result.IsSuccessStatusCode)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> HttpPost(string uri, T entity)
        {
           
            HttpResponseMessage result = await _httpClient.PostAsync(uri, Serialize(entity));
            if (!result.IsSuccessStatusCode)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> HttpPut(string uri, T entity)
        //where T : class
        {
            HttpResponseMessage result = await _httpClient.PutAsync(uri, Serialize(entity));
            if (!result.IsSuccessStatusCode)
            {
                return false;
            }

            return true;
        }

        public void SetAuthorizationHeader(string token)
        //where T : class
        {
             _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.ToString());
        }


        private StringContent Serialize(T entity)
        {
            string data = JsonConvert.SerializeObject(entity);
            return new StringContent(data, Encoding.UTF8, "application/json");
        }
        private async Task<List<T>> DeserializeList(HttpResponseMessage result)
        {
            string apiResponse = await result.Content.ReadAsStringAsync();
            List<T> list = JsonConvert.DeserializeObject<List<T>>(apiResponse);
            return list;
        }
        private async Task<T> Deserialize(HttpResponseMessage result)
        {
            string apiResponse = await result.Content.ReadAsStringAsync();
            T entity = JsonConvert.DeserializeObject<T>(apiResponse);
            return entity;
        }


    }
}
