using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Presentacion_Escritorio
{
    public partial class AddClientForm : Form
    {
        public Client NewClient { get; private set; }

        public AddClientForm()
        {
            InitializeComponent();
            InitializeFormComponents();
        }

        private void InitializeFormComponents()
        {
            var lblCedula = new Label { Text = "Cédula del Cliente:", Dock = DockStyle.Top };
            var txtCedula = new TextBox { Dock = DockStyle.Top, Name = "txtCedula" };
            txtCedula.KeyPress += TxtCedula_KeyPress;
            txtCedula.Leave += TxtCedula_Leave;

            var lblNombre = new Label { Text = "Nombre del Cliente:", Dock = DockStyle.Top };
            var txtNombre = new TextBox { Dock = DockStyle.Top, Name = "txtNombre" };

            var lblApellido = new Label { Text = "Apellido del Cliente:", Dock = DockStyle.Top };
            var txtApellido = new TextBox { Dock = DockStyle.Top, Name = "txtApellido" };

            var lblTelefono = new Label { Text = "Teléfono del Cliente:", Dock = DockStyle.Top };
            var txtTelefono = new TextBox { Dock = DockStyle.Top, Name = "txtTelefono" };
            txtTelefono.KeyPress += TxtTelefono_KeyPress;
            txtTelefono.Leave += TxtTelefono_Leave;

            var lblDireccion = new Label { Text = "Dirección del Cliente:", Dock = DockStyle.Top };
            var txtDireccion = new TextBox { Dock = DockStyle.Top, Name = "txtDireccion" };

            var lblFechaNac = new Label { Text = "Fecha de Nacimiento:", Dock = DockStyle.Top };
            var dtpFechaNac = new DateTimePicker { Dock = DockStyle.Top, Name = "dtpFechaNac", Format = DateTimePickerFormat.Short };

            var lblEstado = new Label { Text = "Estado del Cliente:", Dock = DockStyle.Top };
            var chkEstado = new CheckBox { Dock = DockStyle.Top, Name = "chkEstado", Text = "Activo", Checked = true };

            var btnSave = new Button { Text = "Guardar", Dock = DockStyle.Left };
            var btnCancel = new Button { Text = "Cancelar", Dock = DockStyle.Right };

            btnSave.Click += BtnSave_Click;
            btnCancel.Click += (sender, e) => this.Close();

            var panelButtons = new Panel { Dock = DockStyle.Top, Height = 40 };
            panelButtons.Controls.Add(btnSave);
            panelButtons.Controls.Add(btnCancel);

            this.Controls.Add(panelButtons);
            this.Controls.Add(chkEstado);
            this.Controls.Add(lblEstado);
            this.Controls.Add(dtpFechaNac);
            this.Controls.Add(lblFechaNac);
            this.Controls.Add(txtDireccion);
            this.Controls.Add(lblDireccion);
            this.Controls.Add(txtTelefono);
            this.Controls.Add(lblTelefono);
            this.Controls.Add(txtApellido);
            this.Controls.Add(lblApellido);
            this.Controls.Add(txtNombre);
            this.Controls.Add(lblNombre);
            this.Controls.Add(txtCedula);
            this.Controls.Add(lblCedula);

            this.Text = "Agregar Nuevo Cliente";
            this.Size = new Size(400, 600);
        }

        private void AddClientForm_Load(object sender, EventArgs e)
        {
            // Lógica que se ejecuta cuando el formulario se carga
        }

        private void TxtCedula_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Solo permitir números en el campo de cédula
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtCedula_Leave(object sender, EventArgs e)
        {
            var txtCedula = sender as TextBox;
            if (txtCedula.Text.Length != 10 || !Regex.IsMatch(txtCedula.Text, @"^\d{10}$"))
            {
                MessageBox.Show("La cédula debe contener exactamente 10 dígitos numéricos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCedula.Focus();
            }
        }

        private void TxtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Solo permitir números en el campo de teléfono
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtTelefono_Leave(object sender, EventArgs e)
        {
            var txtTelefono = sender as TextBox;
            if (txtTelefono.Text.Length != 10 || !Regex.IsMatch(txtTelefono.Text, @"^\d{10}$"))
            {
                MessageBox.Show("El teléfono debe contener exactamente 10 dígitos numéricos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTelefono.Focus();
            }
        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            var txtCedula = this.Controls["txtCedula"] as TextBox;
            var txtNombre = this.Controls["txtNombre"] as TextBox;
            var txtApellido = this.Controls["txtApellido"] as TextBox;
            var txtTelefono = this.Controls["txtTelefono"] as TextBox;
            var txtDireccion = this.Controls["txtDireccion"] as TextBox;
            var dtpFechaNac = this.Controls["dtpFechaNac"] as DateTimePicker;
            var chkEstado = this.Controls["chkEstado"] as CheckBox;

            if (txtCedula.Text.Length != 10 || !Regex.IsMatch(txtCedula.Text, @"^\d{10}$"))
            {
                MessageBox.Show("La cédula debe contener exactamente 10 dígitos numéricos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtTelefono.Text.Length != 10 || !Regex.IsMatch(txtTelefono.Text, @"^\d{10}$"))
            {
                MessageBox.Show("El teléfono debe contener exactamente 10 dígitos numéricos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var newClient = new Client
            {
                CEDULACLIENTE = txtCedula.Text,
                NOMBRECLIENTE = txtNombre.Text,
                APELLIDOCLIENTE = txtApellido.Text,
                TELEFONOCLIENTE = txtTelefono.Text,
                DIRECCLIENTE = txtDireccion.Text,
                FECHANACCLIENTE = dtpFechaNac.Value,
                ESTADOCLIENTE = chkEstado.Checked
            };

            NewClient = newClient;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
