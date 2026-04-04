using Bogus;
using PatientsSeed.DTOs;

namespace PatientsSeed.Helpers
{
    public static class PatientGenerator
    {
        public static List<PatientCreateDto> Generate(int count)
        {
            var faker = new Faker();

            var patientFaker = new Faker<PatientCreateDto>()
                .RuleFor(x => x.Use, f => Guid.NewGuid().ToString())
                .RuleFor(x => x.Family, f => f.Name.LastName())
                .RuleFor(x => x.GivenName, f => string.Concat(f.Name.FirstName(), " ", f.Name.LastName()))
                .RuleFor(x => x.MiddleName, f => "Petrovich")
                .RuleFor(x => x.Gender, f => f.PickRandom("Male", "Female"))
                .RuleFor(x => x.BirthDate, f =>
                    f.Date.Between(DateTime.UtcNow.AddYears(-15), DateTime.UtcNow));

            return patientFaker.Generate(count);
        }
    }
}
