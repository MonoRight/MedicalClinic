using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalClinicServer.Model
{
    public class Anamnes
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Localisation { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public string DevelopmentRate { get; set; }
        [Required]
        public string PainRate { get; set; }
    }
}
