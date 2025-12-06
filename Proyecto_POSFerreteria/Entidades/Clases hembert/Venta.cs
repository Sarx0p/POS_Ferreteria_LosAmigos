using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_POSFerreteria.Entidades.Clases_hembert
{
    public class Venta
    {
        public int Id { get; set; }
        public DateTime FechaVenta { get; set; }
        public decimal Total { get; set; }
        public int IdCliente { get; set; }
        public int IdUsuario { get; set; }
        public int IdTipoPago { get; set; }
        public string NombreUsuario { get; set; }
    }
}
