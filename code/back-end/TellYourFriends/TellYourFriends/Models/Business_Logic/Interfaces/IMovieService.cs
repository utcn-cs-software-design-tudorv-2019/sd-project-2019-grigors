using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TellYourFriends.Models.Business_Logic.Interfaces
{
    public interface IMovieService
    {
        IQueryable<Movie> GetAllMovies();
        Movie GetMovie(int id);
        Movie AddMovie(Movie movie);
        Movie EditMovie(Movie movie);
        bool DeleteMovie(int id);
        IQueryable<Movie> GetDashboardMovies(int id);
        IQueryable<Movie> GetMyMovies(int id);
    }
}
