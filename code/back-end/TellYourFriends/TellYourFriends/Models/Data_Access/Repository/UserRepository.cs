using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            throw new NotImplementedException();
        }

        public User ChangePassword(ChangePasswordModel changePassword)
        {
            throw new NotImplementedException();
        }

        public User GetUser(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<User> GetUsers()
        {
            throw new NotImplementedException();
        }

        public User UpdateUser(User user)
        {
            throw new NotImplementedException();
        }

        public bool UserExists(int id)
        {
            throw new NotImplementedException();
        }
    }
}