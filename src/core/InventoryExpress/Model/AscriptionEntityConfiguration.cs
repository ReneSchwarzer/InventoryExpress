using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryExpress.Model
{
    class AscriptionEntityConfiguration : IEntityTypeConfiguration<Ascription>
    {
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
                .HasColumnType("CHAR (36)");

            builder.Property(e => e.MediaId).HasColumnName("MediaID");

            builder.Property(e => e.Name).HasColumnType("VARCHAR (64)");

            builder.Property(e => e.Tag).HasColumnType("VARCHAR (256)");

            builder.Property(e => e.Updated)
                .IsRequired()
                .HasColumnType("TIMESTAMP")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.HasOne(d => d.Media)
                .WithMany(p => p.Ascriptions)
                .HasForeignKey(d => d.MediaId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
