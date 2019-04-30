using demo.ethm.Aplicacao.Models;
using demo.ethm.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace demo.ethm.Aplicacao.EntityExtensions
{
    public static class IdiomaExtensions
    {
        public static IEnumerable<IdiomaDTO> Traduzir(this List<Idioma> lista)
        {
            return lista.Select(x => x.Traduzir());
        }

        public static IdiomaDTO Traduzir(this Idioma item)
        {
            var dto = new IdiomaDTO()
            {
                Id = item.Id,
                Descricao = item.Descricao
            };

            return dto;
        }
    }
}
