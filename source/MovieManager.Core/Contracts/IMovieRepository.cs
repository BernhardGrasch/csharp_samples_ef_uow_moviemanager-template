using MovieManager.Core.Entities;

namespace MovieManager.Core.Contracts
{
    public interface IMovieRepository
    {
        Movie GetLongestFilm();

    }
}
