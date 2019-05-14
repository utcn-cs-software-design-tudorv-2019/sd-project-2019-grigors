﻿using System;
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

        public Movie AddMovie(Movie movie)
        {
            try
            {                
                _context.Movies.Add(movie);
                _context.SaveChanges();
                return movie;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public bool DeleteMovie(int id)
        {
            try
            {
                var foundedMovie = _context.Movies.FirstOrDefault(x => x.Id == id);
                if (foundedMovie == null) return false;

                _context.Movies.Remove(foundedMovie);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public Movie EditMovie(Movie movie)
        {
            try
            {
                var movieToUpdate = _context.Movies.FirstOrDefault(x => x.Id == movie.Id);
                if (movieToUpdate == null) return null;

                if (movie.Description != null && movie.Description != "")
                {
                    movieToUpdate.Description = movie.Description;
                }

                if(movie.Director!=null && movie.Director != "")
                {
                    movieToUpdate.Director = movie.Director;
                }

                if (movie.Name != null && movie.Name != "")
                {
                    movieToUpdate.Name = movie.Name;
                }

                if (movie.Year != 0)
                {
                    movieToUpdate.Year = movie.Year;
                }

                if (movie.Rating != 0.0)
                {
                    movieToUpdate.Rating = movie.Rating;
                }

                if (movie.Image != null && movie.Image != "")
                {
                    movieToUpdate.Image = movie.Image;
                }

                if (movie.Category != null)
                {
                    movieToUpdate.Category = movie.Category;
                }

                if (movie.Comments != null)
                {
                    movieToUpdate.Comments = movie.Comments;
                }

                _context.SaveChanges();

                return movie;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            }

        public IQueryable<Movie> GetAllEventMovies()
        {
            try
            {
                return _context.Movies.Include("Comments").Include("Category");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public Movie GetMovie(int id)
        {
            try
            {
                return _context.Movies.Include("Category").Include("Comments").SingleOrDefault(u => u.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}