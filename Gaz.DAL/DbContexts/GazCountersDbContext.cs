using System.Data.Entity;
using Gaz.Models.Models;
using Gaz.Models.Models.Mapping;

namespace Gaz.DAL.DbContexts
{
    public partial class GazCountersDbContext : DbContext
    {
        static GazCountersDbContext()
        {
            Database.SetInitializer<GazCountersDbContext>(null);
        }

        public GazCountersDbContext()
            : base("Name=GazCountersDbContext")
        {
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<CounterRead> CounterReads { get; set; }
        public DbSet<Counter> Counters { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AddressMap());
            modelBuilder.Configurations.Add(new CounterReadMap());
            modelBuilder.Configurations.Add(new CounterMap());
            modelBuilder.Configurations.Add(new UserMap());
        }
    }
}
