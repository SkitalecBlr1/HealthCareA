using HealthCare.Entities;

namespace HealthCare.Specifications.Implementation
{
    public class PatientByFamilySpec : Specification<Patient>
    {
        public PatientByFamilySpec(string family)
            : base(x => x.Family.Contains(family))
        {
        }
    }
}
