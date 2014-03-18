using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Gaz.Models.Models.Mapping
{
    public class CounterMap : EntityTypeConfiguration<Counter>
    {
        public CounterMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            this.ToTable("Counters");
            this.Property(t => t.ID).HasColumnName("CounterID");
            this.Property(t => t.AddressID).HasColumnName("AddressID");

            // Relationships
            this.HasRequired(t => t.Address)
                .WithMany(t => t.Counters)
                .HasForeignKey(d => d.AddressID);
        }
    }
}
