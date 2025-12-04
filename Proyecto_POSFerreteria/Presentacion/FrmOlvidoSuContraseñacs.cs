using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Proyecto_POSFerreteria.Entidades;
using Proyecto_POSFerreteria.Negocio;

namespace Proyecto_POSFerreteria.Presentacion
{
    public partial class FrmOlvidoSuContraseñacs : Form
    {
       
        // NO TOCAR ES PARA DISEÑO
        int x, y;
        bool move = false;
   
        
        
        public FrmOlvidoSuContraseñacs()
        {
            InitializeComponent();
            

        }

        private void FrmOlvidoSuContraseñacs_Load(object sender, EventArgs e)
        { 
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }



        // DISEÑO
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
            { this.SetDesktopLocation(MousePosition.X - x, MousePosition.Y - y); }

        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {

            try
            {
                // Tomar los campos del form (ajusta nombres si tus controles se llaman diferente)
                string correo = txtCorreo.Text?.Trim();
                string dui = txtDui.Text?.Trim();
                string nombre = txtNombreApellido.Text?.Trim();
           

                // Elegir identificador: preferimos correo, sino DUI
                string identificador = null;
                if (!string.IsNullOrWhiteSpace(correo)) identificador = correo;
                else if (!string.IsNullOrWhiteSpace(dui)) identificador = dui;

                if (string.IsNullOrWhiteSpace(identificador))
                {
                    MessageBox.Show("Ingrese su correo o su DUI para enviar el código.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                
                UsuarioBLL.GenerarTokenPorIdentificador(identificador);

                MessageBox.Show("Código enviado correctamente. Revise su correo (o su solicitud se registró para revisión).", "Enviado", MessageBoxButtons.OK, MessageBoxIcon.Information);

               
                var frmVal = new FrmValidarToken(identificador); 
                frmVal.StartPosition = FormStartPosition.CenterParent;
                frmVal.ShowDialog();
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo generar la solicitud: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
    {
        // Tomar los campos del form 
        string correo = txtCorreo.Text?.Trim();
        string dui = txtDui.Text?.Trim();
        string nombre = txtNombreApellido.Text?.Trim();
        

        
        string identificador = null;
        if (!string.IsNullOrWhiteSpace(correo)) identificador = correo;
        else if (!string.IsNullOrWhiteSpace(dui)) identificador = dui;

        if (string.IsNullOrWhiteSpace(identificador))
        {
            MessageBox.Show("Ingrese su correo o su DUI para enviar el código.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        // Lgenera token
        UsuarioBLL.GenerarTokenPorIdentificador(identificador);

        MessageBox.Show("Código enviado correctamente. Revise su correo (o su solicitud se registró para revisión).", "Enviado", MessageBoxButtons.OK, MessageBoxIcon.Information);

        // Abre form
        var frmVal = new FrmValidarToken(identificador); // requiere constructor que acepte string
        frmVal.StartPosition = FormStartPosition.CenterParent;
        frmVal.ShowDialog();
    }
    catch (Exception ex)
    {
                    MessageBox.Show("No se pudo generar la solicitud: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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

