using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TimeTrack.Model;
using TimeTrack.Presenter;

namespace TimeTrack.View 
{
    public partial class FormInOut : Form, IMainView, IFormInOut
    {
        private Presenter.Presenter _presenter;
        int Logueado;
        public FormInOut(int idLogueado)
        {
            Logueado = idLogueado;
            InitializeComponent();
            Utilities.BorderRadius(panelHora, 10);
            Utilities.BorderRadius(panelInOut, 10);
            Utilities.BorderRadius(panelHour, 10);
            Utilities.BorderRadius(panelDateHour, 10);
            _presenter = new Presenter.Presenter(this);
            MostrarFechaHoraActual();
            _presenter.SetMainView(this);
            _presenter.StartClock();
        }

        private void FormInOut_Load(object sender, EventArgs e)
        {
            _presenter.HorarioEmpleado(Logueado);
        }

        private void MostrarFechaHoraActual()
        {
            string fechaActual = _presenter.ObtenerFechaActual();
            lblDate.Text = "Fecha actual: " + fechaActual;
        }

        public void ActualizarFechaHora(string horaActual)
        {
            lblHour.Text = "Hora Actual: " + horaActual;
        }

        public void MostrarHorarioEmpleado(Horario horario)
        {
            // Mostrar los valores del horario en las etiquetas correspondientes
            lblLunesViernes.Text = $"Horario: \n{horario.nombreHorario}\n\nLunes a Viernes: Entrada {horario.entradaLunesViernes}\nSalida {horario.salidaLunesViernes}";
            lblSabado.Text = $"Sábado: Entrada {horario.entradaSabado} \nSalida  {horario.salidaSabado}";
        }

    }
}
