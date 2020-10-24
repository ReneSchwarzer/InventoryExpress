using InventoryExpress.Model;
using InventoryExpress.Pages;
using System.Reflection;
using WebExpress.Pages;
using WebExpress.Workers;

namespace InventoryExpress
{
    public class InventoryExpressPlugin : WebExpress.Plugins.Plugin
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public InventoryExpressPlugin()
            : base("InventoryExpress", "/Asserts/img/InventoryExpress.svg")
        {
        }

        /// <summary>
        /// Initialisierung des Prozesszustandes. Hier können z.B. verwaltete Ressourcen geladen werden. 
        /// </summary>
        /// <param name="configFileName">Der Dateiname der Konfiguration oder null</param>
        public override void Init(string configFileName = null)
        {
            base.Init(configFileName);

            ViewModel.Instance.Context = Context;
            ViewModel.Instance.Init();
            Context.Log.Info(MethodBase.GetCurrentMethod(), "InventoryExpressPlugin initialisierung");

            SiteMap.AddPage("Assets", "Assets", (x) => { return new WorkerFile(x, Context.AssetBaseFolder); });
            SiteMap.AddPage("Data", "data", (x) => { return new WorkerFile(x, Context.AssetBaseFolder); });

            SiteMap.AddPage("Home", (x) => { return new WorkerPage<PageDashboard>(x); });
            SiteMap.AddPage("Dashboard", "dashboard", (x) => { return new WorkerPage<PageDashboard>(x); });
            SiteMap.AddPage("InventoryAdd", "add", "Neu", (x) => { return new WorkerPage<PageInventoryAdd>(x); });
            SiteMap.AddPage("Details", "details", (x) => { return new WorkerPage<PageDetails>(x); });
            SiteMap.AddPage("Locations", "locations", "Standorte", (x) => { return new WorkerPage<PageLocations>(x); });
            SiteMap.AddPage("Manufactors", "manufactors", "Hersteller", (x) => { return new WorkerPage<PageManufactors>(x); });
            SiteMap.AddPage("ManufactorAdd", "add", "Neu", (x) => { return new WorkerPage<PageManufactorAdd>(x); });
            SiteMap.AddPage("ManufactorEdit", "edit", "Bearbeiten", (x) => { return new WorkerPage<PageManufactorEdit>(x); });
            SiteMap.AddPage("Suppliers", "suppliers", "Lieferanten", (x) => { return new WorkerPage<PageSuppliers>(x); });
            SiteMap.AddPage("GlAccounts", "glaccount", "Sachkonten", (x) => { return new WorkerPage<PageGlAccount>(x); });
            SiteMap.AddPage("CostCenters", "costcenter", "Kostenstellen", (x) => { return new WorkerPage<PageCostcenter>(x); });
            SiteMap.AddPage("Help", "help", (x) => { return new WorkerPage<PageHelp>(x); });

            //SiteMap.AddPathSegmentVariable
            //(
            //    "Details",
            //    new UriPathSegmentDynamicDisplay
            //    (
            //        new UriPathSegmentDynamicDisplayText("Details"),
            //        new UriPathSegmentDynamicDisplayText(" "),
            //        new UriPathSegmentDynamicDisplayReference("id5")
            //    ),
            //    "-",
            //    new UriPathSegmentVariable("id", "([0-9A-Fa-f]{8})")
            //);

            SiteMap.AddPathSegmentVariable
            (
                "ManufactorEdit",
                new UriPathSegmentDynamicDisplay
                (
                    new UriPathSegmentDynamicDisplayText("Bearbeiten")
                ),
                new UriPathSegmentVariable("id", "(\\d+)")
            );

            SiteMap.AddPath("Assets", true);
            SiteMap.AddPath("Data", true);
            SiteMap.AddPath("Home");
            SiteMap.AddPath("Home/Dashboard");
            SiteMap.AddPath("Home/InventoryAdd");
            SiteMap.AddPath("Home/Details");
            SiteMap.AddPath("Home/Locations");
            SiteMap.AddPath("Home/Manufactors");
            SiteMap.AddPath("Home/Manufactors/ManufactorAdd");
            SiteMap.AddPath("Home/Manufactors/ManufactorEdit");
            SiteMap.AddPath("Home/Suppliers");
            SiteMap.AddPath("Home/GlAccounts");
            SiteMap.AddPath("Home/Costcenters");
            SiteMap.AddPath("Home/Help");

        }
    }
}
