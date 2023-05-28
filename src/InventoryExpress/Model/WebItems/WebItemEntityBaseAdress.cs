using InventoryExpress.Model.Entity;
using System.Text.Json.Serialization;

namespace InventoryExpress.Model.WebItems
{
    public class WebItemEntityBaseAddress : WebItemEntityBaseTag
    {
        /// <summary>
        /// Die Aaddresse
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; }

        /// <summary>
        /// Die Postleitzahl
        /// </summary>
        [JsonPropertyName("zip")]
        public string Zip { get; set; }

        /// <summary>
        /// Der Ort
        /// </summary>
        [JsonPropertyName("place")]
        public string Place { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public WebItemEntityBaseAddress()
        {

        }

        /// <summary>
        /// Copy-Konstruktor
        /// Erstellt eine Tiefenkopie.
        /// </summary>
        /// <param name="item">Das zu kopierende Objekt</param>
        public WebItemEntityBaseAddress(WebItemEntityBaseAddress item)
            : base(item)
        {
            Address = item.Address;
            Zip = item.Zip;
            Place = item.Place;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="item">Das Datenbankobjektes des Herstellers</param>
        public WebItemEntityBaseAddress(ItemAddress item)
            : base(item)
        {
            Address = item.Address;
            Place = item.Place;
            Zip = item.Zip;
        }
    }
}
