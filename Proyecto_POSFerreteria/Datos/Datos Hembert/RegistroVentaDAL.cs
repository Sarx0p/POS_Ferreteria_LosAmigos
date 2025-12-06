using Proyecto_POSFerreteria.Entidades.Clases_hembert;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_POSFerreteria.Datos.Datos_Hembert
{
    public class RegistroVentaDAL
    {
        public static void Insertar(RegistroVenta registro, SqlConnection con, SqlTransaction tx)
        {
            string sql = @"INSERT INTO RegistroVenta (IdVenta, IdUsuario, NombreUsuario)
                       VALUES (@IdVenta, @IdUsuario, @NombreUsuario);";

            using (SqlCommand cmd = new SqlCommand(sql, con, tx))
            {
                cmd.Parameters.AddWithValue("@IdVenta", registro.IdVenta);
                cmd.Parameters.AddWithValue("@IdUsuario", registro.IdUsuario);
                cmd.Parameters.AddWithValue("@NombreUsuario", registro.NombreUsuario);

                cmd.ExecuteNonQuery();
            }
        }
    }
}