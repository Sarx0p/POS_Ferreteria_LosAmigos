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
        public Usuario Login(string username, string claveIngresada)
        {
            using (var cn = new SqlConnection(connStr))
            {
                string sql = @"SELECT Id, Nombre, Username, Rol, ContrasenaCifrada, Clave
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

                        // Si existe ContrasenaCifrada -> desencriptar y comparar
                        if (!dr.IsDBNull(dr.GetOrdinal("ContrasenaCifrada")))
                        {
                            var blob = (byte[])dr["ContrasenaCifrada"];
                            string contrasenaPlano = Proyecto_POSFerreteria.Utilidades.CryptoDPAPI.DesencriptarContrasena(blob);
                            if (contrasenaPlano == claveIngresada)
                            {
                                return new Usuario { IdUsuario = id, Nombre = nombre, Rol = rol, Username = dr["Username"]?.ToString() };
                            }
                            else
                            {
                                return null; // contraseña incorrecta
                            }
                        }

                        // Fallback: comparar con la columna Clave (texto)
                        if (!dr.IsDBNull(dr.GetOrdinal("Clave")))
                        {
                            string claveBD = dr["Clave"].ToString();
                            if (claveBD == claveIngresada)
                                return new Usuario { IdUsuario = id, Nombre = nombre, Rol = rol, Username = dr["Username"]?.ToString() };
                        }

                        return null;
                    }
                }
            }
        }


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
        cmd.Parameters.AddWithValue("@now", DateTime.UtcNow);
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
