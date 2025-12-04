using Proyecto_POSFerreteria.Datos;
using Proyecto_POSFerreteria.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_POSFerreteria.Negocio.Hembert
{
    public class ClienteBLL
    {
        ClienteDAL dal = new ClienteDAL();

        public DataTable Listar()
        {
            return dal.Listar();
        }


        public int Guardar(Cliente c)
        {
            if (string.IsNullOrWhiteSpace(c.NombreCompleto))
            {
                throw new Exception("El Nombre del Cliente es Obligatorio.");
            }

            if (c.Id == 0)
            {
                return dal.Insertar(c);
            }
            else
            {
                return c.Id;

            }
        }

        public bool Eliminar(int id)
        {
            if (id <= 0)
            {
                throw new Exception("ID de Cliente inválido.");
            }
            return dal.Eliminar(id);
        }

        public DataTable BuscarPorNombre(string nombre)
        {

            return dal.Buscar(nombre);
        }

        public bool Editar(Cliente c)
        {
            if (c.Id <= 0)
            {
                throw new Exception("ID de Cliente inválido.");
            }
            return dal.Editar(c);

        }



    }
}
