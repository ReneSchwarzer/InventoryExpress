﻿using InventoryExpress.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryExpress.Model.Configure
{
    /// <summary>
    /// Datenbankkonfiguration der Journal-Entität
    /// </summary>
    class EntityConfigurationInventoryJournalMedia : IEntityTypeConfiguration<InventoryJournal>
    {
        /// <summary>
        /// Konfiguration
        /// </summary>
        /// <param name="builder">Der Builder</param>
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