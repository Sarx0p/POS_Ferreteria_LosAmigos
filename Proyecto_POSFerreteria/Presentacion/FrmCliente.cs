using Proyecto_POSFerreteria.Entidades;
using Proyecto_POSFerreteria.Negocio;
using Proyecto_POSFerreteria.Negocio.Hembert;
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
    public partial class FrmCliente : Form


    {

        ClienteBLL bll = new ClienteBLL();
        int clienteId = 0;

        public FrmCliente()
        {
            InitializeComponent();
        }

        private void FrmCliente_Load(object sender, EventArgs e)
        {
            CargarDatos();
            Limpiar();
        }

        void Limpiar()
        {
          
            txtNombre.Clear();
            txtNombre.Focus();

           
        }

        void CargarDatos()
        {
            dgvClientes.DataSource = bll.Listar();
        }



        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Cliente c = new Cliente();
                c.NombreCompleto = txtNombre.Text;

                if (clienteId == 0)
                {
                    // INSERTAR
                    int id = bll.Guardar(c);
                    MessageBox.Show("Cliente guardado con ID: " + id);
                }
                else
                {
                    // EDITAR
                    c.Id = clienteId;
                    bool editado = bll.Editar(c);
                    MessageBox.Show(editado ? "Cliente editado" : "No se pudo editar");
                }

                CargarDatos();
                Limpiar();
                clienteId = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el cliente: " + ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (clienteId == 0)
            {
                MessageBox.Show("Seleccione un cliente para eliminar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("¿Está seguro de eliminar este cliente?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    bool eliminado = bll.Eliminar(clienteId);
                    if (eliminado)
                    {
                        MessageBox.Show("Cliente eliminado correctamente");
                        CargarDatos();
                        Limpiar();
                        clienteId = 0;
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el cliente");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar el cliente: " + ex.Message);
                }
            }
        }

        private void dgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvClientes.Rows[e.RowIndex];

                clienteId = Convert.ToInt32(row.Cells["Id"].Value);   // <-- ESTE TE FALTABA
                txtNombre.Text = row.Cells["NombreCompleto"].Value.ToString();
            }  }

        private void btnNuevo_Click(object sender, EventArgs e)
        {

        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this .Close();
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            dgvClientes.DataSource = bll.BuscarPorNombre(txtBuscar.Text);
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                Cliente c = new Cliente();
                c.NombreCompleto = txtNombre.Text;

                int id = bll.Guardar(c);

                MessageBox.Show("Cliente guardado con ID: " + id);

                CargarDatos();
                Limpiar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el cliente: " + ex.Message);
            }
        }
    }
    
}
