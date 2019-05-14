using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TellYourFriends.Models.Data_Access.Repository.Interfaces;
using TellYourFriends.Models.Entity;

namespace TellYourFriends.Models.Data_Access.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context)
        {
            _context = context;
        }

        public Book AddBook(Book book)
        {
            try
            {
                _context.Books.Add(book);
                _context.SaveChanges();
                return book;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public bool DeleteBook(int id)
        {
            try
            {
                var foundedBook = _context.Books.FirstOrDefault(x => x.Id == id);
                if (foundedBook == null) return false;

                _context.Books.Remove(foundedBook);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public Book EditBook(Book book)
        {
            try
            {
                var bookToUpdate = _context.Books.FirstOrDefault(x => x.Id == book.Id);
                if (bookToUpdate == null) return null;

                if (book.Description != null && book.Description != "")
                {
                    bookToUpdate.Description = book.Description;
                }

                if (book.Name != null && book.Name != "")
                {
                    bookToUpdate.Name = book.Name;
                }

                if (book.Rating != 0.0)
                {
                    bookToUpdate.Rating = book.Rating;
                }

                if (book.Image != null && book.Image != "")
                {
                    bookToUpdate.Image = book.Image;
                }

                if (book.Category != null)
                {
                    bookToUpdate.Category = book.Category;
                }

                if (book.Comments != null)
                {
                    bookToUpdate.Comments = book.Comments;
                }

                _context.SaveChanges();

                return book;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public IQueryable<Book> GetAllEventBooks()
        {
            try
            {
                return _context.Books.Include("Comments").Include("Category");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public Book GetBook(int id)
        {
            try
            {
                return _context.Books.Include("Category").Include("Comments").SingleOrDefault(u => u.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}