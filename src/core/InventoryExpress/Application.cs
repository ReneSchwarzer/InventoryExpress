using InventoryExpress.Model;
using System.IO;
using System.Linq;
using WebExpress.Application;
using WebExpress.Attribute;

namespace InventoryExpress
{
    [ID("InventoryExpress")]
    [Name("app.name")]
    [Description("app.description")]
    [Icon("/assets/img/inventoryexpress.svg")]
    [AssetPath("data")]
    [ContextPath("/ix")]
    [Option("webexpress.webapp.*")]
    public sealed class Application : IApplication
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public Application()
        {
        }

        /// <summary>
        /// Initialisierung der Anwendung. Hier können z.B. verwaltete Ressourcen geladen werden. 
        /// </summary>
        /// <param name="context">Der Kontext, welcher für die Ausführung des Plugins gilt</param>
        public void Initialization(IApplicationContext context)
        {
            var path = Path.Combine(context.AssetPath, "db");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            // Datenbank initialisieren
            ViewModel.Instance.DataSource = Path.Combine(path, "inventory.db");
            ViewModel.Instance.Initialization(context.Plugin);

            // Daten vorladen
            ViewModel.Instance.Inventories.ToList();
            ViewModel.Instance.CostCenters.ToList();
            ViewModel.Instance.Manufacturers.ToList();
            ViewModel.Instance.Suppliers.ToList();
        }

        /// <summary>
        /// Wird aufgerufen, wenn die Anwendung mit der Arbeit beginnt. Der Aufruf erfolgt nebenläufig.
        /// </summary>
        public void Run()
        {

        }

        /// <summary>
        /// Freigeben von nicht verwalteten Ressourcen, welche wärend der Verwendung reserviert wurden.
        /// </summary>
        public void Dispose()
        {

        }
    }
}
