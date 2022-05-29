using InventoryExpress.Model.Entity;
using WebExpress.WebApp.Model;

namespace InventoryExpress.Model.WebItems
{
    /// <summary>
    /// Attribut
    /// </summary>
    public class WebItemEntitySettings : WebItem
    {
        /// <summary>
        /// Die Währung
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public WebItemEntitySettings()
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="setting">Das Datenbankobjektes der Einstellungen</param>
        public WebItemEntitySettings(Setting setting)
        {
            Currency = setting.Currency;
        }
    }
}
