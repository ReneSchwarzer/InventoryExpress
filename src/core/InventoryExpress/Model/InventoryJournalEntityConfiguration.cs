using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryExpress.Model
{
    class InventoryJournalMediaEntityConfiguration : IEntityTypeConfiguration<InventoryJournal>
    {
        public void Configure(EntityTypeBuilder<InventoryJournal> builder)
        {
            builder.HasKey(e => new { e.Id });

            builder.ToTable("InventoryJournal");

            builder.Property(e => e.InventoryId).HasColumnName("InventoryID");

            builder.Property(e => e.Action).HasColumnType("VARCHAR (256)");
            builder.Property(e => e.ActionParam).HasColumnType("VARCHAR (256)");

            builder.Property(e => e.Created)
                .IsRequired()
                .HasColumnType("TIMESTAMP")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.Guid)
               .IsRequired()
               .HasColumnType("CHAR (36)");

            builder.HasOne(d => d.Inventory)
                .WithMany(p => p.InventoryJournals)
                .HasForeignKey(d => d.InventoryId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
