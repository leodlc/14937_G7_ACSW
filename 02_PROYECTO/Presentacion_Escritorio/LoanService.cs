using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Presentacion_Escritorio
{
    public class Loan
    {
        public int IDCLIENTE { get; set; }
        public int IDLIBRO { get; set; }
        public DateTime FECHAINIPREST { get; set; }
        public DateTime FECHAFINPREST { get; set; }
        public string DESCRPREST { get; set; }
        public bool ESTADOPREST { get; set; }
        public Client CLIENTE { get; set; }
        public Book LIBRO { get; set; }
    }

    public class LoanService
    {
        private const string BaseUrl = "http://localhost:54845/";

        public async Task<List<Loan>> GetLoansAsync()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(BaseUrl + "api/Loan/GetAll");
                    response.EnsureSuccessStatusCode();
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Loan>>(json);
                }
            }
            catch (HttpRequestException httpRequestException)
            {
                System.Console.WriteLine("Error fetching data: " + httpRequestException.Message);
                return new List<Loan>(); // Devuelve una lista vacía en caso de error
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Error deserializing data: " + ex.Message);
                return new List<Loan>(); // Devuelve una lista vacía en caso de error
            }
        }

        public async Task<List<Loan>> GetLoansByClientIdAsync(string clientId)
        {
            if (string.IsNullOrEmpty(clientId))
            {
                return new List<Loan>(); // Devuelve una lista vacía si el ID del cliente está vacío
            }

            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(BaseUrl + $"api/Loan/GetByClient/{clientId}");
                    response.EnsureSuccessStatusCode();
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Loan>>(json);
                }
            }
            catch (HttpRequestException httpRequestException)
            {
                System.Console.WriteLine("Error fetching data: " + httpRequestException.Message);
                return new List<Loan>(); // Devuelve una lista vacía en caso de error
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Error deserializing data: " + ex.Message);
                return new List<Loan>(); // Devuelve una lista vacía en caso de error
            }
        }
    }
}
