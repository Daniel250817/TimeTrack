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
    public partial class FormRegistroHorario : Form, IHorarioRegistro
    {
        Presenter.Presenter _presenter;
        public DataGridView DataGridViewHorarioRegistro
        {
            get
            {
                return dgvHorariosAdmin;
            }
        }
        public FormRegistroHorario()
        {
            InitializeComponent();
            _presenter = new Presenter.Presenter(this);
            txtIdRegistroHorario.Visible = false;
            Utilities.BorderRadius(panelTop,10);
            Utilities.AjustarOpacidad(panel2);

        }

        private void FormRegistroHorario_Load(object sender, EventArgs e)
        {
            _presenter.MostrarHorarios(dgvHorariosAdmin);
        }

        public void MostrarMensaje(string mensaje, string titulo, MessageBoxButtons botones, MessageBoxIcon icono)
        {
            MessageBox.Show(this, mensaje, titulo, botones, icono);
        }

        private void dgvNomina_SelectionChanged(object sender, EventArgs e)
        {
            // Verificar si hay una fila seleccionada
            if (dgvHorariosAdmin.SelectedRows.Count > 0)
            {
                // Obtener la fila seleccionada
                DataGridViewRow filaSeleccionada = dgvHorariosAdmin.SelectedRows[0];

                // Verificar si la fila seleccionada no está vacía
                if (!filaSeleccionada.IsNewRow && filaSeleccionada.Cells.Cast<DataGridViewCell>().All(cell => cell.Value != null))
                {
                    txtIdRegistroHorario.Text = filaSeleccionada.Cells["idHorarioEmpleado"].Value.ToString();
                    txtIdEmpleado.Text = filaSeleccionada.Cells["idEmpleado"].Value.ToString();
                    txtIdHorario.Text = filaSeleccionada.Cells["idHorario"].Value.ToString();

                    // Asegurarse de que las celdas de fecha tengan valores válidos y convertirlos a DateTime
                    if (DateTime.TryParse(filaSeleccionada.Cells["fechaInicio"].Value.ToString(), out DateTime fechaInicio))
                    {
                        dtpInicio.Value = fechaInicio;
                    }
                    else
                    {
                        dtpInicio.Value = DateTime.Now; // Valor predeterminado en caso de error
                    }

                    if (DateTime.TryParse(filaSeleccionada.Cells["fechaFin"].Value.ToString(), out DateTime fechaFin))
                    {
                        dtpFin.Value = fechaFin;
                    }
                    else
                    {
                        dtpFin.Value = DateTime.Now; // Valor predeterminado en caso de error
                    }
                }
                else
                {
                    LimpiarCampos();
                }
            }
        }

        private void dgvNominaAdmin_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Verificar si la celda actual pertenece a las columnas de fecha
            if (dgvHorariosAdmin.Columns[e.ColumnIndex].Name == "fechaInicio" || dgvHorariosAdmin.Columns[e.ColumnIndex].Name == "fechaFin")
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
            if (!_presenter.ValidarCamposHorario(txtIdEmpleado.Text, txtIdHorario.Text, dtpInicio.Value.ToString(), dtpFin.Value.ToString()))
            {
                return;
            }

            // Obtener los valores de los controles
            int idEmpleado = Convert.ToInt32(txtIdEmpleado.Text);
            int idHorario = Convert.ToInt32(txtIdHorario.Text);
            DateTime fechaInicio = dtpInicio.Value;
            DateTime fechaFin = dtpFin.Value;

            // Crear un objeto RegistroHorario con los valores
            RegistroHorario registroHorario = new RegistroHorario
            {
                idEmpleado = idEmpleado,
                idHorario = idHorario,
                fechaInicio = fechaInicio,
                fechaFin = fechaFin
            };

            // Solicitar al Presenter que inserte los datos del horario
            _presenter.InsertarRegistroHorario(registroHorario);
            LimpiarCampos();

            // Actualizar el DataGridView con los nuevos datos
            _presenter.MostrarHorarios(dgvHorariosAdmin);
        }


        private void btnActu_Click(object sender, EventArgs e)
        {
            if (!_presenter.ValidarCamposHorario(txtIdEmpleado.Text, txtIdHorario.Text, dtpInicio.Value.ToString(), dtpFin.Value.ToString()))
            {
                return;
            }

            // Obtener los valores editados de los controles
            int idEmpleado = Convert.ToInt32(txtIdEmpleado.Text);
            int idHorario = Convert.ToInt32(txtIdHorario.Text);
            DateTime fechaInicio = dtpInicio.Value;
            DateTime fechaFin = dtpFin.Value;

            // Crear un objeto RegistroHorario con los valores editados
            RegistroHorario registroHorario = new RegistroHorario
            {
                idEmpleado = idEmpleado,
                idHorario = idHorario,
                fechaInicio = fechaInicio,
                fechaFin = fechaFin
            };

            // Solicitar al Presenter que actualice los datos del horario
            _presenter.ActualizarRegistroHorarios(registroHorario);
            LimpiarCampos();

            // Actualizar el DataGridView con los nuevos datos
            _presenter.MostrarHorarios(dgvHorariosAdmin);
        }


        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Verificar si hay una fila seleccionada
            if (dgvHorariosAdmin.SelectedRows.Count > 0)
            {
                // Obtener el ID del horario seleccionado en el DataGridView
                int idHorarioEmpleado = Convert.ToInt32(dgvHorariosAdmin.SelectedRows[0].Cells["idHorarioEmpleado"].Value);

                // Llamar al método en el presentador para eliminar el horario
                _presenter.EliminarRegistroHorario(idHorarioEmpleado);

                // Opcionalmente, puedes limpiar los controles después de eliminar el horario
                LimpiarCampos();
            }
            else
            {
                // Mostrar un mensaje indicando que no se ha seleccionado ningún horario
                MessageBox.Show("Por favor, seleccione un horario para eliminar.", "Ningún horario seleccionado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void LimpiarCampos()
        {
            txtIdRegistroHorario.Text = "";
            txtIdEmpleado.Text = "";
            txtIdHorario.Text = "";
            dtpInicio.Value = DateTime.Now;
            dtpFin.Value = DateTime.Now;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
