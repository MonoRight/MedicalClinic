using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalClinicServer.Model
{
    public class Doctor
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Name can only be 50 characters long")]
        public string Name { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Surname can only be 50 characters long")]
        public string Surname { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Middlename can only be 50 characters long")]
        public string MiddleName { get; set; }
        [Required]
        public string WorkDays { get; set; }
        [Required]
        public int StartWorkHour { get; set; }
        [Required]
        public int EndWorkHour { get; set; }
        [Required]
        public string WorkPosition { get; set; }
    }
}
