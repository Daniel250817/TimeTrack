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

namespace TimeTrack.View
{
    public partial class FormUsuarios : Form, InterfaceCrud
    {

        private Presenter.Presenter _presenter;
        public DataGridView DataGridViewCRUD
        {
            get
            {
                return dgvUsuarios;
            }
        }
        public FormUsuarios()
        {
            InitializeComponent();
            _presenter = new Presenter.Presenter(this);
            Utilities.BorderRadius(panelTop, 10);
        }
        private void FormUsuarios_Load(object sender, EventArgs e)
        {
            txtIdUsuarios.Enabled = false;
            _presenter.MostrarUsuarios(dgvUsuarios);
        }

        private void dgvNominaAdmin_SelectionChanged(object sender, EventArgs e)
        {
            // Verificar si hay una fila seleccionada
            if (dgvUsuarios.SelectedRows.Count > 0)
            {
                // Obtener la fila seleccionada
                DataGridViewRow filaSeleccionada = dgvUsuarios.SelectedRows[0];

                // Verificar si la fila seleccionada no está vacía
                if (!filaSeleccionada.IsNewRow && filaSeleccionada.Cells.Cast<DataGridViewCell>().All(cell => cell.Value != null))
                {
                    txtIdUsuarios.Text = filaSeleccionada.Cells["idUsuario"].Value.ToString();
                    txtIdEmpleado.Text = filaSeleccionada.Cells["idEmpleado"].Value.ToString();
                    txtNombreUsuario.Text = filaSeleccionada.Cells["nombreUsuario"].Value.ToString();
                    txtContrasena.Text = filaSeleccionada.Cells["contrasena"].Value.ToString();
                }
                else
                {
                    LimpiarCampos();
                }
            }
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            if (!_presenter.ValidarCamposUsuario(txtNombreUsuario.Text, txtContrasena.Text, txtIdEmpleado.Text))
            {
                return;
            }

            Usuario usuario = new Usuario
            {
                idUsuario = Convert.ToInt32(txtIdEmpleado.Text),
                NombreUsuario = txtNombreUsuario.Text,
                Contrasena = txtContrasena.Text,
                idEmpleado = Convert.ToInt32(txtIdEmpleado.Text)
            };

            _presenter.InsertarUsuario(usuario);
        }


        private void btnActu_Click(object sender, EventArgs e)
        {
            if (!_presenter.ValidarCamposUsuario(txtNombreUsuario.Text, txtContrasena.Text, txtIdEmpleado.Text))
            {
                return;
            }

            // Obtener los valores editados de los TextBoxes
            int idUsuario = Convert.ToInt32(txtIdUsuarios.Text);
            string nombreUsuario = txtNombreUsuario.Text;
            string contrasena = txtContrasena.Text;
            int idEmpleado = Convert.ToInt32(txtIdEmpleado.Text);

            // Crear un objeto Usuario con los valores editados
            Usuario usuario = new Usuario
            {
                idUsuario = idUsuario,
                NombreUsuario = nombreUsuario,
                Contrasena = contrasena,
                idEmpleado = idEmpleado
            };

            // Solicitar al Presenter que actualice los datos del usuario
            _presenter.ActualizarUsuario(usuario);
            LimpiarCampos(); // Limpia los campos después de la actualización
            _presenter.MostrarUsuarios(dgvUsuarios); // Actualizar el DataGridView con los nuevos datos
        }


        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Verificar si hay una fila seleccionada en el DataGridView
            if (dgvUsuarios.SelectedRows.Count > 0)
            {
                // Obtener el ID del usuario seleccionado en el DataGridView
                int idUsuario = Convert.ToInt32(dgvUsuarios.SelectedRows[0].Cells["idUsuario"].Value);

                // Llamar al método en el presentador para eliminar el usuario
                _presenter.EliminarUsuario(idUsuario);

                // Opcionalmente, puedes limpiar los TextBoxes después de eliminar el usuario
                LimpiarCampos();
            }
            else
            {
                // Mostrar un mensaje indicando que no se ha seleccionado ningún usuario
                MessageBox.Show("Por favor, seleccione un usuario para eliminar.", "Ningún usuario seleccionado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void LimpiarCampos()
        {
            txtIdUsuarios.Text = "";
            txtIdEmpleado.Text = "";
            txtNombreUsuario.Text = "";
            txtContrasena.Text = "";
        }

        public void MostrarMensaje(string mensaje, string titulo, MessageBoxButtons botones, MessageBoxIcon icono)
        {
            MessageBox.Show(this, mensaje, titulo, botones, icono);
        }
    }
        
}
