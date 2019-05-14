using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TellYourFriends.Models.Data_Access.Repository.Interfaces;
using TellYourFriends.Models.Entity;

namespace TellYourFriends.Models.Data_Access.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext _context;

        public CommentRepository(AppDbContext context)
        {
            _context = context;
        }

        public Comment CreateComment(Comment comment)
        {
            throw new NotImplementedException();
        }

        public bool DeleteComment(int id)
        {
            throw new NotImplementedException();
        }

        public Comment EditComment(Comment comment)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Comment> GetAllEventComments()
        {
            throw new NotImplementedException();
        }

        public Comment GetComment(int id)
        {
            throw new NotImplementedException();
        }
    }
}