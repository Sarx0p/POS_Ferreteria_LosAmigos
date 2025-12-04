using Proyecto_POSFerreteria.Entidades;
using Proyecto_POSFerreteria.Presentacion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_POSFerreteria.Datos
{
    public class ClienteDAL
    {


        public DataTable Listar()
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(Conexion.Cadena))
            {
                string sql = "SELECT Id, NombreCompleto FROM Cliente";


                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cn.Open();
                    new SqlDataAdapter(cmd).Fill(dt);

                }
            }
            return dt;
        }

        public int Insertar(Cliente c)
        {
            using (SqlConnection cn = new SqlConnection(Conexion.Cadena))
            {
                string sql = "INSERT INTO Cliente (NombreCompleto) VALUES (@NombreCompleto); SELECT SCOPE_IDENTITY();";

                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@NombreCompleto", c.NombreCompleto);
                    cn.Open();

                    int idCliente = Convert.ToInt32(cmd.ExecuteScalar());
                    return idCliente;
                }
            }
        }


        public bool Editar(Cliente c)
        {
            using (SqlConnection cn = new SqlConnection(Conexion.Cadena))
            {
                string sql = "UPDATE Cliente SET NombreCompleto = @NombreCompleto WHERE Id = @Id";
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@Id", c.Id);
                    cmd.Parameters.AddWithValue("@NombreCompleto", c.NombreCompleto);
                    cn.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
        }

        public bool Eliminar(int id)
        {
            using (SqlConnection cn = new SqlConnection(Conexion.Cadena))
            {
                string sql = "DELETE FROM Cliente WHERE Id = @Id";
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cn.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
        }

        public DataTable Buscar(string filtro)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(Conexion.Cadena))
            {
                string sql = "SELECT Id, NombreCompleto FROM Cliente WHERE NombreCompleto LIKE @Nombre";
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@filtro", "%" + filtro + "%");
                    cn.Open();
                    new SqlDataAdapter(cmd).Fill(dt);
                }
            }
            return dt;
        }


        public static List<Cliente> ObtenerClientes()
        {
            List<Cliente> clientes = new List<Cliente>();
            using (SqlConnection con = new SqlConnection(Conexion.Cadena))
            {
                string sql = "SELECT Id, NombreCompleto FROM Cliente";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            clientes.Add(new Cliente
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                NombreCompleto = reader["NombreCompleto"].ToString()
                            });
                        }
                    }
                }
            }
            return clientes;
        }


    }





















}

