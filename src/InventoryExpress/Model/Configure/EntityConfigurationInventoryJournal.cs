using InventoryExpress.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryExpress.Model.Configure
{
    /// <summary>
    /// Database configuration of the journal entity.
    /// </summary>
    class EntityConfigurationInventoryJournalMedia : IEntityTypeConfiguration<InventoryJournal>
    {
        /// <summary>
        /// Configuration of the journal entity.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public void Configure(EntityTypeBuilder<InventoryJournal> builder)
        {
            builder.HasKey(e => new { e.Id });

            builder.ToTable("InventoryJournal");

            builder.Property(e => e.InventoryId).HasColumnName("InventoryId");

            builder.Property(e => e.Action).HasColumnType("VARCHAR(256)");

            builder.Property(e => e.Created)
                .IsRequired()
                .HasColumnType("TIMESTAMP")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.Guid)
               .IsRequired()
               .HasColumnType("CHAR(36)");

            builder.HasOne(d => d.Inventory)
                .WithMany(p => p.InventoryJournals)
                .HasForeignKey(d => d.InventoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
