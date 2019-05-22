using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TellYourFriends.Models;
using TellYourFriends.Models.Business_Logic.Interfaces;
using TellYourFriends.Utility;

namespace TellYourFriends.Controllers
{
    public class MoviesController : ApiController
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService MovieService)
        {
            _movieService = MovieService;
        }

        [HttpGet]
        public IHttpActionResult GetMovies()
        {
            var movies = _movieService.GetAllMovies();
            if (movies != null) return Ok(movies);
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound));
        }

        [HttpGet]
        public IHttpActionResult GetMovie(int id)
        {
            var foundedMovie = _movieService.GetMovie(id);

            if (foundedMovie != null) return Ok(foundedMovie);

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, Constants.DataNotFound));
        }

        [HttpPost]
        public IHttpActionResult CreateMovie(Movie movie)
        {
            if (movie == null || !ModelState.IsValid)
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, Constants.InvalidData));

            movie.UserId = SecurityService.Instance.GetUserByToken(SecurityService.Instance.GetTokenFromHeader(Request.Headers.GetValues(Constants.MyAuthorizationHeader))).Id;

            var addedMovie = _movieService.AddMovie(movie);

            if (addedMovie != null) return Ok(addedMovie);

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, Constants.ActionNotRegistered));
        }

        [HttpPut]
        public IHttpActionResult EditMovie(Movie movie)
        {
            if (movie == null || !ModelState.IsValid)
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, Constants.InvalidData));

            var updatedMovie = _movieService.EditMovie(movie);

            if (updatedMovie != null) return Ok(updatedMovie);

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, Constants.ActionNotRegistered));
        }
    }
}
