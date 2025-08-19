using Application.Dtos;
using Application.Services;
using Utilities;
using Xunit;

namespace Talks.Tests.Unit.Services
{
    [Trait("Category", "Unit")]
    public class SpeakerValidationServiceTests
    {
        private readonly SpeakerValidationService _speakerValidationService;

        public SpeakerValidationServiceTests()
        {
            _speakerValidationService = new SpeakerValidationService();
        }

        #region Required Tests
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void IsRegistrationValid_WhenFirstNameIsNullOrEmpty_ReturnsFirstNameRequired(string? firstName)
        {
            var speaker = CreateValidSpeaker();
            speaker.FirstName = firstName;

            var result = _speakerValidationService.IsRegistrationValid(speaker);

            Assert.Equal(RegistrationResult.FirstNameRequired, result);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void IsRegistrationValid_WhenLastNameIsNullOrEmpty_ReturnsLastNameRequired(string? lastName)
        {
            var speaker = CreateValidSpeaker();
            speaker.LastName = null;

            var result = _speakerValidationService.IsRegistrationValid(speaker);

            Assert.Equal(RegistrationResult.LastNameRequired, result);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void IsRegistrationValid_WhenEmailIsNullOrEmpty_ReturnsEmailRequired(string? email)
        {
            var speaker = CreateValidSpeaker();
            speaker.Email = email;

            var result = _speakerValidationService.IsRegistrationValid(speaker);

            Assert.Equal(RegistrationResult.EmailRequired, result);
        }     
        #endregion Required Tests

        #region Over 10 years experience with blogs and certifications and employers.
        [Fact]
        public void IsRegistrationValid_WhenHighYearsOfExperienceGreaterThan10AndIsValidEmployer_ReturnsSuccess()
        {
            var speaker = CreateValidSpeaker();
            speaker.YearsOfExperience = 15;
            speaker.Employer = "Google";
            speaker.IsBlog = true;

            var result = _speakerValidationService.IsRegistrationValid(speaker);

            Assert.Equal(RegistrationResult.Success, result);
        }

        [Fact]
        public void IsRegistrationValid_WhenHighYearsOfExperienceOrBlogOrCertificationsWithValidEmployer_ReturnsSuccess()
        {
            var speaker = CreateValidSpeaker();
            speaker.YearsOfExperience = 17;
            speaker.IsBlog = true;
            speaker.Certifications = new List<string> { "AWS", "Azure", "Python", "C#", ".NET" };
            speaker.Employer = "Microsoft";

            var result = _speakerValidationService.IsRegistrationValid(speaker);

            Assert.Equal(RegistrationResult.Success, result);
        }

        [Fact]
        public void IsRegistrationValid_WhenHighYearsOfExperienceOrBlogOrCertificationsWithInValidEmployer_ReturnsSpeakerDoesNotMeetStandards()
        {
            var speaker = CreateValidSpeaker();
            speaker.YearsOfExperience = 17;
            speaker.IsBlog = true;
            speaker.Certifications = new List<string> { "AWS", "Azure", "Python", "C#", ".NET" };
            speaker.Employer = "IBM";

            var result = _speakerValidationService.IsRegistrationValid(speaker);

            Assert.Equal(RegistrationResult.SpeakerDoesNotMeetStandards, result);
        }

        [Fact]
        public void IsRegistrationValid_WhenHighYearsOfExperienceOrBlogWithNoCertificationsWithNoEmployer_ReturnsSpeakerDoesNotMeetStandards()
        {
            var speaker = CreateValidSpeaker();
            speaker.YearsOfExperience = 17;
            speaker.IsBlog = true;
            speaker.Certifications = null;
            speaker.Employer = null;

            var result = _speakerValidationService.IsRegistrationValid(speaker);

            Assert.Equal(RegistrationResult.SpeakerDoesNotMeetStandards, result);
        }
        #endregion Over 10 years experience with blogs and certifications and employers.

        #region Under 10 years experience with no blogs and low certifications and no employers with valid non-legacy domain but invalid browser and invalid version.
        [Fact]
        public void IsRegistrationValid_WhenLowYearsOfExperienceWithNoBlogsWithInValidCertificationsWithValidDomainWithInvalidWebBrowserAndVersion_ReturnsSpeakerDoesNotMeetStandards()
        {
            var speaker = CreateValidSpeaker();
            speaker.YearsOfExperience = 3;
            speaker.IsBlog = false;
            speaker.Certifications = new List<string> { "AWS", "Python" };
            speaker.Email = "test@gmail.com";
            speaker.WebBrowser = new WebBrowserDto
            {
                Name = WebBrowser.InternetExplorer.ToString(),
                MajorVersion = 6
            };

            var result = _speakerValidationService.IsRegistrationValid(speaker);

            Assert.Equal(RegistrationResult.SpeakerDoesNotMeetStandards, result);
        }
        #endregion Under 10 years experience with no blogs and low certifications and no employers.

        [Fact]
        public void IsRegistrationValid_LowYearsOfExperienceWithValidDomainWithInvalidWebBrowserAndVersion_ReturnsSpeakerDoesNotMeetStandards()
        {
            var speaker = CreateValidSpeaker();
            speaker.YearsOfExperience = 5;
            speaker.Email = "test@aol.com";
            speaker.WebBrowser = new WebBrowserDto
            {
                Name = WebBrowser.InternetExplorer.ToString(),
                MajorVersion = 9
            };

            var result = _speakerValidationService.IsRegistrationValid(speaker);

            Assert.Equal(RegistrationResult.SpeakerDoesNotMeetStandards, result);
        }

        #region Specific legacy email domain check with invalid browser and version.
        [Theory]
        [InlineData("aol.com")]
        [InlineData("compuserve.com")]
        [InlineData("prodigy.com")]
        public void IsRegistrationValid_WhenLowYearsOfExperienceWithInValidDomainsWithInvalidWebBrowserAndVersion_ReturnsSpeakerDoesNotMeetStandards(string domain)
        {
            var speaker = CreateValidSpeaker();
            speaker.YearsOfExperience = 3;
            speaker.IsBlog = false;
            speaker.Certifications = new List<string> { ".NET" };
            speaker.Email = $"test@{domain}";
            speaker.WebBrowser = new WebBrowserDto
            {
                Name = WebBrowser.InternetExplorer.ToString(),
                MajorVersion = 8
            };

            var result = _speakerValidationService.IsRegistrationValid(speaker);

            Assert.Equal(RegistrationResult.SpeakerDoesNotMeetStandards, result);
        }
        #endregion Specific legacy email domain check with invalid browser and version.

        #region Specific legacy email domain check with invalid browser and version.
        [Theory]
        [InlineData("aol.com")]
        [InlineData("compuserve.com")]
        [InlineData("prodigy.com")]
        public void IsRegistrationValid_WhenLowYearsOfExperienceWithInValidDomainsWithValidWebBrowserAndVersion_ReturnsSpeakerDoesNotMeetStandards(string domain)
        {
            var speaker = CreateValidSpeaker();
            speaker.YearsOfExperience = 3;
            speaker.IsBlog = false;
            speaker.Certifications = new List<string> { ".NET" };
            speaker.Email = $"test@{domain}";
            speaker.WebBrowser = new WebBrowserDto
            {
                Name = WebBrowser.Firefox.ToString(),
                MajorVersion = 10
            };

            var result = _speakerValidationService.IsRegistrationValid(speaker);

            Assert.Equal(RegistrationResult.SpeakerDoesNotMeetStandards, result);
        }
        #endregion Specific legacy email domain check with invalid browser and version.

        #region Specific modern email domain check with valid browser and valid version.
        [Theory]
        [InlineData("gmail.com")]
        [InlineData("outlook.com")]
        [InlineData("bbc.co.uk")]
        public void IsRegistrationValid_WhenLowYearsOfExperienceWithValidDomainsWithValidWebBrowserAndVersion_ReturnsSucces(string domain)
        {
            var speaker = CreateValidSpeaker();
            speaker.YearsOfExperience = 2;
            speaker.IsBlog = false;
            speaker.Certifications = new List<string> { "Blazor" };
            speaker.Email = $"test@{domain}";
            speaker.WebBrowser = new WebBrowserDto
            {
                Name = WebBrowser.Chrome.ToString(),
                MajorVersion = 15
            };

            var result = _speakerValidationService.IsRegistrationValid(speaker);

            Assert.Equal(RegistrationResult.Success, result);
        }
        #endregion Specific modern email domain check with valid browser and valid version.

        private SpeakerDto CreateValidSpeaker()
        {
            return new SpeakerDto
            {
                FirstName = "Barry",
                LastName = "Smith",
                Email = "barry.smith@example.com",
                Employer = "Microsoft",
                YearsOfExperience = 4,
                IsBlog = false,
                Certifications = new List<string> { "Google Cloud Certified" },
                WebBrowser = new WebBrowserDto
                {
                    Name = WebBrowser.InternetExplorer.ToString(),
                    MajorVersion = 90
                }
            };
        }

        
    }
}