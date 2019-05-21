using System;
using System.Collections.Generic;
using System.Linq;
using Unity;
using System.Web.Http;
using TellYourFriends.Models.Data_Access.Repository;
using TellYourFriends.Models.Data_Access.Repository.Interfaces;
using TellYourFriends.Models.Business_Logic;
using TellYourFriends.Models.Business_Logic.Interfaces;
using TellYourFriends.Models.Data_Access;

namespace TellYourFriends
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //Dependency injection with unity container
            var container = new UnityContainer();

            if (!container.IsRegistered<IUserRepository>())
                container.RegisterType<IUserRepository, UserRepository>();
            if (!container.IsRegistered<ICommentRepository>())
                container.RegisterType<ICommentRepository, CommentRepository>();
            if (!container.IsRegistered<ICategoryRepository>())
                container.RegisterType<ICategoryRepository, CategoryRepository>();
            if (!container.IsRegistered<IBookRepository>())
                container.RegisterType<IBookRepository, BookRepository>();
            if (!container.IsRegistered<IMovieRepository>())
                container.RegisterType<IMovieRepository, MovieRepository>();

            if (!container.IsRegistered<IUserService>())
                container.RegisterType<IUserService, UserService>();
            if (!container.IsRegistered<ICommentService>())
                container.RegisterType<ICommentService, CommentService>();
            if (!container.IsRegistered<ICategoryService>())
                container.RegisterType<ICategoryService, CategoryService>();
            if (!container.IsRegistered<IBookService>())
                container.RegisterType<IBookService, BookService>();
            if (!container.IsRegistered<IMovieService>())
                container.RegisterType<IMovieService, MovieService>();

            config.DependencyResolver = new UnityResolver(container);
        }
    }
}
