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
        private void btnIniciodeSesion_Click_1(object sender, EventArgs e)
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

        



        private void pictureBox2_Click(object sender, EventArgs e)
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

        private void btnOlvidar_Click(object sender, EventArgs e)
        {
            FrmOlvidoSuContraseñacs frm = new FrmOlvidoSuContraseñacs();
            frm.ShowDialog();
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
