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
    public class CategoriesController : ApiController
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService CategoryService)
        {
            _categoryService = CategoryService;
        }

        [HttpGet]
        public IHttpActionResult GetCategorys()
        {
            var categories = _categoryService.GetAllCategories();
            if (categories != null) return Ok(categories);
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound));
        }

        [HttpGet]
        public IHttpActionResult GetCategory(int id)
        {
            var foundedCategory = _categoryService.GetCategory(id);

            if (foundedCategory != null) return Ok(foundedCategory);

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, Constants.DataNotFound));
        }

        [HttpPost]
        public IHttpActionResult CreateCategory(Category category)
        {
            if (category == null || !ModelState.IsValid)
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, Constants.InvalidData));

            var addedCategory = _categoryService.AddCategory(category);

            if (addedCategory != null) return Ok(addedCategory);

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, Constants.ActionNotRegistered));
        }

        [HttpPut]
        public IHttpActionResult EditCategory(Category category)
        {
            if (category == null || !ModelState.IsValid)
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, Constants.InvalidData));

            var updatedCategory = _categoryService.EditCategory(category);

            if (updatedCategory != null) return Ok(updatedCategory);

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, Constants.ActionNotRegistered));
        }
    }
}
