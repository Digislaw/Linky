using FluentValidation;
using LinkyMVC.Models.InputModels;

namespace LinkyMVC.Validation.Validators
{
    public class LinkInputModelValidator : AbstractValidator<LinkInputModel>
    {
        public LinkInputModelValidator()
        {
            RuleFor(l => l.Label).NotEmpty().MaximumLength(50);
            RuleFor(l => l.Label).Matches(@"^[a-zA-Z0-9]+$")
                .WithMessage("Label should contain letters and digits only");

            RuleFor(l => l.URL).NotEmpty().MaximumLength(256)
                .Must(CustomRules.ValidURL).WithMessage("URL must be valid");
        }
    }
}