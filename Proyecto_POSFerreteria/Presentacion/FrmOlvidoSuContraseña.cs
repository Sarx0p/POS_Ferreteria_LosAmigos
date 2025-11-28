using Proyecto_POSFerreteria.Datos;
using Proyecto_POSFerreteria.Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Proyecto_POSFerreteria.Presentacion
{
    public partial class FrmOlvidoSuContraseña : Form
    {
        public FrmOlvidoSuContraseña()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void FrmOlvidoSuContraseña_Load(object sender, EventArgs e)


        {
            cbxTipoRecuperacion.Items.Clear();
            cbxTipoRecuperacion.Items.Add("Olvidé mi usuario");     // índice 0 -> usar Nombre+Apellido
            cbxTipoRecuperacion.Items.Add("Olvidé mi contraseña");  // índice 1 -> usar Username

            // Inicialmente ocultar todos los campos opcionales
            txtNombre.Visible = false;
            txtApellido.Visible = false;
            txtUsuario.Visible = false;
            lblNombre.Visible = false;
            lblApellido.Visible = false;
            lblUsuario.Visible = false;

            cbxTipoRecuperacion.SelectedIndex = -1; // sin selección por defecto
        }

        private void cbxTipoRecuperacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Ocultar todo por defecto
          
            txtNombre.Visible = false;
            txtApellido.Visible = false;
            txtUsuario.Visible = false;
            lblNombre.Visible = false;
            lblApellido.Visible = false;
            lblUsuario.Visible = false;

            if (cbxTipoRecuperacion.SelectedIndex < 0)
            {
                lblMensaje.Text = "";
                return;
            }

            // Mostrar campos según la opción elegida
            if (cbxTipoRecuperacion.SelectedIndex == 0)
            {
                // "Olvidé mi usuario" -> mostrar Nombre y Apellido
                lblMensaje.Text = "Ingresa tu Nombre y Apellido. Tu solicitud será enviada y atendida.";
                txtNombre.Visible = true;
                txtApellido.Visible = true;
                lblNombre.Visible = true;
                lblApellido.Visible = true;
            }
            else if (cbxTipoRecuperacion.SelectedIndex == 1)
            {
                // "Olvidé mi contraseña" -> mostrar Username
                lblMensaje.Text = "Ingresa tu Usuario. Tu solicitud será enviada y atendida.";
                txtUsuario.Visible = true;
                lblUsuario.Visible = true;
            }
        }
        

        private void btnEnvioSolicitud_Click(object sender, EventArgs e)
        {
            if (cbxTipoRecuperacion.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccione el tipo de recuperación.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            RecuperacionBLL rec = new RecuperacionBLL();

            try
            {
                if (cbxTipoRecuperacion.SelectedIndex == 0) // nombre + apellido
                {
                    string nombre = txtNombre.Text.Trim();
                    string apellido = txtApellido.Text.Trim();

                    if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellido))
                    {
                        MessageBox.Show("Ingrese nombre y apellido.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    rec.EnviarSolicitudPorNombreApellido(nombre, apellido);
                }
                else // índice 1 -> username
                {
                    string username = txtUsuario.Text.Trim();

                    if (string.IsNullOrWhiteSpace(username))
                    {
                        MessageBox.Show("Ingrese su usuario.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    rec.EnviarSolicitudPorUsername(username);
                }

                MessageBox.Show("Solicitud enviada correctamente.", "Listo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        }
        }
