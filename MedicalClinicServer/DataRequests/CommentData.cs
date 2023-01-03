using MedicalClinicServer.Context;
using MedicalClinicServer.Interfaces;
using MedicalClinicServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalClinicServer.DataRequests
{
    public class CommentData : IComment
    {

        private ClinicContext _clinicContext;
        public CommentData(ClinicContext clinicContext)
        {
            _clinicContext = clinicContext;
        }

        public async Task<Comment> AddCommentAsync(Comment comment)
        {
            //comment.Id = Guid.NewGuid();
            await _clinicContext.Comments.AddAsync(comment);
            await _clinicContext.SaveChangesAsync();
            return comment;
        }

        public async Task DeleteCommentAsync(Comment comment)
        {
            _clinicContext.Comments.Remove(comment);
            await _clinicContext.SaveChangesAsync();
        }

        public async Task<Comment> EditCommentAsync(Comment comment)
        {
            var existingComment = await _clinicContext.Comments.FindAsync(comment.Id);

            if (existingComment != null)
            {
                existingComment.ClientId = comment.ClientId;
                existingComment.DoctorId = comment.DoctorId;
                existingComment.Mark = comment.Mark;
                existingComment.Text = comment.Text;

                _clinicContext.Comments.Update(existingComment);
                await _clinicContext.SaveChangesAsync();
            }
            return comment;
        }

        public async Task<Comment> GetCommentAsync(int id)
        {
            var comment = await _clinicContext.Comments.FindAsync(id);
            return comment;
        }

        public List<Comment> GetComments()
        {
            return _clinicContext.Comments.ToList();
        }
    }
}
