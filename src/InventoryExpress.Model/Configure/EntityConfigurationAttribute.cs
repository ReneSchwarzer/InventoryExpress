using InventoryExpress.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryExpress.Model.Configure
{
    /// <summary>
    /// Database configuration of the attribute entity.
    /// </summary>
    internal class EntityConfigurationAttribute : IEntityTypeConfiguration<Attribute>
    {
        /// <summary>
        /// Configuration of the attribute entity.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public void Configure(EntityTypeBuilder<Attribute> builder)
        {
            builder.ToTable("Attribute");

            builder.HasKey(key => new { key.Id });

            builder.Property(e => e.Id)
                   .HasColumnName("Id");

            builder.Property(e => e.MediaId)
                   .HasColumnName("MediaId");

            builder.Property(e => e.Name)
                   .HasColumnName("Name")
                   .IsRequired()
                   .HasColumnType("VARCHAR(64)");

            builder.Property(e => e.Description)
                   .HasColumnName("Description")
                   .HasColumnType("TEXT");

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
                   .HasColumnType("CHAR(36)");

            // unique contraints
            builder.HasIndex(e => e.Name)
                   .IsUnique();

            builder.HasIndex(e => e.Guid)
                   .IsUnique();

            // relations
            builder.HasOne(d => d.Media)
                   .WithMany(p => p.Attributes)
                   .HasForeignKey(d => d.MediaId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
