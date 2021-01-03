using Microsoft.EntityFrameworkCore;

namespace InventoryExpress.Model
{
    public class DB : DbContext
    {
        /// <summary>
        /// Liefert oder setzt die Inventargegenstände
        /// </summary>
        public DbSet<Inventory> Inventories { get; set; }

        /// <summary>
        /// Liefert oder setzt die Attribute der Inventargegenstände
        /// </summary>
        public DbSet<InventoryAttribute> InventoryAttributes { get; set; }

        /// <summary>
        /// Liefert oder setzt die Medien der Inventargegenstände
        /// </summary>
        public DbSet<InventoryMedia> InventoryMedia { get; set; }

        /// <summary>
        /// Liefert oder setzt die Kommentare der Inventargegenstände
        /// </summary>
        public DbSet<InventoryComment> InventoryComments { get; set; }

        /// <summary>
        /// Liefert oder setzt das Journal der Inventargegenstände
        /// </summary>
        public DbSet<InventoryJournal> InventoryJournals { get; set; }

        /// <summary>
        /// Liefert oder setzt die Zustände
        /// </summary>
        public DbSet<Condition> Conditions { get; set; }

        /// <summary>
        /// Liefert oder setzt die Standorte
        /// </summary>
        public DbSet<Location> Locations { get; set; }

        /// <summary>
        /// Liefert oder setzt die Hersteller
        /// </summary>
        public DbSet<Manufacturer> Manufacturers { get; set; }

        /// <summary>
        /// Liefert oder setzt die Lieferanten
        /// </summary>
        public DbSet<Supplier> Suppliers { get; set; }

        /// <summary>
        /// Liefert oder setzt die Sachkonten
        /// </summary>
        public DbSet<LedgerAccount> LedgerAccounts { get; set; }

        /// <summary>
        /// Liefert oder setzt die Kostenstellen
        /// </summary>
        public DbSet<CostCenter> CostCenters { get; set; }

        /// <summary>
        /// Liefert oder setzt die Vorlagen
        /// </summary>
        public DbSet<Template> Templates { get; set; }
        
        /// <summary>
        /// Liefert oder setzt die Vorlagenattribute
        /// </summary>
        public DbSet<TemplateAttribute> TemplateAttributes { get; set; }

        /// <summary>
        /// Liefert oder setzt die Attribute
        /// </summary>
        public DbSet<Attribute> Attributes { get; set; }

        /// <summary>
        /// Liefert oder setzt die Medien
        /// </summary>
        public DbSet<Media> Media { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        internal DB()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=Assets/db/inventory.db;");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ascription>(entity =>
            {
                entity.ToTable("Ascription");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Created)
                    .IsRequired()
                    .HasColumnType("TIMESTAMP")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Guid)
                    .IsRequired()
                    .HasColumnType("CHAR (36)");

                entity.Property(e => e.MediaId).HasColumnName("MediaID");

                entity.Property(e => e.Name).HasColumnType("VARCHAR (64)");

                entity.Property(e => e.Tag).HasColumnType("VARCHAR (256)");

                entity.Property(e => e.Updated)
                    .IsRequired()
                    .HasColumnType("TIMESTAMP")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Media)
                    .WithMany(p => p.Ascriptions)
                    .HasForeignKey(d => d.MediaId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Attribute>(entity =>
            {
                entity.ToTable("Attribute");

                //entity.HasIndex(e => e.Guid, "IX_Attribute_Guid")
                //    .IsUnique();

                //entity.HasIndex(e => e.Name, "IX_Attribute_Name")
                //    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Created)
                    .IsRequired()
                    .HasColumnType("TIMESTAMP")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Guid)
                    .IsRequired()
                    .HasColumnType("CHAR (36)");

                entity.Property(e => e.MediaId).HasColumnName("MediaID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("VARCHAR (64)");

                entity.Property(e => e.Updated)
                    .IsRequired()
                    .HasColumnType("TIMESTAMP")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Media)
                    .WithMany(p => p.Attributes)
                    .HasForeignKey(d => d.MediaId);
            });

