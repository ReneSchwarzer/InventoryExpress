using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryExpress.Model
{
    /// <summary>
    /// Datenbankkonfiguration der Standort-Entität
    /// </summary>
    class LocationEntityConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.ToTable("Location");
            builder.HasKey(key => new { key.Id });

            //entity.HasIndex(e => e.Guid, "IX_Location_Guid")
            //    .IsUnique();

            //entity.HasIndex(e => e.Name, "IX_Location_Name")
            //    .IsUnique();

            builder.Property(e => e.Id)
                   .HasColumnName("ID");

            builder.Property(e => e.MediaId)
                   .HasColumnName("MediaID");

            builder.Property(e => e.Name)
                   .HasColumnName("Name")
                   .IsRequired()
                   .HasColumnType("VARCHAR (64)");

            builder.Property(e => e.Address)
                   .HasColumnName("Address")
                   .HasColumnType("VARCHAR (256)");

            builder.Property(e => e.Building)
                   .HasColumnName("Building")
                   .HasColumnType("VARCHAR (64)");

            builder.Property(e => e.Room)
                   .HasColumnName("Room")
                   .HasColumnType("VARCHAR (64)");

            builder.Property(e => e.Zip)
                   .HasColumnName("Zip")
                   .HasColumnType("VARCHAR (10)");

            builder.Property(e => e.Place)
                   .HasColumnName("Place")
                   .HasColumnType("VARCHAR (64)");

            builder.Property(e => e.Tag)
                   .HasColumnName("Tag")
                   .HasColumnType("VARCHAR (256)");

            builder.Property(e => e.Created)
                   .HasColumnName("Created")
                   .IsRequired()
                   .HasColumnType("TIMESTAMP")
                   .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.Updated)
                   .HasColumnName("Updated")
                   .IsRequired()
                   .HasColumnType("TIMESTAMP")
                   .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.Guid)
                   .HasColumnName("Guid")
                   .IsRequired()
                   .HasColumnType("CHAR (36)");

            builder.HasOne(d => d.Media)
                   .WithMany(p => p.Locations)
                   .HasForeignKey(d => d.MediaId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(d => d.Media)
                .WithMany(p => p.Locations)
                .HasForeignKey(d => d.MediaId);
        }
    }
}
