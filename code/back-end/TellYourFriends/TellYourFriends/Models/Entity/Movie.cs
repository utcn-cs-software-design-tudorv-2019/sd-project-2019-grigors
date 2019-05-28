using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        public string Author { get; set; }

        public string Description { get; set; }

        public string Edition { get; set; }

        public string Image { get; set; }

        public double Rating { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public User User { get; set; }

        public List<Category> Categories { get; set; }

        public List<Comment> Comments { get; set; }

    }
}