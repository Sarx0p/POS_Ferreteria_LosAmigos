using Proyecto_POSFerreteria.Datos.Datos_Hembert;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using QuestPDF.Helpers;
using System.Data;
using System;


namespace Proyecto_POSFerreteria.Entidades.Clases_hembert
{
    // Clase final: incluye Compose (para Document.Create) y GeneratePdf de instancia.
    public class FacturaDocument
    {
        private readonly int IdVenta;

        public FacturaDocument(int idVenta)
        {
            IdVenta = idVenta;
        }

        // Método de instancia seguro que siempre funciona:
        // construye un IDocument a partir del Compose de esta instancia y lo genera.
        public void GeneratePdf(string filePath)
        {
            // Document.Create crea un IDocument usando el lambda que llama a Compose.
            // Luego llamamos a la extensión GeneratePdf sobre ese IDocument.
            var doc = Document.Create(container => Compose(container));
            doc.GeneratePdf(filePath);
        }

        // Compose mantiene tu diseño; lo usamos desde Document.Create(...)
        public void Compose(IDocumentContainer container)
        {
            // OBTENER DATOS DE VENTA
            DataTable dtVenta = VentaDAL.ObtenerVentaPorId(IdVenta);
            DataTable dtDetalle = VentaDAL.ObtenerDetallesVenta(IdVenta);

            if (dtVenta == null || dtVenta.Rows.Count == 0)
                throw new Exception("No se encontró la venta.");

            var v = dtVenta.Rows[0];

            container.Page(page =>
            {
                page.Margin(35);

                page.Header().BorderBottom(1).PaddingBottom(10).Row(row =>
                {
                    row.RelativeItem().Column(col =>
                    {
                        col.Item().Text("FERRETERÍA EL AMIGO").FontSize(20).Bold();
                        col.Item().Text("Factura de venta").FontSize(14);
                    });

                    row.RelativeItem().AlignRight().Column(col =>
                    {
                        col.Item().Text($"Factura Nº: {v["Id"]}").FontSize(14).Bold();
                        col.Item().Text($"Fecha: {Convert.ToDateTime(v["FechaVenta"]).ToString("dd/MM/yyyy")}");
                    });
                });

                // DATOS DEL CLIENTE Y DATOS GENERALES
                page.Content().PaddingVertical(10).Column(col =>
                {
                    col.Item().Text($"Cliente: {v["Cliente"]}").FontSize(12);
                    col.Item().Text($"Atendido por: {v["Usuario"]}");
                    col.Item().Text($"Tipo de pago: {v["TipoPago"]}");
                    col.Item().Text($"Total: ${Convert.ToDecimal(v["Total"]):0.00}")
                        .FontSize(14).Bold();
                });

                // TABLA DE DETALLES
                page.Content().PaddingTop(15).Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(40);   // IdProducto
                        columns.RelativeColumn(3);   // Producto
                        columns.ConstantColumn(60);  // Cantidad
                        columns.ConstantColumn(70);  // Precio
                        columns.ConstantColumn(70);  // Subtotal
                    });

                    // Encabezados
                    table.Header(header =>
                    {
                        header.Cell().BorderBottom(1).Text("ID").Bold();
                        header.Cell().BorderBottom(1).Text("Producto").Bold();
                        header.Cell().BorderBottom(1).Text("Cant.").Bold();
                        header.Cell().BorderBottom(1).Text("Precio").Bold();
                        header.Cell().BorderBottom(1).Text("Subtotal").Bold();
                    });

                    // Filas
                    if (dtDetalle != null)
                    {
                        foreach (DataRow d in dtDetalle.Rows)
                        {
                            table.Cell().Text(d["IdProducto"].ToString());
                            table.Cell().Text(d["Producto"].ToString());
                            table.Cell().Text(d["Cantidad"].ToString());
                            table.Cell().Text($"${Convert.ToDecimal(d["Precio"]):0.00}");
                            table.Cell().Text($"${Convert.ToDecimal(d["Subtotal"]):0.00}");
                        }
                    }
                });

                // Footer simple
                page.Footer().AlignCenter().Text(t =>
                {
                    t.Span("Gracias por su compra").FontSize(12);
                });
            });
        }
    }
}


    

