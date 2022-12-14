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
        public int Id { get; set; }
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
        public int AnamnesId { get; set; }
        public Anamnes Anamnes { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        [Required]
        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}
