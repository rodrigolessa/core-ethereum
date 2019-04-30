using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace demo.ethm.Aplicacao.Models.Request
{
    public class UsuarioRequest
    {
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Logradouro { get; private set; }
        public string Complemento { get; private set; }
        public string Numero { get; private set; }
        public string Bairro { get; private set; }
        public string Municipio { get; private set; }
        public int? Uf { get; private set; }
        public string OutroEstado { get; private set; }
        public string Cep { get; private set; }
        public int Idioma { get; set; }
        public int? Pais { get; set; }
        public int? Tipo { get; private set; }
    }
}
