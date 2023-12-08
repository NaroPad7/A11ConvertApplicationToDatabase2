
using Microsoft.EntityFrameworkCore;
using MovieLibraryEntities.Context;
using MovieLibraryEntities.Dao;
using MovieLibraryEntities.Models;
using System;
using System.Collections;
using System.Data.SqlTypes;
using System.Linq;

namespace ContextExample.Services;

/// <summary>
///     You would need to inject your interfaces here to execute the methods in Invoke()
///     See the commented out code as an example
/// </summary>
public class MainService : IMainService
{
    private readonly IRepository _repository;

    public MainService(IRepository repository)
    {
        _repository = repository;
    }

    public void Invoke()
    {
        /*Search Movie -substring case insensitive
        Add Movie
        List Movies
        Update Movie - Extra Credit
        Delete Movie - Extra Credit*/
        string choice;
        do
        {
            Console.WriteLine("----Menu----");
            Console.WriteLine("1. Search Movie by Substring");
            Console.WriteLine("2. Add Movie");
            Console.WriteLine("3. List Movies");
            Console.WriteLine("4. Update Movie");
            Console.WriteLine("5. Delete Movie");
            Console.WriteLine("6.Exit");
            Console.WriteLine("Enter a choice: ");
            choice = Console.ReadLine();
            // Searching movie by substring
            if (choice == "1")
            {
                Console.WriteLine("Please enter the name of the movie: ");
                var movieSearch = Console.ReadLine();
                
                var movies = _repository.Search(movieSearch).ToList();

                // list of movies with same substring
                Console.WriteLine("List of Movies with Substring in Title:");
                foreach (var i in movies)
                {
                    Console.WriteLine($"{i.Id} - {i.Title} ({i.ReleaseDate})");
                }

            }
            // adding a movie
            if(choice == "2")
            { 
                Console.WriteLine("Please enter the movie you are adding:");
                var userMovie = Console.ReadLine();
                Console.WriteLine("Please enter the release date of the movie: ");
                DateTime  realeaseDay = Convert.ToDateTime(Console.ReadLine());
                var newMovie  = new Movie { Title = userMovie, ReleaseDate = realeaseDay };
                
                _repository.AddMovie(newMovie);
                Console.WriteLine($"{userMovie} has been saved!");

            }
            // getting list of movies
            if(choice == "3")
            {
                try
                {
                    var movies = _repository.GetAll();

                    // list of movies
                    Console.WriteLine("List of Movies:");
                    foreach (var i in movies)
                    {
                        Console.WriteLine($"{i.Id} - {i.Title} ({i.ReleaseDate})");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error during listing movies: {ex.Message}");
                }
            }
            // choice 4 update movie
            if (choice == "4")
            {
                var movies = _repository.GetAll();

                // list of movies
                Console.WriteLine("List of Movies:");
                foreach (var i in movies)
                {
                    Console.WriteLine($"{i.Id} - {i.Title} ({i.ReleaseDate})");
                }
                Console.WriteLine("Please select the movie Id you will like to update: ");
                int movieId = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("What would you like the new title to be: ");
                var newTitle = Console.ReadLine();
                var updatedMovie = new Movie { Title = newTitle, };
                _repository.UpdateMovie(updatedMovie, movieId);
                Console.WriteLine($"{newTitle} had been updated!");
            }
            // Choice 5 delete movie
            if (choice == "5")
            {
                var movies = _repository.GetAll();

                // list of movies
                Console.WriteLine("List of Movies:");
                foreach (var i in movies)
                {
                    Console.WriteLine($"{i.Id} - {i.Title} ({i.ReleaseDate})");
                }

                Console.WriteLine("Please select the movie Id you will like to delete:");
                int movieId = Convert.ToInt32(Console.ReadLine());
                _repository.RemoveMovie(movieId);
            }
        } while (choice != "6");
        
        
        
       /* var movie = _repository.GetById(1) ;
        Console.WriteLine($"Your movie is {movie.Title}");
        Console.WriteLine(" ");*/
        
    }
}
