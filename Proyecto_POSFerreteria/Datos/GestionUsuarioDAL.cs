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
    public class GestionUsuarioDAL
    {
        private readonly string connStr = Conexion.Cadena;

        // LISTAR todos los usuarios
        public List<Usuario> Listar()
        {
            var lista = new List<Usuario>();
            string sql = @"SELECT Id, Nombre, Apellido, Username, Dui, Correo, Rol, Estado, ContrasenaCifrada
                           FROM Usuario";

            using (var cn = new SqlConnection(connStr))
            using (var cmd = new SqlCommand(sql, cn))
            {
                cn.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var u = new Usuario
                        {
                            IdUsuario = Convert.ToInt32(dr["Id"]),
                            Nombre = dr["Nombre"]?.ToString(),
                            Apellido = dr["Apellido"]?.ToString(),
                            Username = dr["Username"]?.ToString(),
                            Dui = dr["Dui"]?.ToString(),
                            Correo = dr["Correo"]?.ToString(),
                            Rol = dr["Rol"]?.ToString(),
                            ContrasenaCifrada = dr["ContrasenaCifrada"] == DBNull.Value ? null : (byte[])dr["ContrasenaCifrada"]
                        };
                        lista.Add(u);
                    }
                }
            }
            return lista;
        }



        // OBTENER POR USERNAME (exacto)
        public Usuario ObtenerPorUsername(string username)
        {
            string sql = @"SELECT Id, Nombre, Apellido, Username, Dui, Correo, Rol, Estado, ContrasenaCifrada
                           FROM Usuario WHERE Username = @u";

            using (var cn = new SqlConnection(connStr))
            using (var cmd = new SqlCommand(sql, cn))
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
                        Dui = dr["Dui"]?.ToString(),
                        Correo = dr["Correo"]?.ToString(),
                        Rol = dr["Rol"]?.ToString(),
                        ContrasenaCifrada = dr["ContrasenaCifrada"] == DBNull.Value ? null : (byte[])dr["ContrasenaCifrada"]
                    };
                }
            }
        }

        // OBTENER por username diferente (para validar duplicado al actualizar)
        public Usuario ObtenerPorUsernameDiferente(string username, int idExcluido)
        {
            string sql = @"SELECT TOP 1 Id, Username FROM Usuario WHERE Username = @u AND Id <> @id";
            using (var cn = new SqlConnection(connStr))
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@u", username);
                cmd.Parameters.AddWithValue("@id", idExcluido);
                cn.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    if (!dr.Read()) return null;
                    return new Usuario { IdUsuario = Convert.ToInt32(dr["Id"]), Username = dr["Username"].ToString() };
                }
            }
        }

        // INSERTAR — recibe clave cifrada como byte[] (así lo usamos desde UI/BLL)
        public int Insertar(string username, byte[] claveCifrada, string rol, string nombre, string apellido, string dui, string correo)
        {
            string sql = @"
                INSERT INTO Usuario (Nombre, Apellido, Username, Dui, Correo, Rol, Estado, ContrasenaCifrada)
                VALUES (@nombre, @apellido, @username, @dui, @correo, @rol, 1, @clave);
                SELECT SCOPE_IDENTITY();";

            using (var cn = new SqlConnection(connStr))
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@nombre", (object)nombre ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@apellido", (object)apellido ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@dui", (object)dui ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@correo", (object)correo ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@rol", rol);
                cmd.Parameters.Add("@clave", SqlDbType.VarBinary).Value = (object)claveCifrada ?? DBNull.Value;

                cn.Open();
                object res = cmd.ExecuteScalar();
                return Convert.ToInt32(res);
            }
        }

        // ACTUALIZAR — si claveCifrada == null => no cambiarla
        public bool Actualizar(int id, string username, string nombre, string apellido, string dui, string correo, string rol, bool estado, byte[] claveCifrada = null)
        {
            string sql;
            if (claveCifrada == null)
            {
                sql = @"UPDATE Usuario SET Nombre=@nombre, Apellido=@apellido, Username=@username, Dui=@dui, Correo=@correo, Rol=@rol, Estado=@estado WHERE Id=@id";
            }
            else
            {
                sql = @"UPDATE Usuario SET Nombre=@nombre, Apellido=@apellido, Username=@username, Dui=@dui, Correo=@correo, Rol=@rol, Estado=@estado, ContrasenaCifrada=@clave, Fecha_Ultimo_Cambio=SYSDATETIME() WHERE Id=@id";
            }

            using (var cn = new SqlConnection(connStr))
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@nombre", (object)nombre ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@apellido", (object)apellido ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@dui", (object)dui ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@correo", (object)correo ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@rol", rol);
                cmd.Parameters.AddWithValue("@estado", estado);
                cmd.Parameters.AddWithValue("@id", id);

                if (claveCifrada != null)
                    cmd.Parameters.Add("@clave", SqlDbType.VarBinary).Value = claveCifrada;

                cn.Open();
                int affected = cmd.ExecuteNonQuery();
                return affected > 0;
            }
        }

        // CAMBIAR CLAVE (si necesitas método específico)
        public void CambiarClave(int id, byte[] claveCifrada)
        {
            string sql = @"UPDATE Usuario SET ContrasenaCifrada = @clave, Fecha_Ultimo_Cambio = SYSDATETIME() WHERE Id = @id";
            using (var cn = new SqlConnection(connStr))
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add("@clave", SqlDbType.VarBinary).Value = claveCifrada ?? (object)DBNull.Value;
                cmd.Parameters.AddWithValue("@id", id);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // ELIMINAR
        public bool Eliminar(int id)
        {
            string sql = "DELETE FROM Usuario WHERE Id = @id";
            using (var cn = new SqlConnection(connStr))
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cn.Open();
                int aff = cmd.ExecuteNonQuery();
                return aff > 0;
            }
        }

        // LOGIN — compara clave (se asume que se recibe clavePlano y comparación la hace aquí)
        public Usuario Login(string username, string clavePlano)
        {
            using (var cn = new SqlConnection(connStr))
            using (var cmd = new SqlCommand("SELECT Id, Nombre, Apellido, Username, Rol, Estado, ContrasenaCifrada, Clave FROM Usuario WHERE Username=@u", cn))
            {
                cmd.Parameters.AddWithValue("@u", username);
                cn.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    if (!dr.Read()) return null;
                    int id = Convert.ToInt32(dr["Id"]);
                    string storedPlain = null;

                    if (!dr.IsDBNull(dr.GetOrdinal("ContrasenaCifrada")))
                    {
                        var blob = (byte[])dr["ContrasenaCifrada"];
                        storedPlain = Proyecto_POSFerreteria.Utilidades.CryptoDPAPI.DesencriptarContrasena(blob);
                        if (storedPlain == clavePlano)
                        {
                            return new Usuario
                            {
                                IdUsuario = id,
                                Nombre = dr["Nombre"]?.ToString(),
                                Apellido = dr["Apellido"]?.ToString(),
                                Username = dr["Username"]?.ToString(),
                                Rol = dr["Rol"]?.ToString(),
                            };
                        }
                        else return null;
                    }

                    // fallback to plain Clave column
                    if (!dr.IsDBNull(dr.GetOrdinal("Clave")))
                    {
                        string claveBD = dr["Clave"].ToString();
                        if (claveBD == clavePlano)
                        {
                            return new Usuario
                            {
                                IdUsuario = id,
                                Nombre = dr["Nombre"]?.ToString(),
                                Apellido = dr["Apellido"]?.ToString(),
                                Username = dr["Username"]?.ToString(),
                                Rol = dr["Rol"]?.ToString(),
                               
                            };
                        }
                    }
                }
            }
            return null;
        }
    }
}
    

