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

        public Book CreateBook(Book book)
        {
            throw new NotImplementedException();
        }

        public bool DeleteBook(int id)
        {
            throw new NotImplementedException();
        }

        public Book EditBook(Book book)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Book> GetAllEventBooks()
        {
            throw new NotImplementedException();
        }

        public Book GetBook(int id)
        {
            throw new NotImplementedException();
        }
    }
}