using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TellYourFriends.Models.Entity;

namespace TellYourFriends.Models.Business_Logic.Interfaces
{
    public interface ICommentService
    {
        IQueryable<Comment> GetAllComments();
        Comment GetComment(int id);
        Comment AddComment(Comment comment);
        Comment EditComment(Comment comment);
        bool DeleteComment(int id);
    }
}
