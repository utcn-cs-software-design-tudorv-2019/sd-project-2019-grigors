using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TellYourFriends.Utility
{
    public class Constants
    {
        public static string MyAuthorizationHeader = "MyAuthorization";
        public const string DataNotFound = "Data not found!";
        public const string InvalidData = "Invalid data!";
        public const string InvalidCredentials = "Invalid credentials!";
        public const string ActionNotRegistered = "Your action could not be fulfilled!";
        public const string WrongOldPassword = "Your old password doesn-t match the account's password";
        public const string DataAlreadyInUse =
            "Your data is alredy in use, update the events or employees which use it and you will be able to delete this";

    }
}