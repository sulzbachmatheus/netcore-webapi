using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Data.Protocols;
using WebApi.Models;
using Xunit;

namespace WebApi.Tests
{
    public class ApplicatioServiceTests
    {
        private readonly Mock<IApplicationService> _mockService;
        public ApplicatioServiceTests()
        {
            _mockService = new Mock<IApplicationService>();
        }

        [Fact]
        public async void Application_Service_Returns_Expected_Result_When_Post_IsCalled()
        {
            //arrange
            var app = new Application
            {
                ApplicationId = 1,
                DebuggingMode = true,
                InternalId = new MongoDB.Bson.ObjectId(),
                PathLocal = "pathLocal",
                Url = "url"
            };

            var response = _mockService.Setup(s => s.Post(app)).Returns(Task.FromResult(true));

            //act            
            var result = await _mockService.Object.Post(app);

            //assert
            _mockService.Verify(v => v.Post(app), Times.Once());
            result.Should().Be(true);
        }

        [Fact]
        public async void Application_Service_Returns_Expected_Result_When_Put_IsCalled()
        {
            //arrange
            var app = new Application
            {
                ApplicationId = 1,
                DebuggingMode = true,
                InternalId = new MongoDB.Bson.ObjectId(),
                PathLocal = "pathLocal",
                Url = "url"
            };

            var response = _mockService.Setup(s => s.Put(app, new MongoDB.Bson.ObjectId())).Returns(Task.FromResult(true));

            //act            
            var result = await _mockService.Object.Put(app, new MongoDB.Bson.ObjectId());

            //assert
            _mockService.Verify(v => v.Put(app, new MongoDB.Bson.ObjectId()), Times.Once());
            result.Should().Be(true);
        }

        [Fact]
        public async void Application_Service_Returns_Expected_Result_When_Patch_IsCalled()
        {
            //arrange
            var app = new Application
            {
                ApplicationId = 1,
                DebuggingMode = true,
                InternalId = new MongoDB.Bson.ObjectId(),
                PathLocal = "pathLocal",
                Url = "url"
            };

            var response = _mockService.Setup(s => s.Patch(app, new MongoDB.Bson.ObjectId())).Returns(Task.FromResult(true));

            //act            
            var result = await _mockService.Object.Patch(app, new MongoDB.Bson.ObjectId());

            //assert
            _mockService.Verify(v => v.Patch(app, new MongoDB.Bson.ObjectId()), Times.Once());
            result.Should().Be(true);
        }

    }
}
