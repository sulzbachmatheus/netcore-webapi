using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApi.Controllers;
using WebApi.Data.Protocols;
using WebApi.Models;
using Xunit;

namespace WebApi.Tests
{
    public class ApplicatioControllerTests
    {
        private readonly Mock<IApplicationRepository> _mockRepo;
        private readonly Mock<IApplicationService> _mockService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<INotifier> _mockNotifier;
        private readonly ApplicationsController _controller;
        public ApplicatioControllerTests()
        {
            _mockRepo = new Mock<IApplicationRepository>();
            _mockService = new Mock<IApplicationService>();
            _mockMapper = new Mock<IMapper>();
            _mockNotifier = new Mock<INotifier>();
            _controller = new ApplicationsController(_mockRepo.Object, _mockService.Object, _mockMapper.Object, _mockNotifier.Object);
        }

        [Fact]
        public async void Application_Controller_Returns_Expected_Result_When_Get_IsCalled()
        {
            //act
            var response = await _controller.Get();
            var action = response.Result;

            //assert
            var result = Assert.IsType<OkObjectResult>(action);
            var status = result.StatusCode;
            Assert.Equal(200, status);
        }

        [Fact]
        public async void Application_Controller_Returns_Expected_Result_When_GetById_IsCalled()
        {      
            //act
            var response = await _controller.Get(1);
            var action = response.Result;

            //assert
            var result = Assert.IsType<NotFoundResult>(action);
            var status = result.StatusCode;
            Assert.Equal(404, status);
        }

        [Fact]
        public async void Application_Controller_Returns_Expected_Result_When_Post_IsCalled()
        {
            //act
            var response = await _controller.Post(new Dto.ApplicationDto { ApplicationId = 1, DebuggingMode = true, PathLocal = "pathLocal", Url = "url" });
            var action = response.Result;

            //assert
            var result = Assert.IsType<CreatedResult>(action);
            var status = result.StatusCode;
            Assert.Equal(201, status);
        }

        [Fact]
        public async void Application_Controller_Returns_Expected_Result_When_Patch_IsCalled()
        {
            //act
            var response = await _controller.Patch(1, new Dto.ApplicationDto { ApplicationId = 1, DebuggingMode = true, PathLocal = "pathLocal", Url = "url" });
            var action = response.Result;

            //assert
            var result = Assert.IsType<NotFoundResult>(action);
            var status = result.StatusCode;
            Assert.Equal(404, status);
        }

        [Fact]
        public async void Application_Controller_Returns_Expected_Result_When_Put_IsCalled()
        {
            //act
            var response = await _controller.Put(1, new Dto.ApplicationDto { ApplicationId = 1, DebuggingMode = true, PathLocal = "pathLocal", Url = "url" });
            var action = response.Result;

            //assert
            var result = Assert.IsType<NotFoundResult>(action);
            var status = result.StatusCode;
            Assert.Equal(404, status);
        }

        [Fact]
        public async void Application_Controller_Returns_Expected_Result_When_Delete_IsCalled()
        {
            //act
            var response = await _controller.Delete(1);
            var action = response;

            //assert
            var result = Assert.IsType<NotFoundResult>(action);
            var status = result.StatusCode;
            Assert.Equal(404, status);
        }

    }
}
