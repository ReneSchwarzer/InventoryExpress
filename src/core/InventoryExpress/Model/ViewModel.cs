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
        /// Ermittelt das Exportverzeichnis
        /// </summary>
        public static string ExportDirectory => Path.Combine(Context.Application.DataPath, "export");

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
    }
}