using InventoryExpress.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryExpress.Model.Configure
{
    /// <summary>
    /// Database configuration of the many-to-many relation between the ivnentar and tag entities.
    /// </summary>
    class EntityConfigurationInventoryTag : IEntityTypeConfiguration<InventoryTag>
    {
        /// <summary>
        /// Configuration of the relation.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public void Configure(EntityTypeBuilder<InventoryTag> builder)
        {
            builder.ToTable("InventoryTag");
            builder.HasKey(e => new { e.InventoryId, e.TagId });

            builder.Property(e => e.InventoryId)
                   .HasColumnName("InventoryId");

            builder.Property(e => e.TagId)
                   .HasColumnName("TagId");

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
