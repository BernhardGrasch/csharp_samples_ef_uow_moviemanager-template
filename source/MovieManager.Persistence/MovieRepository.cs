﻿using MovieManager.Core.Contracts;
using MovieManager.Core.Entities;
using System.Linq;

namespace MovieManager.Persistence
{
    public class MovieRepository : IMovieRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public MovieRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddRange(Movie[] movies)
        {
            _dbContext.Movies.AddRange(movies);
        }

        public Movie GetLongestFilm()
        {
            return _dbContext
                .Movies
                .OrderByDescending(m => m.Duration)
                .ThenByDescending(m => m.Title)
                .First();
        }

        public int GetYearOfMostActionMovies()
        {
            return _dbContext
                 .Movies
                 .Where(m => m.Category.CategoryName == "Action")
                 .GroupBy(m => m.Year)
                 .Select(m => new
                 {
                     Year = m.Key,
                     Count = m.Count()
                 })
                 .OrderByDescending(m => m.Count)
                 .Select(m => m.Year)
                 .First();
        }
    }
}