using MovieManager.Core.Contracts;
using MovieManager.Core.Entities;
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
    }
}