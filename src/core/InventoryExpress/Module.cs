using InventoryExpress.Model;
using System.IO;
using System.Linq;
using WebExpress.WebAttribute;
using WebExpress.WebModule;

namespace InventoryExpress
{
    [ID("InventoryExpress")]
    [Name("module.name")]
    [Description("module.description")]
    [Icon("/assets/img/Logo.png")]
    [AssetPath("")]
    [ContextPath("/")]
    [Application("InventoryExpress")]
    public sealed class Module : IModule
    {
        /// <summary>
        /// Der Kontext
        /// </summary>
        private IModuleContext Context { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public Module()
        {
        }

        /// <summary>
        /// Initialisierung des Moduls. Hier können z.B. verwaltete Ressourcen geladen werden. 
        /// </summary>
        /// <param name="context">Der Kontext, welcher für die Ausführung des Plugins gilt</param>
        public void Initialization(IModuleContext context)
        {
            Context = context;

            var path = Path.Combine(context.DataPath, "db");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            // Datenbank initialisieren
            ViewModel.Instance.DataSource = Path.Combine(path, "inventory.db");
            ViewModel.Instance.Initialization(context);

            // Daten vorladen
            _ = ViewModel.Instance.Inventories.ToList();
            _ = ViewModel.Instance.CostCenters.ToList();
            _ = ViewModel.Instance.Manufacturers.ToList();
            _ = ViewModel.Instance.Suppliers.ToList();
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Modul mit der Arbeit beginnt. Der Aufruf erfolgt nebenläufig.
        /// </summary>
        public void Run()
        {
            //ViewModel.Instance.Import(Path.Combine(@"C:\Users\rene_\OneDrive\Bilder\Desktop\Sandbox\inventory.zip"), Context.Application.AssetPath, i => { });
            //ViewModel.Instance.Export(Path.Combine(@"C:\Users\rene_\OneDrive\Bilder\Desktop\Sandbox\_inventory.zip"), Context.Application.AssetPath, i => { });
        }

        /// <summary>
        /// Freigeben von nicht verwalteten Ressourcen, welche wärend der Verwendung reserviert wurden.
        /// </summary>
        public void Dispose()
        {

        }
    }
}
