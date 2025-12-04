using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_POSFerreteria.Entidades
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Username { get; set; }
        public byte[] ContrasenaCifrada { get; set; } // bcrypt hash
        public string Rol { get; set; }
        public string Correo { get; set; }
        public string Dui { get; set; }
        public DateTime? Fecha_Ultimo_Cambio { get; set; }
        public bool Debe_Cambiar_Contrasena { get; set; }
        public int Dias_Minimos_Entre_Cambios { get; set; }
    }
}
