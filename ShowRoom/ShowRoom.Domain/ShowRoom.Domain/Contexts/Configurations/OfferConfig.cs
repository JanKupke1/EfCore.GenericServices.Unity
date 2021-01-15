using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShowRoom.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShowRoom.Domain.Contexts.Configurations
{
    public class OfferConfig : IEntityTypeConfiguration<Offer>
    {
        public void Configure(EntityTypeBuilder<Offer> entity)
        {
            entity.ToTable("Offer");

            entity.HasIndex(e => e.OfferNumber)
                .HasName("OfferNumber_UNIQUE")
                .IsUnique();       

            entity.Property(e => e.OfferNumber)
                .HasColumnName("OfferNumber")
                .IsRequired()
                .HasColumnType("varchar(30)");

            entity.HasOne(d => d.SalesManager)
                .WithMany(p => p.SalesManagerOffers)
                .HasForeignKey(d => d.SalesManagerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
