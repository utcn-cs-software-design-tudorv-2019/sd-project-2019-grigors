using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TellYourFriends.Models.Entity;

namespace TellYourFriends.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Year { get; set; }

        public string Director { get; set; }

        public string Image { get; set; }

        public double Rating { get; set; }

        public List<Category> Category { get; set; }

        public List<Comment> Comments { get; set; }

    }
}