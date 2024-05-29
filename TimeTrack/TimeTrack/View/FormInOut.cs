using System;
using System.Windows.Forms;
using TimeTrack.Model;
using TimeTrack.Presenter;

namespace TimeTrack.View
{
    public partial class FormInOut : Form, IMainView, IFormInOut
    {
        private Presenter.Presenter _presenter;
        private DateTime horaEntrada;
        private DateTime horaSalida;
        private Horario horario;
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
            btnOut.Enabled = false;
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

        public void MostrarHorarioEmpleado(Horario horarioEmpleado)
        {
            horario = horarioEmpleado;
            lblLunesViernes.Text = $"Horario: \n{horario.nombreHorario}\n\nLunes a Viernes: \n\nEntrada {horario.entradaLunesViernes}\nSalida {horario.salidaLunesViernes}";
            lblSabado.Text = $"Sábado: \n\nEntrada {horario.entradaSabado}\nSalida {horario.salidaSabado}";
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"¡Has iniciado tu jornada a las {DateTime.Now.ToString("HH:mm:ss")}!", "Jornada comenzada", MessageBoxButtons.OK, MessageBoxIcon.Information);
            horaEntrada = DateTime.Now;
            btnIn.Enabled = false;
            btnOut.Enabled = true;
        }

        private void btnOut_Click(object sender, EventArgs e)
        {
            horaSalida = DateTime.Now;

            // Obtener el día actual
            DayOfWeek diaActual = DateTime.Today.DayOfWeek;

            // Obtener el horario correspondiente según el día
            TimeSpan horaEntradaDiaActual;
            TimeSpan horaSalidaDiaActual;
            if (diaActual >= DayOfWeek.Monday && diaActual <= DayOfWeek.Friday)
            {
                // Es un día de la semana (de lunes a viernes)
                horaEntradaDiaActual = TimeSpan.Parse(horario.entradaLunesViernes);
                horaSalidaDiaActual = TimeSpan.Parse(horario.salidaLunesViernes);
            }
            else if (diaActual == DayOfWeek.Saturday)
            {
                // Es sábado
                horaEntradaDiaActual = TimeSpan.Parse(horario.entradaSabado);
                horaSalidaDiaActual = TimeSpan.Parse(horario.salidaSabado);
            }
            else
            {
                MessageBox.Show("Che maje, ahora domingo no se trabaja");
                return;
            }

            // Calcular la diferencia de tiempo entre la entrada y la salida
            TimeSpan horasTrabajadas = horaSalida - horaEntrada;

            // Calcular horas tardías y extras
            TimeSpan diferenciaEntrada = horaEntrada.TimeOfDay - horaEntradaDiaActual;
            TimeSpan horasExtras = horasTrabajadas - (horaSalidaDiaActual - horaEntradaDiaActual);

            // Asegurarse de que las horas tardías y extras sean siempre positivas
            string horasTardiasStr = diferenciaEntrada.TotalMinutes > 0 ? diferenciaEntrada.ToString(@"hh\:mm\:ss") : "00:00:00";
            string horasExtrasStr = horasExtras.TotalMinutes > 0 ? horasExtras.ToString(@"hh\:mm\:ss") : "00:00:00";

            // Guardar el registro de jornada
            RegistroJornada registroJornada = new RegistroJornada
            {
                IdEmpleado = Logueado,
                Fecha = DateTime.Today,
                HoraEntrada = horaEntrada.ToString("HH:mm:ss"),
                HoraSalida = horaSalida.ToString("HH:mm:ss"),
                HrsTardias = horasTardiasStr,
                HrsExtras = horasExtrasStr
            };

             _presenter.InsertarJornada(registroJornada);


            btnIn.Enabled = true;
            btnOut.Enabled = false;
        }
        public void MostrarMensaje(string mensaje, string titulo, MessageBoxButtons botones, MessageBoxIcon icono)
        {
            MessageBox.Show(this, mensaje, titulo, botones, icono);
        }

    }
}
