using demo.ethm.Dominio.Entities;
using demo.ethm.Dominio.Entities.Enums;
using demo.ethm.Dominio.Entities.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace demo.ethm.Infraestrutura.Persistencia.EntityTypeConfigurations
{
    public class RequerenteEntityTypeConfiguration : IEntityTypeConfiguration<Requerente>
    {
        // Part of the IEntityTypeConfiguration interface

        public void Configure(EntityTypeBuilder<Requerente> builder)
        {
            builder.ToTable("requerente");
            builder.HasKey(o => o.Id);
            //builder.Property(o => o.Id)
            //    .ForSqlServerUseSequenceHiLo("orderseq", OrderingContext.DEFAULT_SCHEMA);
            //builder.Ignore(b => b.Eventos);

            // Email value object persisted as owned entity in EF Core 2.0
            //builder.OwnsOne(o => o.Email);

            //modelBuilder.Entity<Requerente>().OwnsOne(
            //o => o.Email,
            //sa =>
            //{
            //    sa.Property(p => p.Endereco)
            //        .HasColumnName("Email")
            //        .HasMaxLength(Email.EnderecoMaxLength)
            //        .IsRequired();

            //    // TODO: Criar indice único por endereço de e-mail do usuário
            //    //.HasAnnotation("", new IndexAnnotation(new IndexAttribute("IX_Email", 3) { IsUnique = true }));
            //});

            ////builder.Entity<Usuario>().HasIndex(u => u.Email).IsUnique();

            builder.Property<DateTime>("DataDeInclusao").IsRequired();

            //...Additional validations, constraints and code...
            //...

            builder.HasOne(e => e.Usuario)
                .WithMany(e => e.Requerentes);

            builder.HasOne(a => a.Documento)
                .WithOne(b => b.Requerente)
                .HasForeignKey<Requerente>(b => b.IdDocumento);

            builder.HasOne(e => e.Nacionalidade)
                .WithMany(e => e.Requerentes)
                .HasForeignKey(e => e.IdPais);

            builder.HasMany(e => e.Obras)
                .WithOne(e => e.Requerente)
                .HasForeignKey(e => e.IdRequerente);
            //.WillCascadeOnDelete(false);

            builder.HasMany(e => e.RequerenteRepresentantes)
                .WithOne(e => e.Requerente);
            //.HasForeignKey(e => e.IdRequerente)
            //.WillCascadeOnDelete(false);

            //modelBuilder.Entity<Requerente>().OwnsOne(
            //o => o.Telefone,
            //sa =>
            //{
            //    sa.Property(p => p.DDD)
            //        .HasColumnName("DDD")
            //        .HasMaxLength(Telefone.DDDMaxLength);

            //    sa.Property(p => p.Numero)
            //        .HasColumnName("Telefone")
            //        .HasMaxLength(Telefone.NumeroMaxLength);
            //});

            //modelBuilder.Entity<Requerente>().OwnsOne(
            //o => o.Endereco,
            //sa =>
            //{
            //    sa.Property(p => p.Logradouro)
            //        .HasColumnName("Logradouro")
            //        .IsRequired()
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
            //        .HasColumnName("Uf")
            //        .HasMaxLength(2)
            //        .HasConversion(
            //            v => v.ToString(),
            //            v => (Uf)Enum.Parse(typeof(Uf), v));

            //    sa.Property(x => x.OutroEstado)
            //        .HasColumnName("OutroEstado")
            //        .HasMaxLength(Endereco.EstadoMaxLength);

            //    sa.OwnsOne(x => x.Cep,
            //        ce => ce.Property(c => c.CepCod)
            //            .HasColumnName("Cep")
            //            .IsRequired()
            //            .HasMaxLength(Cep.CepMaxLength)
            //        );
            //});
        }
    }
}
