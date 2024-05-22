using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TimeTrack.Model;
using TimeTrack.Presenter;
using static TimeTrack.Model.Model;

namespace TimeTrack.View
{
    public partial class FormMain : Form
    {
        private Model.Model model;
        private Presenter.Presenter _presenter;

        int idempleado;

        public FormMain(int idEmpleado, string nombreEmpleado, string apellidoEmpleado, string cargoEmpleado)
        {
            idempleado = idEmpleado;
            InitializeComponent();
            lblUser.Text = "Nombre: " + nombreEmpleado + " " + apellidoEmpleado;
            lblCargo.Text = "Rol: " + cargoEmpleado;

            if(idEmpleado != 1)
            {
                btnNominaAdmin.Visible = false;
            }

        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            Utilities.BorderRadius(panelChild, 10);
            Utilities.BorderRadius(panelDatosUser, 10);
            _presenter = new Presenter.Presenter();
            panel2.AutoScroll = true;
            panel2.HorizontalScroll.Visible = false;
            panel2.VerticalScroll.Visible = false;
            panel2.SetAutoScrollMargin(0, 100);
            
        }

        private Dictionary<string, Form> _OpenForms = new Dictionary<string, Form>();
        private void button1_Click(object sender, EventArgs e)
        {
            FormInOut form1 = new FormInOut(idempleado);
            _presenter.ShowOrOpenFormInPanel(form1, "FormInOut", panelChild);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormDetalleEmpleado form1 = new FormDetalleEmpleado();
            _presenter.ShowOrOpenFormInPanel(form1, "FormDetalleEmpleado", panelChild);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormNominaEmpleado form1 = new FormNominaEmpleado(idempleado);
            _presenter.ShowOrOpenFormInPanel(form1, "FormNominaEmpleado", panelChild);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormNomina form1 = new FormNomina();
            _presenter.ShowOrOpenFormInPanel(form1, "FormNomina", panelChild);
        }
    }
}
