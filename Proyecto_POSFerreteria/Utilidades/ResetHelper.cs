using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_POSFerreteria.Utilidades
{
    public static class ResetHelper
    {
        public static string GenerarCodigo6Digitos()
        {
            var bytes = new byte[4];
            using (var rng = RandomNumberGenerator.Create()) rng.GetBytes(bytes);
            uint val = BitConverter.ToUInt32(bytes, 0);
            int code = (int)(val % 1000000);
            return code.ToString("D6");
        }

        public static byte[] HashToken(string token)
        {
            using (var sha = SHA256.Create())
            {
                return sha.ComputeHash(Encoding.UTF8.GetBytes(token));
            }
        }

        public static bool CompararHash(byte[] a, byte[] b)
        {
            if (a == null || b == null || a.Length != b.Length) return false;
            int diff = 0;
            for (int i = 0; i < a.Length; i++) diff |= a[i] ^ b[i];
            return diff == 0;
        }
    }
}