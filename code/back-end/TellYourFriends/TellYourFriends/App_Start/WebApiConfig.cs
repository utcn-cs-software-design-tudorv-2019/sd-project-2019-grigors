using System;
using System.Collections.Generic;
using System.Linq;
using Unity;
using System.Web.Http;
using TellYourFriends.Models.Data_Access.Repository;
using TellYourFriends.Models.Data_Access.Repository.Interfaces;
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

            config.DependencyResolver = new UnityResolver(container);
        }
    }
}
