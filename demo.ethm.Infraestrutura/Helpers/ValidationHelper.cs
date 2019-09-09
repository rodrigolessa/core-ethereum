using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace demo.ethm.Infraestrutura.Helpers
{
    public class ValidationHelper
    {
        public static void ForValidId(string propName, Guid id, ref List<ValidationResult> result)
        {
            ForValidId(id, propName + " id inválido!", ref result);
        }

        public static void ForValidId(Guid id, string errorMessage, ref List<ValidationResult> result)
        {
            if (id == Guid.Empty)
            {
                result.Add(new ValidationResult(errorMessage));
            }
        }

        public static void ForValidId(string propName, int id, ref List<ValidationResult> result)
        {
            ForValidId(id, propName + " id inválido!", ref result);
        }

        public static void ForValidId(int id, string errorMessage, ref List<ValidationResult> result)
        {
            if (!(id > 0))
            {
                result.Add(new ValidationResult(errorMessage));
            }
        }

        public static void ForNegative(int number, string propName, ref List<ValidationResult> result)
        {
            if (number < 0)
            {
                result.Add(new ValidationResult(propName + " não pode ser negativo!"));
            }
        }

        /// <summary>
        /// O Campo deve ser obrigatório
        /// </summary>
        /// <param name="value"></param>
        /// <param name="propName"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static void ForNullOrEmptyDefaultMessage(string value, string propName, ref List<ValidationResult> result)
        {            // TODO: Replacing Throwing Exceptions with Notification in Validations. https://martinfowler.com/articles/replaceThrowWithNotification.html
            if (string.IsNullOrWhiteSpace(value))
            {
                result.Add(new ValidationResult(propName + " é obrigatório!"));
            }
        }

        public static void ForNullOrEmpty(string value, string errorMessage, ref List<ValidationResult> result)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                result.Add(new ValidationResult(errorMessage));
            }
        }

        public static void StringLength(string propName, string stringValue, int maximum, ref List<ValidationResult> result)
        {
            StringLength(stringValue, maximum, propName + " não pode conter mais que " + maximum + " caracteres", ref result);
        }

        public static void StringLength(string stringValue, int maximum, string message, ref List<ValidationResult> result)
        {
            int length = stringValue.Length;
            if (length > maximum)
            {
                result.Add(new ValidationResult(message));
            }
        }

        public static void StringLength(string propName, string stringValue, int minimum, int maximum, ref List<ValidationResult> result)
        {
            StringLength(stringValue, minimum, maximum, propName + " deve ter de " + minimum + " à " + maximum + " caracteres!", ref result);
        }

        public static void StringLength(string stringValue, int minimum, int maximum, string message, ref List<ValidationResult> result)
        {
            if (string.IsNullOrWhiteSpace(stringValue))
            {
                stringValue = string.Empty;
            }

            int length = stringValue.Length;
            if (length < minimum || length > maximum)
            {
                result.Add(new ValidationResult(message));
            }
        }

        public static void AreEqual(string a, string b, string errorMessage, ref List<ValidationResult> result)
        {
            if (a != b)
            {
                result.Add(new ValidationResult(errorMessage));
            }
        }

        public static void EmailIsValid(string email, ref List<ValidationResult> result)
        {
            var regexEmail = new Regex(@"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
            if (regexEmail.IsMatch(email))
            {
                result.Add(new ValidationResult("E-mail inválido"));
            }
        }
    }
}
