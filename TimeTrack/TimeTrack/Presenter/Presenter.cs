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
        private IHorarioRegistro _horarioRegistro;


        public IMainView _mainView { get; set; }

        private Timer _timer;
        private Dictionary<string, Form> _openForms = new Dictionary<string, Form>();

        public Presenter()
        {
            _modeloManager = new ModelManager();
            _model = new Model.Model();
        }

        public Presenter(ILoginView vistaLogin) : this() // Llama al constructor por defecto
        {
            _Loginvista = vistaLogin;
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

        public Presenter(IHorarioRegistro vistaHorario) : this() // Llama al constructor por defecto
        {
            _horarioRegistro = vistaHorario;
        }



        public void SetMainView(IMainView mainView)
        {
            _mainView = mainView;
        }


        /*------Validaciones de campos-------*/



        public bool ValidarCamposNomina(string idEmpleado, string fecha, string descuento, string salarioBase, string montoHrsExtra, string montoHrsDescuento, string salarioNeto)
        {
            bool idEmpleadoValido = Validaciones.ValidarId(idEmpleado, mensaje => _vistaNomina.MostrarMensaje(mensaje, "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error));
            bool fechaValida = Validaciones.ValidarFecha(fecha, mensaje => _vistaNomina.MostrarMensaje(mensaje, "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error));
            bool descuentoValido = Validaciones.ValidarDecimal(descuento, mensaje => _vistaNomina.MostrarMensaje(mensaje, "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error), "Descuento");
            bool salarioBaseValido = Validaciones.ValidarDecimal(salarioBase, mensaje => _vistaNomina.MostrarMensaje(mensaje, "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error), "Salario Base");
            bool montoHrsExtraValido = Validaciones.ValidarDecimal(montoHrsExtra, mensaje => _vistaNomina.MostrarMensaje(mensaje, "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error), "Monto Horas Extra");
            bool montoHrsDescuentoValido = Validaciones.ValidarDecimal(montoHrsDescuento, mensaje => _vistaNomina.MostrarMensaje(mensaje, "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error), "Monto Horas Descuento");
            bool salarioNetoValido = Validaciones.ValidarDecimal(salarioNeto, mensaje => _vistaNomina.MostrarMensaje(mensaje, "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error), "Salario Neto");

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
                    DatosSesion sesion = ObtenerDatosEmpleadoLogueado(nombreUsuario);

                    if (sesion != null)
                    {
                        int idLogueado = sesion.idempleado;
                        string nombreEmpleado = sesion.nombre;
                        string apellidoEmpleado = sesion.apellido;
                        string cargoEmpleado = sesion.cargo;

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
            bool result = _model.ActualizarNomina(nomina); // Llama al método correspondiente en tu modelo
            if (result)
            {
                _vistaNomina.MostrarMensaje("¡La nómina se ha actualizado correctamente!", "Actualización Éxitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                 ActualizarDataGridView();
            }
            else
            {
                _vistaNomina.MostrarMensaje("¡Error al Actualizar la nómina!", "Error al Insertar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
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

        public void MostrarHorarios(DataGridView dataGridView)
        {
            dataGridView.Rows.Clear();
            List<RegistroHorario> horarioRegistro = ObtenerRegistroHorarios();

            if (dataGridView.Columns.Count == 0)
            {
                dataGridView.Columns.Add("idHorarioEmpleado", "ID Horario Empleado");
                dataGridView.Columns.Add("idEmpleado", "ID Empleado");
                dataGridView.Columns.Add("idHorario", "ID Horario");
                dataGridView.Columns.Add("fechaInicio", "Fecha Inicio");
                dataGridView.Columns.Add("fechaFin", "Fecha Fin");
            }

            foreach (var horarios in horarioRegistro)
            {
                dataGridView.Rows.Add(horarios.idHorarioEmpleado, horarios.idEmpleado, horarios.idHorario,  horarios.fechaInicio, horarios.fechaFin);
            }
        }


        public void InsertarRegistroHorario(RegistroHorario horario)
        {
            bool resultado = _model.InsertarHorario(horario);
            if (resultado)
            {
                _horarioRegistro.MostrarMensaje("¡La horario se ha insertado correctamente!", "Inserción del dato éxitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MostrarHorariosEmpleados();
            }
            else
            {
                _vistaNomina.MostrarMensaje("¡Error al insertar el horario!", "Error al Insertar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ActualizarRegistroHorarios(RegistroHorario horario)
        {
            bool result = _model.ActualizarHorario(horario); // Llama al método correspondiente en tu modelo
            if (result)
            {
                _horarioRegistro.MostrarMensaje("¡La nómina se ha actualizado correctamente!", "Actualización Éxitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MostrarHorariosEmpleados();
            }
            else
            {
                _horarioRegistro.MostrarMensaje("¡Error al Actualizar el horario!", "Error al Insertar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public void EliminarRegistroHorario(int idHorarioEmpleado)
        {
            EliminarHorario(idHorarioEmpleado);
            _horarioRegistro.MostrarMensaje("¡El horario se ha eliminado correctamente!", "Eliminación Éxitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
            MostrarHorariosEmpleados();
        }

        public bool ValidarCamposHorario(string idEmpleado, string idHorario, string fechaInicio, string fechaFin)
        {
            string mensajeError = Validaciones.ValidarFechas(fechaInicio, fechaFin);
            if (mensajeError != null)
            {
                _horarioRegistro.MostrarMensaje(mensajeError, "Error de fechas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            bool idEmpleadoValido = Validaciones.ValidarId(idEmpleado, mensaje => _horarioRegistro.MostrarMensaje(mensaje, "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error));
            bool idHorarioValido = Validaciones.ValidarId(idHorario, mensaje => _horarioRegistro.MostrarMensaje(mensaje, "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error));

            return idEmpleadoValido && idHorarioValido && idHorarioValido;
        }


        private void MostrarHorariosEmpleados()
        {
            MostrarHorarios(_horarioRegistro.DataGridViewHorarioRegistro);
        }


    }
}
