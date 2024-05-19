using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using TimeTrack.View;
using static TimeTrack.Model.Model;

namespace TimeTrack.Presenter
{
    internal class Presenter : ILoginPresenter
    {
        private readonly ILoginView _vista;
        private readonly ModelManager _modelo;
        private readonly IMainView _mainVista;

        private Dictionary<string, Form> _openForms = new Dictionary<string, Form>();

        public Presenter(ILoginView vista)
        {
            _vista = vista;
            _modelo = new ModelManager();
        }

        public Presenter() 
        {

        }

        public Presenter(IMainView viewMain)
        {
            _mainVista = viewMain;
        }

        public void IniciarSesion()
        {
            try
            {
                string nombreUsuario = _vista.NombreUsuario;
                string contrasena = _vista.Contrasena;

                if (_modelo.VerificarCredenciales(nombreUsuario, contrasena))
                {
                    // Obtener los datos del empleado logueado
                    Empleado empleado = ObtenerDatosEmpleadoLogueado(nombreUsuario);

                    if (empleado != null)
                    {
                        string nombreEmpleado = empleado.nombre;
                        string apellidoEmpleado = empleado.apellido;
                        string cargoEmpleado = empleado.cargo;

                        FormMain formMain = new FormMain(nombreEmpleado,apellidoEmpleado, cargoEmpleado);
                        formMain.Show();
                    }
                    else
                    {
                        _vista.MostrarMensajeError("No se encontraron datos para el empleado logueado.");
                    }
                }
                else
                {
                    _vista.MostrarMensajeError("Nombre de usuario o contraseña incorrectos.");
                }
            }
            catch (Exception ex)
            {
                string mensajeError = $"Se produjo un error al intentar iniciar sesión. Por favor, inténtelo de nuevo más tarde.\n{ex.Message}";
                _vista.MostrarMensajeError(mensajeError);
                // Opcionalmente, también puedes registrar la excepción para su posterior análisis o registro.
            }
        }

        public string ObtenerFechaActual()
        {
            DateTime fechaActual = DateTime.Now;
            string fechaFormateada = fechaActual.ToString("dd/MM/yyyy");
            return fechaFormateada;
        }

        public string ObtenerHoraActual()
        {
            DateTime horaActual = DateTime.Now;
            string horaFormateada = horaActual.ToString("HH:mm:ss");
            return horaFormateada;
        }

        public void ShowOrOpenFormInPanel(Form form, string formName, Panel panel)
        {
            foreach (Control control in panel.Controls)
            {
                if (control is Form existingForm && existingForm.Name != formName)
                {
                    existingForm.Hide();
                }
            }

            if (!_openForms.ContainsKey(formName))
            {
                _openForms[formName] = form;
                ShowFormInPanel(form, panel);
            }
            else
            {
                Form existingForm = _openForms[formName];
                existingForm.Show();
            }
        }



        private void ShowFormInPanel(Form form, Panel panel)
        {
            // Aquí pondrías la lógica para mostrar el formulario en el panel especificado
            // Por ejemplo:
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            panel.Controls.Add(form);
            form.Show();
        }


    }
}
