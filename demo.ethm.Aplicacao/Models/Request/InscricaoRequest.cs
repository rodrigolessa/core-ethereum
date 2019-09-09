using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace demo.ethm.Aplicacao.Models.Request
{
    public class InscricaoRequest
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Senha { get; set; }
        public string ConfirmacaoDeSenha  { get; set; }
        public int Idioma { get; set; }
        public int? Tipo { get; set; }
    }
}
