using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Data;
using System.Text;

namespace Presentacion_Web.Service
{
    public class AuthorService
    {
        private readonly HttpClient _httpClient;

        public AuthorService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<AUTOR>> GetAutoresAsync()
        {
            var response = await _httpClient.GetAsync("http://localhost:54845/api/author/GetAll");
            response.EnsureSuccessStatusCode();

            var responseData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<AUTOR>>(responseData);
        }

        public async Task<AUTOR> GetAutorByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"http://localhost:54845/api/author/GetById/{id}");
            response.EnsureSuccessStatusCode();

            var responseData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<AUTOR>(responseData);
        }

        public async Task<bool> CreateAutorAsync(AUTOR autor)
        {
            var autorJson = JsonConvert.SerializeObject(autor);
            var content = new StringContent(autorJson, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("http://localhost:54845/api/author/Add", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAutorAsync(int id, AUTOR autor)
        {
            var autorJson = JsonConvert.SerializeObject(autor);
            var content = new StringContent(autorJson, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"http://localhost:54845/api/author/Update/{id}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAutorAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"http://localhost:54845/api/author/Delete/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
