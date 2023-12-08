 using Microsoft.EntityFrameworkCore;
using MovieLibraryEntities.Context;
using MovieLibraryEntities.Models;

namespace MovieLibraryEntities.Dao
{
    public class Repository : IRepository, IDisposable
    {
        private readonly IDbContextFactory<MovieContext> _contextFactory;
        private readonly MovieContext _context;

        public Repository(IDbContextFactory<MovieContext> contextFactory)
        {
            _contextFactory = contextFactory;
            _context = _contextFactory.CreateDbContext();
        }

        

        public IEnumerable<Movie> GetAll()
        {
            return _context.Movies.ToList();
        }

        public IEnumerable<Movie> Search(string searchString)
        {
            var allMovies = _context.Movies;
            var listOfMovies = allMovies.ToList();
            var temp = listOfMovies.Where(x => x.Title.Contains(searchString, StringComparison.CurrentCultureIgnoreCase));

            return temp;
        }

        public Movie GetById(int id)
        {
            return _context.Movies.FirstOrDefault(x => x.Id == id);
        }

        public List<Movie> FindMovie(string title)
        {
            // find by title - could return more than one item
            return new List<Movie>();
        }
        public void AddMovie(Movie movie)
        {
            var newMovie = new Movie
            {
                Title = movie.Title,
                ReleaseDate = movie.ReleaseDate
                
            };

            _context.Movies.Add(newMovie);
            _context.SaveChanges();
        }
        public void UpdateMovie(Movie updatedMovie, int id) 
        {
            var newMovie = _context.Movies.FirstOrDefault(x => x.Id == id);
            if (newMovie != null) 
            {
                newMovie.Title = updatedMovie.Title;
                newMovie.ReleaseDate = updatedMovie.ReleaseDate;

            };
            _context.Movies.Update(updatedMovie);
            _context.SaveChanges();
        }
        public void RemoveMovie(int id) 
        {
            var movie = _context.Movies.FirstOrDefault(x => x.Id == id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
                _context.SaveChanges();
            }

        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
