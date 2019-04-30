using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace demo.ethm.Infraestrutura.Extensions
{
    public static class EnumExtensionMethods
    {
        public static string GetDescription(this Enum GenericEnum)
        {
            Type genericEnumType = GenericEnum.GetType();
            MemberInfo[] memberInfo = genericEnumType.GetMember(GenericEnum.ToString());
            if ((memberInfo != null && memberInfo.Length > 0))
            {
                var _Attribs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if ((_Attribs != null && _Attribs.Count() > 0))
                {
                    return ((DescriptionAttribute)_Attribs.ElementAt(0)).Description;
                }
            }

            return GenericEnum.ToString();
        }

        public static string ObterDescricao(System.Enum value)
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return (attributes.Length > 0) ? attributes[0].Description : value.ToString();
        }

        public static IDictionary<object, string> ToDicionarioEnum(this System.Type enumType)
        {
            IDictionary<object, string> lista = new Dictionary<object, string>();

            foreach (System.Enum item in System.Enum.GetValues(enumType))
            {
                var valorEnum = item.GetType().GetField(item.ToString()).GetRawConstantValue();
                lista.Add(valorEnum, ObterDescricao(item));
            }

            return lista;
        }

        // TODO: Método para retornar o idioma correto
    }
}
