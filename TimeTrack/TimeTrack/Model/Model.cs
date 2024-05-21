using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;

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
    public class Empleado 
    {
         public int idempleado { get; set; }
         public string nombre { get; set; }
         public string apellido { get; set; }
         public string cargo { get; set; }  

    }
    public class Usuario
    {
         public string NombreUsuario { get; set; }
         public string Contrasena { get; set; }


    }

    public class Horario
    {
        public string nombreHorario { get; set; }
        public string entradaLunesViernes { get; set; }
        public string salidaLunesViernes { get; set; }
        public string entradaSabado { get; set; }
        public string salidaSabado { get; set; }
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
                Console.WriteLine("Error al insertar la nómina: " + ex.Message);
                return false;
            }
        }


        public static void ActualizarNomina(Nomina nomina)
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
                }
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



















        public static Empleado ObtenerDatosEmpleadoLogueado(string nombreUsuario)
        {
            Empleado empleado = new Empleado();

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
                            empleado.idempleado = reader.GetInt32(reader.GetOrdinal("id_empleado"));
                            empleado.nombre = reader.GetString(reader.GetOrdinal("nombres"));
                            empleado.apellido = reader.GetString(reader.GetOrdinal("apellidos"));
                            empleado.cargo = reader.GetString(reader.GetOrdinal("cargo"));
                        }
                    }
                }
            }
            return empleado;
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
                    Console.WriteLine("Error al verificar las credenciales: " + ex.Message);
                    return false;
                }
            }
        }
    }
}
