using Fleet.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fleet.Data.EntityTypeConfigurations
{
    public class ContainerTypeConfiguration : IEntityTypeConfiguration<Container>
    {
        public void Configure(EntityTypeBuilder<Container> containerBuilder)
        {
            containerBuilder.ToTable("Containers");
            
            containerBuilder.HasKey(x => x.Id);
            
            containerBuilder.HasIndex(x => x.Name).IsUnique();

            containerBuilder.Property(x => x.Name).IsRequired().HasMaxLength(50);
        }
    }
}
