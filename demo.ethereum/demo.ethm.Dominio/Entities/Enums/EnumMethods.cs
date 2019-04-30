using demo.ethm.Infraestrutura.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace demo.ethm.Dominio.Entities.Enums
{
    public static class EnumMethods
    {
        public static string ObterTiposDeUsuario(int IdIdioma)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (TipoDeUsuario colorEnum in Enum.GetValues(typeof(TipoDeUsuario)))
            {
                stringBuilder.Append(colorEnum.GetDescription() + "|");
            }
            return stringBuilder.ToString();
        }
    }
}
