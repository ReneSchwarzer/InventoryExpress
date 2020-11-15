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
            Context.Log.Info(MethodBase.GetCurrentMethod(), "InventoryExpressPlugin Initialisierung");

            SiteMap.AddPage("Assets", "Assets", (x) => { return new WorkerFile(x, Context.AssetBaseFolder); });

            SiteMap.AddPage("Home", (x) => { return new WorkerPage<PageInventories>(x); });
            SiteMap.AddPage("Dashboard", "dashboard", "inventoryexpress.inventories.label", (x) => { return new WorkerPage<PageInventories>(x); });
            SiteMap.AddPage("InventoryAdd", "add", "inventoryexpress.add.label", (x) => { return new WorkerPage<PageInventoryAdd>(x); });
            SiteMap.AddPage("Details", "details", "inventoryexpress.details.label", (x) => { return new WorkerPage<PageDetails>(x); });
            SiteMap.AddPage("Locations", "locations", "inventoryexpress.locations.label", (x) => { return new WorkerPage<PageLocations>(x); });
            SiteMap.AddPage("LocationAdd", "add", "inventoryexpress.add.label", (x) => { return new WorkerPage<PageLocationAdd>(x); });
            SiteMap.AddPage("LocationEdit", "edit", "inventoryexpress.edit.label", (x) => { return new WorkerPage<PageLocationEdit>(x); });
            SiteMap.AddPage("Manufactors", "manufactors", "inventoryexpress.manufactors.label", (x) => { return new WorkerPage<PageManufactors>(x); });
            SiteMap.AddPage("ManufactorAdd", "add", "inventoryexpress.add.label", (x) => { return new WorkerPage<PageManufactorAdd>(x); });
            SiteMap.AddPage("ManufactorEdit", "edit", "inventoryexpress.edit.label", (x) => { return new WorkerPage<PageManufactorEdit>(x); });
            SiteMap.AddPage("Suppliers", "suppliers", "inventoryexpress.suppliers.label", (x) => { return new WorkerPage<PageSuppliers>(x); });
            SiteMap.AddPage("SupplierAdd", "add", "inventoryexpress.add.label", (x) => { return new WorkerPage<PageSupplierAdd>(x); });
            SiteMap.AddPage("SupplierEdit", "edit", "inventoryexpress.edit.label", (x) => { return new WorkerPage<PageSupplierEdit>(x); });
            SiteMap.AddPage("LedgerAccounts", "ledgeraccounts", "inventoryexpress.ledgeraccounts.label", (x) => { return new WorkerPage<PageLedgerAccounts>(x); });
            SiteMap.AddPage("LedgerAccountAdd", "add", "inventoryexpress.add.label", (x) => { return new WorkerPage<PageLedgerAccountAdd>(x); });
            SiteMap.AddPage("LedgerAccountEdit", "edit", "inventoryexpress.edit.label", (x) => { return new WorkerPage<PageLedgerAccountEdit>(x); });
            SiteMap.AddPage("CostCenters", "costcenters", "inventoryexpress.costcenters.label", (x) => { return new WorkerPage<PageCostCenter>(x); });
            SiteMap.AddPage("CostCenterAdd", "add", "inventoryexpress.add.label", (x) => { return new WorkerPage<PageCostCenterAdd>(x); });
            SiteMap.AddPage("CostCenterEdit", "edit", "inventoryexpress.edit.label", (x) => { return new WorkerPage<PageCostCenterEdit>(x); });
            SiteMap.AddPage("Templates", "templates", "inventoryexpress.templates.label", (x) => { return new WorkerPage<PageTemplates>(x); });
            SiteMap.AddPage("TemplateAdd", "add", "inventoryexpress.add.label", (x) => { return new WorkerPage<PageTemplateAdd>(x); });
            SiteMap.AddPage("TemplateEdit", "edit", "inventoryexpress.edit.label", (x) => { return new WorkerPage<PageTemplateEdit>(x); });
            SiteMap.AddPage("Help", "help", "inventoryexpress.help.label", (x) => { return new WorkerPage<PageHelp>(x); });

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

            SiteMap.AddPathSegmentVariable
            (
                "LocationEdit",
                new UriPathSegmentDynamicDisplay
                (
                    new UriPathSegmentDynamicDisplayText("Bearbeiten")
                ),
                new UriPathSegmentVariable("id", "(\\d+)")
            );

            SiteMap.AddPathSegmentVariable
            (
                "SupplierEdit",
                new UriPathSegmentDynamicDisplay
                (
                    new UriPathSegmentDynamicDisplayText("Bearbeiten")
                ),
                new UriPathSegmentVariable("id", "(\\d+)")
            );

            SiteMap.AddPathSegmentVariable
            (
                "LedgerAccountEdit",
                new UriPathSegmentDynamicDisplay
                (
                    new UriPathSegmentDynamicDisplayText("Bearbeiten")
                ),
                new UriPathSegmentVariable("id", "(\\d+)")
            );

            SiteMap.AddPathSegmentVariable
            (
                "CostCenterEdit",
                new UriPathSegmentDynamicDisplay
                (
                    new UriPathSegmentDynamicDisplayText("Bearbeiten")
                ),
                new UriPathSegmentVariable("id", "(\\d+)")
            );

            SiteMap.AddPathSegmentVariable
            (
                "TemplateEdit",
                new UriPathSegmentDynamicDisplay
                (
                    new UriPathSegmentDynamicDisplayText("Bearbeiten")
                ),
                new UriPathSegmentVariable("id", "(\\d+)")
            );

            SiteMap.AddPath("Assets", true);
            SiteMap.AddPath("Home");
            SiteMap.AddPath("Home/Dashboard");
            SiteMap.AddPath("Home/InventoryAdd");
            SiteMap.AddPath("Home/Details");
            SiteMap.AddPath("Home/Locations");
            SiteMap.AddPath("Home/Locations/LocationAdd");
            SiteMap.AddPath("Home/Locations/LocationEdit");
            SiteMap.AddPath("Home/Manufactors");
            SiteMap.AddPath("Home/Manufactors/ManufactorAdd");
            SiteMap.AddPath("Home/Manufactors/ManufactorEdit");
            SiteMap.AddPath("Home/Suppliers");
            SiteMap.AddPath("Home/Suppliers/SupplierAdd");
            SiteMap.AddPath("Home/Suppliers/SupplierEdit");
            SiteMap.AddPath("Home/LedgerAccounts");
            SiteMap.AddPath("Home/LedgerAccounts/LedgerAccountAdd");
            SiteMap.AddPath("Home/LedgerAccounts/LedgerAccountEdit");
            SiteMap.AddPath("Home/Costcenters");
            SiteMap.AddPath("Home/CostCenters/CostCenterAdd");
            SiteMap.AddPath("Home/CostCenters/CostCenterEdit");
            SiteMap.AddPath("Home/Templates");
            SiteMap.AddPath("Home/Templates/TemplateAdd");
            SiteMap.AddPath("Home/Templates/TemplateEdit");
            SiteMap.AddPath("Home/Help");

        }
    }
}
