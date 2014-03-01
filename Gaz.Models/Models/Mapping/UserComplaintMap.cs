using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Gaz.Models.Models.Mapping
{
    public class UserComplaintMap : EntityTypeConfiguration<UserComplaint>
    {
        public UserComplaintMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ComplaintDescription)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("UserComplaints");
            this.Property(t => t.ID).HasColumnName("ComplaintID");
            this.Property(t => t.ComplaintDescription).HasColumnName("ComplaintDescription");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.CounterID).HasColumnName("CounterID");
            this.Property(t => t.CreateTime).HasColumnName("CreateTime");
            this.Property(t => t.Disable).HasColumnName("Disable");

            // Relationships
            this.HasRequired(t => t.Counter)
                .WithMany(t => t.UserComplaints)
                .HasForeignKey(d => d.CounterID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.UserComplaints)
                .HasForeignKey(d => d.UserID);

        }
    }
}
