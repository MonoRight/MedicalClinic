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
    public class AnamnesesController : ControllerBase
    {
        private IAnamnes _anamnesData;

        public AnamnesesController(IAnamnes anamnesData)
        {
            _anamnesData = anamnesData;
        }

        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult GetAnamneses()
        {
            return Ok(_anamnesData.GetAnamneses());
        }

        [HttpGet]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> GetAnamnesAsync(int id)
        {
            var anamnes = await _anamnesData.GetAnamnesAsync(id);

            if (anamnes != null)
            {
                return Ok(_anamnesData.GetAnamnesAsync(id));
            }

            return NotFound($"Anamnes with id: {id} was not found");
        }

        [HttpPost]
        [Route("api/[controller]")]
        public async Task<IActionResult> AddAnamnesAsync(Anamnes anamnes)
        {
            if (anamnes == null)
            {
                return BadRequest();
            }
            await _anamnesData.AddAnamnesAsync(anamnes);
            return Created(HttpContext.Request.Scheme = "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + anamnes.Id, anamnes);
        }

        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> DeleteAnamnesAsync(int id)
        {
            var anamnes = await _anamnesData.GetAnamnesAsync(id);

            if (anamnes != null)
            {
                await _anamnesData.DeleteAnamesAsync(anamnes);
                return Ok();
            }

            return NotFound($"Anamnes with Id: {id} was not found");
        }

        [HttpPatch]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> EditAnamnesAsync(int id, Anamnes anamnes)
        {
            var existingAnamnes = await _anamnesData.GetAnamnesAsync(id);

            if (existingAnamnes != null)
            {
                anamnes.Id = existingAnamnes.Id;
                await _anamnesData.EditAnamnesAsync(anamnes);
            }
            return Ok(anamnes);
        }
    }
}
