using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShowRoom.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShowRoom.Domain.Contexts.Configurations
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> entity)
        {
            entity.ToTable("Order");

            entity.HasIndex(e => e.OrderNumber)
              .HasName("OrderNumber_UNIQUE")
              .IsUnique();

            entity.Property(e => e.OrderNumber)
          .HasColumnName("OrderNumber")
          .HasColumnType("varchar(30)");

            entity.Property(p => p.Comment)
               .HasMaxLength(2000).HasColumnType("varchar(2000)");


            entity.HasOne(d => d.ProjectManager)
           .WithMany(p => p.ProjectManagerOrders)
           .HasForeignKey(d => d.ProjectManagerId)
           .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.LabProjectManager)
.WithMany(p => p.LabProjectManagerOrders)
.HasForeignKey(d => d.LabProjectManagerId)
.OnDelete(DeleteBehavior.Cascade);
        }
    }
}
