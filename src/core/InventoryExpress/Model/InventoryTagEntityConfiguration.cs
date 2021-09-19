using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryExpress.Model
{
    /// <summary>
    /// Datenbankkonfiguration der Many-to-Many-Relation zwischen der Ivnentar- und der Tag-Entität
    /// </summary>
    class InventoryTagEntityConfiguration : IEntityTypeConfiguration<InventoryTag>
    {
        public void Configure(EntityTypeBuilder<InventoryTag> builder)
        {
            builder.ToTable("InventoryTag");
            builder.HasKey(e => new { e.InventoryId, e.TagId });

            builder.Property(e => e.InventoryId)
                   .HasColumnName("InventoryID");

            builder.Property(e => e.TagId)
                   .HasColumnName("TagID");

            builder.HasOne(d => d.Inventory)
                   .WithMany(p => p.InventoryTag)
                   .HasForeignKey(d => d.InventoryId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(d => d.Tag)
                   .WithMany(p => p.InventoryTag)
                   .HasForeignKey(d => d.TagId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
