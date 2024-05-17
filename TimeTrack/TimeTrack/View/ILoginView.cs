using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTrack.View
{
    internal interface ILoginView
    {
        string NombreUsuario { get; }
        string Contrasena { get; }

        void MostrarMensajeError(string mensaje);
    }
}
