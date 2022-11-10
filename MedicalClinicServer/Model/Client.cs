using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalClinicServer.Model
{
    public class Client
    {
        [Key]
        public Guid Id { get; set; }
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
        [MaxLength(50, ErrorMessage = "Name can only be 50 characters long")]
        public string MiddleName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string TelephoneNumber { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
