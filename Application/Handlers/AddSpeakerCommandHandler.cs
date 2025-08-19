using Application._Interfaces.Repositories;
using Application.Commands;
using Domain.Entities;
using MediatR;
using Utilities;

namespace Application.Handlers
{
    public class AddSpeakerCommandHandler : IRequestHandler<AddSpeakerCommand, Status>
    {
        private readonly ISpeakerRepository _speakerRepository;

        public AddSpeakerCommandHandler(ISpeakerRepository speakerRepository)
        {
            _speakerRepository = speakerRepository;
        }

        public async Task<Status> Handle(AddSpeakerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var dto = request.Speaker;

                var newSpeaker = new Speaker
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Email = dto.Email,
                    Employer = dto.Employer,
                    IsBlog = dto.IsBlog,
                    BlogUrl = dto.BlogUrl,
                    RegistrationFee = request.RegistrationFee,
                    WebBrowserName = dto.WebBrowser?.Name,
                    WebBrowserMajorVersion = dto.WebBrowser?.MajorVersion,
                    YearsOfExperience = dto.YearsOfExperience,
                };

                if (dto.Certifications != null)
                {
                    foreach (var certification in dto.Certifications)
                    {
                        newSpeaker.Certifications.Add(new Certification { Name = certification });
                    }
                }

                if (dto.Sessions != null)
                {
                    foreach (var session in dto.Sessions)
                    {
                        newSpeaker.Sessions.Add(new Session { Description = session.Description, Title = session.Title ?? null, IsApproved = session.IsApproved });
                    }
                }

                var result = await _speakerRepository.AddSpeakerAsync(newSpeaker);
                return result;
            }
            catch (Exception ex)
            {
                return Status.Error;
            }
        }





        }
}
