using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TellYourFriends.Models.Data_Access.Repository.Interfaces;

namespace TellYourFriends.Models.Data_Access.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly AppDbContext _context;

        public MovieRepository(AppDbContext context)
        {
            _context = context;
        }

        public Movie CreateMovie(Movie movie)
        {
            throw new NotImplementedException();
        }

        public bool DeleteMovie(int id)
        {
            throw new NotImplementedException();
        }

        public Movie EditBook(Movie movie)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Movie> GetAllEventMovies()
        {
            throw new NotImplementedException();
        }

        public Movie GetMovie(int id)
        {
            throw new NotImplementedException();
        }
    }
}