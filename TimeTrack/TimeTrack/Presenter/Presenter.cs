using System;
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

        public Presenter(ILoginView vista)
        {
            _vista = vista;
            _modelo = new ModelManager();
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


    }
}
