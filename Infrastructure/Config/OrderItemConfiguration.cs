using Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Config;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<OrderItem> builder)
    {
        builder.OwnsOne(x => x.ItemOrdered, o => o.WithOwner());
        builder.Property(x => x.Price).HasColumnType("decimal(18,2)");
    }
}

