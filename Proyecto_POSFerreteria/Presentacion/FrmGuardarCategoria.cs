using Proyecto_POSFerreteria.Datos;
using Proyecto_POSFerreteria.Entidades;
using Proyecto_POSFerreteria.Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_POSFerreteria.Presentacion
{
    public partial class FrmGuardarCategoria : Form
    {
        public string Modo { get; set; }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        CategoriaProductoBLL bllc = new CategoriaProductoBLL();
        public FrmGuardarCategoria()
        {
            InitializeComponent();
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Seguro que deseas cancelar?",
                    "Confirmación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    MessageBox.Show(
                        "Debe ingresar un nombre para la categoría.",
                        "Validación",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }
                // Creamos objeto categoría
                CategoriaProducto c = new CategoriaProducto()
                {
                    Id = Id,
                    Nombre = txtNombre.Text.Trim(),
                    Descripcion = txtDescripcion.Text.Trim()
                };
                bllc.Guardar(c);

                MessageBox.Show(
                    Modo == "Nuevo"
                        ? "La categoría ha sido registrada correctamente."
                        : "Los cambios han sido guardados correctamente.",
                    "Operación exitosa",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1
                );
                Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(
                    "Error al interactuar con la base de datos.\n\nDetalles técnicos:\n" + ex.Message,
                    "Error SQL",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ocurrió un error inesperado:\n" + ex.Message,
                    "Error general",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
        

        private void FrmGuardarCategoria_Load(object sender, EventArgs e)
        {
            if (Modo == "Nuevo")
            {
                lblTitulo.Text = "AGREGAR NUEVA CATEGORÍA";
            }
            else
            {
                lblTitulo.Text = "MODIFICAR CATEGORÍA";
                // Cargar datos en controles
                txtNombre.Text = Nombre;
                txtDescripcion.Text = Descripcion;
            }
        }
    }
}
