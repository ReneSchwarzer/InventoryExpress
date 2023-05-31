using WebExpress.WebAttribute;

namespace InventoryExpress.QR.WebResource
{
    /// <summary>
    /// Returns of a resource embedded in the Assamby.
    /// </summary>
    [WebExTitle("Assets")]
    [WebExSegment("assets", "")]
    [WebExContextPath("/")]
    [WebExIncludeSubPaths(true)]
    [WebExModule<Module>()]
    public sealed class ResourceAsset : WebExpress.WebResource.ResourceAsset
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ResourceAsset()
        {
        }
    }
}
