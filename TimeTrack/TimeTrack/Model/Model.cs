using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace TimeTrack.Model
{
    internal class Model
    {
        public class Usuario
        {
            public string NombreUsuario { get; set; }
            public string Contrasena { get; set; }
        }

        public class Empleado 
        {
            public int id { get; set; }
            public string nombre { get; set; }
            public string apellido { get; set; }
            public string cargo { get; set; }

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
                            empleado.id = reader.GetInt32(reader.GetOrdinal("id_empleado"));
                            empleado.nombre = reader.GetString(reader.GetOrdinal("nombres"));
                            empleado.apellido = reader.GetString(reader.GetOrdinal("apellidos"));
                            empleado.cargo = reader.GetString(reader.GetOrdinal("cargo"));
                        }
                    }
                }
            }
            return empleado;
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
