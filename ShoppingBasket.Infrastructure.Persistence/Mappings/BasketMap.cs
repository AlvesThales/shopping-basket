using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ShoppingBasket.Domain.Entities;

namespace ShoppingBasket.Infrastructure.Persistence.Mappings
{

    class BasketMap : IEntityTypeConfiguration<Basket>
    {
        public void Configure(EntityTypeBuilder<Basket> builder)
        {
            builder.Property(c => c.IsPaid)
                .IsRequired();

            builder.Property(c => c.IsDeleted)
                .IsRequired();
        }
    }
}
