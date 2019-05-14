using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TellYourFriends.Models.Entity;

namespace TellYourFriends.Models.Data_Access.Repository.Interfaces
{
    interface IBookRepository
    {
        IQueryable<Book> GetAllEventBooks();
        Book GetBook(int id);
        Book CreateBook(Book book);
        Book EditBook(Book book);
        bool DeleteBook(int id);

    }
}
