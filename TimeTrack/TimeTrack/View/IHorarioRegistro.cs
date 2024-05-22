using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimeTrack.View
{
    internal interface IHorarioRegistro
    {
        DataGridView DataGridViewHorarioRegistro { get; }
        void MostrarMensaje(string mensaje, string v, MessageBoxButtons oK, MessageBoxIcon information);    
    }
}
