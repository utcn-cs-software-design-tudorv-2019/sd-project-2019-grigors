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

        public Comment AddComment(Comment comment)
        {
            try
            {
                _context.Comments.Add(comment);
                _context.SaveChanges();
                return comment;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public bool DeleteComment(int id)
        {
            try
            {
                var foundedComment = _context.Comments.FirstOrDefault(x => x.Id == id);
                if (foundedComment == null) return false;

                _context.Comments.Remove(foundedComment);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public Comment EditComment(Comment comment)
        {
            try
            {
                var commentToUpdate = _context.Comments.FirstOrDefault(x => x.Id == comment.Id);
                if (commentToUpdate == null) return null;

                if (comment.Title != null && comment.Title != "")
                {
                    commentToUpdate.Title = comment.Title;
                }

                if (comment.Body != null && comment.Body != "")
                {
                    commentToUpdate.Body = comment.Body;
                }

                if (comment.Date != null)
                {
                    commentToUpdate.Date = comment.Date;
                }

                if (comment.Likes != 0)
                {
                    commentToUpdate.Likes = comment.Likes;
                }

                _context.SaveChanges();

                return comment;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public IQueryable<Comment> GetAllComments()
        {
            try
            {
                return _context.Comments;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public Comment GetComment(int id)
        {
            try
            {
                return _context.Comments.SingleOrDefault(u => u.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}