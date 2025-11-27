using Proyecto_POSFerreteria.Entidades;
using Proyecto_POSFerreteria.Negocio;
using Proyecto_POSFerreteria.Presentacion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_POSFerreteria
{
    public partial class FrmLogin : Form
    {
        private FrmMenuPrincipal menu;

        public FrmLogin()
        {
            InitializeComponent();
          
        }

        private void Form_Load(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void btnIniciodeSesion_Click(object sender, EventArgs e)
        {
            try
            {
                UsuarioBLL bll = new UsuarioBLL();

                Usuario u = bll.Login(txtUsuario.Text, txtClave.Text);

                // Guardar datos globales
                SesionActual.NombreUsuario = u.Nombre;
                SesionActual.Rol = u.Rol;
                SesionActual.IdUsuario = u.IdUsuario;

                // Enviar al menú
                FrmMenuPrincipal frm = new FrmMenuPrincipal();
                frm.Show();
                //this.Hide();
            }
           catch (Exception ex)
         {
           //   MessageBox.Show(ex.Message);
           //}
        //}
        }
        }

        private void txtContraseña_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
