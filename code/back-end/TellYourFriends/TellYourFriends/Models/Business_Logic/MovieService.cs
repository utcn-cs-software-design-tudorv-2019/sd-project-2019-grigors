using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TellYourFriends.Models.Business_Logic.Interfaces;
using TellYourFriends.Models.Data_Access.Repository.Interfaces;

namespace TellYourFriends.Models.Business_Logic
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
        public Movie AddMovie(Movie movie)
        {
            return _movieRepository.AddMovie(movie);
        }

        public bool DeleteMovie(int id)
        {
            return _movieRepository.DeleteMovie(id);
        }

        public Movie EditMovie(Movie movie)
        {
            return _movieRepository.EditMovie(movie);
        }

        public IQueryable<Movie> GetAllMovies()
        {
            return _movieRepository.GetAllMovies();
        }

        public IQueryable<Movie> GetDashboardMovies(int id)
        {
            return _movieRepository.GetDashboardMovies( id);
        }

        public Movie GetMovie(int id)
        {
            return _movieRepository.GetMovie(id);
        }

        public IQueryable<Movie> GetMyMovies(int id)
        {
            return _movieRepository.GetMyMovies(id);
        }
    }
}