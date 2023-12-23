using InventoryExpress.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryExpress.Model.Configure
{
    /// <summary>
    /// Database configuration of the location entity.
    /// </summary>
    internal class EntityConfigurationLocation : IEntityTypeConfiguration<Location>
    {
        /// <summary>
        /// Configuration of the location entity.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.ToTable("Location");
            builder.HasKey(key => new { key.Id });

            builder.Property(e => e.Id)
                   .HasColumnName("Id");

            builder.Property(e => e.MediaId)
                   .HasColumnName("MediaId");

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

            // unique contraints
            builder.HasIndex(e => e.Name)
                   .IsUnique();

            builder.HasIndex(e => e.Guid)
                   .IsUnique();

            // relations
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
