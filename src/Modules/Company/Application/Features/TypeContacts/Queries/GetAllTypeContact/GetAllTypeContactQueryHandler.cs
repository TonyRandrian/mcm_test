using System.Drawing;
using Mcm.Company.Application.Interfaces;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Interfaces;
using MediatR;

namespace Mcm.Company.Application.Features.TypeContacts.Queries.GetAllTypeContact
{
    public class GetAllTypeContactQueryHandler(ITypeContactRepository typeContactRepository)
        : IRequestHandler<GetAllTypeContactQuery, ApiResponse<GetAllTypeContactResponse>>
    {
        private readonly ITypeContactRepository _typeContactRepository = typeContactRepository;
       
        public async Task<ApiResponse<GetAllTypeContactResponse>> Handle(GetAllTypeContactQuery query, CancellationToken cancellationToken)
        {
            var typeContacts = await _typeContactRepository.GetAllAsync(
                predicate: p => (
                    (string.IsNullOrEmpty(query.Request.SearchName) || p.Name.Value.Contains(query.Request.SearchName, StringComparison.CurrentCultureIgnoreCase))),
                orderBy: p => p.OrderBy(t => t.Name.Value),
                pageQuery: new PageQuery(query.Request.Page, query.Request.Limit));
            
            
            var data = typeContacts.Select(res => new TypeContactResponse
            {
                Id = res.Id,
                Name = res.Name,
                Description = res.Description,
                Color = res.Color
            }).ToList();

            return new ApiResponse<GetAllTypeContactResponse>
            {
                Success = true,
                Message = "Type contacts retrieved successfully",
                Code = 200,
                Data = new GetAllTypeContactResponse(data),
                Meta = new Meta
                {
                    Page = query.Request.Page,
                    Limit = query.Request.Limit,
                    Total = await _typeContactRepository.CountAsync()
                }
            };
        }
    }
}