using Alura.Filmes.App.Negocio;
using System.Collections.Generic;
using System.Linq;

namespace Alura.Filmes.App.Extensions
{
    public static class ClassificacaoIndicativaExtensions
    {
        private static readonly IDictionary<string, ClassificacaoIndicativa> mapa;

        static ClassificacaoIndicativaExtensions()
        {
            mapa = new Dictionary<string, ClassificacaoIndicativa>
            {
                { "G", ClassificacaoIndicativa.Livre },
                { "PG", ClassificacaoIndicativa.MaiorQue10 },
                { "PG-13", ClassificacaoIndicativa.MaiorQue13 },
                { "R", ClassificacaoIndicativa.MaiorQue14 },
                { "NC-17", ClassificacaoIndicativa.MaiorQue18 },
            };
        }
        public static string ParaString(this ClassificacaoIndicativa valor)
        {
            return mapa.First(m => m.Value == valor).Key;
        }

        public static ClassificacaoIndicativa ParaValor(this string texto)
        {
            return mapa.First(m => m.Key == texto).Value;
        }
    }
}
