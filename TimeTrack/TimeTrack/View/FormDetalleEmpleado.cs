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
    public partial class FormDetalleEmpleado : Form
    {
        public FormDetalleEmpleado()
        {
            InitializeComponent();
            Utilities.BorderRadius(panelContainer, 10);
            panelContainer.AutoScroll = true;
            panelContainer.VerticalScroll.Visible = true;
            panelContainer.HorizontalScroll.Visible = true;
            panelContainer.SetAutoScrollMargin(0, 100);
        }

        private void FormDetalleEmpleado_Load(object sender, EventArgs e)
        {

        }

    }
}
