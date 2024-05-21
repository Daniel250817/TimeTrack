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
    public partial class FormNomina : Form, INomina
    {
        private Presenter.Presenter _presenter;

        public DataGridView DataGridViewNomina
        {
            get
            {
                return dgvNominaAdmin;
            }
        }
        public FormNomina()
        {
            InitializeComponent();
            _presenter = new Presenter.Presenter(this);
            Utilities.BorderRadius(panelTop, 10);
            txtIdNomina.Visible = false;
        }


        private void FormNomina_Load_1(object sender, EventArgs e)
        {
            _presenter.MostrarTodasLasNominas(dgvNominaAdmin);

        }

        


        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje);
        }


        /*-----------Eventos------------*/

        private void dgvNomina_SelectionChanged(object sender, EventArgs e)
        {
            // Verificar si hay una fila seleccionada
            if (dgvNominaAdmin.SelectedRows.Count > 0)
            {
                // Obtener la fila seleccionada
                DataGridViewRow filaSeleccionada = dgvNominaAdmin.SelectedRows[0];

                // Obtener los valores de la fila seleccionada y cargarlos en los ComboBoxes
                // Asumiendo que los ComboBoxes se llaman cmbIdEmpleado, cmbFecha, cmbDescuento, cmbSalarioBase, cmbMontoHrsExtra, cmbMontoHrsDescuento y cmbSalarioNeto
                txtIdNomina.Text = filaSeleccionada.Cells["idEmpleado"].Value.ToString();
                txtIdEmpleado.Text = filaSeleccionada.Cells["idEmpleado"].Value.ToString();
                txtFecha.Text = filaSeleccionada.Cells["fecha"].Value.ToString();
                txtDescuento.Text = filaSeleccionada.Cells["descuento"].Value.ToString();
                txtSalarioBase.Text = filaSeleccionada.Cells["salarioBase"].Value.ToString();
                txtMontoHrsExtra.Text = filaSeleccionada.Cells["montoHrsExtra"].Value.ToString();
                txtMontoHrsDescuento.Text = filaSeleccionada.Cells["montoHrsDescuento"].Value.ToString();
                txtSalarioNeto.Text = filaSeleccionada.Cells["salarioNeto"].Value.ToString();
            }
        }

        private void dgvNominaAdmin_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Verificar si la celda actual pertenece a la columna de fecha
            if (dgvNominaAdmin.Columns[e.ColumnIndex].Name == "fecha")
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
            if (!_presenter.ValidarCamposNomina(txtIdEmpleado.Text, txtFecha.Text, txtDescuento.Text, txtSalarioBase.Text, txtMontoHrsExtra.Text, txtMontoHrsDescuento.Text, txtSalarioNeto.Text))
            {
                return;
            }

            Nomina nomina = new Nomina
            {
                idEmpleado = Convert.ToInt32(txtIdEmpleado.Text),
                fecha = DateTime.Parse(txtFecha.Text),
                descuento = Convert.ToDecimal(txtDescuento.Text),
                salarioBase = Convert.ToDecimal(txtSalarioBase.Text),
                montoHrsExtra = Convert.ToDecimal(txtMontoHrsExtra.Text),
                montoHrsDescuento = Convert.ToDecimal(txtMontoHrsDescuento.Text),
                salarioNeto = Convert.ToDecimal(txtSalarioNeto.Text)
            };

            _presenter.InsertarNomina(nomina);
        }


        private void btnActu_Click(object sender, EventArgs e)
        {
            if (!_presenter.ValidarCamposNomina(txtIdEmpleado.Text, txtFecha.Text, txtDescuento.Text, txtSalarioBase.Text, txtMontoHrsExtra.Text, txtMontoHrsDescuento.Text, txtSalarioNeto.Text))
            {
                return;
            }
            // Obtener los valores editados de los TextBoxes
            int idNomina = Convert.ToInt32(txtIdNomina.Text);
            int idEmpleado = Convert.ToInt32(txtIdEmpleado.Text);
            DateTime fecha = Convert.ToDateTime(txtFecha.Text);
            decimal descuento = Convert.ToDecimal(txtDescuento.Text);
            decimal salarioBase = Convert.ToDecimal(txtSalarioBase.Text);
            decimal montoHrsExtra = Convert.ToDecimal(txtMontoHrsExtra.Text);
            decimal montoHrsDescuento = Convert.ToDecimal(txtMontoHrsDescuento.Text);
            decimal salarioNeto = Convert.ToDecimal(txtSalarioNeto.Text);

            // Crear un objeto Nomina con los valores editados
            Nomina nomina = new Nomina
            {
                idNomina = idNomina,
                idEmpleado = idEmpleado,
                fecha = fecha,
                descuento = descuento,
                salarioBase = salarioBase,
                montoHrsExtra = montoHrsExtra,
                montoHrsDescuento = montoHrsDescuento,
                salarioNeto = salarioNeto
            };

            // Solicitar al Presenter que actualice los datos de la nómina
            _presenter.ActualizarNominaDGV(nomina);
            LimpiarCampos();

            // Actualizar el DataGridView con los nuevos datos
            _presenter.MostrarTodasLasNominas(dgvNominaAdmin);
        }

        private void LimpiarCampos()
        {
            // Asumiendo que los Comb
            txtIdNomina.Text = "";
            txtIdEmpleado.Text = "";
             txtFecha.Text = "";
            txtDescuento.Text = "";
            txtSalarioBase.Text = "";
            txtMontoHrsExtra.Text = "";
            txtMontoHrsDescuento.Text = "";
            txtSalarioNeto.Text = "";

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Verificar si hay una fila seleccionada
            if (dgvNominaAdmin.SelectedRows.Count > 0)
            {
                // Obtener el ID de la nómina seleccionada en el DataGridView
                int idNomina = Convert.ToInt32(dgvNominaAdmin.SelectedRows[0].Cells["idNomina"].Value);

                // Llamar al método en el presentador para eliminar la nómina
                _presenter.EliminarNominaDGV(idNomina);

                // Opcionalmente, puedes limpiar los TextBoxes después de eliminar la nómina
                LimpiarCampos();
            }
            else
            {
                // Mostrar un mensaje indicando que no se ha seleccionado ninguna nómina
                MessageBox.Show("Por favor, seleccione una nómina para eliminar.", "Ninguna nómina seleccionada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        public void MostrarMensaje(string mensaje, string titulo, MessageBoxButtons botones, MessageBoxIcon icono)
        {
            MessageBox.Show(this, mensaje, titulo, botones, icono);
        }

    }
}
