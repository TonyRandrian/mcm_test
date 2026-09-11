using System.Text.Json;
using Mcm.Company.Application.Features.Company.Commands.AddCompanyValue;
using Mcm.Company.Application.Features.Company.Commands.ConvertTypeContact;
using Mcm.Company.Application.Features.Company.Commands.CreateCompany;
using Mcm.Company.Application.Features.Company.Commands.DefineCompanyLeader;
using Mcm.Company.Application.Features.Company.Commands.DeleteCompany;
using Mcm.Company.Application.Features.Company.Commands.UpdateCompany;
using Mcm.Company.Application.Features.Company.Commands.UpdateCompanyValue;
using Mcm.Company.Application.Features.Company.Queries.GetCompanyById;
using Mcm.Company.Application.Features.Company.Queries.GetCompanyByToken;
using Mcm.Company.Application.Features.Company.Queries.GetCompanyContacts;
using Mcm.Company.Application.Features.Company.Queries.GetSubsidiaries;
using Mcm.Company.Application.Features.Company.Queries.GetSubsidiariesById;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Domain.Enums;
using Mcm.Shared.Infrastructure.Autorisations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mcm.Company.Presentation.Controllers;

[ApiController]
[Route("api/company")]
public class CompanyController(IMediator mediator)
    : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    public async Task<ActionResult<ApiResponse<CreateCompanyResponse>>> Create
        ([FromForm] CreateCompanyRequest body)
    {
        List<CreateCompanyValue>? values = null;
        if (body.Values is not null)
            values = JsonSerializer.Deserialize<List<CreateCompanyValue>>(body.Values, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        var res = await _mediator.Send(new CreateCompanyCommand
        {
            Name = body.Name,
            Acronym = body.Acronym,
            Description = body.Description,
            Logo = body.Logo,
            IsContact = body.IsContact,
            ParentId = body.ParentId,
            TypeContactId = body.TypeContactId,
            ActivitySectors = body.ActivitySectors,
            Values = values
        });
        return Ok(res);
    }

    // [HasAuthorization(PermissionsEnum.View_Company)]  
    [HttpGet("me")]
    public async Task<ActionResult<ApiResponse<GetCompanyByTokenResponse>>> GetCurrent(
        [FromHeader] GetCompanyByTokenRequest header)
    {
        var res = await _mediator.Send(new GetCompanyByTokenQuery(header));
        return Ok(res);
    }


    [HttpGet("{Id}")]
    public async Task<ActionResult<ApiResponse<GetCompanyByIdResponse>>> GetById(        
        Guid Id)
    {
        var res = await _mediator.Send(new GetCompanyByIdQuery(
            new GetCompanyByIdRequest(Id)
        ));
        return Ok(res);
    }

    // [HasAuthorization(PermissionsEnum.Update_Category)]
    [HttpPut("{Id}")]
    public async Task<ActionResult<ApiResponse<UpdateCompanyResponse>>> Update(
        Guid Id, [FromForm] UpdateCompanyRequest body)
    {
        var res = await _mediator.Send(new UpdateCompanyCommand
        {
            Id = Id,
            Name = body.Name,
            Acronym = body.Acronym,
            Description = body.Description,
            Logo = body.Logo,
            ActivitySectors = body.ActivitySectors
        });
        return Ok(res);
    }

    [HttpGet("subsidiary")]
    public async Task<ActionResult<ApiResponse<GetSubsidiariesResponse>>> ViewSubsidiaries
        ([FromQuery] GetSubsidiariesRequest request)
    {
        var res = await _mediator.Send(new GetSubsidiariesQuery(request));
        return Ok(res);
    }

    [HttpGet("{Id}/subsidiary")]
    public async Task<ActionResult<ApiResponse<GetSubsidiariesByIdResponse>>> ViewSubsidiariesById
        (Guid Id, [FromQuery] GetSubsidiariesByIdRequest request)
    {
        var res = await _mediator.Send(new GetSubsidiariesByIdQuery(
            CompanyId: Id,
            Page: request.Page,
            Limit: request.Limit
        ));
        return Ok(res);
    }

    [HttpGet("contacts")]
    public async Task<ActionResult<ApiResponse<GetCompanyContactsResponse>>> ViewCompanyContacts
        ([FromQuery] GetCompanyContactsRequest request)
    {
        var res = await _mediator.Send(new GetCompanyContactsQuery(request));
        return Ok(res);
    }

    [HttpPost("{Id}/convertType")]
    public async Task<ActionResult<ApiResponse<ConvertTypeContactResponse>>> ConvertType(
        Guid Id)
    {
        var res = await _mediator.Send(new ConvertTypeContactCommand
        {
            Id = Id
        });
        return Ok(res);
    }

    // [HasAuthorization(PermissionsEnum.Delete_Company)]
    [HttpDelete("{Id}")]
    public async Task<ActionResult<ApiResponse<DeleteCompanyResponse>>> DeleteCompany   
        (Guid Id, [FromQuery] DeleteCompanyRequest request)
    {
        var res = await _mediator.Send(new DeleteCompanyCommand
        {
            Id = Id,
            Force = request.Force
        });
        return Ok(res);
    }

    [HttpPost("{Id}/values")]
    public async Task<ActionResult<ApiResponse<AddCompanyValueResponse>>> AddCompanyValue(
        Guid Id, [FromBody] AddCompanyValueRequestBody body)
    {
        var res = await _mediator.Send(new AddCompanyValueCommand
        {
            Header = new AddCompanyValueRequestHeader(Id),
            Body = body
        });
        return Ok(res);
    }

    [HttpPut("{Id}/values")]
    public async Task<ActionResult<ApiResponse<UpdateCompanyValueResponse>>> UpdateCompanyValue(
        Guid Id, [FromBody] List<UpdateCompanyValueRequest> body)
    {
        var res = await _mediator.Send(new UpdateCompanyValueCommand
        {
            CompanyId = Id,
            Values = body,
        });
        return Ok(res);
    }

    [HttpPost("{Id}/leader")]
    public async Task<ActionResult<ApiResponse<DefineCompanyLeaderResponse>>> DefineLeader(
        Guid Id, [FromBody] DefineCompanyLeaderRequest body)
    {
        var res = await _mediator.Send(new DefineCompanyLeaderCommand
        {
            Id = Id,
            LeaderId = body.LeaderId
        });
        return Ok(res);
    }
}