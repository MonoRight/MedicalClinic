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
    public class ClientsController : ControllerBase
    {
        private IClient _clientData;

        public ClientsController(IClient clientData)
        {
            _clientData = clientData;
        }

        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult GetClients()
        {
            return Ok(_clientData.GetClients());
        }

        [HttpGet]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> GetClientAsync(int id)
        {
            var client = await _clientData.GetClientAsync(id);

            if (client != null)
            {
                return Ok(_clientData.GetClientAsync(id));
            }

            return NotFound($"Client with id: {id} was not found");
        }

        [HttpPost]
        [Route("api/[controller]")]
        public async Task<IActionResult> AddClientAsync(Client client)
        {
            if (client == null)
            {
                return BadRequest();
            }

            if (_clientData.GetClients().Where(x => x.Login == client.Login).Count() > 0)
            {
                return Forbid();
            }
            
            await _clientData.AddClientAsync(client);
            return Created(HttpContext.Request.Scheme = "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + client.Id, client);
        }

        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> DeleteClientAsync(int id)
        {
            var client = await _clientData.GetClientAsync(id);

            if (client != null)
            {
                await _clientData.DeleteClientAsync(client);
                return Ok();
            }

            return NotFound($"Client with Id: {id} was not found");
        }

        [HttpPatch]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> EditEmployeeAsync(int id, Client client)
        {
            var existingClient = await _clientData.GetClientAsync(id);

            if (existingClient != null)
            {
                client.Id = existingClient.Id;
                await _clientData.EditClientAsync(client);
            }
            return Ok(client);
        }
    }
}
