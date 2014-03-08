using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Gaz.Models.Models.Mapping;
using System.Data.Entity.Core.Objects;
using System;
using Gaz.Models.Models;

namespace Gaz.DAL
{
    public partial class GazSimpleUsersDbContext : DbContext
    {
        static GazSimpleUsersDbContext()
        {
            Database.SetInitializer<GazUsersDbContext>(null);
        }

        public GazSimpleUsersDbContext()
            : base("Name=GazDBContext")
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserMap());
        }
    }
}
