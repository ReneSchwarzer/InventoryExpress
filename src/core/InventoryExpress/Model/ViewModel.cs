using InventoryExpress.Model.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using WebExpress.WebPlugin;

namespace InventoryExpress.Model
{
    public class ViewModel : DB
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
        public DbSet<InventoryAttachment> InventoryAttachments { get; set; }

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
        /// Liefert oder setzt die Schlagwörter der Inventargegenstände
        /// </summary>
        public DbSet<InventoryTag> InventoryTags { get; set; }

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
        public DbSet<Entity.Attribute> Attributes { get; set; }

        /// <summary>
        /// Liefert oder setzt die Medien
        /// </summary>
        public DbSet<Media> Media { get; set; }

        /// <summary>
        /// Liefert oder setzt die Schlagwörter
        /// </summary>
        public DbSet<Tag> Tags { get; set; }

        /// <summary>
        /// Liefert oder setzt die Medien
        /// </summary>
        public DbSet<Setting> Settings { get; set; }

        /// <summary>
        /// Instanz des einzigen Modells
        /// </summary>
        private static ViewModel _this = null;

        /// <summary>
        /// Lifert die einzige Instanz der Modell-Klasse
        /// </summary>
        public static ViewModel Instance
        {
            get
            {
                if (_this == null)
                {
                    _this = new ViewModel();
                }

                return _this;
            }
        }

        /// <summary>
        /// Liefert oder setzt den Kontext
        /// </summary>
        public IPluginContext Context { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        private ViewModel()
        {
            DataSource = "Assets/db/inventory.db";
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext, welcher für die Ausführung des Plugins gilt</param>
        public void Initialization(IPluginContext context)
        {
            Database.EnsureCreated();

            Database.Migrate();
        }


        /// <summary>
        /// Export der Daten
        /// </summary>
        /// <param name="fileName">Das Archive</param>
        /// <param name="dataPath">Das Verzeichnis, indem die Anhänge gespeichert werden</param>
        /// <param name="progress">Der Fortschritt</param>
        public void Export(string fileName, string dataPath, Action<int> progress)
        {
            ImportExport.Export(fileName, dataPath, progress);
        }

        /// <summary>
        /// Import der Daten
        /// </summary>
        /// <param name="fileName">Das Archive</param>
        /// <param name="dataPath">Das Verzeichnis, indem die Anhänge gespeichert werden</param>
        /// <param name="progress">Der Fortschritt</param>
        public void Import(string fileName, string dataPath, Action<int> progress)
        {
            lock (Database)
            {
                Database.BeginTransaction();

                Conditions.RemoveRange(Conditions);
                Locations.RemoveRange(Locations);
                Manufacturers.RemoveRange(Manufacturers);
                Suppliers.RemoveRange(Suppliers);
                LedgerAccounts.RemoveRange(LedgerAccounts);
                CostCenters.RemoveRange(CostCenters);
                Inventories.RemoveRange(Inventories);
                InventoryAttributes.RemoveRange(InventoryAttributes);
                InventoryAttachments.RemoveRange(InventoryAttachments);
                InventoryComments.RemoveRange(InventoryComments);
                InventoryJournals.RemoveRange(InventoryJournals);
                InventoryJournalParameters.RemoveRange(InventoryJournalParameters);
                InventoryTags.RemoveRange(InventoryTags);
                Templates.RemoveRange(Templates);
                TemplateAttributes.RemoveRange(TemplateAttributes);
                Attributes.RemoveRange(Attributes);
                Media.RemoveRange(Media);
                Tags.RemoveRange(Tags);

                Database.CommitTransaction();

                //Database.ExecuteSqlCommand("vacum;");

                SaveChanges();
            }

            ImportExport.Import(fileName, dataPath, progress);
        }
    }
}