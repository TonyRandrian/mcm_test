using Mcm.Shared.Domain.Primitives;

namespace Mcm.Company.Domain.Entities;

public class ActivitySector
    : BaseEntity
{
    public string Name { get; private set; } = string.Empty;

    private ActivitySector(string name)
    {
        Name = name;
    }

    public static ActivitySector Create(string name)
        => new(name);
}