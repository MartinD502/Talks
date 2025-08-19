namespace Application._Interfaces.Services
{
    public interface ISpeakerPricingService
    {
        decimal GetRegistrationFee(int yearsOfExperience);
    }
}