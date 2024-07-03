using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Data;

namespace Presentacion_Web.Service
{
    public class ClientService
    {
        private readonly HttpClient _httpClient;

        public ClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CLIENTE>> GetClientesAsync()
        {
            var response = await _httpClient.GetAsync("http://localhost:54845/api/client/getall");
            response.EnsureSuccessStatusCode();

            var responseData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<CLIENTE>>(responseData);
        }

        public async Task<CLIENTE> GetClienteByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"http://localhost:54845/api/client/getbyid/{id}");
            response.EnsureSuccessStatusCode();

            var responseData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CLIENTE>(responseData);
        }

        public async Task<bool> CreateClienteAsync(CLIENTE cliente)
        {
            var clienteJson = JsonConvert.SerializeObject(cliente);
            var content = new StringContent(clienteJson, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("http://localhost:54845/api/client/add", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateClienteAsync(int id, CLIENTE cliente)
        {
            var clienteJson = JsonConvert.SerializeObject(cliente);
            var content = new StringContent(clienteJson, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"http://localhost:54845/api/client/update/{id}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteClienteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"http://localhost:54845/api/client/delete/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
