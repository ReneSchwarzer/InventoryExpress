using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryExpress.Model
{
    /// <summary>
    /// Datenbankkonfiguration der Many-to-Many-Relation zwischen der Ivnentar- und der Attribut-Entität
    /// </summary>
    class InventoryAttributeEntityConfiguration : IEntityTypeConfiguration<InventoryAttribute>
    {
        public void Configure(EntityTypeBuilder<InventoryAttribute> builder)
        {
            builder.ToTable("InventoryAttribute");
            builder.HasKey(e => new { e.InventoryId, e.AttributeId });

            builder.Property(e => e.InventoryId)
                   .HasColumnName("InventoryID");

            builder.Property(e => e.AttributeId)
                   .HasColumnName("AttributeID");

            builder.Property(e => e.Value)
                   .HasColumnName("Value")
                   .IsRequired()
                   .HasColumnType("TEXT");

            builder.Property(e => e.Created)
                   .HasColumnName("Created")
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
