namespace PatientsSeed.DTOs
{
    public class PatientCreateDto
    {

        public string Use { get; set; }
        public string Family { get; set; }
        public string GivenName { get; set; }
        public string MiddleName { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
