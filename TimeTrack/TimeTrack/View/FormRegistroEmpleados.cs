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
    public partial class FormRegistroEmpleados : Form, InterfaceCrud
    {
        private Presenter.Presenter _presenter;
        public DataGridView DataGridViewCRUD
        {
            get
            {
                return dgvRegistroEmpleados;
            }
        }
        public FormRegistroEmpleados()
        {
            InitializeComponent();
            _presenter = new Presenter.Presenter(this);
            Utilities.BorderRadius(panelTop, 10);
            txtIdEmpleado.Visible = false;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FormRegistroEmpleados_Load(object sender, EventArgs e)
        {
            _presenter.MostrarEmpleados(dgvRegistroEmpleados);
        }

        private void dgvNominaAdmin_SelectionChanged(object sender, EventArgs e)
        {
            // Verificar si hay una fila seleccionada
            if (dgvRegistroEmpleados.SelectedRows.Count > 0)
            {
                // Obtener la fila seleccionada
                DataGridViewRow filaSeleccionada = dgvRegistroEmpleados.SelectedRows[0];

                // Verificar si la fila seleccionada no está vacía
                if (!filaSeleccionada.IsNewRow && filaSeleccionada.Cells.Cast<DataGridViewCell>().All(cell => cell.Value != null))
                {
                    txtIdEmpleado.Text = filaSeleccionada.Cells["idEmpleado"].Value.ToString();
                    txtNombres.Text = filaSeleccionada.Cells["nombres"].Value.ToString();
                    txtApellidos.Text = filaSeleccionada.Cells["apellidos"].Value.ToString();
                    dtpFechaNac.Value = Convert.ToDateTime(filaSeleccionada.Cells["fechaNacimiento"].Value);
                    txtDireccion.Text = filaSeleccionada.Cells["direccion"].Value.ToString();
                    txtIdCargo.Text = filaSeleccionada.Cells["idCargo"].Value.ToString();
                    txtTelefono.Text = filaSeleccionada.Cells["telefono"].Value.ToString();
                }
                else
                {
                    LimpiarCampos();
                }
            }
        }

        private void dgvNominaAdmin_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Verificar si la celda actual pertenece a la columna de fecha
            if (dgvRegistroEmpleados.Columns[e.ColumnIndex].Name == "fecha")
            {
                // Verificar si el valor de la celda es de tipo DateTime
                if (e.Value != null && e.Value.GetType() == typeof(DateTime))
                {
                    // Formatear la fecha y establecer el valor formateado en la celda
                    e.Value = ((DateTime)e.Value).ToShortDateString();
                    e.FormattingApplied = true;
                }
            }
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            if (!_presenter.ValidarCamposEmpleado(txtNombres.Text, txtApellidos.Text, dtpFechaNac.Text, txtDireccion.Text, txtIdCargo.Text, txtTelefono.Text))
            {
                return;
            }

            Empleado empleado = new Empleado
            {
                Nombres = txtNombres.Text,
                Apellidos = txtApellidos.Text,
                FechaNacimiento = dtpFechaNac.Value,
                Direccion = txtDireccion.Text,
                IdCargo = Convert.ToInt32(txtIdCargo.Text),
                Telefono = txtTelefono.Text
            };

            _presenter.InsertarEmpleado(empleado);
        }


        private void btnActu_Click(object sender, EventArgs e)
        {
            if (!_presenter.ValidarCamposEmpleado(txtNombres.Text, txtApellidos.Text, dtpFechaNac.Text, txtDireccion.Text, txtIdCargo.Text, txtTelefono.Text))
            {
                return;
            }

            // Obtener los valores editados de los TextBoxes
            int idEmpleado = Convert.ToInt32(txtIdEmpleado.Text);
            string nombres = txtNombres.Text;
            string apellidos = txtApellidos.Text;
            DateTime fechaNacimiento = dtpFechaNac.Value;
            string direccion = txtDireccion.Text;
            int idCargo = Convert.ToInt32(txtIdCargo.Text);
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
            _presenter.ActualizarEmpleado(empleado);
            LimpiarCampos(); // Limpia los campos después de la actualización
            _presenter.MostrarEmpleados(dgvRegistroEmpleados); // Actualizar el DataGridView con los nuevos datos
        }


        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Verificar si hay una fila seleccionada en el DataGridView
            if (dgvRegistroEmpleados.SelectedRows.Count > 0)
            {
                // Obtener el ID del empleado seleccionado en el DataGridView
                int idEmpleado = Convert.ToInt32(dgvRegistroEmpleados.SelectedRows[0].Cells["IdEmpleado"].Value);

                // Llamar al método en el presentador para eliminar el empleado
                _presenter.EliminarEmpleado(idEmpleado);

                // Opcionalmente, puedes limpiar los TextBoxes después de eliminar el empleado
                LimpiarCampos();
            }
            else
            {
                // Mostrar un mensaje indicando que no se ha seleccionado ningún empleado
                MessageBox.Show("Por favor, seleccione un empleado para eliminar.", "Ningún empleado seleccionado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LimpiarCampos()
        {
            txtIdEmpleado.Text = "";
            txtNombres.Text = "";
            txtApellidos.Text = "";
            txtDireccion.Text = "";
            txtIdCargo.Text = "";
            txtTelefono.Text = "";
            dtpFechaNac.Value = DateTime.Now;
        }



        public void MostrarMensaje(string mensaje, string titulo, MessageBoxButtons botones, MessageBoxIcon icono)
        {
            MessageBox.Show(this, mensaje, titulo, botones, icono);
        }

    }
}
