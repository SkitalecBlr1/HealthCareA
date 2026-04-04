using HealthCare.DTOs;
using HealthCare.Entities;
using HealthCare.Specifications.Extensions;
using HealthCare.Specifications.Implementation;

namespace HealthCare.Specifications
{
    public static class PatientSpecificationBuilder
    {
        public static ISpecification<Patient> Build(PatientQuery query)
        {
            ISpecification<Patient> spec = new Specification<Patient>(x => true);

            if (!string.IsNullOrEmpty(query.Family))
                spec = spec.And(new PatientByFamilySpec(query.Family));

            if (!string.IsNullOrEmpty(query.Gender))
                spec = spec.And(new PatientByGenderSpec(query.Gender));

            
            if (query.BirthDate != null && query.BirthDate.Any())
            {
                foreach (var birthDate in query.BirthDate)
                {
                    if (!string.IsNullOrWhiteSpace(birthDate))
                    {
                        spec = spec.And(new PatientByBirthDateSpec(birthDate));
                    }
                }
            }

            return spec;
        }
    }
}
