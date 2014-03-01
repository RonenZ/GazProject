using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Gaz.Models.Models.Mapping
{
    public class User_Counter_ReferenceMap : EntityTypeConfiguration<User_Counter>
    {
        public User_Counter_ReferenceMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CounterID, t.UserID });

            // Properties
            this.Property(t => t.CounterID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.UserID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("User_Counter_Reference");
            this.Property(t => t.CounterID).HasColumnName("CounterID");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.CreateTime).HasColumnName("CreateTime");

            // Relationships
            this.HasRequired(t => t.Counter)
                .WithMany(t => t.User_Counter_Reference)
                .HasForeignKey(d => d.CounterID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.User_Counter_Reference)
                .HasForeignKey(d => d.UserID);

        }
    }
}
