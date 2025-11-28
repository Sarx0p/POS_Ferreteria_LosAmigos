using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_POSFerreteria.Datos
{
    public class RecuperacionDAL
    
        {
            private readonly string cadena = Conexion.Cadena;

            // 1) Get Id by Username
            public int ObtenerIdUsuarioPorUsername(string username)
            {
                string sql = "SELECT Id FROM Usuario WHERE Username = @Username";

                using (SqlConnection cn = new SqlConnection(cadena))
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);

                    cn.Open();
                    object result = cmd.ExecuteScalar();

                    if (result == null || result == DBNull.Value)
                        return -1; // NOT FOUND

                    return Convert.ToInt32(result);
                }
            }

            // 2) Get Id by Nombre + Apellido
            public int ObtenerIdUsuarioPorNombreApellido(string nombre, string apellido)
            {
                string sql = "SELECT Id FROM Usuario WHERE Nombre=@Nombre AND Apellido=@Apellido";

                using (SqlConnection cn = new SqlConnection(cadena))
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@Nombre", nombre);
                    cmd.Parameters.AddWithValue("@Apellido", apellido);

                    cn.Open();
                    object result = cmd.ExecuteScalar();

                    if (result == null || result == DBNull.Value)
                        return -1;

                    return Convert.ToInt32(result);
                }
            }

            // 3) Insert into SolicitudesRecuperacion using Id_Usuario
            public void InsertarSolicitud(int IdUsuario)
            {
                string sql = "INSERT INTO SolicitudesRecuperacion (IdUsuario, Fecha, Estado) " +
                             "VALUES (@IdUsuario, @Fecha, @Estado)";

                using (SqlConnection cn = new SqlConnection(cadena))
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                    cmd.Parameters.AddWithValue("@Fecha", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Estado", "Pendiente");

                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        }
