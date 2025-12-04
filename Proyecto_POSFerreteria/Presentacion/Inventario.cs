using Proyecto_POSFerreteria.Datos;
using Proyecto_POSFerreteria.Entidades;
using Proyecto_POSFerreteria.Negocio;
using Proyecto_POSFerreteria.Negocio.Hembert;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Proyecto_POSFerreteria.Presentacion
{
    public partial class FrmInventario : Form
    {
        int x, y;
        bool move;
        int ProductoId = 0;
        int CategoriaId = 0;

        ProductoBLL bll = new ProductoBLL();
        CategoriaProductoBLL bllc = new CategoriaProductoBLL();
        string Modo = "Nuevo"; //Nuevo o Editar

        public FrmInventario()
        {
            InitializeComponent();

        }

     
        

        // PRODUCTOOS

        private void FrmInventario_Load(object sender, EventArgs e)
        {
            CargarCategorias();
            CargarProductosEnGrid();
            //Categorias pe 
            CargarDatosc();
            HabilitarBotones();
        }

        private void CargarCategorias()
        {
            CategoriaDAL dal = new CategoriaDAL();
            DataTable dt = dal.Listar(); // debe traer Id, NombreCategoria

            cbxCategoriaProducto.DataSource = dt;
            cbxCategoriaProducto.DisplayMember = "NombreCategoria";
            cbxCategoriaProducto.ValueMember = "Id";
            cbxCategoriaProducto.SelectedIndex = -1;
        }


        private void CargarProductosEnGrid(string filtro = "")
        {
            DataTable dt = bll.ListarParaGrid();
            if (dt == null)

            {
                dgvProductos.DataSource = null;
                return;
            }

            if (string.IsNullOrWhiteSpace(filtro))
                dgvProductos.DataSource = dt;
            else
            {
                string q = filtro.Replace("'", "''");
                DataView dv = dt.DefaultView;
                dv.RowFilter = $"NombreProducto LIKE '%{q}%' OR Categoria LIKE '%{q}%'";
                dgvProductos.DataSource = dv;
            }

            if (dgvProductos.Columns.Contains("Id"))
                dgvProductos.Columns["Id"].Visible = true;



        }


        private void dgvProductos_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvProductos.SelectedRows.Count == 0)
                return;

            var row = dgvProductos.SelectedRows[0];

            ProductoId = Convert.ToInt32(row.Cells["Id"].Value);
            txtIdProducto.Text = ProductoId.ToString();

            txtNombre.Text = row.Cells["NombreProducto"].Value.ToString();
            txtPrecio.Text = row.Cells["Precio"].Value.ToString();
            txtStock.Text = row.Cells["Stock"].Value.ToString();
            chkEstado.Checked = Convert.ToBoolean(row.Cells["Estado"].Value);

            // Categoría
            cbxCategoriaProducto.SelectedIndex =
                cbxCategoriaProducto.FindStringExact(row.Cells["Categoria"].Value.ToString());
        }







        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Producto p = new Producto()
                {
                    Id = ProductoId,
                    NombreProducto = txtNombre.Text.Trim(),
                    Precio = decimal.TryParse(txtPrecio.Text.Trim(), out decimal pr) ? pr : 0,
                    Stock = int.TryParse(txtStock.Text.Trim(), out int st) ? st : 0,
                    IdCategoriaProducto = cbxCategoriaProducto.SelectedValue == null ? 0 : Convert.ToInt32(cbxCategoriaProducto.SelectedValue),
                    Estado = chkEstado.Checked
                };

                if (p.Id == 0)
                {
                    int id = bll.Insertar(p); // Insertar devuelve Id
                    MessageBox.Show("Producto guardado. ID: " + id, "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    bool ok = bll.Actualizar(p);
                    MessageBox.Show(ok ? "Producto actualizado." : "No se actualizó.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                CargarProductosEnGrid();
                Limpiar(); // usa tu método Limpiar actual
                ProductoId = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }






        private void Limpiar()
        {
            ProductoId = 0;
            txtIdProducto.Text = "";
            txtNombre.Text = "";
            txtPrecio.Text = "";
            txtStock.Text = "";
            chkEstado.Checked = false;
            cbxCategoriaProducto.SelectedIndex = -1;
            dgvProductos.ClearSelection();

        }

        // Event handlers de movimiento/otros
        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            move = true;
            x = e.X; y = e.Y;
        }

        private void panel2_MouseUp(object sender, MouseEventArgs e)
        {
            move = false;
        }



        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CargarProductosEnGrid(txtBuscar.Text.Trim());
            }
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtIdProducto_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (ProductoId == 0)
            {
                MessageBox.Show("Seleccione un producto para eliminar.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("¿Está seguro de eliminar el producto?", "Confirmación",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                bool ok = bll.Eliminar(ProductoId);

                if (ok)
                {
                    MessageBox.Show("Producto eliminado correctamente.", "Éxito");
                    CargarProductosEnGrid();
                    Limpiar();
                    ProductoId = 0;
                }
                else
                {
                    MessageBox.Show("No se encontró el producto para eliminar.", "Aviso");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar: " + ex.Message);
            }

        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (move)
            {
                this.SetDesktopLocation(MousePosition.X - x, MousePosition.Y - y);
            }
        }


        //Categorias


        void HabilitarBotones()
        {
            btnModificar.Enabled = false;
            btnElimiarCategoria.Enabled = false;
            dgvCategorias.ClearSelection();
            dgvCategorias.SelectionChanged += (s, e) =>
            {
                bool filaSeleccionada = dgvCategorias.SelectedRows.Count > 0;
                btnModificar.Enabled = filaSeleccionada;
                btnElimiarCategoria.Enabled = filaSeleccionada;
            };
        }

       

        private void btnAgregarCategoria_Click_1(object sender, EventArgs e)
        {
            FrmGuardarCategoria frm = new FrmGuardarCategoria(); //Aca dará error hasta que construyamos el Formulario llamado  FrmCategoriaGestion

            // MODO CREAR NUEVA CATEGORIA
            frm.Modo = "Nuevo"; //definimos por defecto que sea “nuevo”
            frm.Id = 0; //Guardara el Id que traigamos  del FrmCategoriaGestion

            frm.ShowDialog();  // Abrir como modal
            CargarDatosc();     // Refrescar al cerrar
            CargarCategorias();
        }

        private void btnModificar_Click_1(object sender, EventArgs e)
        {
            if (CategoriaId == 0)
            {
                MessageBox.Show("Seleccione una categoría",
                   "Información",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Information);
                return;
            }
            FrmGuardarCategoria frm = new FrmGuardarCategoria();
            // MODO EDITAR
            frm.Modo = "Editar";
            frm.Id = CategoriaId;

            // Pasar información desde el DGV
            frm.Nombre = dgvCategorias.CurrentRow.Cells["NombreCategoria"].Value.ToString();
            frm.Descripcion = dgvCategorias.CurrentRow.Cells["Descripcion"].Value.ToString();

            frm.ShowDialog();
            CargarDatosc();
            CargarCategorias();
        }

        private void btnElimiarCategoria_Click_1(object sender, EventArgs e)
        {
           if (CategoriaId == 0)
            {
                MessageBox.Show("Seleccione una categoría",
                   "Información",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Information);
                return;

            }
            // Abrir formulario de eliminación
            FrmEliminarCategoria frm = new FrmEliminarCategoria();

            frm.Id = CategoriaId;
            frm.NombreCategoria= dgvCategorias.CurrentRow.Cells["NombreCategoria"].Value.ToString();
            frm.Descripcion = dgvCategorias.CurrentRow.Cells["Descripcion"].Value.ToString();

            frm.ShowDialog();
            CargarDatosc();
            CargarCategorias();

        }

        private void txtBuscarCategoria_TextChanged(object sender, EventArgs e)
        {
            dgvCategorias.DataSource = bllc.Buscar(txtBuscarCategoria.Text);
        }

        private void dgvCategorias_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                CategoriaId = Convert.ToInt32(dgvCategorias.Rows[e.RowIndex].Cells["Id"].Value);
            }

        }

        private void cbxCategoriaProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        void CargarDatosc()
        {
            dgvCategorias.DataSource = bllc.Listar();
            dgvCategorias.ClearSelection();
            CategoriaId = 0;   // Reiniciar ID seleccionado

        }




    }
}
