using ClosedXML.Excel;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
        private InterfaceCrud _InterCrud;
        private IEmpleado _vistaEmple;


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

        public Presenter(InterfaceCrud vistaHorario) : this() // Llama al constructor por defecto
        {
            _InterCrud = vistaHorario;
        }

        public Presenter(IEmpleado viewEmpleado) : this() // Llama al constructor por defecto
        {
            _vistaEmple = viewEmpleado;
        }


        public void SetEmpleadoView(IEmpleado view)
        {
            _vistaEmple = view;
            _model = new Model.Model();
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
            MostrarTodasLasNominas(_vistaNomina.DataGridView);
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
                _InterCrud.MostrarMensaje("¡La horario se ha insertado correctamente!", "Inserción del dato éxitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MostrarHorariosEmpleados();
            }
            else
            {
                _InterCrud.MostrarMensaje("¡Error al insertar el horario!", "Error al Insertar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ActualizarRegistroHorarios(RegistroHorario horario)
        {
            bool result = _model.ActualizarHorario(horario); // Llama al método correspondiente en tu modelo
            if (result)
            {
                _InterCrud.MostrarMensaje("¡La nómina se ha actualizado correctamente!", "Actualización Éxitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MostrarHorariosEmpleados();
            }
            else
            {
                _InterCrud.MostrarMensaje("¡Error al Actualizar el horario!", "Error al Insertar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public void EliminarRegistroHorario(int idHorarioEmpleado)
        {
            EliminarHorario(idHorarioEmpleado);
            _InterCrud.MostrarMensaje("¡El horario se ha eliminado correctamente!", "Eliminación Éxitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
            MostrarHorariosEmpleados();
        }

        public bool ValidarCamposHorario(string idEmpleado, string idHorario, string fechaInicio, string fechaFin)
        {
            string mensajeError = Validaciones.ValidarFechas(fechaInicio, fechaFin);
            if (mensajeError != null)
            {
                _InterCrud.MostrarMensaje(mensajeError, "Error de fechas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            bool idEmpleadoValido = Validaciones.ValidarId(idEmpleado, mensaje => _InterCrud.MostrarMensaje(mensaje, "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error));
            bool idHorarioValido = Validaciones.ValidarId(idHorario, mensaje => _InterCrud.MostrarMensaje(mensaje, "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error));

            return idEmpleadoValido && idHorarioValido && idHorarioValido;
        }


        private void MostrarHorariosEmpleados()
        {
            MostrarHorarios(_InterCrud.DataGridViewCRUD);
        }

        public void MostrarJornadasEmpleadoLogueado(DataGridView dataGridView, int idEmpleado)
        {
            // Limpiar el DataGridView
            dataGridView.Rows.Clear();

            // Obtener las jornadas del empleado logueado
            List<RegistroJornada> jornadas = _model.ObtenerJornadaEmpleado(idEmpleado);

            // Si el DataGridView no tiene columnas, agregarlas
            if (dataGridView.Columns.Count == 0)
            {
                dataGridView.Columns.Add("idRegistroHora", "ID Registro Hora");
                dataGridView.Columns.Add("idEmpleado", "ID Empleado");
                dataGridView.Columns.Add("fecha", "Fecha");
                dataGridView.Columns.Add("horaEntrada", "Hora Entrada");
                dataGridView.Columns.Add("horaSalida", "Hora Salida");
                dataGridView.Columns.Add("hrsTardias", "Horas Tardías");
                dataGridView.Columns.Add("hrsExtras", "Horas Extras");
            }

            // Agregar las jornadas del empleado logueado al DataGridView
            foreach (var jornada in jornadas)
            {
                dataGridView.Rows.Add(jornada.IdRegistroHora, jornada.IdEmpleado, jornada.Fecha, jornada.HoraEntrada, jornada.HoraSalida, jornada.HrsTardias, jornada.HrsExtras);
            }
        }


        public void DatosEmpleadoLogueado(int idEmpleado)
        {
            Empleado empleado = ObtenerDatosEmpleadoLogueado(idEmpleado);

            if (empleado != null)
            {
                _vistaEmple.MostrarEmpleadoLogueado(empleado);
            }
            else
            {
                // Manejar caso en que no se encuentra el horario
                MessageBox.Show("No se encontró el empleado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ActualizarEmpleadoLogueado(Empleado empleado)
        {
            bool result = _model.ActualizarEmpleado(empleado); // Llama al método correspondiente en tu modelo
            if (result)
            {
                _vistaEmple.MostrarMensaje("¡El empleado se ha actualizado correctamente!", "Actualización exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                _vistaEmple.MostrarMensaje("¡Error al actualizar el empleado!", "Error al actualizar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void MostrarEmpleados(DataGridView dataGridView)
        {
            dataGridView.Rows.Clear();
            List<Empleado> empleados = ObtenerEmpleados(); // Utiliza el método correspondiente del modelo

            if (dataGridView.Columns.Count == 0)
            {
                dataGridView.Columns.Add("idEmpleado", "ID Empleado");
                dataGridView.Columns.Add("nombres", "Nombres");
                dataGridView.Columns.Add("apellidos", "Apellidos");
                dataGridView.Columns.Add("fechaNacimiento", "Fecha de Nacimiento");
                dataGridView.Columns.Add("direccion", "Dirección");
                dataGridView.Columns.Add("idCargo", "ID Cargo");
                dataGridView.Columns.Add("telefono", "Teléfono");
            }

            foreach (var empleado in empleados)
            {
                dataGridView.Rows.Add(empleado.IdEmpleado, empleado.Nombres, empleado.Apellidos, empleado.FechaNacimiento, empleado.Direccion, empleado.IdCargo, empleado.Telefono);
            }
        }

        public void InsertarEmpleado(Empleado empleado)
        {
            bool resultado = _model.InsertarEmpleado(empleado);
            if (resultado)
            {
                _InterCrud.MostrarMensaje("¡El empleado se ha insertado correctamente!", "Inserción exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MostrarEmpleados(_InterCrud.DataGridViewCRUD); // Mostrar los empleados actualizados en la vista
            }
            else
            {
                _InterCrud.MostrarMensaje("¡Error al insertar el empleado!", "Error al insertar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void ActualizarEmpleado(Empleado empleado)
        {
            bool result = _model.ActualizarEmpleado(empleado); // Llama al método correspondiente en tu modelo
            if (result)
            {
                _InterCrud.MostrarMensaje("¡El empleado se ha actualizado correctamente!", "Actualización exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MostrarEmpleados(_InterCrud.DataGridViewCRUD); // Mostrar los empleados actualizados en la vista
            }
            else
            {
                _InterCrud.MostrarMensaje("¡Error al actualizar el empleado!", "Error al actualizar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void EliminarEmpleado(int idEmpleado)
        {
            bool result = _model.EliminarEmpleado(idEmpleado); // Llama al método correspondiente en tu modelo
            if (result)
            {
                _InterCrud.MostrarMensaje("¡El empleado se ha eliminado correctamente!", "Eliminación exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MostrarEmpleados(_InterCrud.DataGridViewCRUD); // Mostrar los empleados actualizados en la vista
            }
            else
            {
                _InterCrud.MostrarMensaje("¡Error al eliminar el empleado!", "Error al eliminar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool ValidarCamposEmpleado(string nombres, string apellidos, string fechaNacimiento, string direccion, string idCargo, string telefono)
        {
            bool nombresValidos = Validaciones.ValidarNombre(nombres, mensaje => _InterCrud.MostrarMensaje(mensaje, "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error), "Nombres");
            bool apellidosValidos = Validaciones.ValidarNombre(apellidos, mensaje => _InterCrud.MostrarMensaje(mensaje, "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error), "Apellidos");
            bool fechaValida = Validaciones.ValidarFecha(fechaNacimiento, mensaje => _InterCrud.MostrarMensaje(mensaje, "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error));
            bool direccionValida = Validaciones.ValidarNoVacio(direccion, mensaje => _InterCrud.MostrarMensaje(mensaje, "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error), "Dirección");
            bool idCargoValido = Validaciones.ValidarId(idCargo, mensaje => _InterCrud.MostrarMensaje(mensaje, "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error));
            bool telefonoValido = Validaciones.ValidarTelefono(telefono, mensaje => _InterCrud.MostrarMensaje(mensaje, "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error));

            return nombresValidos && apellidosValidos && fechaValida && direccionValida && idCargoValido && telefonoValido;
        }
        public bool ValidarTelefonoEmpleadoLogueado(string telefono)
        {
            bool telefonoValido = Validaciones.ValidarTelefono(telefono, mensaje => _vistaEmple.MostrarMensaje(mensaje, "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error));
            return telefonoValido;
        }

        public void MostrarRegistrosHorarios(DataGridView dataGridView)
        {
            dataGridView.Rows.Clear();
            List<Horario> horarios = _model.ObtenerHorarios(); // Utiliza el método correspondiente del modelo

            if (dataGridView.Columns.Count == 0)
            {
                dataGridView.Columns.Add("idHorario", "ID Horario");
                dataGridView.Columns.Add("nombreHorario", "Nombre");
                dataGridView.Columns.Add("entradaLunesViernes", "Entrada Lunes a Viernes");
                dataGridView.Columns.Add("salidaLunesViernes", "Salida Lunes a Viernes");
                dataGridView.Columns.Add("entradaSabado", "Entrada Sábado");
                dataGridView.Columns.Add("salidaSabado", "Salida Sábado");
            }

            foreach (var horario in horarios)
            {
                dataGridView.Rows.Add(horario.idHorario, horario.nombreHorario, horario.entradaLunesViernes, horario.salidaLunesViernes, horario.entradaSabado, horario.salidaSabado);
            }
        }

        public void InsertarRegistrosHorario(Horario horario)
        {
            bool resultado = _model.InsertarRegistroHorarios(horario); // Utiliza el método correspondiente del modelo
            if (resultado)
            {
                _InterCrud.MostrarMensaje("¡El horario se ha insertado correctamente!", "Inserción exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MostrarRegistrosHorarios(_InterCrud.DataGridViewCRUD); // Mostrar los horarios actualizados en la vista
            }
            else
            {
                _InterCrud.MostrarMensaje("¡Error al insertar el horario!", "Error al insertar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ActualizarRegistrosHorario(Horario horario)
        {
            bool result = _model.ActualizarRegistroHorarios(horario); // Llama al método correspondiente en tu modelo
            if (result)
            {
                _InterCrud.MostrarMensaje("¡El horario se ha actualizado correctamente!", "Actualización exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MostrarHorarios(_InterCrud.DataGridViewCRUD); // Mostrar los horarios actualizados en la vista
            }
            else
            {
                _InterCrud.MostrarMensaje("¡Error al actualizar el horario!", "Error al actualizar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void EliminarRegistrosHorario(int idHorario)
        {
            bool result = _model.EliminarRegistroHorarios(idHorario); // Llama al método correspondiente en tu modelo
            if (result)
            {
                _InterCrud.MostrarMensaje("¡El horario se ha eliminado correctamente!", "Eliminación exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MostrarRegistrosHorarios(_InterCrud.DataGridViewCRUD); // Mostrar los horarios actualizados en la vista
            }
            else
            {
                _InterCrud.MostrarMensaje("¡Error al eliminar el horario!", "Error al eliminar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool ValidarCamposRegistrosHorario( string nombre, string entradaLunesViernes, string salidaLunesViernes, string entradaSabado, string salidaSabado)
        {
            bool nombreValido = Validaciones.ValidarNoVacio(nombre, mensaje => _InterCrud.MostrarMensaje(mensaje, "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error), "Nombre");
            bool entradaLVValida = Validaciones.ValidarHora(entradaLunesViernes, mensaje => _InterCrud.MostrarMensaje(mensaje, "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error), "Entrada");
            bool salidaLVValida = Validaciones.ValidarHora(salidaLunesViernes, mensaje => _InterCrud.MostrarMensaje(mensaje, "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error), "Salida");
            bool entradaSabadoValida = Validaciones.ValidarHora(entradaSabado, mensaje => _InterCrud.MostrarMensaje(mensaje, "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error), "Entrada");
            bool salidaSabadoValida = Validaciones.ValidarHora(salidaSabado, mensaje => _InterCrud.MostrarMensaje(mensaje, "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error), "Salida");

            return nombreValido && entradaLVValida && salidaLVValida && entradaSabadoValida && salidaSabadoValida;
        }

        public void MostrarUsuarios(DataGridView dataGridView)
        {
            dataGridView.Rows.Clear();
            List<Usuario> usuarios = _model.ObtenerUsuarios(); // Utiliza el método correspondiente del modelo

            if (dataGridView.Columns.Count == 0)
            {
                dataGridView.Columns.Add("idUsuario", "ID Usuario");
                dataGridView.Columns.Add("nombreUsuario", "Nombre Usuario");
                dataGridView.Columns.Add("contrasena", "Contraseña");
                dataGridView.Columns.Add("idEmpleado", "ID Empleado");
            }

            foreach (var usuario in usuarios)
            {
                dataGridView.Rows.Add(usuario.idUsuario, usuario.NombreUsuario, usuario.Contrasena, usuario.idEmpleado);
            }
        }

        public void InsertarUsuario(Usuario usuario)
        {
            bool resultado = _model.InsertarUsuario(usuario); // Utiliza el método correspondiente del modelo
            if (resultado)
            {
                _InterCrud.MostrarMensaje("¡El usuario se ha insertado correctamente!", "Inserción exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MostrarUsuarios(_InterCrud.DataGridViewCRUD); // Mostrar los usuarios actualizados en la vista
            }
            else
            {
                _InterCrud.MostrarMensaje("¡Error al insertar el usuario!", "Error al insertar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       

        public void ActualizarUsuario(Usuario usuario)
        {
            bool result = _model.ActualizarUsuario(usuario); // Llama al método correspondiente en tu modelo
            if (result)
            {
                _InterCrud.MostrarMensaje("¡El usuario se ha actualizado correctamente!", "Actualización exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MostrarUsuarios(_InterCrud.DataGridViewCRUD); // Mostrar los usuarios actualizados en la vista
            }
            else
            {
                _InterCrud.MostrarMensaje("¡Error al actualizar el usuario!", "Error al actualizar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void EliminarUsuario(int idUsuario)
        {
            bool result = _model.EliminarUsuario(idUsuario); // Llama al método correspondiente en tu modelo
            if (result)
            {
                _InterCrud.MostrarMensaje("¡El usuario se ha eliminado correctamente!", "Eliminación exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MostrarUsuarios(_InterCrud.DataGridViewCRUD); // Mostrar los usuarios actualizados en la vista
            }
            else
            {
                _InterCrud.MostrarMensaje("¡Error al eliminar el usuario!", "Error al eliminar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool ValidarCamposUsuario(string nombreUsuario, string contrasena, string idEmpleado)
        {
            bool nombreUsuarioValido = Validaciones.ValidarNoVacio(nombreUsuario, mensaje => _InterCrud.MostrarMensaje(mensaje, "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error), "Nombre Usuario");
            bool contrasenaValida = Validaciones.ValidarNoVacio(contrasena, mensaje => _InterCrud.MostrarMensaje(mensaje, "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error), "Contraseña");
            bool idEmpleadoValido = Validaciones.ValidarId(idEmpleado, mensaje => _InterCrud.MostrarMensaje(mensaje, "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error));

            return nombreUsuarioValido && contrasenaValida && idEmpleadoValido;
        }

        public void MostrarRegistrosJornadas(DataGridView dataGridView)
        {
            dataGridView.Rows.Clear();
            List<RegistroJornada> registrosJornadas = _model.ObtenerRegistrosJornadas(); // Utiliza el método correspondiente del modelo

            if (dataGridView.Columns.Count == 0)
            {
                dataGridView.Columns.Add("IdRegistroHora", "ID Registro");
                dataGridView.Columns.Add("IdEmpleado", "ID Empleado");
                dataGridView.Columns.Add("Fecha", "Fecha");
                dataGridView.Columns.Add("HoraEntrada", "Hora Entrada");
                dataGridView.Columns.Add("HoraSalida", "Hora Salida");
                dataGridView.Columns.Add("HorasTardias", "Horas Tardías");
                dataGridView.Columns.Add("HorasExtras", "Horas Extras");
            }

            foreach (var registroJornada in registrosJornadas)
            {
                dataGridView.Rows.Add(registroJornada.IdRegistroHora, registroJornada.IdEmpleado, registroJornada.Fecha, registroJornada.HoraEntrada, registroJornada.HoraSalida, registroJornada.HrsTardias, registroJornada.HrsExtras);
            }
        }

        public void InsertarRegistroJornada(RegistroJornada registroJornada)
        {
            bool resultado = _model.InsertarRegistroJornada(registroJornada); // Utiliza el método correspondiente del modelo
            if (resultado)
            {
                _InterCrud.MostrarMensaje("¡El registro de jornada se ha insertado correctamente!", "Inserción exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MostrarRegistrosJornadas(_InterCrud.DataGridViewCRUD); // Mostrar los registros actualizados en la vista
            }
            else
            {
                _InterCrud.MostrarMensaje("¡Error al insertar el registro de jornada!", "Error al insertar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void InsertarJornada(RegistroJornada jornada)
        {
            bool resultado = _model.InsertarRegistroJornada(jornada); // Utiliza el método correspondiente del modelo
            if (resultado)
            {
                _view.MostrarMensaje("¡La sesión se ha registrado con éxito, hasta luego!", "Inserción exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                _view.MostrarMensaje("¡Error al insertar el usuario!", "Error al insertar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ActualizarRegistroJornada(RegistroJornada registroJornada)
        {
            bool result = _model.ActualizarRegistroJornada(registroJornada); // Llama al método correspondiente en tu modelo
            if (result)
            {
                _InterCrud.MostrarMensaje("¡El registro de jornada se ha actualizado correctamente!", "Actualización exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MostrarRegistrosJornadas(_InterCrud.DataGridViewCRUD); // Mostrar los registros actualizados en la vista
            }
            else
            {
                _InterCrud.MostrarMensaje("¡Error al actualizar el registro de jornada!", "Error al actualizar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void EliminarRegistroJornada(int idRegistroHora)
        {
            bool result = _model.EliminarRegistroJornada(idRegistroHora); // Llama al método correspondiente en tu modelo
            if (result)
            {
                _InterCrud.MostrarMensaje("¡El registro de jornada se ha eliminado correctamente!", "Eliminación exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MostrarRegistrosJornadas(_InterCrud.DataGridViewCRUD); // Mostrar los registros actualizados en la vista
            }
            else
            {
                _InterCrud.MostrarMensaje("¡Error al eliminar el registro de jornada!", "Error al eliminar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool ValidarCamposRegistroJornada(string idEmpleado, string fecha, string horaEntrada, string horaSalida, string horasTardias, string horasExtras)
        {
            bool idEmpleadoValido = Validaciones.ValidarId(idEmpleado, mensaje => _InterCrud.MostrarMensaje(mensaje, "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error));
            bool FechaValida = Validaciones.ValidarFecha(fecha, mensaje => _InterCrud.MostrarMensaje(mensaje, "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error));
            bool horasEntrada = Validaciones.ValidarHora(horaEntrada, mensaje => _InterCrud.MostrarMensaje(mensaje, "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error),"Hora Entrada");
            bool horasSalida = Validaciones.ValidarHora(horaSalida, mensaje => _InterCrud.MostrarMensaje(mensaje, "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error), "Hora Salida");
            bool horaTardia = Validaciones.ValidarHora(horasTardias, mensaje => _InterCrud.MostrarMensaje(mensaje, "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error), "Hora Tardia");
            bool horaExtra= Validaciones.ValidarHora(horasExtras, mensaje => _InterCrud.MostrarMensaje(mensaje, "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error), "Hora Extra");
            return idEmpleadoValido;
        }

        public bool ValidarCamposidActu(string id)
        {
            bool idValido = Validaciones.ValidarId(id, mensaje => _InterCrud.MostrarMensaje(mensaje, "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error));

            return idValido;
        }

        public void ExportarDataGridViewAExcel(DataGridView dgv, string filePath)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Sheet1");

                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    worksheet.Cell(1, i + 1).Value = dgv.Columns[i].HeaderText;
                }

                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    for (int j = 0; j < dgv.Columns.Count; j++)
                    {
                        worksheet.Cell(i + 2, j + 1).Value = dgv.Rows[i].Cells[j].Value?.ToString() ?? string.Empty;
                    }
                }

                workbook.SaveAs(filePath);
            }
        }

        public void ExportarDataGridViewAPDF(DataGridView dgv, string filePath)
        {
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                Document pdfDoc = new Document(PageSize.A4);
                PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();

                PdfPTable pdfTable = new PdfPTable(dgv.Columns.Count);

                foreach (DataGridViewColumn column in dgv.Columns)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                    pdfTable.AddCell(cell);
                }

                foreach (DataGridViewRow row in dgv.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        pdfTable.AddCell(cell.Value?.ToString() ?? string.Empty);
                    }
                }

                pdfDoc.Add(pdfTable);
                pdfDoc.Close();
                stream.Close();
            }
        }

    }
}
