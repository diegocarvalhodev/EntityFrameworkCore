using System;
using Alura.Filmes.App.Negocio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alura.Filmes.App.Dados
{
    public class AtorConfiguration : IEntityTypeConfiguration<Ator>
    {
        public void Configure(EntityTypeBuilder<Ator> builder)
        {
            builder.ToTable("actor");

            builder
                .Property(p => p.ID)
                .HasColumnName("actor_id");

            builder
                .Property(p => p.PrimeiroNome)
                .HasColumnName("first_name")
                .HasColumnType("varchar(45)")
                .IsRequired();

            builder
                .Property(p => p.UltimoNome)
                .HasColumnName("last_name")
                .HasColumnType("varchar(45)")
                .IsRequired();

            builder
                .Property<DateTime>("last_update")
                .HasColumnType("datetime")
                .HasDefaultValueSql("getdate()");

            builder
                .HasIndex(a => a.UltimoNome)
                .HasName("idx_actor_last_name");

            builder
                .HasAlternateKey(a => new { a.PrimeiroNome, a.UltimoNome });
        }
    }
}
