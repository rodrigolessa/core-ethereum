using demo.ethm.Aplicacao.Models;
using demo.ethm.Aplicacao.Models.Response;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace demo.ethm.Aplicacao
{
    public class AppServiceBase : IDisposable
    {
        public string IpUsuario;
        public string NomeSistemaOperacional;
        public string NomeNavegador;
        public ResponseBase<bool> Resposta;
        public UsuarioDTO UsuarioLogado;
        public Stopwatch swTempoRequisicao = new Stopwatch();

        public AppServiceBase()
        {
            Resposta = new ResponseBase<bool>();
            Resposta.Mensagens = new List<string>();
            Resposta.Sucesso = true;
            Resposta.Autorizado = true;
        }

        public void Dispose(bool disposing)
        {
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}