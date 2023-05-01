﻿using InventoryExpress.Model;
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
    /// Ermittelt alle Vorlagen
    /// </summary>
    [WebExID("RestTemplatesV1")]
    [WebExSegment("templates", "")]
    [WebExContextPath("/api/v1")]
    [WebExIncludeSubPaths(true)]
    [WebExModule("inventoryexpress")]
    public sealed class RestTemplates : ResourceRestCrud<WebItem>
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
                new ResourceRestCrudColumn(InternationalizationManager.I18N(request, "inventoryexpress:inventoryexpress.templates.label"))
                {
                    Render = "return item.label;",
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
        public override IEnumerable<WebItem> GetData(WqlStatement wql, Request request)
        {
            var templates = ViewModel.GetTemplates(wql);

            return templates;
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
