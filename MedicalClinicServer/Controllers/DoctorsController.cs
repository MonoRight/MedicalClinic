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
    public class DoctorsController : ControllerBase
    {
        private IDoctor _doctorData;

        public DoctorsController(IDoctor doctorData)
        {
            _doctorData = doctorData;
        }

        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult GetDoctors()
        {
            return Ok(_doctorData.GetDoctors());
        }

        [HttpGet]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> GetDoctorAsync(int id)
        {
            var doctor = await _doctorData.GetDoctorAsync(id);

            if (doctor != null)
            {
                return Ok(_doctorData.GetDoctorAsync(id));
            }

            return NotFound($"Doctor with id: {id} was not found");
        }

        [HttpPost]
        [Route("api/[controller]")]
        public async Task<IActionResult> AddDoctorAsync(Doctor doctor)
        {
            if (doctor == null)
            {
                return BadRequest();
            }

            if (_doctorData.GetDoctors().Where(x => x.Login == doctor.Login).Count() > 0)
            {
                return Forbid();
            }

            await _doctorData.AddDoctorAsync(doctor);
            return Created(HttpContext.Request.Scheme = "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + doctor.Id, doctor);
        }

        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> DeleteDoctorAsync(int id)
        {
            var doctor = await _doctorData.GetDoctorAsync(id);

            if (doctor != null)
            {
                await _doctorData.DeleteDoctorAsync(doctor);
                return Ok();
            }

            return NotFound($"Doctor with Id: {id} was not found");
        }

        [HttpPatch]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> EditDoctorAsync(int id, Doctor doctor)
        {
            var existingDoctor = await _doctorData.GetDoctorAsync(id);

            if (existingDoctor != null)
            {
                doctor.Id = existingDoctor.Id;
                await _doctorData.EditDoctorAsync(doctor);
            }
            return Ok(doctor);
        }
    }
}
