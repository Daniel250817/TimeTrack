using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimeTrack.View
{
    internal interface InterfaceCrud
    {
        DataGridView DataGridViewCRUD { get; }
        void MostrarMensaje(string mensaje, string v, MessageBoxButtons oK, MessageBoxIcon information);    
    }
}
