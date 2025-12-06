using Proyecto_POSFerreteria.Datos;
using Proyecto_POSFerreteria.Datos.Datos_Hembert;
using Proyecto_POSFerreteria.Entidades;
using Proyecto_POSFerreteria.Entidades.Clases_hembert;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Proyecto_POSFerreteria.Presentacion
{


    public partial class FrmFactura : Form
    {
        public int IdVenta { get; set; }

        public FrmFactura()
        {
            InitializeComponent();

           
        }
        


        private void FrmFactura_Load(object sender, EventArgs e)
        {
            InitializeComponent();
            IdVenta = IdVenta;
            CargarFactura();   // Solo carga datos en los TextBox y DataGridView



        }

        private void CargarFactura()
        {
            try
            {
                if (IdVenta <= 0) return;

                DataTable dtVenta = VentaDAL.ObtenerVentaPorId(IdVenta);
                DataTable dtDetalle = VentaDAL.ObtenerDetallesVenta(IdVenta);

                if (dtVenta == null || dtVenta.Rows.Count == 0)
                    throw new Exception("No se encontró la venta.");

                var v = dtVenta.Rows[0];

                //=== llenamos controles usando Buscador de controles ===
                (BuscarControl("txtCliente") as TextBox).Text = v["Cliente"].ToString();
                (BuscarControl("txtAtendido") as TextBox).Text = v["Usuario"].ToString();
                (BuscarControl("txtTipoPago") as TextBox).Text = v["TipoPago"].ToString();
                (BuscarControl("txtTotal") as TextBox).Text = Convert.ToDecimal(v["Total"]).ToString("0.00");

                //=== Cargar DataGridView ===
                DataGridView dgv = BuscarControl("dgvDetallesFactura") as DataGridView;
                if (dgv != null)
                {
                    dgv.Rows.Clear();
                    foreach (DataRow r in dtDetalle.Rows)
                    {
                        dgv.Rows.Add(
                            r["Producto"].ToString(),
                            r["Cantidad"].ToString(),
                            Convert.ToDecimal(r["PrecioUnitario"]).ToString("0.00"),
                            Convert.ToDecimal(r["SubTotal"]).ToString("0.00")
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cargando datos de la factura: " + ex.Message);
            }
        }


        private Control BuscarControl(string nombre, Control parent = null)
        {
            parent = parent ?? this;

            foreach (Control c in parent.Controls)
            {
                if (c.Name == nombre)
                    return c;

                Control hijo = BuscarControl(nombre, c);
                if (hijo != null)
                    return hijo;
            }
            return null;
        }



        private void lblTitulo_Click(object sender, EventArgs e)
        {

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

            try
            {
                if (IdVenta <= 0)
                {
                    MessageBox.Show("No hay venta seleccionada para imprimir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "PDF files (*.pdf)|*.pdf";
                    sfd.FileName = $"Factura_Venta_{IdVenta}.pdf";

                    if (sfd.ShowDialog() != DialogResult.OK) return;

                    string ruta = sfd.FileName;
                    GenerarPdfFactura(ruta);

                    MessageBox.Show("Factura generada correctamente:\n" + ruta, "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Abrir automáticamente el archivo si quiere
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = ruta,
                        UseShellExecute = true
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar factura: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        

        private void GenerarPdfFactura(string ruta)
        {
            try
            {
                // Cargar datos nuevamente (o puedes usar variables ya cargadas si las guardaste)
                DataTable dtVenta = VentaDAL.ObtenerVentaPorId(IdVenta);
                DataTable dtDetalle = VentaDAL.ObtenerDetallesVenta(IdVenta);

                if (dtVenta == null || dtVenta.Rows.Count == 0)
                    throw new Exception("No se encontró la venta.");

                var v = dtVenta.Rows[0];

                var fecha = Convert.ToDateTime(v["FechaVenta"]).ToString("dd/MM/yyyy");
                var cliente = v["Cliente"].ToString();
                var usuario = v["Usuario"].ToString();
                var tipoPago = v["TipoPago"].ToString();
                var total = Convert.ToDecimal(v["Total"]).ToString("0.00");

                Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Margin(30);
                        page.Header().Text("FERRETERÍA EL BUEN AMIGO").FontSize(20).SemiBold().AlignCenter();

                        page.Content().Column(col =>
                        {
                            col.Spacing(10);

                            col.Item().Text($"Factura de Venta Nº {IdVenta}").FontSize(14).SemiBold();
                            col.Item().Text($"Fecha: {fecha}");
                            col.Item().Text($"Cliente: {cliente}");
                            col.Item().Text($"Atendido por: {usuario}");
                            col.Item().Text($"Tipo de Pago: {tipoPago}");

                            col.Item().LineHorizontal(1);

                            col.Item().Table(table =>
                            {
                                table.ColumnsDefinition(cols =>
                                {
                                    cols.ConstantColumn(60);
                                    cols.RelativeColumn(2);
                                    cols.RelativeColumn(1);
                                    cols.RelativeColumn(1);
                                });

                                table.Header(header =>
                                {
                                    header.Cell().Text("Cant.").SemiBold();
                                    header.Cell().Text("Producto").SemiBold();
                                    header.Cell().Text("Precio").SemiBold();
                                    header.Cell().Text("Subtotal").SemiBold();
                                });

                                foreach (DataRow item in dtDetalle.Rows)
                                {
                                    table.Cell().Text(item["Cantidad"].ToString());
                                    table.Cell().Text(item["Producto"].ToString());
                                    table.Cell().Text("$" + Convert.ToDecimal(item["PrecioUnitario"]).ToString("0.00"));
                                    table.Cell().Text("$" + Convert.ToDecimal(item["SubTotal"]).ToString("0.00"));
                                }
                            });

                            col.Item().LineHorizontal(1);

                            col.Item().AlignRight().Text($"TOTAL: ${total}")
                                .FontSize(16).SemiBold();
                        });

                        page.Footer().AlignCenter().Text("Gracias por su compra").FontSize(12);
                    });
                })
                .GeneratePdf(ruta);
            }
            catch
            {
                throw; // deja que el llamador muestre el MessageBox
            }
        }

    }
}

    
    


    



