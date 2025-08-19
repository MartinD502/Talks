using Application.Dtos;
using Utilities;

namespace Application._Interfaces.Services
{
    public interface ISpeakerValidationService
    {
        RegistrationResult IsRegistrationValid(SpeakerDto speaker);
    }
}