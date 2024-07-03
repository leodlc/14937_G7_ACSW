using System;
using System.Drawing;
using System.Windows.Forms;

namespace Presentacion_Escritorio
{
    public partial class AddBookForm : Form
    {
        public Book NewBook { get; private set; }

        public AddBookForm()
        {
            InitializeComponent();
            InitializeFormComponents();
        }

        private void InitializeFormComponents()
        {
            var lblName = new Label { Text = "Nombre del Libro:", Dock = DockStyle.Top };
            var txtName = new TextBox { Dock = DockStyle.Top, Name = "txtName" };

            var lblAuthor = new Label { Text = "Autor:", Dock = DockStyle.Top };
            var txtAuthor = new TextBox { Dock = DockStyle.Top, Name = "txtAuthor" };

            var lblGenre = new Label { Text = "Género Literario:", Dock = DockStyle.Top };
            var txtGenre = new TextBox { Dock = DockStyle.Top, Name = "txtGenre" };

            var lblYear = new Label { Text = "Año de Publicación:", Dock = DockStyle.Top };
            var dtpYear = new DateTimePicker { Dock = DockStyle.Top, Name = "dtpYear", Format = DateTimePickerFormat.Short };

            var lblImageUrl = new Label { Text = "URL de Imagen:", Dock = DockStyle.Top };
            var txtImageUrl = new TextBox { Dock = DockStyle.Top, Name = "txtImageUrl" };

            var lblISBN = new Label { Text = "ISBN:", Dock = DockStyle.Top };
            var txtISBN = new TextBox { Dock = DockStyle.Top, Name = "txtISBN" };

            var lblPublisher = new Label { Text = "Editorial:", Dock = DockStyle.Top };
            var txtPublisher = new TextBox { Dock = DockStyle.Top, Name = "txtPublisher" };

            var lblStock = new Label { Text = "Stock:", Dock = DockStyle.Top };
            var txtStock = new TextBox { Dock = DockStyle.Top, Name = "txtStock" };

            var btnSave = new Button { Text = "Guardar", Dock = DockStyle.Left };
            var btnCancel = new Button { Text = "Cancelar", Dock = DockStyle.Right };

            btnSave.Click += BtnSave_Click;
            btnCancel.Click += (sender, e) => this.Close();

            var panelButtons = new Panel { Dock = DockStyle.Top, Height = 40 };
            panelButtons.Controls.Add(btnSave);
            panelButtons.Controls.Add(btnCancel);

            this.Controls.Add(panelButtons);
            this.Controls.Add(txtStock);
            this.Controls.Add(lblStock);
            this.Controls.Add(txtPublisher);
            this.Controls.Add(lblPublisher);
            this.Controls.Add(txtISBN);
            this.Controls.Add(lblISBN);
            this.Controls.Add(txtImageUrl);
            this.Controls.Add(lblImageUrl);
            this.Controls.Add(dtpYear);
            this.Controls.Add(lblYear);
            this.Controls.Add(txtGenre);
            this.Controls.Add(lblGenre);
            this.Controls.Add(txtAuthor);
            this.Controls.Add(lblAuthor);
            this.Controls.Add(txtName);
            this.Controls.Add(lblName);

            this.Text = "Agregar Nuevo Libro";
            this.Size = new Size(400, 600);
            this.Load += AddBookForm_Load;
        }

        private void AddBookForm_Load(object sender, EventArgs e)
        {
            // Lógica que se ejecuta cuando el formulario se carga
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            var txtName = this.Controls["txtName"] as TextBox;
            var txtAuthor = this.Controls["txtAuthor"] as TextBox;
            var txtGenre = this.Controls["txtGenre"] as TextBox;
            var dtpYear = this.Controls["dtpYear"] as DateTimePicker;
            var txtImageUrl = this.Controls["txtImageUrl"] as TextBox;
            var txtISBN = this.Controls["txtISBN"] as TextBox;
            var txtPublisher = this.Controls["txtPublisher"] as TextBox;
            var txtStock = this.Controls["txtStock"] as TextBox;

            if (!int.TryParse(txtStock.Text, out int stock))
            {
                MessageBox.Show("Por favor ingrese un número de stock válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var newBook = new Book
            {
                NOMBRELIBRO = txtName.Text,
                AUTOR = new Author { NOMBREAUTOR = txtAuthor.Text },
                GENEROLITERARIO = new LiteraryGenre { NOMBREGL = txtGenre.Text },
                ANIOPUBLIBRO = dtpYear.Value,
                IMGLIBRO = txtImageUrl.Text,
                ISBNLIBRO = txtISBN.Text,
                EDITORIALLIBRO = txtPublisher.Text,
                STOCKLIBRO = stock,
                ESTADOLIBRO = true
            };

            var confirmation = MessageBox.Show(
                $"Nombre: {newBook.NOMBRELIBRO}\nAutor: {newBook.AUTOR.NOMBREAUTOR}\nGénero Literario: {newBook.GENEROLITERARIO.NOMBREGL}\nAño: {newBook.ANIOPUBLIBRO}\nURL de Imagen: {newBook.IMGLIBRO}\nISBN: {newBook.ISBNLIBRO}\nEditorial: {newBook.EDITORIALLIBRO}\nStock: {newBook.STOCKLIBRO}",
                "Confirmar Datos",
                MessageBoxButtons.OKCancel);

            if (confirmation == DialogResult.OK)
            {
                NewBook = newBook;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
