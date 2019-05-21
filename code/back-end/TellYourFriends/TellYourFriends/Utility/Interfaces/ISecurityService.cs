using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TellYourFriends.Models;

namespace TellYourFriends.Utility.Interfaces
{
    public interface ISecurityService
    {
        User GetUserByToken(Guid token);
        Guid OnLogin(User employee);
        bool OnLogout(Guid token);
        bool IsAuthorised(IEnumerable<string> headerValues);
        Guid GetTokenFromHeader(IEnumerable<string> headerValues);
    }
}
