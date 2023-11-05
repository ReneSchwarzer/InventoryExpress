using InventoryExpress.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryExpress.Model.Configure
{
    /// <summary>
    /// Database configuration of the many-to-many relation between the ivnentar and attribute entities.
    /// </summary>
    class EntityConfigurationInventoryAttribute : IEntityTypeConfiguration<InventoryAttribute>
    {
        /// <summary>
        /// Configuration of the relation.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public void Configure(EntityTypeBuilder<InventoryAttribute> builder)
        {
            builder.ToTable("InventoryAttribute");
            builder.HasKey(e => new { e.InventoryId, e.AttributeId });

            builder.Property(e => e.InventoryId)
                   .HasColumnName("InventoryId");

            builder.Property(e => e.AttributeId)
                   .HasColumnName("AttributeId");

            builder.Property(e => e.Value)
                   .HasColumnName("Value")
                   .IsRequired()
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

            builder.HasOne(d => d.Attribute)
                   .WithMany(p => p.InventoryAttributes)
                   .HasForeignKey(d => d.AttributeId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(d => d.Inventory)
                   .WithMany(p => p.InventoryAttributes)
                   .HasForeignKey(d => d.InventoryId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
