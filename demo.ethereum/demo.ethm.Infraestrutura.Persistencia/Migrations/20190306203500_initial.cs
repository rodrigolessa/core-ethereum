using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace demo.ethm.Infraestrutura.Persistencia.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Arquivo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    DataDeInclusao = table.Column<DateTime>(nullable: false),
                    DataDeAlteracao = table.Column<DateTime>(nullable: true),
                    Nome = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    Extensao = table.Column<string>(unicode: false, maxLength: 4, nullable: true),
                    Mime = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    Tamanho = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    MD5 = table.Column<string>(unicode: false, maxLength: 32, nullable: false),
                    SHA256 = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    SHA512 = table.Column<string>(unicode: false, maxLength: 200, nullable: false),
                    JWTCertificado = table.Column<string>(unicode: false, nullable: true),
                    JWTChavePublica = table.Column<string>(unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arquivo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GeneroObra",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    DataDeInclusao = table.Column<DateTime>(nullable: false),
                    DataDeAlteracao = table.Column<DateTime>(nullable: true),
                    Codigo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneroObra", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Idioma",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    DataDeInclusao = table.Column<DateTime>(nullable: false),
                    DataDeAlteracao = table.Column<DateTime>(nullable: true),
                    Descricao = table.Column<string>(maxLength: 100, nullable: false),
                    ISO639Alfa2 = table.Column<string>(unicode: false, maxLength: 2, nullable: true),
                    ISO639Alfa3 = table.Column<string>(unicode: false, maxLength: 3, nullable: true),
                    HtmlCode = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    DataDeDesativacao = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Idioma", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LogEvento",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    EventId = table.Column<int>(nullable: true),
                    LogLevel = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogEvento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pais",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    DataDeInclusao = table.Column<DateTime>(nullable: false),
                    DataDeAlteracao = table.Column<DateTime>(nullable: true),
                    ISO3166Alfa2 = table.Column<string>(unicode: false, maxLength: 2, nullable: true),
                    ISO3166Alfa3 = table.Column<string>(unicode: false, maxLength: 3, nullable: true),
                    CodigoTelefone = table.Column<int>(nullable: true),
                    DataDeDesativacao = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pais", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoDocumento",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    DataDeInclusao = table.Column<DateTime>(nullable: false),
                    DataDeAlteracao = table.Column<DateTime>(nullable: true),
                    Codigo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoDocumento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GeneroObraDescricao",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    DataDeInclusao = table.Column<DateTime>(nullable: false),
                    DataDeAlteracao = table.Column<DateTime>(nullable: true),
                    IdIdioma = table.Column<int>(nullable: false),
                    Descricao = table.Column<string>(maxLength: 100, nullable: false),
                    GeneroObraId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneroObraDescricao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GeneroObraDescricao_GeneroObra_GeneroObraId",
                        column: x => x.GeneroObraId,
                        principalTable: "GeneroObra",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GeneroObraDescricao_Idioma_IdIdioma",
                        column: x => x.IdIdioma,
                        principalTable: "Idioma",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaisDescricao",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    DataDeInclusao = table.Column<DateTime>(nullable: false),
                    DataDeAlteracao = table.Column<DateTime>(nullable: true),
                    IdIdioma = table.Column<int>(nullable: false),
                    Descricao = table.Column<string>(maxLength: 100, nullable: false),
                    PaisId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaisDescricao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaisDescricao_Idioma_IdIdioma",
                        column: x => x.IdIdioma,
                        principalTable: "Idioma",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaisDescricao_Pais_PaisId",
                        column: x => x.PaisId,
                        principalTable: "Pais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    DataDeInclusao = table.Column<DateTime>(nullable: false),
                    DataDeAlteracao = table.Column<DateTime>(nullable: true),
                    Nome = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    Senha = table.Column<byte[]>(unicode: false, maxLength: 255, nullable: true),
                    Email = table.Column<string>(maxLength: 200, nullable: true),
                    Telefone = table.Column<string>(maxLength: 12, nullable: true),
                    Logradouro = table.Column<string>(maxLength: 150, nullable: true),
                    Complemento = table.Column<string>(maxLength: 150, nullable: true),
                    Numero = table.Column<string>(maxLength: 50, nullable: true),
                    Bairro = table.Column<string>(maxLength: 150, nullable: true),
                    Municipio = table.Column<string>(maxLength: 150, nullable: true),
                    Uf = table.Column<int>(nullable: true),
                    OutroEstado = table.Column<string>(maxLength: 50, nullable: true),
                    Cep = table.Column<long>(nullable: true),
                    PaisId = table.Column<int>(nullable: true),
                    Foto = table.Column<string>(nullable: true),
                    Tipo = table.Column<int>(nullable: true),
                    Situacao = table.Column<int>(nullable: false),
                    TokenConfirmacaoEmail = table.Column<string>(nullable: true),
                    TokenAlteracaoDeSenha = table.Column<string>(nullable: true),
                    DataDaExclusao = table.Column<DateTime>(nullable: true),
                    IdiomaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuario_Idioma_IdiomaId",
                        column: x => x.IdiomaId,
                        principalTable: "Idioma",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Usuario_Pais_PaisId",
                        column: x => x.PaisId,
                        principalTable: "Pais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Documento",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    DataDeInclusao = table.Column<DateTime>(nullable: false),
                    DataDeAlteracao = table.Column<DateTime>(nullable: true),
                    IdTipoDocumento = table.Column<int>(nullable: false),
                    IdPais = table.Column<int>(nullable: false),
                    Identificador = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    OrgaoExpedidor = table.Column<string>(maxLength: 50, nullable: true),
                    Validade = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documento_Pais_IdPais",
                        column: x => x.IdPais,
                        principalTable: "Pais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Documento_TipoDocumento_IdTipoDocumento",
                        column: x => x.IdTipoDocumento,
                        principalTable: "TipoDocumento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TipoDocumentoDescricao",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    DataDeInclusao = table.Column<DateTime>(nullable: false),
                    DataDeAlteracao = table.Column<DateTime>(nullable: true),
                    IdIdioma = table.Column<int>(nullable: false),
                    Descricao = table.Column<string>(maxLength: 100, nullable: false),
                    TipoDocumentoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoDocumentoDescricao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TipoDocumentoDescricao_Idioma_IdIdioma",
                        column: x => x.IdIdioma,
                        principalTable: "Idioma",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TipoDocumentoDescricao_TipoDocumento_TipoDocumentoId",
                        column: x => x.TipoDocumentoId,
                        principalTable: "TipoDocumento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioToken",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    DataDeInclusao = table.Column<DateTime>(nullable: false),
                    DataDeAlteracao = table.Column<DateTime>(nullable: true),
                    IdUsuario = table.Column<int>(nullable: false),
                    Token = table.Column<string>(unicode: false, maxLength: 128, nullable: false),
                    Ativo = table.Column<int>(nullable: false),
                    UsuarioId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuarioToken_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Representante",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    DataDeInclusao = table.Column<DateTime>(nullable: false),
                    DataDeAlteracao = table.Column<DateTime>(nullable: true),
                    IdUsuario = table.Column<int>(nullable: false),
                    IdDocumento = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Representante", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Representante_Documento_IdDocumento",
                        column: x => x.IdDocumento,
                        principalTable: "Documento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Representante_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "requerente",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    DataDeInclusao = table.Column<DateTime>(nullable: false),
                    DataDeAlteracao = table.Column<DateTime>(nullable: true),
                    IdDocumento = table.Column<int>(nullable: true),
                    IdPais = table.Column<int>(nullable: true),
                    Nome = table.Column<string>(nullable: true),
                    Pseudonimo = table.Column<string>(maxLength: 50, nullable: true),
                    NomeMae = table.Column<string>(maxLength: 100, nullable: true),
                    Nascimento = table.Column<DateTime>(nullable: true),
                    Naturalidade = table.Column<string>(maxLength: 50, nullable: true),
                    Ocupacao = table.Column<string>(maxLength: 50, nullable: true),
                    Escolaridade = table.Column<string>(maxLength: 50, nullable: true),
                    Email = table.Column<string>(maxLength: 200, nullable: true),
                    Telefone = table.Column<string>(maxLength: 12, nullable: true),
                    Logradouro = table.Column<string>(maxLength: 150, nullable: true),
                    Complemento = table.Column<string>(maxLength: 150, nullable: true),
                    Numero = table.Column<string>(maxLength: 50, nullable: true),
                    Bairro = table.Column<string>(maxLength: 150, nullable: true),
                    Municipio = table.Column<string>(maxLength: 150, nullable: true),
                    Uf = table.Column<int>(nullable: true),
                    OutroEstado = table.Column<string>(maxLength: 50, nullable: true),
                    Cep = table.Column<long>(nullable: true),
                    UsuarioId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_requerente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_requerente_Documento_IdDocumento",
                        column: x => x.IdDocumento,
                        principalTable: "Documento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_requerente_Pais_IdPais",
                        column: x => x.IdPais,
                        principalTable: "Pais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_requerente_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Obra",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    DataDeInclusao = table.Column<DateTime>(nullable: false),
                    DataDeAlteracao = table.Column<DateTime>(nullable: true),
                    IdUsuario = table.Column<int>(nullable: false),
                    IdGeneroObra = table.Column<int>(nullable: false),
                    IdRequerente = table.Column<int>(nullable: false),
                    IdRepresentante = table.Column<int>(nullable: true),
                    EhRequerimento = table.Column<int>(nullable: true),
                    EhAverbacao = table.Column<int>(nullable: true),
                    EhInedita = table.Column<int>(nullable: true),
                    EhPublicada = table.Column<int>(nullable: true),
                    Titulo = table.Column<string>(unicode: false, maxLength: 200, nullable: false),
                    Grafica = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Editora = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Ano = table.Column<string>(unicode: false, maxLength: 4, nullable: true),
                    Volume = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    Edicao = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    LocalPublicacao = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Paginas = table.Column<int>(nullable: true),
                    AdaptacaoTituloOriginal = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    AdaptacaoAutorOriginal = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    TraducaoTituloOriginal = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    TraducaoAutorOriginal = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    Observacoes = table.Column<string>(unicode: false, nullable: true),
                    Situacao = table.Column<int>(nullable: true),
                    DataDaExclusao = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Obra", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Obra_GeneroObra_IdGeneroObra",
                        column: x => x.IdGeneroObra,
                        principalTable: "GeneroObra",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Obra_Representante_IdRepresentante",
                        column: x => x.IdRepresentante,
                        principalTable: "Representante",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Obra_requerente_IdRequerente",
                        column: x => x.IdRequerente,
                        principalTable: "requerente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Obra_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequerenteRepresentante",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    DataDeInclusao = table.Column<DateTime>(nullable: false),
                    DataDeAlteracao = table.Column<DateTime>(nullable: true),
                    Parentesco = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    RequerenteId = table.Column<int>(nullable: true),
                    RepresentanteId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequerenteRepresentante", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequerenteRepresentante_Representante_RepresentanteId",
                        column: x => x.RepresentanteId,
                        principalTable: "Representante",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequerenteRepresentante_requerente_RequerenteId",
                        column: x => x.RequerenteId,
                        principalTable: "requerente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ObraVinculo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    DataDeInclusao = table.Column<DateTime>(nullable: false),
                    DataDeAlteracao = table.Column<DateTime>(nullable: true),
                    IdObraOriginal = table.Column<int>(nullable: false),
                    EhSupressaoConteudo = table.Column<int>(nullable: true),
                    EhAcrescimoConteudo = table.Column<int>(nullable: true),
                    EhPublicacao = table.Column<int>(nullable: true),
                    EhMudancaTitulo = table.Column<int>(nullable: true),
                    EhTransferenciaTitular = table.Column<int>(nullable: true),
                    Outros = table.Column<string>(unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObraVinculo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObraVinculo_Obra_IdObraOriginal",
                        column: x => x.IdObraOriginal,
                        principalTable: "Obra",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Registro",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    DataDeInclusao = table.Column<DateTime>(nullable: false),
                    DataDeAlteracao = table.Column<DateTime>(nullable: true),
                    IdObra = table.Column<int>(nullable: false),
                    IdArquivo = table.Column<int>(nullable: false),
                    HashTransacao = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registro", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Registro_Arquivo_IdArquivo",
                        column: x => x.IdArquivo,
                        principalTable: "Arquivo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Registro_Obra_IdObra",
                        column: x => x.IdObra,
                        principalTable: "Obra",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RegistroTransmissao",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    DataDeInclusao = table.Column<DateTime>(nullable: false),
                    DataDeAlteracao = table.Column<DateTime>(nullable: true),
                    HashTransacao = table.Column<string>(unicode: false, maxLength: 200, nullable: false),
                    DataDeConclusao = table.Column<DateTime>(nullable: true),
                    De = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    Para = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    Bloco = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    Valor = table.Column<decimal>(nullable: true),
                    GasLimite = table.Column<int>(nullable: true),
                    GasUsado = table.Column<int>(nullable: true),
                    GasPreco = table.Column<decimal>(nullable: true),
                    CustoFinal = table.Column<decimal>(nullable: true),
                    Nonce = table.Column<int>(nullable: true),
                    Posicao = table.Column<int>(nullable: true),
                    DadoEnviado = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    NotaPrivada = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    Status = table.Column<string>(unicode: false, maxLength: 2, nullable: true),
                    Mensagem = table.Column<string>(unicode: false, nullable: true),
                    RegistroId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistroTransmissao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegistroTransmissao_Registro_RegistroId",
                        column: x => x.RegistroId,
                        principalTable: "Registro",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Documento_IdPais",
                table: "Documento",
                column: "IdPais");

            migrationBuilder.CreateIndex(
                name: "IX_Documento_IdTipoDocumento",
                table: "Documento",
                column: "IdTipoDocumento");

            migrationBuilder.CreateIndex(
                name: "IX_GeneroObraDescricao_GeneroObraId",
                table: "GeneroObraDescricao",
                column: "GeneroObraId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneroObraDescricao_IdIdioma",
                table: "GeneroObraDescricao",
                column: "IdIdioma");

            migrationBuilder.CreateIndex(
                name: "IX_Obra_IdGeneroObra",
                table: "Obra",
                column: "IdGeneroObra");

            migrationBuilder.CreateIndex(
                name: "IX_Obra_IdRepresentante",
                table: "Obra",
                column: "IdRepresentante");

            migrationBuilder.CreateIndex(
                name: "IX_Obra_IdRequerente",
                table: "Obra",
                column: "IdRequerente");

            migrationBuilder.CreateIndex(
                name: "IX_Obra_IdUsuario",
                table: "Obra",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_ObraVinculo_IdObraOriginal",
                table: "ObraVinculo",
                column: "IdObraOriginal");

            migrationBuilder.CreateIndex(
                name: "IX_PaisDescricao_IdIdioma",
                table: "PaisDescricao",
                column: "IdIdioma");

            migrationBuilder.CreateIndex(
                name: "IX_PaisDescricao_PaisId",
                table: "PaisDescricao",
                column: "PaisId");

            migrationBuilder.CreateIndex(
                name: "IX_Registro_IdArquivo",
                table: "Registro",
                column: "IdArquivo");

            migrationBuilder.CreateIndex(
                name: "IX_Registro_IdObra",
                table: "Registro",
                column: "IdObra");

            migrationBuilder.CreateIndex(
                name: "IX_RegistroTransmissao_RegistroId",
                table: "RegistroTransmissao",
                column: "RegistroId");

            migrationBuilder.CreateIndex(
                name: "IX_Representante_IdDocumento",
                table: "Representante",
                column: "IdDocumento",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Representante_IdUsuario",
                table: "Representante",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_requerente_IdDocumento",
                table: "requerente",
                column: "IdDocumento",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_requerente_IdPais",
                table: "requerente",
                column: "IdPais");

            migrationBuilder.CreateIndex(
                name: "IX_requerente_UsuarioId",
                table: "requerente",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_RequerenteRepresentante_RepresentanteId",
                table: "RequerenteRepresentante",
                column: "RepresentanteId");

            migrationBuilder.CreateIndex(
                name: "IX_RequerenteRepresentante_RequerenteId",
                table: "RequerenteRepresentante",
                column: "RequerenteId");

            migrationBuilder.CreateIndex(
                name: "IX_TipoDocumentoDescricao_IdIdioma",
                table: "TipoDocumentoDescricao",
                column: "IdIdioma");

            migrationBuilder.CreateIndex(
                name: "IX_TipoDocumentoDescricao_TipoDocumentoId",
                table: "TipoDocumentoDescricao",
                column: "TipoDocumentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_IdiomaId",
                table: "Usuario",
                column: "IdiomaId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_PaisId",
                table: "Usuario",
                column: "PaisId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioToken_UsuarioId",
                table: "UsuarioToken",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GeneroObraDescricao");

            migrationBuilder.DropTable(
                name: "LogEvento");

            migrationBuilder.DropTable(
                name: "ObraVinculo");

            migrationBuilder.DropTable(
                name: "PaisDescricao");

            migrationBuilder.DropTable(
                name: "RegistroTransmissao");

            migrationBuilder.DropTable(
                name: "RequerenteRepresentante");

            migrationBuilder.DropTable(
                name: "TipoDocumentoDescricao");

            migrationBuilder.DropTable(
                name: "UsuarioToken");

            migrationBuilder.DropTable(
                name: "Registro");

            migrationBuilder.DropTable(
                name: "Arquivo");

            migrationBuilder.DropTable(
                name: "Obra");

            migrationBuilder.DropTable(
                name: "GeneroObra");

            migrationBuilder.DropTable(
                name: "Representante");

            migrationBuilder.DropTable(
                name: "requerente");

            migrationBuilder.DropTable(
                name: "Documento");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "TipoDocumento");

            migrationBuilder.DropTable(
                name: "Idioma");

            migrationBuilder.DropTable(
                name: "Pais");
        }
    }
}
