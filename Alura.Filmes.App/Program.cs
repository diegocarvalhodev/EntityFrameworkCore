using Alura.Filmes.App.Dados;
using Alura.Filmes.App.Negocio;
using Alura.Filmes.App.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Data.SqlClient;

namespace Alura.Filmes.App
{
    class Program
    {
        static void Main(string[] args)
        {
            #region InvocacaoDosMetodos
            //CadastroDeAtor();
            //RecuperandoValoresDaPropriedadeShadowProperty();
            //Lista10AtoresModificadosRecentemente();
            //ListarFilmes();
            //ExibeFilmeAtor();
            //VisualizarFilmeEElenco();
            //ExibeAsCategorias();
            //ExibeFilmeECategoia();
            //ExibeFilmesDeUmaCategoria();
            //ExibeFilmesPorIdioma();
            //ValidacaoUnique();
            //FilmeClassificacao();
            //ExibeClientesFuncionarios();
            //ConsultasUsandoComandoSQL();
            //ExecucaoDeStoredProcedure();
            ExecutarInsertDelete();
            #endregion

            Console.WriteLine("Use o método mais adequado");

        }

        #region MetodosUsadosNoCurso
        private static void ExecutarInsertDelete()
        {
            using (var contexto = new AluraFilmesContexto())
            {
                contexto.LogSQLToConsole();

                var sql = "INSERT INTO dbo.language (name) VALUES ('Teste 1'), ('Teste 2'), ('Teste 3')";
                var registrosAfetados = contexto.Database.ExecuteSqlCommand(sql);
                Console.WriteLine($"Total de registros afetados: {registrosAfetados}");

                sql = "DELETE FROM dbo.language WHERE name LIKE ('Teste%')";
                registrosAfetados = contexto.Database.ExecuteSqlCommand(sql);
                Console.WriteLine($"Total de registros afetados: {registrosAfetados}");
            }
        }
        
        private static void ExecucaoDeStoredProcedure()
        {
            using (var contexto = new AluraFilmesContexto())
            {
                contexto.LogSQLToConsole();

                var categ = "Action";
                var paramCateg = new SqlParameter("category_name", categ);

                var paramTotal = new SqlParameter
                {
                    ParameterName = "total_actors",
                    Size = 4,
                    Direction = System.Data.ParameterDirection.Output
                };

                contexto.Database.ExecuteSqlCommand(
                    "dbo.total_actors_from_given_category @category_name, @total_actors OUTPUT",
                    paramCateg,
                    paramTotal);

                Console.WriteLine($"O total de atores na categoria {categ} é de {paramTotal.Value}");
            }
        }
        
        private static void ConsultasUsandoComandoSQL()
        {
            using (var contexto = new AluraFilmesContexto())
            {
                contexto.LogSQLToConsole();

                /* USANDO CONSULTA LINQ POR MÉTODO */
                //var atores = contexto.Atores
                //            .Include(a => a.Filmografia)
                //            .OrderByDescending(a => a.Filmografia.Count)
                //            .Take(5);

                /* USANDO FROMSQL */
                //var sql = @"SELECT a.*
                //            FROM dbo.actor a
                //            INNER JOIN ( SELECT TOP 5 a.actor_id, COUNT(*) AS total
                //                FROM dbo.actor a
                //                INNER JOIN dbo.film_actor fa ON fa.actor_id = a.actor_id
                //                GROUP BY a.actor_id
                //                ORDER BY total DESC ) filmes ON filmes.actor_id = a.actor_id";
                //var atores = contexto.Atores
                //                .FromSql(sql)
                //                .Include(a => a.Filmografia);

                /* USANDO FROMSQL */
                var sql = @"SELECT a.* FROM dbo.actor a
                            INNER JOIN top5_most_starred_actors filmes ON filmes.actor_id = a.actor_id";
                var atores = contexto.Atores
                                .FromSql(sql)
                                .Include(a => a.Filmografia);


                foreach (var ator in atores)
                {
                    Console.WriteLine($"O {ator.PrimeiroNome} {ator.UltimoNome} atuou em {ator.Filmografia.Count} filmes");
                }

                /* EXEMPLO COM CONSULTA LINQ */
                //var atores = from f in contexto.Atores
                //             let Total = f.Filmografia.Count
                //             orderby Total descending
                //             select new
                //             {
                //                 Nome = f.PrimeiroNome + " " + f.UltimoNome,
                //                 Total
                //             };

                //atores = atores.Take(5);

                //foreach (var ator in atores)
                //{
                //    Console.WriteLine("{0}, {1}", ator.Nome, ator.Total);
                //}                
            }
        }
        
        private static void ExibeClientesFuncionarios()
        {
            using (var contexto = new AluraFilmesContexto())
            {
                contexto.LogSQLToConsole();

                Console.WriteLine("Cliente:");
                foreach (var cliente in contexto.Clientes)
                {
                    Console.WriteLine(cliente);
                }

                Console.WriteLine("\nFuncionários");
                foreach (var funcionario in contexto.Funcionarios)
                {
                    Console.WriteLine(funcionario);
                }
            }
        }
        
        private static void FilmeClassificacao()
        {
            using (var contexto = new AluraFilmesContexto())
            {
                contexto.LogSQLToConsole();

                //var livre = ClassificacaoIndicativa.Livre;
                //Console.WriteLine(livre.ParaString());

                //var maior = ClassificacaoIndicativa.MaiorQue18;
                //Console.WriteLine(maior.ParaString());

                //Console.WriteLine("G".ParaValor());

                var filme = new Filme();
                filme.Titulo = "Senhor dos Anéis";
                filme.Duracao = 120;
                filme.AnoLancamento = "2000";
                filme.Classificacao = ClassificacaoIndicativa.MaiorQue14;
                filme.IdiomaFalado = contexto.Idiomas.First();

                contexto.Filmes.Add(filme);
                contexto.SaveChanges();

                var filmeInserido = contexto.Filmes.First(f => f.Titulo == "Senhor dos Anéis");
                Console.WriteLine(filmeInserido.Classificacao);
            }
        }
        
