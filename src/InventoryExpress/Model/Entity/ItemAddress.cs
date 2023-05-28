namespace InventoryExpress.Model.Entity
{
    public class ItemAddress : ItemTag
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
        /// Constructor
        /// </summary>
        public ItemAddress()
        {
        }
    }
}
