namespace InventoryExpress.Model.Entity
{
    /// <summary>
    /// Journal parameters of an inventory.
    /// </summary>
    public partial class InventoryJournalParameter
    {
        /// <summary>
        /// Returns or sets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Returns or sets the inventory journal id.
        /// </summary>
        public int InventoryJournalId { get; set; }

        /// <summary>
        /// Returns or sets the parameter name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Returns or sets the old value.
        /// </summary>
        public string OldValue { get; set; }

        /// <summary>
        /// Returns or sets the new value.
        /// </summary>
        public string NewValue { get; set; }

        /// <summary>
        /// Returns or sets the guid.
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        ///  Returns or sets the reference  to the journal.
        /// </summary>
        public virtual InventoryJournal InventoryJournal { get; set; }
    }
}
