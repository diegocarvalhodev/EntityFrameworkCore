using System.Collections.Generic;

namespace Alura.Filmes.App.Negocio
{
    public class Idioma
    {
        public byte ID { get; set; }
        public string Nome { get; set; }
        public IList<Filme> FilmeFalado { get; set; }
        public IList<Filme> FilmeOriginal { get; set; }

        public Idioma()
        {
            FilmeFalado = new List<Filme>();
            FilmeOriginal = new List<Filme>();
        }

        public override string ToString()
        {
            return $"Idioma ({this.ID}): {this.Nome}";
        }
    }
}
