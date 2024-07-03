using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Presentacion_Escritorio
{
    public partial class AddLoanForm : Form
    {
        private TextBox txtClientId;
        private TextBox txtClientDetails;
        private TextBox txtBookId;
        private TextBox txtBookDetails;
        private DateTimePicker dtpStartDate;
        private DateTimePicker dtpEndDate;
        private TextBox txtDescription;
        private Button btnFetchClient;
        private Button btnFetchBook;
        private Button btnSave;
        private Button btnCancel;
        private const string BaseUrl = "http://localhost:54845/";

        public AddLoanForm()
        {
            InitializeComponent();
            this.Load += new EventHandler(AddLoanForm_Load);
        }

        private void AddLoanForm_Load(object sender, EventArgs e)
        {
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            this.Text = "Añadir Préstamo";
            this.Size = new Size(800, 600);

            var lblClientId = new Label { Text = "ID del cliente", Top = 20, Left = 20, Width = 150 };
            txtClientId = new TextBox { Top = 50, Left = 20, Width = 150 };
            btnFetchClient = new Button { Text = "Buscar Cliente", Top = 50, Left = 180, Width = 100 };
            btnFetchClient.Click += async (sender, e) => await FetchClientDetailsAsync();

            txtClientDetails = new TextBox { Top = 80, Left = 20, Width = 260, Height = 100, Multiline = true, ReadOnly = true };

            var lblBookId = new Label { Text = "ID del libro", Top = 200, Left = 20, Width = 150 };
            txtBookId = new TextBox { Top = 230, Left = 20, Width = 150 };
            btnFetchBook = new Button { Text = "Buscar Libro", Top = 230, Left = 180, Width = 100 };
            btnFetchBook.Click += async (sender, e) => await FetchBookDetailsAsync();

            txtBookDetails = new TextBox { Top = 260, Left = 20, Width = 260, Height = 100, Multiline = true, ReadOnly = true };

            var lblStartDate = new Label { Text = "Fecha de inicio del préstamo", Top = 380, Left = 20, Width = 200 };
            dtpStartDate = new DateTimePicker { Top = 410, Left = 20, Width = 300, Value = DateTime.Now, Enabled = false };

            var lblEndDate = new Label { Text = "Fecha fin del préstamo", Top = 380, Left = 350, Width = 200 };
            dtpEndDate = new DateTimePicker { Top = 410, Left = 350, Width = 300 };

            var lblDescription = new Label { Text = "Descripción", Top = 440, Left = 20, Width = 150 };
            txtDescription = new TextBox { Top = 470, Left = 20, Width = 630, Height = 100, Multiline = true };

            btnSave = new Button { Text = "Guardar", Top = 580, Left = 20, Width = 100, BackColor = Color.DarkGreen, ForeColor = Color.White };
            btnSave.Click += BtnSave_Click;

            btnCancel = new Button { Text = "Cancelar", Top = 580, Left = 130, Width = 100, BackColor = Color.Gray, ForeColor = Color.White };
            btnCancel.Click += (sender, e) => this.Close();

            this.Controls.Add(lblClientId);
            this.Controls.Add(txtClientId);
            this.Controls.Add(btnFetchClient);
            this.Controls.Add(txtClientDetails);
            this.Controls.Add(lblBookId);
            this.Controls.Add(txtBookId);
            this.Controls.Add(btnFetchBook);
            this.Controls.Add(txtBookDetails);
            this.Controls.Add(lblStartDate);
            this.Controls.Add(dtpStartDate);
            this.Controls.Add(lblEndDate);
            this.Controls.Add(dtpEndDate);
            this.Controls.Add(lblDescription);
            this.Controls.Add(txtDescription);
            this.Controls.Add(btnSave);
            this.Controls.Add(btnCancel);
        }

        private async Task FetchClientDetailsAsync()
        {
            Console.WriteLine($"Fetching details for Client ID: {txtClientId.Text}");
            if (int.TryParse(txtClientId.Text, out int clientId))
            {
                var client = await GetClientByIdAsync(clientId);
                if (client != null)
                {
                    txtClientDetails.Text = $"Nombre: {client.NOMBRECLIENTE} {client.APELLIDOCLIENTE}\n" +
                                            $"Teléfono: {client.TELEFONOCLIENTE}\n" +
                                            $"Dirección: {client.DIRECCLIENTE}\n" +
                                            $"Fecha de Nacimiento: {client.FECHANACCLIENTE?.ToString("yyyy-MM-dd")}\n"
                                            ;
                }
                else
                {
                    MessageBox.Show("Cliente no encontrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("ID de cliente inválido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task FetchBookDetailsAsync()
        {
            Console.WriteLine($"Fetching details for Book ID: {txtBookId.Text}");
            if (int.TryParse(txtBookId.Text, out int bookId))
            {
                var book = await GetBookByIdAsync(bookId);
                if (book != null)
                {
                    txtBookDetails.Text = $"Nombre: {book.NOMBRELIBRO}\n" +
                                          $"Autor: {book.AUTOR}\n" +
                                          $"Género Literario: {book.GENEROLITERARIO}\n" +
                                          $"Año de Publicación: {book.ANIOPUBLIBRO.ToString("yyyy")}\n" +
                                          $"Editorial: {book.EDITORIALLIBRO}\n" +
                                          $"Stock: {book.STOCKLIBRO}";
                }
                else
                {
                    MessageBox.Show("Libro no encontrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("ID de libro inválido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task<Client> GetClientByIdAsync(int clientId)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(BaseUrl + $"api/Client/GetById/{clientId}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Client>(json);
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error al obtener cliente: {errorContent}");
                    return null;
                }
            }
        }

        private async Task<Book> GetBookByIdAsync(int bookId)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(BaseUrl + $"api/Book/GetById/{bookId}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Book>(json);
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error al obtener libro: {errorContent}");
                    return null;
                }
            }
        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            Console.WriteLine($"Client ID: {txtClientId.Text}");
            Console.WriteLine($"Book ID: {txtBookId.Text}");

            if (!int.TryParse(txtClientId.Text, out int clientId) || !int.TryParse(txtBookId.Text, out int bookId))
            {
                MessageBox.Show("Debe ingresar IDs válidos para el cliente y el libro.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var client = await GetClientByIdAsync(clientId);
            var book = await GetBookByIdAsync(bookId);

            if (client == null || book == null)
            {
                MessageBox.Show("Cliente o libro no encontrados.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var startDate = dtpStartDate.Value;
            var endDate = dtpEndDate.Value;
            var description = txtDescription.Text;

            var loan = new Loan
            {
                IDCLIENTE = client.IDCLIENTE,
                IDLIBRO = book.IDLIBRO,
                FECHAINIPREST = startDate,
                FECHAFINPREST = endDate,
                DESCRPREST = description,
                ESTADOPREST = true,
                CLIENTE = client,
                LIBRO = book
            };

            var confirmResult = MessageBox.Show(
                $"Detalles del préstamo:\nCliente: {client.NOMBRECLIENTE} {client.APELLIDOCLIENTE}\nLibro: {book.NOMBRELIBRO}\nFecha de inicio: {startDate}\nFecha de fin: {endDate}\nDescripción: {description}",
                "Confirmar Préstamo",
                MessageBoxButtons.OKCancel);

            if (confirmResult == DialogResult.OK)
            {
                await SaveLoanAsync(loan);
                MessageBox.Show("Préstamo guardado", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private async Task SaveLoanAsync(Loan loan)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsync(BaseUrl + "api/Loan/Add", new StringContent(JsonConvert.SerializeObject(loan), System.Text.Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Préstamo guardado correctamente.");
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error al guardar préstamo: {errorContent}");
                    MessageBox.Show("Error al guardar el préstamo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
