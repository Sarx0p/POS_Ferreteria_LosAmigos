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

        private DataRow Venta;
        private DataTable Detalles;

        public FacturaDocument(int idVenta)
        {
            IdVenta = idVenta;

            // Estos sí existen en tu DAL
            DataTable v = VentaDAL.ObtenerVentaPorId(IdVenta);
            DataTable d = VentaDAL.ObtenerDetallesVenta(IdVenta);

            if (v == null || v.Rows.Count == 0)
                throw new Exception("No se encontró la venta.");

            Venta = v.Rows[0];
            Detalles = d;
        }

        // ======================================================
        // GENERAR PDF (Modelo universal QuestPDF)
        // ======================================================
        public void GeneratePdf(string filePath)
        {
            var doc = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(35);

                    page.Header().Element(Encabezado);
                    page.Content().Element(Contenido);
                    page.Footer().Element(PiePagina);
                });
            });

            doc.GeneratePdf(filePath);
        }

        // ======================================================
        // ENCABEZADO
        // ======================================================
        private void Encabezado(IContainer container)
        {
            container.Row(row =>
            {
                row.RelativeItem().Column(col =>
                {
                    col.Item().Text("FERRETERÍA EL AMIGO")
                        .FontSize(20).Bold().FontColor(Colors.Blue.Medium);

                    col.Item().Text("Factura de Venta").FontSize(14);
                });

                row.RelativeItem().AlignRight().Column(col =>
                {
                    col.Item().Text($"Factura Nº: {Venta["Id"]}").FontSize(14).Bold();
                    col.Item().Text($"Fecha: {Convert.ToDateTime(Venta["FechaVenta"]).ToString("dd/MM/yyyy")}");
                });
            });
        }

        // ======================================================
        // CONTENIDO
        // ======================================================
        private void Contenido(IContainer container)
        {
            container.PaddingVertical(10).Column(col =>
            {
                col.Item().Text($"Cliente: {Venta["Cliente"]}").FontSize(12);
                col.Item().Text($"Atendido por: {Venta["Usuario"]}");
                col.Item().Text($"Tipo de pago: {Venta["TipoPago"]}");
                col.Item().Text($"Total: ${Convert.ToDecimal(Venta["Total"]):0.00}")
                    .FontSize(14).Bold();

                col.Item().PaddingTop(10).Element(TablaDetalles);
            });
        }

        // ======================================================
        // TABLA DE DETALLES
        // ======================================================
        private void TablaDetalles(IContainer container)
        {
            container.Table(table =>
            {
                table.ColumnsDefinition(cols =>
                {
                    cols.ConstantColumn(40);
                    cols.RelativeColumn(3);
                    cols.ConstantColumn(60);
                    cols.ConstantColumn(80);
                    cols.ConstantColumn(80);
                });

                table.Header(header =>
                {
                    header.Cell().BorderBottom(1).Padding(5).Text("ID").Bold();
                    header.Cell().BorderBottom(1).Padding(5).Text("Producto").Bold();
                    header.Cell().BorderBottom(1).Padding(5).Text("Cant.").Bold();
                    header.Cell().BorderBottom(1).Padding(5).Text("Precio").Bold();
                    header.Cell().BorderBottom(1).Padding(5).Text("Subtotal").Bold();
                });

                foreach (DataRow d in Detalles.Rows)
                {
                    table.Cell().Padding(5).Text(d["IdProducto"].ToString());
                    table.Cell().Padding(5).Text(d["Producto"].ToString());
                    table.Cell().Padding(5).Text(d["Cantidad"].ToString());
                    table.Cell().Padding(5).Text($"${Convert.ToDecimal(d["Precio"]):0.00}");
                    table.Cell().Padding(5).Text($"${Convert.ToDecimal(d["Subtotal"]):0.00}");
                }
            });
        }

        // ======================================================
        // PIE DE PÁGINA
        // ======================================================
        private void PiePagina(IContainer container)
        {
            container.AlignCenter().Text("Gracias por su compra").FontSize(12);
        }
    }
}





    

