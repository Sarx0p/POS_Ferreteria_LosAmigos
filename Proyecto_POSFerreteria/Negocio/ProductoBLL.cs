using Proyecto_POSFerreteria.Datos;
using Proyecto_POSFerreteria.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_POSFerreteria.Negocio
{
    public class ProductoBLL
    {
        private readonly ProductoDAL dal = new ProductoDAL();

        public List<Producto> Listar()
        {
            return ProductoDAL.Listar();
        }

        public DataTable ListarParaGrid()
        {
            return dal.Listas(); 
        }

        public int Insertar(Producto p)
        {
            if (string.IsNullOrWhiteSpace(p.NombreProducto))
                throw new Exception("Nombre requerido.");

            if (dal.ExisteNombreProducto(p.NombreProducto))
                throw new Exception("Ya existe un producto con ese nombre.");

            return dal.Insertar(p);
        }

        public bool Actualizar(Producto p)
        {
            if (p.Id <= 0)
                throw new Exception("Id inválido.");

            if (string.IsNullOrWhiteSpace(p.NombreProducto))
                throw new Exception("Nombre requerido.");

            if (dal.ExisteNombreProductoEnOtro(p.NombreProducto, p.Id))
                throw new Exception("Otro producto ya tiene ese nombre.");

            return dal.Actualizar(p);
        }

        public bool Eliminar(int id)
        {
            if (id <= 0)
                throw new Exception("Id inválido.");

            if (dal.ProductoTieneVentasAsociadas(id))
                throw new Exception("No se puede eliminar: producto con ventas asociadas.");

            return dal.Eliminar(id);
        }

        public DataTable Buscar(string filtro)
        {
            return dal.Buscar(filtro);
        }
    }
}
