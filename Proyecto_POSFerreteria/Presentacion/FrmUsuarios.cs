using Proyecto_POSFerreteria.Datos;
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

namespace Proyecto_POSFerreteria.Presentacion
{
    public partial class FrmUsuarios : Form
    {
        private int selectedId = -1; // Id interno, no mostrado
        public FrmUsuarios()
        {
            InitializeComponent();


        int x, y;
        bool move = false;

    }

        private void FrmUsuarios_Load(object sender, EventArgs e)
        {
            CargarRoles();
            Limpiar();
            CargarUsuarios();
        }
        private void CargarRoles()
        {
            cmbRol.Items.Clear();
            cmbRol.Items.AddRange(new string[] { "Admin", "Cajero" });
            cmbRol.SelectedIndex = -1;
        }

        private void CargarUsuarios()
        {
            try
            {
                var bll = new GestionUsuarioBLL();   // instancia del BLL
                var lista = bll.Listar;     // debe devolver List<Usuario> o equivalente

                var tabla = lista.Select(u => new
                {
                    IdUsuario = u.Id ??"",
                    Nombre = (u.Nombre ?? "") + " " + (u.Apellido ?? ""),
                    Username = u.Username ?? u.Username ?? "",
                    Dui = u.Dui ?? "",
                    Correo = u.Correo ?? "",
                    Rol = u.Rol ?? "",
                }).ToList();

                dgvUsuarios.DataSource = tabla;

                // Opcional: ocultar la columna Id si se muestra
                if (dgvUsuarios.Columns["IdUsuario"] != null)
                    dgvUsuarios.Columns["IdUsuario"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cargando usuarios: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string q = txtBuscar.Text.Trim().ToLower();
                var bll = new GestionUsuarioBLL();
                var lista = bll.Listar;

                var filtrado = lista
                    .Where(u =>
                        ((u.Username ?? u.NombreUsuario ?? "").ToLower().Contains(q)) ||
                        ((u.Nombre ?? "").ToLower().Contains(q)) ||
                        ((u.Apellido ?? "").ToLower().Contains(q)) ||
                        ((u.Dui ?? "").ToLower().Contains(q)) ||
                        ((u.Rol ?? "").ToLower().Contains(q)))
                    .Select(u => new
                    {
                        IdUsuario = u.Id,
                        Nombre = (u.Nombre ?? "") + " " + (u.Apellido ?? ""),
                        Username = u.Username ?? u.NombreUsuario ?? "",
                        Dui = u.Dui ?? "",
                        Correo = u.Correo ?? "",
                        Rol = u.Rol ?? "",
                        Estado = (u.Estado ? "Activo" : "Inactivo")
                    }).ToList();

                dgvUsuarios.DataSource = filtrado;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en búsqueda: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                var row = dgvUsuarios.Rows[e.RowIndex];

                // lee Id interno (si lo ocultas en la grid sigue existiendo)
                selectedId = Convert.ToInt32(row.Cells["IdUsuario"].Value);

                // Rellenar campos visibles
                // Si tu DataGrid tiene nombres de columnas distintos, cámbialos aquí
                var full = row.Cells["Nombre"].Value?.ToString() ?? "";
                var parts = full.Split(new[] { ' ' }, 2);
                txtNombre.Text = parts.Length > 0 ? parts[0] : "";
                txtApellido.Text = parts.Length > 1 ? parts[1] : "";

                txtUsernName.Text = row.Cells["Username"].Value?.ToString() ?? "";
                txtDui.Text = row.Cells["Dui"].Value?.ToString() ?? "";
                txtCorreo.Text = row.Cells["Correo"].Value?.ToString() ?? "";
                cmbRol.Text = row.Cells["Rol"].Value?.ToString() ?? "";
              

                // No mostramos la contraseña cifrada; txtClave se usa para establecer nueva clave
                txtClave.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error seleccionando usuario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validaciones básicas
                string username = txtUsernName.Text.Trim();
                string nombre = txtNombre.Text.Trim();
                string apellido = txtApellido.Text.Trim();
                string dui = txtDui.Text.Trim();
                string correo = txtCorreo.Text.Trim();
                string rol = cmbRol.Text;
                string clavePlain = txtClave.Text;

                if (string.IsNullOrWhiteSpace(username)) { MessageBox.Show("Ingrese el username."); txtUsernName.Focus(); return; }
                if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellido)) { MessageBox.Show("Ingrese nombre y apellido."); return; }
                if (string.IsNullOrWhiteSpace(dui)) { MessageBox.Show("Ingrese DUI."); txtDui.Focus(); return; }
                if (string.IsNullOrWhiteSpace(rol)) { MessageBox.Show("Seleccione rol."); cmbRol.Focus(); return; }
                if (string.IsNullOrWhiteSpace(clavePlain)) { MessageBox.Show("Ingrese contraseña."); txtClave.Focus(); return; }

                // Ciframos la contraseña con DPAPI (usa tu util CryptoDPAPI)
                byte[] claveCifrada = CryptoDPAPI.CifrarContrasena(clavePlain);

                // Llamada al BLL/DAL: asegúrate que tu Insertar acepta estos parámetros
                var bll = new GestionUsuarioBLL();
                // Si tu DAL espera clave en base64 en vez de byte[], adapta la llamada (por ejemplo Convert.ToBase64String)
                int newId = bll.Insertar(username, claveCifrada, rol, nombre, apellido, dui, correo);
                MessageBox.Show("Usuario creado. ID: " + newId, "Listo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Limpiar();
                CargarUsuarios();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedId < 0) { MessageBox.Show("Seleccione un usuario para actualizar."); return; }

                string username = txtUsernName.Text.Trim();
                string nombre = txtNombre.Text.Trim();
                string apellido = txtApellido.Text.Trim();
                string dui = txtDui.Text.Trim();
                string correo = txtCorreo.Text.Trim();
                string rol = cmbRol.Text;
                string nuevaClave = txtClave.Text;

                var bll = new UsuarioBLL();

                // Si hay nueva clave -> cifrar y pasarla; si no, pasar null/empty para no actualizarla
                byte[] claveCifrada = null;
                if (!string.IsNullOrWhiteSpace(nuevaClave))
                    claveCifrada = CryptoDPAPI.CifrarContrasena(nuevaClave);

                // Llama a Actualizar en BLL. Asegúrate de la firma:
                // Propuesta: bool Actualizar(int id, string username, string nombre, string apellido, string dui, string correo, string rol, bool estado, byte[] claveCifrada = null)
                bool ok = bll.Actualizar(selectedId, username, nombre, apellido, dui, correo, rol, claveCifrada);
                MessageBox.Show(ok ? "Usuario actualizado." : "No se actualizó.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarUsuarios();
                Limpiar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedId < 0) { MessageBox.Show("Seleccione usuario para eliminar."); return; }

                var r = MessageBox.Show("¿Eliminar usuario?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (r == DialogResult.Yes)
                {
                    var bll = new UsuarioBLL();
                    bool ok = bll.Eliminar(selectedId);
                    MessageBox.Show(ok ? "Usuario eliminado." : "No se pudo eliminar.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarUsuarios();
                    Limpiar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            CargarUsuarios();
        }

        private void Limpiar()
        {
            selectedId = -1;
            txtUsernName.Text = "";
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtDui.Text = "";
            txtCorreo.Text = "";
            txtClave.Text = "";
            cmbRol.SelectedIndex = -1;
            txtBuscar.Text = "";
        }
    }
    }
    
    
    
    
    

        