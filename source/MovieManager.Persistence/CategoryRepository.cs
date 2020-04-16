using MovieManager.Core.Contracts;
using MovieManager.Core.Entities;
using System.Linq;

namespace MovieManager.Persistence
{
    internal class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddRange(Category[] categories)
        {
            _dbContext.Categories.AddRange(categories);
        }

        (string Category, int Amount) ICategoryRepository.GetCategoryWithTheMostMovies()
        {
            return _dbContext
                .Categories
                .Select(c => new
                {
                    Category = c.CategoryName,
                    Amount = c.Movies.Count()
                })
                .OrderByDescending(c => c.Amount)
                .AsEnumerable()
                .Select(c => (c.Category, c.Amount))
                .First();
        }

    }
}