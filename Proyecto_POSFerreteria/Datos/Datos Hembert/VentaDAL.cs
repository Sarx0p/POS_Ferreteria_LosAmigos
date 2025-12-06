using Proyecto_POSFerreteria.Entidades.Clases_hembert;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_POSFerreteria.Datos.Datos_Hembert
{
    public class VentaDAL
    {
        public static ResultadoVenta RegistrarVenta(Venta venta, List<DetalleVenta> detalles)
        {
            ResultadoVenta resultado = new ResultadoVenta();

            using (SqlConnection con = new SqlConnection(Conexion.Cadena))
            {
                con.Open();
                SqlTransaction tx = con.BeginTransaction();

                try
                {
                    string sqlVenta = @"INSERT INTO Venta 
                                (FechaVenta, Total, IdCliente, IdUsuario, IdTipoPago)
                                VALUES (@FechaVenta, @Total, @IdCliente, @IdUsuario, @IdTipoPago);
                                SELECT SCOPE_IDENTITY();";

                    int idVenta;

                    using (SqlCommand cmd = new SqlCommand(sqlVenta, con, tx))
                    {
                        cmd.Parameters.AddWithValue("@FechaVenta", venta.FechaVenta);
                        cmd.Parameters.AddWithValue("@Total", venta.Total);
                        cmd.Parameters.AddWithValue("@IdCliente", venta.IdCliente);
                        cmd.Parameters.AddWithValue("@IdUsuario", venta.IdUsuario);
                        cmd.Parameters.AddWithValue("@IdTipoPago", venta.IdTipoPago);

                        idVenta = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    // Insertar detalles
                    string sqlDetalle = @"INSERT INTO DetalleVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, SubTotal)
                                  VALUES (@IdVenta, @IdProducto, @Cantidad, @PrecioUnitario, @SubTotal)";

                    foreach (var d in detalles)
                    {
                        using (SqlCommand cmdDet = new SqlCommand(sqlDetalle, con, tx))
                        {
                            cmdDet.Parameters.AddWithValue("@IdVenta", idVenta);
                            cmdDet.Parameters.AddWithValue("@IdProducto", d.IdProducto);
                            cmdDet.Parameters.AddWithValue("@Cantidad", d.Cantidad);
                            cmdDet.Parameters.AddWithValue("@PrecioUnitario", d.PrecioUnitario);
                            cmdDet.Parameters.AddWithValue("@SubTotal", d.SubTotal);

                            cmdDet.ExecuteNonQuery();
                        }
                    }

                    tx.Commit();

                    resultado.Exito = true;
                    resultado.Mensaje = "Venta registrada exitosamente";
                    resultado.IdGenerado = idVenta;
                    return resultado;
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    resultado.Exito = false;
                    resultado.Mensaje = "Error: " + ex.Message;
                    return resultado;
                }
            }
        }

        public static DataTable ObtenerVentaPorId(int idVenta)
        {
            using (SqlConnection con = new SqlConnection(Conexion.Cadena))
            {
                string query = @"
            SELECT 
    v.Id,
    v.FechaVenta,
    c.NombreCompleto AS Cliente,
    (u.Nombre + ' ' + u.Apellido) AS Usuario,
    t.Pago AS TipoPago,
    v.Total
FROM Venta v
INNER JOIN Cliente c ON c.Id = v.IdCliente
INNER JOIN Usuario u ON u.Id = v.IdUsuario
INNER JOIN TipoPago t ON t.Id = v.IdTipoPago
WHERE v.Id = @IdVenta
";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@IdVenta", idVenta);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
        }
        public static DataTable ObtenerDetallesVenta(int idVenta)
        {
            using (SqlConnection con = new SqlConnection(Conexion.Cadena))
            {
                string query = @"
           SELECT 
    d.Cantidad,
    p.NombreProducto AS Producto,
    d.PrecioUnitario,
    d.SubTotal
FROM DetalleVenta d
INNER JOIN Producto p ON p.Id = d.IdProducto
WHERE d.IdVenta = @IdVenta
";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@IdVenta", idVenta);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
        }

        public static DataTable ObtenerVentas()
        {
            using (SqlConnection conn = new SqlConnection(Conexion.Cadena))
            {
                conn.Open();

                string query = @"
            SELECT  
                v.Id AS N_Venta,
                c.NombreCompleto AS Cliente,
                u.Nombre + ' ' + u.Apellido AS Vendedor,
                tp.Pago AS TipoPago,
                v.FechaVenta,
                v.Total
            FROM Venta v
            INNER JOIN Cliente c ON c.Id = v.IdCliente
            INNER JOIN Usuario u ON u.Id = v.IdUsuario
            INNER JOIN TipoPago tp ON tp.Id = v.IdTipoPago
            ORDER BY v.Id DESC";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public static DataTable BuscarVentasPorCliente(string nombreCliente)
        {
            using (SqlConnection conn = new SqlConnection(Conexion.Cadena))
            {
                string query = @"SELECT V.Id, V.FechaVenta, V.Total, C.NombreCompleto AS Cliente, U.Nombre AS Usuario, T.Pago AS TipoPago
                         FROM Venta V
                         INNER JOIN Cliente C ON V.IdCliente = C.Id
                         INNER JOIN Usuario U ON V.IdUsuario = U.Id
                         INNER JOIN TipoPago T ON V.IdTipoPago = T.Id
                         WHERE C.NombreCompleto LIKE @nombre";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@nombre", "%" + nombreCliente + "%");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
        }


    }
}


