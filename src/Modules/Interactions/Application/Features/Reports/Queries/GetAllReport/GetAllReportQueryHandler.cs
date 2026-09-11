// using System.Text.Json;
// using Mcm.Interactions.Application.Features.InteractionTypes.Queries.GetAllInteractionType;
// using Mcm.Interactions.Application.Interfaces;
// using Mcm.Shared.Application.Common;
// using Mcm.Shared.Application.Interfaces;
// using Mcm.Shared.Application.Modules;
// using Mcm.Shared.Application.Modules.DTOs;
// using MediatR;

// namespace Mcm.Interactions.Application.Features.TypeFields.Queries.GetAllTypeField
// {
//     public class GetAllTypeFieldQueryHandler(
//         ITypeFieldRepository fieldRepository)
//         : IRequestHandler<GetAllTypeFieldQuery, ApiResponse<List<GetAllTypeFieldResponse>>>
//     {
//         private readonly ITypeFieldRepository _typeFieldRepository = fieldRepository;
        
//         public async Task<ApiResponse<List<GetAllTypeFieldResponse>>> Handle(
//             GetAllTypeFieldQuery query, CancellationToken cancellationToken)
//         {
//             var fields = await _typeFieldRepository.GetAllAsync(
//                 predicate: c =>
//                     (query.Request.InteractionTypeId == null
//                         || query.Request.InteractionTypeId == c.InteractionTypeId) &&
//                     (string.IsNullOrWhiteSpace(query.Request.Search)
//                         || c.Name.Value.ToLower().Contains(query.Request.Search.ToLower())),
//                 orderBy: q => q.OrderByDescending(c => c.CreatedAt),
//                 pageQuery: new PageQuery(query.Request.Page, query.Request.Limit),
//                 ct: cancellationToken);

//             return new ApiResponse<List<GetAllTypeFieldResponse>>
//             {
//                 Success = true,
//                 Message = "InteractionTypes retrieved successfully",
//                 Code = 200,
//                 Data = fields.Select(field =>
//                     new GetAllTypeFieldResponse{
//                         Id = field.Id,
//                         Name = field.Name,
//                         Type = field.Type.ToString()
//                     }).ToList(),
//                 Meta = new Meta
//                 {
//                     Page  = query.Request.Page,
//                     Limit = query.Request.Limit,
//                     Total = await _typeFieldRepository.CountAsync()
//                 }
//             };
//         }
//     }
// }