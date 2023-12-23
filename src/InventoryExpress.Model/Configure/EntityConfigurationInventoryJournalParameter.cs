using InventoryExpress.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryExpress.Model.Configure
{
    /// <summary>
    /// Database configuration of the journal parameter entity.
    /// </summary>
    internal class EntityConfigurationInventoryJournalParameter : IEntityTypeConfiguration<InventoryJournalParameter>
    {
        /// <summary>
        /// Configuration of the journal parameter entity.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public void Configure(EntityTypeBuilder<InventoryJournalParameter> builder)
        {
            builder.HasKey(e => new { e.Id });

            builder.ToTable("InventoryJournalParameter");

            builder.Property(e => e.InventoryJournalId).HasColumnName("InventoryJournalId");

            builder.Property(e => e.Name).HasColumnType("VARCHAR (256)");
            builder.Property(e => e.OldValue).HasColumnType("VARCHAR (256)");
            builder.Property(e => e.NewValue).HasColumnType("VARCHAR (256)");

            builder.Property(e => e.Guid)
               .IsRequired()
               .HasColumnType("CHAR (36)");

            builder.HasOne(d => d.InventoryJournal)
                .WithMany(p => p.InventoryJournalParameters)
                .HasForeignKey(d => d.InventoryJournalId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
