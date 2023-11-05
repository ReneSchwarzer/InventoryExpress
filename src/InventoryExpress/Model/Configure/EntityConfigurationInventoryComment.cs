using InventoryExpress.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryExpress.Model.Configure
{
    /// <summary>
    /// Database configuration of the comment entity.
    /// </summary>
    class InventoryCommentMediaEntityConfiguration : IEntityTypeConfiguration<InventoryComment>
    {
        /// <summary>
        /// Configuration of the comment entity.
        /// </summary>
        /// <param name="builder">The builder.</param>
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
