using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TellYourFriends.Models.Entity;

namespace TellYourFriends.Models.Business_Logic.Interfaces
{
    public interface ICategoryService
    {
        IQueryable<Category> GetAllCategories();
        Category GetCategory(int id);
        Category AddCategory(Category category);
        Category EditCategory(Category category);
        bool DeleteCategory(int id);
    }
}
