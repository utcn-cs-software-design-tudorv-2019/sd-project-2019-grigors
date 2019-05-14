using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TellYourFriends.Models.Entity;

namespace TellYourFriends.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Image { get; set; }

        public List<Movie> Movies { get; set; }

        public List<Book> Books { get; set; }
    }
}