using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TimeTrack.Presenter;
using static TimeTrack.Model.Model;

namespace TimeTrack.View
{
    public partial class FormMain : Form, IMainView
    {
        private Model.Model model;
        private Presenter.Presenter _presenter;

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
            _presenter = new Presenter.Presenter(this);
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
            FormInOut form1 = new FormInOut();
            _presenter.ShowOrOpenFormInPanel(form1, "FormInOut", panelChild);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormNominaEmpleado form1 = new FormNominaEmpleado();
            _presenter.ShowOrOpenFormInPanel(form1, "FormNominaEmpleado", panelChild);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormNominaEmpleado form1 = new FormNominaEmpleado();
            _presenter.ShowOrOpenFormInPanel(form1, "FormNominaEmpleado", panelChild);
        }
    }
}
