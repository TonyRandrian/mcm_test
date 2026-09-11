using Mcm.Interactions.Domain.ValueObjects;
using Mcm.Shared.Domain.Interfaces;
using Mcm.Shared.Domain.Primitives;
using Mcm.Shared.Domain.ValueObjects;

namespace Mcm.Interactions.Domain.Entities
{
    public class Report
        : AuditableEntity, ITenantScoped
    {
        public Name Name { get; set; } = null!;
        public string Description { get; set; } = string.Empty;
        public string ActionPlan { get; set; } = string.Empty;
        public Guid TenantId { get; set; }
        public DataTime Date { get; set; } = null!;
        public Guid InteractionId { get; set; }

        public Interaction Interaction { get; set; } = null!;
        private List<Resource> _attachments { get; set; } = new();
        private List<ReportContact> _presentContacts { get; set; } = new();
        private List<ReportMember> _presentMembers { get; set; } = new();
        public IReadOnlyList<Resource> Attachments => _attachments;
        public IReadOnlyList<ReportContact> PresentContacts => _presentContacts;
        public IReadOnlyList<ReportMember> PresentMembers => _presentMembers;


        private Report() {}
        private Report(
            Guid interactionId,
            string name,
            string description,
            string actionPlan,
            string startDate,
            string endDate)
        {
            InteractionId = interactionId;
            Name = name;
            Description = description;
            ActionPlan = actionPlan;
            Date = DataTime.Create(startDate, endDate);
        }

        public static Report Create(
            Guid interactionId,
            string name,
            string description,
            string actionPlan,
            string startDate,
            string endDate)
            => new(interactionId, name, description, actionPlan, startDate, endDate);

        public void AddResource(Resource resource)
        {
            if (_attachments.FirstOrDefault(i => i.Url == resource.Url) is not null)
                return;
            _attachments.Add(resource);
        }

        // public void SyncAttachment(List<string> attachments, List<Resource> resources)
        // {
        //     var hashAttachments = attachments.ToHashSet();
        //     _attachments.RemoveAll(a =>
        //         !attachments.Contains(a.Url));
        //     _attachments.AddRange(resources);
        // }

        public void AddContact(Guid contactId)
        {
            var exists = _presentContacts
                .Any(im => im.ContactId == contactId);
            if (exists)
                return;
        
            _presentContacts.Add(ReportContact.Create(Id, contactId));
        }

        public void SyncContact(List<Guid> contactIds)
        {
            var hashContactsIds = contactIds.ToHashSet();
            _presentContacts.RemoveAll(
                rc => !contactIds.Contains(rc.ContactId));
            foreach (var contactId in hashContactsIds.ToList())
            {
                AddContact(contactId);
            }
        }

        public void AddMember(Guid teamMemberId)
        {
            var exists = _presentMembers
                .Any(im => im.TeamMemberId == teamMemberId);
            if (exists)
                return;
        
            _presentMembers.Add(ReportMember.Create(Id, teamMemberId));
        }

        public void SyncMember(List<Guid> teamMemberIds)
        {
            var hashMembersIds = teamMemberIds.ToHashSet();
            _presentMembers.RemoveAll(
                rc => !teamMemberIds.Contains(rc.TeamMemberId));
            foreach (var teamMemberId in hashMembersIds.ToList())
            {
                AddMember(teamMemberId);
            }
        }
    }
}