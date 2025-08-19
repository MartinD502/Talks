using Application._Interfaces.Services;
using Application.Commands;
using Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utilities;

namespace Talks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SpeakerController : ControllerBase
    {
        private readonly ISessionApprovalService _sessionApprovalService;
        private readonly ISpeakerPricingService _speakerPricingService;
        private readonly ISpeakerValidationService _speakerValidationService;
        private readonly IMediator _mediator;

        public SpeakerController(IMediator mediator, ISessionApprovalService sessionApprovalService, ISpeakerPricingService speakerPricingService, ISpeakerValidationService speakerValidationService)
        {            
            _mediator = mediator;
            _sessionApprovalService = sessionApprovalService;
            _speakerPricingService = speakerPricingService;
            _speakerValidationService = speakerValidationService;
        }

        //CREATED BY MD 19/08/2025 - Can reach the endpoint through the following, crude approach... http://localhost:5121/speaker/Run
        //#1 - .NET 8 / C# > V8
        //#2 - I stopped short of integration testing (in-memory database) as we have persistance, so this could have been done and would normally have been.       
        //#3 - I used XUnit/Moq which is are test frameworks of choice, generally.
        //#4 - I utilised EF Core code-first approach.

        #region Speaker
        [AllowAnonymous]
        [HttpPost("RegisterSpeaker")]
        public async Task<IActionResult> RegisterSpeakerAsync([FromBody] SpeakerDto speaker)
        {
            if (speaker.Sessions == null || speaker.Sessions.Count == 0)
            {
                return Ok(RegistrationResult.NoSessionsProvided.ToString());
            }

            RegistrationResult registrationResult = _speakerValidationService.IsRegistrationValid(speaker);

            if (registrationResult == RegistrationResult.Success)
            {
                speaker.Sessions = _sessionApprovalService.ApproveSessions(speaker.Sessions);

                var isAnySessionsApproved = speaker.Sessions.Any(p => p.IsApproved == true);

                if (isAnySessionsApproved == false)
                {
                    return Ok(RegistrationResult.NoSessionsApproved.ToString());
                }
                else
                {
                    decimal registrationFee = _speakerPricingService.GetRegistrationFee(speaker.YearsOfExperience);
                    var command = new AddSpeakerCommand(speaker, registrationFee);
                    var result = await _mediator.Send(command);
                    return Ok(result.ToString());
                }
            }

            return Ok(Status.Error.ToString());
        }

        [AllowAnonymous]
        [HttpGet("Run")]
        public async Task<IActionResult> RunExampleAsync()
        {
            var speaker = new SpeakerDto
            {
                BlogUrl = null,
                Certifications = new List<string> { "HND", "Azure Cloud" },
                Email = "helene.jones@Email.com",
                Employer = "Google",
                FirstName = "Helen",
                IsBlog = false,
                LastName = "Jones",
                WebBrowser = new WebBrowserDto
                {
                    Name = WebBrowser.InternetExplorer.ToString(),
                    MajorVersion = 4
                },
                YearsOfExperience = 11,
                Sessions = new List<SessionDto>()
            };

            speaker.Sessions.Add(new SessionDto { Title = "ASP.NET", IsApproved = false });
            speaker.Sessions.Add(new SessionDto { Description = "Commodore", IsApproved = false });
            speaker.Sessions.Add(new SessionDto { Description = "VBScript", IsApproved = false });

            var result = await RegisterSpeakerAsync(speaker);

            var resultValue = result is OkObjectResult okResult ? okResult.Value?.ToString() : "Error";
            return Ok(resultValue);
        }
        #endregion Speaker
    }
}
