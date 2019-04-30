using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace demo.ethm.Aplicacao.Models.Response
{
    public class ResponseBase<T>
    {
        public T Objeto { get; set; }
        public List<string> Mensagens = new List<string>();
        public bool Autorizado { get; set; }
        public TimeSpan TempoLevado { get; set; }
        private bool _sucesso;
        public bool Sucesso
        {
            get
            {
                if (Mensagens != null && Mensagens.Any())
                {
                    _sucesso = false;
                    return false;
                }
                _sucesso = true;
                return true;
            }
            set { _sucesso = value; }
        }
    }
}