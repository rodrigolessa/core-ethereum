//using Microsoft.EntityFrameworkCore;
using demo.ethm.Infraestrutura.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.RegularExpressions;

namespace demo.ethm.Dominio.Entities.ValueObjects
{
    //[Owned]
    public class Email : ValueObject, IValidatableObject
    {
        [NotMapped]
        private readonly List<ValidationResult> validationResults = new List<ValidationResult>();

        public const int EnderecoMaxLength = 254;
        public string Endereco { get; private set; }

        // Constructor for the EntityFramework works
        protected Email() { }

        public Email(string endereco)
        {
            ValidationHelper.ForNullOrEmptyDefaultMessage(endereco, "E-mail", ref validationResults);
            ValidationHelper.StringLength("E-mail", endereco, EnderecoMaxLength, ref validationResults);

            ValidationHelper.EmailIsValid(endereco, ref validationResults);

            Endereco = endereco;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            // Using a yield return statement to return each element one at a time
            yield return Endereco;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return validationResults;
        }
    }
}
