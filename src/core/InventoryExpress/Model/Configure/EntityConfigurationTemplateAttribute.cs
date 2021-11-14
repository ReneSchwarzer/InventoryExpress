using InventoryExpress.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryExpress.Model.Configure
{
    /// <summary>
    /// Datenbankkonfiguration der Vorlagen-Attribut-Entität
    /// </summary>
    class EntityConfigurationTemplateAttribute : IEntityTypeConfiguration<TemplateAttribute>
    {
        /// <summary>
        /// Konfiguration
        /// </summary>
        /// <param name="builder">Der Builder</param>
        public void Configure(EntityTypeBuilder<TemplateAttribute> builder)
        {
            builder.HasKey(e => new { e.TemplateId, e.AttributeId });

            builder.ToTable("TemplateAttribute");

            builder.Property(e => e.TemplateId).HasColumnName("TemplateID");

            builder.Property(e => e.AttributeId).HasColumnName("AttributeID");

            builder.Property(e => e.Created)
                .HasColumnName("Created")
                .IsRequired()
                .HasColumnType("TIMESTAMP")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            // Beziehungen
            builder.HasOne(d => d.Attribute)
                .WithMany(p => p.TemplateAttributes)
                .HasForeignKey(d => d.AttributeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(d => d.Template)
                .WithMany(p => p.TemplateAttributes)
                .HasForeignKey(d => d.TemplateId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
