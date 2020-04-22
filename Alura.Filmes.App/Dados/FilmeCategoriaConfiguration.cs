using Alura.Filmes.App.Negocio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Alura.Filmes.App.Dados
{
    public class FilmeCategoriaConfiguration : IEntityTypeConfiguration<FilmeCategoria>
    {
        public void Configure(EntityTypeBuilder<FilmeCategoria> builder)
        {
            builder
                .ToTable("film_category");

            builder
                .Property <int> ("film_id");

            builder
                .Property <byte> ("category_id");

            builder
                .Property<DateTime>("last_update")
                .HasColumnType("datetime")
                .HasDefaultValueSql("getdate()");

            builder
                .HasKey("film_id", "category_id");

            builder
                .HasOne(f => f.Filme)
                .WithMany(c => c.Categorias)
                .HasForeignKey("film_id");

            builder
                .HasOne(fc => fc.Categoria)
                .WithMany(c => c.Filmes)
                .HasForeignKey("category_id");
        }
    }
}
