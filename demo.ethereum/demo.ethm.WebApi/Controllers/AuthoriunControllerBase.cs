using demo.ethm.Aplicacao.Models;
using demo.ethm.Aplicacao.Models.Response;
using demo.ethm.Dominio.Entities;
using demo.ethm.Infraestrutura.Persistencia;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace demo.ethm.WebApi.Controllers
{
    /// <summary>
    /// Classe que serve de base para todas as Apis
    /// </summary>
    public class DemoControllerBase : ControllerBase
    {
        /// <summary>
        /// Obter contexto de uma requisição
        /// </summary>
        /// <returns></returns>
        protected ResponseBase<UsuarioDTO> ObterUsuarioRequest()
        {
            return (ResponseBase<UsuarioDTO>)Request.HttpContext.Items.Where(x => x.Key.ToString() == "usuario").Select(x => x.Value).FirstOrDefault();
        }
    }
}