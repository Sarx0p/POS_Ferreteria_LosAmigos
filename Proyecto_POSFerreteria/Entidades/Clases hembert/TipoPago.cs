using Proyecto_POSFerreteria.Datos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_POSFerreteria.Entidades.Clases_hembert
{
    public class TipoPago
    {
        public int Id { get; set; }
        public string Pago { get; set; }
    }
    public class TipoPagoDAL
    {
        public static List<TipoPago> Listar()
        {
            List<TipoPago> tiposPago = new List<TipoPago>();
            using (SqlConnection con = new SqlConnection(Conexion.Cadena))
            {
                string sql = "SELECT Id, Pago FROM TipoPago";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tiposPago.Add(new TipoPago
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Pago = reader["Pago"].ToString()
                            });
                        }
                    }
                }
            }
            return tiposPago;
        }
    }
}
