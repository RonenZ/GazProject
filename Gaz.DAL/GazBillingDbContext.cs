using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Gaz.Models.Models.Mapping;
using System.Data.Entity.Core.Objects;
using System;
using Gaz.Models.Models;

namespace Gaz.DAL
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
