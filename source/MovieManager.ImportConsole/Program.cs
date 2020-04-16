using MovieManager.Core;
using MovieManager.Core.Entities;
using MovieManager.Persistence;
using System;
using System.Linq;

namespace MovieManager.ImportConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            InitData();
            AnalyzeData();

            Console.WriteLine();
            Console.Write("Beenden mit Eingabetaste ...");
            Console.ReadLine();
        }

        private static void InitData()
        {
            Console.WriteLine("***************************");
            Console.WriteLine("          Import");
            Console.WriteLine("***************************");

            Console.WriteLine("Import der Movies und Categories in die Datenbank");
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                Console.WriteLine("Datenbank löschen");
                unitOfWork.DeleteDatabase();

                Console.WriteLine("Datenbank migrieren");
                unitOfWork.MigrateDatabase();

                Console.WriteLine("Movies/Categories werden eingelesen");

                var movies = ImportController.ReadFromCsv().ToArray();
                if (movies.Length == 0)
                {
                    Console.WriteLine("!!! Es wurden keine Movies eingelesen");
                    return;
                }

                var categories = movies
                                .Select(m => m.Category)
                                .Distinct()
                                .ToArray();

                Console.WriteLine($"  Es wurden {movies.Count()} Movies in {categories.Count()} Kategorien eingelesen!");

                unitOfWork.Save();
                Console.WriteLine();
            }
        }

        private static void AnalyzeData()
        {
            Console.WriteLine("***************************");
            Console.WriteLine("        Statistik");
            Console.WriteLine("***************************");

            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                // Längster Film: Bei mehreren gleichlangen Filmen, soll jener angezeigt werden, dessen Titel im Alphabet am weitesten vorne steht.
                // Die Dauer des längsten Films soll in Stunden und Minuten angezeigt werden!
                //TODO
                var longestMovie = unitOfWork.MovieRepository.GetLongestFilm();
                Console.WriteLine($"Längster Film: {longestMovie.Title}; Länge: {longestMovie.Duration}");


                // Top Kategorie:
                //   - Jene Kategorie mit den meisten Filmen.
                //TODO
                var categoryWithTheMostMovies = unitOfWork.CategoryRepository.GetCategoryWithTheMostMovies();
                Console.WriteLine($"Kategorie mit den meisten Filmen: '{categoryWithTheMostMovies.Category}'; Filme: {categoryWithTheMostMovies.Amount} ");

                // Jahr der Kategorie "Action":
                //  - In welchem Jahr wurden die meisten Action-Filme veröffentlicht?
                //TODO
                var yearWithMostActionMovies = unitOfWork.MovieRepository.GetYearOfMostActionMovies();
                Console.WriteLine($"Jahr der Action Filme: {yearWithMostActionMovies}");

            }









            // Kategorie Auswertung (Teil 1):
            //   - Eine Liste in der je Kategorie die Anzahl der Filme und deren Gesamtdauer dargestellt wird.
            //   - Sortiert nach dem Namen der Kategorie (aufsteigend).
            //   - Die Gesamtdauer soll in Stunden und Minuten angezeigt werden!
            //TODO


            // Kategorie Auswertung (Teil 2):
            //   - Alle Kategorien und die durchschnittliche Dauer der Filme der Kategorie
            //   - Absteigend sortiert nach der durchschnittlichen Dauer der Filme.
            //     Bei gleicher Dauer dann nach dem Namen der Kategorie aufsteigend sortieren.
            //   - Die Gesamtdauer soll in Stunden, Minuten und Sekunden angezeigt werden!
            //TODO
        }

        private static string GetDurationAsString(double minutes, bool withSeconds = true)
        {
            throw new NotImplementedException();
        }
    }
}
