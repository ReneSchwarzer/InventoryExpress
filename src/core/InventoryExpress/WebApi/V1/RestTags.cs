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
    /// Ermittelt alle Standorte
    /// </summary>
    [ID("RestTagsV1")]
    [Segment("tags", "")]
    [Path("/api/v1")]
    [IncludeSubPaths(true)]
    [Module("inventoryexpress")]
    public sealed class RestTags : ResourceRestCrud<WebItem>
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public RestTags()
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
                new ResourceRestCrudColumn(I18N(request, "inventoryexpress:inventoryexpress.tags.label"))
                {
                    Render = "return item.label;",
                    Width = null
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
            lock (ViewModel.Instance.Database)
            {
                var tags = ViewModel.Instance.Tags.Select(x => new WebItem
                {
                    ID = x.Label,
                    Label = x.Label
                });

                tags = wql.Apply(tags);


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
