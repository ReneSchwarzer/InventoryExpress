using InventoryExpress.Model.WebItems;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.IO;
using System.Linq;
using WebExpress.WebModule;

namespace InventoryExpress.Model
{
    public partial class ViewModel
    {
        /// <summary>
        /// Instanz des einzigen Modells
        /// </summary>
        public static string RootUri { get; private set; }

        /// <summary>
        /// Ermittelt die Uri des Anwendungsicons 
        /// </summary>
        public static string ApplicationIcon { get; private set; }

        /// <summary>
        /// Lifert die einzige Instanz der Datenbank-Klasse
        /// </summary>
        private static InventoryDbContext DbContext { get; } = new InventoryDbContext();

        /// <summary>
        /// Liefert oder setzt den Kontext
        /// </summary>
        public static IModuleContext Context { get; private set; }

        /// <summary>
        /// Ermittelt das Medienverzeichnis
        /// </summary>
        public static string MediaDirectory => Path.Combine(Context.Application.DataPath, "media");

        /// <summary>
        /// Konstruktor
        /// </summary>
        private ViewModel()
        {
            DbContext.DataSource = "Assets/db/inventory.db";
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext, welcher für die Ausführung des Plugins gilt</param>
        public static void Initialization(IModuleContext context)
        {
            Context = context;
            ApplicationIcon = Context.Application.Icon.ToString();
            RootUri = context.ContextPath.ToString();

            var path = Path.Combine(context.DataPath, "db");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            // Datenbank initialisieren
            DbContext.DataSource = Path.Combine(path, "inventory.db");

            // möglicherweise erstellen und ggf. Migrationspfad anwenden
            DbContext.Database.Migrate();

            // Daten vorladen
            _ = DbContext.Inventories.ToList();
            _ = DbContext.CostCenters.ToList();
            _ = DbContext.Manufacturers.ToList();
            _ = DbContext.Suppliers.ToList();
        }

        /// <summary>
        /// Ermittelt die Datenbankinformationen
        /// </summary>
        /// <returns>Die Datenbankinformationen</returns>
        public static WebItemDbInfo GetDbInfo()
        {
            var info = new WebItemDbInfo()
            {
                ProviderName = DbContext.Database.ProviderName,
                DataSource = DbContext.DataSource
            };

            return info;
        }

        /// <summary>
        /// Startet eine neue Transaktion
        /// </summary>
        /// <returns>Die Transaktion</returns>
        public static IDbContextTransaction BeginTransaction()
        {
            return DbContext.Database.BeginTransaction();
        }

        /// <summary>
        /// Export der Daten
        /// </summary>
        /// <param name="fileName">Das Archive</param>
        /// <param name="dataPath">Das Verzeichnis, indem die Anhänge gespeichert werden</param>
        /// <param name="progress">Der Fortschritt</param>
        public static void Export(string fileName, string dataPath, Action<int> progress)
        {
            ImportExport.Export(fileName, dataPath, progress);
        }
        
        

        /// <summary>
        /// Import der Daten
        /// </summary>
        /// <param name="fileName">Das Archive</param>
        /// <param name="dataPath">Das Verzeichnis, indem die Anhänge gespeichert werden</param>
        /// <param name="progress">Der Fortschritt</param>
        public static void Import(string fileName, string dataPath, Action<int> progress)
        {
            lock (DbContext)
            {
                using var transaction = DbContext.Database.BeginTransaction();

                DbContext.Conditions.RemoveRange(DbContext.Conditions);
                DbContext.Locations.RemoveRange(DbContext.Locations);
                DbContext.Manufacturers.RemoveRange(DbContext.Manufacturers);
                DbContext.Suppliers.RemoveRange(DbContext.Suppliers);
                DbContext.LedgerAccounts.RemoveRange(DbContext.LedgerAccounts);
                DbContext.CostCenters.RemoveRange(DbContext.CostCenters);
                DbContext.Inventories.RemoveRange(DbContext.Inventories);
                DbContext.InventoryAttributes.RemoveRange(DbContext.InventoryAttributes);
                DbContext.InventoryAttachments.RemoveRange(DbContext.InventoryAttachments);
                DbContext.InventoryComments.RemoveRange(DbContext.InventoryComments);
                DbContext.InventoryJournals.RemoveRange(DbContext.InventoryJournals);
                DbContext.InventoryJournalParameters.RemoveRange(DbContext.InventoryJournalParameters);
                DbContext.InventoryTags.RemoveRange(DbContext.InventoryTags);
                DbContext.Templates.RemoveRange(DbContext.Templates);
                DbContext.TemplateAttributes.RemoveRange(DbContext.TemplateAttributes);
                DbContext.Attributes.RemoveRange(DbContext.Attributes);
                DbContext.Media.RemoveRange(DbContext.Media);
                DbContext.Tags.RemoveRange(DbContext.Tags);
                //Database.ExecuteSqlCommand("vacum;");

                DbContext.SaveChanges();
                
                transaction.Commit();
            }

            ImportExport.Import(fileName, dataPath, progress);
        }
    }
}