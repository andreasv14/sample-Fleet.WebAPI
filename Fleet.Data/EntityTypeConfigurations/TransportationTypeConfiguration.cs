using Fleet.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fleet.Data.EntityTypeConfigurations
{
    public class TransportationTypeConfiguration : IEntityTypeConfiguration<Transportation>
    {
        public void Configure(EntityTypeBuilder<Transportation> transportationBuilder)
        {
            transportationBuilder.ToTable("Transportations");
            
            transportationBuilder.HasKey(x => x.Id);
            
            transportationBuilder.HasIndex(x => x.Name).IsUnique();
            
            transportationBuilder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            transportationBuilder.Property(x => x.Capacity).IsRequired();
            transportationBuilder.Property(x => x.Type).IsRequired();
        }
    }
}
