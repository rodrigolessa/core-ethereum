using demo.ethm.Dominio.Entities.Enums;
using demo.ethm.Infraestrutura.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace demo.ethm.Dominio.Entities.ValueObjects
{
    public class Endereco : ValueObject, IValidatableObject
    {
        [NotMapped]
        private List<ValidationResult> validationResults = new List<ValidationResult>();

        public const int LogradouroMaxLength = 150;
        public virtual string Logradouro { get; private set; }

        public const int ComplementoMaxLength = 150;
        public virtual string Complemento { get; private set; }

        public const int NumeroMaxLength = 50;
        public virtual string Numero { get; private set; }

        public const int BairroMaxLength = 150;
        public virtual string Bairro { get; private set; }

        public const int MunicipioMaxLength = 150;
        public virtual string Municipio { get; private set; }

        public virtual Uf? Uf { get; private set; }

        public const int EstadoMaxLength = 50;
        public virtual string OutroEstado { get; private set; }

        public virtual Cep Cep { get; private set; }

        //[ForeignKey("FKEnderecoPais")]
        //public virtual Pais Pais { get; set; }

        protected Endereco() { }

        public Endereco(string logradouro, string complemento, string numero, string bairro, string municipio, Uf? uf, string outroEstado, Cep cep)
        {
            SetCep(cep);
            SetBairro(bairro);
            SetMunicipio(municipio);
            SetComplemento(complemento);
            SetLogradouro(logradouro);
            SetNumero(numero);
            SetUf(uf, outroEstado);
        }

        public void SetCep(Cep cep)
        {
            // TODO: Replacing Throwing Exceptions with Notification in Validations. https://martinfowler.com/articles/replaceThrowWithNotification.html
            var cepvalidationContext = new ValidationContext(cep, null, null);
            var cepvalidationResults = ((IValidatableObject)cep).Validate(cepvalidationContext);
            if (cepvalidationResults.Count() > 0)
            {
                validationResults.AddRange(cepvalidationResults);
            }
            else
            {
                Cep = cep;
            }
        }

        public void SetComplemento(string complemento)
        {
            if (string.IsNullOrWhiteSpace(complemento))
            {
                complemento = string.Empty;
            }

            Complemento = TextHelper.ToTitleCase(complemento);
        }

        public void SetLogradouro(string logradouro)
        {
            ValidationHelper.ForNullOrEmptyDefaultMessage(logradouro, "Endereço", ref validationResults);
            Logradouro = TextHelper.ToTitleCase(logradouro);
        }

        public void SetNumero(string numero)
        {
            ValidationHelper.ForNullOrEmptyDefaultMessage(numero, "Número", ref validationResults);
            Numero = numero;
        }

        public void SetBairro(string bairro)
        {
            ValidationHelper.ForNullOrEmptyDefaultMessage(bairro, "Bairro", ref validationResults);
            Bairro = TextHelper.ToTitleCase(bairro);
        }

        public void SetMunicipio(string municipio)
        {
            ValidationHelper.ForNullOrEmptyDefaultMessage(municipio, "Município", ref validationResults);
            Municipio = TextHelper.ToTitleCase(municipio);
        }

        public void SetUf(Uf? uf, string outro)
        {
            if (!uf.HasValue && string.IsNullOrWhiteSpace(outro))
            {
                validationResults.Add(new ValidationResult("Estado é obrigatório"));
            }
            else
            {
                Uf = uf;
                OutroEstado = outro;
            }
        }

        public override string ToString()
        {
            return $"{Logradouro}, {Numero}, {Complemento}, {Bairro} - {Municipio} / {Uf}";
        }

        public bool HasError()
        {
            return (validationResults.Count > 0);
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
