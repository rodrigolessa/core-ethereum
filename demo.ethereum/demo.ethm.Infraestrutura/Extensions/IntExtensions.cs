using System;
using System.Collections.Generic;
using System.Text;

namespace demo.ethm.Infraestrutura.Extensions
{
    public static class IntExtensions
    {
        public static bool EhPar(this int numero)
        {
            return (numero % 2 == 0);
        }
    }
}
