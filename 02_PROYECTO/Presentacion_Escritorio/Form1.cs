using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion_Escritorio
{
    public partial class Form1 : Form
    {
        private Panel mainPanel;
        private DataGridView dataGridView;
        private BookService bookService;
        private LoanService loanService;
        private ClientService clientService; // Nueva instancia de ClientService
        private LiteraryGenreService literaryGenreService;
        private AuthorService authorService;

        private Button btnHome;
        private Button btnLoans;
        private Button btnBooks;
        private Button btnClients;
        private Button btnGenres;
        private Button btnAuthors;

        public Form1()
        {
            InitializeComponent();
            bookService = new BookService();
            loanService = new LoanService();
            clientService = new ClientService(); // Inicializar ClientService
            literaryGenreService = new LiteraryGenreService();
            authorService = new AuthorService();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeCustomComponents();
            LoadHomePage();
        }

        private void InitializeCustomComponents()
        {
            var tableLayoutPanel = new TableLayoutPanel
            {
                //Size = new System.Drawing.Size(1280, 720),
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1
            };
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            var menuPanel = CreateMenuPanel();

            mainPanel = new Panel { Dock = DockStyle.Fill, BackColor = Color.White };

            tableLayoutPanel.Controls.Add(menuPanel, 0, 0);
            tableLayoutPanel.Controls.Add(mainPanel, 1, 0);

            this.Controls.Add(tableLayoutPanel);
        }

        private Panel CreateMenuPanel()
        {
            var menuPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.WhiteSmoke
            };

            var pictureBoxLogo = new PictureBox
            {
                ImageLocation = "C:\\Users\\UsuarioLVD\\Downloads\\capi.png",
                //ImageLocation = "C:\\Users\\Shadow Moon\\OneDrive\\Escritorio\\Grupo-distribuidas\\apps\\Libros_Proyecto\\images\\logo.jpg",
                SizeMode = PictureBoxSizeMode.StretchImage,
                Height = 100,
                Dock = DockStyle.Top
            };
            menuPanel.Controls.Add(pictureBoxLogo);

            btnHome = CreateMenuButton("INICIO", Color.DarkGreen);
            btnLoans = CreateMenuButton("PRÉSTAMOS", Color.Gray);
            btnBooks = CreateMenuButton("LIBROS", Color.Gray);
            btnClients = CreateMenuButton("CLIENTES", Color.Gray);
            btnGenres = CreateMenuButton("GÉNEROS LITERARIOS", Color.Gray);
            btnAuthors = CreateMenuButton("AUTORES", Color.Gray);

            btnHome.Click += (sender, e) => LoadHomePage();
            btnLoans.Click += (sender, e) => LoadLoanPage();
            btnBooks.Click += (sender, e) => LoadBooksPage();
            btnClients.Click += (sender, e) => LoadClientsPage();
            btnGenres.Click += (sender, e) => LoadGenresPage();
            btnAuthors.Click += (sender, e) => LoadAuthorsPage();

            menuPanel.Controls.Add(btnAuthors);
            menuPanel.Controls.Add(btnGenres);
            menuPanel.Controls.Add(btnClients);
            menuPanel.Controls.Add(btnBooks);
            menuPanel.Controls.Add(btnLoans);
            menuPanel.Controls.Add(btnHome);

            return menuPanel;
        }

        private Button CreateMenuButton(string text, Color backColor)
        {
            var button = new Button
            {
                Text = text,
                Dock = DockStyle.Top,
                Height = 50,
                BackColor = backColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            return button;
        }

        private void UpdateMenuButtonStyles(Button activeButton)
        {
            var buttons = new[] { btnHome, btnLoans, btnBooks, btnClients, btnGenres, btnAuthors };
            foreach (var button in buttons)
            {
                if (button == activeButton)
                {
                    button.BackColor = Color.DarkGreen;
                    button.ForeColor = Color.White;
                }
                else
                {
                    button.BackColor = Color.White;
                    button.ForeColor = Color.Black;
                }
            }
        }

        private void LoadHomePage()
        {
            mainPanel.Controls.Clear();
            var homeLabel = new Label
            {
                Text = "Bienvenido a la Gestión de Libros",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Arial", 24, FontStyle.Bold)
            };
            mainPanel.Controls.Add(homeLabel);
            UpdateMenuButtonStyles(btnHome);
        }

        private void LoadLoanPage()
        {
            mainPanel.Controls.Clear();

            var btnAddLoan = new Button
            {
                Text = "Añadir Préstamo",
                Dock = DockStyle.Top,
                Height = 40,
                BackColor = Color.DarkGreen,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnAddLoan.Click += BtnAddLoan_Click;
            mainPanel.Controls.Add(btnAddLoan);

            var searchPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60
            };

            var lblSearch = new Label
            {
                Text = "Buscar Préstamos por cliente",
                Dock = DockStyle.Left,
                Width = 200,
                TextAlign = ContentAlignment.MiddleLeft
            };
            searchPanel.Controls.Add(lblSearch);

            var txtSearch = new TextBox
            {
                Dock = DockStyle.Left,
                Width = 200
            };
            SetPlaceholderText(txtSearch, "ID del Cliente");
            searchPanel.Controls.Add(txtSearch);

            var btnSearch = new Button
            {
                Text = "Enviar",
                Dock = DockStyle.Left,
                Height = 30,
                BackColor = Color.DarkGreen,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnSearch.Click += async (sender, e) => await BtnSearch_Click(sender, e, txtSearch.Text);
            searchPanel.Controls.Add(btnSearch);

            mainPanel.Controls.Add(searchPanel);

            dataGridView = new DataGridView
            {
                Location = new System.Drawing.Point(0, 100),
                Size = new System.Drawing.Size(740, 600),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            mainPanel.Controls.Add(dataGridView);

            LoadAllLoans();
            UpdateMenuButtonStyles(btnLoans);
        }

        private async void LoadAllLoans()
        {
            try
            {
                var loans = await loanService.GetLoansAsync();
                if (loans != null && loans.Any())
                {
                    dataGridView.DataSource = null;
                    dataGridView.DataSource = loans;
                }
                else
                {
                    dataGridView.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Error loading loans: " + ex.Message);
            }
        }

        private async void LoadBooksPage()
        {
            mainPanel.Controls.Clear();

            var btnAddBook = new Button
            {
                Text = "Añadir Libro",
                Dock = DockStyle.Top,
                Height = 40,
                BackColor = Color.DarkGreen,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnAddBook.Click += BtnAddBook_Click;
            mainPanel.Controls.Add(btnAddBook);

            var booksPanel = new FlowLayoutPanel
            {
                //Dock = DockStyle.Fill,
                Location = new System.Drawing.Point(0, 50),
                Size = new System.Drawing.Size(740, 600),
                AutoScroll = true
            };
            mainPanel.Controls.Add(booksPanel);

            await LoadAllBooks(booksPanel);
            UpdateMenuButtonStyles(btnBooks);
        }

        private async Task LoadAllBooks(FlowLayoutPanel booksPanel)
        {
            try
            {
                var books = await bookService.GetBooksAsync();
                DisplayBooks(booksPanel, books);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Error loading books: " + ex.Message);
            }
        }

        private void DisplayBooks(FlowLayoutPanel booksPanel, List<Book> books)
        {
            booksPanel.Controls.Clear();
            foreach (var book in books)
            {
                var bookPanel = CreateBookPanel(book);
                booksPanel.Controls.Add(bookPanel);
            }
        }

        private Panel CreateBookPanel(Book book)
        {
            var panel = new Panel
            {
                Width = 200,
                Height = 450,
                Margin = new Padding(10),
            };

            var pictureBox = new PictureBox
            {
                ImageLocation = book.IMGLIBRO,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Width = 180,
                Height = 200,
                Dock = DockStyle.Top
            };
            panel.Controls.Add(pictureBox);

            var lblTitle = new Label
            {
                Text = book.NOMBRELIBRO,
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Arial", 12, FontStyle.Bold)
            };
            panel.Controls.Add(lblTitle);

            var lblAuthor = new Label
            {
                Text = "Autor: " + book.AUTOR,
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter
            };
            panel.Controls.Add(lblAuthor);

            var lblYear = new Label
            {
                Text = "Año: " + book.ANIOPUBLIBRO.ToString("yyyy"),
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter
            };
            panel.Controls.Add(lblYear);

            return panel;
        }

        private async Task BtnSearch_Click(object sender, EventArgs e, string clientId)
        {
            try
            {
                var loans = await loanService.GetLoansByClientIdAsync(clientId);
                if (loans != null && loans.Any())
                {
                    dataGridView.DataSource = null;
                    dataGridView.DataSource = loans;
                }
                else
                {
                    dataGridView.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Error searching loans: " + ex.Message);
            }
        }

        private void BtnAddLoan_Click(object sender, EventArgs e)
        {
            var addLoanForm = new AddLoanForm();
            addLoanForm.ShowDialog();
        }

        private async void BtnAddBook_Click(object sender, EventArgs e)
        {
            var addBookForm = new AddBookForm();
            var result = addBookForm.ShowDialog();

            if (result == DialogResult.OK)
            {
                var newBook = addBookForm.NewBook;
                var confirmation = MessageBox.Show(
                    $"Nombre: {newBook.NOMBRELIBRO}\nAutor: {newBook.AUTOR}\nGénero Literario: {newBook.GENEROLITERARIO}\nAño: {newBook.ANIOPUBLIBRO}\nURL de Imagen: {newBook.IMGLIBRO}\nISBN: {newBook.ISBNLIBRO}\nEditorial: {newBook.EDITORIALLIBRO}\nStock: {newBook.STOCKLIBRO}",
                    "Confirmar Datos",
                    MessageBoxButtons.OKCancel);

                if (confirmation == DialogResult.OK)
                {
                    await bookService.AddBookAsync(newBook);
                    LoadBooksPage();
                }
            }
        }

        private void SetPlaceholderText(TextBox textBox, string placeholderText)
        {
            textBox.ForeColor = Color.Gray;
            textBox.Text = placeholderText;
            textBox.Enter += (sender, e) =>
            {
                if (textBox.Text == placeholderText)
                {
                    textBox.Text = "";
                    textBox.ForeColor = Color.Black;
                }
            };
            textBox.Leave += (sender, e) =>
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.Text = placeholderText;
                    textBox.ForeColor = Color.Gray;
                }
            };
        }

        private void LoadClientsPage()
        {
            mainPanel.Controls.Clear();

            var btnAddClient = new Button
            {
                Text = "Añadir Cliente",
                Dock = DockStyle.Top,
                Height = 40,
                BackColor = Color.DarkGreen,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnAddClient.Click += BtnAddClient_Click;
            mainPanel.Controls.Add(btnAddClient);

            dataGridView = new DataGridView
            {
                Location = new System.Drawing.Point(0, 50),
                Size = new System.Drawing.Size(740, 600),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            mainPanel.Controls.Add(dataGridView);

            LoadAllClients();
            UpdateMenuButtonStyles(btnClients);
        }

        private async void LoadAllClients()
        {
            try
            {
                var clients = await clientService.GetClientsAsync();
                if (clients != null && clients.Any())
                {
                    // Imprimir los datos de los clientes en la consola
                    foreach (var client in clients)
                    {
                        Console.WriteLine($"ID: {client.IDCLIENTE}, Cédula: {client.CEDULACLIENTE}, Nombre: {client.NOMBRECLIENTE}, Apellido: {client.APELLIDOCLIENTE}, Teléfono: {client.TELEFONOCLIENTE}, Dirección: {client.DIRECCLIENTE}, Fecha de Nacimiento: {client.FECHANACCLIENTE}, Estado: {client.ESTADOCLIENTE}");
                    }

                    dataGridView.DataSource = null;
                    dataGridView.DataSource = clients;
                }
                else
                {
                    dataGridView.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading clients: " + ex.Message);
            }
        }


        private async void BtnAddClient_Click(object sender, EventArgs e)
        {
            var addClientForm = new AddClientForm();
            var result = addClientForm.ShowDialog();

            if (result == DialogResult.OK)
            {
                var newClient = addClientForm.NewClient;
                var confirmation = MessageBox.Show(
                    $"Nombre: {newClient.NOMBRECLIENTE}\nApellido: {newClient.APELLIDOCLIENTE}\nCédula: {newClient.CEDULACLIENTE}\nTeléfono: {newClient.TELEFONOCLIENTE}\nDirección: {newClient.DIRECCLIENTE}\nFecha de Nacimiento: {newClient.FECHANACCLIENTE}\nEstado: {newClient.ESTADOCLIENTE}",
                    "Confirmar Datos",
                    MessageBoxButtons.OKCancel);

                if (confirmation == DialogResult.OK)
                {
                    await clientService.AddClientAsync(newClient);
                    LoadClientsPage();
                }
            }
        }


        private void LoadGenresPage()
        {
            mainPanel.Controls.Clear();

            var btnAddGenre = new Button
            {
                Text = "Añadir Género Literario",
                Dock = DockStyle.Top,
                Height = 40,
                BackColor = Color.DarkGreen,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnAddGenre.Click += BtnAddGenre_Click;
            mainPanel.Controls.Add(btnAddGenre);

            dataGridView = new DataGridView
            {
                Location = new System.Drawing.Point(0, 50),
                Size = new System.Drawing.Size(740, 600),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            mainPanel.Controls.Add(dataGridView);

            LoadAllGenres();
            UpdateMenuButtonStyles(btnGenres);
        }

        private async void LoadAllGenres()
        {
            try
            {
                var genres = await literaryGenreService.GetLiteraryGenresAsync();
                if (genres != null && genres.Any())
                {
                    dataGridView.DataSource = null;
                    dataGridView.DataSource = genres;
                }
                else
                {
                    dataGridView.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Error loading genres: " + ex.Message);
            }
        }

        private async void BtnAddGenre_Click(object sender, EventArgs e)
        {
            var addGenreForm = new AddGenreForm();
            var result = addGenreForm.ShowDialog();

            if (result == DialogResult.OK)
            {
                var newGenre = addGenreForm.NewGenre;
                var confirmation = MessageBox.Show(
                    $"Nombre: {newGenre.NOMBREGL}\nDescripción: {newGenre.DESCRIPGL}",
                    "Confirmar Datos",
                    MessageBoxButtons.OKCancel);

                if (confirmation == DialogResult.OK)
                {
                    await literaryGenreService.AddLiteraryGenreAsync(newGenre);
                    LoadGenresPage();
                }
            }
        }

        private void LoadAuthorsPage()
        {
            mainPanel.Controls.Clear();

            var btnAddAuthor = new Button
            {
                Text = "Añadir Autor",
                Dock = DockStyle.Top,
                Height = 40,
                BackColor = Color.DarkGreen,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnAddAuthor.Click += BtnAddAuthor_Click;
            mainPanel.Controls.Add(btnAddAuthor);

            dataGridView = new DataGridView
            {
                //Dock = DockStyle.Fill,
                Location = new System.Drawing.Point(0, 50),
                Size = new System.Drawing.Size(740, 600),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            mainPanel.Controls.Add(dataGridView);

            LoadAllAuthors();
            UpdateMenuButtonStyles(btnAuthors);
        }

        private async void LoadAllAuthors()
        {
            try
            {
                var authors = await authorService.GetAuthorsAsync();
                if (authors != null && authors.Any())
                {
                    dataGridView.DataSource = null;
                    dataGridView.DataSource = authors;
                }
                else
                {
                    dataGridView.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Error loading authors: " + ex.Message);
            }
        }

        private async void BtnAddAuthor_Click(object sender, EventArgs e)
        {
            var addAuthorForm = new AddAuthorForm();
            var result = addAuthorForm.ShowDialog();

            if (result == DialogResult.OK)
            {
                var newAuthor = addAuthorForm.NewAuthor;
                var confirmation = MessageBox.Show(
                    $"Nombre: {newAuthor.NOMBREAUTOR}\nApellido: {newAuthor.APELLIDOAUTOR}\nNacionalidad: {newAuthor.NACIONALIDADAUTOR}\nBiografía: {newAuthor.BIOGRAFIAAUTOR}",
                    "Confirmar Datos",
                    MessageBoxButtons.OKCancel);

                if (confirmation == DialogResult.OK)
                {
                    await authorService.AddAuthorAsync(newAuthor);
                    LoadAuthorsPage();
                }
            }
        }
    }
}
