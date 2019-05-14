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

        public Category AddCategory(Category category)
        {
            try
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                return category;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public bool DeleteCategory(int id)
        {
            try
            {
                var foundedCategory = _context.Categories.FirstOrDefault(x => x.Id == id);
                if (foundedCategory == null) return false;

                _context.Categories.Remove(foundedCategory);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public Category EditCategory(Category category)
        {
            try
            {
                var categoryToUpdate = _context.Categories.FirstOrDefault(x => x.Id == category.Id);
                if (categoryToUpdate == null) return null;

                if (category.Description != null && category.Description != "")
                {
                    categoryToUpdate.Description = category.Description;
                }

                if (category.Name != null && category.Name != "")
                {
                    categoryToUpdate.Name = category.Name;
                }

                if (category.Books != null)
                {
                    categoryToUpdate.Books = category.Books;
                }

                if (category.Movies != null)
                {
                    categoryToUpdate.Movies = category.Movies;
                }

                _context.SaveChanges();

                return category;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public IQueryable<Category> GetAllCategories()
        {
            try
            {
                return _context.Categories;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public Category GetCategory(int id)
        {
            try
            {
                return _context.Categories.SingleOrDefault(u => u.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}