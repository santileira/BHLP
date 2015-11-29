using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AerolineaFrba
{
    class Encriptador
    {

        // Método que genera el código hash para la contraseña antes de enviarlo a la BD
        public static string encriptarSegunSHA256(string cadena)
        {
            SHA256CryptoServiceProvider provider = new SHA256CryptoServiceProvider();

            byte[] bytesEntrada = Encoding.UTF8.GetBytes(cadena);
            byte[] bytesEncriptados = provider.ComputeHash(bytesEntrada);

            StringBuilder cadenaEncriptada = new StringBuilder();

            for (int i = 0; i < bytesEncriptados.Length; i++)
            {
                cadenaEncriptada.Append(bytesEncriptados[i].ToString("x2").ToLower());
            }

            return cadenaEncriptada.ToString();
        }  
    }
}
