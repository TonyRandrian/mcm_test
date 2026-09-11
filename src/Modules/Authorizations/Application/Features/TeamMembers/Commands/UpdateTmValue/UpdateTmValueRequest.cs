namespace Mcm.Authorizations.Application.Features.TeamMembers.Commands.UpdateTmValue
{
    public record UpdateTmValueRequest
    (
        Guid CategoryId,
        List<TmValueRequest> Informations
    );

    public class TmValueRequest
    {
        public Guid PropertyId { get; set; }
        public string Value { get; set; } = string.Empty;
    }
}