namespace Alura.Filmes.App.Negocio
{
    public class Pessoa
    {
        public byte ID { get; set; }
        public string PrimeiroNome { get; set; }
        public string UltimoNome { get; set; }
        public string Email { get; set; }
        public bool Ativo { get; set; }

        public override string ToString()
        {
            var tipo = this.GetType().Name;
            return $"{tipo} ({this.ID}): {this.PrimeiroNome} {this.UltimoNome} - {this.Ativo}";
        }
    }
}
