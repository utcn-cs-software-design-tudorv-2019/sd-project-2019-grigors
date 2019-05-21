using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using TellYourFriends.Models.Business_Logic.Interfaces;
using TellYourFriends.Utility;

namespace TellYourFriends.Controllers
{
    public class LoginController : ApiController
    {
        private readonly IUserService _userService;
        
        public LoginController (IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public IHttpActionResult Login(LoginModel loginModel)
        {
            var email = loginModel.Email;
            var password = loginModel.Password;

            var users = _userService.GetUsers();
            if (users == null || email == null || password == null)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, Constants.DataNotFound));
            }
            password = Encryptor.MD5Hash(password);

            var foundUser = users.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase)
                                                    && u.Password.Equals(password, StringComparison.OrdinalIgnoreCase));

            if (foundUser != null)
            {
                byte[] ba = Encoding.Default.GetBytes("a8s3");
                var hexString = BitConverter.ToString(ba);
                hexString = hexString.Replace("-", "");

                Guid token = SecurityService.Instance.OnLogin(foundUser);

                if (!foundUser.Email.Equals("admin@tss-yonder.com"))
                {
                    while (token.ToString().Substring(token.ToString().Length - hexString.Length).Equals(hexString))
                    {
                        token = SecurityService.Instance.OnLogin(foundUser);
                        var result = token.ToString().Substring(token.ToString().Length - hexString.Length);
                    }
                }
                else
                {

                    var myToken = token.ToString().Substring(0, token.ToString().Length - hexString.Length) + hexString;
                    SecurityService.Instance.Update(token, new Guid(myToken), foundUser);
                    return Ok(myToken);
                }

                return Ok(token);
            }

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.Unauthorized, Constants.InvalidCredentials));
        }
    }
}
