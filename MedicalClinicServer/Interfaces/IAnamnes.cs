using MedicalClinicServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalClinicServer.Interfaces
{
    public interface IAnamnes
    {
        List<Anamnes> GetAnamneses();
        Task<Anamnes> GetAnamnesAsync(Guid id);
        Task<Anamnes> AddAnamnesAsync(Anamnes anamnes);
        Task DeleteAnamesAsync(Anamnes anamnes);
        Task<Anamnes> EditAnamnesAsync(Anamnes anamnes);
    }
}
