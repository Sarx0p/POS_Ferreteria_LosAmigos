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
       

        public FrmValidarToken()
        {
            InitializeComponent();
        }





        private void FrmValidarToken_Load(object sender, EventArgs e)
        {
            
        }

        private void cbxTipoRecuperacion_SelectedIndexChanged(object sender, EventArgs e)
        
        {
            
        }

        private void btnValidar_Click(object sender, EventArgs e)
        {
            try
            {
                string identificador = txtIdentificador.Text.Trim();
                string codigo = txtCodigo.Text.Trim();

                if (string.IsNullOrWhiteSpace(identificador) || string.IsNullOrWhiteSpace(codigo))
                {
                    MessageBox.Show("Identificador y código requeridos.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var resultado = UsuarioBLL.ValidarTokenYDecidir(identificador, codigo, UsuarioBLL.TipoRecordatorio.RecordarContrasena);

                // resultado.Username, resultado.Contrasena (si recuerda), resultado.CanChange, resultado.Mensaje, resultado.IdUsuario
                MessageBox.Show(resultado.Mensaje, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (resultado.Username != null)
                {
                    lblUsuario.Text = resultado.Username;
                }

                if (resultado.Contrasena != null)
                {
                    // solo si el BLL retorna la contraseña desencriptada (ten cuidado con mostrarla)
                    txtContrasenaActual.Text = resultado.Contrasena;
                }

                if (resultado.CanChange)
                {
                    // habilitar inputs para cambiar
                    panelCambioClave.Enabled = true;
                    txtNuevaClave.Focus();
                    // guarda id en variable local para ejecutar cambio más tarde
                    this._idUsuarioParaCambio = resultado.IdUsuario;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        }

        private void btnCambiar_Click(object sender, EventArgs e)
        {
           
        }
    }

        
    
}
        

        