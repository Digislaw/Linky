using FluentValidation;
using LinkyMVC.Models.InputModels;
using LinkyMVC.Validation.Validators;
using System;
using System.Collections.Generic;

namespace LinkyMVC.Validation
{
    public class ValidatorFactory : ValidatorFactoryBase
    {
        private static Dictionary<Type, IValidator> validators = new Dictionary<Type, IValidator>();

        public ValidatorFactory()
        {
            validators.Add(typeof(IValidator<LinkInputModel>), new LinkInputModelValidator());
            validators.Add(typeof(IValidator<LinkUpdateModel>), new LinkUpdateModelValidator());
        }

        public override IValidator CreateInstance(Type validatorType)
        {
            validators.TryGetValue(validatorType, out IValidator validator);
            return validator;
        }
    }
}