using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalClinicServer.Model
{
    public class Visit
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Illness { get; set; }
        [Required]
        public string Complaint { get; set; }
        public string Analysis { get; set; }
        [Required]
        public string Medicines { get; set; }
        [Required]
        public string Recomendations { get; set; }
        [Required]
        public Anamnes Anamnes { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public Doctor Doctor { get; set; }
        [Required]
        public Client Client { get; set; }
    }
}
