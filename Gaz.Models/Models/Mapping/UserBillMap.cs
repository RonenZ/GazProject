using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Gaz.Models.Models.Mapping
{
    public class UserBillMap : EntityTypeConfiguration<UserBill>
    {
        public UserBillMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            this.ToTable("UserBills");
            this.Property(t => t.ID).HasColumnName("BillID");
            this.Property(t => t.BillAmount).HasColumnName("BillAmount");
            this.Property(t => t.CreateTime).HasColumnName("CreateTime");
            this.Property(t => t.CounterID).HasColumnName("CounterID");
            this.Property(t => t.Payed).HasColumnName("Payed");

            // Relationships
            this.HasRequired(t => t.Counter)
                .WithMany(t => t.UserBills)
                .HasForeignKey(d => d.CounterID);

        }
    }
}
