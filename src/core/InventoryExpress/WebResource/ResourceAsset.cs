﻿using WebExpress.WebAttribute;
using WebExpress.WebResource;

namespace InventoryExpress.WebResource
{
    /// <summary>
    /// Lieferung einer im Assamby eingebetteten Ressource
    /// </summary>
    [Id("Asset")]
    [Title("Assets")]
    [Segment("assets", "")]
    [Path("/")]
    [IncludeSubPaths(true)]
    [Module("inventoryexpress")]
    public sealed class ResourceAsset : WebExpress.WebResource.ResourceAsset
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ResourceAsset()
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
    }
}
