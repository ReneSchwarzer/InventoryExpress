using InventoryExpress.Model.WebItems;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using WebExpress.WebApp.Model;
using WebExpress.WebApp.WebIndex;
using WebExpress.WebCore.WebApplication;
using WebExpress.WebCore.WebComponent;
using WebExpress.WebCore.WebModule;
using WebExpress.WebCore.WebUri;

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
        /// CallBack function to determine the uri of a web item.
        /// </summary>
        public static Func<WebItem, UriResource> GetUri { get; private set; }

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
        /// <param name="uriFunc">CallBack function to determine the root uri of a web item.</param>
        public static void Initialization(IApplicationContext applicationContext, IModuleContext moduleContext, Func<WebItem, UriResource> uriFunc)
        {
            ModuleContext = moduleContext;
            ApplicationIcon = applicationContext.Icon.ToString();
            GetUri = uriFunc;

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

            // indexing the data
            ComponentManager.GetComponent<IndexManager>()?.Register<WebItemEntityInventory>(CultureInfo.CurrentCulture);
            ComponentManager.GetComponent<IndexManager>()?.Register<WebItemEntityCostCenter>(CultureInfo.CurrentCulture);
            ComponentManager.GetComponent<IndexManager>()?.Register<WebItemEntityLedgerAccount>(CultureInfo.CurrentCulture);
            ComponentManager.GetComponent<IndexManager>()?.Register<WebItemEntityLocation>(CultureInfo.CurrentCulture);
            ComponentManager.GetComponent<IndexManager>()?.Register<WebItemEntityManufacturer>(CultureInfo.CurrentCulture);
            ComponentManager.GetComponent<IndexManager>()?.Register<WebItemEntitySupplier>(CultureInfo.CurrentCulture);
            ComponentManager.GetComponent<IndexManager>()?.Register<WebItemEntityJournal>(CultureInfo.CurrentCulture);
            ComponentManager.GetComponent<IndexManager>()?.Register<WebItemEntityJournalParameter>(CultureInfo.CurrentCulture);
            ComponentManager.GetComponent<IndexManager>()?.Register<WebItemEntityAttribute>(CultureInfo.CurrentCulture);
            ComponentManager.GetComponent<IndexManager>()?.Register<WebItemEntityTag>(CultureInfo.CurrentCulture);
            ComponentManager.GetComponent<IndexManager>()?.Register<WebItemEntityTemplate>(CultureInfo.CurrentCulture);
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