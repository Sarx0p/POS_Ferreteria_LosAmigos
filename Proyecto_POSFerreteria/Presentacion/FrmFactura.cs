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
            CargarFactura();
           if (IdVenta <= 0)
            {
                MessageBox.Show("No se ha especificado una venta para mostrar la factura.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }

           if (IdVenta > 0) {
                CargarFactura();
            }

           if (IdVenta <= 0)
            {
                MessageBox.Show("No se ha especificado una venta para mostrar la factura.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }

           

        }

        private void CargarFactura()
        {
            // 1) OBTENER DATOS PRINCIPALES DE LA VENTA
            DataTable dtVenta = VentaDAL.ObtenerVentaPorId(IdVenta);

            if (dtVenta.Rows.Count == 0)
            {
                MessageBox.Show("No se encontró la venta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var v = dtVenta.Rows[0];

            txtNumeroVenta.Text = v["Id"].ToString();
            txtFechaVenta.Text = Convert.ToDateTime(v["FechaVenta"]).ToString("dd/MM/yyyy");
            txtCliente.Text = v["Cliente"].ToString();
            txtAtendidoPor.Text = v["Usuario"].ToString();
            txtTipoPago.Text = v["TipoPago"].ToString();
            txtTotal.Text = "$" + Convert.ToDecimal(v["Total"]).ToString("0.00");

            // 2) OBTENER DETALLES DE LA VENTA
            DataTable dtDetalles = VentaDAL.ObtenerDetallesVenta(IdVenta);

            dgvDetalleFactura.DataSource = dtDetalles;

            // Ajustar columnas
            dgvDetalleFactura.Columns["IdProducto"].Visible = false;
            dgvDetalleFactura.Columns["Producto"].Width = 200;
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
                    MessageBox.Show("No hay venta seleccionada para imprimir.",
                        "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string ruta = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    $"Factura_{IdVenta}.pdf");

                var document = new FacturaDocument(IdVenta);

                // MÉTODO CORRECTO (QuestPDF 2022+)
                document.GeneratePdf(ruta);

                MessageBox.Show("Factura generada correctamente:\n" + ruta,
                    "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);

                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = ruta,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al imprimir: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("¿Seguro que deseas cerrar la factura?",
                    "Confirmación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);
            this.Close();
        }
    }
    }
    
    


    



