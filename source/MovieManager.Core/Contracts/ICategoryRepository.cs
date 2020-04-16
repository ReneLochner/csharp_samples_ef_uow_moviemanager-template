using MovieManager.Core.Entities;

namespace MovieManager.Core.Contracts
{
    public interface ICategoryRepository
    {
        (Category Category, int amount) GetCategoryWithMostMovies();
    }
}
