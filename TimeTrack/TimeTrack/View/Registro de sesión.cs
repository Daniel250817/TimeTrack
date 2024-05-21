using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TimeTrack.View;
using static TimeTrack.Model.Model;

namespace TimeTrack
{
    public partial class FormLogin : Form, ILoginView
    {
        private ModelManager modelManager;
        private Presenter.Presenter presenter;

        private bool mostrarPassword = false;

        public FormLogin()
        {
            InitializeComponent();
            modelManager = new ModelManager();
            presenter = new Presenter.Presenter(this);

            txtPass.Text = "12345";
            txtUser.Text = "JulioAdmin";
        }

        public void MostrarMensajeError(string mensaje)
        {
            MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public string NombreUsuario => txtUser.Text;
        public string Contrasena => txtPass.Text;

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string nombreUsuario = txtUser.Text;
            string contrasena = txtPass.Text;

            // Verificar las credenciales ingresadas
            if (modelManager.VerificarCredenciales(nombreUsuario, contrasena))
            {
                presenter.IniciarSesion();
                this.Hide(); // Cierra el formulario de inicio de sesión y elimina la instancia de la memoria
            }
            else
            {
                MessageBox.Show("Nombre de usuario o contraseña incorrectos");
            }
        }

        private void lblPassEye_Click(object sender, EventArgs e)
        {
            if (!mostrarPassword)
            {
                txtPass.UseSystemPasswordChar = false;
                txtPass.Text = txtPass.Text; 
                mostrarPassword = true;
            }
            else
            {
                txtPass.UseSystemPasswordChar = true;
                txtPass.Text = txtPass.Text;
                mostrarPassword = false;
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }
    }
}
