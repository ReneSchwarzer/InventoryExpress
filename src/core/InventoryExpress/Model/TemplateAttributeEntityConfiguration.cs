using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryExpress.Model
{
    class TemplateAttributeEntityConfiguration : IEntityTypeConfiguration<TemplateAttribute>
    {
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

            builder.HasOne(d => d.Attribute)
                .WithMany(p => p.TemplateAttributes)
                .HasForeignKey(d => d.AttributeId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(d => d.Template)
                .WithMany(p => p.TemplateAttributes)
                .HasForeignKey(d => d.TemplateId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
