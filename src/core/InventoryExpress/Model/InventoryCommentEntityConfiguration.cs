using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryExpress.Model
{
    class InventoryCommentMediaEntityConfiguration : IEntityTypeConfiguration<InventoryComment>
    {
        public void Configure(EntityTypeBuilder<InventoryComment> builder)
        {
            builder.HasKey(e => new { e.Id });

            builder.ToTable("InventoryComment");

            builder.Property(e => e.InventoryId).HasColumnName("InventoryID");

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
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
