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
            if (book.Categories == null)
            {
                return null;
            }
            List<Category> categories = new List<Category>();
            foreach (Category c in book.Categories)
            {
                Category category = _context.Categories.FirstOrDefault(x => x.Id == c.Id);
                categories.Add(category);
            }
            book.Categories = categories;
            
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

                if (book.Categories != null)
                {
                    bookToUpdate.Categories = book.Categories;
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

        public IQueryable<Book> GetAllBooks()
        {
            try
            {
                return _context.Books.Include("User").Include("Comments").Include("Categories");
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
                return _context.Books.Include("User").Include("Categories").Include("Comments").SingleOrDefault(u => u.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public IQueryable<Book> GetDashboardBooks(int id)
        {
            try
            {
                List<Book> books = _context.Books.Include("Categories").Include("Comments").Where(e => e.UserId != id).ToList<Book>();

                return books.AsQueryable<Book>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public IQueryable<Book> GetMyBooks(int id)
        {
            try
            {
                List<Book> books = _context.Books.Include("Categories").Where(e => e.UserId == id).ToList<Book>();

                return books.AsQueryable<Book>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}