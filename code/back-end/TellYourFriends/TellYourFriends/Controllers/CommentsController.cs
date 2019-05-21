using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TellYourFriends.Models.Business_Logic.Interfaces;
using TellYourFriends.Models.Entity;
using TellYourFriends.Utility;

namespace TellYourFriends.Controllers
{
    public class CommentsController : ApiController
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService CommentService)
        {
            _commentService = CommentService;
        }

        [HttpGet]
        public IHttpActionResult GetComments()
        {
            var comments = _commentService.GetAllComments();
            if (comments != null) return Ok(comments);
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound));
        }

        [HttpGet]
        public IHttpActionResult GetComment(int id)
        {
            var foundedComment = _commentService.GetComment(id);

            if (foundedComment != null) return Ok(foundedComment);

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, Constants.DataNotFound));
        }

        [HttpPost]
        public IHttpActionResult CreateComment(Comment comment)
        {
            if (comment == null || !ModelState.IsValid)
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, Constants.InvalidData));

            var addedComment = _commentService.AddComment(comment);

            if (addedComment != null) return Ok(addedComment);

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, Constants.ActionNotRegistered));
        }

        [HttpPut]
        public IHttpActionResult EditComment(Comment comment)
        {
            if (comment == null || !ModelState.IsValid)
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, Constants.InvalidData));

            var updatedComment = _commentService.EditComment(comment);

            if (updatedComment != null) return Ok(updatedComment);

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, Constants.ActionNotRegistered));
        }
    }
}
