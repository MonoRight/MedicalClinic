namespace MedicalClinicEmployeeClient.Model
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string MiddleName { get; set; }
        public string WorkDays { get; set; }
        public int StartWorkHour { get; set; }
        public int EndWorkHour { get; set; }
        public string WorkPosition { get; set; }
    }
}
