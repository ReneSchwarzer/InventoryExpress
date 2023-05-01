using WebExpress.WebAttribute;

namespace InventoryExpress.QR.WebResource
{
    /// <summary>
    /// Lieferung einer im Assamby eingebetteten Ressource
    /// </summary>
    [WebExID("Asset")]
    [WebExTitle("Assets")]
    [WebExSegment("assets", "")]
    [WebExContextPath("/")]
    [WebExIncludeSubPaths(true)]
    [WebExModule("InventoryExpress.QR")]
    public sealed class ResourceAsset : WebExpress.WebResource.ResourceAsset
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ResourceAsset()
        {
        }
    }
}
