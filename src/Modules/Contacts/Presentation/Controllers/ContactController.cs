using System.Text.Json;
using Mcm.Contacts.Application.Features.Contacts.Commands.CreateContact;
using Mcm.Contacts.Application.Features.Contacts.Commands.DeleteContact;
using Mcm.Contacts.Application.Features.Contacts.Commands.UpdateContact;
using Mcm.Contacts.Application.Features.Contacts.Queries.GetAllContact;
using Mcm.Contacts.Application.Features.Contacts.Queries.GetContact;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Domain.Enums;
using Mcm.Shared.Infrastructure.Autorisations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Mcm.Contacts.Presentation.Controllers;

[ApiController]
[Route("api/contacts")]
public class ContactController(IMediator mediator)
    : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HasAuthorization(PermModule.Contact, PermAction.Create)]
    [HttpPost]
    public async Task<ActionResult<ApiResponse<CreateContactResponse>>> Create(
        [FromForm] CreateContactRequest body)
    {
        List<ContactValueAdd>? values = null;
        if (body.SupplementaryValues is not null)
            values = JsonSerializer.Deserialize<List<ContactValueAdd>>(body.SupplementaryValues, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        var res = await _mediator.Send(new CreateContactCommand
        {
            LastName = body.LastName,
            FirstName = body.FirstName,
            Poste = body.Poste,
            Email = body.Email,
            Cin = body.Cin,
            Phone = body.Phone,
            Image = body.Image,
            AssociatedCompanyId = body.AssociatedCompanyId,
            SupplementaryValues = values ?? [],
        });
        return Ok(res);
    }

    [HasAuthorization(PermModule.Contact, PermAction.Read)]
    [HttpGet("{Id}")]
    public async Task<ActionResult<ApiResponse<GetContactResponse>>> GetById(        
        Guid Id)
    {
        var res = await _mediator.Send(new GetContactQuery(
            new GetContactRequest(Id)
        ));
        return Ok(res);
    }

    [HasAuthorization(PermModule.Contact, PermAction.Update)]
    [HttpPut("{Id}")]
    public async Task<ActionResult<ApiResponse<UpdateContactResponse>>> Update(
        Guid Id, [FromForm] UpdateContactRequest body)
    {
        var res = await _mediator.Send(new UpdateContactCommand
        {
            Id = Id,
            LastName = body.LastName,
            FirstName = body.FirstName,
            Poste = body.Poste,
            Email = body.Email,
            Cin = body.Cin,
            Phone = body.Phone,
            Image = body.Image,
            AssociatedCompanyId = body.AssociatedCompanyId
        });
        return Ok(res);
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<GetAllContactResponse>>> GetAll(
        [FromQuery] string? search = null,
        [FromQuery] int page = 1,
        [FromQuery] int limit = 5,
        [FromQuery] string? fields = null,
        [FromQuery] string? propertyIds = null)
    {
        var parsedFields = fields?
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(f => f.Trim())
            .ToList() ?? [];

        var parsedPropertyIds = propertyIds?
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(p => Guid.TryParse(p.Trim(), out var g) ? g : (Guid?)null)
            .Where(g => g.HasValue)
            .Select(g => g!.Value)
            .ToList() ?? [];

        var request = new GetAllContactRequest(
            Search: search,
            Page: page,
            Limit: limit,
            Projection: parsedFields.Count > 0 || parsedPropertyIds.Count > 0
                ? new DynamicProjection { Fields = parsedFields, PropertyIds = parsedPropertyIds }
                : null
        );
        var res = await _mediator.Send(new GetAllContactQuery(request));
        return Ok(res);
    }

    [HasAuthorization(PermModule.Contact, PermAction.Delete)]
    [HttpDelete("{Id}")]
    public async Task<ActionResult<ApiResponse<DeleteContactResponse>>> DeleteCompany   
        (Guid Id, [FromQuery] DeleteContactRequest request)
    {
        var res = await _mediator.Send(new DeleteContactCommand
        {
            Id = Id,
            Force = request.Force
        });
        return Ok(res);
    }
}