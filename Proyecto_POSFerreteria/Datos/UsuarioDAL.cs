using Proyecto_POSFerreteria.Datos;
using Proyecto_POSFerreteria.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    namespace Proyecto_POSFerreteria.Datos
    {
    public class UsuarioDAL
    {
        // <-- campo a nivel de clase (NO dentro de un método)
        private static readonly string cnStr = Conexion.Cadena;

        // LOGIN 
        public Usuario Login(string username, string claveIngresada)
        {
            using (var cn = new SqlConnection(cnStr))
            {
                string sql = @"SELECT Id, Nombre, Username, Rol, ContrasenaCifrada
                               FROM Usuario
                               WHERE Username = @Username";

                using (var cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cn.Open();

                    using (var dr = cmd.ExecuteReader())
                    {
                        if (!dr.Read()) return null;

                        int id = Convert.ToInt32(dr["Id"]);
                        string nombre = dr["Nombre"]?.ToString();
                        string rol = dr["Rol"]?.ToString();
                        string usernameDb = dr["Username"]?.ToString();

                        // Intentar ContrasenaCifrada primero (varbinary)
                        int idxBlob = -1;
                        try { idxBlob = dr.GetOrdinal("ContrasenaCifrada"); } catch { idxBlob = -1; }

                        if (idxBlob >= 0 && !dr.IsDBNull(idxBlob))
                        {
                            object o = dr.GetValue(idxBlob);
                            byte[] blob = null;
                            if (o is byte[] b) blob = b;
                            else if (o is System.Data.SqlTypes.SqlBinary sb) blob = sb.Value;

                            if (blob != null)
                            {
                                string desencriptada = null;
                                try
                                {
                                    desencriptada = Proyecto_POSFerreteria.Utilidades.CryptoDPAPI.DesencriptarContrasena(blob);
                                }
                                catch { }

                                if (!string.IsNullOrEmpty(desencriptada) && desencriptada == claveIngresada)
                                {
                                    return new Usuario
                                    {
                                        IdUsuario = id,
                                        Nombre = nombre,
                                        Rol = rol,
                                        Username = usernameDb
                                    };
                                }
                            }
                        }

                        // Fallback: comparar con la columna Clave (texto)
                        int idxClave = -1;
                        try { idxClave = dr.GetOrdinal("ContrasenaCifrada"); } catch { idxClave = -1; }

                        if (idxClave >= 0 && !dr.IsDBNull(idxClave))
                        {
                            string claveBD = dr["ContrasenaCifrada"]?.ToString();
                            if (!string.IsNullOrEmpty(claveBD) && claveBD == claveIngresada)
                            {
                                return new Usuario
                                {
                                    IdUsuario = id,
                                    Nombre = nombre,
                                    Rol = rol,
                                    Username = usernameDb
                                };
                            }
                        }

                        return null;
                    }
                }
            }
        }

        // Aquí van los demás métodos (Listar, Insertar, etc.)...

        public static List<Usuario> Listar()
        {
            var lista = new List<Usuario>();

            using (SqlConnection cn = new SqlConnection(cnStr))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand(
                    @"SELECT Id, Nombre, Apellido, Username, Rol, Correo, Dui,
                         ContrasenaCifrada, Fecha_Ultimo_Cambio,
                         Dias_Minimos_Entre_Cambios, Debe_Cambiar_Contrasena
                  FROM Usuario", cn))
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Usuario
                        {
                            IdUsuario = Convert.ToInt32(dr["Id"]),
                            Nombre = dr["Nombre"]?.ToString(),
                            Apellido = dr["Apellido"]?.ToString(),
                            Username = dr["Username"]?.ToString(),
                            Rol = dr["Rol"]?.ToString(),
                            Correo = dr["Correo"]?.ToString(),
                            Dui = dr["Dui"]?.ToString(),

                            ContrasenaCifrada = dr["ContrasenaCifrada"] == DBNull.Value
                                                ? null
                                                : (byte[])dr["ContrasenaCifrada"],

                            Fecha_Ultimo_Cambio = dr["Fecha_Ultimo_Cambio"] == DBNull.Value
                                                  ? (DateTime?)null
                                                  : Convert.ToDateTime(dr["Fecha_Ultimo_Cambio"]),

                            Dias_Minimos_Entre_Cambios = dr["Dias_Minimos_Entre_Cambios"] == DBNull.Value
                                                         ? 0
                                                         : Convert.ToInt32(dr["Dias_Minimos_Entre_Cambios"]),

                            Debe_Cambiar_Contrasena = dr["Debe_Cambiar_Contrasena"] == DBNull.Value
                                                      ? false
                                                      : Convert.ToBoolean(dr["Debe_Cambiar_Contrasena"])
                        });
                    }
                }
            }

            return lista;
        }

        // ======================================================
        //  INSERTAR
        // ======================================================
        public static int Insertar(string nombre, string apellido, string username,
                                   string rol, string correo, string dui,
                                   byte[] contrasenaCifrada,
                                   int diasMinimos, bool debeCambiar)
        {
            if (ExisteUsername(username)) throw new InvalidOperationException("El Username ya existe.");
            if (!string.IsNullOrWhiteSpace(correo) && ExisteCorreo(correo)) throw new InvalidOperationException("El correo ya existe.");
            if (!string.IsNullOrWhiteSpace(dui) && ExisteDui(dui)) throw new InvalidOperationException("El DUI ya existe.");

            using (SqlConnection cn = new SqlConnection(cnStr))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand(@"
                INSERT INTO Usuario
                (Nombre, Apellido, Username, Rol, Correo, Dui,
                 ContrasenaCifrada, Fecha_Ultimo_Cambio,
                 Dias_Minimos_Entre_Cambios, Debe_Cambiar_Contrasena)
                VALUES
                (@n, @a, @u, @r, @c, @d, @blob, @now, @dias, @debe);
                SELECT SCOPE_IDENTITY();", cn))
                {
                    cmd.Parameters.AddWithValue("@n", nombre);
                    cmd.Parameters.AddWithValue("@a", apellido);
                    cmd.Parameters.AddWithValue("@u", username);
                    cmd.Parameters.AddWithValue("@r", rol);
                    cmd.Parameters.AddWithValue("@c", (object)correo ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@d", (object)dui ?? DBNull.Value);
                    cmd.Parameters.Add("@blob", SqlDbType.VarBinary).Value = (object)contrasenaCifrada ?? DBNull.Value;
                    cmd.Parameters.AddWithValue("@now", DateTime.UtcNow);
                    cmd.Parameters.AddWithValue("@dias", diasMinimos);
                    cmd.Parameters.AddWithValue("@debe", debeCambiar);

                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        // ======================================================
        //  ACTUALIZAR (NO CAMBIA CONTRASENA)
        // ======================================================
        public static bool Actualizar(int idUsuario, string nombre, string apellido, string username,
                                      string rol, string correo, string dui,
                                      int diasMinimos, bool debeCambiar)
        {
            if (ExisteUsername(username, idUsuario)) throw new InvalidOperationException("El Username ya existe en otro usuario.");
            if (!string.IsNullOrWhiteSpace(correo) && ExisteCorreo(correo, idUsuario)) throw new InvalidOperationException("El correo ya existe.");
            if (!string.IsNullOrWhiteSpace(dui) && ExisteDui(dui, idUsuario)) throw new InvalidOperationException("El DUI ya existe.");

            using (SqlConnection cn = new SqlConnection(cnStr))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand(@"
                UPDATE Usuario SET
                    Nombre=@n, Apellido=@a, Username=@u, Rol=@r,
                    Correo=@c, Dui=@d,
                    Dias_Minimos_Entre_Cambios=@dias,
                    Debe_Cambiar_Contrasena=@debe
                WHERE Id=@id", cn))
                {
                    cmd.Parameters.AddWithValue("@n", nombre);
                    cmd.Parameters.AddWithValue("@a", apellido);
                    cmd.Parameters.AddWithValue("@u", username);
                    cmd.Parameters.AddWithValue("@r", rol);
                    cmd.Parameters.AddWithValue("@c", (object)correo ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@d", (object)dui ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@dias", diasMinimos);
                    cmd.Parameters.AddWithValue("@debe", debeCambiar);
                    cmd.Parameters.AddWithValue("@id", idUsuario);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // ======================================================
        //  ELIMINAR
        // ======================================================
        public static bool Eliminar(int idUsuario)
        {
            using (SqlConnection cn = new SqlConnection(cnStr))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Usuario WHERE Id=@id", cn))
                {
                    cmd.Parameters.AddWithValue("@id", idUsuario);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        //BUSCAr
        public Usuario BuscarPorUsername(string username)
        {
            using (var cn = new SqlConnection(cnStr))
            using (var cmd = new SqlCommand("SELECT * FROM Usuario WHERE Username=@u", cn))
            {
                cmd.Parameters.AddWithValue("@u", username);
                cn.Open();

                using (var dr = cmd.ExecuteReader())
                {
                    if (!dr.Read()) return null;

                    return new Usuario
                    {
                        IdUsuario = Convert.ToInt32(dr["Id"]),
                        Nombre = dr["Nombre"]?.ToString(),
                        Apellido = dr["Apellido"]?.ToString(),
                        Username = dr["Username"]?.ToString(),
                        Rol = dr["Rol"]?.ToString(),
                        Correo = dr["Correo"]?.ToString(),
                        Dui = dr["Dui"]?.ToString(),
                        ContrasenaCifrada = dr["ContrasenaCifrada"] == DBNull.Value ? null : (byte[])dr["ContrasenaCifrada"]
                    };
                }
            }
        }

        // ======================================================
        //  CAMBIAR CONTRASEÑA
        // ======================================================
        public static bool CambiarContrasena(int idUsuario, byte[] nuevaCifrada, bool debeCambiar)
        {
            using (SqlConnection cn = new SqlConnection(cnStr))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand(@"
                UPDATE Usuario SET 
                    ContrasenaCifrada=@c,
                    Fecha_Ultimo_Cambio=@now,
                    Debe_Cambiar_Contrasena=@debe
                WHERE Id=@id", cn))
                {
                    cmd.Parameters.Add("@c", SqlDbType.VarBinary).Value = (object)nuevaCifrada ?? DBNull.Value;
                    cmd.Parameters.AddWithValue("@now", DateTime.UtcNow);
                    cmd.Parameters.AddWithValue("@debe", debeCambiar);
                    cmd.Parameters.AddWithValue("@id", idUsuario);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // ======================================================
        //  VALIDACIONES DE UNICIDAD
        // ======================================================
        private static bool ExisteUsername(string username, int excludeId = 0)
        {
            using (SqlConnection cn = new SqlConnection(cnStr))
            {
                cn.Open();
                string sql = excludeId == 0
                    ? "SELECT COUNT(*) FROM Usuario WHERE Username=@u"
                    : "SELECT COUNT(*) FROM Usuario WHERE Username=@u AND Id<>@id";

                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@u", username);
                    if (excludeId != 0) cmd.Parameters.AddWithValue("@id", excludeId);
                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
        }

        private static bool ExisteCorreo(string correo, int excludeId = 0)
        {
            if (string.IsNullOrWhiteSpace(correo)) return false;

            using (SqlConnection cn = new SqlConnection(cnStr))
            {
                cn.Open();
                string sql = excludeId == 0
                    ? "SELECT COUNT(*) FROM Usuario WHERE Correo=@c"
                    : "SELECT COUNT(*) FROM Usuario WHERE Correo=@c AND Id<>@id";

                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@c", correo);
                    if (excludeId != 0) cmd.Parameters.AddWithValue("@id", excludeId);
                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
        }

        private static bool ExisteDui(string dui, int excludeId = 0)
        {
            if (string.IsNullOrWhiteSpace(dui)) return false;

            using (SqlConnection cn = new SqlConnection(cnStr))
            {
                cn.Open();
                string sql = excludeId == 0
                    ? "SELECT COUNT(*) FROM Usuario WHERE Dui=@d"
                    : "SELECT COUNT(*) FROM Usuario WHERE Dui=@d AND Id<>@id";

                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@d", dui);
                    if (excludeId != 0) cmd.Parameters.AddWithValue("@id", excludeId);
                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
        }




        //Nao

            // Buscar usuario por correo en bd
        private readonly string connStr = Conexion.Cadena;
        public DataRow ObtenerUsuarioPorCorreo(string correo)
        {
            using (var cn = new SqlConnection(connStr))
            using (var da = new SqlDataAdapter("SELECT * FROM Usuario WHERE Correo = @Correo", cn))
            {
                da.SelectCommand.Parameters.AddWithValue("@Correo", correo);
                var dt = new DataTable();
                da.Fill(dt);
                return dt.Rows.Count == 0 ? null : dt.Rows[0];
            }
        }

        // Buscar usuario por Dui
        public DataRow ObtenerUsuarioPorDui(string dui)
        {
            using (var cn = new SqlConnection(connStr))
            using (var da = new SqlDataAdapter("SELECT * FROM Usuario WHERE Dui = @Dui", cn))
            {
                da.SelectCommand.Parameters.AddWithValue("@Dui", dui);
                var dt = new DataTable();
                da.Fill(dt);
                return dt.Rows.Count == 0 ? null : dt.Rows[0];
            }
        }

        // Insertar token (ya lo debes tener; aquí para confirmar firma)
        public void InsertarToken(int idUsuario, byte[] tokenHash, DateTime expiraEn, string ip = null)
        {
            using (var cn = new SqlConnection(connStr))
            using (var cmd = new SqlCommand(@"
                INSERT INTO TokensRestablecimiento (IdUsuario, TokenHash, ExpiraEn, IP_Solicitud, TipoSolicitud)
                VALUES (@IdUsuario, @hash, @exp, @ip, 1)", cn))
            {
                cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                cmd.Parameters.Add("@hash", SqlDbType.VarBinary, tokenHash.Length).Value = tokenHash;
                cmd.Parameters.AddWithValue("@exp", expiraEn);
                cmd.Parameters.AddWithValue("@ip", (object)ip ?? DBNull.Value);
                cn.Open(); cmd.ExecuteNonQuery();
            }
        }

        // Obtener último token por IdUsuario
        public DataRow ObtenerUltimoToken(int idUsuario)
        {
            using (var cn = new SqlConnection(connStr))
            using (var da = new SqlDataAdapter(@"
        SELECT TOP 1 * FROM TokensRestablecimiento
        WHERE IdUsuario = @IdUsuario
        ORDER BY FechaCreacion DESC", cn))
            {
                da.SelectCommand.Parameters.AddWithValue("@IdUsuario", idUsuario);
                var dt = new DataTable();
                da.Fill(dt);
                return dt.Rows.Count == 0 ? null : dt.Rows[0];
            }
        }

        // Marcar token como usado
        public void MarcarTokenUsado(int tokenId)
        {
            using (var cn = new SqlConnection(connStr))
            using (var cmd = new SqlCommand("UPDATE TokensRestablecimiento SET Usado = 1 WHERE Id = @Id", cn))
            {
                cmd.Parameters.AddWithValue("@Id", tokenId);
                cn.Open(); cmd.ExecuteNonQuery();
            }
        }

        // Actualizar contraseña (guarda ContrasenaCifrada y Fecha_Ultimo_Cambio)
        public void ActualizarContrasena(int idUsuario, byte[] contrasenaCifrada)
        {
            using (var cn = new SqlConnection(connStr))
            using (var cmd = new SqlCommand(@"
        UPDATE Usuario SET ContrasenaCifrada = @c, Fecha_Ultimo_Cambio = @now WHERE Id = @id", cn))
            {
                cmd.Parameters.Add("@c", SqlDbType.VarBinary).Value = (object)contrasenaCifrada ?? DBNull.Value;
                cmd.Parameters.AddWithValue("@now", DBNull.Value);
                cmd.Parameters.AddWithValue("@id", idUsuario);
                cn.Open(); cmd.ExecuteNonQuery();
            }
        }

        // Obtener contraseña cifrada (para mostrar si es necesario, o fallback a Clave)
        public byte[] ObtenerContrasenaCifrada(int idUsuario)
        {
            using (var cn = new SqlConnection(connStr))
            using (var cmd = new SqlCommand("SELECT ContrasenaCifrada FROM Usuario WHERE Id = @Id", cn))
            {
                cmd.Parameters.AddWithValue("@Id", idUsuario);
                cn.Open();
                var o = cmd.ExecuteScalar();
                return o == DBNull.Value || o == null ? null : (byte[])o;
            }
        }
    }

}

































