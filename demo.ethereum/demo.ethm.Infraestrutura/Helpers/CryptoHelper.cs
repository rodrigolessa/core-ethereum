using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace demo.ethm.Infraestrutura.Helpers
{
    public class CryptoHelper
    {
        public static byte[] CriptografarSenha(string senha)
        {
            return Criptografar(senha, "eckhart-ledger-darknight-3698");
        }

        public static byte[] Criptografar(string texto, string salt)
        {
            while (salt.Length < 6)
            {
                salt += salt + "Z";
            }
            using (var sha = SHA512.Create())
            {
                salt = Encoding.UTF8.GetString(sha.ComputeHash(Encoding.UTF8.GetBytes(salt.Substring(salt.Length - 4))));
                return sha.ComputeHash(Encoding.UTF8.GetBytes(texto + salt));
            }
        }

        /// <summary>
        /// Criptografia SHA512 com string length 128
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string CriptografarSHA512(string text)
        {
            var data = Encoding.ASCII.GetBytes(text);
            var hashData = new SHA512Managed().ComputeHash(data);
            var hash = string.Empty;
            foreach (var b in hashData)
                hash += b.ToString("X2");

            return hash;
        }

        public static string GerarToken(string salt)
        {
            return CriptografarSHA512(salt + DateTime.Now.ToString("yyyyMMddHHmmssffff") + Guid.NewGuid().ToString());
        }
    }
}
