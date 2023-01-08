using System;

namespace MedicalClinicEmployeeClient.Model
{
    public class Anamnes
    {
        public int Id { get; set; }
        public string Localisation { get; set; }
        public DateTime StartDate { get; set; }
        public string DevelopmentRate { get; set; }
        public string PainRate { get; set; }
    }
}
