using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TellYourFriends.Models.Entity
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public DateTime Date { get; set; }

        public int Likes { get; set; }

        public Book Book { get; set; }
        
        public Movie Movie { get; set; }
    }
}