using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using TimeTrack.Model;
using TimeTrack.View;
using static TimeTrack.Model.Model;
using Timer = System.Windows.Forms.Timer;

namespace TimeTrack.Presenter
{
    internal class Presenter
    {
        private Model.Model _model;
        private readonly ModelManager _modeloManager;
        private readonly ILoginView _Loginvista;
        private INomina _vistaNomina;
        private IFormInOut _view;


        public IMainView _mainView { get; set; }

        private Timer _timer;
        private Dictionary<string, Form> _openForms = new Dictionary<string, Form>();

        public Presenter()
        {
            _modeloManager = new ModelManager();
            _model = new Model.Model();
        }

        public Presenter(ILoginView vista) : this() // Llama al constructor por defecto
        {
            _Loginvista = vista;
        }

        public Presenter(INomina vista) : this() // Llama al constructor por defecto
        {
            _vistaNomina = vista;
        }


        public Presenter(IFormInOut view)
        {
            _view = view;
            _model = new Model.Model();
        }

        public void SetMainView(IMainView mainView)
        {
            _mainView = mainView;
        }


        /*------Validaciones de campos-------*/

        public bool ValidarIdEmpleado(string idEmpleado)
        {
            if (string.IsNullOrWhiteSpace(idEmpleado) || !int.TryParse(idEmpleado, out int id) || id < 0)
            {
                _vistaNomina.MostrarMensaje("El campo 'ID Empleado' es inválido o negativo.", "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        public bool ValidarFecha(string fecha)
        {
            if (string.IsNullOrWhiteSpace(fecha) || !DateTime.TryParse(fecha, out _))
            {
                _vistaNomina.MostrarMensaje("El campo 'Fecha' es inválido.", "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        public bool ValidarDescuento(string descuento)
        {
            if (string.IsNullOrWhiteSpace(descuento) || !decimal.TryParse(descuento, out _))
            {
                _vistaNomina.MostrarMensaje("El campo 'Descuento' es inválido.", "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        public bool ValidarSalarioBase(string salarioBase)
        {
            if (string.IsNullOrWhiteSpace(salarioBase) || !decimal.TryParse(salarioBase, out _))
            {
                _vistaNomina.MostrarMensaje("El campo 'Salario Base' es inválido.", "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        public bool ValidarMontoHorasExtra(string montoHrsExtra)
        {
            if (string.IsNullOrWhiteSpace(montoHrsExtra) || !decimal.TryParse(montoHrsExtra, out _))
            {
                _vistaNomina.MostrarMensaje("El campo 'Monto Horas Extra' es inválido.", "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        public bool ValidarMontoHorasDescuento(string montoHrsDescuento)
        {
            if (string.IsNullOrWhiteSpace(montoHrsDescuento) || !decimal.TryParse(montoHrsDescuento, out _))
            {
                _vistaNomina.MostrarMensaje("El campo 'Monto Horas Descuento' es inválido.", "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        public bool ValidarSalarioNeto(string salarioNeto)
        {
            if (string.IsNullOrWhiteSpace(salarioNeto) || !decimal.TryParse(salarioNeto, out _))
            {
                _vistaNomina.MostrarMensaje("El campo 'Salario Neto' es inválido.", "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        public bool ValidarCamposNomina(string idEmpleado, string fecha, string descuento, string salarioBase, string montoHrsExtra, string montoHrsDescuento, string salarioNeto)
        {
            bool idEmpleadoValido = ValidarIdEmpleado(idEmpleado);
            bool fechaValida = ValidarFecha(fecha);
            bool descuentoValido = ValidarDescuento(descuento);
            bool salarioBaseValido = ValidarSalarioBase(salarioBase);
            bool montoHrsExtraValido = ValidarMontoHorasExtra(montoHrsExtra);
            bool montoHrsDescuentoValido = ValidarMontoHorasDescuento(montoHrsDescuento);
            bool salarioNetoValido = ValidarSalarioNeto(salarioNeto);

            return idEmpleadoValido && fechaValida && descuentoValido && salarioBaseValido && montoHrsExtraValido && montoHrsDescuentoValido && salarioNetoValido;
        }

        public void IniciarSesion()
        {
            try
            {
                string nombreUsuario = _Loginvista.NombreUsuario;
                string contrasena = _Loginvista.Contrasena;

                if (_modeloManager.VerificarCredenciales(nombreUsuario, contrasena))
                {
                    Empleado empleado = ObtenerDatosEmpleadoLogueado(nombreUsuario);

                    if (empleado != null)
                    {
                        int idLogueado = empleado.idempleado;
                        string nombreEmpleado = empleado.nombre;
                        string apellidoEmpleado = empleado.apellido;
                        string cargoEmpleado = empleado.cargo;

                        FormMain formMain = new FormMain(idLogueado, nombreEmpleado, apellidoEmpleado, cargoEmpleado);

                        formMain.Show();
                        
                    }
                    else
                    {
                        _Loginvista.MostrarMensajeError("No se encontraron datos para el empleado logueado.");
                    }
                }
                else
                {
                    _Loginvista.MostrarMensajeError("Nombre de usuario o contraseña incorrectos.");
                }
            }
            catch (Exception ex)
            {
                string mensajeError = $"Se produjo un error al intentar iniciar sesión. Por favor, inténtelo de nuevo más tarde.\n{ex.Message}";
                _Loginvista.MostrarMensajeError(mensajeError);
            }
        }

        public string ObtenerFechaActual()
        {
            DateTime fechaActual = DateTime.Now;
            return fechaActual.ToString("dd/MM/yyyy");
        }

        public void StartClock()
        {
            _timer = new Timer();
            _timer.Interval = 1000;
            _timer.Tick += UpdateTime;
            _timer.Start();
        }

        private void UpdateTime(object sender, EventArgs e)
        {
            string fechaHoraActual = DateTime.Now.ToString("hh:mm tt");
            _mainView.ActualizarFechaHora(fechaHoraActual);
        }

        public void ShowOrOpenFormInPanel(Form form, string formName, Panel panel)
        {
            foreach (Control control in panel.Controls)
            {
                if (control is Form existingForm && existingForm.Name != formName)
                {
                    existingForm.Hide();
                }
            }

            if (!_openForms.ContainsKey(formName))
            {
                _openForms[formName] = form;
                ShowFormInPanel(form, panel);
            }
            else
            {
                Form existingForm = _openForms[formName];
                existingForm.Show();
            }
        }

        private void ShowFormInPanel(Form form, Panel panel)
        {
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            panel.Controls.Add(form);
            form.Show();
        }

        public void MostrarNominasEmpleadoLogueado(DataGridView dataGridView, int idEmpleado)
        {
            // Limpiar el DataGridView
            dataGridView.Rows.Clear();

            // Obtener las nominas del empleado logueado
            List<Nomina> nominas = _model.ObtenerNominasEmpleadoLogueado(idEmpleado);

            // Si el DataGridView no tiene columnas, agregarlas
            if (dataGridView.Columns.Count == 0)
            {
                dataGridView.Columns.Add("idNomina", "ID Nomina");
                dataGridView.Columns.Add("idEmpleado", "ID Empleado");
                dataGridView.Columns.Add("fecha", "Fecha");
                dataGridView.Columns.Add("descuento", "Descuento");
                dataGridView.Columns.Add("salarioBase", "Salario Base");
                dataGridView.Columns.Add("montoHrsExtra", "Monto Hrs Extra");
                dataGridView.Columns.Add("montoHrsDescuento", "Monto Hrs Descuento");
                dataGridView.Columns.Add("salarioNeto", "Salario Neto");
            }

            // Agregar las nominas del empleado logueado al DataGridView
            foreach (var nomina in nominas)
            {
                dataGridView.Rows.Add(nomina.idNomina, nomina.idEmpleado, nomina.fecha, nomina.descuento, nomina.salarioBase, nomina.montoHrsExtra, nomina.montoHrsDescuento, nomina.salarioNeto);
            }
        }

        public void MostrarTodasLasNominas(DataGridView dataGridView)
        {
            dataGridView.Rows.Clear();
            List<Nomina> nominas = ObtenerTodasLasNominas();

            if (dataGridView.Columns.Count == 0)
            {
                dataGridView.Columns.Add("idNomina", "ID Nomina");
                dataGridView.Columns.Add("idEmpleado", "ID Empleado");
                dataGridView.Columns.Add("fecha", "Fecha");
                dataGridView.Columns.Add("descuento", "Descuento");
                dataGridView.Columns.Add("salarioBase", "Salario Base");
                dataGridView.Columns.Add("montoHrsExtra", "Monto Hrs Extra");
                dataGridView.Columns.Add("montoHrsDescuento", "Monto Hrs Descuento");
                dataGridView.Columns.Add("salarioNeto", "Salario Neto");
            }

            foreach (var nomina in nominas)
            {
                dataGridView.Rows.Add(nomina.idNomina, nomina.idEmpleado, nomina.fecha, nomina.descuento, nomina.salarioBase, nomina.montoHrsExtra, nomina.montoHrsDescuento, nomina.salarioNeto);
            }
        }

        public void InsertarNomina(Nomina nomina)
        {
            bool resultado = _model.InsertarNomina(nomina);
            if (resultado)
            {
                _vistaNomina.MostrarMensaje("¡La nómina se ha insertado correctamente!","Inserción del dato éxitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ActualizarDataGridView();
            }
            else
            {
                _vistaNomina.MostrarMensaje("¡Error al insertar la nómina!", "Error al Insertar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ActualizarNominaDGV(Nomina nomina)
        {
            ActualizarNomina(nomina); // Llama al método correspondiente en tu modelo
            _vistaNomina.MostrarMensaje("¡La nómina se ha actualizado correctamente!", "Actualización Éxitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ActualizarDataGridView();
        }

        public void EliminarNominaDGV(int idNomina)
        {
            EliminarNomina(idNomina);
            _vistaNomina.MostrarMensaje("¡La nómina se ha eliminado correctamente!", "Eliminación Éxitosa",MessageBoxButtons.OK, MessageBoxIcon.Information);
            ActualizarDataGridView();

        }


        private void ActualizarDataGridView()
        {
            MostrarTodasLasNominas(_vistaNomina.DataGridViewNomina);
        }

        public void HorarioEmpleado(int idEmpleado)
        {
            Horario horario = _model.ObtenerHorarioEmpleado(idEmpleado);

            if (horario != null)
            {
                _view.MostrarHorarioEmpleado(horario);
            }
            else
            {
                // Manejar caso en que no se encuentra el horario
                MessageBox.Show("No se encontró el horario del empleado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
