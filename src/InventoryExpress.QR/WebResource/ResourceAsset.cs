using WebExpress.WebCore.WebAttribute;

namespace InventoryExpress.QR.WebResource
{
    /// <summary>
    /// Returns of a resource embedded in the Assamby.
    /// </summary>
    [Title("Assets")]
    [Segment("assets", "")]
    [ContextPath("/")]
    [IncludeSubPaths(true)]
    [Module<Module>()]
    public sealed class ResourceAsset : WebExpress.WebCore.WebResource.ResourceAsset
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ResourceAsset()
        {
        }
    }
}
