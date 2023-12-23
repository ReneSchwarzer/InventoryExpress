using InventoryExpress.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryExpress.Model.Configure
{
    /// <summary>
    /// Database configuration of the settings entity.
    /// </summary>
    internal class EntityConfigurationSetting : IEntityTypeConfiguration<Setting>
    {
        /// <summary>
        /// Configuration of the settings entity.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public void Configure(EntityTypeBuilder<Setting> builder)
        {
            builder.ToTable("Setting");

            builder.Property(e => e.Id)
                   .HasColumnName("Id");

            builder.Property(e => e.Currency)
                   .HasColumnName("Currency")
                   .IsRequired()
                   .HasColumnType("VARCHAR(10)");
        }
    }
}
