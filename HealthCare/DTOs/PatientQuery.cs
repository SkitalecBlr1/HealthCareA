using Swashbuckle.AspNetCore.Annotations;

namespace HealthCare.DTOs
{
    public class PatientQuery
    {
        public string? Family { get; set; }
        public string? Gender { get; set; }
        
        [SwaggerSchema(Description = "FHIR поиск. Пример: ge1990, lt2000. Может быть комбинированым: birthDate=ge1990&birthDate=lt2000")]
        public List<string>? BirthDate { get; set; }

        [SwaggerSchema(Description = "Номер страницы")]
        public int Page { get; set; } = 1;

        [SwaggerSchema(Description = "Результатов на страницу")]
        public int PageSize { get; set; } = 10;
    }
}
