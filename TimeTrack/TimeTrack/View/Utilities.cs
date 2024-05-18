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
}
