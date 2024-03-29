﻿using InventoryExpress.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryExpress.Model.Configure
{
    /// <summary>
    /// Database configuration of the inventory entity.
    /// </summary>
    class EntityConfigurationInventory : IEntityTypeConfiguration<Inventory>
    {
        /// <summary>
        /// Configuration of the inventory entity.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            builder.ToTable("Inventory");

            builder.HasKey(key => new { key.Id });

            builder.Property(e => e.Id)
                   .HasColumnName("Id");

            builder.Property(e => e.ConditionId)
                   .HasColumnName("ConditionId");

            builder.Property(e => e.CostCenterId)
                   .HasColumnName("CostCenterId");

            builder.Property(e => e.LedgerAccountId)
                   .HasColumnName("LedgerAccountId");

            builder.Property(e => e.LocationId)
                   .HasColumnName("LocationId");

            builder.Property(e => e.ManufacturerId)
                   .HasColumnName("ManufacturerId");

            builder.Property(e => e.SupplierId)
                   .HasColumnName("SupplierId");

            builder.Property(e => e.TemplateId)
                   .HasColumnName("TemplateId");

            builder.Property(e => e.MediaId)
                   .HasColumnName("MediaId");

            builder.Property(e => e.Name)
                   .HasColumnName("Name")
                   .IsRequired()
                   .HasColumnType("VARCHAR(64)");

            builder.Property(e => e.CostValue)
                   .HasColumnName("CostValue")
                   .HasColumnType("DECIMAL");

            builder.Property(e => e.DerecognitionDate)
                   .HasColumnName("DerecognitionDate")
                   .HasColumnType("DATE");

            builder.Property(e => e.PurchaseDate)
                   .HasColumnName("PurchaseDate")
                   .HasColumnType("DATE");

            builder.Property(e => e.Description)
                   .HasColumnName("Description")
                   .HasColumnType("TEXT");

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

            // unique contraints
            builder.HasIndex(e => e.Name)
                   .IsUnique();

            builder.HasIndex(e => e.Guid)
                   .IsUnique();

            // relations
            builder.HasOne(d => d.Condition)
                   .WithMany(p => p.Inventories)
                   .HasForeignKey(d => d.ConditionId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(d => d.CostCenter)
                   .WithMany(p => p.Inventories)
                   .HasForeignKey(d => d.CostCenterId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(d => d.LedgerAccount)
                   .WithMany(p => p.Inventories)
                   .HasForeignKey(d => d.LedgerAccountId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(d => d.Location)
                   .WithMany(p => p.Inventories)
                   .HasForeignKey(d => d.LocationId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(d => d.Manufacturer)
                   .WithMany(p => p.Inventories)
                   .HasForeignKey(d => d.ManufacturerId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(d => d.Parent)
                   .WithMany(p => p.Inventories)
                   .HasForeignKey(d => d.ParentId)
                    .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(d => d.Media)
                   .WithMany(p => p.Inventories)
                   .HasForeignKey(d => d.MediaId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(d => d.Supplier)
                   .WithMany(p => p.Inventories)
                   .HasForeignKey(d => d.SupplierId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(d => d.Template)
                   .WithMany(p => p.Inventories)
                   .HasForeignKey(d => d.TemplateId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
