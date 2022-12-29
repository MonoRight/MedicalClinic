using MedicalClinicServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalClinicServer.Interfaces
{
    public interface IComment
    {
        List<Comment> GetComments();
        Task<Comment> GetCommentAsync(Guid id);
        Task<Comment> AddCommentAsync(Comment comment);
        Task DeleteCommentAsync(Comment comment);
        Task<Comment> EditCommentAsync(Comment comment);
    }
}
