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
    public partial class FormJornadaAdmin : Form, InterfaceCrud
    {
        private Presenter.Presenter _presenter;
        public DataGridView DataGridViewCRUD
        {
            get
            {
                return dgvJornada;
            }
        }

        public FormJornadaAdmin()
        {
            InitializeComponent();
            Utilities.BorderRadius(panelTop1, 10);
            Utilities.BorderRadius(PanelTop2, 10);
            _presenter = new Presenter.Presenter(this);
        }

        private void FormEmpleado_Load(object sender, EventArgs e)
        {
            _presenter.MostrarRegistrosJornadas(dgvJornada);
        }


        private void dgvNominaAdmin_SelectionChanged(object sender, EventArgs e)
        {
            // Verificar si hay una fila seleccionada
            if (dgvJornada.SelectedRows.Count > 0)
            {
                // Obtener la fila seleccionada
                DataGridViewRow filaSeleccionada = dgvJornada.SelectedRows[0];

                // Verificar si la fila seleccionada no está vacía
                if (!filaSeleccionada.IsNewRow && filaSeleccionada.Cells.Cast<DataGridViewCell>().All(cell => cell.Value != null))
                {
                    txtIdRegistroHora.Text = filaSeleccionada.Cells["IdRegistroHora"].Value.ToString();
                    txtIdEmpleado.Text = filaSeleccionada.Cells["IdEmpleado"].Value.ToString();
                    dtpFecha.Value = Convert.ToDateTime(filaSeleccionada.Cells["Fecha"].Value);
                    txtHoraEntrada.Text = filaSeleccionada.Cells["HoraEntrada"].Value.ToString();
                    txtHoraSalida.Text = filaSeleccionada.Cells["HoraSalida"].Value.ToString();
                    txtHorasTardias.Text = filaSeleccionada.Cells["HorasTardias"].Value.ToString();
                    txtHorasExtras.Text = filaSeleccionada.Cells["HorasExtras"].Value.ToString();
                }
                else
                {
                    LimpiarCampos();
                }
            }
        }


        private void dgvNominaAdmin_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvJornada.Columns[e.ColumnIndex].Name == "Fecha")
            {
                if (e.Value != null && e.Value.GetType() == typeof(DateTime))
                {
                    e.Value = ((DateTime)e.Value).ToShortDateString();
                    e.FormattingApplied = true;
                }
            }
        }

        public void MostrarMensaje(string mensaje, string titulo, MessageBoxButtons botones, MessageBoxIcon icono)
        {
            MessageBox.Show(this, mensaje, titulo, botones, icono);
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            if (!_presenter.ValidarCamposRegistroJornada(txtIdEmpleado.Text, dtpFecha.Value.ToString("yyyy-MM-dd"), txtHoraEntrada.Text, txtHoraSalida.Text, txtHorasTardias.Text, txtHorasExtras.Text))
            {
                return;
            }

            RegistroJornada registroJornada = new RegistroJornada
            {
                IdEmpleado = Convert.ToInt32(txtIdEmpleado.Text),
                Fecha = dtpFecha.Value,
                HoraEntrada = txtHoraEntrada.Text,
                HoraSalida = txtHoraSalida.Text,
                HrsTardias = txtHorasTardias.Text,
                HrsExtras = txtHorasExtras.Text
            };

            _presenter.InsertarRegistroJornada(registroJornada);
        }


        private void btnActu_Click(object sender, EventArgs e)
        {
            if (!_presenter.ValidarCamposRegistroJornada(txtIdEmpleado.Text, dtpFecha.Value.ToString("yyyy-MM-dd"), txtHoraEntrada.Text, txtHoraSalida.Text, txtHorasTardias.Text, txtHorasExtras.Text))
            {
                return;
            }
            int idRegistroHora = Convert.ToInt32(txtIdRegistroHora.Text);
            int idEmpleado = Convert.ToInt32(txtIdEmpleado.Text);
            DateTime fecha = dtpFecha.Value;
            string horaEntrada = txtHoraEntrada.Text;
            string horaSalida = txtHoraSalida.Text;
            string horasTardias = txtHorasTardias.Text;
            string horasExtras = txtHorasExtras.Text;

            RegistroJornada registroJornada = new RegistroJornada
            {
                IdRegistroHora = idRegistroHora,
                IdEmpleado = idEmpleado,
                Fecha = fecha,
                HoraEntrada = horaEntrada,
                HoraSalida = horaSalida,
                HrsTardias = horasTardias,
                HrsExtras = horasExtras
            };
            _presenter.ActualizarRegistroJornada(registroJornada);
            LimpiarCampos(); 
            _presenter.MostrarRegistrosJornadas(dgvJornada); 
        }


        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvJornada.SelectedRows.Count > 0)
            {
                int idRegistroHora = Convert.ToInt32(dgvJornada.SelectedRows[0].Cells["IdRegistroHora"].Value);
                _presenter.EliminarRegistroJornada(idRegistroHora);

                LimpiarCampos();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un registro de jornada para eliminar.", "Ningún registro seleccionado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LimpiarCampos()
        {
            txtIdRegistroHora.Text = "";
            txtIdEmpleado.Text = "";
            dtpFecha.Value = DateTime.Now;
            txtHoraEntrada.Text = "";
            txtHoraSalida.Text = "";
            txtHorasTardias.Text = "";
            txtHorasExtras.Text = "";
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {

        }

    }
}
