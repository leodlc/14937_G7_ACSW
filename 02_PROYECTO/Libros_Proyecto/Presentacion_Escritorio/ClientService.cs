using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Presentacion_Escritorio
{
    public class Client
    {
        public List<Loan> PRESTAMO { get; set; }
        public int IDCLIENTE { get; set; }
        public string CEDULACLIENTE { get; set; }
        public string NOMBRECLIENTE { get; set; }
        public string APELLIDOCLIENTE { get; set; }
        public string TELEFONOCLIENTE { get; set; }
        public string DIRECCLIENTE { get; set; }
        public DateTime? FECHANACCLIENTE { get; set; }
        public bool? ESTADOCLIENTE { get; set; }
    }

    public class ClientService
    {
        private const string BaseUrl = "http://localhost:54845/";

        public async Task<List<Client>> GetClientsAsync()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(BaseUrl + "api/Client/GetAll");
                    response.EnsureSuccessStatusCode();
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Client>>(json);
                }
            }
            catch (HttpRequestException httpRequestException)
            {
                System.Console.WriteLine("Error fetching data: " + httpRequestException.Message);
                return new List<Client>(); // Devuelve una lista vacía en caso de error
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Error deserializing data: " + ex.Message);
                return new List<Client>(); // Devuelve una lista vacía en caso de error
            }
        }

        public async Task AddClientAsync(Client client)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var json = JsonConvert.SerializeObject(client);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await httpClient.PostAsync(BaseUrl + "api/Client/Add", content);
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
