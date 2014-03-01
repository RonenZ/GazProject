using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Gaz.Models.Models.Mapping
{
    public class AddressMap : EntityTypeConfiguration<Address>
    {
        public AddressMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.CityName)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.StreetName)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("Addresses");
            this.Property(t => t.ID).HasColumnName("AddressID");
            this.Property(t => t.CityName).HasColumnName("CityName");
            this.Property(t => t.StreetName).HasColumnName("StreetName");
            this.Property(t => t.HouseNumber).HasColumnName("HouseNumber");
            this.Property(t => t.ApartmentNumber).HasColumnName("ApartmentNumber");
            this.Property(t => t.latitude).HasColumnName("latitude");
            this.Property(t => t.longitude).HasColumnName("longitude");
        }
    }
}
