using DeliverySystem.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverySystem.Data
{
    public class DeliveriesMap
    {
        public DeliveriesMap(EntityTypeBuilder <Delivery> entity)
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.PhoneNumber).IsRequired();
            entity.Property(x => x.StreetName).IsRequired();
            entity.Property(x => x.DeliveryEnd).IsRequired();
            entity.Property(x => x.DeliveryStart).IsRequired();
            entity.Property(x => x.City).IsRequired();
            entity.HasOne(y => y.Category).WithMany(z => z.Delivery).HasForeignKey(q => q.CategoryID);
            entity.HasOne(y => y.Product).WithMany(z => z.Delivery).HasForeignKey(q => q.ProductID);
        }
    }
}
