using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TimeTrack.Presenter;

namespace TimeTrack.View 
{
    public partial class FormInOut : Form
    {
        private Presenter.Presenter _presenter;
        public FormInOut()
        {
            InitializeComponent();
            Utilities.BorderRadius(panelHora, 10);
            Utilities.BorderRadius(panelInOut, 10);
            Utilities.BorderRadius(panelHour, 10);
            Utilities.BorderRadius(panelDateHour, 10);
            _presenter = new Presenter.Presenter();
            MostrarFechaHoraActual();

        }

        private void FormInOut_Load(object sender, EventArgs e)
        {

        }

        private void MostrarFechaHoraActual()
        {
            string fechaActual = _presenter.ObtenerFechaActual();
            lblDate.Text = "Fecha actual: " + fechaActual;

            string horaActual = _presenter.ObtenerHoraActual();
            lblHour.Text = "Hora actual: " + horaActual;
        }

    }
}
