namespace HealthCare.Swagger
{
    using HealthCare.DTOs;
    using Swashbuckle.AspNetCore.Filters;

    public class PatientQueryExample : IExamplesProvider<PatientQuery>
    {
        public PatientQuery GetExamples()
        {
            return new PatientQuery
            {
                BirthDate = new List<string>
            {
                "ge1990",
                "lt2000"
            },
                Family = "Ivanov",
                Gender = "male",
                Page = 1,
                PageSize = 10
            };
        }
    }
}
