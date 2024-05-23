using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TimeTrack.Model;

namespace TimeTrack.View
{
    internal interface IEmpleado
    {
        void MostrarEmpleadoLogueado(Empleado empleado);
        void MostrarMensaje(string mensaje, string v, MessageBoxButtons oK, MessageBoxIcon information);
    }
}
