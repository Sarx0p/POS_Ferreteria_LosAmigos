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
    public partial class FrmEliminarCategoria : Form
    {
        int x, y; 
        bool move = false;

        public int Id { get; set; }
        public string NombreCategoria{ get; set; }
        public string Descripcion { get; set; }

        CategoriaProductoBLL bllc = new CategoriaProductoBLL();

        public FrmEliminarCategoria()

        {
            InitializeComponent();
        }

        private void FrmEliminarCategoria_Load(object sender, EventArgs e)
        {
            txtNombre.Text = NombreCategoria;
            txtDescripcion.Text = Descripcion;
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

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                // Confirmación previa
                var r = MessageBox.Show(
                    $"¿Está seguro que desea eliminar la categoría. " +
                    "Esta acción no se puede deshacer.",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2
                );
                if (r == DialogResult.No)
                    return;

                bllc.Eliminar(Id);

                MessageBox.Show(
                    "La categoría ha sido eliminada correctamente.",
                    "Operación exitosa",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                Close();
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                {
                    MessageBox.Show(
                        "No se puede eliminar esta categoría porque está asociada a otros registros.\n" +
                        "Actualice o elimine esos registros primero.",
                        "Eliminación no permitida",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                // Otros errores SQL
                MessageBox.Show(
                    "Ocurrió un error al intentar eliminar la categoría.\n\nDetalles técnicos:\n" + ex.Message,
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

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
