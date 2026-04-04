using FluentValidation;
using HealthCare.DTOs;

namespace HealthCare.Validation
{
    public class CreatePatientValidator : AbstractValidator<CreatePatientDto>
    {
        public CreatePatientValidator()
        {
            RuleFor(x => x.Family).NotEmpty().MaximumLength(100);
            RuleFor(x => x.GivenName).NotEmpty();

            RuleFor(x => x.Gender)
                .Must(x => x == "male" || x == "female");

            RuleFor(x => x.BirthDate)
                .LessThan(DateTime.UtcNow);
        }
    }
}
