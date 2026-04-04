namespace HealthCare.DTOs
{
    public class PatientDto
    {
        public Guid Id { get; set; }
        public string Use { get; set; }
        public string Family { get; set; }
        public string GivenName { get; set; }
        public string MiddleName { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
    }

    public class CreatePatientDto
    {
        public string Use { get; set; }
        public string Family { get; set; }
        public string GivenName { get; set; }
        public string MiddleName { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
    }

    public class UpdatePatientDto : CreatePatientDto
    {
    }
}
