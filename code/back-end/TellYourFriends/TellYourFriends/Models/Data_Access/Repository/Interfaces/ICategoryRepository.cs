using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TellYourFriends.Models.Entity;

namespace TellYourFriends.Models.Data_Access.Repository.Interfaces
{
    public interface ICategoryRepository
    {
        IQueryable<Category> GetAllCategories();
        Category GetCategory(int id);
        Category AddCategory(Category category);
        Category EditCategory(Category category);
        bool DeleteCategory(int id);
    }
}
