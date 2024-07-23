using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Presentacion_Escritorio
{
    public class Book
    {
        public int IDLIBRO { get; set; }
        public int IDAUTOR { get; set; }
        public int IDGL { get; set; }
        public string NOMBRELIBRO { get; set; }
        public DateTime ANIOPUBLIBRO { get; set; }
        public string IMGLIBRO { get; set; }
        public string ISBNLIBRO { get; set; }
        public bool ESTADOLIBRO { get; set; }
        public string EDITORIALLIBRO { get; set; }
        public int STOCKLIBRO { get; set; }
        public Author AUTOR { get; set; }
        public LiteraryGenre GENEROLITERARIO { get; set; }
        public List<object> PRESTAMO { get; set; }
    }

    public class BookService
    {
        private const string BaseUrl = "http://localhost:54845/";

        public async Task<List<Book>> GetBooksAsync()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(BaseUrl + "api/Book/GetAll");
                    response.EnsureSuccessStatusCode();
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Book>>(json);
                }
            }
            catch (HttpRequestException httpRequestException)
            {
                System.Console.WriteLine("Error fetching data: " + httpRequestException.Message);
                return new List<Book>(); // Devuelve una lista vacía en caso de error
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Error deserializing data: " + ex.Message);
                return new List<Book>(); // Devuelve una lista vacía en caso de error
            }
        }

        public async Task AddBookAsync(Book newBook)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var json = JsonConvert.SerializeObject(newBook);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(BaseUrl + "api/Book/Add", content);
                    response.EnsureSuccessStatusCode();
                }
            }
            catch (HttpRequestException httpRequestException)
            {
                System.Console.WriteLine("Error adding book: " + httpRequestException.Message);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Error adding book: " + ex.Message);
            }
        }
    }

}
