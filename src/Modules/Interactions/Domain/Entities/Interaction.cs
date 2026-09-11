using System.Runtime.InteropServices.Marshalling;
using Mcm.Interactions.Domain.Enums;
using Mcm.Interactions.Domain.ValueObjects;
using Mcm.Shared.Domain.Interfaces;
using Mcm.Shared.Domain.Primitives;
using Mcm.Shared.Domain.ValueObjects;

namespace Mcm.Interactions.Domain.Entities;

public class Interaction : AggregateRoot, ITenantScoped
{
    public Name Title { get; private set; } = null!;
    public bool IsDone { get; private set; }
    public string? Note { get; private set; }
    public DataTime Date { get; private set; } = null!;
    public Reminder Reminder { get; private set; } = null!;

    public Guid TenantId { get; set; }
    public Guid? ReportId { get; private set; }
    public Report? Report { get; private set; }
    public Guid TypeId { get; private set; }
    public InteractionType Type { get; private set; } = null!;
    
    private List<InteractionMember> _interactionMembers { get; set; } = new();
    private List<InteractionContact> _interactionContacts {get; set; } = new();
    private List<FieldValue> _fieldsValues {get; set; } = new();
    private List<Resource> _attachments {get; set; } = new();
    public IReadOnlyList<InteractionMember> InteractionMembers => _interactionMembers;
    public IReadOnlyList<InteractionContact> InteractionContacts => _interactionContacts;
    public IReadOnlyList<FieldValue> FieldsValues => _fieldsValues;
    public IReadOnlyList<Resource> Attachments => _attachments;


    private Interaction() {}
    private Interaction(
        string title, 
        Guid typeId, 
        string? note,
        string startDate,
        string endDate,
        string? reminderType,
        double? reminderValue,
        int? reminderRepeat,
        Guid createdBy)
    {
        Title = title;
        IsDone = false;
        TypeId = typeId;
        Note = note;
        Date = DataTime.Create(startDate, endDate);
        Reminder = Reminder.Create(reminderType, reminderValue, reminderRepeat);
        CreatedBy = createdBy;
    }

    public static Interaction Create(
        string title, 
        Guid typeId, 
        string? note,
        string startDate,
        string endDate,
        string? reminderType,
        double? reminderValue,
        int? reminderRepeat,
        Guid createdBy)
        => new (title, typeId, note, startDate, endDate, reminderType, reminderValue, reminderRepeat, createdBy);

    public void Update(
        string title, 
        Guid typeId, 
        string? note,
        string startDate,
        string endDate,
        string? reminderType,
        double? reminderValue,
        int? reminderRepeat)
    {
        Title = title;
        TypeId =  typeId;
        Note =  note;
        Date = DataTime.Create(startDate, endDate);
        Reminder = Reminder.Create(reminderType, reminderValue, reminderRepeat);
    }

    public void SetReport(Guid reportId)
        => ReportId = reportId;

    public void AddMember(Guid memberId)
    {
        var exists = _interactionMembers
            .Any(im => im.TeamMemberId == memberId);
        if (exists)
            return;
        
        _interactionMembers.Add(InteractionMember.Create(Id, memberId));
    }

    public void SyncMember(List<Guid> memberIds)
    {
        var hashMembersIds = memberIds.ToHashSet();
        _interactionMembers.RemoveAll(
            im => !memberIds.Contains(im.TeamMemberId));
        foreach (var memberId in hashMembersIds.ToList())
        {
            AddMember(memberId);
        }
    }

    public void AddContact(Guid contactId)
    {
        var exists = _interactionContacts
            .Any(im => im.ContactId == contactId);
        if (exists)
            return;
        
        _interactionContacts.Add(InteractionContact.Create(Id, contactId));
    }

    public void SyncContact(List<Guid> contactIds)
    {
        var hashContactsIds = contactIds.ToHashSet();
        _interactionContacts.RemoveAll(
            im => !contactIds.Contains(im.ContactId));
        foreach (var contactId in hashContactsIds.ToList())
        {
            AddContact(contactId);
        }
    }

    public void AddResource(Resource resource)
    {
        if (_attachments.FirstOrDefault(i => i.Url == resource.Url) is not null)
            return;
        _attachments.Add(resource);
    }

    public void UpdateFieldValue(Guid fvId, string value)
    {
        var exists = _fieldsValues.FirstOrDefault(v => v.TypeFieldId == fvId);
        if (exists is not null)
            exists.Update(value);
        else
        {
            _fieldsValues.Add(FieldValue.Create(fvId, value));
        }
    }

    public void SyncFieldsValues(List<(Guid typeFieldId, string value)> fieldsValues)
    {
        var hashFv = fieldsValues.ToHashSet();
        foreach (var (typeFieldId, value) in hashFv.ToList())
        {
            UpdateFieldValue(typeFieldId, value);
        }
    }

    public int AttachmentCount() => _attachments.Count();

    public void SyncAttachment(List<Guid> contactIds)
    {
        var hashContactsIds = contactIds.ToHashSet();
        _interactionContacts.RemoveAll(
            im => !contactIds.Contains(im.ContactId));
        foreach (var contactId in hashContactsIds.ToList())
        {
            AddMember(contactId);
        }
    }

}
