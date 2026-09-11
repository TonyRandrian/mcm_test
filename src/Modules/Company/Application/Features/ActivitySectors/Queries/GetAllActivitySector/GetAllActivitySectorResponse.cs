namespace Mcm.Company.Application.Features.ActivitySectors.Queries.GetAllActivitySector
{
    public record GetAllActivitySectorResponse
    (
        List<ActivitySectorDto> ActivitySectors
    );

    public record ActivitySectorDto
    (
        Guid Id,
        string Name
    );
}