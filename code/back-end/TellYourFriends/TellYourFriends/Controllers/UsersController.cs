using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TellYourFriends.Models;
using TellYourFriends.Models.Data_Access.Repository.Interfaces;
using TellYourFriends.Utility;

namespace TellYourFriends.Controllers
{
    public class UsersController : ApiController
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository UserRepository)
        {
            _userRepository = UserRepository;
        }

        [HttpGet]
        public IHttpActionResult GetUsers()
        {
            var users = _userRepository.GetUsers();
            if (User != null) return Ok(users);
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound));
        }

        [HttpGet]
        public IHttpActionResult GetUser(int id)
        {
            var foundedUser = _userRepository.GetUser(id);

            if (foundedUser != null) return Ok(foundedUser);

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, Constants.DataNotFound));
        }

        [HttpPost]
        public IHttpActionResult CreateUser(User user)
        {
            if (user == null || !ModelState.IsValid)
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, Constants.InvalidData));

            var addedUser = _userRepository.AddUser(user);

            if (addedUser != null) return Ok(addedUser);

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, Constants.ActionNotRegistered));
        }

        [HttpPut]
        public IHttpActionResult EditUser(User user)
        {
            if (user == null || !ModelState.IsValid)
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, Constants.InvalidData));

            var updatedUser = _userRepository.UpdateUser(user);

            if (updatedUser != null) return Ok(updatedUser);

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, Constants.ActionNotRegistered));
        }
    }
}
