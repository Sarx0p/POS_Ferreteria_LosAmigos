using Proyecto_POSFerreteria.Datos;
using Proyecto_POSFerreteria.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_POSFerreteria.Datos
{
    public class ProductoDAL
    {
        public DataTable Listas()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(Conexion.Cadena))
            {
                string sql = @"SELECT p.Id,
                     p.NombreProducto,
                     c.NombreCategoria AS Categoria,
                     p.Precio,
                     p.Stock,
                     p.Estado
              FROM Producto p
              INNER JOIN CategoriaProducto c ON p.IdCategoriaProducto = c.Id"; ;

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        // INSERTAR DATOS
        public int Insertar(Producto p)
        {
            using (SqlConnection con = new SqlConnection(Conexion.Cadena))
            {
                string sql = @"
                    INSERT INTO Producto (NombreProducto, IdCategoriaProducto, Precio, Stock, Estado)
                    VALUES (@NombreProducto, @IdCategoriaProducto, @Precio, @Stock, @Estado);
                    SELECT SCOPE_IDENTITY();";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@NombreProducto", p.NombreProducto);
                    cmd.Parameters.AddWithValue("@IdCategoriaProducto", p.IdCategoriaProducto);
                    cmd.Parameters.AddWithValue("@Precio", p.Precio);
                    cmd.Parameters.AddWithValue("@Stock", p.Stock);
                    cmd.Parameters.AddWithValue("@Estado", p.Estado);

                    con.Open();
                    object result = cmd.ExecuteScalar();

                    if (result == null || result == DBNull.Value)
                        return 0;

                    return Convert.ToInt32(Convert.ToDecimal(result));
                }
            }
        }
        public bool Actualizar(Producto p)
        {
            using (SqlConnection con = new SqlConnection(Conexion.Cadena))
            {
                string sql = @"UPDATE Producto SET 
                NombreProducto, 
                Precio, 
                Stock, 
                Estado, 
                IdCategoriaProducto
                 
                   WHERE Id=@Id";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@Id", p.Id);
                    cmd.Parameters.AddWithValue("@NombreProducto", p.NombreProducto);
                    cmd.Parameters.AddWithValue("@Precio", p.Precio);
                    cmd.Parameters.AddWithValue("@Stock", p.Stock);
                    cmd.Parameters.AddWithValue("@Estado", p.Estado);
                    cmd.Parameters.AddWithValue("@IdCategoria", p.IdCategoriaProducto);

                    con.Open();

                    // ExecuteNonQuery devuelve número de filas afectadas
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        public bool Eliminar(int Id)
        {

            using (SqlConnection con = new SqlConnection(Conexion.Cadena))
            {
                string sql = "DELETE FROM Producto WHERE Id=@Id";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@Id", Id);
                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                    //Elimina y devuleve true si se elimino almenos una fila
                }
            }
        }
        public DataTable Buscar(string filtro)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(Conexion.Cadena))
            {
                string sql = @"SELECT 
                     p.Id,
                     p.NombreProducto,
                     c.NombreCategoria AS Categoria,
                     p.Precio,
                     p.Stock,
                     p.Estado
              FROM Producto p
              INNER JOIN CategoriaProducto c ON p.IdCategoriaProducto = c.Id";

                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@filtro", "%" + filtro + "%");
                    cn.Open();
                    new SqlDataAdapter(cmd).Fill(dt);
                }
            }
            return dt;
        }


        //VALIDACIONES

        public bool ExisteNombreProducto(string nombre)
        {
            using (SqlConnection cn = new SqlConnection(Conexion.Cadena))
            {
                string sql = "SELECT COUNT(*) FROM Producto WHERE NombreProducto = @nombre";

                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@nombre", nombre);

                    cn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
        }

        public bool ProductoTieneVentasAsociadas(int id)
        {
            using (SqlConnection cn = new SqlConnection(Conexion.Cadena))
            {
                string sql = @"SELECT COUNT(*) 
                       FROM Venta
                       WHERE Id_Producto = @idProducto";

                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@idProducto", id);

                    cn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
        }

        public bool ExisteNombreProductoEnOtro(string nombre, int id)
        {
            using (SqlConnection cn = new SqlConnection(Conexion.Cadena))
            {
                string sql = @"SELECT COUNT(*) 
                       FROM Producto
                       WHERE NombreProducto = @nombre AND Id <> @id";

                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@id", id);

                    cn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
        }


        public static List<Producto> Listar()
        {
            List<Producto> lista = new List<Producto>();

            using (SqlConnection con = new SqlConnection(Conexion.Cadena))
            {
                string sql = @"
        SELECT 
            p.Id,
            p.NombreProducto,
            p.Precio,
            p.Stock,
            p.Estado,
            c.NombreCategoria AS Categoria
        FROM Producto p
        INNER JOIN CategoriaProducto c ON p.IdCategoriaProducto = c.Id";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Producto
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                NombreProducto = dr["NombreProducto"].ToString(),
                                Precio = Convert.ToDecimal(dr["Precio"]),
                                Stock = Convert.ToInt32(dr["Stock"]),
                                Estado = Convert.ToBoolean(dr["Estado"]),
                                IdCategoriaProducto = int.Parse(dr["IdCategoriaProducto"].ToString())
                            });
                        }
                    }
                }
            }

            return lista;
        }
    }
}
        
    






