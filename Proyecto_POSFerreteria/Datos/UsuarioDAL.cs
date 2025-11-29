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
<<<<<<< HEAD
                string sql = @"SELECT Id, Username, Clave, Rol
                               FROM Usuario
                               WHERE Username = @Username AND Clave = @Clave";

                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@Username", nombre);
=======
                string sql = @"SELECT Id, Nombre, Clave, Rol
                               FROM Usuario
                               WHERE Nombre = @Nombre AND Clave = @Clave";

                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@Nombre", nombre);
>>>>>>> master
                    cmd.Parameters.AddWithValue("@Clave", clave);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return new Usuario
                            {
<<<<<<< HEAD
                                Id = Convert.ToInt32(dr["Id"]),
                                Nombre = dr["Username"].ToString(),
                                Clave = dr["Clave"].ToString(),
=======
                                IdUsuario = Convert.ToInt32(dr["Id"]),
                                Nombre = dr["Nombre"].ToString(),
                                Clave = dr["Clave"].ToString(), 
>>>>>>> master
                                Rol = dr["Rol"].ToString()
                            };
                        }
                    }
                }
            }

            return null; // usuario NO encontrado
        }
    }
}