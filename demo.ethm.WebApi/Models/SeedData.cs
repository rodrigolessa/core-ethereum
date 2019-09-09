using demo.ethm.Dominio.Entities;
using demo.ethm.Dominio.Entities.Enums;
using demo.ethm.Dominio.Entities.ValueObjects;
using demo.ethm.Infraestrutura.Persistencia;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace demo.ethm.WebApi.Models
{
    /// <summary>
    /// Classe para popular o banco de dados com valores de inicialização
    /// </summary>
    public static class SeedData
    {
        /// <summary>
        /// Método para iniciar a população do banco de dados
        /// </summary>
        /// <param name="serviceProvider"></param>
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var contexto = new MainContext(
                serviceProvider.GetRequiredService<DbContextOptions<MainContext>>()))
            {
                // Creates the database if not exists
                contexto.Database.EnsureCreated();

                /////////////////////////////////////////////////////////////////////////
                // TODO: Carga de Idiomas

                // English, inglês
                Idioma eng = new Idioma()
                {
                    Descricao = "English",
                    ISO639Alfa2 = "en",
                    ISO639Alfa3 = "eng",
                    HtmlCode = string.Empty,
                    DataDeInclusao = DateTime.Now
                };

                if (contexto.Idioma.Where(i => i.ISO639Alfa3 == eng.ISO639Alfa3).Count() == 0)
                {
                    contexto.Idioma.Add(eng);
                }

                string siglaPortugues = "por";

                // Portuguese, português
                Idioma por = contexto.Idioma.Where(x => x.ISO639Alfa3 == siglaPortugues).FirstOrDefault();
                if (por == null)
                {
                    por = new Idioma()
                    {
                        Descricao = "Português",
                        ISO639Alfa2 = "pt",
                        ISO639Alfa3 = siglaPortugues,
                        HtmlCode = string.Empty,
                        DataDeInclusao = DateTime.Now
                    };

                    contexto.Idioma.Add(por);
                }

                /////////////////////////////////////////////////////////////////////////
                // TODO: Carga de Paises

                var paises = new List<Pais>();

                var brasil = new Pais()
                {
                    ISO3166Alfa2 = "Br",
                    ISO3166Alfa3 = "Bra",
                    CodigoTelefone = 55,
                    DataDeInclusao = DateTime.Now
                };

                brasil.Descricoes.Add(new PaisDescricao()
                {
                    Descricao = "Brasil",
                    Idioma = por
                });

                brasil.Descricoes.Add(new PaisDescricao()
                {
                    Descricao = "Brazil",
                    Idioma = eng
                });

                paises.Add(brasil);

                foreach (var pais in paises)
                {
                    if (contexto.Pais.Where(x => x.ISO3166Alfa3.Contains(pais.ISO3166Alfa3)).Count() == 0)
                    {
                        contexto.Pais.Add(pais);
                    }
                    else
                    {
                        var paisfromdb = contexto.Pais.Where(x => x.ISO3166Alfa3.Contains(pais.ISO3166Alfa3))
                            .Include(p => p.Descricoes)
                            .FirstOrDefault();
                        // TODO: Verificar se as descrições estão completas com todos os idiomas
                        // TODO: Ou excluir todas as descrições e incluir novamente
                        //foreach(var d in paisfromdb.Descricoes){
                        //foreach(var s in pais.Descricoes){
                        //if (d.Idioma.ISO639Alfa3 == s.Idioma.ISO639Alfa3)
                        //}
                        //}
                    }
                }

                brasil = contexto.Pais.Where(x => x.ISO3166Alfa3.Contains(brasil.ISO3166Alfa3)).FirstOrDefault();

                /////////////////////////////////////////////////////////////////////////
                // TODO: Carga de Gêneros das Obras

                var generos = new List<GeneroObra>();

                var literaria = new GeneroObra()
                {
                    Codigo = 1,
                    DataDeInclusao = DateTime.Now
                };

                literaria.Descricoes.Add(new GeneroObraDescricao()
                {
                    Descricao = "Literária",
                    Idioma = por
                });

                literaria.Descricoes.Add(new GeneroObraDescricao()
                {
                    Descricao = "English",
                    Idioma = eng
                });

                generos.Add(literaria);

                foreach (var g in generos)
                {
                    if (contexto.GeneroObra.Where(x => x.Codigo == g.Codigo).Count() == 0)
                    {
                        contexto.GeneroObra.Add(g);
                    }
                    else
                    {
                        var generofromdb = contexto.GeneroObra.Where(x => x.Codigo == g.Codigo)
                            .Include(p => p.Descricoes)
                            .FirstOrDefault();
                        // TODO: Excluir todas as descrições e incluir novamente
                    }
                }

                /////////////////////////////////////////////////////////////////////////
                // TODO: Carga de Tipos de Documentos

                var tiposDeDocumentos = new List<TipoDocumento>();

                var cpf = new TipoDocumento()
                {
                    Codigo = 1,
                    DataDeInclusao = DateTime.Now
                };

                cpf.Descricoes.Add(new TipoDocumentoDescricao()
                {
                    Descricao = "CPF",
                    Idioma = por
                });

                cpf.Descricoes.Add(new TipoDocumentoDescricao()
                {
                    Descricao = "English",
                    Idioma = eng
                });

                tiposDeDocumentos.Add(cpf);

                foreach (var t in tiposDeDocumentos)
                {
                    if (contexto.TipoDocumento.Where(x => x.Codigo == t.Codigo).Count() == 0)
                    {
                        contexto.TipoDocumento.Add(t);
                    }
                    else
                    {
                        var tipodocfromdb = contexto.TipoDocumento.Where(x => x.Codigo == t.Codigo)
                            .Include(p => p.Descricoes)
                            .FirstOrDefault();
                        // TODO: Excluir todas as descrições e incluir novamente
                    }
                }

                /////////////////////////////////////////////////////////////////////////
                // Criando um usuário para testes em produção

                var nome = "Rodrigo Lessa";
                //var email = new Email("rodrigolsr@gmail.com");
                var email = "rodrigolsr@gmail.com";
                //var telefone = new Telefone("21", "988997240");
                var telefone = "21988997240";
                var endereco = new Endereco("Rua Doutor Nilo Peçanha", "Bloco 04 Apto 906", "1170", "Estrela Do Norte", "São Gonçalo", Uf.RJ, string.Empty, new Cep("24450000", false));
                var senha = "Lcd!123";
                var confirmacaoDeSenha = "Lcd!123";

                // HACK: Create a user for debugging proposes
                var usuario = new Usuario(nome, senha, confirmacaoDeSenha, email, telefone)
                {
                    Idioma = por,
                    Pais = brasil
                };

                usuario.SetEndereco(endereco);
                usuario.Requerentes.Add(new Requerente(
                    "Manuela Lessa",
                    "Manu",
                    "Lilia Silva",
                    new DateTime(2019, 1, 1),
                    "Brasileira",
                    new Email("manuelalessa@gmail.com")
                ));

                if (contexto.Usuario.Where(x => x.Email == email).Count() == 0)
                {
                    contexto.Usuario.Add(usuario);
                }

                /////////////////////////////////////////////////////////////////////////
                // Saves all data in MySQL
                contexto.SaveChanges();
            }
        }
    }
}
