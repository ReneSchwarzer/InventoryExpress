using InventoryExpress.Model;
using System.Collections.Generic;
using WebExpress.Internationalization;
using WebExpress.WebMessage;
using WebExpress.WebApp.Model;
using WebExpress.WebApp.WebResource;
using WebExpress.WebApp.Wql;
using WebExpress.WebAttribute;
using WebExpress.WebResource;

namespace InventoryExpress.WebApi.V1
{
    /// <summary>
    /// Ermittelt alle Attribute
    /// </summary>
    [WebExId("RestAttributesV1")]
    [WebExSegment("attributes", "")]
    [WebExContextPath("/api/v1")]
    [WebExIncludeSubPaths(true)]
    [WebExModule(typeof(Module))]
    public sealed class RestAttributes : ResourceRestCrud<WebItem>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public RestAttributes()
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
                new ResourceRestCrudColumn(InternationalizationManager.I18N(request, "inventoryexpress:inventoryexpress.attribute.name.label"))
                {
                    Render = "return item.name;",
                    Width = 20
                },
                new ResourceRestCrudColumn(InternationalizationManager.I18N(request, "inventoryexpress:inventoryexpress.attribute.description.label"))
                {
                    Render = "return item.description;"
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
            var attributes = ViewModel.GetAttributes(wql);

            return attributes;
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
