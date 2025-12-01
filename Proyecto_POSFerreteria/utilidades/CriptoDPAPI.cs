using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_POSFerreteria.Utilidades
{
    public static class CryptoDPAPI 
    { public static byte[] CifrarContrasena(string contrasena)
        { 
            if (contrasena == null) throw new ArgumentNullException(nameof(contrasena)); 
            var datos = Encoding.UTF8.GetBytes(contrasena); 
            return ProtectedData.Protect(datos, null, DataProtectionScope.LocalMachine);
        }
        public static string DesencriptarContrasena(byte[] blob) 
        { 
            if (blob == null) return null;
            var dec = ProtectedData.Unprotect(blob, null, DataProtectionScope.LocalMachine);
            return Encoding.UTF8.GetString(dec); } }
}
