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
        // Propiedad pública para que el otro form prefille el identificador (correo o DUI)
        public string PrefillIdentificador { get; set; } = "";

        // id resuelto después de validar el token (lo usamos para cambiar contraseña)
        private int _idUsuario = 0;

        // instancia local del BLL
        private readonly UsuarioBLL _bll;

        public FrmValidarToken()
        {
            InitializeComponent();

            panelCambiar.Visible = false;
            lblEstado.Text = "";
            _bll = new UsuarioBLL();

            // asegurar que el combo tiene opciones (si no lo llenaste en Designer)
            if (cbxOpcion != null && cbxOpcion.Items.Count == 0)
            {
                cbxOpcion.Items.AddRange(new object[] { "Recordar usuario", "Restablecer contraseña" });
                cbxOpcion.SelectedIndex = 0;
            }
        }
     

    
      

        private void FrmValidarToken_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(PrefillIdentificador) && txtIdentificador != null)
                txtIdentificador.Text = PrefillIdentificador;

            lblEstado.Text = "";
            panelCambiar.Visible = false;
        }

        private void cbxTipoRecuperacion_SelectedIndexChanged(object sender, EventArgs e)
        
        {
            lblEstado.Text = "";
            if (cbxTipoRecuperacion.SelectedIndex == 0) // recordar usuario
            {
                lblEstado.Text = "Ingrese el codigo recibido al correo electronico registrado en su cuenta";
            }
            else if (cbxTipoRecuperacion.SelectedIndex == 1) // restaurar contraseña
            {
                lblEstado.Text = "Ingrese el codigo recibido al correo electronico registrado en su cuenta";
            }

        }

        private void btnValidar_Click(object sender, EventArgs e)
        {
            lblEstado.ForeColor = Color.Black;
            lblEstado.Text = "Validando token...";

            try
            {
                string identificador = txtIdentificador.Text.Trim();
                string codigo = txtCodigo.Text.Trim();

                if (string.IsNullOrWhiteSpace(identificador))
                {
                    lblEstado.ForeColor = Color.Red;
                    lblEstado.Text = "Falta el identificador (correo o DUI).";
                    return;
                }

                if (string.IsNullOrWhiteSpace(codigo))
                {
                    lblEstado.ForeColor = Color.Red;
                    lblEstado.Text = "Ingrese el código recibido.";
                    return;
                }

                // decidir tipo según cbxOpcion (0 => recordar usuario, 1 => restablecer contraseña)
                TipoRecordatorio tipo = TipoRecordatorio.RecordarUsuario;
                if (cbxOpcion != null && cbxOpcion.SelectedIndex == 1)
                    tipo = TipoRecordatorio.RecordarContrasena;

                // Llamada a BLL
                var resultado = _bll.ValidarTokenYDecidir(identificador, codigo, tipo);

                // Guardamos idUsuario
                _idUsuario = resultado.IdUsuario;

                // Resultado - mostrar mensaje de estado
                lblEstado.ForeColor = Color.Green;
                lblEstado.Text = resultado.Mensaje ?? "Token válido.";

                // Si el usuario pidió recordar usuario -> mostrar username y cerrar
                if (tipo == TipoRecordatorio.RecordarUsuario)
                {
                    if (!string.IsNullOrWhiteSpace(resultado.Username))
                        MessageBox.Show($"Tu usuario es: {resultado.Username}", "Recordatorio de usuario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("No se pudo obtener el usuario.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Close();
                    return;
                }

                // Si pidió restablecer contraseña
                if (tipo == TipoRecordatorio.RecordarContrasena)
                {
                    if (resultado.CanChange)
                    {
                        // permitir cambio: mostrar panel para cambiar contraseña
                        panelCambiar.Visible = true;
                        panelCambiar.BringToFront();
                        txtNueva.Focus();
                        lblEstado.Text = "Token válido. Ingrese nueva contraseña.";
                    }
                    else
                    {
                        // No puede cambiar: mostrar el mensaje (p.ej. "consulte con admin") y opcionalmente la contraseña
                        lblEstado.ForeColor = Color.Orange;
                        lblEstado.Text = resultado.Mensaje ?? "No puede cambiar la contraseña en este momento.";

                        if (!string.IsNullOrWhiteSpace(resultado.Contrasena))
                        {
                            // mostrar contraseña desencriptada (solo si así lo decides)
                            MessageBox.Show($"Contraseña registrada: {resultado.Contrasena}\n\nContacte con el administrador si desea cambiarla.", "Recordatorio de contraseña", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show(resultado.Mensaje ?? "Consulte con el administrador.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                lblEstado.ForeColor = Color.Red;
                lblEstado.Text = ex.Message;
            }
        }

        private void btnCambiar_Click(object sender, EventArgs e)
        {
            // validar campos
            string nueva = txtNueva.Text;
            string confirm = txtConfirm.Text;

            if (string.IsNullOrWhiteSpace(nueva) || string.IsNullOrWhiteSpace(confirm))
            {
                MessageBox.Show("Complete la nueva contraseña y su confirmación.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (nueva != confirm)
            {
                MessageBox.Show("Las contraseñas no coinciden.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (nueva.Length < 6)
            {
                MessageBox.Show("La contraseña debe tener al menos 6 caracteres.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_idUsuario <= 0)
            {
                MessageBox.Show("Id de usuario no válido. Vuelva a validar el token.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                _bll.CambiarContrasenaPorId(_idUsuario, nueva);
                MessageBox.Show("Contraseña cambiada correctamente.", "Listo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al cambiar contraseña", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

        
    
}
        

        