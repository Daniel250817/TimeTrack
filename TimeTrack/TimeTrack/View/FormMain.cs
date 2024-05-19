using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static TimeTrack.Model.Model;

namespace TimeTrack.View
{
    public partial class FormMain : Form, IMainView
    {
        private Model.Model model;
        public FormMain(string nombreEmpleado, string apellidoEmpleado, string cargoEmpleado)
        {
            InitializeComponent();
            lblUser.Text = "Nombre: " + nombreEmpleado + " " + apellidoEmpleado;
            lblCargo.Text = "Rol: " + cargoEmpleado;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            Utilities.BorderRadius(panelChild, 10);
            Utilities.BorderRadius(panelDatosUser, 10);

        }

        public void ShowFormPanel(Form formulario)
        {
            if (panelChild.Controls.Count > 0)
                panelChild.Controls.RemoveAt(0);

            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;
            panelChild.Controls.Add(formulario);
            panelChild.Tag = formulario;
            formulario.Show();
        }

        private Dictionary<string, Form> _OpenForms = new Dictionary<string, Form>();
        private void button1_Click(object sender, EventArgs e)
        {
            // Verificar si ya hay una instancia del formulario FormLogin
            Form existingForm = null;
            if (_OpenForms.ContainsKey("FormLogin"))
            {
                existingForm = _OpenForms["FormLogin"];
            }

            if (existingForm != null)
            {
                // Si existe una instancia, mostrarla
                ShowFormPanel(existingForm);
            }
            else
            {
                // Si no existe, crear una nueva instancia y mostrarla
                FormLogin form1 = new FormLogin();
                _OpenForms["FormLogin"] = form1;
                ShowFormPanel(form1);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Verificar si ya hay una instancia del formulario FormLogin
            Form existingForm = null;
            if (_OpenForms.ContainsKey("FormInOut"))
            {
                existingForm = _OpenForms["FormInOut"];
            }

            if (existingForm != null)
            {
                // Si existe una instancia, mostrarla
                ShowFormPanel(existingForm);
            }
            else
            {
                // Si no existe, crear una nueva instancia y mostrarla
                FormInOut form2 = new FormInOut();
                _OpenForms["FormInOut"] = form2;
                ShowFormPanel(form2);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
