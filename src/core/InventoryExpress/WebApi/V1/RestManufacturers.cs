using InventoryExpress.Model;
using InventoryExpress.Model.WebItems;
using System.Collections.Generic;
using WebExpress.Internationalization;
using WebExpress.WebMessage;
using WebExpress.WebApp.WebResource;
using WebExpress.WebApp.Wql;
using WebExpress.WebAttribute;
using WebExpress.WebResource;

namespace InventoryExpress.WebApi.V1
{
    /// <summary>
    /// Ermittelt alle Hersteller
    /// </summary>
    [WebExId("RestManufacturersV1")]
    [WebExSegment("manufacturers", "")]
    [WebExContextPath("/api/v1")]
    [WebExIncludeSubPaths(true)]
    [WebExModule(typeof(Module))]
    public sealed class RestManufacturers : ResourceRestCrud<WebItemEntityManufacturer>
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public RestManufacturers()
        {
        }

        /// <summary>
        /// Initialisierung
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
                new ResourceRestCrudColumn(InternationalizationManager.I18N(request, "inventoryexpress:inventoryexpress.manufacturer.label"))
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
        public override IEnumerable<WebItemEntityManufacturer> GetData(WqlStatement wql, Request request)
        {
            return ViewModel.GetManufacturers(wql);
        }

        /// <summary>
        /// Processing of the resource. des DELETE-Request
        /// </summary>
        /// <param name="id">Die zu löschende ID</param>
        /// <param name="request">The request.</param>
        /// <returns>Das Ergebnis der Löschung</returns>
        public override bool DeleteData(string id, Request request)
        {
            using var transaction = ViewModel.BeginTransaction();

            ViewModel.DeleteManufacturer(id);

            transaction.Commit();

            return true;
        }
    }
}
