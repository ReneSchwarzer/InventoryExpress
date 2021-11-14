using InventoryExpress.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryExpress.Model.Configure
{
    /// <summary>
    /// Datenbankkonfiguration der Einstellungen-Entität
    /// </summary>
    class EntityConfigurationSetting : IEntityTypeConfiguration<Setting>
    {
        /// <summary>
        /// Konfiguration
        /// </summary>
        /// <param name="builder">Der Builder</param>
        public void Configure(EntityTypeBuilder<Setting> builder)
        {
            builder.ToTable("Setting");

            builder.Property(e => e.Id)
                   .HasColumnName("ID");

            builder.Property(e => e.Currency)
                   .HasColumnName("Currency")
                   .IsRequired()
                   .HasColumnType("VARCHAR(10)");
        }
    }
}
