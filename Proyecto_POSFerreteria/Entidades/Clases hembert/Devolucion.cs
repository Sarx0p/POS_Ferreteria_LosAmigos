using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_POSFerreteria.Entidades.Clases_hembert
{
    public class Devolucion
    {
        public int Id { get; set; }
        public int IdDetalleVenta { get; set; }
        public DateTime FechaDevolucion { get; set; }
        public int Cantidad { get; set; }
        public string Motivo { get; set; }
    }
}
