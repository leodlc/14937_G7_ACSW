using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Data;

namespace Presentacion_Web.Service
{
    public class LoanService
    {
        private readonly HttpClient _httpClient;

        public LoanService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<PRESTAMO>> GetLoansAsync()
        {
            var response = await _httpClient.GetAsync("http://localhost:54845/api/loan/GetAll");
            response.EnsureSuccessStatusCode();

            var responseData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<PRESTAMO>>(responseData);
        }

        public async Task<PRESTAMO> GetLoanByClientAndBookAsync(int idCliente, int idLibro)
        {
            var response = await _httpClient.GetAsync($"http://localhost:54845/api/loan/GetById/{idCliente}/{idLibro}");
            response.EnsureSuccessStatusCode();

            var responseData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<PRESTAMO>(responseData);
        }

        public async Task<bool> CreateLoanAsync(PRESTAMO prestamo)
        {
            var prestamoJson = JsonConvert.SerializeObject(prestamo);

            System.Diagnostics.Debug.WriteLine("Datos antes de enviar a la API: " + prestamoJson);
            var content = new StringContent(prestamoJson, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("http://localhost:54845/api/loan/Add", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateLoanAsync(int idCliente, int idLibro, PRESTAMO prestamo)
        {
            var prestamoJson = JsonConvert.SerializeObject(prestamo);
            var content = new StringContent(prestamoJson, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"http://localhost:54845/api/loan/Update/{idCliente}/{idLibro}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteLoanAsync(int idCliente, int idLibro)
        {
            var response = await _httpClient.DeleteAsync($"http://localhost:54845/api/loan/Delete/{idCliente}/{idLibro}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<CLIENTE>> GetClientesDesdePrestamoAsync()
        {
            var response = await _httpClient.GetAsync("http://localhost:54845/api/client/getall");
            response.EnsureSuccessStatusCode();

            var responseData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<CLIENTE>>(responseData);
        }

        public async Task<List<LIBRO>> GetLibrosDesdePrestamoAsync()
        {
            var response = await _httpClient.GetAsync("http://localhost:54845/api/book/getall");
            response.EnsureSuccessStatusCode();

            var responseData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<LIBRO>>(responseData);
        }


    }
}