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
    public partial class FormNominaEmpleado : Form, INomina
    {
        Presenter.Presenter _presenter;
        Nomina nomina;

        public DataGridView DataGridViewNomina
        {
            get
            {
                return dgvNominaEmployee;
            }
        }
        int empleado;
        public FormNominaEmpleado(int idempleado)
        {
            empleado = idempleado;
            InitializeComponent();
            ActualizarDataGridViewNomina(nomina);
            _presenter = new Presenter.Presenter(this);

        }

        private void FormNominaEmpleado_Load(object sender, EventArgs e)
        {
            Utilities.BorderRadius(PanelTop, 10);
            _presenter.MostrarNominasEmpleadoLogueado(dgvNominaEmployee, empleado);
        }

        public void ActualizarDataGridViewNomina(Nomina nomina)
        {
            dgvNominaEmployee.Rows.Clear();

            if (dgvNominaEmployee.Columns.Count == 0)
            {
                dgvNominaEmployee.Columns.Add("idNomina", "ID Nomina");
                dgvNominaEmployee.Columns.Add("idEmpleado", "ID Empleado");
                dgvNominaEmployee.Columns.Add("fecha", "Fecha");
                dgvNominaEmployee.Columns.Add("salarioBase", "Salario Base");
                dgvNominaEmployee.Columns.Add("descuento", "Descuento");
                dgvNominaEmployee.Columns.Add("montoHrsExtra", "Monto Horas Extra");
                dgvNominaEmployee.Columns.Add("montoHrsDescuento", "Monto Horas Descuento");
                dgvNominaEmployee.Columns.Add("salarioNeto", "Salario Neto");
            }
            if (nomina != null)
            {
                dgvNominaEmployee.Rows.Add(nomina.idNomina, nomina.idEmpleado,
                    nomina.fecha, nomina.salarioBase, nomina.descuento, nomina.montoHrsExtra, 
                    nomina.montoHrsDescuento, nomina.salarioNeto);
            }
            
        }

        private void dgvNominaAdmin_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvNominaAdmin_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Verificar si la celda actual pertenece a la columna de fecha
            if (dgvNominaEmployee.Columns[e.ColumnIndex].Name == "fecha")
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


        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje);
        }

        public void MostrarMensaje(string mensaje, string titulo, MessageBoxButtons botones, MessageBoxIcon icono)
        {
            MessageBox.Show(this, mensaje, titulo, botones, icono);
        }

    }
}
