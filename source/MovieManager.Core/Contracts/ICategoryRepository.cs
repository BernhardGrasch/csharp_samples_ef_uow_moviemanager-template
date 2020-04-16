using MovieManager.Core.Entities;

namespace MovieManager.Core.Contracts
{
    public interface ICategoryRepository
    {
       (string Category, int Amount) GetCategoryWithTheMostMovies();

        Category GetYearWithMostActionMovies(string categoryName);
    }
}
