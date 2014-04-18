using System.Data.Entity;
using Gaz.Models.Models;
using Gaz.Models.Models.Mapping;

namespace Gaz.DAL.DbContexts
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
