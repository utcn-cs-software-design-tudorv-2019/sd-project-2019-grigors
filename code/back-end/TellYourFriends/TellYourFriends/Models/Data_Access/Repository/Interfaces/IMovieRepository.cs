using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TellYourFriends.Models.Data_Access.Repository.Interfaces
{
    interface IMovieRepository
    {
        IQueryable<Movie> GetAllEventMovies();
        Movie GetMovie(int id);
        Movie CreateMovie(Movie movie);
        Movie EditBook(Movie movie);
        bool DeleteMovie(int id);
        
    }
}
