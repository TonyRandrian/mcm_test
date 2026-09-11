using Mcm.Authorizations.Application.Interfaces;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Domain.Enums;
using Mcm.Shared.Domain.ValueObjects;
using MediatR;

namespace Mcm.Authorizations.Application.Features.TeamMembers.Commands.UploadPhoto
{
    public class UploadPhotoCommandHandler(ITeamMemberRepository teamMemberRepository, IResourceService fileService, IAuthorizationUow uow)
        : IRequestHandler<UploadPhotoCommand, ApiResponse<UploadPhotoResponse>>
    {
        private readonly ITeamMemberRepository _teamMemberRepository = teamMemberRepository;
        private readonly IResourceService _fileService = fileService;
        private readonly IAuthorizationUow _uow = uow;

        public async Task<ApiResponse<UploadPhotoResponse>> Handle(UploadPhotoCommand command, CancellationToken ct)
        {
            var teamMember = await _teamMemberRepository.GetByIdAsync(command.Id)
                ?? throw NotFoundException.NotFoundById(nameof(Authorizations.Domain.Entities.TeamMember), command.Id); 

            if (teamMember.Image is not null)
                _fileService.DeleteResource(teamMember.Image);
            var image = await _fileService.SaveResource(command.Image);
            teamMember.SetImage(image);

            _teamMemberRepository.Update(teamMember);
            await _uow.SaveChangesAsync(ct);

            return new ApiResponse<UploadPhotoResponse>
            {
                Success = true,
                Message = "POST CreateTeamMember",
                Code = 200,
                Data = new UploadPhotoResponse{Image = image}
            };
        }
    }
}