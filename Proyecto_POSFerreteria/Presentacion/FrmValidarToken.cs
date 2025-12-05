using Proyecto_POSFerreteria.Entidades;
using Proyecto_POSFerreteria.Negocio;
using Proyecto_POSFerreteria.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Proyecto_POSFerreteria.Negocio.UsuarioBLL;

namespace Proyecto_POSFerreteria.Presentacion
{
    public partial class FrmValidarToken : Form
    {

        private readonly string _identificador;
        private int _idUsuarioValidado = 0;
        private int _tokenId = 0;

        public FrmValidarToken() { InitializeComponent(); }

        public FrmValidarToken(string identificador) : this()
        {
            _identificador = identificador;
        }



        private void FrmValidarToken_Load(object sender, EventArgs e)
        {
            lblUsuarioMostrado.Text = "";
            lblUsuarioMostrado.Visible = false;
            groupCambio.Enabled = false;
            btnCambiar.Enabled = false;

            if (!string.IsNullOrWhiteSpace(_identificador))
            {
                txtIdentificador.Text = _identificador;
                txtCodigo.Focus();
            }
        
    }

        private void btnCambiar_Click(object sender, EventArgs e)
        {
            try
            {
                if (_idUsuarioValidado <= 0 || _tokenId <= 0)
                {
                    MessageBox.Show("Primero valide el token correctamente.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string nueva = txtNueva.Text?.Trim();
                string confirma = txtConfirm.Text?.Trim();

                if (string.IsNullOrWhiteSpace(nueva) || string.IsNullOrWhiteSpace(confirma))
                {
                    MessageBox.Show("Ingrese la nueva contraseña y su confirmación.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (nueva != confirma)
                {
                    MessageBox.Show("Las contraseñas no coinciden.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Cambiar contraseña (BLL se encarga de cifrar y actualizar)
                UsuarioBLL.CambiarContrasenaPorId(_idUsuarioValidado, nueva);

                // Marcar token como usado AHORA que la operación finalizó
                UsuarioBLL.MarcarTokenUsado(_tokenId);

                MessageBox.Show("Contraseña actualizada correctamente.", "Listo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // limpiar y deshabilitar
                txtNueva.Clear();
                txtConfirm.Clear();
                groupCambio.Enabled = false;
                btnCambiar.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cambiando contraseña: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnValidar_Click(object sender, EventArgs e)
        {
            try
            {
                string identificador = txtIdentificador.Text?.Trim();
                string codigo = txtCodigo.Text?.Trim();

                if (string.IsNullOrWhiteSpace(identificador))
                {
                    MessageBox.Show("Falta identificador (correo o DUI).", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrWhiteSpace(codigo))
                {
                    MessageBox.Show("Ingrese el código recibido por correo.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var tipo = UsuarioBLL.TipoRecordatorio.RecordarContrasena;

                var resultado = UsuarioBLL.ValidarTokenYDecidir(identificador, codigo, tipo);

                _idUsuarioValidado = resultado.IdUsuario;
                _tokenId = resultado.TokenId;

                MessageBox.Show(resultado.Mensaje, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (!string.IsNullOrWhiteSpace(resultado.Username))
                {
                    lblUsuarioMostrado.Text = resultado.Username;
                    lblUsuarioMostrado.Visible = true;
                }

                if (resultado.CanChange)
                {
                    groupCambio.Enabled = true;
                    btnCambiar.Enabled = true;
                    txtNueva.Focus();
                }
                else
                {
                    groupCambio.Enabled = false;
                    btnCambiar.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error validando token: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtConfirm_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblEstado_Click(object sender, EventArgs e)
        {

        }
    }
}
