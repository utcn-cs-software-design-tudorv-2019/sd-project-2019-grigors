using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TellYourFriends.Utility
{
    public class ChangePasswordModel
    {
        public int Id { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}