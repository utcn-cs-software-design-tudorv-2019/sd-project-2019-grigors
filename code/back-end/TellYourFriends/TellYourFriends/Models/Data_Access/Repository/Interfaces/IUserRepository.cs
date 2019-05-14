using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TellYourFriends.Models.Entity;
using TellYourFriends.Utility;

namespace TellYourFriends.Models.Data_Access.Repository.Interfaces
{
    public interface IUserRepository
    {
        User GetUser(int id);
        IQueryable<User> GetUsers();
        User UpdateUser(User user);
        User ChangePassword(ChangePasswordModel changePassword);
        User AddUser(User user);
        bool UserExists(int id);
    }
}
