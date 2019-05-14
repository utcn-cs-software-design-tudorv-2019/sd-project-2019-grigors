using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TellYourFriends.Models.Data_Access.Repository.Interfaces;
using TellYourFriends.Models.Entity;

namespace TellYourFriends.Models.Data_Access.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public Category CreateCategory(Category category)
        {
            throw new NotImplementedException();
        }

        public bool DeleteCategory(int id)
        {
            throw new NotImplementedException();
        }

        public Category EditCategory(Category category)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Category> GetAllEventCategorys()
        {
            throw new NotImplementedException();
        }

        public Category GetCategory(int id)
        {
            throw new NotImplementedException();
        }
    }
}