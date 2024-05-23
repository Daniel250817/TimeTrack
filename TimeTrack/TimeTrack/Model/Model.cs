using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TimeTrack.Model
{
    public class Nomina
    {
        public int idNomina { get; set; }
        public int idEmpleado { get; set; }
        public DateTime fecha { get; set; }
        public decimal descuento { get; set; }
        public decimal salarioBase { get; set; }
        public decimal montoHrsExtra { get; set; }
        public decimal montoHrsDescuento { get; set; }
        public decimal salarioNeto { get; set; }
    }
    public class DatosSesion 
    {
         public int idempleado { get; set; }
         public string nombre { get; set; }
         public string apellido { get; set; }
         public string cargo { get; set; }  

    }
    public class Usuario
    {
        public int idUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string Contrasena { get; set; }
        public int idEmpleado { get; set; }
    }

    public class Horario
    {
        public int idHorario { get; set; }
        public string nombreHorario { get; set; }
        public string entradaLunesViernes { get; set; }
        public string salidaLunesViernes { get; set; }
        public string entradaSabado { get; set; }
        public string salidaSabado { get; set; }
    }

    public class RegistroHorario
    {
        public int idHorarioEmpleado { get; set; }
        public int idEmpleado { get; set; }
        public int idHorario { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFin { get; set; }
    }

    public class RegistroJornada
    {
        public int IdRegistroHora { get; set; }
        public int IdEmpleado { get; set; }
        public DateTime Fecha { get; set; }
        public string HoraEntrada { get; set; }
        public string HoraSalida { get; set; }
        public string HrsTardias { get; set; }
        public string HrsExtras { get; set; }
    }


    public class Empleado
    {
        public int IdEmpleado { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Direccion { get; set; }
        public int IdCargo { get; set; }
        public string Telefono { get; set; }
    }

    internal class Model
    {

        /*---------Nominas---------*/
        public List<Nomina> ObtenerNominasEmpleadoLogueado(int idEmpleado)
        {
            List<Nomina> nominas = new List<Nomina>();

            // Obtener la cadena de conexión desde app.config
            string connectionString = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;

            // Consulta SQL para obtener las nominas del empleado logueado
            string consulta = "SELECT id_nomina, id_empleado, fecha, descuento, salarioBase, montoHrsExtra, montoHrsDescuento, salarioNeto " +
                              "FROM nomina " +
                              "WHERE id_empleado = @IdEmpleado";

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@IdEmpleado", idEmpleado);

                    // Abrir la conexión
                    conexion.Open();

                    // Ejecutar la consulta y leer los resultados
                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Crear un objeto Nomina con los datos de la fila actual
                            Nomina nomina = new Nomina
                            {
                                idNomina = reader.GetInt32(reader.GetOrdinal("id_nomina")),
                                idEmpleado = reader.GetInt32(reader.GetOrdinal("id_empleado")),
                                fecha = reader.GetDateTime(reader.GetOrdinal("fecha")),
                                descuento = reader.GetDecimal(reader.GetOrdinal("descuento")),
                                salarioBase = reader.GetDecimal(reader.GetOrdinal("salarioBase")),
                                montoHrsExtra = reader.GetDecimal(reader.GetOrdinal("montoHrsExtra")),
                                montoHrsDescuento = reader.GetDecimal(reader.GetOrdinal("montoHrsDescuento")),
                                salarioNeto = reader.GetDecimal(reader.GetOrdinal("salarioNeto"))
                            };

                            // Agregar la nomina a la lista de nominas
                            nominas.Add(nomina);
                        }
                    }
                }
            }

            return nominas;
        }



        public static List<Nomina> ObtenerTodasLasNominas()
        {
            List<Nomina> nominas = new List<Nomina>();
            string connectionString = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                string consulta = "SELECT id_nomina, id_empleado, fecha, descuento, salarioBase, montoHrsExtra, montoHrsDescuento, salarioNeto FROM nomina";

                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    conexion.Open();
                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Nomina nomina = new Nomina
                            {
                                idNomina = reader.GetInt32(reader.GetOrdinal("id_nomina")),
                                idEmpleado = reader.GetInt32(reader.GetOrdinal("id_empleado")),
                                fecha = reader.GetDateTime(reader.GetOrdinal("fecha")),
                                salarioBase = reader.GetDecimal(reader.GetOrdinal("salarioBase")),
                                descuento = reader.GetDecimal(reader.GetOrdinal("descuento")),
                                montoHrsExtra = reader.GetDecimal(reader.GetOrdinal("montoHrsExtra")),
                                montoHrsDescuento = reader.GetDecimal(reader.GetOrdinal("montoHrsDescuento")),
                                salarioNeto = reader.GetDecimal(reader.GetOrdinal("salarioNeto"))
                            };
                            nominas.Add(nomina);
                        }
                    }
                }
            }
            return nominas;
        }

        public bool InsertarNomina(Nomina nomina)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;

                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    string consulta = "INSERT INTO nomina (id_empleado, fecha, descuento, salarioBase, montoHrsExtra, montoHrsDescuento, salarioNeto) " +
                                      "VALUES (@IdEmpleado, @Fecha, @Descuento, @SalarioBase, @MontoHrsExtra, @MontoHrsDescuento, @SalarioNeto)";

                    using (SqlCommand comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@IdEmpleado", nomina.idEmpleado);
                        comando.Parameters.AddWithValue("@Fecha", nomina.fecha);
                        comando.Parameters.AddWithValue("@Descuento", nomina.descuento);
                        comando.Parameters.AddWithValue("@SalarioBase", nomina.salarioBase);
                        comando.Parameters.AddWithValue("@MontoHrsExtra", nomina.montoHrsExtra);
                        comando.Parameters.AddWithValue("@MontoHrsDescuento", nomina.montoHrsDescuento);
                        comando.Parameters.AddWithValue("@SalarioNeto", nomina.salarioNeto);

                        conexion.Open();
                        int rowsAffected = comando.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar la nómina: " + ex.Message);
                return false;
            }
        }


        public bool ActualizarNomina(Nomina nomina)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;

                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    string consulta = "UPDATE nomina SET id_empleado = @IdEmpleado, fecha = @Fecha, descuento = @Descuento, salarioBase = @SalarioBase, montoHrsExtra = @MontoHrsExtra, montoHrsDescuento = @MontoHrsDescuento, salarioNeto = @SalarioNeto " +
                                      "WHERE id_nomina = @IdNomina";

                    using (SqlCommand comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@IdNomina", nomina.idNomina);
                        comando.Parameters.AddWithValue("@IdEmpleado", nomina.idEmpleado);
                        comando.Parameters.AddWithValue("@Fecha", nomina.fecha);
                        comando.Parameters.AddWithValue("@Descuento", nomina.descuento);
                        comando.Parameters.AddWithValue("@SalarioBase", nomina.salarioBase);
                        comando.Parameters.AddWithValue("@MontoHrsExtra", nomina.montoHrsExtra);
                        comando.Parameters.AddWithValue("@MontoHrsDescuento", nomina.montoHrsDescuento);
                        comando.Parameters.AddWithValue("@SalarioNeto", nomina.salarioNeto);

                        conexion.Open();
                        comando.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar la nómina: " + ex.Message);
                return false;
            }
            
        }

        public static void EliminarNomina(int idNomina)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                string consulta = "DELETE FROM nomina WHERE id_nomina = @IdNomina";

                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@IdNomina", idNomina);
                    conexion.Open();
                    comando.ExecuteNonQuery();
                }
            }
        }

        /*---------Empleados---------*/



        /*---------Horario-----------*/

        public static List<RegistroHorario> ObtenerRegistroHorarios()
        {
            List<RegistroHorario> horarioRegistro = new List<RegistroHorario>();
            string connectionString = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;

            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    string consulta = "SELECT id_empleado_horario, id_empleado, id_horario, fecha_inicio, fecha_fin FROM empleado_horario";

                    using (SqlCommand comando = new SqlCommand(consulta, conexion))
                    {
                        conexion.Open();
                        using (SqlDataReader reader = comando.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                RegistroHorario horarioRegistros = new RegistroHorario
                                {
                                    idHorarioEmpleado = reader.GetInt32(reader.GetOrdinal("id_empleado_horario")),
                                    idEmpleado = reader.GetInt32(reader.GetOrdinal("id_empleado")),
                                    idHorario = reader.GetInt32(reader.GetOrdinal("id_horario")),
                                    fechaInicio = reader.GetDateTime(reader.GetOrdinal("fecha_inicio")),
                                    fechaFin = reader.GetDateTime(reader.GetOrdinal("fecha_fin"))
                                };
                                horarioRegistro.Add(horarioRegistros);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Se produjo un error al acceder a la base de datos: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se produjo un error inesperado: " + ex.Message);
            }

            return horarioRegistro;
        }

        public bool InsertarHorario(RegistroHorario horario)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;

                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    string consulta = "INSERT INTO empleado_horario (id_empleado, id_horario, fecha_inicio, fecha_fin) " +
                                      "VALUES (@IdEmpleado, @IdHorario, @FechaInicio, @FechaFin)";

                    using (SqlCommand comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@IdEmpleado", horario.idEmpleado);
                        comando.Parameters.AddWithValue("@IdHorario", horario.idHorario);
                        comando.Parameters.AddWithValue("@FechaInicio", horario.fechaInicio);
                        comando.Parameters.AddWithValue("@FechaFin", horario.fechaFin);

                        conexion.Open();
                        int rowsAffected = comando.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar el horario: " + ex.Message);
                return false;
            }
        }

        public bool ActualizarHorario(RegistroHorario horario)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;

                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    string consulta = "UPDATE empleado_horario SET id_empleado = @IdEmpleado, id_horario = @IdHorario, fecha_inicio = @FechaInicio, fecha_fin = @FechaFin " +
                                      "WHERE id_empleado_horario = @IdHorarioEmpleado";

                    using (SqlCommand comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@IdHorarioEmpleado", horario.idHorarioEmpleado);
                        comando.Parameters.AddWithValue("@IdEmpleado", horario.idEmpleado);
                        comando.Parameters.AddWithValue("@IdHorario", horario.idHorario);
                        comando.Parameters.AddWithValue("@FechaInicio", horario.fechaInicio);
                        comando.Parameters.AddWithValue("@FechaFin", horario.fechaFin);

                        conexion.Open();
                        comando.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar el horario: " + ex.Message);
                return false;
            }
        }

        public static bool EliminarHorario(int idHorarioEmpleado)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;

                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    string consulta = "DELETE FROM empleado_horario WHERE id_empleado_horario = @IdHorarioEmpleado";

                    using (SqlCommand comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@IdHorarioEmpleado", idHorarioEmpleado);
                        conexion.Open();
                        int rowsAffected = comando.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el horario: " + ex.Message);
                return false;
            }
        }





        public static DatosSesion ObtenerDatosEmpleadoLogueado(string nombreUsuario)
        {
            DatosSesion sesion = new DatosSesion();

            // Obtener la cadena de conexión desde app.config
            string connectionString = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                // Consulta SQL para obtener los datos del empleado asociados al nombre de usuario
                string consulta = "SELECT e.id_empleado, e.nombres, e.apellidos, c.cargo_empleado AS cargo " +
                                  "FROM empleado e " +
                                  "JOIN usuarios u ON e.id_empleado = u.id_empleado " +
                                  "JOIN cargo c ON e.id_cargo = c.id_cargo " +
                                  "WHERE u.nombre_usuario = @NombreUsuario";

                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);

                    // Abrir la conexión
                    conexion.Open();

                    // Ejecutar la consulta y leer los resultados
                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Asignar los valores leídos a las propiedades del objeto Empleado
                            sesion.idempleado = reader.GetInt32(reader.GetOrdinal("id_empleado"));
                            sesion.nombre = reader.GetString(reader.GetOrdinal("nombres"));
                            sesion.apellido = reader.GetString(reader.GetOrdinal("apellidos"));
                            sesion.cargo = reader.GetString(reader.GetOrdinal("cargo"));
                        }
                    }
                }
            }
            return sesion;
        }

        public Horario ObtenerHorarioEmpleado(int idEmpleado)
        {
            Horario horario = null;
            string connectionString = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;

            string consulta = "SELECT h.[nombre], h.[hora_entrada_lunes_viernes], h.[hora_salida_lunes_viernes], h.[hora_entrada_sabado], h.[hora_salida_sabado] " +
                              "FROM [SISHRS].[dbo].[empleado_horario] eh " +
                              "INNER JOIN [SISHRS].[dbo].[horario] h ON eh.[id_horario] = h.[id_horario] " +
                              "WHERE eh.[id_empleado] = @IdEmpleado";

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@IdEmpleado", idEmpleado);
                    conexion.Open();

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Obtener los valores del horario
                            string nombreHorario = reader["nombre"].ToString();
                            string entradaLunesViernes = reader["hora_entrada_lunes_viernes"].ToString();
                            string salidaLunesViernes = reader["hora_salida_lunes_viernes"].ToString();
                            string entradaSabado = reader["hora_entrada_sabado"].ToString();
                            string salidaSabado = reader["hora_salida_sabado"].ToString();

                            // Crear un objeto Horario con los valores obtenidos
                            horario = new Horario
                            {
                                nombreHorario = nombreHorario,
                                entradaLunesViernes = entradaLunesViernes,
                                salidaLunesViernes = salidaLunesViernes,
                                entradaSabado = entradaSabado,
                                salidaSabado = salidaSabado
                            };
                        }
                    }
                }
            }

            return horario;
        }


        /*--------------Jornada Logueado-----------------*/

        public List<RegistroJornada> ObtenerJornadaEmpleado(int idEmpleado)
        {
            List<RegistroJornada> jornadas = new List<RegistroJornada>();
            string connectionString = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;

            string consulta = "SELECT [id_registro_hora], [id_empleado], [fecha], [hora_entrada], [hora_salida], [hrsTardias], [hrsExtras] " +
                              "FROM [SISHRS].[dbo].[registro_jornadas] " +
                              "WHERE [id_empleado] = @IdEmpleado " +
                              "ORDER BY [fecha] DESC";

            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    using (SqlCommand comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@IdEmpleado", idEmpleado);
                        conexion.Open();

                        using (SqlDataReader reader = comando.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    // Obtener los valores del registro de hora
                                    int idRegistroHora = Convert.ToInt32(reader["id_registro_hora"]);
                                    int idEmp = Convert.ToInt32(reader["id_empleado"]);
                                    DateTime fechaRegistro = Convert.ToDateTime(reader["fecha"]);
                                    string horaEntrada = reader["hora_entrada"].ToString();
                                    string horaSalida = reader["hora_salida"].ToString();
                                    string hrsTardias = reader["hrsTardias"].ToString();
                                    string hrsExtras = reader["hrsExtras"].ToString();

                                    // Crear un objeto RegistroHora con los valores obtenidos
                                    RegistroJornada jornada = new RegistroJornada
                                    {
                                        IdRegistroHora = idRegistroHora,
                                        IdEmpleado = idEmp,
                                        Fecha = fechaRegistro,
                                        HoraEntrada = horaEntrada,
                                        HoraSalida = horaSalida,
                                        HrsTardias = hrsTardias,
                                        HrsExtras = hrsExtras
                                    };

                                    // Agregar el registro a la lista
                                    jornadas.Add(jornada);
                                }
                            }
                            else
                            {
                                // Mostrar mensaje si no se encuentra el registro
                                MessageBox.Show("No se encontró ningún registro para el ID del empleado especificado.", "Registro no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de cualquier excepción que pueda ocurrir
                MessageBox.Show("Se produjo un error al acceder a la base de datos: " + ex.Message, "Error de base de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return jornadas;
        }


        /*----------Empleados------------*/

        public static Empleado ObtenerDatosEmpleadoLogueado(int idEmpleado)
        {
            Empleado empleado = new Empleado();

            // Obtener la cadena de conexión desde app.config
            string connectionString = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                // Consulta SQL para obtener los datos del empleado basados en su ID
                string consulta = "SELECT id_empleado, nombres, apellidos, fecha_nac, direccion, id_cargo, telefono " +
                                  "FROM empleado " +
                                  "WHERE id_empleado = @IdEmpleado";

                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@IdEmpleado", idEmpleado);

                    // Abrir la conexión
                    conexion.Open();

                    // Ejecutar la consulta y leer los resultados
                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Asignar los valores leídos a las propiedades del objeto Empleado
                            empleado.IdEmpleado = reader.GetInt32(reader.GetOrdinal("id_empleado"));
                            empleado.Nombres = reader.GetString(reader.GetOrdinal("nombres"));
                            empleado.Apellidos = reader.GetString(reader.GetOrdinal("apellidos"));
                            empleado.FechaNacimiento = reader.GetDateTime(reader.GetOrdinal("fecha_nac"));
                            empleado.Direccion = reader.GetString(reader.GetOrdinal("direccion"));
                            empleado.IdCargo = reader.GetInt32(reader.GetOrdinal("id_cargo"));
                            empleado.Telefono = reader.GetString(reader.GetOrdinal("telefono"));
                        }
                    }
                }
            }
            return empleado;
        }


        public static List<Empleado> ObtenerEmpleados()
        {
            List<Empleado> empleados = new List<Empleado>();
            string connectionString = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;

            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    string consulta = "SELECT id_empleado, nombres, apellidos, fecha_nac, direccion, id_cargo, telefono FROM empleado";

                    using (SqlCommand comando = new SqlCommand(consulta, conexion))
                    {
                        conexion.Open();
                        using (SqlDataReader reader = comando.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Empleado empleado = new Empleado
                                {
                                    IdEmpleado = reader.GetInt32(reader.GetOrdinal("id_empleado")),
                                    Nombres = reader.GetString(reader.GetOrdinal("nombres")),
                                    Apellidos = reader.GetString(reader.GetOrdinal("apellidos")),
                                    FechaNacimiento = reader.GetDateTime(reader.GetOrdinal("fecha_nac")),
                                    Direccion = reader.GetString(reader.GetOrdinal("direccion")),
                                    IdCargo = reader.GetInt32(reader.GetOrdinal("id_cargo")),
                                    Telefono = reader.GetString(reader.GetOrdinal("telefono"))
                                };
                                empleados.Add(empleado);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Se produjo un error al acceder a la base de datos: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se produjo un error inesperado: " + ex.Message);
            }

            return empleados;
        }

        public bool InsertarEmpleado(Empleado empleado)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;

                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    string consulta = "INSERT INTO empleado (nombres, apellidos, fecha_nac, direccion, id_cargo, telefono) " +
                                      "VALUES (@Nombres, @Apellidos, @FechaNac, @Direccion, @IdCargo, @Telefono)";

                    using (SqlCommand comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@Nombres", empleado.Nombres);
                        comando.Parameters.AddWithValue("@Apellidos", empleado.Apellidos);
                        comando.Parameters.AddWithValue("@FechaNac", empleado.FechaNacimiento);
                        comando.Parameters.AddWithValue("@Direccion", empleado.Direccion);
                        comando.Parameters.AddWithValue("@IdCargo", empleado.IdCargo);
                        comando.Parameters.AddWithValue("@Telefono", empleado.Telefono);

                        conexion.Open();
                        int rowsAffected = comando.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar el empleado: " + ex.Message);
                return false;
            }
        }

        public bool ActualizarEmpleado(Empleado empleado)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;

                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    string consulta = "UPDATE empleado SET nombres = @Nombres, apellidos = @Apellidos, fecha_nac = @FechaNac, direccion = @Direccion, id_cargo = @IdCargo, telefono = @Telefono " +
                                      "WHERE id_empleado = @IdEmpleado";

                    using (SqlCommand comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@IdEmpleado", empleado.IdEmpleado);
                        comando.Parameters.AddWithValue("@Nombres", empleado.Nombres);
                        comando.Parameters.AddWithValue("@Apellidos", empleado.Apellidos);
                        comando.Parameters.AddWithValue("@FechaNac", empleado.FechaNacimiento);
                        comando.Parameters.AddWithValue("@Direccion", empleado.Direccion);
                        comando.Parameters.AddWithValue("@IdCargo", empleado.IdCargo);
                        comando.Parameters.AddWithValue("@Telefono", empleado.Telefono);

                        conexion.Open();
                        comando.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar el empleado: " + ex.Message);
                return false;
            }
        }

        public bool EliminarEmpleado(int idEmpleado)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;

                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    string consulta = "DELETE FROM empleado WHERE id_empleado = @IdEmpleado";

                    using (SqlCommand comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@IdEmpleado", idEmpleado);
                        conexion.Open();
                        int rowsAffected = comando.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar el empleado: " + ex.Message);
                return false;
            }
        }

        public List<Horario> ObtenerHorarios()
        {
            List<Horario> horarios = new List<Horario>();
            string connectionString = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;

            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    string consulta = "SELECT id_horario, nombre, hora_entrada_lunes_viernes, hora_salida_lunes_viernes, hora_entrada_sabado, hora_salida_sabado FROM horario";

                    using (SqlCommand comando = new SqlCommand(consulta, conexion))
                    {
                        conexion.Open();
                        using (SqlDataReader reader = comando.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Horario horario = new Horario
                                {
                                    idHorario = reader.GetInt32(reader.GetOrdinal("id_horario")),
                                    nombreHorario = reader.GetString(reader.GetOrdinal("nombre")),
                                    entradaLunesViernes = reader.GetTimeSpan(reader.GetOrdinal("hora_entrada_lunes_viernes")).ToString(), // Convertir TimeSpan a string
                                    salidaLunesViernes = reader.GetTimeSpan(reader.GetOrdinal("hora_salida_lunes_viernes")).ToString(), // Convertir TimeSpan a string
                                    entradaSabado = reader.GetTimeSpan(reader.GetOrdinal("hora_entrada_sabado")).ToString(), // Convertir TimeSpan a string
                                    salidaSabado = reader.GetTimeSpan(reader.GetOrdinal("hora_salida_sabado")).ToString() // Convertir TimeSpan a string
                                };
                                horarios.Add(horario);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener los horarios: " + ex.Message);
            }

            return horarios;
        }

        public bool InsertarRegistroHorarios(Horario horario)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;

            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    string consulta = "INSERT INTO horario (nombre, hora_entrada_lunes_viernes, hora_salida_lunes_viernes, hora_entrada_sabado, hora_salida_sabado) " +
                                      "VALUES (@Nombre, @EntradaLunesViernes, @SalidaLunesViernes, @EntradaSabado, @SalidaSabado)";

                    using (SqlCommand comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@Nombre", horario.nombreHorario);
                        comando.Parameters.AddWithValue("@EntradaLunesViernes", horario.entradaLunesViernes);
                        comando.Parameters.AddWithValue("@SalidaLunesViernes", horario.salidaLunesViernes);
                        comando.Parameters.AddWithValue("@EntradaSabado", horario.entradaSabado);
                        comando.Parameters.AddWithValue("@SalidaSabado", horario.salidaSabado);

                        conexion.Open();
                        int filasAfectadas = comando.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar el horario: " + ex.Message);
                return false;
            }
        }

        public bool ActualizarRegistroHorarios(Horario horario)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;

            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    string consulta = "UPDATE horario SET nombre = @Nombre, hora_entrada_lunes_viernes = @EntradaLunesViernes, " +
                                      "hora_salida_lunes_viernes = @SalidaLunesViernes, hora_entrada_sabado = @EntradaSabado, " +
                                      "hora_salida_sabado = @SalidaSabado WHERE id_horario = @IdHorario";

                    using (SqlCommand comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@Nombre", horario.nombreHorario);
                        comando.Parameters.AddWithValue("@EntradaLunesViernes", horario.entradaLunesViernes);
                        comando.Parameters.AddWithValue("@SalidaLunesViernes", horario.salidaLunesViernes);
                        comando.Parameters.AddWithValue("@EntradaSabado", horario.entradaSabado);
                        comando.Parameters.AddWithValue("@SalidaSabado", horario.salidaSabado);
                        comando.Parameters.AddWithValue("@IdHorario", horario.idHorario);

                        conexion.Open();
                        int filasAfectadas = comando.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el horario: " + ex.Message);
                return false;
            }
        }

        public bool EliminarRegistroHorarios(int idHorario)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;

            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    string consulta = "DELETE FROM horario WHERE id_horario = @IdHorario";

                    using (SqlCommand comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@IdHorario", idHorario);

                        conexion.Open();
                        int filasAfectadas = comando.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el horario: " + ex.Message);
                return false;
            }
        }

        public List<Usuario> ObtenerUsuarios()
        {
            List<Usuario> usuarios = new List<Usuario>();
            string connectionString = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;

            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    string consulta = "SELECT id_usuario, nombre_usuario, contrasena, id_empleado FROM usuarios";

                    using (SqlCommand comando = new SqlCommand(consulta, conexion))
                    {
                        conexion.Open();
                        using (SqlDataReader reader = comando.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Usuario usuario = new Usuario
                                {
                                    idUsuario = reader.GetInt32(reader.GetOrdinal("id_usuario")),
                                    NombreUsuario = reader.GetString(reader.GetOrdinal("nombre_usuario")),
                                    Contrasena = reader.GetString(reader.GetOrdinal("contrasena")),
                                    idEmpleado = reader.GetInt32(reader.GetOrdinal("id_empleado"))
                                };
                                usuarios.Add(usuario);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener los usuarios: " + ex.Message);
            }

            return usuarios;
        }

        public bool InsertarUsuario(Usuario usuario)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;

            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    string consulta = "INSERT INTO usuarios (nombre_usuario, contrasena, id_empleado) " +
                                      "VALUES (@NombreUsuario, @Contrasena, @IdEmpleado)";

                    using (SqlCommand comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@NombreUsuario", usuario.NombreUsuario);
                        comando.Parameters.AddWithValue("@Contrasena", usuario.Contrasena);
                        comando.Parameters.AddWithValue("@IdEmpleado", usuario.idEmpleado);

                        conexion.Open();
                        int rowsAffected = comando.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar el usuario: " + ex.Message);
                return false;
            }
        }

        public bool ActualizarUsuario(Usuario usuario)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;

            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    string consulta = "UPDATE usuarios SET nombre_usuario = @NombreUsuario, contrasena = @Contrasena, id_empleado = @IdEmpleado " +
                                      "WHERE id_usuario = @IdUsuario";

                    using (SqlCommand comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@IdUsuario", usuario.idUsuario);
                        comando.Parameters.AddWithValue("@NombreUsuario", usuario.NombreUsuario);
                        comando.Parameters.AddWithValue("@Contrasena", usuario.Contrasena);
                        comando.Parameters.AddWithValue("@IdEmpleado", usuario.idEmpleado);

                        conexion.Open();
                        int rowsAffected = comando.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el usuario: " + ex.Message);
                return false;
            }
        }

        public bool EliminarUsuario(int idUsuario)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;

            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    string consulta = "DELETE FROM usuarios WHERE id_usuario = @IdUsuario";

                    using (SqlCommand comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@IdUsuario", idUsuario);

                        conexion.Open();
                        int rowsAffected = comando.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el usuario: " + ex.Message);
                return false;
            }
        }






















        // Clase que maneja la lógica de negocio y la autenticación
        public class ModelManager
        {
            // Método para verificar las credenciales del usuario
            public bool VerificarCredenciales(string nombreUsuario, string contrasena)
            {
                try
                {
                    // Obtener la cadena de conexión desde app.config
                    string connectionString = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;

                    using (SqlConnection conexion = new SqlConnection(connectionString))
                    {
                        // Abrir la conexión
                        conexion.Open();

                        // Consulta SQL para verificar las credenciales
                        string consulta = "SELECT COUNT(*) FROM Usuarios WHERE nombre_usuario = @NombreUsuario AND contrasena = @Contrasena";

                        using (SqlCommand comando = new SqlCommand(consulta, conexion))
                        {
                            // Añadir parámetros para evitar inyección SQL
                            comando.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);
                            comando.Parameters.AddWithValue("@Contrasena", contrasena);

                            // Ejecutar la consulta y obtener el resultado
                            int cantidadFilas = (int)comando.ExecuteScalar();

                            // La consulta debe devolver exactamente 1 fila si las credenciales son correctas
                            return cantidadFilas == 1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Manejar cualquier excepción que pueda ocurrir
                    MessageBox.Show("Error al verificar las credenciales: " + ex.Message);
                    return false;
                }
            }
        }
    }
}
