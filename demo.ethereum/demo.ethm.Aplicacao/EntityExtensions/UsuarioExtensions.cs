using demo.ethm.Aplicacao.Models;
using demo.ethm.Aplicacao.Models.Response;
using demo.ethm.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace demo.ethm.Aplicacao.EntityExtensions
{
    public static class UsuarioExtensions
    {
        public static IEnumerable<UsuarioResponse> Traduzir(this IEnumerable<Usuario> lista)
        {
            return lista.Select(x => x.Traduzir());
        }

        public static UsuarioResponse Traduzir(this Usuario item)
        {
            var usuario = new UsuarioResponse()
            {
                Id = item.Id,
                Nome = item.Nome,
                Email = item.Email,
                Telefone = item.Telefone,
                Logradouro = item.Logradouro,
                Complemento = item.Complemento,
                Numero = item.Numero,
                Bairro = item.Bairro,
                Municipio = item.Municipio,
                Cep = item.GetCepFormatado()
            };

            if (item.Uf.HasValue)
            {
                usuario.Uf = item.Uf.ToString();
            }
            else
            {
                usuario.Uf = item.OutroEstado;
            }

            if (item.Pais != null)
            {
                if (item.Pais.Descricoes.Any(x => x.Idioma.Equals(item.Idioma)))
                {
                    usuario.PaisDescricao = item.Pais.Descricoes.FirstOrDefault(x => x.Idioma.Equals(item.Idioma)).Descricao;
                }
            }

            if (item.UsuarioToken.Any(x => x.Ativo == 1))
            {
                usuario.Token = item.UsuarioToken.FirstOrDefault(x => x.Ativo == 1).Token;
            }

            return usuario;
        }

        public static IEnumerable<UsuarioDTO> TraduzirParaDTO(this IEnumerable<Usuario> lista)
        {
            return lista.Select(x => x.TraduzirParaDTO());
        }

        public static UsuarioDTO TraduzirParaDTO(this Usuario item)
        {
            var usuario = new UsuarioDTO()
            {
                Id = item.Id,
                Nome = item.Nome,
                Email = item.Email,
                Telefone = item.Telefone,
                Logradouro = item.Logradouro,
                Complemento = item.Complemento,
                Numero = item.Numero,
                Bairro = item.Bairro,
                Municipio = item.Municipio,
                Cep = item.Cep,
                Uf = item.Uf,
                OutroEstado = item.OutroEstado,
                Tipo = item.Tipo,
                Situacao = item.Situacao
            };

            if (item.Pais != null)
            {
                usuario.IdPais = item.Pais.Id;
            }

            if (item.UsuarioToken.Any(x => x.Ativo == 1))
            {
                usuario.Token = item.UsuarioToken.FirstOrDefault(x => x.Ativo == 1).Token;
            }

            return usuario;
        }
    }
}
