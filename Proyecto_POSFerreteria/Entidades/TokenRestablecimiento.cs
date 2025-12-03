using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_POSFerreteria.Entidades
{
    public class TokenRestablecimiento
    {

        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public byte[] TokenHash { get; set; } // SHA256 bytes
        public DateTime ExpiraEn { get; set; }
        public bool Usado { get; set; }
        public string IP_Solicitud { get; set; }
        public byte TipoSolicitud { get; set; }
    }
}
