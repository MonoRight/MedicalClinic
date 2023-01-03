using System;
using System.Collections.Generic;
using System.Text;

namespace MedicalClinicClientApp.Model
{
    public class Visit
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Illness { get; set; }
        public string Complaint { get; set; }
        public string Analysis { get; set; }
        public string Medicines { get; set; }
        public string Recomendations { get; set; }
        public int AnamnesId { get; set; }
        public Anamnes Anamnes { get; set; } = null;
        public double Price { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; } = null;
        public int ClientId { get; set; }
        public Client Client { get; set; } = null;
    }
}
