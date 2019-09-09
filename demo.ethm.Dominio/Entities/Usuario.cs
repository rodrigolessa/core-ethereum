using demo.ethm.Dominio.Entities.Enums;
using demo.ethm.Dominio.Entities.ValueObjects;
using demo.ethm.Infraestrutura.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace demo.ethm.Dominio.Entities
{
    public partial class Usuario : EntityBase, IValidatableObject
    {
        [NotMapped]
        private List<ValidationResult> validationResults = new List<ValidationResult>();

        public const int NomeMinLength = 3;
        public const int NomeMaxLength = 30;
        [StringLength(NomeMaxLength)]
        public string Nome { get; private set; }

        public const int SenhaMinLength = 6;
        public const int SenhaMaxLength = 255;
        [StringLength(SenhaMaxLength)]
        public byte[] Senha { get; private set; }

        //public Email Email { get; private set; }
        public const int EmailMaxLength = 200;
        [StringLength(EmailMaxLength)]
        public string Email { get; private set; }

        //public Telefone Telefone { get; private set; }
        public const int TelefoneMaxLength = 12;
        [StringLength(TelefoneMaxLength)]
        public string Telefone { get; private set; }

        //[NotMapped]
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

        public string GetCepFormatado()
        {
            if (!Cep.HasValue)
            {
                return "";
            }

            var cep = Cep.ToString();

            while (cep.Length < 8)
            {
                cep = "0" + cep;
            }

            return cep.Substring(0, 5) + "-" + cep.Substring(5);
        }

        //[ForeignKey("FKEnderecoPais")]
        public virtual Pais Pais { get; set; }

        #endregion

        public string Foto { get; private set; }

        public TipoDeUsuario? Tipo { get; set; }

        public SituacaoDoUsuario Situacao { get; private set; }

        public string TokenConfirmacaoEmail { get; private set; }

        public string TokenAlteracaoDeSenha { get; private set; }

        public DateTime? DataDaExclusao { get; set; }

        //[ForeignKey("IdIdioma")]
        public virtual Idioma Idioma { get; set; }

        public virtual ICollection<Obra> Obras { get; set; }

        public virtual ICollection<Requerente> Requerentes { get; set; }

        // HACK: Representante não deveria existir sozinho, sempre deve estar relacionado a um Requerente, 
        // mas pode representar mais de um Requerente e este pode ser representado por mais de um representante
        public virtual ICollection<Representante> Representantes { get; set; }

        public virtual ICollection<UsuarioToken> UsuarioToken { get; set; }

        // TODO: Entidade de crédito ?

        // For the EntityFramework works
        protected Usuario()
        {
            Init();
        }

        public Usuario(string nome, string senha, string confirmacaoDeSenha, string email, string telefone)
        {
            Init();
            SetNome(nome);
            SetEmail(email);
            SetSenha(senha, confirmacaoDeSenha);
            SetTelefone(telefone);
            GerarTokenConfirmacaoEmail();
            Situacao = SituacaoDoUsuario.Aguardando;
            DataDeInclusao = DateTime.Now;
        }

        private void Init()
        {
            Obras = new HashSet<Obra>();
            Requerentes = new HashSet<Requerente>();
            Representantes = new HashSet<Representante>();
            UsuarioToken = new HashSet<UsuarioToken>();
        }

        public void SetNome(string nome)
        {
            ValidationHelper.ForNullOrEmptyDefaultMessage(nome, "Nome", ref validationResults);
            ValidationHelper.StringLength("Nome", nome, NomeMinLength, NomeMaxLength, ref validationResults);

            Nome = nome;
        }

        //public void SetEmail(Email email)
        public void SetEmail(string email)
        {
            ValidationHelper.ForNullOrEmptyDefaultMessage(email, "E-mail", ref validationResults);
            ValidationHelper.StringLength("E-mail", email, EmailMaxLength, ref validationResults);
            // TODO: Corrigir validação
            //ValidationHelper.EmailIsValid(email, ref validationResults);

            Email = email;
        }

        public void SetEndereco(Endereco valor)
        {
            var enderecoValidationContext = new ValidationContext(valor, null, null);
            var enderecoValidationResults = ((IValidatableObject)valor).Validate(enderecoValidationContext);
            if (enderecoValidationResults.Count() > 0)
            {
                validationResults.AddRange(enderecoValidationResults);
            }
            else
            {
                Logradouro = valor.Logradouro;
                Complemento = valor.Complemento;
                Numero = valor.Numero;
                Bairro = valor.Bairro;
                Municipio = valor.Municipio;
                Uf = valor.Uf;
                OutroEstado = valor.OutroEstado;
                Cep = valor.Cep.CepCod;
            }
        }

        public void SetTelefone(string telefone)
        {
            ValidationHelper.StringLength("Telefone", telefone, TelefoneMaxLength, ref validationResults);

            Telefone = telefone;
        }

        public void SetFoto(byte[] arquivo)
        {
            // TODO: Salvar arquivo em disco ?
            Foto = "nome.jpg";
        }

        private void SetSenha(string senha, string senhaConfirmacao)
        {
            ValidationHelper.ForNullOrEmptyDefaultMessage(senha, "Senha", ref validationResults);
            ValidationHelper.ForNullOrEmptyDefaultMessage(senhaConfirmacao, "Confirmação de Senha", ref validationResults);
            ValidationHelper.StringLength("Senha", senha, SenhaMinLength, SenhaMaxLength, ref validationResults);
            ValidationHelper.AreEqual(senha, senhaConfirmacao, "As senhas não conferem!", ref validationResults);

            Senha = CryptoHelper.CriptografarSenha(senha);
        }

        public void AlterarSenha(string senhaAtual, string novaSenha, string confirmacaoDeSenha)
        {
            ValidarSenha(senhaAtual);
            SetSenha(novaSenha, confirmacaoDeSenha);
        }

        public bool ValidarSenha(string senha)
        {
            ValidationHelper.ForNullOrEmptyDefaultMessage(senha, "Senha", ref validationResults);
            var senhaCriptografada = CryptoHelper.CriptografarSenha(senha);
            if (!Senha.SequenceEqual(senhaCriptografada))
            {
                validationResults.Add(new ValidationResult("Usuário ou senha inválidos."));
                return false;
            }

            return true;
        }

        public string GerarNovoTokenAlterarSenha()
        {
            TokenAlteracaoDeSenha = CryptoHelper.GerarToken(this.Email);
            return TokenAlteracaoDeSenha;
        }

        public void AlterarSenhaToken(string token, string novaSenha, string confirmacaoDeSenha)
        {
            if (!TokenAlteracaoDeSenha.Equals(token))
            {
                validationResults.Add(new ValidationResult("token para alteração de senha inválido!"));
            }
            else
            {
                SetSenha(novaSenha, confirmacaoDeSenha);
                GerarNovoTokenAlterarSenha();
            }
        }

        public void GerarTokenConfirmacaoEmail()
        {
            TokenConfirmacaoEmail = CryptoHelper.GerarToken(this.Email);
        }

        public void ConfirmarCadastro()
        {
            Situacao = SituacaoDoUsuario.Liberado;
        }

        #region Member of IValidatableObject

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return validationResults;
        }

        #endregion

        public bool HasError(){
            return validationResults.Count() > 0;
        }
    }
}
