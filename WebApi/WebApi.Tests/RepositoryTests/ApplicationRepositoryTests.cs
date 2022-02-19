using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using Xunit;

namespace WebApi.Tests
{
    public class ApplicationRepositoryTests
    {
        private readonly Mock<IApplicationRepository> _mockRepo;
        public ApplicationRepositoryTests()
        {
            _mockRepo = new Mock<IApplicationRepository>();
        }

        [Fact]
        public async void Application_Repository_Returns_Expected_Result_When_GetAll_IsCalled()
        {
            // arrange
            IEnumerable<Application> app = new List<Application>()
            {
                new Application() { ApplicationId = 1, DebuggingMode = true, InternalId = new MongoDB.Bson.ObjectId(), PathLocal = "pathLocal", Url = "url" },
                new Application() { ApplicationId = 2, DebuggingMode = true, InternalId = new MongoDB.Bson.ObjectId(), PathLocal = "pathLocal", Url = "url" },
            };
            
            _mockRepo.Setup(
                rep => rep.GetAllApplications()).Returns(Task.FromResult(app));

            //act
            var result = await _mockRepo.Object.GetAllApplications();

            //assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async void Application_Repository_Returns_Expected_Result_When_GetAllById_IsCalled()
        {
            // arrange
            IEnumerable<Application> app = new List<Application>()
            {
                new Application() { ApplicationId = 1, DebuggingMode = true, InternalId = new MongoDB.Bson.ObjectId(), PathLocal = "pathLocal", Url = "url" },
                new Application() { ApplicationId = 2, DebuggingMode = true, InternalId = new MongoDB.Bson.ObjectId(), PathLocal = "pathLocal", Url = "url" },
            };

            _mockRepo.Setup(
                rep => rep.GetApplication(2)).Returns(Task.FromResult(app.Where(o => o.ApplicationId == 2).FirstOrDefault()));

            //act
            var result = await _mockRepo.Object.GetApplication(2);

            //assert
            Assert.NotNull(result);
            Assert.Equal(2, result.ApplicationId);
        }

        [Fact]
        public void Application_Repository_Returns_Expected_Result_When_Create_IsCalled()
        {
            // arrange
            var app = new Application()
            {
                ApplicationId = 3,
                DebuggingMode = true,
                InternalId = new MongoDB.Bson.ObjectId(),
                PathLocal = "pathLocal",
                Url = "url"
            };

            //act
            var result = _mockRepo.Object.Create(app);

            //assert
            Assert.NotNull(result);
            result.Exception.Should().BeNull();
        }

        [Fact]
        public void Application_Repository_Returns_Expected_Result_When_Update_IsCalled()
        {
            // arrange
            var newAppContent = new Application()
            {
                ApplicationId = 2,
                DebuggingMode = false,
                InternalId = new MongoDB.Bson.ObjectId(),
                PathLocal = "new-pathLocal",
                Url = "new-url"
            };

            _mockRepo.Setup(
                rep => rep.Update(newAppContent)).Returns(Task.FromResult(true));            

            //act
            var result = _mockRepo.Object.Update(newAppContent);
            result.Wait();
            bool isValid = result.Result;

            //assert
            Assert.NotNull(result);
            isValid.Should().Be(true);
        }

        [Fact]
        public void Application_Repository_Returns_Expected_Result_When_PartialUpdate_IsCalled()
        {
            // arrange
            var newAppContent = new Application()
            {
                ApplicationId = 2,
                DebuggingMode = true,
                InternalId = new MongoDB.Bson.ObjectId(),
                PathLocal = "pathLocal",
                Url = "new-url"
            };

            _mockRepo.Setup(
                rep => rep.PartialUpdate(newAppContent)).Returns(Task.FromResult(true));

            //act
            var result = _mockRepo.Object.PartialUpdate(newAppContent);
            result.Wait();
            bool isValid = result.Result;

            //assert
            Assert.NotNull(result);
            isValid.Should().Be(true);
        }

        [Fact]
        public void Application_Repository_Returns_Expected_Result_When_Delete_IsCalled()
        {
            // arrange
            var appToBeDeleted = new Application()
            {
                ApplicationId = 2,
                DebuggingMode = true,
                InternalId = new MongoDB.Bson.ObjectId(),
                PathLocal = "pathLocal",
                Url = "new-url"
            };

            _mockRepo.Setup(
                rep => rep.Delete(appToBeDeleted.ApplicationId)).Returns(Task.FromResult(true));

            //act
            var result = _mockRepo.Object.Delete(appToBeDeleted.ApplicationId);
            result.Wait();
            bool isValid = result.Result;

            //assert
            Assert.NotNull(result);
            isValid.Should().Be(true);
        }


    }
}
