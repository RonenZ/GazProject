using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Gaz.Models.Models.Mapping;
using System.Data.Entity.Core.Objects;
using System;

namespace Gaz.Models.Models
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
        public DbSet<User_Counter> User_Counter_Reference { get; set; }
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

        public virtual int usp_GetBillForCounter(Nullable<int> counterID)
        {
            var counterIDParameter = counterID.HasValue ?
                new ObjectParameter("counterID", counterID) :
                new ObjectParameter("counterID", typeof(int));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_GetBillForCounter", counterIDParameter);
        }

        public virtual int usp_GetCounterReadPerPeriod(Nullable<int> counterID, Nullable<System.DateTime> startTime, Nullable<System.DateTime> endTime)
        {
            var counterIDParameter = counterID.HasValue ?
                new ObjectParameter("CounterID", counterID) :
                new ObjectParameter("CounterID", typeof(int));

            var startTimeParameter = startTime.HasValue ?
                new ObjectParameter("StartTime", startTime) :
                new ObjectParameter("StartTime", typeof(System.DateTime));

            var endTimeParameter = endTime.HasValue ?
                new ObjectParameter("EndTime", endTime) :
                new ObjectParameter("EndTime", typeof(System.DateTime));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_GetCounterReadPerPeriod", counterIDParameter, startTimeParameter, endTimeParameter);
        }

        public virtual ObjectResult<Counter> usp_GetCountersForUser(Nullable<int> usrID)
        {
            var usrIDParameter = usrID.HasValue ?
                new ObjectParameter("usrID", usrID) :
                new ObjectParameter("usrID", typeof(int));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Counter>("usp_GetCountersForUser", usrIDParameter);
        }

        public virtual int usp_GetLastBillForCounter(Nullable<int> counterID)
        {
            var counterIDParameter = counterID.HasValue ?
                new ObjectParameter("counterID", counterID) :
                new ObjectParameter("counterID", typeof(int));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_GetLastBillForCounter", counterIDParameter);
        }

        public virtual int usp_GetOpenBillsForCounter(Nullable<int> counterID)
        {
            var counterIDParameter = counterID.HasValue ?
                new ObjectParameter("counterID", counterID) :
                new ObjectParameter("counterID", typeof(int));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_GetOpenBillsForCounter", counterIDParameter);
        }

        public virtual int usp_GetUserDetails(Nullable<int> userID)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("userID", userID) :
                new ObjectParameter("userID", typeof(int));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_GetUserDetails", userIDParameter);
        }

        public virtual int usp_LoginUser(string userName, string passWord)
        {
            var userNameParameter = userName != null ?
                new ObjectParameter("userName", userName) :
                new ObjectParameter("userName", typeof(string));

            var passWordParameter = passWord != null ?
                new ObjectParameter("passWord", passWord) :
                new ObjectParameter("passWord", typeof(string));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_LoginUser", userNameParameter, passWordParameter);
        }
    }
}
