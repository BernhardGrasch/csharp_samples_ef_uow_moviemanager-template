using MovieManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using Utils;

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
            bool isFirstLine = true;
            string filePath = MyFile.GetFullNameInApplicationTree(Filename);
            string[] lines = File.ReadAllLines(filePath);
            Dictionary<string, Category> categories = new Dictionary<string, Category>();
            List<Movie> movies = new List<Movie>();

            foreach(var item in lines)
            {
                if(!isFirstLine)
                {
                    string[] parts = item.Split(";");
                    string movieName = parts[0];
                    int year = Convert.ToInt32(parts[1]);
                    string categoryName = parts[2];
                    int movieDuration = Convert.ToInt32(parts[3]);

                    Movie newMovie = new Movie { Title = movieName, Year = year, Duration = movieDuration };
                    
                    if(!categories.ContainsKey(categoryName))
                    {
                        Category newCategory = new Category { CategoryName = categoryName };
                        categories.Add(categoryName, newCategory);
                        newCategory.Movies.Add(newMovie);
                        newMovie.Category = newCategory;
                    }
                    else
                    {
                        categories.TryGetValue(categoryName, out Category existingCategory);
                        newMovie.Category = existingCategory;
                        existingCategory.Movies.Add(newMovie);
                    }
                    movies.Add(newMovie);
                }
                isFirstLine = false;
            }
            return movies;
        }

    }
}
