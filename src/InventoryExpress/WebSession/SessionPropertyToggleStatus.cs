using WebExpress.WebCore.WebSession;

namespace InventoryExpress.WebSession
{
    public class SessionPropertyToggleStatus : SessionProperty
    {
        /// <summary>
        /// Determines the view
        /// </summary>
        public bool ViewList { get; set; } = true;
    }
}
