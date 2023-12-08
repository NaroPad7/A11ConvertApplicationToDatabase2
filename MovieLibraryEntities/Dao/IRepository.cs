using MovieLibraryEntities.Models;

namespace MovieLibraryEntities.Dao
{
    public interface IRepository
    {
        IEnumerable<Movie> GetAll();
        IEnumerable<Movie> Search(string searchString);
        void AddMovie(Movie movie);
        Movie GetById(int id);
        List<Movie> FindMovie(string title);

        void UpdateMovie(Movie updatedMovie, int id);
        void RemoveMovie(int id);
    }
}
