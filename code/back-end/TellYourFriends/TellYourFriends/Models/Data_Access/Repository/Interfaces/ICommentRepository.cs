using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TellYourFriends.Models.Entity;

namespace TellYourFriends.Models.Data_Access.Repository.Interfaces
{
    interface ICommentRepository
    {
        IQueryable<Comment> GetAllEventComments();
        Comment GetComment(int id);
        Comment CreateComment(Comment comment);
        Comment EditComment(Comment comment);
        bool DeleteComment(int id);
    }
}
