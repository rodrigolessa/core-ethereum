using demo.ethm.Aplicacao.Models;
using demo.ethm.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace demo.ethm.Aplicacao.EntityExtensions
{
    public static class TipoDocumentoExtensions
    {
        public static IEnumerable<TipoDocumentoDTO> Traduzir(this List<TipoDocumento> lista, int idIdioma)
        {
            return lista.Select(x => x.Traduzir(idIdioma));
        }

        public static TipoDocumentoDTO Traduzir(this TipoDocumento item, int idIdioma)
        {
            var dto = new TipoDocumentoDTO()
            {
                Id = item.Id,
                Descricao = item.Descricoes.FirstOrDefault(x => x.IdIdioma == idIdioma).Descricao
            };

            return dto;
        }
    }
}
