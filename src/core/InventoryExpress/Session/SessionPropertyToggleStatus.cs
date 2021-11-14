using WebExpress.Session;

namespace InventoryExpress.Session
{
    public class SessionPropertyToggleStatus : SessionProperty
    {
        /// <summary>
        /// Bestimmt die Ansicht
        /// </summary>
        public bool ViewList { get; set; } = true;
    }
}
