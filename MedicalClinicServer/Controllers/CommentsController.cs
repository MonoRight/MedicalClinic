using MedicalClinicServer.Interfaces;
using MedicalClinicServer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalClinicServer.Controllers
{
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private IComment _commentData;

        public CommentsController(IComment commentData)
        {
            _commentData = commentData;
        }

        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult GetComments()
        {
            return Ok(_commentData.GetComments());
        }

        [HttpGet]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> GetCommentAsync(int id)
        {
            var comment = await _commentData.GetCommentAsync(id);

            if (comment != null)
            {
                return Ok(_commentData.GetCommentAsync(id));
            }

            return NotFound($"Comment with id: {id} was not found");
        }

        [HttpPost]
        [Route("api/[controller]")]
        public async Task<IActionResult> AddCommentAsync(Comment comment)
        {
            if (comment == null)
            {
                return BadRequest();
            }
            await _commentData.AddCommentAsync(comment);
            return Created(HttpContext.Request.Scheme = "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + comment.Id, comment);
        }

        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> DeleteCommentAsync(int id)
        {
            var comment = await _commentData.GetCommentAsync(id);

            if (comment != null)
            {
                await _commentData.DeleteCommentAsync(comment);
                return Ok();
            }

            return NotFound($"Comment with Id: {id} was not found");
        }

        [HttpPatch]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> EditCommentAsync(int id, Comment comment)
        {
            var existingComment = await _commentData.GetCommentAsync(id);

            if (existingComment != null)
            {
                comment.Id = existingComment.Id;
                await _commentData.EditCommentAsync(comment);
            }
            return Ok(comment);
        }
    }
}
