using MovieManager.Core.Entities;
using System;
using System.Collections.Generic;
using Utils;
using System.IO;
using System.Text;
using System.Linq;

namespace MovieManager.Core
{
    public class ImportController
    {
        const string Filename = "movies.csv";

        /// <summary>
        /// Liefert die Movies mit den dazugehörigen Kategorien
        /// </summary>
        public static IEnumerable<Movie> ReadFromCsv()
        {
            string filePath = MyFile.GetFullNameInApplicationTree(Filename);
            string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);
            IList<Movie> movies = new List<Movie>();

            for (int i = 1; i < lines.Length; i++)
            {
                string item = lines[i];
                string[] parts = item.Split(';');
                string title = parts[0];
                int year = Convert.ToInt32(parts[1]);
                Category category = new Category
                {
                    CategoryName = parts[2]
                };
                int duration = Convert.ToInt32(parts[3]);
                Movie movieToAdd = new Movie
                {
                    Title = title,
                    Year = year,
                    Category = category,
                    Duration = duration
                };
                movies.Add(movieToAdd);
            }
            return movies.ToArray();
        }
    }
}
