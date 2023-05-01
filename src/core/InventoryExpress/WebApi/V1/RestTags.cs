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
    /// Ermittelt alle Standorte
    /// </summary>
    [WebExID("RestTagsV1")]
    [WebExSegment("tags", "")]
    [WebExContextPath("/api/v1")]
    [WebExIncludeSubPaths(true)]
    [WebExModule("inventoryexpress")]
    public sealed class RestTags : ResourceRestCrud<WebItem>
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public RestTags()
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
                new ResourceRestCrudColumn(InternationalizationManager.I18N(request, "inventoryexpress:inventoryexpress.tags.label"))
                {
                    Render = "return item.label;",
                    Width = null
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
            var tags = ViewModel.GetTags(wql);

            //if (id != null)
            //{
            //    locations = locations.Where(x => x.Id.Equals(id));
            //}

            //if (!string.IsNullOrWhiteSpace(search))
            //{
            //    var split = search.Split(' ', System.StringSplitOptions.RemoveEmptyEntries);
            //    locations = locations.Where
            //    (
            //        x =>
            //        split.Contains(x.Label.ToLower())
            //    );
            //}

            //var x = locations.Select(x => (object)new
            //{
            //    ID = x.Label,
            //    Label = x.Label
            //}).ToList();

            //if (!string.IsNullOrWhiteSpace(search))
            //{
            //    foreach (var s in search.Split(' ', System.StringSplitOptions.RemoveEmptyEntries))
            //    {
            //        if (!locations.Where(x => x.Label.ToLower().Equals(s?.ToLower())).Any())
            //        {
            //            x.Add((object)new
            //            {
            //                ID = s?.ToLower(),
            //                Label = s?.ToLower(),
            //                Instruction = I18N(request, "inventoryexpress:inventoryexpress.add.label")
            //            });
            //        }
            //    }
            //}

            return tags;
        }

        /// <summary>
        /// Processing of the resource. des DELETE-Request
        /// </summary>
        /// <param name="id">Die zu löschende ID</param>
        /// <param name="request">The request.</param>
        /// <returns>Das Ergebnis der Löschung</returns>
        public override bool DeleteData(string id, Request request)
        {
            return true;
        }
    }
}
