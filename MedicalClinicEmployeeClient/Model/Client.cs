using System;

namespace MedicalClinicEmployeeClient.Model
{
    public class Client
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        public string TelephoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
