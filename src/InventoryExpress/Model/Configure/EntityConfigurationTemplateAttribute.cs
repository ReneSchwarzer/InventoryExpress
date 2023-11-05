using InventoryExpress.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryExpress.Model.Configure
{
    /// <summary>
    /// Database configuration of the template attribute entity.
    /// </summary>
    class EntityConfigurationTemplateAttribute : IEntityTypeConfiguration<TemplateAttribute>
    {
        /// <summary>
        /// Configuration of the template attribute entity.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public void Configure(EntityTypeBuilder<TemplateAttribute> builder)
        {
            builder.HasKey(e => new { e.TemplateId, e.AttributeId });

            builder.ToTable("TemplateAttribute");

            builder.Property(e => e.TemplateId).HasColumnName("TemplateId");

            builder.Property(e => e.AttributeId).HasColumnName("AttributeId");

            builder.Property(e => e.Created)
                .HasColumnName("Created")
                .IsRequired()
                .HasColumnType("TIMESTAMP")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            // relations
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
