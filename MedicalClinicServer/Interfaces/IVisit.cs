using MedicalClinicServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalClinicServer.Interfaces
{
    public interface IVisit
    {
        List<Visit> GetVisits();
        Task<Visit> GetVisitAsync(int id);
        Task<Visit> AddVisitAsync(Visit visit);
        Task DeleteVisitAsync(Visit visit);
        Task<Visit> EditVisitAsync(Visit visit);
    }
}
