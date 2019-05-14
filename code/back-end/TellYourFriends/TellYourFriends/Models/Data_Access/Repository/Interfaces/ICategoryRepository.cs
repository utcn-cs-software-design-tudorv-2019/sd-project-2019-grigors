using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TellYourFriends.Models.Entity;

namespace TellYourFriends.Models.Data_Access.Repository.Interfaces
{
    interface ICategoryRepository
    {
        IQueryable<Category> GetAllEventCategorys();
        Category GetCategory(int id);
        Category CreateCategory(Category category);
        Category EditCategory(Category category);
        bool DeleteCategory(int id);
    }
}
