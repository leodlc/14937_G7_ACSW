using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Presentacion_Escritorio
{
    public class Author
    {
        public List<Book> LIBRO { get; set; }
        public int IDAUTOR { get; set; }
        public string NOMBREAUTOR { get; set; }
        public string APELLIDOAUTOR { get; set; }
        public string NACIONALIDADAUTOR { get; set; }
        public string BIOGRAFIAAUTOR { get; set; }
    }

    public class AuthorService
    {
        private const string BaseUrl = "http://localhost:54845/";

        public async Task<List<Author>> GetAuthorsAsync()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(BaseUrl + "api/Author/GetAll");
                    response.EnsureSuccessStatusCode();
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Author>>(json);
                }
            }
            catch (HttpRequestException httpRequestException)
            {
                System.Console.WriteLine("Error fetching data: " + httpRequestException.Message);
                return new List<Author>(); // Devuelve una lista vacía en caso de error
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Error deserializing data: " + ex.Message);
                return new List<Author>(); // Devuelve una lista vacía en caso de error
            }
        }

        public async Task AddAuthorAsync(Author author)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var json = JsonConvert.SerializeObject(author);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await httpClient.PostAsync(BaseUrl + "api/Author/Add", content);
                    response.EnsureSuccessStatusCode();
                }
            }
            catch (HttpRequestException httpRequestException)
            {
                System.Console.WriteLine("Error posting data: " + httpRequestException.Message);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Error serializing data: " + ex.Message);
            }
        }
    }
}
