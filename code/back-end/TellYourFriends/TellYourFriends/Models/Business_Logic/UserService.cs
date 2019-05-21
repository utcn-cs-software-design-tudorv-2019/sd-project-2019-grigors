using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TellYourFriends.Models.Business_Logic.Interfaces;
using TellYourFriends.Models.Data_Access.Repository.Interfaces;
using TellYourFriends.Utility;

namespace TellYourFriends.Models.Business_Logic
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public User AddUser(User user)
        {
            return _userRepository.AddUser(user);
        }

        public User ChangePassword(ChangePasswordModel changePassword)
        {
            return _userRepository.ChangePassword(changePassword);
        }

        public User GetUser(int id)
        {
            return _userRepository.GetUser(id);
        }

        public IQueryable<User> GetUsers()
        {
            return _userRepository.GetUsers();
        }

        public User UpdateUser(User user)
        {
            return _userRepository.UpdateUser(user);
        }

        public bool UserExists(int id)
        {
            return _userRepository.UserExists(id);
        }
    }
}