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
          
        }

        private void btnCambiar_Click(object sender, EventArgs e)
        {
           
        }
    }

        
    
}
        

        