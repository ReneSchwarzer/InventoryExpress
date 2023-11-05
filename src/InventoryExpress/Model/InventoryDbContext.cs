using InventoryExpress.Model.Entity;
using Microsoft.EntityFrameworkCore;

namespace InventoryExpress.Model
{
    public class InventoryDbContext : DbContext
    {
        /// <summary>
        /// Returns or sets the data source.
        /// </summary>
        public string DataSource { get; internal set; }

        /// <summary>
        /// Returns or sets the inventory items.
        /// </summary>
        public DbSet<Inventory> Inventories { get; set; }

        /// <summary>
        /// Returns or sets the attributes of the inventory items.
        /// </summary>
        public DbSet<InventoryAttribute> InventoryAttributes { get; set; }

        /// <summary>
        /// Returns or sets the file attachments of the inventory items.
        /// </summary>
        public DbSet<InventoryAttachment> InventoryAttachments { get; set; }

        /// <summary>
        /// Returns or sets the comments of the inventory items.
        /// </summary>
        public DbSet<InventoryComment> InventoryComments { get; set; }

        /// <summary>
        /// Returns or sets the journal of inventory items.
        /// </summary>
        public DbSet<InventoryJournal> InventoryJournals { get; set; }

        /// <summary>
        /// Returns or sets the journal parameters of the inventory items.
        /// </summary>
        public DbSet<InventoryJournalParameter> InventoryJournalParameters { get; set; }

        /// <summary>
        /// Returns or sets the tags of the inventory items.
        /// </summary>
        public DbSet<InventoryTag> InventoryTags { get; set; }

        /// <summary>
        /// Returns or sets the states.
        /// </summary>
        public DbSet<Condition> Conditions { get; set; }

        /// <summary>
        /// Returns or sets the locations.
        /// </summary>
        public DbSet<Location> Locations { get; set; }

        /// <summary>
        /// Returns or sets the manufacturers.
        /// </summary>
        public DbSet<Manufacturer> Manufacturers { get; set; }

        /// <summary>
        /// Returns or sets the suppliers.
        /// </summary>
        public DbSet<Supplier> Suppliers { get; set; }

        /// <summary>
        /// Returns or sets the ledger accounts.
        /// </summary>
        public DbSet<LedgerAccount> LedgerAccounts { get; set; }

        /// <summary>
        /// Returns or sets the cost centers.
        /// </summary>
        public DbSet<CostCenter> CostCenters { get; set; }

        /// <summary>
        /// Returns or sets the templates.
        /// </summary>
        public DbSet<Template> Templates { get; set; }

        /// <summary>
        /// Returns or sets the template attributes.
        /// </summary>
        public DbSet<TemplateAttribute> TemplateAttributes { get; set; }

        /// <summary>
        /// Returns or sets the attributes.
        /// </summary>
        public DbSet<Attribute> Attributes { get; set; }

        /// <summary>
        /// Returns or sets the media.
        /// </summary>
        public DbSet<Media> Media { get; set; }

        /// <summary>
        /// Returns or sets the tags.
        /// </summary>
        public DbSet<Tag> Tags { get; set; }

        /// <summary>
        /// Returns or sets the settings.
        /// </summary>
        public DbSet<Setting> Settings { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public InventoryDbContext()
        {
        }

        /// <summary>
        /// Invoked when the db is to be configured.
        /// </summary>
        /// <param name="optionsBuilder">The options builder.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={DataSource};");
        }

        /// <summary>
        /// Invoked when the model is to be created.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(InventoryDbContext).Assembly);
        }

        /// <summary>
        /// Update everything.
        /// </summary>
        public void RefreshAll()
        {
            foreach (var entity in ChangeTracker.Entries())
            {
                entity.Reload();
            }
        }

        /// <summary>
        /// Discards the changes that have been made since the last save.
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