using demo.ethm.Infraestrutura.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace demo.ethm.Dominio.Entities.ValueObjects
{
    public class Cep : ValueObject, IValidatableObject
    {
        [NotMapped]
        private List<ValidationResult> validationResults = new List<ValidationResult>();

        public const int CepMaxLength = 8;
        public long? CepCod { get; private set; }

        protected Cep() { }

        public Cep(string cep, bool obrigatorio)
        {
            if (obrigatorio)
            {
                ValidationHelper.ForNullOrEmptyDefaultMessage("CEP", cep, ref validationResults);
            }

            if (!string.IsNullOrWhiteSpace(cep))
            {
                cep = TextHelper.GetNumeros(cep);
                ValidationHelper.StringLength("CEP", CepMaxLength, cep, ref validationResults);
                try
                {
                    CepCod = Convert.ToInt64(cep);
                }
                catch (Exception)
                {
                    validationResults.Add(new ValidationResult("Cep inválido: " + cep));
                }
            }
        }

        public bool Vazio()
        {
            return !CepCod.HasValue;
        }

        public string GetCepFormatado()
        {
            if (CepCod == null)
            {
                return "";
            }

            var cep = CepCod.ToString();

            while (cep.Length < 8)
            {
                cep = "0" + cep;
            }

            return cep.Substring(0, 5) + "-" + cep.Substring(5);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return validationResults;
        }
    }
}
