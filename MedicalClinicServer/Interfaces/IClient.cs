using MedicalClinicServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalClinicServer.Interfaces
{
    public interface IClient
    {
        List<Client> GetClients();
        Task<Client> GetClientAsync(Guid id);
        Task<Client> AddClientAsync(Client client);
        Task DeleteClientAsync(Client client);
        Task<Client> EditClientAsync(Client client);
    }
}
