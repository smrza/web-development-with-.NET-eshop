using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Models.Database.Configuration
{
    public class OrderConfiguration : EntityConfiguration, IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {

            base.Configure(builder);

            /*
            builder.HasMany(order => order.OrderItems)
                .WithOne(orderItem => orderItem.Order)
                .IsRequired()
                .HasForeignKey(orderItem => orderItem.OrderID)
                .OnDelete(DeleteBehavior.Restrict);
            */
        }
    }
}
