using MedicalClinicServer.Context;
using MedicalClinicServer.Interfaces;
using MedicalClinicServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalClinicServer.DataRequests
{
    public class ClientData : IClient
    {
        private ClinicContext _clinicContext;
        public ClientData(ClinicContext clinicContext)
        {
            _clinicContext = clinicContext;
        }

        public async Task<Client> AddClientAsync(Client client)
        {
            client.Id = Guid.NewGuid();
            await _clinicContext.Clients.AddAsync(client);
            await _clinicContext.SaveChangesAsync();
            return client;
        }

        public async Task DeleteClientAsync(Client client)
        {
            _clinicContext.Clients.Remove(client);
            await _clinicContext.SaveChangesAsync();
        }

        public async Task<Client> EditClientAsync(Client client)
        {
            var existingClient = await _clinicContext.Clients.FindAsync(client.Id);

            if (existingClient != null)
            {
                existingClient.Login = client.Login;
                existingClient.Password = client.Password;
                existingClient.Name = client.Name;
                existingClient.MiddleName = client.MiddleName;
                existingClient.Surname = client.Surname;
                existingClient.Email = client.Email;
                existingClient.TelephoneNumber = client.TelephoneNumber;
                existingClient.BirthDate = client.BirthDate;

                _clinicContext.Clients.Update(existingClient);
                await _clinicContext.SaveChangesAsync();
            }
            return client;
        }

        public async Task<Client> GetClientAsync(Guid id)
        {
            var client = await _clinicContext.Clients.FindAsync(id);
            return client;
        }

        public List<Client> GetClients()
        {
            return _clinicContext.Clients.ToList();
        }
    }
}
