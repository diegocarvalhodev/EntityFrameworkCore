using System.Collections.Generic;

namespace Alura.Filmes.App.Negocio
{
    public class Categoria
    {
        public byte ID { get; set; }
        public string Nome { get; set; }
        public IList<FilmeCategoria> Filmes { get; set; }

        public Categoria()
        {
            Filmes = new List<FilmeCategoria>();
        }

        public override string ToString()
        {
            return $"Categoria ({this.ID}): {this.Nome}";
        }
    }
}
