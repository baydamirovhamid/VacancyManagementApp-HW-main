using Moq;
using VacancyManagementApp.Application.DTOs.Vacancy;
using VacancyManagementApp.Application.Repositories;
using VacancyManagementApp.Domain.Entities;
using VacancyManagementApp.Persistence.Services;
using AutoMapper;
using System;
using System.Threading.Tasks;
using NUnit.Framework;


namespace VacancyManagementApp.UnitTest.Services
{
    [TestFixture]
    public class VacancyServiceTests
    {
        private Mock<IVacancyWriteRepository> _vacancyWriteRepoMock;
        private Mock<IMapper> _mapperMock;
        private VacancyService _vacancyService;

        [SetUp]
        public void SetUp()
        {
            _vacancyWriteRepoMock = new Mock<IVacancyWriteRepository>();
            _mapperMock = new Mock<IMapper>();

            // Creating the service
            _vacancyService = new VacancyService(
                _vacancyWriteRepoMock.Object,
                _mapperMock.Object,
                null
            );
        }

        [Test]
        public async Task CreateVacancyAsync_ShouldReturnSuccessMessage_WhenVacancyIsCreated()
        {
            var createVacancyDto = new CreateVacancyDto { Title = "Backend Developer" };

            var vacancy = new Vacancy { Id = Guid.NewGuid(), Title = createVacancyDto.Title };

            _mapperMock.Setup(m => m.Map<Vacancy>(It.IsAny<CreateVacancyDto>())).Returns(vacancy);
            _vacancyWriteRepoMock.Setup(r => r.AddAsync(It.IsAny<Vacancy>())).ReturnsAsync(true);
            _vacancyWriteRepoMock.Setup(r => r.SaveAsync()).ReturnsAsync(1); 

            var result = await _vacancyService.CreateVacancyAsync(createVacancyDto);

            Assert.That(result, Is.Not.Null, "Result should not be null");
            Assert.That(result.Succeeded, Is.True, "Operation should succeed");
            Assert.That(result.Message, Is.EqualTo("Vacancy created successfully!"), "Success message should match");
        }
    }
}
