using InventoryExpress.Model;
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
    /// Ermittelt alle Status
    /// </summary>
    [ID("RestConditionsV1")]
    [Segment("conditions", "")]
    [Path("/api/v1")]
    [IncludeSubPaths(true)]
    [Module("inventoryexpress")]
    public sealed class RestConditions : ResourceRestCrud<WebItemEntityCondition>
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public RestConditions()
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
                new ResourceRestCrudColumn(I18N(request, "inventoryexpress:inventoryexpress.condition.image.label"))
                {
                    Render = "return $(\"<img style='height:1em;' src='\" + item.image + \"' alt='\" + item.name + \"'/>\");"
                },
                new ResourceRestCrudColumn(I18N(request, "inventoryexpress:inventoryexpress.condition.name.label"))
                {
                    Render = "return item.label;",
                    Width = 5
                },
                new ResourceRestCrudColumn(I18N(request, "inventoryexpress:inventoryexpress.condition.description.label"))
                {
                    Render = "return item.description;",
                    Width = 40
                },
                new ResourceRestCrudColumn(I18N(request, "inventoryexpress:inventoryexpress.condition.order.label"))
                {
                    Render = "return item.grade;",
                    Width = 40
                }
            };
        }

        /// <summary>
        /// Verarbeitung des GET-Request
        /// </summary>
        /// <param name="wql">Der Filter</param>
        /// <param name="request">Die Anfrage</param>
        /// <returns>Eine Aufzählung, welche JsonSerializer serialisiert werden kann.</returns>
        public override IEnumerable<WebItemEntityCondition> GetData(WqlStatement wql, Request request)
        {
            lock (ViewModel.Instance.Database)
            {
                var conditions = ViewModel.GetConditions(wql).ToList();

                return conditions;
            }
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
