using System.Data.Entity;
using Gaz.Models.Models;
using Gaz.Models.Models.Mapping;

namespace Gaz.DAL.DbContexts
{
    public partial class GazBillingDbContext : DbContext
    {
        static GazBillingDbContext()
        {
            Database.SetInitializer<GazBillingDbContext>(null);
        }

        public GazBillingDbContext()
            : base("Name=GazBillingDbContext")
        {
        }

        public DbSet<Counter> Counters { get; set; }
        public DbSet<UserBill> UserBills { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CounterMap());
            modelBuilder.Configurations.Add(new UserBillMap());
            modelBuilder.Configurations.Add(new UserMap());
        }
    }
}
