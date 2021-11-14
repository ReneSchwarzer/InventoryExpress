using InventoryExpress.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryExpress.Model.Configure
{
    /// <summary>
    /// Datenbankkonfiguration der Anlage-Entität
    /// </summary>
    class EntityConfigurationInventoryAttachment : IEntityTypeConfiguration<InventoryAttachment>
    {
        /// <summary>
        /// Konfiguration
        /// </summary>
        /// <param name="builder">Der Builder</param>
        public void Configure(EntityTypeBuilder<InventoryAttachment> builder)
        {
            builder.HasKey(e => new { e.InventoryId, e.MediaId });

            builder.ToTable("InventoryAttachment");

            builder.Property(e => e.InventoryId).HasColumnName("InventoryID");

            builder.Property(e => e.MediaId).HasColumnName("MediaID");

            builder.Property(e => e.Created)
                .IsRequired()
                .HasColumnType("TIMESTAMP")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            // Beziehungen
            builder.HasOne(d => d.Media)
                .WithMany(p => p.InventoryAttachment)
                .HasForeignKey(d => d.MediaId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(d => d.Inventory)
                .WithMany(p => p.InventoryMedia)
                .HasForeignKey(d => d.InventoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
