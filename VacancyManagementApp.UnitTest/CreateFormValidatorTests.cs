using FluentValidation.TestHelper;
using NUnit.Framework;
using VacancyManagementApp.Application.DTOs.ApplicationForm;
using VacancyManagementApp.Application.Validators;

namespace VacancyManagementApp.Tests.Validators
{
    public class CreateFormValidatorTests
    {
        private CreateFormValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new CreateFormValidator();
        }

        [Test]
        public void Should_Have_Error_When_Name_Is_Empty()
        {
            var model = new CreateApplicationFormDto { Name = "" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Test]
        public void Should_Have_Error_When_Name_Contains_Invalid_Characters()
        {
            var model = new CreateApplicationFormDto { Name = "1234" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Test]
        public void Should_Not_Have_Error_When_Name_Is_Valid()
        {
            var model = new CreateApplicationFormDto { Name = "John" };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.Name);
        }

        [Test]
        public void Should_Have_Error_When_Email_Is_Invalid()
        {
            var model = new CreateApplicationFormDto { Email = "invalid-email" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Test]
        public void Should_Not_Have_Error_When_Email_Is_Valid()
        {
            var model = new CreateApplicationFormDto { Email = "test@example.com" };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.Email);
        }

        [Test]
        public void Should_Have_Error_When_PhoneNumber_Is_Invalid()
        {
            var model = new CreateApplicationFormDto { PhoneNumber = "123456" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.PhoneNumber);
        }

        [Test]
        public void Should_Not_Have_Error_When_PhoneNumber_Is_Valid()
        {
            var model = new CreateApplicationFormDto { PhoneNumber = "050 123 45 67" };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.PhoneNumber);
        }
    }
}
