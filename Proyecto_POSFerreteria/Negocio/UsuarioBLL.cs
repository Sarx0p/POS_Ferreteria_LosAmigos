using Proyecto_POSFerreteria.Datos;
using Proyecto_POSFerreteria.Entidades;
using Proyecto_POSFerreteria.Negocio;
using Proyecto_POSFerreteria.Utilidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Proyecto_POSFerreteria.Negocio
{
   
        public static class UsuarioBLL
        {
            // Instancia única del DAL (corta, limpia)
            private static readonly UsuarioDAL dal = new UsuarioDAL();

            // LOGIN
            public static Usuario Login(string usuario, string clave)
            {
                if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(clave))
                    throw new ArgumentException("Debe ingresar usuario y contraseña.");

                return dal.Login(usuario.Trim(), clave);
            }

            // LISTAR
            public static List<Usuario> Listar()
            {
                return UsuarioDAL.Listar( );
            }

            // INSERTAR
            public static int Insertar(string nombre, string apellido, string username, string clave,
                                       string rol, string correo, string dui,
                                       int diasMinimos = 90, bool debeCambiar = false)
            {
                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(clave))
                    throw new ArgumentException("Usuario y contraseña requeridos.");

                byte[] blob = CryptoDPAPI.CifrarContrasena(clave);

                return UsuarioDAL.Insertar(nombre?.Trim(), apellido?.Trim(), username.Trim(), rol,
                                    correo?.Trim(), dui?.Trim(), blob,
                                    diasMinimos, debeCambiar);
            }

            // ACTUALIZAR (sin cambiar contraseña)
            public static bool Actualizar(int id, string nombre, string apellido, string username,
                                          string rol, string correo, string dui,
                                          int diasMinimos = 90, bool debeCambiar = false)
            {
                if (id <= 0) throw new ArgumentException("Id inválido.");
                if (string.IsNullOrWhiteSpace(username)) throw new ArgumentException("Nombre de usuario requerido.");

                return UsuarioDAL.Actualizar(id, nombre?.Trim(), apellido?.Trim(), username.Trim(), rol,
                                      correo?.Trim(), dui?.Trim(),
                                      diasMinimos, debeCambiar);
            }

            // ELIMINAR
            public static bool Eliminar(int id)
            {
                if (id <= 0) throw new ArgumentException("Id inválido.");
                return UsuarioDAL.Eliminar(id);
            }

            // CAMBIAR CONTRASEÑA
            public static bool CambiarContrasena(int id, string nuevaClave, bool debeCambiar = false)
            {
                if (id <= 0) throw new ArgumentException("Id inválido.");
                if (string.IsNullOrWhiteSpace(nuevaClave))
                    throw new ArgumentException("La nueva contraseña no puede estar vacía.");

                byte[] blob = CryptoDPAPI.CifrarContrasena(nuevaClave);

                return UsuarioDAL.CambiarContrasena(id, blob, debeCambiar);
            }



        // NAO

        // metodos para el correo
        private static readonly string smtpHost = "smtp.gmail.com";
        private static readonly int smtpPort = 587;
        private static readonly string smtpUser = "ferreterialosamigossv@gmail.com";
        private static readonly string smtpPass = "jfgc ceve fqxl kdhv"; // clave APP
        private static readonly string fromAddress = "ferreterialosamigossv@gmail.com";

      




        // Tipo de recordatorio
        public enum TipoRecordatorio { RecordarUsuario = 0, RecordarContrasena = 1 }

        // Generar token por identificador (correo preferido, si pasa DUI busca usuario y usa su Correo)
        public static void GenerarTokenPorIdentificador(string identificador)
        {
            if (string.IsNullOrWhiteSpace(identificador)) throw new Exception("Ingrese correo o DUI.");

            DataRow user = null;
            bool esCorreo = false;
            try { var addr = new System.Net.Mail.MailAddress(identificador); esCorreo = addr.Address == identificador; }
            catch { esCorreo = false; }

            if (esCorreo) user = dal.ObtenerUsuarioPorCorreo(identificador);
            else user = dal.ObtenerUsuarioPorDui(identificador);

            if (user == null) throw new Exception("No se encontró una cuenta con esa identificación.");

            int idUsuario = Convert.ToInt32(user["Id"]);

            // preferimos enviar al Correo registrado
            string correo = user.Table.Columns.Contains("Correo") && user["Correo"] != DBNull.Value
                            ? user["Correo"].ToString() : null;

            if (string.IsNullOrWhiteSpace(correo))
            {
                // no hay correo -> registrar solicitud o avisar al usuario
                throw new Exception("No existe correo asociado. Su solicitud será revisada por el administrador.");
            }

            string codigo = ResetHelper.GenerarCodigo6Digitos();
            byte[] hash = ResetHelper.HashToken(codigo);
            DateTime expira = DateTime.UtcNow.AddMinutes(15);

            dal.InsertarToken(idUsuario, hash, expira);



            var mail = new MailService(smtpHost, smtpPort, smtpUser, smtpPass, fromAddress);

            string body = $"Su código de verificación es: {codigo}\nCaduca en 15 minutos.";
            mail.Send(correo, "Código de verificación - Ferretería", body);
        }

        // Validar token y decidir si permite cambio o mostrar usuario/contraseña
        public static (string Username, string Contrasena, bool CanChange, string Mensaje, int IdUsuario) ValidarTokenYDecidir(string identificador, string codigoIngresado, TipoRecordatorio tipo)
        {
            if (string.IsNullOrWhiteSpace(identificador) || string.IsNullOrWhiteSpace(codigoIngresado))
                throw new Exception("Identificador y código son obligatorios.");

            DataRow user = null;
            bool esCorreo = false;
            try { var addr = new System.Net.Mail.MailAddress(identificador); esCorreo = addr.Address == identificador; }
            catch { esCorreo = false; }

            if (esCorreo) user = dal.ObtenerUsuarioPorCorreo(identificador);
            else user = dal.ObtenerUsuarioPorDui(identificador);

            if (user == null) throw new Exception("No se encontró la cuenta.");

            int idUsuario = Convert.ToInt32(user["Id"]);

            DataRow tokenRow = dal.ObtenerUltimoToken(idUsuario);
            if (tokenRow == null) throw new Exception("No existe un código generado. Solicítelo primero.");

            bool usado = Convert.ToBoolean(tokenRow["Usado"]);
            DateTime expira = Convert.ToDateTime(tokenRow["ExpiraEn"]);
            byte[] hashDB = (byte[])tokenRow["TokenHash"];

            if (usado) throw new Exception("El código ya fue usado.");
            if (DateTime.UtcNow > expira) throw new Exception("El código expiró.");

            byte[] hashIngresado = ResetHelper.HashToken(codigoIngresado);
            if (!ResetHelper.CompararHash(hashDB, hashIngresado)) throw new Exception("Código incorrecto.");

            // marcar token como usado
            dal.MarcarTokenUsado(Convert.ToInt32(tokenRow["Id"]));

            string username = user.Table.Columns.Contains("Username") ? user["Username"].ToString() : null;
            string contrasenaDesencriptada = null;
            if (tipo == TipoRecordatorio.RecordarContrasena)
            {
                byte[] blob = dal.ObtenerContrasenaCifrada(idUsuario);
                if (blob != null) contrasenaDesencriptada = CryptoDPAPI.DesencriptarContrasena(blob);
                else if (user.Table.Columns.Contains("Clave") && user["Clave"] != DBNull.Value)
                    contrasenaDesencriptada = user["Clave"].ToString();
            }

            // reglas para permitir cambio
            bool permiteCambio = user.Table.Columns.Contains("PermiteCambioPorUsuario") && user["PermiteCambioPorUsuario"] != DBNull.Value
                                 ? Convert.ToBoolean(user["PermiteCambioPorUsuario"])
                                 : true;

            int diasMinimos = user.Table.Columns.Contains("Dias_Minimos_Entre_Cambios") && user["Dias_Minimos_Entre_Cambios"] != DBNull.Value
                              ? Convert.ToInt32(user["Dias_Minimos_Entre_Cambios"]) : 30;

            DateTime? fechaUltimo = user.Table.Columns.Contains("Fecha_Ultimo_Cambio") && user["Fecha_Ultimo_Cambio"] != DBNull.Value
                                    ? (DateTime?)Convert.ToDateTime(user["Fecha_Ultimo_Cambio"]) : null;

            bool diasCumplidos = true;
            string mensaje = "Token válido.";

            if (fechaUltimo.HasValue)
            {
                double diasTrans = (DateTime.UtcNow - fechaUltimo.Value).TotalDays;
                if (diasTrans < diasMinimos)
                {
                    diasCumplidos = false;
                    double faltan = Math.Ceiling(diasMinimos - diasTrans);
                    mensaje = $"Aún no puede cambiar la contraseña. Faltan {faltan} día(s). Contacte con el administrador.";
                }
            }

            bool canChange = permiteCambio && diasCumplidos;

            if (!permiteCambio && tipo == TipoRecordatorio.RecordarContrasena) mensaje = "Su cuenta no permite que cambie la contraseña. Contacte con su administrador.";

            return (Username: username, Contrasena: contrasenaDesencriptada, CanChange: canChange, Mensaje: mensaje, IdUsuario: idUsuario);
        }

        // Cambiar contraseña por Id (se usa si CanChange == true)
        public static void CambiarContrasenaPorId(int idUsuario, string nuevaContrasena)
        {
            if (idUsuario <= 0) throw new Exception("Id inválido.");
            if (string.IsNullOrWhiteSpace(nuevaContrasena)) throw new Exception("Contraseña vacía.");

            byte[] cifrada = CryptoDPAPI.CifrarContrasena(nuevaContrasena);
            dal.ActualizarContrasena(idUsuario, cifrada);
        }
    }
}




