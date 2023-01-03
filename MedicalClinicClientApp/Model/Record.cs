using System;
using System.Collections.Generic;
using System.Text;

namespace MedicalClinicClientApp.Model
{
    public class Record
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; } = null;
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; } = null;
    }
}
