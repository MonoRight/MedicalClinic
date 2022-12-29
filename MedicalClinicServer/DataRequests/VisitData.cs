using MedicalClinicServer.Context;
using MedicalClinicServer.Interfaces;
using MedicalClinicServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalClinicServer.DataRequests
{
    public class VisitData : IVisit
    {
        private ClinicContext _clinicContext;
        public VisitData(ClinicContext clinicContext)
        {
            _clinicContext = clinicContext;
        }

        public async Task<Visit> AddVisitAsync(Visit visit)
        {
            visit.Id = Guid.NewGuid();
            await _clinicContext.Visits.AddAsync(visit);
            await _clinicContext.SaveChangesAsync();
            return visit;
        }

        public async Task DeleteVisitAsync(Visit visit)
        {
            _clinicContext.Visits.Remove(visit);
            await _clinicContext.SaveChangesAsync();
        }

        public async Task<Visit> EditVisitAsync(Visit visit)
        {
            var existingVisit = await _clinicContext.Visits.FindAsync(visit.Id);

            if (existingVisit != null)
            {
                existingVisit.Analysis = visit.Analysis;
                existingVisit.Anamnes = visit.Anamnes;
                existingVisit.Client = visit.Client;
                existingVisit.Complaint = visit.Complaint;
                existingVisit.Date = visit.Date;
                existingVisit.Doctor = visit.Doctor;
                existingVisit.Illness = visit.Illness;
                existingVisit.Medicines = visit.Medicines;
                existingVisit.Price = visit.Price;
                existingVisit.Recomendations = visit.Recomendations;

                _clinicContext.Visits.Update(existingVisit);
                await _clinicContext.SaveChangesAsync();
            }
            return visit;
        }

        public Task<Visit> GetVisitAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Visit> GetVisits()
        {
            throw new NotImplementedException();
        }
    }
}
