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
                btnRegistro.Visible = false;
                btnNominaAdmin.Visible = false;
                btnJornadaAdmin.Visible = false;
                btnRegistroHora.Visible = false;
                btnUsers.Visible = false;
                btnHorariosAdmin.Visible = false;


            }

        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            Utilities.BorderRadius(panelChild, 10);
            Utilities.BorderRadius(panelDatosUser, 10);
            Utilities.AjustarOpacidad(panelDatosUser);
            _presenter = new Presenter.Presenter();
            panel2.AutoScroll = true;
            panel2.HorizontalScroll.Visible = false;
            panel2.VerticalScroll.Visible = true;
            panel2.SetAutoScrollMargin(0, 100);
            FormInOut form1 = new FormInOut(idempleado);
            _presenter.ShowOrOpenFormInPanel(form1, "FormInOut", panelChild);

        }

        private Dictionary<string, Form> _OpenForms = new Dictionary<string, Form>();
        private void button1_Click(object sender, EventArgs e)
        {
            FormInOut form1 = new FormInOut(idempleado);
            _presenter.ShowOrOpenFormInPanel(form1, "FormInOut", panelChild);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormDetalleEmpleado form1 = new FormDetalleEmpleado(idempleado);
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

        private void button5_Click(object sender, EventArgs e)
        {
            FormRegistroHorario form1 = new FormRegistroHorario();
            _presenter.ShowOrOpenFormInPanel(form1, "FormRegistroHorario", panelChild);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            FormJornada jornada = new FormJornada(idempleado);
            _presenter.ShowOrOpenFormInPanel(jornada, "FormJornada", panelChild);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            FormRegistroEmpleados formRegistroEmpleados = new FormRegistroEmpleados();
            _presenter.ShowOrOpenFormInPanel(formRegistroEmpleados, "FormRegistroEmpleados", panelChild);
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            FormUsuarios formUsuarios = new FormUsuarios();
            _presenter.ShowOrOpenFormInPanel(formUsuarios, "FormUsuarios", panelChild);
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            FormJornadaAdmin formJornadaAdmin = new FormJornadaAdmin();
            _presenter.ShowOrOpenFormInPanel(formJornadaAdmin, "FormJornadaAdmin", panelChild);
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            FormHorariosJornadas formHorariosJornadas = new FormHorariosJornadas();
            _presenter.ShowOrOpenFormInPanel(formHorariosJornadas, "FormHorariosJornadas", panelChild);
        }
    }
}
