using InventoryExpress.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryExpress.Model.Configure
{
    /// <summary>
    /// Database configuration of the tag entity.
    /// </summary>
    internal class EntityConfigurationTag : IEntityTypeConfiguration<Tag>
    {
        /// <summary>
        /// Configuration of the tag entity.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.ToTable("Tag");

            builder.HasKey(key => new { key.Id });

            builder.Property(e => e.Id)
                   .HasColumnName("Id");

            builder.Property(e => e.Label)
                   .HasColumnName("Label")
                   .IsRequired()
                   .HasColumnType("VARCHAR(64)");
        }
    }
}
