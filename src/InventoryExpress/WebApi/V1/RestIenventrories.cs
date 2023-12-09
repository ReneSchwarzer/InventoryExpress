using InventoryExpress.Model;
using InventoryExpress.Model.WebItems;
using System.Collections.Generic;
using WebExpress.WebApp.WebResource;
using WebExpress.WebCore.Internationalization;
using WebExpress.WebCore.WebAttribute;
using WebExpress.WebCore.WebMessage;
using WebExpress.WebCore.WebResource;
using WebExpress.WebIndex.Wql;

namespace InventoryExpress.WebApi.V1
{
    /// <summary>
    /// Returns all inventory items.
    /// </summary>
    [Segment("inventories", "")]
    [ContextPath("/api/v1")]
    [IncludeSubPaths(true)]
    [Module<Module>]
    public sealed class RestInventories : ResourceRestCrud<WebItemEntityInventory>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public RestInventories()
        {
        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The context.</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);
        }

        /// <summary>
        /// Processing of the resource that was called via the get request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>An enumeration of which json serializer can be serialized.</returns>
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
                    Render = "return $(\"<a class='link' href='\" + item.template?.uri + \"'>\" + (item.template?.name ?? '') + \"</a>\");"
                },
                new ResourceRestCrudColumn(InternationalizationManager.I18N(request, "inventoryexpress:inventoryexpress.manufacturer.label"))
                {
                    Render = "return $(\"<a class='link' href='\" + item.manufacturer?.uri + \"'>\" + (item.manufacturer?.name ?? '') + \"</a>\");"
                },
                new ResourceRestCrudColumn(InternationalizationManager.I18N(request, "inventoryexpress:inventoryexpress.supplier.label"))
                {
                    Render = "return $(\"<a class='link' href='\" + item.supplier?.uri + \"'>\" + (item.supplier?.name ?? '') + \"</a>\");"
                },
                new ResourceRestCrudColumn(InternationalizationManager.I18N(request, "inventoryexpress:inventoryexpress.location.label"))
                {
                    Render = "return $(\"<a class='link' href='\" + item.location?.uri + \"'>\" + (item.location?.name ?? '') + \"</a>\");"
                },
                new ResourceRestCrudColumn(InternationalizationManager.I18N(request, "inventoryexpress:inventoryexpress.costcenter.label"))
                {
                    Render = "return $(\"<a class='link' href='\" + item.costcenter?.uri + \"'>\" + (item.costcenter?.name ?? '') + \"</a>\");"
                },
                new ResourceRestCrudColumn(InternationalizationManager.I18N(request, "inventoryexpress:inventoryexpress.ledgeraccount.label"))
                {
                    Render = "return $(\"<a class='link' href='\" + item.ledgeraccount?.uri + \"'>\" + (item.ledgeraccount?.name ?? '') + \"</a>\");"
                },
                new ResourceRestCrudColumn(InternationalizationManager.I18N(request, "inventoryexpress:inventoryexpress.condition.label"))
                {
                    Render = "return item.condition != null ? $(\"<img style='height:1em;' src='\" + item.condition?.image + \"' alt='\" + item.condition?.name + \"'/>\") : null;"
                }
            };
        }

        /// <summary>
        /// Processing of the resource that was called via the get request.
        /// </summary>
        /// <param name="wql">The filtering and sorting options.</param>
        /// <param name="request">The request.</param>
        /// <returns>An enumeration of which json serializer can be serialized.</returns>
        public override IEnumerable<WebItemEntityInventory> GetData(IWqlStatement<WebItemEntityInventory> wql, Request request)
        {
            var inventories = ViewModel.GetInventories(wql);

            return inventories;
        }

        /// <summary>
        /// Processing of the resource that was called via the delete request.
        /// </summary>
        /// <param name="id">The id to delete.</param>
        /// <param name="request">The request.</param>
        /// <returns>The result of the deletion.</returns>
        public override bool DeleteData(string id, Request request)
        {
            return true;
        }
    }
}
