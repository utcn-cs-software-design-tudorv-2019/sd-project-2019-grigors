using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TellYourFriends.Models.Business_Logic.Interfaces;
using TellYourFriends.Models.Data_Access.Repository.Interfaces;
using TellYourFriends.Models.Entity;

namespace TellYourFriends.Models.Business_Logic
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public Book AddBook(Book book)
        {
            return _bookRepository.AddBook(book);
        }

        public bool DeleteBook(int id)
        {
            return _bookRepository.DeleteBook(id);
        }

        public Book EditBook(Book book)
        {
            return _bookRepository.EditBook(book);
        }

        public IQueryable<Book> GetAllBooks()
        {
            return _bookRepository.GetAllBooks();
        }

        public Book GetBook(int id)
        {
            return _bookRepository.GetBook(id);
        }
    }
}