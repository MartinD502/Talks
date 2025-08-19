using Application._Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Contexts;
using Utilities;

namespace Infrastructure.Repositories
{
    public class SpeakerRepository : ISpeakerRepository
    {
        private readonly TalksDbContext _context;

        public SpeakerRepository(TalksDbContext context)
        {
            _context = context;
        }

        public async Task<Status> AddSpeakerAsync(Speaker speaker)
        {
            _context.Set<Speaker>().Add(speaker);
            int resultCount = await _context.SaveChangesAsync();

            return (resultCount > 0) ? Status.Success : Status.Error;
        }
    }
}
