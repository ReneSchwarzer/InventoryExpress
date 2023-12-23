using InventoryExpress.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryExpress.Model.Configure
{
    /// <summary>
    /// Database configuration of the ascription entity.
    /// </summary>
    internal class EntityConfigurationAscription : IEntityTypeConfiguration<Ascription>
    {
        /// <summary>
        /// Configuration of the ascription entity
        /// </summary>
        /// <param name="builder">The builder.</param>
        public void Configure(EntityTypeBuilder<Ascription> builder)
        {
            builder.ToTable("Ascription");

            builder.Property(e => e.Id).HasColumnName("Id");

            builder.Property(e => e.Created)
                .IsRequired()
                .HasColumnType("TIMESTAMP")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.Guid)
                .IsRequired()
                .HasColumnType("CHAR(36)");

            builder.Property(e => e.MediaId).HasColumnName("MediaId");

            builder.Property(e => e.Name).HasColumnType("VARCHAR (64)");

            builder.Property(e => e.Tag).HasColumnType("VARCHAR (256)");

            builder.Property(e => e.Updated)
                .IsRequired()
                .HasColumnType("TIMESTAMP")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            // relations
            builder.HasOne(d => d.Media)
                .WithMany(p => p.Ascriptions)
                .HasForeignKey(d => d.MediaId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
