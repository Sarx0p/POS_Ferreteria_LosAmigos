using Proyecto_POSFerreteria.Entidades;
using Proyecto_POSFerreteria.Presentacion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_POSFerreteria.Datos
{
    public class ClienteDAL
    {


        public DataTable Listar()
        {
           DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(Conexion.Cadena))
            {
                string sql = "SELECT Id, NombreCompleto";

                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cn.Open();
                    new SqlDataAdapter(cmd).Fill(dt);

                }
            }
            return dt;
        }

        public int Insertar(Cliente c )
        { 
        using (SqlConnection cn = new SqlConnection(Conexion.Cadena))
            {
                string sql = "INSERT INTO Cliente (NombreCompleto) VALUES (@NombreCompleto,); SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@NombreCompleto", c.NombreCompleto);
                    cn.Open();
                    int idCliente = Convert.ToInt32(cmd.ExecuteScalar());
                    return idCliente;
                }
            }
        }






















    }
}
