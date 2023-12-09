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
    /// Returns all tags.
    /// </summary>
    [Segment("tags", "")]
    [ContextPath("/api/v1")]
    [IncludeSubPaths(true)]
    [Module<Module>]
    public sealed class RestTags : ResourceRestCrud<WebItemEntityTag>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public RestTags()
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
                new ResourceRestCrudColumn(InternationalizationManager.I18N(request, "inventoryexpress:inventoryexpress.tags.label"))
                {
                    Render = "return item.label;",
                    Width = null
                }
            };
        }

        /// <summary>
        /// Processing of the resource that was called via the get request.
        /// </summary>
        /// <param name="wql">The filtering and sorting options.</param>
        /// <param name="request">The request.</param>
        /// <returns>An enumeration of which json serializer can be serialized.</returns>
        public override IEnumerable<WebItemEntityTag> GetData(IWqlStatement<WebItemEntityTag> wql, Request request)
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
            //    Id = x.Label,
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
            //                Id = s?.ToLower(),
            //                Label = s?.ToLower(),
            //                Instruction = I18N(request, "inventoryexpress:inventoryexpress.add.label")
            //            });
            //        }
            //    }
            //}

            return tags;
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
