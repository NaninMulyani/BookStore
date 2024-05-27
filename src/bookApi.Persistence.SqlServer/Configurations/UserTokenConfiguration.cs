using bookApi.Domain.Entities;
using bookApi.Shared.Abstractions.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace bookApi.Persistence.SqlServer.Configurations;

public class UserTokenConfiguration : BaseEntityConfiguration<UserToken>
{
    protected override void EntityConfiguration(EntityTypeBuilder<UserToken> builder)
    {
        builder.HasKey(e => e.UserTokenId);
        builder.Property(e => e.UserTokenId).ValueGeneratedNever();
        builder.Property(e => e.RefreshToken).HasColumnType("varchar")
            .HasMaxLength(256);
    }
}