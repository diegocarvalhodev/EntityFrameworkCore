using Alura.Filmes.App.Negocio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Alura.Filmes.App.Dados
{
    public class FilmeAtorConfiguration : IEntityTypeConfiguration<FilmeAtor>
    {
        public void Configure(EntityTypeBuilder<FilmeAtor> builder)
        {
            builder.ToTable("film_actor");

            builder.Property<int>("actor_id");

            builder.Property<int>("film_id");

            builder.Property<DateTime>("last_update")
                .HasColumnType("datetime")
                .HasDefaultValueSql("getdate()")
                .IsRequired();

            builder.HasKey("actor_id", "film_id");

            builder
                .HasOne(f => f.Filme)
                .WithMany(f => f.Atores)
                .HasForeignKey("film_id");

            builder
                .HasOne(a => a.Ator)
                .WithMany(fa => fa.Filmografia)
                .HasForeignKey("actor_id");
        }
    }
}
