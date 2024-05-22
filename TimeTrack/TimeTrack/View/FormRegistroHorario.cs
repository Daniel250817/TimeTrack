using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TimeTrack.Presenter;

namespace TimeTrack.View
{
    public partial class FormRegistroHorario : Form, IHorarioRegistro
    {
        Presenter.Presenter _presenter;
        public DataGridView DataGridViewHorarioRegistro
        {
            get
            {
                return dgvHorariosAdmin;
            }
        }
        public FormRegistroHorario()
        {
            InitializeComponent();
            _presenter = new Presenter.Presenter(this);
            
        }

        private void FormRegistroHorario_Load(object sender, EventArgs e)
        {
            _presenter.MostrarHorarios(dgvHorariosAdmin);
        }

        public void MostrarMensaje(string mensaje, string titulo, MessageBoxButtons botones, MessageBoxIcon icono)
        {
            MessageBox.Show(this, mensaje, titulo, botones, icono);
        }
    }
}
