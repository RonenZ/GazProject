using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Gaz.Models.Models.Mapping
{
    public class CounterReadMap : EntityTypeConfiguration<CounterRead>
    {
        public CounterReadMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            this.ToTable("CounterReads");
            this.Property(t => t.ID).HasColumnName("ReadID");
            this.Property(t => t.CounterID).HasColumnName("CounterID");
            this.Property(t => t.CreateTime).HasColumnName("CreateTime");
            this.Property(t => t.ReadAmount).HasColumnName("ReadAmount");

            // Relationships
            this.HasRequired(t => t.Counter)
                .WithMany(t => t.CounterReads)
                .HasForeignKey(d => d.CounterID);

        }
    }
}
