using InventoryExpress.Model;
using System.Collections.Generic;
using WebExpress.Message;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.Model;
using WebExpress.WebApp.WebApiControl;
using WebExpress.WebApp.WebResource;
using WebExpress.WebApp.Wql;
using WebExpress.WebAttribute;
using WebExpress.WebResource;
using static WebExpress.Internationalization.InternationalizationManager;

namespace InventoryExpress.WebApi.V1
{
    /// <summary>
    /// Ermittelt alle Kostenstellen
    /// </summary>
    [ID("RestCostCentersV1")]
    [Segment("costcenters", "")]
    [Path("/api/v1")]
    [IncludeSubPaths(true)]
    [Module("inventoryexpress")]
    public sealed class RestCostCenters : ResourceRestCrud<WebItem>
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public RestCostCenters()
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
                new ResourceRestCrudColumn(I18N(request, "inventoryexpress:inventoryexpress.costcenters.label"))
                {
                    Render = "return item.name;",
                    Width = 5
                }
            };
        }

        /// <summary>
        /// Verarbeitung des GET-Request
        /// </summary>
        /// <param name="wql">Der Filter</param>
        /// <param name="request">Die Anfrage</param>
        /// <returns>Eine Aufzählung, welche JsonSerializer serialisiert werden kann.</returns>
        public override IEnumerable<WebItem> GetData(WqlStatement wql, Request request)
        {
            var costCenters = ViewModel.GetCostCenters(wql);

            return costCenters;
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
