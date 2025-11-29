using System;
using System.Data;
using System.Data.SqlClient;
using Proyecto_POSFerreteria.Datos;
using Proyecto_POSFerreteria.Entidades;
using Proyecto_POSFerreteria.Negocio;

namespace Proyecto_POSFerreteria.Negocio
{
    public class UsuarioBLL
    {
            UsuarioDAL dal = new UsuarioDAL();

            public Usuario Login(string nombre, string clave)
            {
                if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(clave))
                    throw new Exception("Debe ingresar usuario y Clave.");

                Usuario u = dal.Login(nombre, clave);

                if (u == null)
                    throw new Exception("Usuario o Clave incorrectos.");

                return u;
            }
        }
    }


