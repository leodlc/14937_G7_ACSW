using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Presentacion_Escritorio
{
    public partial class AddAuthorForm : Form
    {
        public Author NewAuthor { get; private set; }

        public AddAuthorForm()
        {
            InitializeComponent();
            InitializeFormComponents();
        }

        private void InitializeFormComponents()
        {
            var lblName = new Label { Text = "Nombre del Autor:", Dock = DockStyle.Top };
            var txtName = new TextBox { Dock = DockStyle.Top, Name = "txtName" };

            var lblSurname = new Label { Text = "Apellido del Autor:", Dock = DockStyle.Top };
            var txtSurname = new TextBox { Dock = DockStyle.Top, Name = "txtSurname" };

            var lblNationality = new Label { Text = "Nacionalidad del Autor:", Dock = DockStyle.Top };
            var txtNationality = new TextBox { Dock = DockStyle.Top, Name = "txtNationality" };

            var lblBiography = new Label { Text = "Biografía del Autor:", Dock = DockStyle.Top };
            var txtBiography = new TextBox { Dock = DockStyle.Top, Name = "txtBiography", Multiline = true, Height = 100 };

            var btnSave = new Button { Text = "Guardar", Dock = DockStyle.Left };
            var btnCancel = new Button { Text = "Cancelar", Dock = DockStyle.Right };

            btnSave.Click += BtnSave_Click;
            btnCancel.Click += (sender, e) => this.Close();

            var panelButtons = new Panel { Dock = DockStyle.Top, Height = 40 };
            panelButtons.Controls.Add(btnSave);
            panelButtons.Controls.Add(btnCancel);

            this.Controls.Add(panelButtons);
            this.Controls.Add(txtBiography);
            this.Controls.Add(lblBiography);
            this.Controls.Add(txtNationality);
            this.Controls.Add(lblNationality);
            this.Controls.Add(txtSurname);
            this.Controls.Add(lblSurname);
            this.Controls.Add(txtName);
            this.Controls.Add(lblName);

            this.Text = "Agregar Nuevo Autor";
            this.Size = new Size(400, 400);
        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            var txtName = this.Controls["txtName"] as TextBox;
            var txtSurname = this.Controls["txtSurname"] as TextBox;
            var txtNationality = this.Controls["txtNationality"] as TextBox;
            var txtBiography = this.Controls["txtBiography"] as TextBox;

            var newAuthor = new Author
            {
                NOMBREAUTOR = txtName.Text,
                APELLIDOAUTOR = txtSurname.Text,
                NACIONALIDADAUTOR = txtNationality.Text,
                BIOGRAFIAAUTOR = txtBiography.Text
            };

            var confirmation = MessageBox.Show(
                $"Nombre: {newAuthor.NOMBREAUTOR}\nApellido: {newAuthor.APELLIDOAUTOR}\nNacionalidad: {newAuthor.NACIONALIDADAUTOR}\nBiografía: {newAuthor.BIOGRAFIAAUTOR}",
                "Confirmar Datos",
                MessageBoxButtons.OKCancel);

            if (confirmation == DialogResult.OK)
            {
                await SaveAuthorAsync(newAuthor);
                MessageBox.Show("Autor guardado", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void AddAuthorForm_Load(object sender, EventArgs e)
        {
            // Lógica que se ejecuta cuando el formulario se carga
        }

        private async Task SaveAuthorAsync(Author author)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var json = JsonConvert.SerializeObject(author);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync("http://localhost:54845/api/Author/Add", content);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Autor guardado correctamente.");
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error al guardar autor: {errorContent}");
                    MessageBox.Show("Error al guardar el autor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
