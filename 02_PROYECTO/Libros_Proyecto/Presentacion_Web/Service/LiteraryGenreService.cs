using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Data;
using System.Text;

namespace Presentacion_Web.Service
{
    public class LiteraryGenreService
    {
        private readonly HttpClient _httpClient;

        public LiteraryGenreService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<GENEROLITERARIO>> GetGenresAsync()
        {
            var response = await _httpClient.GetAsync("http://localhost:54845/api/literarygenre/GetAll");
            response.EnsureSuccessStatusCode();

            var responseData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<GENEROLITERARIO>>(responseData);
        }

        public async Task<GENEROLITERARIO> GetGenreByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"http://localhost:54845/api/literarygenre/getbyid/{id}");
            response.EnsureSuccessStatusCode();

            var responseData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<GENEROLITERARIO>(responseData);
        }

        public async Task<bool> CreateGenreAsync(GENEROLITERARIO genre)
        {
            var genreJson = JsonConvert.SerializeObject(genre);
            var content = new StringContent(genreJson, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("http://localhost:54845/api/literarygenre/AddGenre", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateGenreAsync(int id, GENEROLITERARIO genre)
        {
            var genreJson = JsonConvert.SerializeObject(genre);
            var content = new StringContent(genreJson, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"http://localhost:54845/api/literarygenre/Update/{id}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteGenreAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"http://localhost:54845/api/literarygenre/Delete/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
