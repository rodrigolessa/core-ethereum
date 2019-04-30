using demo.ethm.Dominio.Entities;
using demo.ethm.Dominio.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace demo.ethm.Aplicacao.Models.Response
{
    public class UsuarioResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        //public string Senha { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Municipio { get; set; }
        public string Uf { get; set; }
        //public string OutroEstado { get; set; }
        public string Cep { get; set; }
        public string PaisDescricao { get; set; }
        public string Token { get; set; }
    }
}
