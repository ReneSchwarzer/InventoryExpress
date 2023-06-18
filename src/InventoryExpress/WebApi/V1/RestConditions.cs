using InventoryExpress.Model;
using InventoryExpress.Model.WebItems;
using System.Collections.Generic;
using WebExpress.Internationalization;
using WebExpress.WebApp.WebResource;
using WebExpress.WebApp.Wql;
using WebExpress.WebAttribute;
using WebExpress.WebMessage;
using WebExpress.WebResource;

namespace InventoryExpress.WebApi.V1
{
    /// <summary>
    /// Ermittelt alle Status
    /// </summary>
    [Segment("conditions", "")]
    [ContextPath("/api/v1")]
    [IncludeSubPaths(true)]
    [Module<Module>]
    public sealed class RestConditions : ResourceRestCrud<WebItemEntityCondition>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public RestConditions()
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
                new ResourceRestCrudColumn(InternationalizationManager.I18N(request, "inventoryexpress:inventoryexpress.condition.image.label"))
                {
                    Render = "return $(\"<img style='height:1em;' src='\" + item.image + \"' alt='\" + item.name + \"'/>\");"
                },
                new ResourceRestCrudColumn(InternationalizationManager.I18N(request, "inventoryexpress:inventoryexpress.condition.name.label"))
                {
                    Render = "return item.label;",
                    Width = 5
                },
                new ResourceRestCrudColumn(InternationalizationManager.I18N(request, "inventoryexpress:inventoryexpress.condition.description.label"))
                {
                    Render = "return item.description;",
                    Width = 40
                },
                new ResourceRestCrudColumn(InternationalizationManager.I18N(request, "inventoryexpress:inventoryexpress.condition.order.label"))
                {
                    Render = "return item.grade;",
                    Width = 40
                }
            };
        }

        /// <summary>
        /// Processing of the resource. des GET-Request
        /// </summary>
        /// <param name="wql">The filter.</param>
        /// <param name="request">The request.</param>
        /// <returns>An enumeration of which json serializer can be serialized.</returns>
        public override IEnumerable<WebItemEntityCondition> GetData(WqlStatement wql, Request request)
        {
            var conditions = ViewModel.GetConditions(wql);
            return conditions;
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
