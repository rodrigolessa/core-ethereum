using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace demo.ethm.Aplicacao.Models.Request
{
    public class AlterarSenhaRequest
    {
        public string Senha {get;set;}
        public string ConfirmacaoDeSenha {get;set;}
        public string Token {get;set;}
    }
}
