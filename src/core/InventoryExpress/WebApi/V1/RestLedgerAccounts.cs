using InventoryExpress.Model;
using System.Collections.Generic;
using System.Linq;
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
    /// Ermittelt alle Sachkonten
    /// </summary>
    [ID("RestLedgerAccountsV1")]
    [Segment("ledgeraccounts", "")]
    [Path("/api/v1")]
    [IncludeSubPaths(true)]
    [Module("inventoryexpress")]
    public sealed class RestLedgerAccounts : ResourceRestCrud<WebItem>
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public RestLedgerAccounts()
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
                new ResourceRestCrudColumn(I18N(request, "inventoryexpress:inventoryexpress.ledgeraccounts.label"))
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
            var ledgerAccounts = ViewModel.GetLedgerAccounts(wql);

            return ledgerAccounts;
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
