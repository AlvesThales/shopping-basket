using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ShoppingBasket.Domain.Entities;

namespace ShoppingBasket.Infrastructure.Persistence.Mappings
{

    class BasketMap : IEntityTypeConfiguration<Basket>
    {
        public void Configure(EntityTypeBuilder<Basket> builder)
        {
            //builder.HasOne(o => o.Customer)
            //    .WithMany(c => c.Orders)
            //    .HasForeignKey(o => o.CustomerId)
            //    .OnDelete(DeleteBehavior.Restrict)
            //    .IsRequired();

            builder.Property(c => c.IsPaid)
                .IsRequired();

            builder.Property(c => c.IsDeleted)
                .IsRequired();
        }
    }
}
