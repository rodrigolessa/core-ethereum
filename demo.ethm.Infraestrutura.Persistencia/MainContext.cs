using demo.ethm.Dominio.Entities;
using demo.ethm.Dominio.Entities.Enums;
using demo.ethm.Dominio.Entities.ValueObjects;
using demo.ethm.Infraestrutura.ExtensionLogger.Model;
using demo.ethm.Infraestrutura.Persistencia.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;
using MySql.Data.EntityFrameworkCore.Extensions;
using System;

namespace demo.ethm.Infraestrutura.Persistencia
{
    public class MainContext : DbContext
    {
        public MainContext(DbContextOptions options)
            : base(options)
        {

        }

        public MainContext()
        {

        }

        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<UsuarioToken> UsuarioToken { get; set; }
        public virtual DbSet<GeneroObra> GeneroObra { get; set; }
        //public virtual DbSet<GeneroObraDescricao> GeneroObraDescricao { get; set; }
        public virtual DbSet<Idioma> Idioma { get; set; }
        public virtual DbSet<Pais> Pais { get; set; }
        //public virtual DbSet<PaisDescricao> PaisDescricao { get; set; }
        public virtual DbSet<TipoDocumento> TipoDocumento { get; set; }
        //public virtual DbSet<TipoDocumentoDescricao> TipoDocumentoDescricao { get; set; }
        public virtual DbSet<Arquivo> Arquivo { get; set; }
        public virtual DbSet<Obra> Obra { get; set; }
        //public virtual DbSet<ObraVinculo> ObraVinculo { get; set; }
        public virtual DbSet<Registro> Registro { get; set; }
        public virtual DbSet<RegistroTransmissao> RegistroTransmissao { get; set; }
        public virtual DbSet<Representante> Representante { get; set; }
        public virtual DbSet<Requerente> Requerente { get; set; }
        //public virtual DbSet<RequerenteObra> RequerenteObra { get; set; }
        //public virtual DbSet<RequerenteRepresentante> RequerenteRepresentante { get; set; }
        public virtual DbSet<Documento> Documento { get; set; }
        //public virtual DbSet<Pagamento> Pagamento { get; set; }

        public virtual DbSet<LogEvento> LogEvento { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("server=localhost;database=demo.ethm;user=demo.ethmRoot;password=lcD!12345");
                optionsBuilder.EnableSensitiveDataLogging(true);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new RequerenteEntityTypeConfiguration());

            /////////////////////////////////////////////////////
            // Usuario

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Nome)
                    .IsUnicode(false);

                entity.Property(e => e.Senha)
                    .IsUnicode(false);

                entity.HasOne(e => e.Idioma)
                    .WithMany(e => e.Usuarios);
                //.HasForeignKey(e => e.IdTenant)
                //.WillCascadeOnDelete(false);

                entity.HasMany(e => e.Obras)
                    .WithOne(e => e.Usuario)
                    .HasForeignKey(e => e.IdUsuario);
                //.WillCascadeOnDelete(false);

                //entity.HasOne(e => e.Pais)
                //.WithMan(e => e.Usuario)
                //.HasForeignKey(e => e.IdTenant)
                //.WillCascadeOnDelete(false);

                entity.HasMany(e => e.Representantes)
                    .WithOne(e => e.Usuario)
                    .HasForeignKey(e => e.IdUsuario);
                //.WillCascadeOnDelete(false);

                entity.HasMany(e => e.Requerentes)
                    .WithOne(e => e.Usuario);
                //.HasForeignKey(e => e.IdTenant)
                //.WillCascadeOnDelete(false);

