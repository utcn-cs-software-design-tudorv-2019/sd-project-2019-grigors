using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using TellYourFriends.Models.Entity;

namespace TellYourFriends.Models.Data_Access
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("AppDbContext")
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Category> Categories { get; set; }

    }
}