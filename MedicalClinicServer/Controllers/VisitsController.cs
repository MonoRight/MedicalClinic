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
    public class VisitsController : ControllerBase
    {
        private IVisit _visitData;

        public VisitsController(IVisit visitData)
        {
            _visitData = visitData;
        }

        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult GetVisits()
        {
            return Ok(_visitData.GetVisits());
        }

        [HttpGet]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> GetVisitAsync(int id)
        {
            var visit = await _visitData.GetVisitAsync(id);

            if (visit != null)
            {
                return Ok(_visitData.GetVisitAsync(id));
            }

            return NotFound($"Visit with id: {id} was not found");
        }

        [HttpPost]
        [Route("api/[controller]")]
        public async Task<IActionResult> AddVisitAsync(Visit visit)
        {
            if (visit == null)
            {
                return BadRequest();
            }
            await _visitData.AddVisitAsync(visit);
            return Created(HttpContext.Request.Scheme = "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + visit.Id, visit);
        }

        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> DeleteVisitAsync(int id)
        {
            var visit = await _visitData.GetVisitAsync(id);

            if (visit != null)
            {
                await _visitData.DeleteVisitAsync(visit);
                return Ok();
            }

            return NotFound($"Visit with Id: {id} was not found");
        }

        [HttpPatch]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> EditCommentAsync(int id, Visit visit)
        {
            var existingVisit = await _visitData.GetVisitAsync(id);

            if (existingVisit != null)
            {
                visit.Id = existingVisit.Id;
                await _visitData.EditVisitAsync(visit);
            }
            return Ok(visit);
        }
    }
}
