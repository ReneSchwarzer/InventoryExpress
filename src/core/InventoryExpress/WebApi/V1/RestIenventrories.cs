﻿using InventoryExpress.Model;
using InventoryExpress.Model.WebItems;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Message;
using WebExpress.WebApp.WebResource;
using WebExpress.WebApp.Wql;
using WebExpress.WebAttribute;
using WebExpress.WebResource;
using static WebExpress.Internationalization.InternationalizationManager;

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
            Guard = ViewModel.Instance.Database;
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
                new ResourceRestCrudColumn(I18N(request, "inventoryexpress:inventoryexpress.inventory.label"))
                {
                    Render = "return $(\"<a class='link' href='\" + item.Uri + \"'>\" + item.Name + \"</a>\");",
                    Width = 10
                },
                new ResourceRestCrudColumn(I18N(request, "inventoryexpress:inventoryexpress.template.label"))
                {
                    Render = "return $(\"<a class='link' href='\" + item.Template?.Uri + \"'>\" + item.Template?.Name + \"</a>\");"
                },
                new ResourceRestCrudColumn(I18N(request, "inventoryexpress:inventoryexpress.manufacturer.label"))
                {
                    Render = "return $(\"<a class='link' href='\" + item.Manufacturer?.Uri + \"'>\" + item.Manufacturer?.Name + \"</a>\");"
                },
                new ResourceRestCrudColumn(I18N(request, "inventoryexpress:inventoryexpress.supplier.label"))
                {
                    Render = "return $(\"<a class='link' href='\" + item.Supplier?.Uri + \"'>\" + item.Supplier?.Name + \"</a>\");"
                },
                new ResourceRestCrudColumn(I18N(request, "inventoryexpress:inventoryexpress.location.label"))
                {
                    Render = "return $(\"<a class='link' href='\" + item.Location?.Uri + \"'>\" + item.Location?.Name + \"</a>\");"
                },
                new ResourceRestCrudColumn(I18N(request, "inventoryexpress:inventoryexpress.costcenter.label"))
                {
                    Render = "return $(\"<a class='link' href='\" + item.CostCenter?.Uri + \"'>\" + item.CostCenter?.Name + \"</a>\");"
                },
                new ResourceRestCrudColumn(I18N(request, "inventoryexpress:inventoryexpress.ledgeraccount.label"))
                {
                    Render = "return $(\"<a class='link' href='\" + item.LedgerAccount?.Uri + \"'>\" + item.LedgerAccount?.Name + \"</a>\");"
                },
                new ResourceRestCrudColumn(I18N(request, "inventoryexpress:inventoryexpress.condition.label"))
                {
                    Render = "return $(\"<img style='height:1em;' src='\" + item.Condition?.Image + \"' alt='\" + item.Condition?.Name + \"'/>\");"
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