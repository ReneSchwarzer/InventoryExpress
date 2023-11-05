using System.Text.Json.Serialization;
using WebExpress.WebApp.Model;

namespace InventoryExpress.Model.WebItems
{
    public class WebItemDbInfo : WebItem
    {
        /// <summary>
        /// Returns or sets the provider name.
        /// </summary>
        [JsonPropertyName("providername")]
        public string ProviderName { get; set; }

        /// <summary>
        /// Returns or sets the data source.
        /// </summary>
        [JsonPropertyName("datasource")]
        public string DataSource { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public WebItemDbInfo()
        {
        }
    }
}
