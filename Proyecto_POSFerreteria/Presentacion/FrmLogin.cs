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

                // Verificar si el usuario existe
                if (u != null)
                {
                    // Guardar datos globales
                    SesionActual.NombreUsuario = u.Nombre;
                    SesionActual.Rol = u.Rol;
                    SesionActual.IdUsuario = u.IdUsuario;

                    // Entrar al menú
                    FrmMenuPrincipal frm = new FrmMenuPrincipal();
                    frm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Usuario o Clave incorrectos.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message);
            }
        }




        private void txtContraseña_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
