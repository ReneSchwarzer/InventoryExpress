using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace InventoryExpress.Model.WebItems
{
    /// <summary>
    /// It will compare if they are two identical attributes. There is no comparison of values. 
    /// </summary>
    public class WebItemEntityInventoryAttributeComparer : IEqualityComparer<WebItemEntityInventoryAttribute>
    {
        /// <summary>
        /// Compares two inventory attributes.
        /// </summary>
        /// <param name="x">The first attribute.</param>
        /// <param name="y">The second attribute.</param>
        /// <returns>True if the equality exists, false otherwise.</returns>
        public bool Equals(WebItemEntityInventoryAttribute x, WebItemEntityInventoryAttribute y)
        {
            return x.Id == y.Id;
        }

        /// <summary>
        /// Returns the hash code.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        /// <returns>The hash code.</returns>
        public int GetHashCode([DisallowNull] WebItemEntityInventoryAttribute attribute)
        {
            return attribute.Id.GetHashCode();
        }
    }
}
