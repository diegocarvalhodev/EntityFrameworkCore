using System.Collections.Generic;

namespace Alura.Filmes.App.Negocio
{
    public class Ator
    {
        public int ID { get; set; }
        public string PrimeiroNome { get; set; }
        public string UltimoNome { get; set; }
        public IList<FilmeAtor> Filmografia { get; set; }

        public Ator()
        {
            Filmografia = new List<FilmeAtor>();
        }

        public override string ToString()
        {
            return $"Ator ({this.ID}): {this.PrimeiroNome} {this.UltimoNome}";
        }
    }
}
