﻿using bookApi.Domain.Entities;
using bookApi.Shared.Abstractions.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace bookApi.Persistence.Postgres.Configurations;

public class RoleScopeConfiguration : BaseEntityConfiguration<RoleScope>
{
    protected override void EntityConfiguration(EntityTypeBuilder<RoleScope> builder)
    {
        builder.HasKey(e => e.RoleScopeId);
        builder.Property(e => e.RoleScopeId).ValueGeneratedNever();
        builder.Property(e => e.Name).HasMaxLength(256);
        builder.Property(e => e.Description).HasMaxLength(2048);
        builder.Property(e => e.RevokedMessage).HasMaxLength(2048);
    }
}