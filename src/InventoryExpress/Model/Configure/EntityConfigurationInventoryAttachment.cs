﻿using InventoryExpress.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryExpress.Model.Configure
{
    /// <summary>
    /// Database configuration of the attachment entity.
    /// </summary>
    class EntityConfigurationInventoryAttachment : IEntityTypeConfiguration<InventoryAttachment>
    {
        /// <summary>
        /// Configuration of the attachment entity.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public void Configure(EntityTypeBuilder<InventoryAttachment> builder)
        {
            builder.HasKey(e => new { e.InventoryId, e.MediaId });

            builder.ToTable("InventoryAttachment");

            builder.Property(e => e.InventoryId).HasColumnName("InventoryId");

            builder.Property(e => e.MediaId).HasColumnName("MediaId");

            builder.Property(e => e.Created)
                .IsRequired()
                .HasColumnType("TIMESTAMP")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            // relations
            builder.HasOne(d => d.Media)
                .WithMany(p => p.InventoryAttachment)
                .HasForeignKey(d => d.MediaId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(d => d.Inventory)
                .WithMany(p => p.InventoryMedia)
                .HasForeignKey(d => d.InventoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
