using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
    }

    public static class Validaciones
    {
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
    }

}
