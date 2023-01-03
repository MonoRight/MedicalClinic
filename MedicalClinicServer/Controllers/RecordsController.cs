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
    public class RecordsController : ControllerBase
    {
        private IRecord _recordData;

        public RecordsController(IRecord recordData)
        {
            _recordData = recordData;
        }

        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult GetRecords()
        {
            return Ok(_recordData.GetRecords());
        }

        [HttpGet]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> GetRecordAsync(int id)
        {
            var record = await _recordData.GetRecordAsync(id);

            if (record != null)
            {
                return Ok(_recordData.GetRecordAsync(id));
            }

            return NotFound($"Record with id: {id} was not found");
        }

        [HttpPost]
        [Route("api/[controller]")]
        public async Task<IActionResult> AddRecordAsync(Record record)
        {
            if (record == null)
            {
                return BadRequest();
            }

            await _recordData.AddRecordAsync(record);
            return Created(HttpContext.Request.Scheme = "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + record.Id, record);
        }

        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> DeleteRecordAsync(int id)
        {
            var record = await _recordData.GetRecordAsync(id);

            if (record != null)
            {
                await _recordData.DeleteRecordAsync(record);
                return Ok();
            }

            return NotFound($"Record with Id: {id} was not found");
        }

        [HttpPatch]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> EditRecordAsync(int id, Record record)
        {
            var existingRecord = await _recordData.GetRecordAsync(id);

            if (existingRecord != null)
            {
                record.Id = existingRecord.Id;
                await _recordData.EditRecordAsync(record);
            }
            return Ok(record);
        }
    }
}
