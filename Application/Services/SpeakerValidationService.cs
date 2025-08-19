using Application._Interfaces.Services;
using Application.Dtos;
using Utilities;

namespace Application.Services
{
    public class SpeakerValidationService : ISpeakerValidationService
    {
        public RegistrationResult IsRegistrationValid(SpeakerDto speaker)
        {
            if (string.IsNullOrEmpty(speaker.FirstName))
            {
                return RegistrationResult.FirstNameRequired;
            }

            if (string.IsNullOrEmpty(speaker.LastName))
            {
                return RegistrationResult.LastNameRequired;
            }

            if (string.IsNullOrEmpty(speaker.Email))
            {
                return RegistrationResult.EmailRequired;
            }
            
            if (speaker.YearsOfExperience > 10 || speaker.IsBlog == true || (speaker.Certifications != null && speaker.Certifications.Count > 3))
            {
                if (!string.IsNullOrEmpty(speaker.Employer) && CommonConstants.Employers.Any(p => speaker.Employer.ToLower().Contains(p.ToLower())))
                {
                    return RegistrationResult.Success;
                }
            }
            else
            {
                var splits = speaker.Email.Split('@');
                if (splits.Length == 2)
                {
                    string emailDomain = splits.Last().ToLower();

                    if (!CommonConstants.Domains.Contains(emailDomain) && !(speaker?.WebBrowser?.Name.ToLower() == WebBrowser.InternetExplorer.ToString().ToLower() && speaker.WebBrowser.MajorVersion < 9))
                    {
                        return RegistrationResult.Success;
                    }
                }
            }

            return RegistrationResult.SpeakerDoesNotMeetStandards;
        }
    }
}