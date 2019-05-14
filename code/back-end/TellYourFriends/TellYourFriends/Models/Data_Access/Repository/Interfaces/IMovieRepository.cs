using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TellYourFriends.Models.Data_Access.Repository.Interfaces
{
    public interface IMovieRepository
    {
        IQueryable<Movie> GetAllEventMovies();
        Movie GetMovie(int id);
        Movie AddMovie(Movie movie);
        Movie EditMovie(Movie movie);
        bool DeleteMovie(int id);
        
    }
}
