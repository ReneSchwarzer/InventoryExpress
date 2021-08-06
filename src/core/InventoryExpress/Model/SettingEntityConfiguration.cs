using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryExpress.Model
{
    class SettingEntityConfiguration : IEntityTypeConfiguration<Setting>
    {
        public void Configure(EntityTypeBuilder<Setting> builder)
        {
            builder.ToTable("Setting");

            builder.Property(e => e.Id)
                   .HasColumnName("ID");

            builder.Property(e => e.Currency)
                   .HasColumnName("Currency")
                   .IsRequired()
                   .HasColumnType("VARCHAR (10)");
        }
    }
}
