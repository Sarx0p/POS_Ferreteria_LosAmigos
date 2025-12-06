using Proyecto_POSFerreteria.Datos.Datos_Hembert;
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
    public partial class FrmReportes : Form
    {
        public FrmReportes()
        {
            InitializeComponent();
        }

        private void dtpDesde_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("DESEA SALIR?",
                     "CONFIMARCION",
                     MessageBoxButtons.YesNo,
                     MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void dgbRegistroVentas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void CargarHistorialVentas()
        {
            dgbRegistroVentas.DataSource = VentaDAL.ObtenerVentas();
            dgbRegistroVentas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgbRegistroVentas.Columns["Total"].DefaultCellStyle.Format = "0.00";
            dgbRegistroVentas.Columns["FechaVenta"].DefaultCellStyle.Format = "dd/MM/yyyy";
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FrmReportes_Load(object sender, EventArgs e)
        {
            CargarHistorialVentas();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBuscar.Text))
            {
                CargarHistorialVentas();
            }
            else
            {
                dgbRegistroVentas.DataSource = VentaDAL.BuscarVentasPorCliente(txtBuscar.Text);
            }
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        
            {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrWhiteSpace(txtBuscar.Text))
                {
                    CargarHistorialVentas(); // Si está vacío recarga todo
                }
                else
                {
                    dgbRegistroVentas.DataSource = VentaDAL.BuscarVentasPorCliente(txtBuscar.Text);
                }

                e.SuppressKeyPress = true; // Evita sonido de Windows
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
    }

