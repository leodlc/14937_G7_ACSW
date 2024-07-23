using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Data;
using Newtonsoft.Json;

namespace Presentacion_Web.Service
{
    public class BookService
    {
        private readonly HttpClient _httpClient;

        public BookService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<LIBRO>> GetBooksAsync()
        {
            var response = await _httpClient.GetAsync("http://localhost:54845/api/book/GetAll");
            response.EnsureSuccessStatusCode();

            var responseData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<LIBRO>>(responseData);
        }

        public async Task<LIBRO> GetBookByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"http://localhost:54845/api/book/GetById/{id}");
            response.EnsureSuccessStatusCode();

            var responseData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<LIBRO>(responseData);
        }

        public async Task<bool> CreateBookAsync(LIBRO libro)
        {
            var bookJson = JsonConvert.SerializeObject(libro);
            var content = new StringContent(bookJson, Encoding.UTF8, "application/json");
            Debug.WriteLine(bookJson);

            var response = await _httpClient.PostAsync("http://localhost:54845/api/book/Add", content);

            // Imprimir el contenido de la respuesta para depuración
            var responseContent = await response.Content.ReadAsStringAsync();
            Debug.WriteLine(responseContent);

            return response.IsSuccessStatusCode;
        }


        public async Task<bool> UpdateBookAsync(int id, LIBRO libro)
        {
            var libroJson = JsonConvert.SerializeObject(libro);
            var content = new StringContent(libroJson, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"http://localhost:54845/api/book/Update/{id}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"http://localhost:54845/api/book/Delete/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
