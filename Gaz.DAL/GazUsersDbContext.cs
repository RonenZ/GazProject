using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Gaz.Models.Models.Mapping;
using System.Data.Entity.Core.Objects;
using System;
using Gaz.Models.Models;

namespace Gaz.DAL
{
    public partial class GazUsersDbContext : DbContext
    {
        static GazUsersDbContext()
        {
            Database.SetInitializer<GazUsersDbContext>(null);
        }

        public GazUsersDbContext()
            : base("Name=GazUsersDbContext")
        {
        }

        public DbSet<UserBill> UserBills { get; set; }
        public DbSet<UserComplaint> UserComplaints { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserBillMap());
            modelBuilder.Configurations.Add(new UserComplaintMap());
            modelBuilder.Configurations.Add(new UserNotificationMap());
            modelBuilder.Configurations.Add(new UserRoleMap());
            modelBuilder.Configurations.Add(new UserMap());
        }
    }
}
