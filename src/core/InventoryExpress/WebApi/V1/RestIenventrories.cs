using InventoryExpress.Model;
using InventoryExpress.Model.WebItems;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Internationalization;
using WebExpress.Message;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebApiControl;
using WebExpress.WebApp.WebResource;
using WebExpress.WebApp.Wql;
using WebExpress.WebAttribute;
using WebExpress.WebResource;

namespace InventoryExpress.WebApi.V1
{
    /// <summary>
    /// Ermittelt alle Inventargegenstände
    /// </summary>
    [ID("RestInventoriesV1")]
    [Segment("inventories", "")]
    [Path("/api/v1")]
    [IncludeSubPaths(true)]
    [Module("inventoryexpress")]
    public sealed class RestInventories : ResourceRestCrud<WebItemEntityInventory>
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public RestInventories()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);
        }

        /// <summary>
        /// Verarbeitung des GET-Request
        /// </summary>
        /// <param name="request">Die Anfrage</param>
        /// <returns>Eine Aufzählung, welche JsonSerializer serialisiert werden kann.</returns>
        public override IEnumerable<ResourceRestCrudColumn> GetColumns(Request request)
        {
            return new ResourceRestCrudColumn[]
            {
                new ResourceRestCrudColumn(InternationalizationManager.I18N(request, "inventoryexpress:inventoryexpress.inventory.label"))
                {
                    Render = "return $(\"<a class='link' href='\" + item.uri + \"'>\" + item.name + \"</a>\");",
                    Width = 10
                },
                new ResourceRestCrudColumn(InternationalizationManager.I18N(request, "inventoryexpress:inventoryexpress.template.label"))
                {
                    Render = "return $(\"<a class='link' href='\" + item.template?.Uri + \"'>\" + item.template?.name + \"</a>\");"
                },
                new ResourceRestCrudColumn(InternationalizationManager.I18N(request, "inventoryexpress:inventoryexpress.manufacturer.label"))
                {
                    Render = "return $(\"<a class='link' href='\" + item.manufacturer?.Uri + \"'>\" + item.manufacturer?.name + \"</a>\");"
                },
                new ResourceRestCrudColumn(InternationalizationManager.I18N(request, "inventoryexpress:inventoryexpress.supplier.label"))
                {
                    Render = "return $(\"<a class='link' href='\" + item.supplier?.uri + \"'>\" + item.supplier?.name + \"</a>\");"
                },
                new ResourceRestCrudColumn(InternationalizationManager.I18N(request, "inventoryexpress:inventoryexpress.location.label"))
                {
                    Render = "return $(\"<a class='link' href='\" + item.location?.uri + \"'>\" + item.location?.name + \"</a>\");"
                },
                new ResourceRestCrudColumn(InternationalizationManager.I18N(request, "inventoryexpress:inventoryexpress.costcenter.label"))
                {
                    Render = "return $(\"<a class='link' href='\" + item.costcenter?.uri + \"'>\" + item.costcenter?.name + \"</a>\");"
                },
                new ResourceRestCrudColumn(InternationalizationManager.I18N(request, "inventoryexpress:inventoryexpress.ledgeraccount.label"))
                {
                    Render = "return $(\"<a class='link' href='\" + item.ledgeraccount?.uri + \"'>\" + item.ledgeraccount?.name + \"</a>\");"
                },
                new ResourceRestCrudColumn(InternationalizationManager.I18N(request, "inventoryexpress:inventoryexpress.condition.label"))
                {
                    Render = "return $(\"<img style='height:1em;' src='\" + item.condition?.image + \"' alt='\" + item.condition?.name + \"'/>\");"
                }
            };
        }

        /// <summary>
        /// Verarbeitung des GET-Request
        /// </summary>
        /// <param name="wql">Der Filter</param>
        /// <param name="request">Die Anfrage</param>
        /// <returns>Eine Aufzählung, welche JsonSerializer serialisiert werden kann.</returns>
        public override IEnumerable<WebItemEntityInventory> GetData(WqlStatement wql, Request request)
        {
            var inventories = ViewModel.GetInventories(wql);

            return inventories;
        }

        /// <summary>
        /// Verarbeitung des DELETE-Request
        /// </summary>
        /// <param name="id">Die zu löschende ID</param>
        /// <param name="request">Die Anfrage</param>
        /// <returns>Das Ergebnis der Löschung</returns>
        public override bool DeleteData(string id, Request request)
        {
            return true;
        }
    }
}
