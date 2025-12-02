using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_POSFerreteria.Entidades.Clases_hembert
{
    public class RegistroVenta
    {
        public int Id { get; set; }
        public int IdVenta { get; set; }
        public int IdUsuario { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string NombreUsuario { get; set; }
    }
}
