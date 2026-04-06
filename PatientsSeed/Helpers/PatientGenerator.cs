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
                
                .RuleFor(x => x.Use, f => f.Random.Number(100000, 999999).ToString())

                
                .RuleFor(x => x.Family, f => f.Name.LastName())

                
                .RuleFor(x => x.GivenName, f => f.Name.FirstName())

                
                .RuleFor(x => x.Gender, f => f.PickRandom("Male", "Female"))
                .RuleFor(x => x.MiddleName, (f, p) =>
                {
                    if (p.Gender == "Male")
                        return $"{f.Name.FirstName()}ovich";
                    else
                        return $"{f.Name.FirstName()}ovna";
                })

                
                .RuleFor(x => x.BirthDate, f =>
                    f.Date.Between(DateTime.UtcNow.AddYears(-20), DateTime.UtcNow.AddYears(-1)));

            return patientFaker.Generate(count);
        }
    }
}
