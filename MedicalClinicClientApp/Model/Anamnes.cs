using System;
using System.Collections.Generic;
using System.Text;

namespace MedicalClinicClientApp.Model
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
