using Application._Interfaces.Services;

namespace Application.Services
{
    public class SpeakerPricingService : ISpeakerPricingService
    {
        public decimal GetRegistrationFee(int yearsOfExperience)
        {
            if (yearsOfExperience <= 1)
            {
                return 500;
            }
            else if (yearsOfExperience == 2 || yearsOfExperience == 3)
            {
                return 250;
            }
            else if (yearsOfExperience == 4 || yearsOfExperience == 5)
            {
                return 100;
            }
            else if (yearsOfExperience == 6 || yearsOfExperience == 7 || yearsOfExperience == 8 || yearsOfExperience == 9)
            {
                return 50;
            }
            else
            {
                return 0;
            }
        }
    }
}