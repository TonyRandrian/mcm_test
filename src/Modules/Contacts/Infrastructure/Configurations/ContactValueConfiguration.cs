using Mcm.Contacts.Domain.Entities;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mcm.Contacts.Infrastructure.Configurations;

public class ContactValueConfiguration : IEntityTypeConfiguration<ContactValue>
{
    public void Configure(EntityTypeBuilder<ContactValue> builder)
    {
        builder.ToTable("contact_values");

    }
}