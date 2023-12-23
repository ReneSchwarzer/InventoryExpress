using InventoryExpress.Model.Entity;
using System.Text.Json.Serialization;
using WebExpress.WebApp.Model;

namespace InventoryExpress.Model.WebItems
{
    /// <summary>
    /// The settings.
    /// </summary>
    public class WebItemEntitySettings : WebItem
    {
        /// <summary>
        /// Returns or sets the currency.
        /// </summary>
        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public WebItemEntitySettings()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="setting">The database object of the setting.</param>
        internal WebItemEntitySettings(Setting setting)
        {
            Currency = setting.Currency;
        }
    }
}
