﻿using MovieManager.Core.Entities;
using System.Collections.Generic;

namespace MovieManager.Core.Contracts
{
    public interface IMovieRepository
    {
        void AddRange(IEnumerable<Movie> movies);

        int GetLongestMovie();

        int GetCategoryWithMostMovies();
    }
}
