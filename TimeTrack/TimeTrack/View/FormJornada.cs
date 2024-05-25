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
    public partial class FormJornada : Form, INomina
    {
        Presenter.Presenter _presenter;

        public DataGridView DataGridView
        {
            get
            {
                return dgvJornada;
            }
        }
        int empleado;
        public FormJornada(int idempleado)
        {
            empleado = idempleado;
            InitializeComponent();
            _presenter = new Presenter.Presenter(this);
        }

        private void FormJornada_Load(object sender, EventArgs e)
        {
            Utilities.BorderRadius(PanelTop, 10);
            _presenter.MostrarJornadasEmpleadoLogueado(dgvJornada, empleado);
        }

        private void dgvJornada_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Verificar si la celda actual pertenece a la columna de fecha
            if (dgvJornada.Columns[e.ColumnIndex].Name == "fecha")
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

        public void MostrarMensaje(string mensaje, string titulo, MessageBoxButtons botones, MessageBoxIcon icono)
        {
            MessageBox.Show(this, mensaje, titulo, botones, icono);
        }
    }
}
