using System.Text.Json.Serialization;
using WebExpress.WebApp.Model;

namespace InventoryExpress.Model.WebItems
{
    public class WebItemDbInfo : WebItem
    {
        /// <summary>
        /// Liefert oder setzt den Provider
        /// </summary>
        [JsonPropertyName("providername")]
        public string ProviderName { get; set; }

        /// <summary>
        /// Returns or sets the data.quelle
        /// </summary>
        [JsonPropertyName("datasource")]
        public string DataSource { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public WebItemDbInfo()
        {
        }
    }
}
