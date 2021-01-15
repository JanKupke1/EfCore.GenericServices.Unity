using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShowRoom.Domain.Entities;

namespace ShowRoom.Domain.Contexts.Configurations
{
    public class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> entity)
        {
            entity.ToTable("Employee");

            entity.Property(e => e.IsActive)
                .HasColumnName("IsActive")
                .HasDefaultValueSql("'0'");

         

            entity.Property(e => e.Email)
                .IsRequired()
                .HasColumnName("EMail")
                .HasColumnType("varchar(150)");

            entity.Property(e => e.LastName)
                .HasColumnName("Nachname")
                .IsRequired()
                .HasColumnType("varchar(30)");            

            entity.Property(e => e.UserName)
                .IsRequired()
                .HasColumnType("varchar(31)");

            entity.Property(e => e.FirstName)
                .HasColumnName("Vorname")
                .IsRequired()
                .HasColumnType("varchar(30)");

         
        }
    }
}
