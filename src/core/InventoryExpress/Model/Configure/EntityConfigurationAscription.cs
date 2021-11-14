using InventoryExpress.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryExpress.Model.Configure
{
    /// <summary>
    /// Datenbankkonfiguration der Zuschreibungs-Entität
    /// </summary>
    class EntityConfigurationAscription : IEntityTypeConfiguration<Ascription>
    {
        /// <summary>
        /// Konfiguration
        /// </summary>
        /// <param name="builder">Der Builder</param>
        public void Configure(EntityTypeBuilder<Ascription> builder)
        {
            builder.ToTable("Ascription");

            builder.Property(e => e.Id).HasColumnName("ID");

            builder.Property(e => e.Created)
                .IsRequired()
                .HasColumnType("TIMESTAMP")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.Guid)
                .IsRequired()
                .HasColumnType("CHAR(36)");

            builder.Property(e => e.MediaId).HasColumnName("MediaID");

            builder.Property(e => e.Name).HasColumnType("VARCHAR (64)");

            builder.Property(e => e.Tag).HasColumnType("VARCHAR (256)");

            builder.Property(e => e.Updated)
                .IsRequired()
                .HasColumnType("TIMESTAMP")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            // Beziehungen
            builder.HasOne(d => d.Media)
                .WithMany(p => p.Ascriptions)
                .HasForeignKey(d => d.MediaId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
