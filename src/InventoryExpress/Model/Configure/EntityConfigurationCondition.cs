using InventoryExpress.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryExpress.Model.Configure
{
    /// <summary>
    /// Datenbankkonfiguration der Zustands-Entität
    /// </summary>
    class EntityConfigurationCondition : IEntityTypeConfiguration<Condition>
    {
        /// <summary>
        /// Konfiguration
        /// </summary>
        /// <param name="builder">Der Builder</param>
        public void Configure(EntityTypeBuilder<Condition> builder)
        {
            builder.ToTable("Condition");
            builder.HasKey(key => new { key.Id });

            //entity.HasIndex(e => e.Grade, "IX_Condition_Grade")
            //    .IsUnique();

            //entity.HasIndex(e => e.Guid, "IX_Condition_Guid")
            //    .IsUnique();

            //entity.HasIndex(e => e.Name, "IX_Condition_Name")
            //    .IsUnique();

            builder.Property(e => e.Id)
                   .HasColumnName("Id");

            builder.Property(e => e.MediaId)
                   .HasColumnName("MediaId");

            builder.Property(e => e.Name)
                   .HasColumnName("Name")
                   .IsRequired()
                   .HasColumnType("VARCHAR (64)");

            builder.Property(e => e.Grade)
                   .HasColumnName("Grade")
                   .HasColumnType("INTEGER (1)");

            builder.Property(e => e.Description)
                   .HasColumnName("Description")
                   .HasColumnType("TEXT");

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
                   .HasColumnType("CHAR (36)");

            builder.HasOne(d => d.Media)
                .WithMany(p => p.Conditions)
                .HasForeignKey(d => d.MediaId);
        }
    }
}
