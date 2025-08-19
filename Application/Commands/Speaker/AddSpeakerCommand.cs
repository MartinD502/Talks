using Application.Dtos;
using MediatR;
using Utilities;

namespace Application.Commands
{
    public class AddSpeakerCommand : IRequest<Status>
    {
        public SpeakerDto Speaker { get; }
        public decimal RegistrationFee { get; }

        public AddSpeakerCommand(SpeakerDto speaker, decimal registrationFee )
        {
            Speaker = speaker;
            RegistrationFee = registrationFee;
        }
    }
}
