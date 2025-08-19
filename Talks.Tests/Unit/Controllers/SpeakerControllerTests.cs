using Application._Interfaces.Services;
using Application.Commands;
using Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Talks.API.Controllers;
using Utilities;
using Xunit;

namespace Talks.Tests.Unit.Controllers
{
    [Trait("Category", "Unit")]
    public class SpeakerControllerTests
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly Mock<ISessionApprovalService> _mockSessionApprovalService;
        private readonly Mock<ISpeakerPricingService> _mockSpeakerPricingService;
        private readonly Mock<ISpeakerValidationService> _mockSpeakerValidationService;
        private readonly SpeakerController _controller;

        public SpeakerControllerTests()
        {
            _mockMediator = new Mock<IMediator>();
            _mockSessionApprovalService = new Mock<ISessionApprovalService>();
            _mockSpeakerPricingService = new Mock<ISpeakerPricingService>();
            _mockSpeakerValidationService = new Mock<ISpeakerValidationService>();
            _controller = new SpeakerController(_mockMediator.Object, _mockSessionApprovalService.Object, _mockSpeakerPricingService.Object, _mockSpeakerValidationService.Object);
        }

        [Fact]
        public async Task RegisterSpeakerAsync_WhenNoSessions_ReturnsOkWithNoSessionsProvided()
        {
            var speaker = new SpeakerDto { Sessions = null };
            var result = await _controller.RegisterSpeakerAsync(speaker);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(RegistrationResult.NoSessionsProvided.ToString(), okResult.Value);
        }
 
        [Fact]
        public async Task RegisterSpeakerAsync_WhenSuccessful_CallsMediatorAndReturnsResult()
        {
            var speaker = CreateValidSpeaker();
            var approvedSessions = new List<SessionDto> { new() { IsApproved = true } };
            var registrationFee = 100m;
            var mediatorResult = Status.Success;

            _mockSpeakerValidationService.Setup(x => x.IsRegistrationValid(speaker))
                .Returns(RegistrationResult.Success);

            _mockSessionApprovalService.Setup(x => x.ApproveSessions(speaker.Sessions))
                .Returns(approvedSessions);

            _mockSpeakerPricingService.Setup(x => x.GetRegistrationFee(speaker.YearsOfExperience))
                .Returns(registrationFee);

            _mockMediator.Setup(x => x.Send(It.IsAny<AddSpeakerCommand>(), default))
                .ReturnsAsync(mediatorResult);

            var result = await _controller.RegisterSpeakerAsync(speaker);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(mediatorResult.ToString(), okResult.Value);
            _mockMediator.Verify(x => x.Send(It.IsAny<AddSpeakerCommand>(), default), Times.Once);
        }

        [Fact]
        public async Task RunExampleAsync_ReturnsOkResult()
        {
            var result = await _controller.RunExampleAsync();
            Assert.IsType<OkObjectResult>(result);
        }

        private SpeakerDto CreateValidSpeaker()
        {
            return new SpeakerDto
            {
                FirstName = "Stephen",
                LastName = "Mayer",
                Email = "stephen.mayer@gmail.com",
                YearsOfExperience = 5,
                Sessions = new List<SessionDto> { new() { Title = "Massive Test" } }
            };
        }
    }
}
