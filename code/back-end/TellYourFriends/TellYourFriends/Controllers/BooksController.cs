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
    public class BooksController : ApiController
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService BookService)
        {
            _bookService = BookService;
        }

        [HttpGet]
        public IHttpActionResult GetBooks()
        {
            var books = _bookService.GetAllBooks();
            if (books != null) return Ok(books);
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound));
        }

        [HttpGet]
        public IHttpActionResult GetBook(int id)
        {
            var foundedBook = _bookService.GetBook(id);

            if (foundedBook != null) return Ok(foundedBook);

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, Constants.DataNotFound));
        }

        [HttpPost]
        public IHttpActionResult CreateBook(Book book)
        {
            if (book == null || !ModelState.IsValid)
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, Constants.InvalidData));

            book.UserId = SecurityService.Instance.GetUserByToken(SecurityService.Instance.GetTokenFromHeader(Request.Headers.GetValues(Constants.MyAuthorizationHeader))).Id;

            var addedBook = _bookService.AddBook(book);

            if (addedBook != null) return Ok(addedBook);

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, Constants.ActionNotRegistered));
        }

        [HttpPut]
        public IHttpActionResult EditBook(Book book)
        {
            if (book == null || !ModelState.IsValid)
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, Constants.InvalidData));

            var updatedBook = _bookService.EditBook(book);

            if (updatedBook != null) return Ok(updatedBook);

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, Constants.ActionNotRegistered));
        }

        [Route("api/books/dashboard")]
        public IHttpActionResult GetDashboardEvents()
        {
            if (!SecurityService.Instance.IsAuthorised(Request.Headers.GetValues(Constants.MyAuthorizationHeader)))
            {
                return Unauthorized();
            }

            var id = SecurityService.Instance.GetUserByToken(SecurityService.Instance.GetTokenFromHeader(Request.Headers.GetValues(Constants.MyAuthorizationHeader))).Id;

            var events = _bookService.GetDashboardBooks(id);
            if (events != null)
            {
                return Ok(events);
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, Constants.DataNotFound));
        }

        [Route("api/books/myBooks")]
        public IHttpActionResult GetMyBooks()
        {
            if (!SecurityService.Instance.IsAuthorised(Request.Headers.GetValues(Constants.MyAuthorizationHeader)))
            {
                return Unauthorized();
            }

            var id = SecurityService.Instance.GetUserByToken(SecurityService.Instance.GetTokenFromHeader(Request.Headers.GetValues(Constants.MyAuthorizationHeader))).Id;

            var events = _bookService.GetMyBooks(id);
            if (events != null)
            {
                return Ok(events);
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, Constants.DataNotFound));
        }
    }
}
