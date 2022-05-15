using InventoryExpress.Model.Entity;

namespace InventoryExpress.Model.WebItems
{
    public class WebItemEntityBaseAddress : WebItemEntityBaseTag
    {
        /// <summary>
        /// Die Aaddresse
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Die Postleitzahl
        /// </summary>
        public string Zip { get; set; }

        /// <summary>
        /// Der Ort
        /// </summary>
        public string Place { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public WebItemEntityBaseAddress()
        {

        }

        /// <summary>
        /// Konstruktor
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
