using demo.ethm.Dominio.Entities;
using demo.ethm.Dominio.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace demo.ethm.Aplicacao.Models
{
    // TODO: Classe para representar o usuário logado
    //public interface IUser
    //string Name { get; }
    //bool IsAuthenticated();
    //IEnumerable<Claim> GetClaimsIdentity();

    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Municipio { get; set; }
        public Uf? Uf { get; set; }
        public string OutroEstado { get; set; }
        public long? Cep { get; set; }
        public int IdIdioma { get; set; }
        public int IdPais { get; set; }
        public TipoDeUsuario? Tipo { get; set; }
        public SituacaoDoUsuario Situacao { get; set; }
        public string Token { get; set; }
    }
}
