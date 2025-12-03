using Proyecto_POSFerreteria.Datos;
using Proyecto_POSFerreteria.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_POSFerreteria.Negocio
{
    public class GestionUsuarioBLL
    {
        private readonly GestionUsuarioDAL dal;
        public GestionUsuarioBLL()
        {
            dal = new GestionUsuarioDAL();
        }

        public List<Usuario> Listar => dal.Listar();

        // Insertar acepta clave cifrada (byte[])
        public int Insertar(string username, byte[] claveCifrada, string rol, string nombre = null, string apellido = null, string dui = null, string correo = null)
        {
            if (string.IsNullOrWhiteSpace(username)) throw new ArgumentException("Username requerido");
            if (string.IsNullOrWhiteSpace(rol)) throw new ArgumentException("Rol requerido");

            var existing = dal.ObtenerPorUsername(username);
            if (existing != null) throw new Exception("Usuario ya existe.");

            return dal.Insertar(username, claveCifrada, rol, nombre, apellido, dui, correo);
        }

        // Actualizar (claveCifrada opcion)
        public bool Actualizar(int id, string username, string nombre, string apellido, string dui, string correo, string rol, bool estado, byte[] claveCifrada = null)
        {
            if (id <= 0) throw new ArgumentException("Id inválido");
            if (string.IsNullOrWhiteSpace(username)) throw new ArgumentException("Username requerido");

            

            return dal.Actualizar(id, username, nombre, apellido, dui, correo, rol, estado, claveCifrada);
        }

        public bool Eliminar(int id)
        {
            if (id <= 0) throw new ArgumentException("Id inválido");
            return dal.Eliminar(id);
        }

        public bool CambiarClave(int id, byte[] claveCifrada)
        {
            if (id <= 0) throw new ArgumentException("Id inválido");
            dal.CambiarClave(id, claveCifrada);
            return true;
        }

        public Usuario Login(string username, string clavePlain)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(clavePlain))
                throw new ArgumentException("Usuario y contraseña son requeridos.");

            return dal.Login(username, clavePlain);
        }
    }
}
    

