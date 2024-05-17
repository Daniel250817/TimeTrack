using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimeTrack.View
{
    public partial class FormMain : Form
    {
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

        private void lblUser_Click(object sender, EventArgs e)
        {

        }

        private void lblCargo_Click(object sender, EventArgs e)
        {

        }
    }
}
