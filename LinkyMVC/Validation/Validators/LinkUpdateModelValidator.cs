﻿using FluentValidation;
using LinkyMVC.Models.InputModels;

namespace LinkyMVC.Validation.Validators
{
    public class LinkUpdateModelValidator : AbstractValidator<LinkUpdateModel>
    {
        public LinkUpdateModelValidator()
        {
            RuleFor(l => l.Label).NotEmpty().MaximumLength(50);

            RuleFor(l => l.URL).NotEmpty().MaximumLength(256)
                .Must(CustomRules.ValidURL).WithMessage("URL must be valid");
        }
    }
}