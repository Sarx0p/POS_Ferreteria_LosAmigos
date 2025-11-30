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
    public partial class FrmInventario : Form
    {
        int x, y;
        bool move;
        public FrmInventario()
        {
            InitializeComponent();
        }

        private void txtUsernName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtClave_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
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

        private void btnAgregarCategoria_Click(object sender, EventArgs e)
        {
            FrmGuardarCategoria frm = new FrmGuardarCategoria();
            frm.ShowDialog();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            FrmEditarCategoria frm = new FrmEditarCategoria();
            frm.ShowDialog();
        }

        private void btnElimiarCategoria_Click(object sender, EventArgs e)
        {
            FrmEliminarCategoria frm = new FrmEliminarCategoria();
            frm.ShowDialog();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
