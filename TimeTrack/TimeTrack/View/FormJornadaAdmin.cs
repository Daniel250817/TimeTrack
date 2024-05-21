using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimeTrack.View
{
    public partial class FormJornadaAdmin : Form
    {
        private Presenter.Presenter _presenter;

        public FormJornadaAdmin()
        {
            InitializeComponent();
            Utilities.BorderRadius(panelTop1, 10);
            Utilities.BorderRadius(PanelTop2, 10);
        }

        private void FormEmpleado_Load(object sender, EventArgs e)
        {

        }
    }
}
