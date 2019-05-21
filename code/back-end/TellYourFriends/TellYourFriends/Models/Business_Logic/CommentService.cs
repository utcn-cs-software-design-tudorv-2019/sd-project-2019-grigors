using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TellYourFriends.Models.Business_Logic.Interfaces;
using TellYourFriends.Models.Data_Access.Repository.Interfaces;
using TellYourFriends.Models.Entity;

namespace TellYourFriends.Models.Business_Logic
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public Comment AddComment(Comment comment)
        {
            return _commentRepository.AddComment(comment);
        }

        public bool DeleteComment(int id)
        {
            return _commentRepository.DeleteComment(id);
        }

        public Comment EditComment(Comment comment)
        {
            return _commentRepository.EditComment(comment);
        }

        public IQueryable<Comment> GetAllComments()
        {
            return _commentRepository.GetAllComments();
        }

        public Comment GetComment(int id)
        {
            return _commentRepository.GetComment(id);
        }
    }
}