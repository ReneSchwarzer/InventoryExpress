using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryExpress.Model
{
    /// <summary>
    /// Datenbankkonfiguration der Schlüsselwort-Entität
    /// </summary>
    class TagEntityConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.ToTable("Tag");
            builder.HasKey(key => new { key.Id });

            builder.Property(e => e.Id)
                   .HasColumnName("ID");

            builder.Property(e => e.Label)
                   .HasColumnName("Label")
                   .IsRequired()
                   .HasColumnType("VARCHAR (64)");
        }
    }
}
