using Proyecto_POSFerreteria.Entidades.Clases_hembert;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_POSFerreteria.Datos.Datos_Hembert
{
    public class VentaDAL
    {
        public static (bool Exito, string Mensaje) RegistrarVenta(Venta venta, List<DetalleVenta> detalles)
        {
            using (SqlConnection con = new SqlConnection(Conexion.Cadena))
            {
                con.Open();
                SqlTransaction tx = con.BeginTransaction();
                try
                {
                    string sqlVenta = @" INSERT INTO Venta (FechaVenta, Total, IdCliente, IdUsuario, IdTipoPago)
                                         VALUES (@FechaVenta, @Total, @IdCliente, @IdUsuario, @IdTipoPago)
                                         SELECT SCOPE_IDENTITY();";
                    using (SqlCommand cmd = new SqlCommand(sqlVenta, con, tx))
                    {
                        cmd.Parameters.AddWithValue("@FechaVenta", venta.FechaVenta);
                        cmd.Parameters.AddWithValue("@Total", venta.Total);
                        cmd.Parameters.AddWithValue("@IdCliente", venta.IdCliente);
                        cmd.Parameters.AddWithValue("@IdUsuario", venta.IdUsuario);
                        cmd.Parameters.AddWithValue("@IdTipoPago", venta.IdTipoPago);
                        int idVenta = Convert.ToInt32(cmd.ExecuteScalar());
                        string sqlDetalle = @" INSERT INTO DetalleVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario)
                                              VALUES (@IdVenta, @IdProducto, @Cantidad, @PrecioUnitario);";
                        var acumulador = new Dictionary<int, int>();
                        foreach (var detalle in detalles)
                        {
                            using (SqlCommand cmdDetalle = new SqlCommand(sqlDetalle, con, tx))
                            {
                                cmdDetalle.Parameters.AddWithValue("@IdVenta", idVenta);
                                cmdDetalle.Parameters.AddWithValue("@IdProducto", detalle.IdProducto);
                                cmdDetalle.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);
                                cmdDetalle.Parameters.AddWithValue("@PrecioUnitario", detalle.PrecioUnitario);
                                cmdDetalle.ExecuteNonQuery();
                            }
                           if (acumulador.ContainsKey(detalle.IdProducto))
                                acumulador[detalle.IdProducto] = 0;
                                acumulador[detalle.IdProducto] += detalle.Cantidad;
                        }
                        string sqlStock = @" UPDATE Producto SET Stock = Stock - @Cantidad WHERE Id = @IdProducto AND Stock >= @Cantidad;";

                        foreach (var item in acumulador)
                        {
                            using (SqlCommand cmdStock = new SqlCommand(sqlStock, con, tx))
                            {
                                cmdStock.Parameters.AddWithValue("@Cantidad", item.Value);
                                cmdStock.Parameters.AddWithValue("@IdProducto", item.Key);
                                int filasAfectadas = cmdStock.ExecuteNonQuery();
                                if (filasAfectadas == 0)
                                {
                                    tx.Rollback();
                                    return (false, $"No hay suficiente stock para el producto con Id {item.Key}.");
                                }
                            }
                        }
                        tx.Commit();
                        return (true, "Venta registrada exitosamente." + venta.Id);

                    }
                    
                }
                   catch (Exception ex)
                {
                    tx.Rollback();
                    return (false, "Error al registrar la venta: " + ex.Message);
                }

            }
        }

    } 
}
