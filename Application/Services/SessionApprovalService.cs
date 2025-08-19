using Application._Interfaces.Services;
using Application.Dtos;
using Utilities;

namespace Application.Services
{
    public class SessionApprovalService : ISessionApprovalService
    {
        public List<SessionDto> ApproveSessions(List<SessionDto> sessions)
        {
            foreach (var session in sessions)
            {
                var hasLegacyInTitle = !string.IsNullOrEmpty(session.Title) &&
                    CommonConstants.LegacyTechnology.Any(x => session.Title.ToLower().Contains(x.ToLower()));

                var hasLegacyInDescription = !string.IsNullOrEmpty(session.Description) &&
                    CommonConstants.LegacyTechnology.Any(x => session.Description.ToLower().Contains(x.ToLower()));

                if (!hasLegacyInTitle && !hasLegacyInDescription)
                {
                    session.IsApproved = true;
                }
            }

            return sessions;
        }
    }
}