using MedicalClinicServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalClinicServer.Interfaces
{
    public interface IDoctor
    {
        List<Doctor> GetDoctors();
        Task<Doctor> GetDoctorAsync(int id);
        Task<Doctor> AddDoctorAsync(Doctor doctor);
        Task DeleteDoctorAsync(Doctor doctor);
        Task<Doctor> EditDoctorAsync(Doctor doctor);
    }
}
