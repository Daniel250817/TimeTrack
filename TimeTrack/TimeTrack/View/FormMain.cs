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

        private void button1_Click(object sender, EventArgs e)
        {
            FormLogin form1 = new FormLogin();
            ShowFormPanel(form1);
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
