using Proyecto_POSFerreteria.Datos;
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

namespace Proyecto_POSFerreteria.Presentacion
{
    public partial class FrmUsuarios : Form
    {
        private int _idSeleccionado = 0;
        private readonly string _cn = Conexion.Cadena;
        public FrmUsuarios()
        {
            InitializeComponent();

        }
        int x, y;
        bool move = false;

 

        private void FrmUsuarios_Load(object sender, EventArgs e)
        {
            CargarRoles();
            Limpiar();
            CargarUsuarios();
            CargarSolicitudes();
        }
        private void CargarRoles()
        {
            cmbRol.Items.Clear();
            cmbRol.Items.AddRange(new string[] { "Admin", "Empleado" }); // ajusta a tus roles
            cmbRol.SelectedIndex = -1;
        }

        private void CargarSolicitudes()
        {
            try
            {
                using (var cn = new System.Data.SqlClient.SqlConnection(_cn))
                using (var da = new System.Data.SqlClient.SqlDataAdapter(@"
            SELECT Id, IdUsuario, Fecha, Estado
            FROM SolicitudRecuperacion
            ORDER BY Fecha DESC", cn))
                {
                    var dt = new System.Data.DataTable();
                    da.Fill(dt);
                    dgvSolicitudes.DataSource = dt;

                    // Opcional: ocultar Id si no quieres que lo vean
                    if (dgvSolicitudes.Columns["Id"] != null) dgvSolicitudes.Columns["Id"].Visible = false;
                    dgvSolicitudes.AutoResizeColumns();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cargando historial: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            try
            {
                var row = dgvUsuarios.Rows[e.RowIndex];
                _idSeleccionado = Convert.ToInt32(row.Cells["IdUsuario"].Value);

                // Cargar datos completos desde DAL/BLL para evitar campos faltantes en la grid
                // (mejor para obtener Apellido, Nombre separado, etc.)
                var usuario = UsuarioBLL.Listar().FirstOrDefault(x => x.IdUsuario == _idSeleccionado);
                if (usuario == null)
                {
                    MessageBox.Show("No se encontró el usuario seleccionado.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Mapear a controles
                txtUsernName.Text = usuario.Username ?? "";
                txtNombre.Text = usuario.Nombre ?? "";
                txtApellido.Text = usuario.Apellido ?? "";
                txtCorreo.Text = usuario.Correo ?? "";
                txtDui.Text = usuario.Dui ?? "";
                cmbRol.Text = usuario.Rol ?? "";
                txtClave.Text = ""; // no mostrar password
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al seleccionar usuario: " + ex.Message);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (_idSeleccionado <= 0)
                {
                    MessageBox.Show("Seleccione un usuario para actualizar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string username = txtUsernName.Text.Trim();
                string rol = cmbRol.Text;
                string nombre = txtNombre.Text.Trim();
                string apellido = txtApellido.Text.Trim();
                string correo = txtCorreo.Text.Trim();
                string dui = txtDui.Text.Trim();

                if (string.IsNullOrWhiteSpace(username))
                {
                    MessageBox.Show("Ingrese un usuario.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool ok = UsuarioBLL.Actualizar(_idSeleccionado, nombre, apellido, username, rol, correo, dui);
                MessageBox.Show(ok ? "Usuario actualizado." : "No se pudo actualizar.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Limpiar();
                CargarUsuarios();
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
                if (_idSeleccionado <= 0)
                {
                    MessageBox.Show("Seleccione un usuario para eliminar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var r = MessageBox.Show("¿Desea eliminar el usuario seleccionado?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (r == DialogResult.Yes)
                {
                    bool ok = UsuarioBLL.Eliminar(_idSeleccionado);
                    MessageBox.Show(ok ? "Usuario eliminado." : "No se pudo eliminar.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Limpiar();
                    CargarUsuarios();
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
            _idSeleccionado = 0;
            txtUsernName.Text = "";
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtCorreo.Text = "";
            txtClave.Text = "";
            txtDui.Text = "";
            cmbRol.SelectedIndex = -1;
        }
        

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validaciones mínimas
                string username = txtUsernName.Text.Trim();
                string clave = txtClave.Text;
                string rol = cmbRol.Text;
                string nombre = txtNombre.Text.Trim();
                string apellido = txtApellido.Text.Trim();
                string correo = txtCorreo.Text.Trim();
                string dui = txtDui.Text.Trim();

                if (string.IsNullOrWhiteSpace(username))
                {
                    MessageBox.Show("Ingrese un usuario.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUsernName.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(clave))
                {
                    MessageBox.Show("Ingrese la contraseña.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtClave.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(rol))
                {
                    MessageBox.Show("Seleccione un rol.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbRol.Focus();
                    return;
                }

                // Insertar via BLL (Bll se encarga de cifrar)
                int nuevoId = UsuarioBLL.Insertar(nombre, apellido, username, clave, rol, correo, dui);
                MessageBox.Show("Usuario creado con ID: " + nuevoId, "Listo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Limpiar();
                CargarUsuarios();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string q = txtBuscar.Text.Trim().ToLower();
                var lista = UsuarioBLL.Listar();
                var filtrado = lista.Where(u => (u.Username ?? "").ToLower().Contains(q)
                                             || (u.Nombre ?? "").ToLower().Contains(q)
                                             || (u.Apellido ?? "").ToLower().Contains(q)
                                             || (u.Rol ?? "").ToLower().Contains(q)).Select(u => new
                                             {
                                                 IdUsuario = u.IdUsuario,
                                                 Nombre = (u.Nombre ?? "") + " " + (u.Apellido ?? ""),
                                                 NombreUsuario = u.Username,
                                                 Rol = u.Rol,
                                                 Correo = u.Correo,
                                                 Dui = u.Dui
                                             }).ToList();
                dgvUsuarios.DataSource = filtrado;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en búsqueda: " + ex.Message);
            }
        }

        private void btnEliminarP_Click(object sender, EventArgs e)
        {
            if (dgvSolicitudes.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una solicitud en el historial.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = Convert.ToInt32(dgvSolicitudes.SelectedRows[0].Cells["Id"].Value);
            if (MessageBox.Show("¿Eliminar la petición seleccionada?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                using (var cn = new System.Data.SqlClient.SqlConnection(_cn))
                using (var cmd = new System.Data.SqlClient.SqlCommand("DELETE FROM SolicitudRecuperacion WHERE Id = @id", cn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Petición eliminada.", "Listo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarSolicitudes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error eliminando petición: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMarcarAtendida_Click(object sender, EventArgs e)
        {
            if (dgvSolicitudes.SelectedRows.Count == 0) { MessageBox.Show("Seleccione una solicitud."); return; }
            int id = Convert.ToInt32(dgvSolicitudes.SelectedRows[0].Cells["Id"].Value);

            try
            {
                using (var cn = new System.Data.SqlClient.SqlConnection(_cn))
                using (var cmd = new System.Data.SqlClient.SqlCommand("UPDATE SolicitudRecuperacion SET Estado = @e WHERE Id = @id", cn))
                {
                    cmd.Parameters.AddWithValue("@e", "Atendida");
                    cmd.Parameters.AddWithValue("@id", id);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
                CargarSolicitudes();
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }        

        private void CargarUsuarios()
        {
            try
            {
                // Si UsuarioBLL tiene métodos estáticos:
                List<Usuario> lista = UsuarioBLL.Listar();

                // Si tu BLL no son static: uncomment:
                // var bll = new UsuarioBLL(); List<Usuario> lista = bll.Listar();

                var tabla = lista.Select(u => new
                {
                    IdUsuario = u.IdUsuario,
                    Nombre = (u.Nombre ?? "") + " " + (u.Apellido ?? ""),
                    NombreUsuario = u.Username,
                    Rol = u.Rol ?? "",
                    Correo = u.Correo ?? "",
                    Dui = u.Dui ?? "",
                    Estado = (u.Debe_Cambiar_Contrasena ? "Forzar cambio" : "OK")
                }).ToList();

                dgvUsuarios.DataSource = tabla;
                dgvUsuarios.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cargando usuarios: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

        }
      