                entity.HasMany(e => e.UsuarioToken)
                    .WithOne(e => e.Usuario);
                //.HasForeignKey(e => e.IdTenant)
                //.WillCascadeOnDelete(false);
            });

            //modelBuilder.Entity<Usuario>().OwnsOne(
            //    o => o.Email,
            //    sa =>
            //    {
            //        sa.Property(p => p.Endereco)
            //            .HasColumnName("Email")
            //            .HasMaxLength(Email.EnderecoMaxLength)
            //            .IsRequired();

            //        // TODO: Criar indice único por endereço de e-mail do usuário
            //        //.HasAnnotation("", new IndexAnnotation(new IndexAttribute("IX_Email", 3) { IsUnique = true }));
            //    });

            ////builder.Entity<Usuario>().HasIndex(u => u.Email).IsUnique();

            //modelBuilder.Entity<Usuario>().OwnsOne(
            //    o => o.Telefone,
            //    sa =>
            //    {
            //        sa.Property(p => p.DDD)
            //            .HasColumnName("DDD")
            //            .HasMaxLength(Telefone.DDDMaxLength)
            //            .IsRequired(false);

            //        sa.Property(p => p.Numero)
            //            .HasColumnName("Telefone")
            //            .HasMaxLength(Telefone.NumeroMaxLength)
            //            .IsRequired(false);
            //    });


            //modelBuilder.Entity<Usuario>().OwnsOne(
            //o => o.Endereco,
            //sa =>
            //{
            //    sa.Property(p => p.Logradouro)
            //        .HasColumnName("Logradouro")
            //        .HasMaxLength(Endereco.LogradouroMaxLength);

            //    sa.Property(x => x.Bairro)
            //        .HasColumnName("Bairro")
            //        .HasMaxLength(Endereco.BairroMaxLength);

            //    sa.Property(x => x.Municipio)
            //        .HasColumnName("Cidade")
            //        .HasMaxLength(Endereco.MunicipioMaxLength);

            //    sa.Property(x => x.Complemento)
            //        .HasColumnName("Complemento")
            //        .HasMaxLength(Endereco.ComplementoMaxLength);

            //    sa.Property(x => x.Numero)
            //        .HasColumnName("Numero")
            //        .HasMaxLength(Endereco.NumeroMaxLength);

            //    sa.Property(x => x.Uf)
            //        .HasColumnName("Uf");
            //        //.IsRequired(false)
            //        //.HasMaxLength(2)
            //        //.HasConversion(
            //            //v => v.ToString(),
            //            //v => (Uf)Enum.Parse(typeof(Uf), v));

            //    sa.Property(x => x.OutroEstado)
            //        .HasColumnName("OutroEstado")
            //        .HasMaxLength(Endereco.EstadoMaxLength);

            //    sa.OwnsOne(x => x.Cep,
            //        ce => ce.Property(c => c.CepCod)
            //            .HasColumnName("Cep")
            //            .HasMaxLength(Cep.CepMaxLength)
            //            .IsRequired(false)
            //    );
            //});

            //// Ignore property using Fluent API
            ////modelBuilder.Entity<Usuario>().Ignore(b => b.validationResults);

            modelBuilder.Entity<Usuario>().ToTable("Usuario");

            modelBuilder.Entity<UsuarioToken>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Token)
                    .IsUnicode(false);
            });

            /////////////////////////////////////////////////////
            // GeneroObra

            modelBuilder.Entity<GeneroObra>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasMany(e => e.Descricoes)
                    .WithOne(e => e.GeneroObra)
                    .OnDelete(DeleteBehavior.Cascade);
                //.WithRequired(e => e.GeneroObra)
                //.WillCascadeOnDelete(false);

                entity.HasMany(e => e.Obras)
                    .WithOne(e => e.GeneroObra)
                //.WithRequired(e => e.GeneroObra)
                    .HasForeignKey(e => e.IdGeneroObra);
                //.WillCascadeOnDelete(false);
            });

            modelBuilder.Entity<GeneroObraDescricao>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .IsUnicode(true);
            });

            // Obra literária

            modelBuilder.Entity<Obra>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Titulo)
                    .IsUnicode(false);

                entity.Property(e => e.Grafica)
                    .IsUnicode(false);

                entity.Property(e => e.Editora)
                    .IsUnicode(false);

                entity.Property(e => e.Ano)
                    .IsUnicode(false);

                entity.Property(e => e.Volume)
                    .IsUnicode(false);

                entity.Property(e => e.Edicao)
                    .IsUnicode(false);

                entity.Property(e => e.LocalPublicacao)
                    .IsUnicode(false);

                entity.Property(e => e.AdaptacaoTituloOriginal)
                    .IsUnicode(false);

                entity.Property(e => e.AdaptacaoAutorOriginal)
                    .IsUnicode(false);

                entity.Property(e => e.TraducaoTituloOriginal)
                    .IsUnicode(false);

                entity.Property(e => e.TraducaoAutorOriginal)
                    .IsUnicode(false);

                entity.Property(e => e.Observacoes)
                    .IsUnicode(false);

                //entity.HasMany(e => e.Arquivo)
                //.WithOne(e => e.Obra);
                //.WithRequired(e => e.Obra)
                //.HasForeignKey(e => e.IdObra)
                //.WillCascadeOnDelete(false);

                //entity.HasOne(e => e.Vinculo)
                //.WithOne(e => e.ObraVinculo);
                //.HasForeignKey(e => e.IdObra)
                //.WillCascadeOnDelete(false);

                entity.HasMany(e => e.OriginalVinculos)
                    .WithOne(e => e.ObraOriginal)
                    .HasForeignKey(e => e.IdObraOriginal);

                entity.HasMany(e => e.Registros)
                    .WithOne(e => e.Obra)
                    .HasForeignKey(e => e.IdObra);
                //.WillCascadeOnDelete(false);

                //entity.HasMany(e => e.RequerenteObra)
                //.WithOne(e => e.Obra)
                //.HasForeignKey(e => e.IdObra);
                //.WillCascadeOnDelete(false);
            });

            modelBuilder.Entity<ObraVinculo>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Outros)
                    .IsUnicode(false);

                //entity.HasOne(e => e.Obra)
                //.WithOne(e => e.Vinculo);
                //.HasForeignKey(e => e.IdObra);
                //.WillCascadeOnDelete(false);
            });

            /////////////////////////////////////////////////////
            // Idioma e Região

            modelBuilder.Entity<Idioma>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Descricao)
                    .IsUnicode(true);

                entity.Property(e => e.ISO639Alfa2)
                    .IsUnicode(false);

                entity.Property(e => e.ISO639Alfa3)
                    .IsUnicode(false);

                entity.Property(e => e.HtmlCode)
                    .IsUnicode(false);

                entity.HasMany(e => e.GeneroObraDescricoes)
                    .WithOne(e => e.Idioma)
                    .HasForeignKey(e => e.IdIdioma)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.PaisDescricoes)
                    .WithOne(e => e.Idioma)
                    .HasForeignKey(e => e.IdIdioma)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.TipoDocumentoDescricoes)
                    .WithOne(e => e.Idioma)
                    .HasForeignKey(e => e.IdIdioma)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Pais>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.ISO3166Alfa2)
                    .IsUnicode(false);

                entity.Property(e => e.ISO3166Alfa3)
                    .IsUnicode(false);

                entity.HasMany(e => e.Documentos)
                    .WithOne(e => e.Pais)
                //.WithOptional(e => e.Pais)
                    .HasForeignKey(e => e.IdPais);

                entity.HasMany(e => e.Descricoes)
                    .WithOne(e => e.Pais)
                    .OnDelete(DeleteBehavior.Cascade);
                //.HasForeignKey(e => e.IdPais)
            });

            modelBuilder.Entity<PaisDescricao>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Descricao)
                    .IsUnicode(true);
            });

            modelBuilder.Entity<TipoDocumento>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasMany(e => e.Documentos)
                    .WithOne(e => e.TipoDocumento)
                    .HasForeignKey(e => e.IdTipoDocumento);
                //.WillCascadeOnDelete(false);

                entity.HasMany(e => e.Descricoes)
                    .WithOne(e => e.TipoDocumento);
                //.HasForeignKey(e => e.IdTipoDocumento)
                //.WillCascadeOnDelete(false);
            });

            modelBuilder.Entity<TipoDocumentoDescricao>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .IsUnicode(true);
                entity.HasOne(e => e.Idioma)
                    .WithMany(e => e.TipoDocumentoDescricoes);
                entity.HasOne(e => e.TipoDocumento)
                    .WithMany(e => e.Descricoes);
            });

            /////////////////////////////////////////////////////
            // Arquivo e registro da Obra

            modelBuilder.Entity<Arquivo>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Extensao)
                    .IsUnicode(false);

                entity.Property(e => e.Mime)
                    .IsUnicode(false);

                entity.Property(e => e.Tamanho)
                    .IsUnicode(false);

                entity.Property(e => e.MD5)
                    .IsUnicode(false);

                entity.Property(e => e.SHA256)
                    .IsUnicode(false);

                entity.Property(e => e.SHA512)
                    .IsUnicode(false);

                entity.Property(e => e.JWTCertificado)
                .IsUnicode(false);

                entity.Property(e => e.JWTChavePublica)
                    .IsUnicode(false);

                entity.HasMany(e => e.Registros)
                    .WithOne(e => e.Arquivo)
                    .HasForeignKey(e => e.IdArquivo);
                //.WillCascadeOnDelete(false);
            });


            modelBuilder.Entity<Registro>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.HashTransacao)
                    .IsUnicode(false);

                entity.HasMany(e => e.RegistroTransmissao)
                    .WithOne(e => e.Registro);
                //.HasForeignKey(e => e.IdRegistro)
                //.WillCascadeOnDelete(false);
            });

            modelBuilder.Entity<RegistroTransmissao>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.HashTransacao)
                .IsRequired()
                .IsUnicode(false);

                entity.Property(e => e.De)
                .IsUnicode(false);

                entity.Property(e => e.Para)
                .IsUnicode(false);

                entity.Property(e => e.Bloco)
                .IsUnicode(false);
                //entity.Property(e => e.Valor);
                //.HasPrecision(18, 9);

                //entity.Property(e => e.GasPreco);
                //.HasPrecision(18, 9);

                //entity.Property(e => e.CustoFinal);
                //.HasPrecision(18, 9);

                entity.Property(e => e.DadoEnviado)
                        .IsUnicode(false);

                entity.Property(e => e.NotaPrivada)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .IsUnicode(false);

                entity.Property(e => e.Mensagem)
                    .IsUnicode(false);
            });

            /////////////////////////////////////////////////////
            // Requerente e seu Representante

            modelBuilder.Entity<Representante>()
                .HasOne(a => a.Documento)
                .WithOne(b => b.Representante)
                .HasForeignKey<Representante>(b => b.IdDocumento);

            modelBuilder.Entity<Representante>(entity =>
            {
                entity.HasKey(e => e.Id);

                //entity.HasOne(e => e.Usuario)
                //.WithMany(e => e.Representantes);

                entity.HasMany(e => e.RequerenteRepresentantes)
                    .WithOne(e => e.Representante);
                //.HasForeignKey(e => e.IdRepresentante)
                //.WillCascadeOnDelete(false);

                entity.HasMany(e => e.Obras)
                    .WithOne(e => e.Representante)
                    .HasForeignKey(e => e.IdRepresentante);
            });



            modelBuilder.Entity<RequerenteRepresentante>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Parentesco).IsRequired()
                    .IsUnicode(false);
                entity.HasOne(e => e.Requerente)
                    .WithMany(e => e.RequerenteRepresentantes);
                entity.HasOne(e => e.Representante)
                    .WithMany(e => e.RequerenteRepresentantes);
            });

            modelBuilder.Entity<Documento>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Identificador)
                    .IsRequired()
                    .IsUnicode(false);
            });
        }
    }
}