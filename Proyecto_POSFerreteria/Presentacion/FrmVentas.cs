using Proyecto_POSFerreteria.Datos;
using Proyecto_POSFerreteria.Datos.Datos_Hembert;
using Proyecto_POSFerreteria.Entidades.Clases_hembert;
using Proyecto_POSFerreteria.Negocio;
using Proyecto_POSFerreteria.Negocio.Hembert;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_POSFerreteria.Presentacion
{
    public partial class FrmVentas : Form

    {
        //MIRAR SI DA ERROR
        CategoriaProductoBLL bll = new CategoriaProductoBLL();

        int x,y;
        bool move = false;
        public FrmVentas()
        {
            InitializeComponent();
        }
        

        private void timer1_Tick(object sender, EventArgs e)
        {
           
        }

        private void FrmVentas_Load(object sender, EventArgs e)
        {
            cboCliente.DataSource = ClienteDAL.ObtenerClientes();
            cboCliente.DisplayMember = "NombreCompleto";
            cboCliente.ValueMember = "Id";

            cboTipoPago.DataSource = TipoPagoDAL.Listar();
            cboTipoPago.DisplayMember = "Pago";
            cboTipoPago.ValueMember = "Id";

            dtpFecha.Value = DateTime.Now;//obtiene la fecha de ahora 
            CargarProductos(string.Empty);                              // --- CONFIGURAR COLUMNAS DEL DETALLE --- 
            ConfigurarTablaDetalles();
        }

        private void ConfigurarTablaDetalles()
        { 
            dvgDetalles.Columns.Clear();
            DataGridViewTextBoxColumn colIdProd = new DataGridViewTextBoxColumn();
            colIdProd.Name = "IdProducto";
            colIdProd.HeaderText = "ID";
            colIdProd.Visible = false;
            dvgDetalles.Columns.Add(colIdProd);

            //Nombre Producto
            dvgDetalles.Columns.Add("NombreProducto", "Producto");

            //Cantidad
            DataGridViewTextBoxColumn colCant = new DataGridViewTextBoxColumn();
            colCant.Name = "Cantidad";
            colCant.HeaderText = "Cant.";
            dvgDetalles.Columns.Add(colCant);


            //Precio Unitario
            DataGridViewTextBoxColumn colPrecio = new DataGridViewTextBoxColumn();
            colPrecio.Name = "PrecioUnitario";
            colPrecio.HeaderText = "Precio Unitario";
            dvgDetalles.Columns.Add(colPrecio);

            //Subtotal
            DataGridViewTextBoxColumn colSubTotal = new DataGridViewTextBoxColumn();
            colSubTotal.Name = "SubTotal";
            colSubTotal.HeaderText = "Subtotal";
            dvgDetalles.Columns.Add(colSubTotal);

            //Asegurar Permisos de Edición
            dvgDetalles.ReadOnly = false;

            //Columnas que no se pueden editar
            dvgDetalles.Columns["IdProducto"].ReadOnly = true;
            dvgDetalles.Columns["NombreProducto"].ReadOnly = true;
            dvgDetalles.Columns["SubTotal"].ReadOnly = true;
            dvgDetalles.Columns["PrecioUnitario"].ReadOnly = true;
            //Unica columna editable
            dvgDetalles.Columns["Cantidad"].ReadOnly = false;



        }

        private void CargarProductos(string filtro)
        {
            //Obtener lista de productos desde la base de datos
            var tabla = ProductoDAL.Listar();
            //Aplicar filtro si es necesario
            if (!string.IsNullOrEmpty(filtro))
            {
                var dv = tabla.DefaultView;
                dv.RowFilter = $"NombreProducto LIKE '%{filtro}%'";
                dvgProducto.DataSource = dv;

            }
            else
            {
                dvgProducto.DataSource = tabla;
            }
            dvgProducto.Columns["Id"].Visible = false;
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void lblFecha_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            dvgDetalles.Rows.Clear();
            RecalcularTotal();
        }

        private void btnAgregarCompra_Click(object sender, EventArgs e)
        {
            if (dvgProducto.SelectedRows.Count == 0)
            {
                MessageBox.Show("SELECCIONA UN PRODUCTO PARA AGREGAR");
                return;
            }
            DataGridViewRow row = dvgProducto.SelectedRows[0];
            int idProducto = Convert.ToInt32(row.Cells["Id"].Value);
            string nombreProducto = row.Cells["NombreProducto"].Value.ToString();
            decimal precioUnitario = Convert.ToDecimal(row.Cells["Precio"].Value);

            //Catidad inicial 1
            int cantidad = 1;
            decimal subTotal = cantidad * precioUnitario;
            //Agregar fila al detalle
            dvgDetalles.Rows.Add(
                idProducto,
                nombreProducto,
                cantidad, 
                precioUnitario, 
                subTotal);
            RecalcularTotal(); //Despues se creara este metodo
        }

        private decimal ObtenerTotalVenta()
        {
            decimal total = 0;
            foreach (DataGridViewRow row in dvgDetalles.Rows)
                total += Convert.ToDecimal(row.Cells["SubTotal"].Value);
            return total;
        }
        private void LimpiarFormulario()
        {
            dvgDetalles.Rows.Clear();
            lblTotal.Text = "Total: $0.00";
            txtBuscarProducto.Clear();
            CargarProductos(string.Empty); // recarga lista completa 
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dvgDetalles.Rows.Count == 0)
                {
                    MessageBox.Show("La venta no tiene productos.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // --------------------------------------------------- 
                // 1) CREAR OBJETO VENTA 
                // --------------------------------------------------- 
                Venta venta = new Venta()
                {
                    FechaVenta = dtpFecha.Value,
                    Total = ObtenerTotalVenta(),
                    IdCliente = Convert.ToInt32(cboCliente.SelectedValue),
                    IdTipoPago = Convert.ToInt32(cboTipoPago.SelectedValue)
                };


                // --------------------------------------------------- 
                // 2) CREAR LISTA DE DETALLES 
                // --------------------------------------------------- 

                List<DetalleVenta> detalles = new List<DetalleVenta>();
                foreach (DataGridViewRow row in dvgDetalles.Rows)
                {
                    detalles.Add(new DetalleVenta()
                    {
                        IdProducto = Convert.ToInt32(row.Cells["IdProducto"].Value),
                        Cantidad = Convert.ToInt32(row.Cells["Cantidad"].Value),
                        PrecioUnitario = Convert.ToDecimal(row.Cells["PrecioUnitario"].Value),
                        SubTotal = Convert.ToDecimal(row.Cells["SubTotal"].Value)
                    });
                }
                var validacion = VentaBLL.ValidarVenta(venta, detalles);

                // Si la validación falla → mostramos error y salimos
                if (!validacion.Exito)
                {
                    MessageBox.Show(validacion.Mensaje, "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ================================
                // GUARDAR EN BASE DE DATOS (TRANSACCIÓN)
                // ================================
                var resultado = VentaDAL.RegistrarVenta(venta, detalles);

                if (resultado.Exito)
                {
                    MessageBox.Show(resultado.Mensaje, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarFormulario();
                }
                else
                {
                    MessageBox.Show(resultado.Mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message);
            }



        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
         if (MessageBox.Show("¿DESEAS CANCELAR ESTA VENTA?","CONFIRMACIÓN",
          MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                this.Close();   
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
           if (MessageBox.Show("¿DESEAS SALIR?", "CONFIRMACIÓN",
         MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void panel2_MouseUp(object sender, MouseEventArgs e)
        {
            move = false;
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            move = true;
            x = e.X;
            y = e.Y;
        }

        private void txtCliente_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbxTipoPago_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dgvProducto_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvDetalleVenta_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void lblTotal_Click(object sender, EventArgs e)
        {

        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            if (dvgDetalles.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una fila para quitar.");
                return;
            }
            dvgDetalles.Rows.RemoveAt(dvgDetalles.SelectedRows[0].Index);
            RecalcularTotal();
        }

        private void cbxCliente_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtCliente_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtBuscarProducto_TextChanged(object sender, EventArgs e)
        {
            dvgProducto.DataSource = bll.Buscar(txtBuscarProducto.Text);

        }

        private void dvgProducto_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnAgregarCompra_Click(sender, e);
        }

        //Funcion 
        private void RecalcularTotal()
        {
            decimal total = 0;
            foreach (DataGridViewRow row in dvgDetalles.Rows)
            {
                total += Convert.ToDecimal(row.Cells["SubTotal"].Value);
            }
            lblTotal.Text = "Total: $" + total.ToString("0.00");
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            FrmCliente frm = new FrmCliente();
            frm.ShowDialog();
        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (move) {
                this.SetDesktopLocation(MousePosition.X-x, MousePosition.Y-y);
            }
        }
    }
}