            modelBuilder.Entity<Condition>(entity =>
            {
                entity.ToTable("Condition");

                //entity.HasIndex(e => e.Grade, "IX_Condition_Grade")
                //    .IsUnique();

                //entity.HasIndex(e => e.Guid, "IX_Condition_Guid")
                //    .IsUnique();

                //entity.HasIndex(e => e.Name, "IX_Condition_Name")
                //    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Created)
                    .IsRequired()
                    .HasColumnType("TIMESTAMP")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Grade).HasColumnType("INTEGER (1)");

                entity.Property(e => e.Guid)
                    .IsRequired()
                    .HasColumnType("CHAR (36)");

                entity.Property(e => e.MediaId).HasColumnName("MediaID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("VARCHAR (64)");

                entity.Property(e => e.Updated)
                    .IsRequired()
                    .HasColumnType("TIMESTAMP")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Media)
                    .WithMany(p => p.Conditions)
                    .HasForeignKey(d => d.MediaId);
            });

            modelBuilder.Entity<CostCenter>(entity =>
            {
                entity.ToTable("CostCenter");

                //entity.HasIndex(e => e.Guid, "IX_CostCenter_Guid")
                //    .IsUnique();

                //entity.HasIndex(e => e.Name, "IX_CostCenter_Name")
                //    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Created)
                    .IsRequired()
                    .HasColumnType("TIMESTAMP")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Guid)
                    .IsRequired()
                    .HasColumnType("CHAR (36)");

                entity.Property(e => e.MediaId).HasColumnName("MediaID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("VARCHAR (64)");

                entity.Property(e => e.Tag).HasColumnType("VARCHAR (256)");

                entity.Property(e => e.Updated)
                    .IsRequired()
                    .HasColumnType("TIMESTAMP")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Media)
                    .WithMany(p => p.CostCenters)
                    .HasForeignKey(d => d.MediaId);
            });

            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.ToTable("Inventory");

                //entity.HasIndex(e => e.Guid, "IX_Inventory_Guid")
                //    .IsUnique();

                //entity.HasIndex(e => e.Name, "IX_Inventory_Name")
                //    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ConditionId).HasColumnName("ConditionID");

                entity.Property(e => e.CostCenterId).HasColumnName("CostCenterID");

                entity.Property(e => e.CostValue).HasColumnType("DECIMAL");

                entity.Property(e => e.Created)
                    .IsRequired()
                    .HasColumnType("TIMESTAMP")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.DerecognitionDate).HasColumnType("DATE");

                entity.Property(e => e.Guid)
                    .IsRequired()
                    .HasColumnType("CHAR (36)");

                entity.Property(e => e.LedgerAccountId).HasColumnName("LedgerAccountID");

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.ManufacturerId).HasColumnName("ManufacturerID");

                entity.Property(e => e.MediaId).HasColumnName("MediaID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("VARCHAR (64)");

                entity.Property(e => e.PurchaseDate).HasColumnType("DATE");

                entity.Property(e => e.SupplierId).HasColumnName("SupplierID");

                entity.Property(e => e.Tag).HasColumnType("VARCHAR (256)");

                entity.Property(e => e.TemplateId).HasColumnName("TemplateID");

                entity.Property(e => e.Updated)
                    .IsRequired()
                    .HasColumnType("TIMESTAMP")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Condition)
                    .WithMany(p => p.Inventories)
                    .HasForeignKey(d => d.ConditionId);

                entity.HasOne(d => d.CostCenter)
                    .WithMany(p => p.Inventories)
                    .HasForeignKey(d => d.CostCenterId);

                entity.HasOne(d => d.LedgerAccount)
                    .WithMany(p => p.Inventories)
                    .HasForeignKey(d => d.LedgerAccountId);

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Inventories)
                    .HasForeignKey(d => d.LocationId);

                entity.HasOne(d => d.Manufacturer)
                    .WithMany(p => p.Inventories)
                    .HasForeignKey(d => d.ManufacturerId);

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.Inventories)
                    .HasForeignKey(d => d.ParentId);

                entity.HasOne(d => d.Media)
                    .WithMany(p => p.Inventories)
                    .HasForeignKey(d => d.MediaId);

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.Inventories)
                    .HasForeignKey(d => d.SupplierId);

                entity.HasOne(d => d.Template)
                    .WithMany(p => p.Inventories)
                    .HasForeignKey(d => d.TemplateId);
            });

            modelBuilder.Entity<InventoryAttribute>(entity =>
            {
                entity.HasKey(e => new { e.InventoryId, e.AttributeId });

                entity.ToTable("InventoryAttribute");

                entity.Property(e => e.InventoryId).HasColumnName("InventoryID");

                entity.Property(e => e.AttributeId).HasColumnName("AttributeID");

                entity.Property(e => e.Created)
                    .IsRequired()
                    .HasColumnType("TIMESTAMP")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Attribute)
                    .WithMany(p => p.InventoryAttributes)
                    .HasForeignKey(d => d.AttributeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Inventory)
                    .WithMany(p => p.InventoryAttributes)
                    .HasForeignKey(d => d.InventoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<InventoryMedia>(entity =>
            {
                entity.HasKey(e => new { e.InventoryId, e.MediaId });

                entity.ToTable("InventoryMedia");

                entity.Property(e => e.InventoryId).HasColumnName("InventoryID");

                entity.Property(e => e.MediaId).HasColumnName("MediaID");

                entity.Property(e => e.Created)
                    .IsRequired()
                    .HasColumnType("TIMESTAMP")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Media)
                    .WithMany(p => p.InventoryMedia)
                    .HasForeignKey(d => d.MediaId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Inventory)
                    .WithMany(p => p.InventoryMedia)
                    .HasForeignKey(d => d.InventoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<InventoryComment>(entity =>
            {
                entity.HasKey(e => new { e.Id});

                entity.ToTable("InventoryComment");

                entity.Property(e => e.InventoryId).HasColumnName("InventoryID");

                entity.Property(e => e.Created)
                    .IsRequired()
                    .HasColumnType("TIMESTAMP")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Updated)
                    .IsRequired()
                    .HasColumnType("TIMESTAMP")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Guid)
                   .IsRequired()
                   .HasColumnType("CHAR (36)");

                entity.HasOne(d => d.Inventory)
                    .WithMany(p => p.InventoryComments)
                    .HasForeignKey(d => d.InventoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<InventoryJournal>(entity =>
            {
                entity.HasKey(e => new { e.Id });

                entity.ToTable("InventoryJournal");

                entity.Property(e => e.InventoryId).HasColumnName("InventoryID");

                entity.Property(e => e.Action).HasColumnType("VARCHAR (256)");
                entity.Property(e => e.ActionParam).HasColumnType("VARCHAR (256)");

                entity.Property(e => e.Created)
                    .IsRequired()
                    .HasColumnType("TIMESTAMP")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Guid)
                   .IsRequired()
                   .HasColumnType("CHAR (36)");

                entity.HasOne(d => d.Inventory)
                    .WithMany(p => p.InventoryJournals)
                    .HasForeignKey(d => d.InventoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<LedgerAccount>(entity =>
            {
                entity.ToTable("LedgerAccount");

                //entity.HasIndex(e => e.Guid, "IX_LedgerAccount_Guid")
                //    .IsUnique();

                //entity.HasIndex(e => e.Name, "IX_LedgerAccount_Name")
                //    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Created)
                    .IsRequired()
                    .HasColumnType("TIMESTAMP")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Guid)
                    .IsRequired()
                    .HasColumnType("CHAR (36)");

                entity.Property(e => e.MediaId).HasColumnName("MediaID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("VARCHAR (64)");

                entity.Property(e => e.Tag).HasColumnType("VARCHAR (256)");

                entity.Property(e => e.Updated)
                    .IsRequired()
                    .HasColumnType("TIMESTAMP")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Media)
                    .WithMany(p => p.LedgerAccounts)
                    .HasForeignKey(d => d.MediaId);
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("Location");

                //entity.HasIndex(e => e.Guid, "IX_Location_Guid")
                //    .IsUnique();

                //entity.HasIndex(e => e.Name, "IX_Location_Name")
                //    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address).HasColumnType("VARCHAR (256)");

                entity.Property(e => e.Building).HasColumnType("VARCHAR (64)");

                entity.Property(e => e.Created)
                    .IsRequired()
                    .HasColumnType("TIMESTAMP")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Guid)
                    .IsRequired()
                    .HasColumnType("CHAR (36)");

                entity.Property(e => e.MediaId).HasColumnName("MediaID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("VARCHAR (64)");

                entity.Property(e => e.Place).HasColumnType("VARCHAR (64)");

                entity.Property(e => e.Room).HasColumnType("VARCHAR (64)");

                entity.Property(e => e.Tag).HasColumnType("VARCHAR (256)");

                entity.Property(e => e.Updated)
                    .HasColumnType("TIMESTAMP")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Zip).HasColumnType("VARCHAR (10)");

                entity.HasOne(d => d.Media)
                    .WithMany(p => p.Locations)
                    .HasForeignKey(d => d.MediaId);
            });

            modelBuilder.Entity<Manufacturer>(entity =>
            {
                entity.ToTable("Manufacturer");

                //entity.HasIndex(e => e.Guid, "IX_Manufacturer_Guid")
                //    .IsUnique();

                //entity.HasIndex(e => e.Name, "IX_Manufacturer_Name")
                //    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address).HasColumnType("VARCHAR (256)");

                entity.Property(e => e.Created)
                    .IsRequired()
                    .HasColumnType("TIMESTAMP")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Guid)
                    .IsRequired()
                    .HasColumnType("CHAR (36)");

                entity.Property(e => e.MediaId).HasColumnName("MediaID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("VARCHAR (64)");

                entity.Property(e => e.Place).HasColumnType("VARCHAR (64)");

                entity.Property(e => e.Tag).HasColumnType("VARCHAR (256)");

                entity.Property(e => e.Updated)
                    .IsRequired()
                    .HasColumnType("TIMESTAMP")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Zip).HasColumnType("VARCHAR (10)");

                entity.HasOne(d => d.Media)
                    .WithMany(p => p.Manufacturers)
                    .HasForeignKey(d => d.MediaId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Media>(entity =>
            {
                //entity.HasIndex(e => e.Guid, "IX_Media_Guid")
                //    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Created)
                    .IsRequired()
                    .HasColumnType("TIMESTAMP")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Guid)
                    .IsRequired()
                    .HasColumnType("CHAR (36)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("VARCHAR (64)");

                entity.Property(e => e.Tag).HasColumnType("VARCHAR (256)");

                entity.Property(e => e.Updated)
                    .IsRequired()
                    .HasColumnType("TIMESTAMP")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.ToTable("Supplier");

                //entity.HasIndex(e => e.Guid, "IX_Supplier_Guid")
                //    .IsUnique();

                //entity.HasIndex(e => e.Name, "IX_Supplier_Name")
                //    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address).HasColumnType("VARCHAR (256)");

                entity.Property(e => e.Created)
                    .IsRequired()
                    .HasColumnType("TIMESTAMP")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Guid)
                    .IsRequired()
                    .HasColumnType("CHAR (36)");

                entity.Property(e => e.MediaId).HasColumnName("MediaID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("VARCHAR (64)");

                entity.Property(e => e.Place).HasColumnType("VARCHAR (64)");

                entity.Property(e => e.Tag).HasColumnType("VARCHAR (256)");

                entity.Property(e => e.Updated)
                    .IsRequired()
                    .HasColumnType("TIMESTAMP")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Zip).HasColumnType("VARCHAR (10)");

                entity.HasOne(d => d.Media)
                    .WithMany(p => p.Suppliers)
                    .HasForeignKey(d => d.MediaId);
            });

            modelBuilder.Entity<Template>(entity =>
            {
                entity.ToTable("Template");

                //entity.HasIndex(e => e.Guid, "IX_Template_Guid")
                //    .IsUnique();

                //entity.HasIndex(e => e.Name, "IX_Template_Name")
                //    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Created)
                    .IsRequired()
                    .HasColumnType("TIMESTAMP")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Guid)
                    .IsRequired()
                    .HasColumnType("CHAR (36)");

                entity.Property(e => e.MediaId).HasColumnName("MediaID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("VARCHAR (64)");

                entity.Property(e => e.Tag).HasColumnType("VARCHAR (256)");

                entity.Property(e => e.Updated)
                    .IsRequired()
                    .HasColumnType("TIMESTAMP")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Media)
                    .WithMany(p => p.Templates)
                    .HasForeignKey(d => d.MediaId);
            });

            modelBuilder.Entity<TemplateAttribute>(entity =>
            {
                entity.HasKey(e => new { e.TemplateId, e.AttributeId });

                entity.ToTable("TemplateAttribute");

                entity.Property(e => e.TemplateId).HasColumnName("TemplateID");

                entity.Property(e => e.AttributeId).HasColumnName("AttributeID");

                entity.Property(e => e.Created)
                    .IsRequired()
                    .HasColumnType("TIMESTAMP")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Attribute)
                    .WithMany(p => p.TemplateAttributes)
                    .HasForeignKey(d => d.AttributeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Template)
                    .WithMany(p => p.TemplateAttributes)
                    .HasForeignKey(d => d.TemplateId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });
        }

        /// <summary>
        /// Alles aktualliesieren
        /// </summary>
        public void RefreshAll()
        {
            foreach (var entity in ChangeTracker.Entries())
            {
                entity.Reload();
            }
        }

        /// <summary>
        /// Verwirft die Änderungen, die seit dem letzten Speichern durchgeführt worden
        /// </summary>
        public void Rollback()
        {
            if (ChangeTracker.HasChanges())
            {
                RefreshAll();
            }
        }
    }
}