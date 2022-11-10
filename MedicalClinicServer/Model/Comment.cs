using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalClinicServer.Model
{
    public class Comment
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        [Range(1,5, ErrorMessage = "Mark can be from 1 to 5")]
        public int Mark { get; set; }
        [Required]
        public Client Client { get; set; }
        [Required]
        public Doctor Doctor { get; set; }
    }
}
