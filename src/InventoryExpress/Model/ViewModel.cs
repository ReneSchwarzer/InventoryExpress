using InventoryExpress.Model.WebItems;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.IO;
using System.Linq;
using WebExpress.WebApplication;
using WebExpress.WebModule;

namespace InventoryExpress.Model
{
    public partial class ViewModel
    {
        /// <summary>
        /// Ermittelt die Uri des Anwendungsicons 
        /// </summary>
        public static string ApplicationIcon { get; private set; }

        /// <summary>
        /// Lifert die einzige Instanz der Datenbank-Klasse
        /// </summary>
        private static InventoryDbContext DbContext { get; } = new InventoryDbContext();

        /// <summary>
        /// Returns the context of the module.
        /// </summary>
        public static IApplicationContext ApplicationContext { get; private set; }

        /// <summary>
        /// Returns the context of the module.
        /// </summary>
        public static IModuleContext ModuleContext { get; private set; }

        /// <summary>
        /// Ermittelt das Medienverzeichnis
        /// </summary>
        public static string MediaDirectory => Path.Combine(ModuleContext.DataPath, "media");

        /// <summary>
        /// Ermittelt das Exportverzeichnis
        /// </summary>
        public static string ExportDirectory => Path.Combine(ModuleContext.DataPath, "export");

        /// <summary>
        /// Constructor
        /// </summary>
        private ViewModel()
        {
            DbContext.DataSource = "Assets/db/inventory.db";
        }

        /// <summary>
        /// Initialization.
        /// </summary>
        /// <param name="applicationContext">The application context.</param>
        /// <param name="moduleContext">The module context.</param>
        public static void Initialization(IApplicationContext applicationContext, IModuleContext moduleContext)
        {
            ModuleContext = moduleContext;
            ApplicationIcon = applicationContext.Icon.ToString();

            var path = Path.Combine(moduleContext.DataPath, "db");

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