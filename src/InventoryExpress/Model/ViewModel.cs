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
        /// Returns the Uri of the application icon.
        /// </summary>
        public static string ApplicationIcon { get; private set; }

        /// <summary>
        /// Returns the only instance of the database class.
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
        /// Returns the media directory.
        /// </summary>
        public static string MediaDirectory => Path.Combine(ModuleContext.DataPath, "media");

        /// <summary>
        /// Returns the export directory.
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

            // initializing the database
            DbContext.DataSource = Path.Combine(path, "inventory.db");

            // and apply a migration path if necessary
            DbContext.Database.Migrate();

            // preload data
            _ = DbContext.Inventories.ToList();
            _ = DbContext.CostCenters.ToList();
            _ = DbContext.Manufacturers.ToList();
            _ = DbContext.Suppliers.ToList();
        }

        /// <summary>
        /// Determines the database informations.
        /// </summary>
        /// <returns>the database informations.</returns>
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
        /// Starts a new transaction.
        /// </summary>
        /// <returns>The transaction</returns>
        public static IDbContextTransaction BeginTransaction()
        {
            return DbContext.Database.BeginTransaction();
        }
    }
}