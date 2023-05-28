namespace InventoryExpress.Model.Entity
{
    /// <summary>
    /// Zuordnung der Schlüsselwörter zu den Inventargegenständen
    /// </summary>
    public partial class InventoryTag
    {
        /// <summary>
        /// Das Inventar
        /// </summary>
        public int InventoryId { get; set; }

        /// <summary>
        /// Die Id des Schlüsselwortes
        /// </summary>
        public int TagId { get; set; }

        /// <summary>
        /// Verweis auf den Inventargegenstand
        /// </summary>
        public virtual Inventory Inventory { get; set; }

        /// <summary>
        /// Verweis auf das Schlüsselwort
        /// </summary>
        public virtual Tag Tag { get; set; }
    }
}
