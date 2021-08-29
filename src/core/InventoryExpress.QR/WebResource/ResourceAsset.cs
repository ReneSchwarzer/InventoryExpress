using WebExpress.Attribute;

namespace InventoryExpress.QR.WebResource
{
    /// <summary>
    /// Lieferung einer im Assamby eingebetteten Ressource
    /// </summary>
    [ID("Asset")]
    [Title("Assets")]
    [Segment("assets", "")]
    [Path("/")]
    [IncludeSubPaths(true)]
    [Module("InventoryExpress.QR")]
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
