using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TellYourFriends.Models.Entity;

namespace TellYourFriends.Models.Business_Logic.Interfaces
{
    public interface IBookService
    {
        IQueryable<Book> GetAllBooks();
        Book GetBook(int id);
        Book AddBook(Book book);
        Book EditBook(Book book);
        bool DeleteBook(int id);
        IQueryable<Book> GetDashboardBooks(int id);
        IQueryable<Book> GetMyBooks(int id);
    }
}
