﻿using InventoryExpress.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryExpress.Model.Configure
{
    /// <summary>
    /// Datenbankkonfiguration der Sachkonto-Entität
    /// </summary>
    class EntityConfigurationLedgerAccount : IEntityTypeConfiguration<LedgerAccount>
    {
        /// <summary>
        /// Konfiguration
        /// </summary>
        /// <param name="builder">Der Builder</param>
        public void Configure(EntityTypeBuilder<LedgerAccount> builder)
        {
            builder.ToTable("LedgerAccount");

            builder.HasKey(key => new { key.Id });

            builder.Property(e => e.Id)
                   .HasColumnName("ID");

            builder.Property(e => e.MediaId)
                   .HasColumnName("MediaID");

            builder.Property(e => e.Name)
                   .HasColumnName("Name")
                   .IsRequired()
                   .HasColumnType("VARCHAR(64)");

            builder.Property(e => e.Tag)
                   .HasColumnName("Tag")
                   .HasColumnType("VARCHAR(256)");

            builder.Property(e => e.Created)
                   .HasColumnName("Created")
                   .IsRequired()
                   .HasColumnType("TIMESTAMP")
                   .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.Updated)
                   .HasColumnName("Updated")
                   .IsRequired()
                   .HasColumnType("TIMESTAMP")
                   .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.Guid)
                   .HasColumnName("Guid")
                   .IsRequired()
                   .HasColumnType("CHAR(36)");

            // Unique-Contraints
            builder.HasIndex(e => e.Name)
                   .IsUnique();

            builder.HasIndex(e => e.Guid)
                   .IsUnique();

            // Beziehungen
            builder.HasOne(d => d.Media)
                   .WithMany(p => p.LedgerAccounts)
                   .HasForeignKey(d => d.MediaId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
