using MovieManager.Core.Contracts;
using MovieManager.Core.Entities;
using System;
using System.Collections.Generic;
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

        public void AddRange(IEnumerable<Movie> movies)
        {
            _dbContext.Movies.AddRange(movies);
        }

        public int GetLongestMovie()
        {
            return _dbContext.Movies.Max(movie => movie.Duration);
        }

        public int GetCategoryWithMostMovies()
        {
            return _dbContext.Movies
                .Where(movie => movie.Category.CategoryName.Equals("Action")).Count();
        }

        public int YearOfActionsMovies()
        {
            return _dbContext.Movies
                .Select(movie => movie)
                .Where(movie => movie.Category.CategoryName.Equals("Action"))
                .AsEnumerable()
                .GroupBy(movie => movie.Year)
                .OrderByDescending(movie => movie.Count())
                First().Key;
        }
    }
}