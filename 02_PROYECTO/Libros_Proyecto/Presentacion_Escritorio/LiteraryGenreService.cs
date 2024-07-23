using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Presentacion_Escritorio
{
    public class LiteraryGenre
    {
        public List<Book> LIBRO { get; set; }
        public int IDGL { get; set; }
        public string NOMBREGL { get; set; }
        public string DESCRIPGL { get; set; }
    }

    public class LiteraryGenreService
    {
        private const string BaseUrl = "http://localhost:54845/";

        public async Task<List<LiteraryGenre>> GetLiteraryGenresAsync()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(BaseUrl + "api/LiteraryGenre/GetAll");
                    response.EnsureSuccessStatusCode();
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<LiteraryGenre>>(json);
                }
            }
            catch (HttpRequestException httpRequestException)
            {
                System.Console.WriteLine("Error fetching data: " + httpRequestException.Message);
                return new List<LiteraryGenre>(); // Devuelve una lista vacía en caso de error
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Error deserializing data: " + ex.Message);
                return new List<LiteraryGenre>(); // Devuelve una lista vacía en caso de error
            }
        }

        public async Task AddLiteraryGenreAsync(LiteraryGenre genre)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var json = JsonConvert.SerializeObject(genre);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await httpClient.PostAsync(BaseUrl + "api/LiteraryGenre/Add", content);
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
