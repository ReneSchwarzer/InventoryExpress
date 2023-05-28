﻿using WebExpress.WebAttribute;
using WebExpress.WebResource;

namespace InventoryExpress.WebResource
{
    /// <summary>
    /// Delivery of a resource embedded in the assembly.
    /// </summary>
    [WebExTitle("Assets")]
    [WebExSegment("assets", "")]
    [WebExContextPath("/")]
    [WebExIncludeSubPaths(true)]
    [WebExModule(typeof(Module))]
    public sealed class ResourceAsset : WebExpress.WebResource.ResourceAsset
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ResourceAsset()
        {
        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The context of the resource.</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);
        }
    }
}