using Proyecto_POSFerreteria.Negocio;
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
    public partial class FrmMenuPrincipal : Form
    {
        public FrmMenuPrincipal()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FrmMenuPrincipal_Load(object sender, EventArgs e)
        {
            lblUsuario.Text = "Usuario: " + SesionActual.NombreUsuario;
            lblRol.Text = "Rol: " + SesionActual.Rol;

            if (SesionActual.Rol == "Empleado")
            {
                btnUsuario.Visible = false;
                btnReportes.Visible = false;
            }

            if (SesionActual.Rol == "Empleado")
            {
                // Lo que el empleado NO debe ver:
                btnUsuario.Visible = false;
                btnReportes.Visible = false;
                btnConfig.Visible = false;

                // Lo que debe ver:
                btnVentas.Visible = true;
                btnProductos.Visible = true;
            }
            else if (SesionActual.Rol == "Administrador")
            {
                // El administrador ve toditom, no quitamos nada
            }
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            // Cerrar este formulario (el menú)
            this.Hide();

            // Volver a mostrar el login
            FrmLogin login = new FrmLogin();
            login.Show();
        }
    }
}
