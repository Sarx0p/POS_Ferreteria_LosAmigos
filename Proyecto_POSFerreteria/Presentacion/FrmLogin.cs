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

        //Es para el panel frontral     DISEÑO
        int x, y;
        bool move = false;
        
        public FrmLogin()
        {
            InitializeComponent();

        }

        private void Form_Load(object sender, EventArgs e)
        {

        }
       






        // CODIGO DE DISEÑOS

       

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            move = false;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            move = true;
            x = e.X;
            y = e.Y;
        }

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            move = false;
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            move = true;
            x = e.X;
            y = e.Y;
        }

        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            if (move)
            {
                this.SetDesktopLocation(MousePosition.X - x, MousePosition.Y - y);
            }
        }

  

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnIniciodeSesion_Click(object sender, EventArgs e)
        {
           try
            {
                string user = txtUsuario.Text.Trim();
                string pass = txtClave.Text; // texto plano que el usuario escribió

                var u = UsuarioBLL.Login(user, pass);
                if (u == null)
                {
                    MessageBox.Show("Usuario o contraseña incorrectos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // login OK: guarda sesión, abre form principal
                // ejemplo:
                SesionActual.IdUsuario = u.IdUsuario;
                SesionActual.Username = u.Username;
                SesionActual.Rol = u.Rol;

                var frm = new FrmMenuPrincipal();
                frm.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en login: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (move)
            {
                this.SetDesktopLocation(MousePosition.X - x, MousePosition.Y - y);
            }
        }

        

    }
}
