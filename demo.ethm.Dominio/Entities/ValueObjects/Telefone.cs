using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using demo.ethm.Infraestrutura.Helpers;

namespace demo.ethm.Dominio.Entities.ValueObjects
{
    public class Telefone : ValueObject, IValidatableObject
    {
        [NotMapped]
        private List<ValidationResult> validationResults = new List<ValidationResult>();

        public const int NumeroMaxLength = 10;
        public string Numero { get; private set; }

        public const int DDDMaxLength = 2;
        public string DDD { get; private set; }

        // Contrutor para o entityFramework
        protected Telefone() { }

        public Telefone(string ddd, string numero)
        {
            SetTelefone(numero);
            SetDDD(ddd);
        }

        private void SetTelefone(string numero)
        {
            if (string.IsNullOrEmpty(numero))
                numero = "";
            else
                ValidationHelper.StringLength("Telefone", numero, NumeroMaxLength, ref validationResults);

            Numero = numero;
        }

        private void SetDDD(string ddd)
        {
            if (string.IsNullOrEmpty(ddd))
                ddd = "";
            else
                ValidationHelper.StringLength("DDD", ddd, DDDMaxLength, ref validationResults);

            DDD = ddd;
        }

        public string GetTelefoneCompleto()
        {
            return $"({DDD}) {Numero}";
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            // Using a yield return statement to return each element one at a time
            yield return DDD;
            yield return Numero;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
