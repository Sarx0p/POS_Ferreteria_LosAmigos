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
    public partial class FrmEditarCategoria : Form
    {
        int x,y;
        bool move = false;

        public FrmEditarCategoria()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

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

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (move)
            {
                this.SetDesktopLocation(MousePosition.X - x, MousePosition.Y - y);
            }
        }
    }
}
