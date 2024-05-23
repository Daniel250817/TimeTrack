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

namespace TimeTrack.View
{
    public partial class FormDetalleEmpleado : Form, IEmpleado
    {
        private Presenter.Presenter _presenter;
        int Empleado;
        public FormDetalleEmpleado(int EmpleadoLogueado)
        {
            InitializeComponent();
            _presenter = new Presenter.Presenter(this);
            _presenter.SetEmpleadoView(this);
            Empleado = EmpleadoLogueado; 
            Utilities.BorderRadius(panelContainer, 10);
            panelContainer.AutoScroll = true;
            panelContainer.VerticalScroll.Visible = true;
            panelContainer.HorizontalScroll.Visible = true;
            panelContainer.SetAutoScrollMargin(0, 100);

            txtIdEmpleado.Enabled = false;
            txtCargo.Enabled = false;
        }

        private void FormDetalleEmpleado_Load(object sender, EventArgs e)
        {
            _presenter.DatosEmpleadoLogueado(Empleado);
        }

        public void MostrarEmpleadoLogueado(Empleado empleado)
        {
            txtNombre.Text = empleado.Nombres;
            txtApellidos.Text = empleado.Apellidos;
            txtCargo.Text = Convert.ToString(empleado.IdCargo);
            txtDireccion.Text = empleado.Direccion;
            txtTelefono.Text = empleado.Telefono;
            txtIdEmpleado.Text = Convert.ToString(empleado.IdEmpleado);
            dtpFechaNac.Text = empleado.FechaNacimiento.ToString();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            // Obtener los valores editados de los TextBoxes
            int idEmpleado = Convert.ToInt32(txtIdEmpleado.Text);
            string nombres = txtNombre.Text;
            string apellidos = txtApellidos.Text;
            DateTime fechaNacimiento = dtpFechaNac.Value;
            string direccion = txtDireccion.Text;
            int idCargo = Convert.ToInt32(txtCargo.Text);
            string telefono = txtTelefono.Text;

            // Crear un objeto Empleado con los valores editados
            Empleado empleado = new Empleado
            {
                IdEmpleado = idEmpleado,
                Nombres = nombres,
                Apellidos = apellidos,
                FechaNacimiento = fechaNacimiento,
                Direccion = direccion,
                IdCargo = idCargo,
                Telefono = telefono
            };

            // Solicitar al Presenter que actualice los datos del empleado
            _presenter.ActualizarEmpleadoLogueado(empleado);
        }

        public void MostrarMensaje(string mensaje, string titulo, MessageBoxButtons botones, MessageBoxIcon icono)
        {
            MessageBox.Show(this, mensaje, titulo, botones, icono);
        }
    }
}
