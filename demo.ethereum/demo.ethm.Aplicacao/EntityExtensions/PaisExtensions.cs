using demo.ethm.Aplicacao.Models;
using demo.ethm.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace demo.ethm.Aplicacao.EntityExtensions
{
    public static class PaisExtensions
    {
        public static IEnumerable<PaisDTO> Traduzir(this List<Pais> lista, int idIdioma)
        {
            return lista.Select(x => x.Traduzir(idIdioma));
        }

        public static PaisDTO Traduzir(this Pais item, int idIdioma)
        {
            var dto = new PaisDTO()
            {
                Id = item.Id,
                Descricao = item.Descricoes.FirstOrDefault(x => x.IdIdioma == idIdioma).Descricao
            };

            return dto;
        }
    }
}
