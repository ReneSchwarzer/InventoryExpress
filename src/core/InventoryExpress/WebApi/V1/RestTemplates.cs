using InventoryExpress.Model;
using InventoryExpress.Model.Entity;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Message;
using WebExpress.WebApp.WebResource;
using WebExpress.WebAttribute;
using WebExpress.WebResource;
using static WebExpress.Internationalization.InternationalizationManager;

namespace InventoryExpress.WebApi.V1
{
    /// <summary>
    /// Ermittelt alle Vorlagen
    /// </summary>
    [ID("RestTemplatesV1")]
    [Segment("templates", "")]
    [Path("/api/v1")]
    [IncludeSubPaths(true)]
    [Module("inventoryexpress")]
    public sealed class RestTemplates : ResourceRestCrud
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public RestTemplates()
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
                new ResourceRestCrudColumn(I18N(request, "inventoryexpress:inventoryexpress.templates.label"))
                {
                    Render = "return item.Label;",
                    Width = 5
                }
            };
        }

        /// <summary>
        /// Verarbeitung des GET-Request
        /// </summary>
        /// <param name="id">Die ID oder null wenn nicht gefiltert werden soll</param>
        /// <param name="search">Ein Suchstring oder null wenn nicht gefiltert werden soll</param>
        /// <param name="request">Die Anfrage</param>
        /// <returns>Eine Aufzählung, welche JsonSerializer serialisiert werden kann.</returns>
        public override IEnumerable<object> GetData(string id, string search, Request request)
        {
            lock (ViewModel.Instance.Database)
            {
                var templates = ViewModel.Instance.Templates as IEnumerable<Template>;

                if (id != null)
                {
                    templates = templates.Where(x => x.Id.Equals(id));
                }

                if (search != null)
                {
                    templates = templates.Where
                    (
                        x =>
                        x.Name.Contains(search, System.StringComparison.OrdinalIgnoreCase)
                    );
                }

                return templates.Select(x => (object)new
                {
                    ID = x.Guid,
                    Label = x.Name
                }).ToList();
            }
        }

        /// <summary>
        /// Verarbeitung des DELETE-Request
        /// </summary>
        /// <param name="request">Die Anfrage</param>
        /// <returns>Das Ergebnis der Löschung</returns>
        public override bool DeleteData(Request request)
        {
            return true;
        }
    }
}
