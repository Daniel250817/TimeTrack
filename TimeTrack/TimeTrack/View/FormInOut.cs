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
    public partial class FormInOut : Form
    {
        public FormInOut()
        {
            InitializeComponent();
            Utilities.BorderRadius(panelHora, 10);
            Utilities.BorderRadius(panelInOut, 10);
        }
    }
}
