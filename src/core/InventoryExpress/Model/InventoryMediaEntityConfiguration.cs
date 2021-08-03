﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryExpress.Model
{
    class InventoryMediaEntityConfiguration : IEntityTypeConfiguration<InventoryMedia>
    {
        public void Configure(EntityTypeBuilder<InventoryMedia> builder)
        {
            builder.HasKey(e => new { e.InventoryId, e.MediaId });

            builder.ToTable("InventoryMedia");

            builder.Property(e => e.InventoryId).HasColumnName("InventoryID");

            builder.Property(e => e.MediaId).HasColumnName("MediaID");

            builder.Property(e => e.Created)
                .IsRequired()
                .HasColumnType("TIMESTAMP")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.HasOne(d => d.Media)
                .WithMany(p => p.InventoryMedia)
                .HasForeignKey(d => d.MediaId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(d => d.Inventory)
                .WithMany(p => p.InventoryMedia)
                .HasForeignKey(d => d.InventoryId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
