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
    public partial class FormHorariosJornadas : Form, InterfaceCrud
    {
        private Presenter.Presenter _presenter;
        public DataGridView DataGridViewCRUD
        {
            get
            {
                return dgvRegistrosHorarios;
            }
        }
        public FormHorariosJornadas()
        {
            InitializeComponent();
            _presenter = new Presenter.Presenter(this);
            Utilities.BorderRadius(panelTop, 10);
            txtIdHorario.Visible = false;
        }

        private void FormHorariosJornadas_Load(object sender, EventArgs e)
        {
            _presenter.MostrarRegistrosHorarios(dgvRegistrosHorarios);
        }

        private void dgvNominaAdmin_SelectionChanged(object sender, EventArgs e)
        {
            // Verificar si hay una fila seleccionada
            if (dgvRegistrosHorarios.SelectedRows.Count > 0)
            {
                // Obtener la fila seleccionada
                DataGridViewRow filaSeleccionada = dgvRegistrosHorarios.SelectedRows[0];

                // Verificar si la fila seleccionada no está vacía
                if (!filaSeleccionada.IsNewRow && filaSeleccionada.Cells.Cast<DataGridViewCell>().All(cell => cell.Value != null))
                {
                    txtIdHorario.Text = filaSeleccionada.Cells["idHorario"].Value.ToString();
                    txtNombre.Text = filaSeleccionada.Cells["nombreHorario"].Value.ToString();
                    txtLVEntrada.Text = filaSeleccionada.Cells["entradaLunesViernes"].Value.ToString();
                    txtLVSalida.Text = filaSeleccionada.Cells["salidaLunesViernes"].Value.ToString();
                    txtSbEntrada.Text = filaSeleccionada.Cells["entradaSabado"].Value.ToString();
                    txtSbSalida.Text = filaSeleccionada.Cells["salidaSabado"].Value.ToString();
                }
                else
                {
                    LimpiarCampos();
                }
            }
        }


        private void LimpiarCampos()
        {
            txtIdHorario.Text = "";
            txtNombre.Text = "";
            txtLVEntrada.Text = "";
            txtLVSalida.Text = "";
            txtSbEntrada.Text = "";
            txtSbSalida.Text = "";
        }

        public void MostrarMensaje(string mensaje, string titulo, MessageBoxButtons botones, MessageBoxIcon icono)
        {
            MessageBox.Show(this, mensaje, titulo, botones, icono);
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            if (!_presenter.ValidarCamposRegistrosHorario(txtNombre.Text, txtLVEntrada.Text, txtLVSalida.Text, txtSbEntrada.Text, txtSbSalida.Text))
            {
                return;
            }

            Horario horario = new Horario
            {
                
                nombreHorario = txtNombre.Text,
                entradaLunesViernes = txtLVEntrada.Text,
                salidaLunesViernes = txtLVSalida.Text,
                entradaSabado = txtSbEntrada.Text,
                salidaSabado = txtSbSalida.Text
            };

            _presenter.InsertarRegistrosHorario(horario);
        }

        private void btnActu_Click(object sender, EventArgs e)
        {
            if (!_presenter.ValidarCamposRegistrosHorario(txtNombre.Text, txtLVEntrada.Text, txtLVSalida.Text, txtSbEntrada.Text, txtSbSalida.Text))
            {
                return;
            }

            // Convertir los valores de texto a TimeSpan y luego a string en formato "c" (general long form)
            TimeSpan entradaLV = TimeSpan.Parse(txtLVEntrada.Text);
            TimeSpan salidaLV = TimeSpan.Parse(txtLVSalida.Text);
            TimeSpan entradaSB = TimeSpan.Parse(txtSbEntrada.Text);
            TimeSpan salidaSB = TimeSpan.Parse(txtSbSalida.Text);

            // Crear un objeto Horario con los valores editados
            Horario horario = new Horario
            {
                idHorario = Convert.ToInt32(txtIdHorario.Text),
                nombreHorario = txtNombre.Text,
                entradaLunesViernes = entradaLV.ToString(@"hh\:mm"),
                salidaLunesViernes = salidaLV.ToString(@"hh\:mm"),
                entradaSabado = entradaSB.ToString(@"hh\:mm"),
                salidaSabado = salidaSB.ToString(@"hh\:mm")
            };

            // Solicitar al Presenter que actualice los datos del horario
            _presenter.ActualizarRegistrosHorario(horario);
            LimpiarCampos(); // Limpia los campos después de la actualización
            _presenter.MostrarRegistrosHorarios(dgvRegistrosHorarios); // Actualizar el DataGridView con los nuevos datos
        }


        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Verificar si hay una fila seleccionada en el DataGridView
            if (dgvRegistrosHorarios.SelectedRows.Count > 0)
            {
                // Obtener el ID del empleado seleccionado en el DataGridView
                int idHorario = Convert.ToInt32(dgvRegistrosHorarios.SelectedRows[0].Cells["idHorario"].Value);

                // Llamar al método en el presentador para eliminar el empleado
                _presenter.EliminarRegistrosHorario(idHorario);

                // Opcionalmente, puedes limpiar los TextBoxes después de eliminar el empleado
                LimpiarCampos();
            }
            else
            {
                // Mostrar un mensaje indicando que no se ha seleccionado ningún empleado
                MessageBox.Show("Por favor, seleccione un empleado para eliminar.", "Ningún empleado seleccionado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
