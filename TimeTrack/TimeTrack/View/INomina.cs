using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static TimeTrack.Model.Model;

namespace TimeTrack.View
{
    public interface INomina
    {
        DataGridView DataGridViewNomina { get; }
        void MostrarMensaje(string mensaje, string v, MessageBoxButtons oK, MessageBoxIcon information);
    }
}
