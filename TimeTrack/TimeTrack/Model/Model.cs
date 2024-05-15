using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTrack.Model
{
    internal class Model
    {
        // Clase que representa una empresa
        public class Empresa
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
            public string Descripcion { get; set; }
            public string Direccion { get; set; }
            public List<Empleado> Empleados { get; set; } // Relación uno a muchos con Empleado
        }

        // Clase que representa un empleado
        public class Empleado
        {
            public int Id { get; set; }
            public string Nombres { get; set; }
            public string Apellidos { get; set; }
            public DateTime FechaNacimiento { get; set; }
            public decimal Sueldo { get; set; }
            public string Direccion { get; set; }
            public string Telefono { get; set; }
            public int EmpresaId { get; set; } // Clave foránea a Empresa
            public int HorarioId { get; set; } // Clave foránea a Horario
            public Empresa Empresa { get; set; } // Relación muchos a uno con Empresa
            public Horario Horario { get; set; } // Relación uno a uno con Horario
            public List<RegistroHora> RegistrosHora { get; set; } // Relación uno a muchos con RegistroHora
            public List<Nomina> Nominas { get; set; } // Relación uno a muchos con Nomina
        }

        // Clase que representa el horario de un empleado
        public class Horario
        {
            public int Id { get; set; }
            public DateTime Horario1 { get; set; }
            public DateTime Horario2 { get; set; }
            public DateTime Horario3 { get; set; }
        }

        // Clase que representa una nómina
        public class Nomina
        {
            public int Id { get; set; }
            public decimal Sueldo { get; set; }
            public int EmpleadoId { get; set; } // Clave foránea a Empleado
            public int EmpresaId { get; set; } // Clave foránea a Empresa
            public string Concepto { get; set; }
            public decimal Descuento { get; set; }
            public string Descripcion { get; set; }
            public Empleado Empleado { get; set; } // Relación muchos a uno con Empleado
            public Empresa Empresa { get; set; } // Relación muchos a uno con Empresa
        }

        // Clase que representa un usuario
        public class Usuario
        {
            public int Id { get; set; }
            public string NombreUsuario { get; set; }
            public string Contraseña { get; set; }
            public int Nivel { get; set; }
            public int EmpleadoId { get; set; } // Clave foránea a Empleado
            public Empleado Empleado { get; set; } // Relación uno a uno con Empleado
        }

        // Clase que representa un registro de hora de un empleado
        public class RegistroHora
        {
            public int Id { get; set; }
            public int EmpleadoId { get; set; } // Clave foránea a Empleado
            public DateTime FechaIn { get; set; }
            public DateTime FechaOut { get; set; }
            public float Jornada { get; set; }
            public Empleado Empleado { get; set; } // Relación muchos a uno con Empleado
        }
    }
}
