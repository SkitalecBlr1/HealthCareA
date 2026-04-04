using HealthCare.Entities;

namespace HealthCare.Specifications.Implementation
{
    public class PatientByGenderSpec : Specification<Patient>
    {
        public PatientByGenderSpec(string gender)
            : base(x => x.Gender == gender)
        {
        }
    }
}
