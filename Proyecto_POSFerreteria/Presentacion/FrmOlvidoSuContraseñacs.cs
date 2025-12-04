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
                // Leer campos
                string nombre = txtNombreApellido.Text?.Trim();
                string dui = txtDui.Text?.Trim();
                string correo = txtCorreo.Text?.Trim();

                // Validaciones básicas
                if (string.IsNullOrWhiteSpace(nombre) && string.IsNullOrWhiteSpace(dui) && string.IsNullOrWhiteSpace(correo))
                {
                    MessageBox.Show("Ingrese al menos uno de los datos: Nombre, DUI o Correo.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Preferimos correo (porque la BLL envía correo). Si no hay correo, usamos DUI.
                string identificador = null;
                if (!string.IsNullOrWhiteSpace(correo))
                {
                    identificador = correo;
                }
                else if (!string.IsNullOrWhiteSpace(dui))
                {
                    identificador = dui;
                }
                else
                {
                    // Si solo hay nombre (sin correo/dui), podemos mostrar instrucción:
                    MessageBox.Show("Para recuperar la cuenta necesita proporcionar al menos el correo o el DUI asociado.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Desactivar botón para evitar doble click
                btnEnviar.Enabled = false;
                Cursor = Cursors.WaitCursor;

                // Llamada a la BLL (genera token y envía correo)
                try
                {
                    // Esto usa UsuarioBLL.GenerarTokenPorIdentificador
                    UsuarioBLL.GenerarTokenPorIdentificador(identificador);

                    MessageBox.Show("Solicitud enviada. Si existe una cuenta con esos datos, recibirá un correo con el código de recuperación.", "Enviado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // opcional: limpiar formulario
                    txtNombreApellido.Text = "";
                    txtDui.Text = "";
                    txtCorreo.Text = "";
                }
                catch (Exception ex)
                {
                    // Mensajes claros para el usuario
                    MessageBox.Show("No se pudo generar la solicitud: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    // Adicional: registrar en log (si tienes logger) o dejar trace
                    // Logger.LogError(ex);
                }
            }
            finally
            {
                // Restaurar UI
                btnEnviar.Enabled = true;
                Cursor = Cursors.Default;
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
