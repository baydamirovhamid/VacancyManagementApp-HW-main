using Moq;
using NUnit.Framework;
using VacancyManagementApp.Application.DTOs.ApplicationForm;
using VacancyManagementApp.Application.Validators;
using FluentValidation;
using System.Threading.Tasks;

namespace VacancyManagementApp.UnitTest.Validators
{
    [TestFixture]
    public class CreateFormValidatorTests
    {
        private CreateFormValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new CreateFormValidator();
        }

        [Test]
        public async Task CreateFormValidator_ShouldPass_WhenAllFieldsAreValid()
        {
            // Arrange
            var dto = new CreateApplicationFormDto
            {
                Name = "John",
                Surname = "Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "050 123 45 67"
            };

            // Act
            var result = await _validator.ValidateAsync(dto);

            // Assert
            Assert.That(result.IsValid, Is.True, "All fields are valid so validation should pass.");
        }

        [Test]
        public async Task CreateFormValidator_ShouldFail_WhenNameIsEmpty()
        {
            var dto = new CreateApplicationFormDto
            {
                Name = "",
                Surname = "Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "050 123 45 67"
            };

            var result = await _validator.ValidateAsync(dto);

            Assert.That(result.IsValid, Is.False);
            Assert.That(result.Errors, Has.Some.Matches<FluentValidation.Results.ValidationFailure>(e => e.PropertyName == "Name"));
        }

        [Test]
        public async Task CreateFormValidator_ShouldFail_WhenSurnameHasDigits()
        {
            var dto = new CreateApplicationFormDto
            {
                Name = "John",
                Surname = "Doe123",
                Email = "john.doe@example.com",
                PhoneNumber = "050 123 45 67"
            };

            var result = await _validator.ValidateAsync(dto);

            Assert.That(result.IsValid, Is.False);
            Assert.That(result.Errors, Has.Some.Matches<FluentValidation.Results.ValidationFailure>(e => e.PropertyName == "Surname"));
        }

        [Test]
        public async Task CreateFormValidator_ShouldFail_WhenEmailIsInvalid()
        {
            var dto = new CreateApplicationFormDto
            {
                Name = "John",
                Surname = "Doe",
                Email = "invalid-email",
                PhoneNumber = "050 123 45 67"
            };

            var result = await _validator.ValidateAsync(dto);

            Assert.That(result.IsValid, Is.False);
            Assert.That(result.Errors, Has.Some.Matches<FluentValidation.Results.ValidationFailure>(e => e.PropertyName == "Email"));
        }

        [Test]
        public async Task CreateFormValidator_ShouldFail_WhenPhoneNumberIsInvalid()
        {
            var dto = new CreateApplicationFormDto
            {
                Name = "John",
                Surname = "Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "9991234567" // Invalid prefix
            };

            var result = await _validator.ValidateAsync(dto);

            Assert.That(result.IsValid, Is.False);
            Assert.That(result.Errors, Has.Some.Matches<FluentValidation.Results.ValidationFailure>(e => e.PropertyName == "PhoneNumber"));
        }
    }
}
