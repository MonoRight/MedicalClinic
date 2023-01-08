namespace MedicalClinicEmployeeClient.Model
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Mark { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; } = null;
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; } = null;
    }
}
