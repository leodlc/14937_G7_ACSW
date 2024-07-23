using System;
using System.Drawing;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Presentacion_Escritorio
{
    public partial class AddGenreForm : Form
    {
        public LiteraryGenre NewGenre { get; private set; }

        public AddGenreForm()
        {
            InitializeComponent();
            InitializeFormComponents();
        }

        private void InitializeFormComponents()
        {
            var lblName = new Label { Text = "Nombre del Género Literario:", Dock = DockStyle.Top };
            var txtName = new TextBox { Dock = DockStyle.Top, Name = "txtName" };

            var lblDescription = new Label { Text = "Descripción:", Dock = DockStyle.Top };
            var txtDescription = new TextBox { Dock = DockStyle.Top, Name = "txtDescription", Multiline = true, Height = 100 };

            var btnSave = new Button { Text = "Guardar", Dock = DockStyle.Left };
            var btnCancel = new Button { Text = "Cancelar", Dock = DockStyle.Right };

            btnSave.Click += BtnSave_Click;
            btnCancel.Click += (sender, e) => this.Close();

            var panelButtons = new Panel { Dock = DockStyle.Top, Height = 40 };
            panelButtons.Controls.Add(btnSave);
            panelButtons.Controls.Add(btnCancel);

            this.Controls.Add(panelButtons);
            this.Controls.Add(txtDescription);
            this.Controls.Add(lblDescription);
            this.Controls.Add(txtName);
            this.Controls.Add(lblName);

            this.Text = "Agregar Nuevo Género Literario";
            this.Size = new Size(400, 300);
        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            var txtName = this.Controls["txtName"] as TextBox;
            var txtDescription = this.Controls["txtDescription"] as TextBox;

            var newGenre = new LiteraryGenre
            {
                NOMBREGL = txtName.Text,
                DESCRIPGL = txtDescription.Text
            };

            var confirmation = MessageBox.Show(
                $"Nombre: {newGenre.NOMBREGL}\nDescripción: {newGenre.DESCRIPGL}",
                "Confirmar Datos",
                MessageBoxButtons.OKCancel);

            if (confirmation == DialogResult.OK)
            {
                await SaveGenreAsync(newGenre);
                MessageBox.Show("Género literario guardado", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void AddGenreForm_Load(object sender, EventArgs e)
        {
            // Lógica que se ejecuta cuando el formulario se carga
        }

        private async Task SaveGenreAsync(LiteraryGenre genre)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var json = JsonConvert.SerializeObject(genre);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync("http://localhost:54845/api/LiteraryGenre/AddGenre", content);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Género literario guardado correctamente.");
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error al guardar género literario: {errorContent}");
                    MessageBox.Show("Error al guardar el género literario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
