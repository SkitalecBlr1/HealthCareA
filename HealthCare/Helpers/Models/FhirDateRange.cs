namespace HealthCare.Helpers.Models
{
    public class FhirDateRange
    {
        public string Operator { get; set; } = "eq";
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
