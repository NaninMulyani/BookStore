using bookApi.Domain.Entities;
using bookApi.Shared.Abstractions.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace bookApi.Persistence.Postgres.Configurations;

public class CartConfiguration : BaseEntityConfiguration<Cart>
{
    protected override void EntityConfiguration(EntityTypeBuilder<Cart> builder)
    {
        builder.HasKey(e => e.CartId);
        builder.Property(e => e.CartId).ValueGeneratedNever();
        builder.Property(e => e.Price).HasColumnType("decimal");
        builder.Property(e => e.Qty).HasColumnType("int");
        builder.Property(e => e.TotalPrice).HasColumnType("decimal");
    }
}