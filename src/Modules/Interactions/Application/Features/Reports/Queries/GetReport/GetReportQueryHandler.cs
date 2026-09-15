using Mcm.Interactions.Application.Interfaces;
using Mcm.Interactions.Domain.Entities;
using Mcm.Interactions.Domain.Extensions;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Application.Modules;
using MediatR;

namespace Mcm.Interactions.Application.Features.Reports.Queries.GetReport
{
    public class GetReportQueryHandler(
        IReportRepository reportRepository,
        IContactModule contactModule,
        ITeamMemberModule teamMemberModule,
        IInteractionRepository interactionRepository)
        : IRequestHandler<GetReportQuery, ApiResponse<GetReportResponse>>
    {
        private readonly IReportRepository _reportRepository = reportRepository;
        private readonly IContactModule _contactModule = contactModule;
        private readonly ITeamMemberModule _teamMemberModule = teamMemberModule;
        private readonly IInteractionRepository _interactionRepository = interactionRepository;

        public async Task<ApiResponse<GetReportResponse>> Handle(GetReportQuery query, CancellationToken cancellationToken)
        {
            var interaction = await _interactionRepository.GetByIdAsync(query.Request.InteractionId)
                ?? throw NotFoundException.NotFoundById(nameof(Interaction), query.Request.InteractionId);
            
            if (interaction.ReportId is null)
                throw new NotFoundException("Interaction has not report");
            var report = await _reportRepository.GetByIdAsync(interaction.ReportId.Value)
                ?? throw NotFoundException.NotFoundById(nameof(Report), interaction.ReportId.Value);
            
            var members = await _teamMemberModule.GetIdentities(interaction.InteractionMembers.Select(im => im.TeamMemberId).ToList());
            var membersDict = members.ToDictionary(
                m => m.Id,
                m => new
                {
                    m.Id,
                    m.FirstName,
                    m.LastName,
                    m.Email
                });
            var contacts = await _contactModule.GetIdentities(interaction.InteractionContacts.Select(im => im.ContactId).ToList());
            var contactsDict = contacts.ToDictionary(
                c => c.Id,
                c => new
                {
                    c.Id,
                    c.FirstName,
                    c.LastName,
                    c.Email
                });

            return new ApiResponse<GetReportResponse>
            {
                Success = true,
                Message = "Get Report successfully",
                Code = 200,
                Data = new GetReportResponse
                {
                    Name = report.Name,
                    Description = report.Description,
                    ActionPlan = report.ActionPlan,
                    Attachments = report.Attachments.Select(
                        a => new GetReport_Attachment(a.ContentType, a.Url)).ToList(),
                    Date = new GetReport_Date(
                        report.Date.StartDate.GetDate(),
                        report.Date.StartDate.GetHour(),
                        report.Date.EndDate.GetDate(),
                        report.Date.EndDate.GetHour()
                    ),
                    Members = new GetReport_Members(
                        Present: report.PresentMembers.Select(m =>
                            {
                                var member = membersDict[m.TeamMemberId];
                                return new GetReport_Identity(member.Id, member.LastName, member.FirstName);
                            }).ToList(),
                        Absent: members
                            .Where(m => !report.PresentMembers
                                .Select(pm => pm.TeamMemberId)
                                .Contains(m.Id))
                            .Select(m =>
                            {
                                var member = membersDict[m.Id];
                                return new GetReport_Identity(member.Id, member.LastName, member.FirstName);
                            }).ToList()
                    ),
                    Contacts = new GetReport_Contacts(
                        Present: report.PresentContacts.Select(m =>
                            {
                                var contact = contactsDict[m.ContactId];
                                return new GetReport_Identity(contact.Id, contact.LastName, contact.FirstName);
                            }).ToList(),
                        Absent: contacts
                            .Where(m => !report.PresentContacts
                                .Select(pm => pm.ContactId)
                                .Contains(m.Id))
                            .Select(m =>
                            {
                                var contact = contactsDict[m.Id];
                                return new GetReport_Identity(contact.Id, contact.LastName, contact.FirstName);
                            }).ToList()
                    )
                }
            };
        }
    }
}