using demo.ethm.Aplicacao.Models;
using demo.ethm.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace demo.ethm.Aplicacao.EntityExtensions
{
    public static class GeneroObraExtensions
    {
        public static IEnumerable<GeneroObraDTO> Traduzir(this List<GeneroObra> lista, int idIdioma)
        {
            return lista.Select(x => x.Traduzir(idIdioma));
        }

        public static GeneroObraDTO Traduzir(this GeneroObra item, int idIdioma)
        {
            var dto = new GeneroObraDTO()
            {
                Id = item.Id,
                Descricao = item.Descricoes.FirstOrDefault(x => x.IdIdioma == idIdioma).Descricao
            };

            return dto;
        }
    }
}
