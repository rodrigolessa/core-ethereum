using demo.ethm.Dominio.Entities.Enums;
using demo.ethm.Dominio.Entities.ValueObjects;
using demo.ethm.Infraestrutura.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace demo.ethm.Dominio.Entities
{
    public class Requerente : EntityBase
    {
        private void Init()
        {
            Obras = new HashSet<Obra>();
            //RequerenteObras = new HashSet<RequerenteObra>();
            RequerenteRepresentantes = new HashSet<RequerenteRepresentante>();
        }

        // For the EntityFramework works
        protected Requerente()
        {
            Init();
        }

        public Requerente(string nome, string pseudonimo, string nomeDaMae, DateTime? nascimento, string naturalidade, Email email)
        {
            Init();
            Nome = nome;
            Pseudonimo = pseudonimo;
            NomeMae = nome;
            Nascimento = nascimento;
            Naturalidade = naturalidade;
            Email = email.Endereco;
            DataDeInclusao = DateTime.Now;
        }

        public int? IdDocumento { get; set; }

        public int? IdPais { get; set; }

        public const int NomeMinLength = 3;
        public const int NomeMaxLength = 100;
        public string Nome { get; private set; }

        [StringLength(50)]
        public string Pseudonimo { get; private set; }

        [StringLength(100)]
        public string NomeMae { get; private set; }

        public DateTime? Nascimento { get; private set; }

        [StringLength(50)]
        public string Naturalidade { get; private set; }

        [StringLength(50)]
        public string Ocupacao { get; set; }

        [StringLength(50)]
        public string Escolaridade { get; set; }

        //public Email Email { get; private set; }
        public const int EmailMaxLength = 200;
        [StringLength(EmailMaxLength)]
        public string Email { get; private set; }

        //public Telefone Telefone { get; private set; }
        public const int TelefoneMaxLength = 12;
        [StringLength(TelefoneMaxLength)]
        public string Telefone { get; private set; }

        //public Endereco Endereco { get; private set; }
        #region Endereço

        public const int LogradouroMaxLength = 150;
        [StringLength(LogradouroMaxLength)]
        public string Logradouro { get; private set; }

        public const int ComplementoMaxLength = 150;
        [StringLength(ComplementoMaxLength)]
        public string Complemento { get; private set; }

        public const int NumeroMaxLength = 50;
        [StringLength(NumeroMaxLength)]
        public string Numero { get; private set; }

        public const int BairroMaxLength = 150;
        [StringLength(BairroMaxLength)]
        public string Bairro { get; private set; }

        public const int MunicipioMaxLength = 150;
        [StringLength(MunicipioMaxLength)]
        public string Municipio { get; private set; }

        public Uf? Uf { get; private set; }

        public const int EstadoMaxLength = 50;
        [StringLength(EstadoMaxLength)]
        public string OutroEstado { get; private set; }

        //public Cep Cep { get; private set; }
        public const int CepMaxLength = 8;
        public long? Cep { get; private set; }

        #endregion

        public virtual Usuario Usuario { get; set; }

        public virtual Documento Documento { get; set; }

        public virtual Pais Nacionalidade { get; set; }

        public virtual ICollection<RequerenteRepresentante> RequerenteRepresentantes { get; set; }

        //public virtual ICollection<RequerenteObra> RequerenteObras { get; set; }

        public virtual ICollection<Obra> Obras { get; set; }
    }
}
