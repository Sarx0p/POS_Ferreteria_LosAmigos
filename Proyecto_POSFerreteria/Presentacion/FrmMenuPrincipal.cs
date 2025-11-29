using Proyecto_POSFerreteria.Entidades;
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
        // DISEÑOO
        int x, y;
        bool move = false;
        AutoEscala scaler = new AutoEscala();


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







        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }



        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void lblUsuario_Click(object sender, EventArgs e)
        {

        }



        private void dgvStock_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        // DISEÑO
       

        private void FrmMenuPrincipal_Shown(object sender, EventArgs e)
        {
            scaler.Capture(this);
        }

        private void FrmMenuPrincipal_Resize(object sender, EventArgs e)
        {
            scaler.Resize(this);
        }
        private void button3_Click_1(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
                this.WindowState = FormWindowState.Maximized;
            else
                this.WindowState = FormWindowState.Normal;

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

        private void pictureBox2_Click(object sender, EventArgs e)
        {

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
                this.SetDesktopLocation(MousePosition.X-x, MousePosition.Y-y);
            }
        }

        private void btnUsuario_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void lblRol_Click(object sender, EventArgs e)
        {

        }

        private void btnReportes_Click(object sender, EventArgs e)
        {

        }

        private void btnVentas_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        

    }
}
