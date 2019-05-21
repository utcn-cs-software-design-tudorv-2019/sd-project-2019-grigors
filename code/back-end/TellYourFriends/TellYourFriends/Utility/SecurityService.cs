using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TellYourFriends.Models;
using TellYourFriends.Utility.Interfaces;

namespace TellYourFriends.Utility
{
    public class SecurityService : ISecurityService
    {
        private Dictionary<Guid, User> tokenDictionary = new Dictionary<Guid, User>();

        private static SecurityService instance = null;
        private static readonly object padlock = new object();

        SecurityService()
        {
        }

        public static SecurityService Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new SecurityService();
                    }
                    return instance;
                }
            }
        }

        public Guid GetTokenFromHeader(IEnumerable<string> headerValues)
        {
            var tokenStr = headerValues.FirstOrDefault();
            var tokenString = new string((from c in tokenStr
                                          where char.IsWhiteSpace(c) || char.IsLetterOrDigit(c) || c.Equals("-")
                                          select c
            ).ToArray());
            return new Guid(tokenString);
        }

        public User GetUserByToken(Guid token)
        {
            User user;
            if (tokenDictionary.TryGetValue(token, out user) == false)
            {
                return null;
            }
            return user;
        }

        public bool IsAuthorised(IEnumerable<string> headerValues)
        {
            if (SecurityService.Instance.GetUserByToken(GetTokenFromHeader(headerValues)) == null)
            {
                return false;
            }
            return true;
        }

        public Guid OnLogin(User employee)
        {
            Guid token = Guid.NewGuid();
            tokenDictionary.Add(token, employee);
            return token;
        }

        public bool OnLogout(Guid token)
        {
            return tokenDictionary.Remove(token);
        }

        public void Update(Guid oldToken, Guid newToken, User user)
        {
            tokenDictionary.Remove(oldToken);
            tokenDictionary.Add(newToken, user);
        }
    }
}