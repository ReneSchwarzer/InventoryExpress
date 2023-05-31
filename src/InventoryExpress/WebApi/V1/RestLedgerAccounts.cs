using InventoryExpress.Model;
using System.Collections.Generic;
using WebExpress.Internationalization;
using WebExpress.WebApp.Model;
using WebExpress.WebApp.WebResource;
using WebExpress.WebApp.Wql;
using WebExpress.WebAttribute;
using WebExpress.WebMessage;
using WebExpress.WebResource;

namespace InventoryExpress.WebApi.V1
{
    /// <summary>
    /// Ermittelt alle Sachkonten
    /// </summary>
    [WebExSegment("ledgeraccounts", "")]
    [WebExContextPath("/api/v1")]
    [WebExIncludeSubPaths(true)]
    [WebExModule(typeof(Module))]
    public sealed class RestLedgerAccounts : ResourceRestCrud<WebItem>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public RestLedgerAccounts()
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
        /// Processing of the resource. des GET-Request
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>An enumeration of which json serializer can be serialized.</returns>
        public override IEnumerable<ResourceRestCrudColumn> GetColumns(Request request)
        {
            return new ResourceRestCrudColumn[]
            {
                new ResourceRestCrudColumn(InternationalizationManager.I18N(request, "inventoryexpress:inventoryexpress.ledgeraccounts.label"))
                {
                    Render = "return item.name;",
                    Width = 5
                }
            };
        }

        /// <summary>
        /// Processing of the resource. des GET-Request
        /// </summary>
        /// <param name="wql">The filter.</param>
        /// <param name="request">The request.</param>
        /// <returns>An enumeration of which json serializer can be serialized.</returns>
        public override IEnumerable<WebItem> GetData(WqlStatement wql, Request request)
        {
            var ledgerAccounts = ViewModel.GetLedgerAccounts(wql);

            return ledgerAccounts;
        }

        /// <summary>
        /// Processing of the resource. des DELETE-Request
        /// </summary>
        /// <param name="id">Die zu löschende Id</param>
        /// <param name="request">The request.</param>
        /// <returns>Das Ergebnis der Löschung</returns>
        public override bool DeleteData(string id, Request request)
        {
            return true;
        }
    }
}
