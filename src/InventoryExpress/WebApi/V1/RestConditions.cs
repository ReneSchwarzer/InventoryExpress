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
    /// Returns all states.
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
        /// Processing of the resource that was called via the get request.
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
        /// Processing of the resource that was called via the get request.
        /// </summary>
        /// <param name="wql">The filtering and sorting options.</param>
        /// <param name="request">The request.</param>
        /// <returns>An enumeration of which json serializer can be serialized.</returns>
        public override IEnumerable<WebItemEntityCondition> GetData(IWqlStatement<WebItemEntityCondition> wql, Request request)
        {
            var conditions = ViewModel.GetConditions(wql);

            return conditions;
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
