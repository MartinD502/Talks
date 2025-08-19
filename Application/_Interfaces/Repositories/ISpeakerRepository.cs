using Domain.Entities;
using Utilities;

namespace Application._Interfaces.Repositories
{
    public interface ISpeakerRepository
    {
        Task<Status> AddSpeakerAsync(Speaker speaker);
    }
}
