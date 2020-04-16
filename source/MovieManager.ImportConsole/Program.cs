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

                var categories = movies.GroupBy(movie => movie.Category.CategoryName).Select(movie => movie.First());

                Console.WriteLine($"  Es wurden {movies.Count()} Movies in {categories.Count()} Kategorien eingelesen!");

                unitOfWork.MovieRepository.AddRange(movies);
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
                var longestMovie = unitOfWork.MovieRepository.GetLongestMovie();
                Console.WriteLine($"Längster Film: {longestMovie}; Länge: {GetDurationAsString(longestMovie)}");
                
                // Top Kategorie:
                //   - Jene Kategorie mit den meisten Filmen.
                //TODO


                // Jahr der Kategorie "Action":
                //  - In welchem Jahr wurden die meisten Action-Filme veröffentlicht?
                //TODO


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
        }

        private static string GetDurationAsString(double minutes, bool withSeconds = true)
        {
            double outHours = 0.0;
            double outMinutes = 0.0;

            outHours = minutes / 60.0;
            outMinutes = minutes - (outHours * 60);

            return $"{outHours.ToString("00")} h {outMinutes.ToString("00")} min";
        }
    }
}