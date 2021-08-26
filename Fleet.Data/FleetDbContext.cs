using Fleet.Data.EntityTypeConfigurations;
using Fleet.Domain;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace Fleet.Data
{
    public class FleetDbContext : DbContext
    {
        #region DbSets

        public DbSet<Container> Containers { get; set; }
        public DbSet<Transportation> Transportations { get; set; }
        public DbSet<Domain.Transaction> Transactions { get; set; }

        #endregion

        #region Constructors

        public FleetDbContext(DbContextOptions options) : base(options)
        {
        }

        #endregion

        #region Protected methods

        protected override void OnModelCreating(ModelBuilder builder)
        {
            #region Entity type configurations

            builder.ApplyConfiguration(new ContainerTypeConfiguration());
            builder.ApplyConfiguration(new TransportationTypeConfiguration());
            builder.ApplyConfiguration(new TransactionTypeConfiguration());

            #endregion

            #region Navigate relations

            builder.Entity<Container>()
                .HasOne(x => x.Transportation)
                .WithMany(x => x.LoadContainers)
                .HasForeignKey(x => x.TransportationId)
                .OnDelete(DeleteBehavior.NoAction);

            #endregion

            base.OnModelCreating(builder);
        }

        #endregion
    }
}
