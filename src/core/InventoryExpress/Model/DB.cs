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
        /// Liefert oder setzt die Datei-Anhänge der Inventargegenstände
        /// </summary>
        public DbSet<InventoryAttachment> InventoryAttachment { get; set; }

        /// <summary>
        /// Liefert oder setzt die Kommentare der Inventargegenstände
        /// </summary>
        public DbSet<InventoryComment> InventoryComments { get; set; }

        /// <summary>
        /// Liefert oder setzt das Journal der Inventargegenstände
        /// </summary>
        public DbSet<InventoryJournal> InventoryJournals { get; set; }
        
        /// <summary>
        /// Liefert oder setzt die Journal-Parameter der Inventargegenstände
        /// </summary>
        public DbSet<InventoryJournalParameter> InventoryJournalParameters { get; set; }

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
        /// Liefert oder setzt die Medien
        /// </summary>
        public DbSet<Setting> Settings { get; set; }

        /// <summary>
        /// Liefert oder setzt die Datenquelle
        /// </summary>
        public string DataSource { get; private set; } = "Assets/db/inventory.db";

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
            optionsBuilder.UseSqlite($"Data Source={ DataSource };");
        }

        /// <summary>
        /// Modell erstellen
        /// </summary>
        /// <param name="modelBuilder">Der Modellbuilder</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DB).Assembly);
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