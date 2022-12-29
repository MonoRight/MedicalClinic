using MedicalClinicServer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalClinicServer.Context
{
    public class ClinicContext :DbContext
    {
        public ClinicContext(DbContextOptions<ClinicContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Anamnes> Anamneses { get; set; }
        public DbSet<Record> Records { get; set; }
        public DbSet<Visit> Visits { get; set; }
    }
}
