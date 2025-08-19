using Application.Dtos;

namespace Application._Interfaces.Services
{
    public interface ISessionApprovalService
    {
        List<SessionDto> ApproveSessions(List<SessionDto> sessions);
    }
}