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
        public FrmValidarToken()
        {
            InitializeComponent();


     

    }
        public string PrefillIdentificador { get; set; } = "";

        private void FrmValidarToken_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(PrefillIdentificador))
                txtCodigo.Text = PrefillIdentificador;

            lblEstado.Text = "";
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
        }
        }

        