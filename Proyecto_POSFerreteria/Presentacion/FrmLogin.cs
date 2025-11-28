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

        private void label4_Click(object sender, EventArgs e)
        {

        }

        
       

        private void btnSesion_Click(object sender, EventArgs e)
        {
            
            {
                UsuarioBLL bll = new UsuarioBLL();
                Usuario u = bll.Login(txtUsuario.Text, txtClave.Text);

                // Guardar la sesión
                SesionActual.IdUsuario = u.Id;
                SesionActual.NombreUsuario = u.Nombre;
                SesionActual.Rol = u.Rol;

                // Ir al menú
                FrmMenuPrincipal menu = new FrmMenuPrincipal();
                menu.Show();
            }
    }
        }
        }