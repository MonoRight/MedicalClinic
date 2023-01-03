using MedicalClinicServer.Context;
using MedicalClinicServer.Interfaces;
using MedicalClinicServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalClinicServer.DataRequests
{
    public class DoctorData : IDoctor
    {
        private ClinicContext _clinicContext;
        public DoctorData(ClinicContext clinicContext)
        {
            _clinicContext = clinicContext;
        }

        public async Task<Doctor> AddDoctorAsync(Doctor doctor)
        {
            //doctor.Id = Guid.NewGuid();
            await _clinicContext.Doctors.AddAsync(doctor);
            await _clinicContext.SaveChangesAsync();
            return doctor;
        }

        public async Task DeleteDoctorAsync(Doctor doctor)
        {
            _clinicContext.Doctors.Remove(doctor);
            await _clinicContext.SaveChangesAsync();
        }

        public async Task<Doctor> EditDoctorAsync(Doctor doctor)
        {
            var existingDoctor = await _clinicContext.Doctors.FindAsync(doctor.Id);

            if (existingDoctor != null)
            {
                existingDoctor.Login = doctor.Login;
                existingDoctor.Password = doctor.Password;
                existingDoctor.Name = doctor.Name;
                existingDoctor.MiddleName = doctor.MiddleName;
                existingDoctor.Surname = doctor.Surname;
                existingDoctor.WorkDays = doctor.WorkDays;
                existingDoctor.StartWorkHour = doctor.StartWorkHour;
                existingDoctor.EndWorkHour = doctor.EndWorkHour;
                existingDoctor.WorkPosition = doctor.WorkPosition;

                _clinicContext.Doctors.Update(existingDoctor);
                await _clinicContext.SaveChangesAsync();
            }
            return doctor;
        }

        public List<Doctor> GetDoctors()
        {
            return _clinicContext.Doctors.ToList();
        }

        public async Task<Doctor> GetDoctorAsync(int id)
        {
            var doctor = await _clinicContext.Doctors.FindAsync(id);
            return doctor;
        }
    }
}
