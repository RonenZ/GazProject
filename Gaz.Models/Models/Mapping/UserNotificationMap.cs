using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Gaz.Models.Models.Mapping
{
    public class UserNotificationMap : EntityTypeConfiguration<UserNotification>
    {
        public UserNotificationMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.NotificationDescription)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("UserNotification");
            this.Property(t => t.ID).HasColumnName("NotificationID");
            this.Property(t => t.NotificationDescription).HasColumnName("NotificationDescription");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.CreateTime).HasColumnName("CreateTime");
            this.Property(t => t.Disabled).HasColumnName("Disabled");

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.UserNotifications)
                .HasForeignKey(d => d.UserID);

        }
    }
}
