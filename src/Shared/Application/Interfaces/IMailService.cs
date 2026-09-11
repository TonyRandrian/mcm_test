using Mcm.Shared.Application.Common;

namespace Mcm.Shared.Application.Interfaces
{
    public interface IMailService
    {
        Task SendInvitationAsync(MailInvitationRequest request);
    }
}