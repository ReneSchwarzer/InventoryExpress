using WebExpress.WebSession;

namespace InventoryExpress.WebSession
{
    public class SessionPropertyToggleStatus : SessionProperty
    {
        /// <summary>
        /// Bestimmt die Ansicht
        /// </summary>
        public bool ViewList { get; set; } = true;
    }
}
