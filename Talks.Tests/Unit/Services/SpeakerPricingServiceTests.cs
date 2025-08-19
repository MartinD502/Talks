using Application.Services;
using Xunit;

namespace Talks.Tests.Unit.Services
{
    [Trait("Category", "Unit")]
    public class SpeakerPricingServiceTests
    {
        private readonly SpeakerPricingService _speakerPricingService;

        public SpeakerPricingServiceTests()
        {
            _speakerPricingService = new SpeakerPricingService();
        }

        [Theory]
        [InlineData(0, 500)]
        [InlineData(1, 500)]
        public void GetRegistrationFee_When1YearOrLess_Returns500(int yearsOfExperience, decimal expectedFee)
        {
            var result = _speakerPricingService.GetRegistrationFee(yearsOfExperience);
            Assert.Equal(expectedFee, result);
        }

        [Theory]
        [InlineData(2, 250)]
        [InlineData(3, 250)]
        public void GetRegistrationFee_When2Or3Years_Returns250(int yearsOfExperience, decimal expectedFee)
        {
            var result = _speakerPricingService.GetRegistrationFee(yearsOfExperience);
            Assert.Equal(expectedFee, result);
        }

        [Theory]
        [InlineData(4, 100)]
        [InlineData(5, 100)]
        public void GetRegistrationFee_When4Or5Years_Returns100(int yearsOfExperience, decimal expectedFee)
        {
            var result = _speakerPricingService.GetRegistrationFee(yearsOfExperience);
            Assert.Equal(expectedFee, result);
        }

        [Theory]
        [InlineData(6, 50)]
        [InlineData(7, 50)]
        [InlineData(8, 50)]
        [InlineData(9, 50)]
        public void GetRegistrationFee_When6To9Years_Returns50(int yearsOfExperience, decimal expectedFee)
        {
            var result = _speakerPricingService.GetRegistrationFee(yearsOfExperience);
            Assert.Equal(expectedFee, result);
        }

        [Theory]
        [InlineData(10, 0)]
        [InlineData(20, 0)]
        [InlineData(30, 0)]
        public void GetRegistrationFee_When10YearsOrMore_Returns0(int yearsOfExperience, decimal expectedFee)
        {
            var result = _speakerPricingService.GetRegistrationFee(yearsOfExperience);
            Assert.Equal(expectedFee, result);
        }
    }
}
