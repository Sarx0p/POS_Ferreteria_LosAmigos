using Proyecto_POSFerreteria.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_POSFerreteria.Negocio
{
    public class RecuperacionBLL
    {
        private readonly RecuperacionDAL dal = new RecuperacionDAL();

        // Enviar por username
        public void EnviarSolicitudPorUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new Exception("Debe ingresar un usuario");

            int? IdUsuario = dal.ObtenerIdUsuarioPorUsername(username);
            if (!IdUsuario.HasValue)
                throw new Exception("No se encontró el usuario especificado.");

            dal.InsertarSolicitud(IdUsuario.Value);
        }

        // Enviar por nombre + apellido
        public void EnviarSolicitudPorNombreApellido(string nombre, string apellido)
        {
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellido))
                throw new Exception("Debe ingresar nombre y apellido");

            int? idUsuario = dal.ObtenerIdUsuarioPorNombreApellido(nombre, apellido);
            if (!idUsuario.HasValue)
                throw new Exception("No se encontró un usuario con ese nombre y apellido");

            dal.InsertarSolicitud(idUsuario.Value);
        }
    }
}