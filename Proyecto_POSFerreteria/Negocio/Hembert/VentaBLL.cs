using Proyecto_POSFerreteria.Entidades.Clases_hembert;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proyecto_POSFerreteria.Entidades;
using Proyecto_POSFerreteria.Datos.Datos_Hembert;

namespace Proyecto_POSFerreteria.Negocio.Hembert
{
    public class VentaBLL
    {
        public static RespuestaOperacion ValidarVenta(Venta venta, List<DetalleVenta> detalles)
        {
            // 1) Validar existencia del objeto Venta 
            if (venta == null)
                return new RespuestaOperacion { Exito = false, Mensaje = "Venta no válida." };
            // 2) Validar cliente 
            if (venta.IdCliente <= 0)
                return new RespuestaOperacion { Exito = false, Mensaje = "Debe seleccionar un cliente." };
            // 3) Validar tipo de pago 
            if (venta.IdTipoPago <= 0)
                return new RespuestaOperacion { Exito = false, Mensaje = "Debe seleccionar un tipo de pago." };
            // 4) Validar detalles 
            if (detalles == null || detalles.Count == 0)
            {
                return new RespuestaOperacion
                {
                    Exito = false,
                    Mensaje = "La venta debe contener al menos un producto."
                };
            }

            if (venta.Total <= 0)
                return new RespuestaOperacion { Exito = false, Mensaje = "El total de la venta debe ser mayor a cero." };

            // Si todas las validaciones pasan, retornar éxito
            foreach (var detalle in detalles)
            {
                if (detalle.Cantidad <= 0)
                {
                    return new RespuestaOperacion
                    {
                        Exito = false,
                        Mensaje = "La cantidad de cada producto debe ser mayor a cero."
                    };
                }
                if (detalle.PrecioUnitario <= 0)
                {
                    return new RespuestaOperacion
                    {
                        Exito = false,
                        Mensaje = "El precio unitario de cada producto debe ser mayor a cero."
                    };
                }
                if (detalle.SubTotal != detalle.Cantidad * detalle.PrecioUnitario)
                    return new RespuestaOperacion
                    {
                        Exito = false,
                        Mensaje = "El subtotal de cada detalle debe ser igual a cantidad por precio unitario."
                    };

                //Falta producto DAL para obtener stock

                int stockActual = ProductoDAL.ObtenerStockProducto(detalle.IdProducto);
                if (stockActual < detalle.Cantidad)
                {
                    return new RespuestaOperacion
                    {
                        Exito = false,
                        Mensaje = $"No hay suficiente stock para el producto con ID {detalle.IdProducto}."
                    };
                }
            }
            return new RespuestaOperacion { Exito = true, Mensaje = "Validación exitosa." };

        }
    }
}
