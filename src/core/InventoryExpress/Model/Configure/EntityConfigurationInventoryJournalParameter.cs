using InventoryExpress.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryExpress.Model.Configure
{
    /// <summary>
    /// Datenbankkonfiguration der JournalParameter-Entität
    /// </summary>
    class EntityConfigurationInventoryJournalParameter : IEntityTypeConfiguration<InventoryJournalParameter>
    {
        /// <summary>
        /// Konfiguration
        /// </summary>
        /// <param name="builder">Der Builder</param>
        public void Configure(EntityTypeBuilder<InventoryJournalParameter> builder)
        {
            builder.HasKey(e => new { e.Id });

            builder.ToTable("InventoryJournalParameter");

            builder.Property(e => e.InventoryJournalId).HasColumnName("InventoryJournalID");

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
