using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TellYourFriends.Models.Entity
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }

        public string Description { get; set; }

        public string Edition { get; set; }

        public string Image { get; set; }

        public double Rating { get; set; }

        public List<Category> Category { get; set; }

        public List<Comment> Comments { get; set; }
    }
}