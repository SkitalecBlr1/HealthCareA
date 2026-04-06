using FluentValidation;
using HealthCare.DTOs;

namespace HealthCare.Validation
{
    public class CreatePatientValidator : AbstractValidator<CreatePatientDto>
    {
        public CreatePatientValidator()
        {
            RuleFor(x => x.Family)
                .NotEmpty()
                .WithMessage("Family не должна быть пустой")
                .MaximumLength(100)
                .WithMessage("Family не должна превышать 100 символов");

            RuleFor(x => x.GivenName)
                .NotEmpty()
                .WithMessage("GivenName (имя) не должно быть пустым");

            RuleFor(x => x.Gender)
                .NotEmpty()
                .WithMessage("Gender не должен быть пустым")
                .Must(x => x == "male" || x == "female")
                .WithMessage("Gender должен быть 'male' или 'female'");

            RuleFor(x => x.BirthDate)
                .LessThan(DateTime.UtcNow)
                .WithMessage("BirthDate должен быть в прошлом");
        }
    }
}