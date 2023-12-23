namespace InventoryExpress.Model.Entity
{
    internal class ItemAddress : ItemTag
    {
        /// <summary>
        /// Returns or sets the address.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Returns or sets the zip.
        /// </summary>
        public string Zip { get; set; }

        /// <summary>
        /// Returns or sets the place.
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
