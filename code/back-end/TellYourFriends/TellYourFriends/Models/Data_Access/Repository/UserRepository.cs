using System;
using System.Linq;
using TellYourFriends.Models.Data_Access.Repository.Interfaces;
using TellYourFriends.Utility;

namespace TellYourFriends.Models.Data_Access.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public User AddUser(User user)
        {
            try
            {
                user.Password = Encryptor.MD5Hash(user.Password);
                _context.Users.Add(user);
                _context.SaveChanges();
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public User ChangePassword(ChangePasswordModel changePassword)
        {
            try
            {
                changePassword.NewPassword = Encryptor.MD5Hash(changePassword.NewPassword);
                changePassword.OldPassword = Encryptor.MD5Hash(changePassword.OldPassword);
                var employee = _context.Users.FirstOrDefault(x => x.Id == changePassword.Id && x.Password == changePassword.OldPassword);
                if (employee == null) return null;

                employee.Password = changePassword.NewPassword;

                _context.SaveChanges();

                return employee;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public User GetUser(int id)
        {
            try
            {
                return _context.Users.Include("Books").Include("Movies").SingleOrDefault(u => u.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public IQueryable<User> GetUsers()
        {
            try
            {
                return _context.Users.Include("Book").Include("Movie");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public User UpdateUser(User user)
        {
            try
            {
                var userToUpdate = _context.Users.FirstOrDefault(x => x.Id == user.Id);
                if (userToUpdate == null) return null;

                if (user.Email != null && user.Email != "")
                {
                    userToUpdate.Email = user.Email;
                }

                if (user.Name != null && user.Name != "")
                {
                    userToUpdate.Name = user.Name;
                }

                if (user.Password != null && user.Password != "")
                {
                    userToUpdate.Password = Encryptor.MD5Hash(user.Password);
                }

                if (user.Image != null && user.Image != "")
                {
                    userToUpdate.Image = user.Image;
                }

                if (user.Books != null)
                {
                    userToUpdate.Books = user.Books;
                }

                if (user.Movies != null)
                {
                    userToUpdate.Movies = user.Movies;
                }

                _context.SaveChanges();

                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public bool UserExists(int id)
        {
            return _context.Users.Count(u => u.Id == id) > 0;
        }
    }
}