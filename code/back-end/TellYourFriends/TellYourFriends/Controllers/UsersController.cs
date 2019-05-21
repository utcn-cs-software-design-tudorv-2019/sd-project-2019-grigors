using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TellYourFriends.Models;
using TellYourFriends.Models.Business_Logic.Interfaces;
using TellYourFriends.Utility;

namespace TellYourFriends.Controllers
{
    public class UsersController : ApiController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService UserService)
        {
            _userService = UserService;
        }

        [HttpGet]
        public IHttpActionResult GetUsers()
        {
            var users = _userService.GetUsers();
            if (User != null) return Ok(users);
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound));
        }

        [HttpGet]
        public IHttpActionResult GetUser(int id)
        {
            var foundedUser = _userService.GetUser(id);

            if (foundedUser != null) return Ok(foundedUser);

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, Constants.DataNotFound));
        }

        [HttpPost]
        public IHttpActionResult CreateUser(User user)
        {
            if (user == null || !ModelState.IsValid)
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, Constants.InvalidData));

            var addedUser = _userService.AddUser(user);

            if (addedUser != null) return Ok(addedUser);

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, Constants.ActionNotRegistered));
        }

        [HttpPut]
        public IHttpActionResult EditUser(User user)
        {
            if (user == null || !ModelState.IsValid)
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, Constants.InvalidData));

            var updatedUser = _userService.UpdateUser(user);

            if (updatedUser != null) return Ok(updatedUser);

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, Constants.ActionNotRegistered));
        }

        [Route("api/users/loggedUser")]
        [HttpGet]
        public IHttpActionResult GetLoggedUser()
        {
            if (!SecurityService.Instance.IsAuthorised(Request.Headers.GetValues(Constants.MyAuthorizationHeader)))
            {
                return Unauthorized();
            }
            var id = SecurityService.Instance.GetUserByToken(SecurityService.Instance.GetTokenFromHeader(Request.Headers.GetValues(Constants.MyAuthorizationHeader))).Id;
            var employee = _userService.GetUser(id);
            if (employee != null)
            {
                return Ok(employee);
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, Constants.DataNotFound));
        }

        [Route("api/users/changePassword")]
        [HttpPut]
        public IHttpActionResult ChangePassword(ChangePasswordModel changePassword)
        {
            if (!SecurityService.Instance.IsAuthorised(Request.Headers.GetValues(Constants.MyAuthorizationHeader)))
            {
                return Unauthorized();
            }
            changePassword.Id = SecurityService.Instance.GetUserByToken(SecurityService.Instance.GetTokenFromHeader(Request.Headers.GetValues(Constants.MyAuthorizationHeader))).Id;

            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            if (changePassword == null) { return BadRequest(); }

            var updatedEmployee = _userService.ChangePassword(changePassword);

            if (updatedEmployee != null)
            {
                return Ok(updatedEmployee);
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.Unauthorized, Constants.WrongOldPassword));
        }
    }
}