        private static void ValidacaoUnique()
        {
            using (var contexto = new AluraFilmesContexto())
            {
                contexto.LogSQLToConsole();

                var ator1 = new Ator() { PrimeiroNome = "Emma", UltimoNome = "Watson" };
                var ator2 = new Ator() { PrimeiroNome = "Emma", UltimoNome = "Watson" };

                contexto.Atores.AddRange(ator1, ator2);
                contexto.SaveChanges();

                var emmaWatson = contexto.Atores
                                  .Where(a => a.PrimeiroNome == "Emma" && a.UltimoNome == "Watson");

                Console.WriteLine($"Total de atores encontrados: {emmaWatson.Count()}");
            }
        }

        private static void ExibeFilmesPorIdioma()
        {
            using (var contexto = new AluraFilmesContexto())
            {
                contexto.LogSQLToConsole();

                var idiomas = contexto.Idiomas
                                .Include(f => f.FilmeFalado);

                foreach (var idioma in idiomas)
                {
                    Console.WriteLine(idioma);
                    Console.WriteLine();

                    foreach (var filme in idioma.FilmeFalado)
                    {
                        Console.WriteLine(filme);
                    }

                    Console.WriteLine("\n");
                }
            }
        }

        private static void ExibeFilmesDeUmaCategoria()
        {
            using (var contexto = new AluraFilmesContexto())
            {
                contexto.LogSQLToConsole();

                var categoria = contexto.Categorias
                                    .Include(c => c.Filmes)
                                    .ThenInclude(cc => cc.Filme)
                                    .First();

                Console.WriteLine(categoria);
                Console.WriteLine();

                foreach (var filme in categoria.Filmes)
                {
                    Console.WriteLine(filme.Filme);
                }
            }
        }

        private static void ExibeFilmeECategoia()
        {
            using (var contexto = new AluraFilmesContexto())
            {
                contexto.LogSQLToConsole();

                var filme = contexto.Filmes
                            .Include(f => f.Categorias)
                            .ThenInclude(c => c.Categoria)
                            .First();

                Console.WriteLine(filme);

                foreach (var item in filme.Categorias)
                {
                    var categoriaId = item.Categoria.ID;
                    var categoriaNome = item.Categoria.Nome;
                    var lastUpdate = contexto.Entry(item.Categoria).Property("last_update").CurrentValue;
                    Console.WriteLine($"Categoria: {categoriaNome} (ID: {categoriaId}) - UP: {lastUpdate}");
                    //Console.WriteLine(item.Categoria);
                }
            }
        }

        private static void ExibeAsCategorias()
        {
            using (var contexto = new AluraFilmesContexto())
            {
                contexto.LogSQLToConsole();

                foreach (var item in contexto.Categorias)
                {
                    Console.WriteLine(item);
                }
            }
        }

        private static void VisualizarFilmeEElenco()
        {
            using (var contexto = new AluraFilmesContexto())
            {
                contexto.LogSQLToConsole();

                var filme = contexto.Filmes.Include(f => f.Atores).ThenInclude(fa => fa.Ator).First();
                Console.WriteLine(filme);

                Console.WriteLine("\nElenco:");

                foreach (var item in filme.Atores)
                {
                    Console.WriteLine(item.Ator);
                }

            }
        }

        private static void ExibeFilmeAtor()
        {
            using (var contexto = new AluraFilmesContexto())
            {
                contexto.LogSQLToConsole();

                foreach (var item in contexto.Elenco)
                {
                    var entidade = contexto.Entry(item);
                    var filmId = entidade.Property("film_id").CurrentValue;
                    var actorId = entidade.Property("actor_id").CurrentValue;
                    var lastUpdate = entidade.Property("last_update").CurrentValue;

                    Console.WriteLine($"Filme {filmId}, Ator {actorId}, Last Updatde {lastUpdate}");
                }

            }
        }

        private static void ListarFilmes()
        {
            using (var contexto = new AluraFilmesContexto())
            {
                contexto.LogSQLToConsole();

                foreach (var filme in contexto.Filmes)
                {
                    Console.WriteLine(filme);
                }
            }
        }

        private static void Lista10AtoresModificadosRecentemente()
        {
            using (var contexto = new AluraFilmesContexto())
            {
                contexto.LogSQLToConsole();

                var atores = contexto.Atores
                                .OrderByDescending(a => EF.Property<DateTime>(a, "last_update"))
                                .Take(10);

                foreach (var ator in atores)
                {
                    Console.WriteLine("{0} - {1}", ator, contexto.Entry(ator).Property("last_update").CurrentValue);
                }
            }
        }

        private static void RecuperandoValoresDaPropriedadeShadowProperty()
        {
            using (var contexto = new AluraFilmesContexto())
            {
                contexto.LogSQLToConsole();

                var ator = contexto.Atores.First();

                Console.WriteLine(ator + " " + contexto.Entry(ator).Property("last_update").CurrentValue);
            }
        }

        private static void CadastroDeAtor()
        {
            using (var contexto = new AluraFilmesContexto())
            {
                contexto.LogSQLToConsole();

                Ator ator = new Ator()
                {
                    PrimeiroNome = "Will",
                    UltimoNome = "Smith"
                };
                contexto.Entry(ator).Property("last_update").CurrentValue = DateTime.Now;

                contexto.Atores.Add(ator);
                contexto.SaveChanges();
            }
        }

        #endregion
    }
}