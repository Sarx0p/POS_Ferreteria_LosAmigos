using Proyecto_POSFerreteria.Datos;
using Proyecto_POSFerreteria.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    namespace Proyecto_POSFerreteria.Datos
    {
    public class UsuarioDAL
    {
        public Usuario Login(string nombre, string clave)
        {
            using (SqlConnection cn = new SqlConnection(Conexion.Cadena))
            {
                string sql = @"SELECT Id, Nombre, Contraseña, Rol
                               FROM Usuario
                               WHERE Nombre = @nombre AND Contraseña = @clave";

                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@clave", clave);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return new Usuario
                            {
                                IdUsuario = Convert.ToInt32(dr["Id"]),
                                Nombre = dr["Nombre"].ToString(),
                                Contraseña = dr["Contraseña"].ToString(),
                                Rol = dr["Rol"].ToString()
                            };
                        }
                    }
                }
            }

            return null;
        }
    }
}