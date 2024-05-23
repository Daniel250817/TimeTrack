using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace TimeTrack.View
{
    internal class Utilities
    {

        public static void BorderRadius(Panel panel, int radio)
        {
            // Suscribirse al evento SizeChanged del panel
            panel.SizeChanged += (sender, e) => ResizePanel(sender as Panel, radio);

            // Llamar al método de redimensionamiento una vez para aplicar el redondeado inicialmente
            ResizePanel(panel, radio);
        }

        private static void ResizePanel(Panel panel, int radio)
        {
            // Crear un rectángulo con las dimensiones del panel
            GraphicsPath forma = new GraphicsPath();
            forma.AddArc(0, 0, radio * 2, radio * 2, 180, 90);
            forma.AddArc(panel.Width - (radio * 2), 0, radio * 2, radio * 2, 270, 90);
            forma.AddArc(panel.Width - (radio * 2), panel.Height - (radio * 2), radio * 2, radio * 2, 0, 90);
            forma.AddArc(0, panel.Height - (radio * 2), radio * 2, radio * 2, 90, 90);
            forma.CloseFigure();

            // Aplicar la forma al panel
            panel.Region = new Region(forma);
        }

        public static void AjustarOpacidad(Panel panel)
        {
            if (panel != null)
            {
                panel.BackColor = Color.FromArgb(120, panel.BackColor);
            }
            return;
        }
    }

    public static class Validaciones
    {

        public static bool ValidarNombre(string nombre, Action<string> mostrarMensaje)
        {
            if (string.IsNullOrWhiteSpace(nombre) || nombre.Any(char.IsDigit))
            {
                mostrarMensaje("El campo 'Nombre' es inválido. No debe contener números.");
                return false;
            }
            return true;
        }

        public static bool ValidarNoVacio(string valor, Action<string> mostrarMensaje, string nombreCampo)
        {
            if (string.IsNullOrWhiteSpace(valor))
            {
                mostrarMensaje($"El campo '{nombreCampo}' no puede estar vacío.");
                return false;
            }
            return true;
        }

        public static bool ValidarId(string id, Action<string> mostrarMensaje)
        {
            if (string.IsNullOrWhiteSpace(id) || !int.TryParse(id, out int idValor) || idValor <= 0)
            {
                mostrarMensaje("El campo 'ID' es inválido o negativo.");
                return false;
            }
            return true;
        }

        public static bool ValidarFecha(string fecha, Action<string> mostrarMensaje)
        {
            if (string.IsNullOrWhiteSpace(fecha) || !DateTime.TryParse(fecha, out _))
            {
                mostrarMensaje("El campo 'Fecha' es inválido.");
                return false;
            }
            return true;
        }

        public static string ValidarFechas(string fechaInicio, string fechaFin)
        {
            if (!DateTime.TryParse(fechaInicio, out DateTime inicio) || !DateTime.TryParse(fechaFin, out DateTime fin))
            {
                return "Las fechas tienen un formato inválido.";
            }

            if (inicio > fin)
            {
                return "La fecha de inicio no puede ser mayor que la fecha de fin.";
            }
            if (fin < inicio)
            {
                return "La fecha fin no puede ser menor que la fecha de inicio.";
            }

            return null; // Si no hay errores, devuelve null
        }

        public static bool ValidarDecimal(string valor, Action<string> mostrarMensaje, string nombreCampo)
        {
            if (string.IsNullOrWhiteSpace(valor) || !decimal.TryParse(valor, out _))
            {
                mostrarMensaje($"El campo '{nombreCampo}' es inválido.");
                return false;
            }
            return true;
        }

        public static bool ValidarTelefono(string telefono, Action<string> mostrarMensaje)
        {
            // Patrón para validar números de teléfono de 8 dígitos
            string patronTelefono = @"^\d{8}$";

            if (string.IsNullOrWhiteSpace(telefono) || !Regex.IsMatch(telefono, patronTelefono))
            {
                mostrarMensaje("El campo 'Teléfono' es inválido. Debe tener 8 dígitos.");
                return false;
            }
            return true;
        }

        public static bool ValidarHora(string hora, Action<string> mostrarMensaje, string nombreCamp)
        {
            // Patrón para validar una hora en formato HH:mm:ss
            string patronHora = @"^([01][0-9]|2[0-3]):[0-5][0-9]:[0-5][0-9]$";

            if (string.IsNullOrWhiteSpace(hora))
            {
                mostrarMensaje($"El campo {nombreCamp} está vacío.");
                return false;
            }
            else if (!Regex.IsMatch(hora, patronHora))
            {
                mostrarMensaje($"El campo {nombreCamp} no tiene el formato correcto (HH:mm:ss).");
                return false;
            }
            else
            {
                return true;
            }
        }



    }

}
