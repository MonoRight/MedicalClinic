using MedicalClinicServer.Context;
using MedicalClinicServer.Interfaces;
using MedicalClinicServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalClinicServer.DataRequests
{
    public class AnamnesData : IAnamnes
    {
        private ClinicContext _clinicContext;
        public AnamnesData(ClinicContext clinicContext)
        {
            _clinicContext = clinicContext;
        }

        public async Task<Anamnes> AddAnamnesAsync(Anamnes anamnes)
        {
            anamnes.Id = Guid.NewGuid();
            await _clinicContext.Anamneses.AddAsync(anamnes);
            await _clinicContext.SaveChangesAsync();
            return anamnes;
        }

        public async Task DeleteAnamesAsync(Anamnes anamnes)
        {
            _clinicContext.Anamneses.Remove(anamnes);
            await _clinicContext.SaveChangesAsync();
        }

        public async Task<Anamnes> EditAnamnesAsync(Anamnes anamnes)
        {
            var existingAnamnes = await _clinicContext.Anamneses.FindAsync(anamnes.Id);

            if (existingAnamnes != null)
            {
                existingAnamnes.DevelopmentRate = anamnes.DevelopmentRate;
                existingAnamnes.Localisation = anamnes.Localisation;
                existingAnamnes.PainRate = anamnes.PainRate;
                existingAnamnes.StartDate = anamnes.StartDate;

                _clinicContext.Anamneses.Update(existingAnamnes);
                await _clinicContext.SaveChangesAsync();
            }
            return anamnes;
        }

        public async Task<Anamnes> GetAnamnesAsync(Guid id)
        {
            var anamnes = await _clinicContext.Anamneses.FindAsync(id);
            return anamnes;
        }

        public List<Anamnes> GetAnamneses()
        {
            return _clinicContext.Anamneses.ToList();
        }
    }
}
