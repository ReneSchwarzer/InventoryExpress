using InventoryExpress.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryExpress.Model.Configure
{
    /// <summary>
    /// Datenbankkonfiguration der Kommentar-Entität
    /// </summary>
    class InventoryCommentMediaEntityConfiguration : IEntityTypeConfiguration<InventoryComment>
    {
        /// <summary>
        /// Konfiguration
        /// </summary>
        /// <param name="builder">Der Builder</param>
        public void Configure(EntityTypeBuilder<InventoryComment> builder)
        {
            builder.HasKey(e => new { e.Id });

            builder.ToTable("InventoryComment");

            builder.Property(e => e.InventoryId).HasColumnName("InventoryId");

            builder.Property(e => e.Created)
                .IsRequired()
                .HasColumnType("TIMESTAMP")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.Updated)
                .IsRequired()
                .HasColumnType("TIMESTAMP")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.Guid)
               .IsRequired()
               .HasColumnType("CHAR (36)");

            builder.HasOne(d => d.Inventory)
                .WithMany(p => p.InventoryComments)
                .HasForeignKey(d => d.InventoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
