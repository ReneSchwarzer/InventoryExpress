using InventoryExpress.Model.Entity;

namespace InventoryExpress.Model.WebItems
{
    /// <summary>
    /// The attribut.
    /// </summary>
    public class WebItemEntityAttribute : WebItemEntity
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public WebItemEntityAttribute()
        {
        }

        /// <summary>
        /// Copy-Konstruktor
        /// Creates a deep copy.
        /// </summary>
        /// <param name="item">The object to be copied.</param>
        public WebItemEntityAttribute(WebItemEntityAttribute item)
            : base(item)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="attribute">The database object of the attribute.</param>
        internal WebItemEntityAttribute(Attribute attribute)
            : base(attribute)
        {
            Uri = ViewModel.GetUri(this).Append(attribute.Guid);
        }
    }
}
