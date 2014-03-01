using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Gaz.Models.Models.Mapping;
using System.Data.Entity.Core.Objects;
using System;
using Gaz.Models.Models;

namespace Gaz.DAL
{
    public partial class GazDBContext : DbContext
    {
        static GazDBContext()
        {
            Database.SetInitializer<GazDBContext>(null);
        }

        public GazDBContext()
            : base("Name=GazDBContext")
        {
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<CounterRead> CounterReads { get; set; }
        public DbSet<Counter> Counters { get; set; }
        public DbSet<UserBill> UserBills { get; set; }
        public DbSet<UserComplaint> UserComplaints { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AddressMap());
            modelBuilder.Configurations.Add(new CounterReadMap());
            modelBuilder.Configurations.Add(new CounterMap());
            modelBuilder.Configurations.Add(new User_Counter_ReferenceMap());
            modelBuilder.Configurations.Add(new UserBillMap());
            modelBuilder.Configurations.Add(new UserComplaintMap());
            modelBuilder.Configurations.Add(new UserNotificationMap());
            modelBuilder.Configurations.Add(new UserRoleMap());
            modelBuilder.Configurations.Add(new UserMap());
        }
    }
}
