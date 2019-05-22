using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TellYourFriends.Models.Entity;

namespace TellYourFriends.Models.Data_Access.Repository.Interfaces
{
    public interface IBookRepository
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
