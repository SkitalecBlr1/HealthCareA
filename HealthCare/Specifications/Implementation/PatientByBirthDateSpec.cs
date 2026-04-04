using HealthCare.Entities;
using HealthCare.Helpers;
using System.Linq.Expressions;

namespace HealthCare.Specifications.Implementation
{
    public class PatientByBirthDateSpec : Specification<Patient>
    {
        public PatientByBirthDateSpec(string input)
            : base(BuildExpression(input))
        {
        }

        private static Expression<Func<Patient, bool>> BuildExpression(string input)
        {
            var parsed = FhirDateParser.Parse(input);

            if (parsed == null)
                return x => true;

            var op = parsed.Operator;
            var from = parsed.From;
            var to = parsed.To;

            return op switch
            {
                "eq" => x => x.BirthDate >= from && x.BirthDate < to,
                "ne" => x => x.BirthDate < from || x.BirthDate >= to,

                "gt" => x => x.BirthDate >= to,
                "ge" => x => x.BirthDate >= from,

                "lt" => x => x.BirthDate < from,
                "le" => x => x.BirthDate < to,

                _ => x => true
            };
        }
    }
}
