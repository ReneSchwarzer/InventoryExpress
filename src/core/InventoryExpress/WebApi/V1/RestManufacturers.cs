using InventoryExpress.Model;
using InventoryExpress.Model.WebItems;
using System.Collections.Generic;
using WebExpress.Internationalization;
using WebExpress.Message;
using WebExpress.WebApp.WebResource;
using WebExpress.WebApp.Wql;
using WebExpress.WebAttribute;
using WebExpress.WebResource;

namespace InventoryExpress.WebApi.V1
{
    /// <summary>
    /// Ermittelt alle Hersteller
    /// </summary>
    [Id("RestManufacturersV1")]
    [Segment("manufacturers", "")]
    [ContextPath("/api/v1")]
    [IncludeSubPaths(true)]
    [Module("inventoryexpress")]
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
        /// <returns>Eine Aufzählung, welche JsonSerializer serialisiert werden kann.</returns>
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
        /// <param name="wql">Der Filter</param>
        /// <param name="request">The request.</param>
        /// <returns>Eine Aufzählung, welche JsonSerializer serialisiert werden kann.</returns>
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
